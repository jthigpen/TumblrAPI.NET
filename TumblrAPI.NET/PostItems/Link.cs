using System.Collections.Generic;

namespace TumblrAPI.PostItems
{
	public class Link : PostItemBase
	{
		public Link()
		{
		}

		public Link(string url)
			: this(url, null, null)
		{
		}

		public Link(string url, string name)
			: this(url, name, null)
		{
		}

		public Link(string url, string name, string description)
		{
			Url = url;
			Name = name;
			Description = description;
		}

		/// <summary>
		/// The description of the link.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// This item supports HTML content.
		/// </remarks>
		public string Description { get; set; }

		/// <summary>
		/// The name of the link.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// </remarks>
		public string Name { get; set; }

		/// <summary>
		/// The link URL
		/// </summary>
		public string Url { get; set; }

		protected override Dictionary<string, string> GetPostItemsInternal()
		{
			return new Dictionary<string, string>
			{
				{ PostItemParameters.Type, PostItemType.Link },
				{ PostItemParameters.Name, Name },
				{ PostItemParameters.Url, Url },
				{ PostItemParameters.Description, Description },
			};
		}
	}
}