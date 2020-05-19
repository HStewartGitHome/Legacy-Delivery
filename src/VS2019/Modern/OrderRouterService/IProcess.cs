using System.Threading.Tasks;

namespace OrderRouterService
{
    public interface IProcess
    {
        Task<bool> ProcessNextFile();
    }
}