using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nine.Api.Controllers;
using Nine.Core.Interfaces;
using Nine.Core.Models;
using Nine.Test.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;

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

        //test data
        private string TEST_REQUEST = ConfigurationManager.AppSettings["test.request"];
        private string TEST_RESPONSE = ConfigurationManager.AppSettings["test.response"];
        private string TEST_SERVICE_RESPONSE = ConfigurationManager.AppSettings["test.service.response"];

        string _testResponseDataPath;
        string _testServiceResponseDataPath;
        string _testRequestDataPath;
        string _testResponseData;
        string _testRequestData;
        string _testServiceResponseData;


        private ShowsRootModel _model;

        [TestInitialize]
        public void Setup()
        {

            //-----------------------------------------------------------------------------------------------------------
            // Setup some test params
            //-----------------------------------------------------------------------------------------------------------
            _model = new ShowsRootModel()
            {
                payload = null,
                skip = 0,
                take = 1,
                totalRecords = 1
            };

            _testRequestDataPath = string.Format("{0}{1}", PathHelper.GetTestProjectPath(), TEST_REQUEST);
            _testResponseDataPath = string.Format("{0}{1}", PathHelper.GetTestProjectPath(), TEST_RESPONSE);
            _testServiceResponseDataPath = string.Format("{0}{1}", PathHelper.GetTestProjectPath(), TEST_SERVICE_RESPONSE);
            _testRequestData = File.ReadAllText(_testRequestDataPath);
            _testResponseData = File.ReadAllText(_testResponseDataPath);
            _testServiceResponseData = File.ReadAllText(_testServiceResponseDataPath);

            List<ShowResponseModel> ShowResponseModel = JsonConvert.DeserializeObject<List<ShowResponseModel>>(_testServiceResponseData);

            //-----------------------------------------------------------------------------------------------------------
            // Arrange Mocks
            //-----------------------------------------------------------------------------------------------------------
            _mockJsonHelper = new Mock<IJsonHelper>();
            _mockShowService = new Mock<IShowService>();
            _mockJsonHelper.Setup(x => x.CheckForValidJson(It.IsAny<ShowsRootModel>())).Returns(true);
            _mockShowService.Setup(x => x.ProcessShowResponse(It.IsAny<ShowsRootModel>())).Returns(ShowResponseModel);

            //-----------------------------------------------------------------------------------------------------------
            //  Inject
            //-----------------------------------------------------------------------------------------------------------
            _controller = new HomeController(_mockJsonHelper.Object, _mockShowService.Object);
        }


        [TestMethod]
        public void Index_Model_Is_Null()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------
            _expectedResponseResult = (string)JObject.FromObject(new { error = "Could not decode request: JSON parsing failed." })["error"];
            _model = null;
            _response = _controller.Index(_model);

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
        public void Index_Model_Is_Not_Null_But_PayLoad_Is_Null()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------
            _expectedResponseResult = (string)JObject.FromObject(new { error = "Could not decode request: JSON parsing failed." })["error"];

            ////-----------------------------------------------------------------------------------------------------------
            //// Act
            ////-----------------------------------------------------------------------------------------------------------
            _response = _controller.Index(_model);
            var actualResponseMessage = (string)JObject.Parse(_response.Content.ReadAsStringAsync().Result)["error"];

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            Assert.IsInstanceOfType(_response, typeof(HttpResponseMessage));
            Assert.IsNotNull(_response);
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
            Assert.AreEqual(actualResponseMessage, _expectedResponseResult);
        }

        [TestMethod]
        public void Index_Model_Is_Valid()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------
            var testRequestDataPath = string.Format("{0}{1}", PathHelper.GetTestProjectPath(), TEST_REQUEST);
            var testRequestData = File.ReadAllText(testRequestDataPath);
            _model = JsonConvert.DeserializeObject<ShowsRootModel>(testRequestData);

            ////-----------------------------------------------------------------------------------------------------------
            //// Act
            ////-----------------------------------------------------------------------------------------------------------
            _response = _controller.Index(_model);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            Assert.AreEqual(10, _model.take);
            Assert.AreEqual(75, _model.totalRecords);
        }


        [TestMethod]
        public void Index_Response_Is_Valid()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------
            _expectedJson = JObject.Parse(_testResponseData)["response"];
            _model = JsonConvert.DeserializeObject<ShowsRootModel>(_testRequestData);

            ////-----------------------------------------------------------------------------------------------------------
            //// Act
            ////-----------------------------------------------------------------------------------------------------------
            _response = _controller.Index(_model);
            _json = JObject.Parse(_response.Content.ReadAsStringAsync().Result)["response"];


            //-----------------------------------------------------------------------------------------------------------
            // Json Tests
            //-----------------------------------------------------------------------------------------------------------
            JToken.DeepEquals(_expectedJson, _json);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            Assert.IsInstanceOfType(_response, typeof(HttpResponseMessage));
            Assert.IsNotNull(_response);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
    }
}
