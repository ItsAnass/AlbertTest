using System;
using System.Threading.Tasks;

namespace Albert.BackendChallenge.Utils
{
    public class ThreadingUtils
    {

        public static async Task SleepRandomAsync(int min, int max)
        {
            await Task.Delay(new Random((int)DateTime.Now.Ticks).Next(min, max));
        }
    }
}
