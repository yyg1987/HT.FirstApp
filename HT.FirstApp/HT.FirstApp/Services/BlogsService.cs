
using HT.FirstApp.Interfaces;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using System.Threading.Tasks;


namespace HT.FirstApp.Services
{
    public class BlogsService : IBlogsService
    {
        public BlogsService()
        {
        }
        public async Task<ResponseMessage> GetArticlesAsync(string blogApp, int pageIndex = 1, int pageSize = 20)
        {
            var url = string.Format(Apis.BlogPosts, blogApp, pageIndex, pageSize);
            return await UserHttpClient.Current.GetAsyn(url);
        }
    }
}
