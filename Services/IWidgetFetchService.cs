using System.Threading.Tasks;
using Challenge.Domain;

namespace Challenge.Services
{
    public interface IWidgetFetchService
    {
        Task<Widget> DownloadWidget(int id);
    }
}