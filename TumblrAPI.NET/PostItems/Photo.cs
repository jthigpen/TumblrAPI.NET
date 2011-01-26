using System.Collections.Generic;

namespace TumblrAPI.PostItems
{
	public class Photo : PostItemBase
	{
		public Photo()
		{

		}
		public Photo(string source)
			: this(source, null, null)
		{
		}

		public Photo(string source, string caption)
			: this(source, caption, null)
		{
		}

		public Photo(string source, string caption, string clickThroughUrl)
		{
			Source = source;
			Caption = caption;
			ClickThroughUrl = clickThroughUrl;
		}

		/// <summary>
		/// The URL of the photo to copy. This must be a web-accessible URL, not
		/// a local file or intranet location.
		/// </summary>
		/// <remarks>
		/// Either the Source or Data property must be set, if both are set
		/// Source will be used.
		/// </remarks>
		public string Source { get; set; }

		/// <summary>
		/// The caption for the photo.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// This item supports HTML content.
		/// </remarks>
		public string Caption { get; set; }

		/// <summary>
		/// The click through link for the image
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// This is the link that will be visited when uses click on the image.
		/// </remarks>
		public string ClickThroughUrl { get; set; }

		/// <summary>
		/// The filepath location on the hard drive for the file to be uploaded.
		/// </summary>
		/// <remarks>
		/// Either the Source or FilePath property must be set, if both are set
		/// Source will be used.
		/// </remarks>
		public string FilePath { get; set; }

		protected override Dictionary<string, string> GetPostItemsInternal()
		{
			var parameters = new Dictionary<string, string> 
			{
				{PostItemParameters.Type, PostItemType.Photo},
				{PostItemParameters.Caption, Caption},
				{PostItemParameters.ClickThroughUrl, ClickThroughUrl},
			};
			if (string.IsNullOrEmpty(Source) && !string.IsNullOrEmpty(FilePath))
			{
				parameters.Add(PostItemParameters.Data, FilePath);
			}
			else
			{
				parameters.Add(PostItemParameters.Source, Source);
			}
			return parameters;
		}
	}
}
