using System.Collections.Generic;

namespace TumblrAPI.PostItems
{
	public class Chat : PostItemBase
	{
		public Chat()
		{
		}

		public Chat(string converstation)
			: this(converstation, null)
		{
		}

		public Chat(string conversation, string title)
		{
			Title = title;
			ConversationText = conversation;
		}

		/// <summary>
		/// The conversation
		/// </summary>
		/// <remarks>
		/// The speakers alternate per line.
		/// </remarks>
		public string ConversationText { get; set; }

		/// <summary>
		/// The title of the conversation.
		/// </summary>
		/// <remarks>
		/// This item is optional.
		/// </remarks>
		public string Title { get; set; }

		protected override Dictionary<string, string> GetPostItemsInternal()
		{
			return new Dictionary<string, string>
			{
				{ PostItemParameters.Type, PostItemType.Conversation },
				{ PostItemParameters.Title , Title },
				{ PostItemParameters.Conversation, ConversationText }
			};
		}
	}
}