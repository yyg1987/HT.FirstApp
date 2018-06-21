
using HT.FirstApp.Interfaces;
using HT.FirstApp.Helpers;
using HT.FirstApp.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace HT.FirstApp.Services
{
    public class NewsDetailsService : INewsDetailsService
    {
        public NewsDetailsService()
        {
        }
        public async Task<ResponseMessage> GetNewsAsync(int id)
        {
            var url = string.Format(Apis.NewsBody, id);
            return await TokenHttpClient.Current.GetAsyn(url);
        }
        public async Task<ResponseMessage> GetCommentAsync(int id, int pageIndex, int pageSize)
        {
            var url = string.Format(Apis.NewsComment, id, pageIndex, pageSize);
            return await TokenHttpClient.Current.GetAsyn(url);
        }
        public async Task<ResponseMessage> PostCommentAsync(int id, string content, bool hasEdit = false)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("Content", content);
            if (hasEdit)
            {
                var url = string.Format(Apis.NewsCommentEdit, id);
                return await UserHttpClient.Current.PatchAsync(url, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json"));
            }
            else
            {
                var url = string.Format(Apis.NewsCommentAdd, id);
                return await UserHttpClient.Current.PostAsync(url, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json"));
            }
        }
        public async Task<ResponseMessage> DeleteCommentAsync(int id)
        {
            var url = string.Format(Apis.NewsCommentEdit, id);
            return await UserHttpClient.Current.DeleteAsync(url);
        }
    }
}
