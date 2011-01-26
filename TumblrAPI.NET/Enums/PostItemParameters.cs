namespace TumblrAPI
{
	public class PostItemParameters
	{
		/// <summary>
		/// Applies to All
		/// </summary>
		public const string Type = "type";

		/// <summary>
		/// Applies to All
		/// </summary>
		public const string Email = "email";

		/// <summary>
		/// Applies to All
		/// </summary>
		public const string Password = "password";

		/// <summary>
		/// Applies to All
		/// </summary>
		public const string Generator = "generator";

		/// <summary>
		/// Applies to Tumblr "actions"
		/// </summary>
		/// <remarks>
		/// Used for authentication, requesting user permissions, etc.
		/// </remarks>
		public const string Action = "action";

		/// <summary>
		/// Applies to Text, Chat, and Video
		/// </summary>
		public const string Title = "title";

		/// <summary>
		/// Applies to Text
		/// </summary>
		public const string Body = "body";

		/// <summary>
		/// Applies to Photo, and Quote
		/// </summary>
		public const string Source = "source";

		/// <summary>
		/// Applies to Photo, Video, and Audio
		/// </summary>
		public const string Data = "data";

		/// <summary>
		/// Applies to Photo, Video, and Audio
		/// </summary>
		public const string Caption = "caption";

		/// <summary>
		/// Applies to Photo
		/// </summary>
		public const string ClickThroughUrl = "click-through-url";

		/// <summary>
		/// Applies to Quote
		/// </summary>
		public const string Quote = "quote";

		/// <summary>
		/// Applies to Link
		/// </summary>
		public const string Name = "name";

		/// <summary>
		/// Applies to Link
		/// </summary>
		public const string Url = "url";

		/// <summary>
		/// Applies to Link
		/// </summary>
		public const string Description = "description";

		/// <summary>
		/// Applies to Chat
		/// </summary>
		public const string Conversation = "conversation";

		/// <summary>
		/// Applies to Video
		/// </summary>
		public const string Embed = "embed";

		/// <summary>
		/// Applies to Audio
		/// </summary>
		public const string ExternallyHostedUrl = "externally-hosted-url";
	}
}
