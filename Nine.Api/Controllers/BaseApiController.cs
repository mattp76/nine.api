using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Nine.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Return Json response
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected HttpResponseMessage JsonResponse(JToken token, HttpStatusCode code)
        {
            var response = new HttpResponseMessage(code)
            {
                Content = new StringContent(token.ToString(Newtonsoft.Json.Formatting.Indented), Encoding.UTF8, "application/json")
            };

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            //security measure
            response.Headers.Add("X-Content-Type-Options", "nosniff");

            return response;
        }

        /// <summary>
        /// Return encapsulated JsonResult in JObject for the response.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        protected JObject JsonResult(bool success = true, HttpStatusCode code = HttpStatusCode.OK, object data = null)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["success"] = success;
            result["code"] = code;
            if (data != null && code == HttpStatusCode.OK)
            {
                result["response"] = data;
            }
            if (data != null && code != HttpStatusCode.OK)
            {
                result["error"] = data;
            }
            return JObject.FromObject(result);
        }

        /// <summary>
        /// Return Successful JsonResult.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JObject JsonResult(object data = null)
        {
            return JsonResult(true, HttpStatusCode.OK, data);
        }

        /// <summary>
        /// Return Error JsonResult
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JObject JsonErrorResult(string message = "")
        {
            return JsonResult(false, HttpStatusCode.BadRequest, message);
        }

        protected HttpResponseMessage BadResponse()
        {
            return JsonResponse(JsonErrorResult("Could not decode request: JSON parsing failed."), HttpStatusCode.BadRequest);
        }
    }
}