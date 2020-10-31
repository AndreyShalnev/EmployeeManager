using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManager.Data.Common;
using GorestClient.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GorestClient.Extensions
{
    internal static class HttpResponseMessageExtensions
    {

        public static TResult DeserializeTo<TResult>(this Task<HttpResponseMessage> response)
            where TResult : class, new()
        {
            var json = response.Result.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<TResult>(json);
            return result;
        }

        public static ActionResult<TData> GetActionResult<TData>(this Task<HttpResponseMessage> response)
            where TData : class, new()
        {
            var json = response.Result.Content.ReadAsStringAsync().Result;
            var jsonObject = JObject.Parse(json);

            var data = jsonObject["data"].ToString();
            var statusCode = int.Parse(jsonObject["code"].ToString());

            if (statusCode == 200 || statusCode == 201)
            {
                var dataObj = JsonConvert.DeserializeObject<TData>(data);
                return new ActionResult<TData>(dataObj);
            }

            if (statusCode == 204)
            {
                return new ActionResult<TData>(true);
            }

            return new ActionResult<TData>(GetErrorText(data));
        }

        public static string GetErrorText(string json)
        {
            if (json.StartsWith("["))
            {
                var errors = JsonConvert.DeserializeObject<ErrorData[]>(json);
                return string.Join("\n", errors.Select(x => x.ToString()));
            }

            var error = JsonConvert.DeserializeObject<ErrorData>(json);
            return error.ToString();
        }

    }
}
