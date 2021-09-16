using System;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Domain;

namespace Challenge.Services
{
    //for the sake of this excercise, consider this class a total blackbox
    public class WidgetFetchService : IWidgetFetchService
    {
        private static Random _random = new Random();
        public async Task<Widget> DownloadWidget(int id)
        {
            var widget = new Widget {Id = id, Data = FakeData(25)};
            await Task.Delay(_random.Next(20,50));
            return  widget;
        }

        private string FakeData(int length)
        {
            return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}