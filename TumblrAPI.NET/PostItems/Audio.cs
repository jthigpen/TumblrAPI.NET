using System.Collections.Generic;

namespace TumblrAPI.PostItems
{
	public class Audio : PostItemBase
	{
		/// <summary>
		/// The url of an audio file that stored on an external location.
		/// </summary>
		/// <remarks>
		/// Either the ExternallyHostedUrl or FilePath property must be set.
		/// 
		/// Must be MP3 format.
		/// </remarks>
		public string ExternallyHostedUrl { get; set; }

		/// <summary>
		/// The caption for the photo.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// This item supports HTML content.
		/// </remarks>
		public string Caption { get; set; }

		/// <summary>
		/// The filepath location on the hard drive for the file to be uploaded.
		/// </summary>
		/// <remarks>
		/// Either the ExternallyHostedUrl or FilePath property must be set.
		/// 
		/// Must be MP3 or AIFF format.
		/// </remarks>
		public string FilePath { get; set; }

		protected override Dictionary<string, string> GetPostItemsInternal()
		{
			var parameters = new Dictionary<string, string> 
			{
				{PostItemParameters.Type, PostItemType.Audio},
				{PostItemParameters.Caption, Caption},
			};
			if (string.IsNullOrEmpty(ExternallyHostedUrl) && !string.IsNullOrEmpty(FilePath))
			{
				parameters.Add(PostItemParameters.Data, FilePath);
			}
			else
			{
				parameters.Add(PostItemParameters.Source, ExternallyHostedUrl);
			}
			return parameters;
		}
	}
}