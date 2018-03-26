using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Nine.Api.Controllers;
using Nine.Core.Interfaces;
using Nine.Core.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Nine.Test
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IShowService> _mockShowService;
        private Mock<IJsonHelper> _mockJsonHelper;

        private HomeController _controller;

        private HttpResponseMessage _response;
        private JToken _json;
        private string _expectedResponseResult;
        private JToken _expectedJson;

        private ShowsRootModel model;

        [TestInitialize]
        public void Setup()
        {

            //-----------------------------------------------------------------------------------------------------------
            // Setup test params
            //-----------------------------------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------------------------------
            // Arrange Mocks
            //-----------------------------------------------------------------------------------------------------------
            _mockJsonHelper = new Mock<IJsonHelper>();
            _mockShowService = new Mock<IShowService>();

            //-----------------------------------------------------------------------------------------------------------
            //  Inject
            //-----------------------------------------------------------------------------------------------------------
            _controller = new HomeController(_mockJsonHelper.Object, _mockShowService.Object);
        }


        [TestMethod]
        public void Index_ModelIsNull()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------
            _expectedResponseResult = (string)JObject.FromObject(new { error = "Could not decode request: JSON parsing failed." })["error"];
            model = null;
            _response = _controller.Index(model);

            ////-----------------------------------------------------------------------------------------------------------
            //// Act
            ////-----------------------------------------------------------------------------------------------------------
            var actualResponseMessage = (string)JObject.Parse(_response.Content.ReadAsStringAsync().Result)["error"];

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            Assert.IsInstanceOfType(_response, typeof(HttpResponseMessage));
            Assert.IsNotNull(_response); // Check we have a response
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            Assert.AreEqual(actualResponseMessage, _expectedResponseResult);
        }


        [TestMethod]
        public void Index_ModelIsNotNullButPayLoadIsNull()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------
            _expectedResponseResult = (string)JObject.FromObject(new { error = "Could not decode request: JSON parsing failed." })["error"];

            model = new ShowsRootModel()
            {
                payload = null,
                skip = 0,
                take = 1,
                totalRecords = 1
            };
            _response = _controller.Index(model);

            ////-----------------------------------------------------------------------------------------------------------
            //// Act
            ////-----------------------------------------------------------------------------------------------------------
            var actualResponseMessage = (string)JObject.Parse(_response.Content.ReadAsStringAsync().Result)["error"];

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            Assert.IsInstanceOfType(_response, typeof(HttpResponseMessage));
            Assert.IsNotNull(_response); // Check we have a response
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            Assert.AreEqual(actualResponseMessage, _expectedResponseResult);
        }

        [TestMethod]
        public void Index_ModelIsValid()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------
            _expectedResponseResult = (string)JObject.FromObject(new { error = "Could not decode request: JSON parsing failed." })["error"];

            //load in our valid request from json


            //check against valid response


            //create a fake stream of data to represent request body
            var json = "{ \"Key\": \"Value\"}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());
            var stream = new MemoryStream(bytes);

            model = new ShowsRootModel()
            {
                payload = null,
                skip = 0,
                take = 1,
                totalRecords = 1
            };
            _response = _controller.Index(model);

            ////-----------------------------------------------------------------------------------------------------------
            //// Act
            ////-----------------------------------------------------------------------------------------------------------
            //_response = _controller.Index(model);
            var actualResponseMessage = (string)JObject.Parse(_response.Content.ReadAsStringAsync().Result)["error"];

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            Assert.IsInstanceOfType(_response, typeof(HttpResponseMessage));
            Assert.IsNotNull(_response); // Check we have a response
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            Assert.AreEqual(actualResponseMessage, _expectedResponseResult);
        }

    }
}
