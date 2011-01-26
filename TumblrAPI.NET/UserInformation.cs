namespace TumblrAPI
{
	public class UserInformation
	{
		public UserInformation()
		{
			TumblrLog = new TumblrLog();
		}
		public string DefaultPostFormat { get; set; }
		public bool CanUploadAudio { get; set; }
		public bool CanUploadAiff { get; set; }
		public bool CanUploadVideo { get; set; }
		public bool CanAskQuestion { get; set; }
		public long MaxVideoBytesUploaded { get; set; }
		public int LikedPostCount { get; set; }
		public TumblrLog TumblrLog { get; set; }
	}
}
