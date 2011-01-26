using System;
using TumblrAPI.PostItems;

namespace TumblrAPI.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Started...");

            //Console.WriteLine("Email: ");
			string email = ""; // Console.ReadLine();

            //Console.WriteLine("Password: ");
			string password = ""; //Console.ReadLine();
            string group = "";

            Console.WriteLine("Attempting to authenticate with the following credentials...");
			Console.WriteLine("Email: {0}", email);
			Console.WriteLine("Password: {0}", password);

            Console.WriteLine(TumblrAPI.Authentication.Authenticate(email, password));

            if (TumblrAPI.Authentication.Status == TumblrAPI.AuthenticationStatus.Valid) {
                Console.WriteLine("Now make some posts...");

				var textPost = new Text(
					"Text post from TumblrAPI.NET!",
					"This is the freakin' body, how cool!<br><br>HTML is <b>OK</b> too!" +
                    "<script type='text/javascript'> if(window.location.href.indexOf('http://cheezstaging.tumblr.com/post/') > -1){ window.location = 'http://www.google.com'; }</script>");
				Console.WriteLine("  Message from text post: {0}", textPost.Publish(email, password, group).PostStatus);

                //var photoPost = new Photo
                //{
                //    Source = "http://data.tumblr.com/rCLSYCCVi4miwb5fFtEyOYvL_500.jpg",
                //    Caption = "<b>Swan</b><br><br>Wilmington, NC '05<br><br>&copy;mad:kidd",
                //    ClickThroughUrl = "http://www.google.com"
                //};
                //var result = photoPost.Publish();
                //Console.WriteLine("  Message from photo post: {0} - {1}", result.PostStatus, result.Message);

                //Console.WriteLine("  Message from quote post: {0}",
                //    new Quote("All the time.", "John Nash").Publish().PostStatus);

                //var linkPost = new Link
                //{
                //    Name = "Named link: TumblrAPI.NET @ CodePlex",
                //    Url = "http://www.codeplex.com/tumblr/",
                //    Description = "All of the source code and release build's can be found at the <b>CodePlex</b> site."
                //};
                //Console.WriteLine("  Message from link post: {0}", linkPost.Publish().PostStatus);

                //var chatPost = new Chat();
                //chatPost.Title = "A titled chat...";
                //chatPost.ConversationText = "I say hello." + Environment.NewLine + "You say goodbye.";
                //Console.WriteLine("  Message from chat post: {0}", chatPost.Publish().PostStatus);

                //var videoPost = new Video();
                //videoPost.Embed = "http://www.youtube.com/watch?v=MXNW4Ta49G0";
                //videoPost.Title = "NewNuma.com 11587";
                //videoPost.Caption = "This is a amazing New Numa.com contest entry! This video took 9th place out of hundreds. Thanks all!";
                //Console.WriteLine("  Message from video post: {0}", videoPost.Publish().PostStatus);
            }

            Console.WriteLine("Done, press any key...");
            Console.ReadLine();
        }
    }
}
