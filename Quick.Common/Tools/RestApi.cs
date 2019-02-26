using RestSharp;
using SyntacticSugar;

namespace Quick.Common.Tools
{
    public class RestApi<T> where T : class, new()
    {
        public T Get(string url, object pars)
        {
            const Method type = Method.GET;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }
        public T Post(string url, object pars)
        {
            const Method type = Method.POST;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }
        public T Delete(string url, object pars)
        {
            const Method type = Method.DELETE;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }
        public T Put(string url, object pars)
        {
            const Method type = Method.PUT;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }

        private static IRestResponse<T> GetApiInfo(string url, object pars, Method type)
        {
            var request = new RestRequest(type);
            if (pars != null)
                request.AddObject(pars);
            var client = new RestClient(RequestInfo.HttpDomain + url)
            {
                CookieContainer = new System.Net.CookieContainer()
            };
            IRestResponse<T> res = client.Execute<T>(request);
            return res;
        }
    }
}
