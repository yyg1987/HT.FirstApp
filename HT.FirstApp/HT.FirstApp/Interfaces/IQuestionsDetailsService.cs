using HT.FirstApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HT.FirstApp.Interfaces
{
    public interface IQuestionsDetailsService
    {
        Task<ResponseMessage> GetQuestionsAsync(int id);
        Task<ResponseMessage> GetAnswersAsync(int id, int pageIndex, int pageSize);
        Task<ResponseMessage> PostAnswerAsync(int id, string content);
        Task<ResponseMessage> EditAnswerAsync(int questionId, int answerId, int userId, string content);
        Task<ResponseMessage> DeleteAnswerAsync(int questionId, int answerId);
    }
}
