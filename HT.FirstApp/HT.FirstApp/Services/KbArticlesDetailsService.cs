
using HT.FirstApp.Interfaces;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using System.Threading.Tasks;


namespace HT.FirstApp.Services
{
    public class KbArticlesDetailsService : IKbArticlesDetailsService
    {
        public KbArticlesDetailsService()
        {
        }
        public async Task<ResponseMessage> GetKbArticlesAsync(int id)
        {
            var url = string.Format(Apis.KbArticlesBody, id);
            return await TokenHttpClient.Current.GetAsyn(url);
        }
    }
}
