
using HT.FirstApp.Interfaces;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using System.Threading.Tasks;


namespace HT.FirstApp.Services
{
    public class KbArticlesService : IKbArticlesService
    {
        public KbArticlesService()
        {
        }
        public async Task<ResponseMessage> GetKbArticlesAsync(int pageIndex = 1, int pageSize = 20)
        {
            var url = string.Format(Apis.KbArticles, pageIndex, pageSize);
            return await TokenHttpClient.Current.GetAsyn(url);
        }
    }
}
