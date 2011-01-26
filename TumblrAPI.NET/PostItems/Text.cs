using System.Collections.Generic;

namespace TumblrAPI.PostItems
{
	/// <summary>
	/// Traditional text blog post. At least one, title or body, is required.
	/// </summary>
	public class Text : PostItemBase
	{
		public Text()
		{
		}

		public Text(string title)
			: this(title, null)
		{
		}

		public Text(string title, string body)
		{
			Title = title;
			Body = body;
		}

		/// <summary>
		/// The body of the post.
		/// </summary>
		/// <remarks>
		/// This item supports HTML content.
		/// Either Title or Body is required;
		/// </remarks>
		public string Body { get; set; }

		/// <summary>
		/// The title of the post.
		/// </summary>
		/// Either Title or Body is required;
		public string Title { get; set; }

		protected override Dictionary<string, string> GetPostItemsInternal()
		{
			return new Dictionary<string, string>
			{
				{ PostItemParameters.Type, PostItemType.Text },
				{ PostItemParameters.Title, Title },
				{ PostItemParameters.Body, Body }
			};
		}
	}
}