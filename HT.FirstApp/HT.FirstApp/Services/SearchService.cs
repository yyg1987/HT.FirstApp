
using System.Threading.Tasks;
using HT.FirstApp.Helpers;
using HT.FirstApp.Interfaces;
using HT.FirstApp.Model;


namespace HT.FirstApp.Services
{
    public class SearchService : ISearchService
    {
        public SearchService()
        {
        }
        public async Task<ResponseMessage> GetSearchAsync(int position, string keyWords, int pageIndex = 1, int pageSize = 20)
        {
            var url = "";
            switch (position)
            {
                case 0:
                    url = string.Format(Apis.Search, "bolgs", keyWords, pageIndex, pageSize);
                    break;
                case 1:
                    url = string.Format(Apis.Search, "news", keyWords, pageIndex, pageSize);
                    break;
                case 2:
                    url = string.Format(Apis.Search, "kb", keyWords, pageIndex, pageSize);
                    break;
                case 3:
                    url = string.Format(Apis.Search, "question", keyWords, pageIndex, pageSize);
                    break;
            }
            return await TokenHttpClient.Current.GetAsyn(url);
        }
    }
}
