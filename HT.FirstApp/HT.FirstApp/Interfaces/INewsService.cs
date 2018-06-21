using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface INewsService
    {
        Task<ResponseMessage> GetNewsAsync(int position, int pageIndex = 1, int pageSize = 20);
    }
}
