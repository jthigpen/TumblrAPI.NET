using System.Collections.Generic;

namespace TumblrAPI.PostItems
{
	public class Quote : PostItemBase
	{
		public Quote()
		{
		}

		public Quote(string quoteText)
			: this(quoteText, null)
		{
		}

		public Quote(string quoteText, string source)
		{
			QuoteText = quoteText;
			Source = source;
		}

		/// <summary>
		/// The text of the quote.
		/// </summary>
		public string QuoteText { get; set; }

		/// <summary>
		/// The source of the quote.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// This item supports HTML content.
		/// </remarks>
		public string Source { get; set; }

		protected override Dictionary<string, string> GetPostItemsInternal()
		{
			return new Dictionary<string, string>
			{
				{ PostItemParameters.Type, PostItemType.Quote },
				{ PostItemParameters.Quote, QuoteText },
				{ PostItemParameters.Source, Source }
			};
		}
	}
}