using System.Collections.Generic;
using System.IO;
using System.Xml;
using TumblrAPI.Properties;

namespace TumblrAPI
{
	public class Authentication
	{
		internal static string Email{get;private set;}
		internal static string Password{get;private set;}
		public static AuthenticationStatus Status { get; set; }

		public static AuthenticationStatus Authenticate(string email, string password)
		{
			Email = email;
			Password = password;
			var authRequest = new HttpHelper(
				Settings.Default.API_URL,
				new Dictionary<string, string>
				{
					{ PostItemParameters.Email, Email },
					{ PostItemParameters.Password, Password },
					{ PostItemParameters.Action, "authenticate" }
				});
			var result = authRequest.Post();
			ParseRequest(result.Message);
			Status = result.PostStatus == PostStatus.Created
				? Status = AuthenticationStatus.Valid
				: Status = AuthenticationStatus.Invalid;
			return Status;
		}

		private static UserInformation ParseRequest(string xmlResponse)
		{
			var userInfo = new UserInformation();
			using (XmlReader reader = XmlReader.Create(new StringReader(xmlResponse)))
			{
				var ws = new XmlWriterSettings();
				ws.Indent = true;
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
					{
						//This would have been much nicer and cleaner with Linq to Xml but 
						//I figured it was probably more important to keep it pointed at .NET 2.0
						for (int i = 0; i < reader.AttributeCount; i++)
						{
							reader.MoveToAttribute(i);
							#region Ugly switch statement to set properties
							switch (reader.Name.ToLowerInvariant())
							{
								case "default-post-format":
									userInfo.DefaultPostFormat = reader.Value;
									break;
								case "can-upload-audio":
									userInfo.CanUploadAudio = ParseBool(reader.Value);
									break;
								case "can-upload-aiff":
									userInfo.CanUploadAiff = ParseBool(reader.Value);
									break;
								case "can-ask-question":
									userInfo.CanAskQuestion = ParseBool(reader.Value);
									break;
								case "can-upload-video":
									userInfo.CanUploadVideo = ParseBool(reader.Value);
									break;
								case "max-video-bytes-uploaded":
									userInfo.MaxVideoBytesUploaded = ParseLong(reader.Value);
									break;
								case "liked-post-count":
									userInfo.LikedPostCount = ParseInt(reader.Value);
									break;
								case "title":
									userInfo.TumblrLog.Title = reader.Value;
									break;
								case "is-admin":
									userInfo.TumblrLog.IsAdmin = ParseBool(reader.Value);
									break;
								case "posts":
									userInfo.TumblrLog.Posts = ParseInt(reader.Value);
									break;
								case "twitter-enabled":
									userInfo.TumblrLog.IsTwitterEnabled = ParseBool(reader.Value);
									break;
								case "draft-count":
									userInfo.TumblrLog.DraftCount = ParseInt(reader.Value);
									break;
								case "messages-count":
									userInfo.TumblrLog.MessageCount = ParseInt(reader.Value);
									break;
								case "queue-count":
									userInfo.TumblrLog.QueueCount = ParseInt(reader.Value);
									break;
								case "name":
									userInfo.TumblrLog.Name = reader.Value;
									break;
								case "url":
									userInfo.TumblrLog.Url = reader.Value;
									break;
								case "type":
									userInfo.TumblrLog.Type = reader.Value;
									break;
								case "followers":
									userInfo.TumblrLog.Followers = ParseInt(reader.Value);
									break;
								case "avatar-url":
									userInfo.TumblrLog.AvatarUrl = reader.Value;
									break;
								case "is-primary":
									userInfo.TumblrLog.IsPrimary = ParseBool(reader.Value);
									break;
								case "backup-post-limit":
									userInfo.TumblrLog.BackUpPostLimit = ParseInt(reader.Value);
									break;
							} 
							#endregion
						}
					}
				}
			}
			return userInfo;
		}

		private static bool ParseBool(string str)
		{
			if (str.ToLowerInvariant() == "yes")
				return true;

			int value = ParseInt(str);
			return value == 1;
		}

		private static int ParseInt(string str)
		{
			int value;
			int.TryParse(str, out value);
			return value;
		}

		private static long ParseLong(string str)
		{
			long value;
			long.TryParse(str, out value);
			return value;
		}
	}
}