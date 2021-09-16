using System.Threading.Tasks;
using Challenge.Domain;

namespace Challenge.Repositories
{
    public interface IWidgetRepo
    {
        Task CommitWidget(Widget widget);
    }
}