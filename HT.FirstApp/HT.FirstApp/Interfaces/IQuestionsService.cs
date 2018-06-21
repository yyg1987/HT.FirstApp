using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IQuestionsService
    {
        Task<ResponseMessage> GetQuestionsAsync(int position, int pageIndex = 1, int pageSize = 20);
        Task<ResponseMessage> EditQuestionsAsync(Questions questions);
    }
}
