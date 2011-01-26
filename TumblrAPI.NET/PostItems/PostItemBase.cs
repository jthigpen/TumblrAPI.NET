using System;
using System.Collections.Generic;
using TumblrAPI.Properties;

namespace TumblrAPI.PostItems
{
	public abstract class PostItemBase
	{
		/// <summary>
		/// The id of the post on Tumblr
		/// </summary>
		public int PostId { get; set; }

		/// <summary>
		/// Gets the <see cref="PostItems"/> that are specific to each subclass.
		/// </summary>
		protected abstract Dictionary<string, string> GetPostItemsInternal();

		/// <summary>
		/// Uses the credentials stored in the <see cref="TumblrAPI.Authentication"/>
		/// class to publish.
		/// </summary>
		public TumblrResult Publish()
		{
			if (TumblrAPI.Authentication.Status == AuthenticationStatus.Valid)
			{
				return Publish(Authentication.Email, Authentication.Password);
			}
			throw new InvalidOperationException(
				"You are not authenticated.  You can call the Connect method or the Publish method and pass in your credentials.");
		}

		/// <summary>
		/// Authenticates against the service for this publish only.
		/// </summary>
		/// <param name="email">Tumblr account email address</param>
		/// <param name="password">Tumblr account password</param>
		public TumblrResult Publish(string email, string password)
		{
			var postItems = new Dictionary<string, string>(GetPostItemsInternal());
			postItems.Add(PostItemParameters.Email, email);
			postItems.Add(PostItemParameters.Password, password);
			postItems.Add(PostItemParameters.Generator, "TumblrAPI.NET");

			var request = new HttpHelper(Settings.Default.API_URL, postItems);

			var result = request.Post();
			int postId;
			if (result.PostStatus == PostStatus.Created
				&& int.TryParse(result.Message, out postId))
			{
				PostId = postId;
			}
			return result;
		}
	}
}
