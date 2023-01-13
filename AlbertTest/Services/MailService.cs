using System;
using System.Threading.Tasks;
using Albert.BackendChallenge.Utils;

namespace Albert.BackendChallenge.Services
{
    public class MailService
    {
        public static async Task SendAsync(string userId)
        {
            await ThreadingUtils.SleepRandomAsync(3000, 10000);

            if (new Random((int)DateTime.Now.Ticks).NextDouble() > 0.5)
            {
                throw new Exception("Failed to connect to the mail server");
            }

            Console.WriteLine($"[*] A mail was sent to the user with id {userId}");
        }
    }
}
