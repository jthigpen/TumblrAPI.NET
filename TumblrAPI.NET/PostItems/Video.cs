using System.Collections.Generic;

namespace TumblrAPI.PostItems
{
	public class Video : PostItemBase
	{
		public Video()
		{
		}

		public Video(string embed)
			: this(embed, null, null)
		{
		}

		public Video(string embed, string title)
			: this(embed, title, null)
		{
		}

		public Video(string embed, string title, string caption)
		{
			Embed = embed;
			Title = title;
			Caption = caption;
		}

		/// <summary>
		/// The caption for the video.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// This item supports HTML content.
		/// </remarks>
		public string Caption { get; set; }

		/// <summary>
		/// Either the complete HTML code to embed the video, or the URL of a YouTube video page.
		/// </summary>
		/// <remarks>
		/// Either the Embed or FilePath property must be set.
		/// </remarks>
		public string Embed { get; set; }

		/// <summary>
		/// The filepath location on the hard drive for the file to be uploaded.
		/// </summary>
		/// <remarks>
		/// Either the Embed or FilePath property must be set.
		/// </remarks>
		public string FilePath { get; set; }

		/// <summary>
		/// The title of the video.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// </remarks>
		public string Title { get; set; }

		protected override Dictionary<string, string> GetPostItemsInternal()
		{
			var parameters = new Dictionary<string, string> 
			{
				{ PostItemParameters.Type, PostItemType.Video },
				{ PostItemParameters.Embed, Embed },
				{ PostItemParameters.Title, Title },
				{ PostItemParameters.Caption, Caption },
			};
			if (string.IsNullOrEmpty(Embed) && !string.IsNullOrEmpty(FilePath))
			{
				parameters.Add(PostItemParameters.Data, FilePath);
			}
			else
			{
				parameters.Add(PostItemParameters.Source, Embed);
			}
			return parameters;
		}
	}
}