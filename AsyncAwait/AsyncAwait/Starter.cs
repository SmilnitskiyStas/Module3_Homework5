using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    internal class Starter
    {
        public void Run()
        {
            Task<string> concatText = GetTextAsync();

            Console.WriteLine(concatText.Result);
        }

        private async Task<string> GetTextAsync()
        {
            List<Task> tasks = new List<Task>();

            Task<string> firstMethod = ReadFirstFileAsync();

            Task<string> secondMethod = ReadSecondFileAsync();

            tasks.Add(Task.Run(() => firstMethod));

            tasks.Add(Task.Run(() => secondMethod));

            Task.WaitAll(tasks.ToArray());

            return firstMethod.Result + " " + secondMethod.Result;
        }

        private async Task<string> ReadFirstFileAsync()
        {
            string text = null;
            await using (FileStream fileStream = File.OpenRead("C:\\Users\\Stas2\\source\\repos\\GitProject\\Module3_Homework5\\AsyncAwait\\AsyncAwait\\Hello.txt"))
            {
                byte[] buffer = new byte[fileStream.Length];

                await fileStream.ReadAsync(buffer, 0, buffer.Length);

                text = Encoding.UTF8.GetString(buffer);
            }

            return text;
        }

        private async Task<string> ReadSecondFileAsync()
        {
            string text = null;

            await using (FileStream fileStream = File.OpenRead("C:\\Users\\Stas2\\source\\repos\\GitProject\\Module3_Homework5\\AsyncAwait\\AsyncAwait\\World.txt"))
            {
                byte[] buffer = new byte[fileStream.Length];

                fileStream.Read(buffer, 0, buffer.Length);

                text = Encoding.UTF8.GetString(buffer);
            }

            return text;
        }
    }
}
