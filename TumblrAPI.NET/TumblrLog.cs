namespace TumblrAPI
{
	public class TumblrLog
	{
		public string Title { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsTwitterEnabled { get; set; }
		public int DraftCount { get; set; }
		public int MessageCount { get; set; }
		public int Posts { get; set; }
		public int QueueCount { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string Type { get; set; }
		public int Followers { get; set; }
		public string AvatarUrl { get; set; }
		public bool IsPrimary { get; set; }
		public int BackUpPostLimit { get; set; }
		public int PrivateId { get; set; }
	}
}