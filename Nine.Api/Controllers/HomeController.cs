using Nine.Core.Interfaces;
using Nine.Core.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nine.Api.Controllers
{
    public class HomeController : BaseApiController
    {

        protected readonly IJsonHelper _jsonHelper;
        protected readonly IShowService _showService;

        public HomeController(IJsonHelper jsonHelper, IShowService showService)
        {
            this._showService = showService;
            this._jsonHelper = jsonHelper;
        }

        /// <summary>
        /// The default action for the site
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage Index([FromBody] ShowsRootModel model)
        {
            if (model == null)
            {
                return BadResponse();
            }

            //do some error handling first.
            if (model.payload == null)
            {
                return BadResponse();
            }
            //run a further check in case we got past the payload check
            if (!_jsonHelper.CheckForValidJson(model))
            {
                return BadResponse();
            }

            //passed the above error handling. Now apply the logic and process the response
            var resp = _showService.ProcessShowResponse(model);
            var response = JsonResponse(JsonResult(resp), HttpStatusCode.OK);

            return response;
        }
    }

}
