using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IArticlesDetailsService
    {
        Task<ResponseMessage> GetArticlesAsync(int id);
        Task<ResponseMessage> GetCommentAsync(string blogApp, int id, int pageIndex = 1, int pageSize = 20);
        Task<ResponseMessage> PostCommentAsync(string blogApp, int id, string content);
    }
}
