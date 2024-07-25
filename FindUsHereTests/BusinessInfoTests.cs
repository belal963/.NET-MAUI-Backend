using FindUsHere.DbConnector;
using FindUsHere.General;
using FindUsHere.General.Interfaces;
using FindUsHere.RestApi;
using FindUsHere.RestApi.Controllers;
using FindUsHere.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text.Json;

namespace FindUsHereTests
{
    [TestFixture]
    public class BusinessInfoTests
    {


        private Mock<IDBConnection> dbConnectionMock;
        public BusinessInfoController sut;
        

        [SetUp]
        public void Setup()
        {
            this.dbConnectionMock = new();
            this.sut = new(dbConnectionMock.Object);
        }


        #region Business
        [Test]
        public async Task GetAllBusinessInfosByRadius_Valid_ReturnListOfBusiness()
        {
            // Arrange
            double lat = 48.803247;
            double lon = 9.222083;
            int radius = 25;
            IList<RestApiBusinessInfo> restApiBusinessInfos = new List<RestApiBusinessInfo>();
            dbConnectionMock.Setup(x => x.GetAllBusinessInfosByRadius(lat, lon, radius)).Returns(restApiBusinessInfos.Cast<IBusinessInfo>().ToList());

            // Act
            var result = await sut.GetAllBusinessInfosByRadius(lat, lon, radius) as ObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            this.dbConnectionMock.Verify(x => x.GetAllBusinessInfosByRadius(lat, lon, radius), Times.Once);
        }

        [Test]
        public async Task GetAllBusinessInfosByRadius_NotValid_ReturnException()
        {
            // Arrange
            double lat = 60.803247;
            double lon = 20.222083;
            int radius = 25;
            dbConnectionMock.Setup(x => x.GetAllBusinessInfosByRadius(lat,lon,radius)).Throws(() => new NotFoundException(It.IsAny<string>()));

            // Act
            var result = await sut.GetAllBusinessInfosByRadius(lat, lon, radius) ;

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
        }
        #endregion

        #region Category
        [Test]
        public async Task GetAllBusinessCategories_Valid_ReturnListCategories()
        {
            // Arrange
            IList<RestApiCategory> restApiCategories = new List<RestApiCategory>();
            dbConnectionMock.Setup(x => x.SelectAllCategories()).Returns(restApiCategories.Cast<ICategory>().ToList());

            // Act
            var result = await sut.SelectCategory();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            this.dbConnectionMock.Verify(x => x.SelectAllCategories(), Times.Once);
            
        }

        [Test]
        public async Task GetAllBusinessCategories_NotValid_ReturnException()
        {
            // Arrange
            dbConnectionMock.Setup(x => x.SelectAllCategories()).Throws(() => new NotFoundException(It.IsAny<string>()));

            // Act
            var result = await sut.SelectCategory();

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        #endregion


    }
}