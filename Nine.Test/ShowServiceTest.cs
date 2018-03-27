using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nine.Api.Controllers;
using Nine.Core.Interfaces;
using Nine.Core.Models;
using Nine.Core.Services;
using Nine.Test.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Nine.Test
{
    [TestClass]
    public class ShowServiceTest
    {

        private IShowService _showService;

        //test data
        private string TEST_REQUEST = ConfigurationManager.AppSettings["test.request"];

        string _testRequestDataPath;
        string _testRequestData;


        private ShowsRootModel _model;
        private List<ShowResponseModel> _response;

        [TestInitialize]
        public void Setup()
        {

            //-----------------------------------------------------------------------------------------------------------
            // Setup some test params
            //-----------------------------------------------------------------------------------------------------------

            _testRequestDataPath = string.Format("{0}{1}", PathHelper.GetTestProjectPath(), TEST_REQUEST);
            _testRequestData = File.ReadAllText(_testRequestDataPath);

            _model = JsonConvert.DeserializeObject<ShowsRootModel>(_testRequestData);

            //-----------------------------------------------------------------------------------------------------------
            // Arrange Mocks
            //-----------------------------------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------------------------------
            //  Inject
            //-----------------------------------------------------------------------------------------------------------
            _showService = new ShowService();
        }

        [TestMethod]
        public void ProcessShowResponse_Result_Is_Correct()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Setup
            //-----------------------------------------------------------------------------------------------------------


            ////-----------------------------------------------------------------------------------------------------------
            //// Act
            ////-----------------------------------------------------------------------------------------------------------
            _response = _showService.ProcessShowResponse(_model);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            Assert.IsNotNull(_response);
            Assert.AreEqual(7, _response.Count);
            Assert.AreEqual("http://mybeautifulcatchupservice.com/img/shows/Thunderbirds_1280.jpg", _response[2].image);
            Assert.AreEqual("show/toyhunter", _response[4].slug);
            Assert.AreEqual("The Originals", _response[6].title);

        }
    }
}
