namespace CityInfo.Api.Services
{
    public class LocalMailService
    {
        private string _mailTo = "";
        private string _mailFrom = "";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
