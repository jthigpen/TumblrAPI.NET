//Modification of PostSubmitter class by rakker
//http://geekswithblogs.net/rakker/archive/2006/04/21/76044.aspx

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace TumblrAPI
{
	/// <summary>
	/// Submits post data to a url.
	/// </summary>
	internal class HttpHelper
	{
		private readonly IDictionary<string, string> myPostItems = new Dictionary<string, string>();
		private string myUrl;

		/// <summary>
		/// Constructor allowing the setting of the url and items to post.
		/// </summary>
		/// <param name="url">the url for the post.</param>
		/// <param name="values">The values for the post.</param>
		public HttpHelper(string url, IDictionary<string, string> values)
		{
			myUrl = url;
			myPostItems = values;
		}

		/// <summary>
		/// determines what type of post to perform.
		/// </summary>
		public enum PostType
		{
			Get,
			Post
		}

		/// <summary>
		/// Gets or sets the url to submit the post to.
		/// </summary>
		public string Url
		{
			get { return myUrl; }
			set { myUrl = value; }
		}

		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <returns>a string containing the result of the post.</returns>
		public TumblrResult Post()
		{
			return PostData(myUrl);
		}

		private string EncodePostItems()
		{
			var sb = new StringBuilder();
			foreach (var item in myPostItems)
			{
				if (item.Key != PostItemParameters.Data)
				{
					sb.AppendFormat("{0}={1}&", item.Key, HttpUtility.UrlEncode(item.Value));
				}
			}
			return sb.ToString().TrimEnd('&');
		}

		/// <summary>
		/// Posts data to a specified url. Note that this assumes that you have already url encoded the post data.
		/// </summary>
		/// <param name="url">the url to post to.</param>
		/// <returns>Returns the result of the post.</returns>
		private TumblrResult PostData(string url)
		{
			var uri = new Uri(url);
			var request = (HttpWebRequest)WebRequest.Create(uri);
			request.Method = "POST";
			byte[] postData;
			if (myPostItems.ContainsKey(PostItemParameters.Data))
			{
				var filename = myPostItems[PostItemParameters.Data];
				byte[] data;
				// Read file data
				using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
				{
					data = new byte[fs.Length];
					fs.Read(data, 0, data.Length);
					fs.Close();
				}
				myPostItems.Remove(PostItemParameters.Data);
				// Generate post objects
				var postParameters = new Dictionary<string, object>();
				foreach (var item in myPostItems)
				{
					postParameters.Add(item.Key, item.Value);
				}
				postParameters.Add("data", new FormUpload.FileParameter(data));

				// Create request and receive response
				request = FormUpload.MultipartFormDataPost(myUrl, "TumblrAPI.NET", postParameters);
			}
			else
			{
				postData = new UTF8Encoding().GetBytes(EncodePostItems());
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = postData.Length;
				using (Stream writeStream = request.GetRequestStream())
				{
					writeStream.Write(postData, 0, postData.Length);
				}
			}
			try
			{
				var response = (HttpWebResponse)request.GetResponse();
				var stream = new StreamReader(response.GetResponseStream(), true);
				string content = stream.ReadToEnd();
				response.Close();
				return new TumblrResult
				{
					Message = content,
					PostStatus = PostStatus.Created
				};
			}
			catch (WebException ex)
			{
				PostStatus status;
				var stream = new StreamReader(ex.Response.GetResponseStream(), true);
				string content = stream.ReadToEnd();
				ex.Response.Close();
				var temp = ((HttpWebResponse)ex.Response).StatusCode;
				switch (temp)
				{
					case HttpStatusCode.OK:
					case HttpStatusCode.Created:
						status = PostStatus.Created;
						break;
					case HttpStatusCode.Unauthorized:
					case HttpStatusCode.Forbidden:
						status = PostStatus.Forbidden;
						break;
					case HttpStatusCode.NotFound:
					case HttpStatusCode.BadRequest:
						status = PostStatus.BadRequest;
						break;
					default:
						goto case HttpStatusCode.BadRequest;
				}
				return new TumblrResult 
				{
					Message = content,
					PostStatus = status
				};
			}
			catch (Exception ex)
			{
				return new TumblrResult
				{
					Message = ex.ToString(),
					PostStatus = PostStatus.Unknown
				};
			}
		}
	}

	//http://www.briangrinstead.com/blog/multipart-form-post-in-c
	internal static class FormUpload
	{
		private static readonly Encoding encoding = Encoding.UTF8;
		public static HttpWebRequest MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters)
		{
			string formDataBoundary = "-----------------------------28947758029299";
			string contentType = "multipart/form-data; boundary=" + formDataBoundary;

			byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

			return PostForm(postUrl, userAgent, contentType, formData);
		}
		private static HttpWebRequest PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
		{
			var request = WebRequest.Create(postUrl) as HttpWebRequest;
			if (request == null)
			{
				throw new NullReferenceException("request is not a http request");
			}

			// Set up the request properties
			request.Method = "POST";
			request.ContentType = contentType;
			request.UserAgent = userAgent;
			request.SendChunked = true;
			request.CookieContainer = new CookieContainer();
			request.ContentLength = formData.Length;  // We need to count how many bytes we're sending. 

			using (Stream requestStream = request.GetRequestStream())
			{
				// Push it out there
				requestStream.Write(formData, 0, formData.Length);
				requestStream.Close();
			}

			return request;
		}

		private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
		{
			Stream formDataStream = new System.IO.MemoryStream();

			foreach (var param in postParameters)
			{
				if (param.Value is FileParameter)
				{
					var fileToUpload = (FileParameter)param.Value;

					// Add just the first part of this param, since we will write the file data directly to the Stream
					string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
						boundary,param.Key,fileToUpload.FileName ?? param.Key,
						fileToUpload.ContentType ?? "application/octet-stream");

					formDataStream.Write(encoding.GetBytes(header), 0, header.Length);

					// Write the file data directly to the Stream, rather than serializing it to a string.
					formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
					// Thanks to feedback from commenters, add a CRLF to allow multiple files to be uploaded
					formDataStream.Write(encoding.GetBytes("\r\n"), 0, 2);
				}
				else
				{
					string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n",
						boundary,
						param.Key,
						param.Value);
					formDataStream.Write(encoding.GetBytes(postData), 0, postData.Length);
				}
			}

			// Add the end of the request
			string footer = "\r\n--" + boundary + "--\r\n";
			formDataStream.Write(encoding.GetBytes(footer), 0, footer.Length);

			// Dump the Stream into a byte[]
			formDataStream.Position = 0;
			byte[] formData = new byte[formDataStream.Length];
			formDataStream.Read(formData, 0, formData.Length);
			formDataStream.Close();

			return formData;
		}

		public class FileParameter
		{
			public byte[] File { get; set; }
			public string FileName { get; set; }
			public string ContentType { get; set; }
			public FileParameter(byte[] file) : this(file, null) { }
			public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
			public FileParameter(byte[] file, string filename, string contenttype)
			{
				File = file;
				FileName = filename;
				ContentType = contenttype;
			}
		}
	}
}
