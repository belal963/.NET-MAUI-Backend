using FindUsHere.General;
using FindUsHere.General.Interfaces;
using FindUsHere.RestApi.Controllers;
using FindUsHereTests.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace FindUsHereTests
{
    [TestFixture]
    public class UserTests
    {

        
        private Mock<IDBConnection> dbConnectionMock;

        private UserController sut;
        public int UserId;

        [SetUp]
        public void Setup()
        {
            this.dbConnectionMock = new();
            this.sut = new(dbConnectionMock.Object);
        }


        #region favorites

        [Test]
        public async Task CreateFavorites_Valid_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;

            dbConnectionMock.Setup(x => x.CreateFavorite(userId, businessId)).Returns(1);

            // Act
            var result = await this.sut.CreateFavorites(userId, businessId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            this.dbConnectionMock.Verify(x => x.CreateFavorite(userId, businessId), Times.Once);

        }

        [Test]
        public async Task CreateFavorites_NotValid_ReturnsNotFoundException()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;

            dbConnectionMock.Setup(x => x.CreateFavorite(userId, businessId)).Returns(0);

            // Act
            var result = await this.sut.CreateFavorites(userId, businessId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
            this.dbConnectionMock.Verify(x => x.CreateFavorite(userId, businessId), Times.Once);
        }



        [Test]
        public async Task DeleteFavorites_ValidItem_ReturnOkResult()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;

            dbConnectionMock.Setup(x => x.DeleteFavorites(userId, businessId)).Returns(1);

            // Act
            var result = await sut.DeleteFavorites(userId, businessId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            this.dbConnectionMock.Verify(x => x.DeleteFavorites(userId, businessId), Times.Once);
        }

        [Test]
        public async Task DeleteFavorites_NotValidItem_ReturnOException()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.DeleteFavorites(userId, businessId)).Returns(0);
            // Act
            var result = await sut.DeleteFavorites(userId, businessId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
        }


        #endregion

        #region liked 

        [Test]
        public async Task CreateLiked_Valid_Returns_OkResult()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.CreateLiked(userId, businessId)).Returns(1);

            // Act
            var result = await this.sut.CreateLiked(userId, businessId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            this.dbConnectionMock.Verify(x => x.CreateLiked(userId, businessId), Times.Once);

        }

        [Test]
        public async Task CreateLiked_NotValid_ReturnsException()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.CreateLiked(userId, businessId)).Returns(0);

            // Act
            var result = await this.sut.CreateLiked(userId, businessId);

            // Assert
            Assert.AreEqual(404, ((ObjectResult)result).StatusCode);
            this.dbConnectionMock.Verify(x => x.CreateLiked(userId, businessId), Times.Once());
        }

        [Test]
        public async Task DeleteLiked_ValidLikedItem_ReturnOkResult()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.DeleteLiked(userId, businessId)).Returns(1);

            // Act
            var result = await sut.DeleteLiked(userId, businessId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            this.dbConnectionMock.Verify(x => x.DeleteLiked(userId, businessId), Times.Once);
        }
        

        [Test]
        public async Task DeleteLiked_NotValidLikedItem_ReturnException()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.DeleteLiked(userId, businessId)).Returns(0);

            // Act
            var result = await sut.DeleteLiked(userId, businessId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        #endregion

        #region disliked

        [Test]
        public async Task CreateDisliked_Valid_Returns_OkResult()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.CreatDisliked(userId, businessId)).Returns(1);

            // Act
            var result = await this.sut.CreatDisliked(userId, businessId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            this.dbConnectionMock.Verify(x => x.CreatDisliked(userId, businessId), Times.Once);
        }

        [Test]
        public async Task Createdisliked_NotValid_ReturnsException()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.CreatDisliked(userId, businessId)).Returns(0);
            // Act
            var result = await sut.CreatDisliked(userId, businessId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        [Test]
        public async Task DeleteDisliked_ValidLikedItem_ReturnOkResult()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.DeleteDisliked(userId, businessId)).Returns(1);

            // Act
            var result = await sut.DeleteDisliked(userId, businessId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            this.dbConnectionMock.Verify(x => x.DeleteDisliked(userId, businessId), Times.Once);
        }

        [Test]
        public async Task Deletedisiked_NotValidLikedItem_ReturnException()
        {
            // Arrange
            int userId = 1;
            int businessId = 1;
            dbConnectionMock.Setup(x => x.DeleteDisliked(userId, businessId)).Returns(0);

            // Act
            var result = await sut.DeleteDisliked(userId, businessId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
        }

        #endregion

        #region User
        [Test]
        public async Task RegisterUser_ValidUser_ReturnsRegisteredUser()
        {

            // Arrange
            string username = "be";
            string email = "be@example.com";
            string password = "password";
            var userForReturn = new ConcreteUser();
            dbConnectionMock.Setup(x => x.RegisterUser(username, email, password)).Returns(userForReturn);
            
            // Act
            var result = await sut.RegisterUser(username, email, password) as OkObjectResult;

            // Assert
             Assert.IsInstanceOf<OkObjectResult>(result);
            this.dbConnectionMock.Verify(x => x.RegisterUser(username, email, password), Times.Once);

        }

        [Test]
        public async Task RegisterUser_NotValidUser_ReturnsException()
        {
            // Arrange
            string username = "be";
            string email = "be@example.com";
            string password = "password";
            dbConnectionMock.Setup(x => x.RegisterUser(username, email, password)).Throws(() => new NotFoundException(It.IsAny<string>()));

            // Act
            var result = await sut.RegisterUser(username, email, password);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode); 
            this.dbConnectionMock.Verify(x => x.RegisterUser(username, email, password), Times.Once);
        }

        [Test]
        public async Task LoginUser_ValidUser_ReturnsLoggedInUser()
        {
            // Arrange
            string username = "be";
            string email = "be@example.com";
            string password = "password";
            var userForReturn = new ConcreteUser();
            dbConnectionMock.Setup(x => x.LoginUser(username, password)).Returns(userForReturn);

            // Act
            var result = await sut.LoginUser(username, password) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            this.dbConnectionMock.Verify(x => x.LoginUser(username, password), Times.Once);
           
        }

        [Test]
        public async Task LoginUser_NotValidUser_ReturnsException()
        {
            // Arrange
            string username = "be";
            string email = "be@example.com";
            string password = "password";
            dbConnectionMock.Setup(x => x.LoginUser(username, password)).Throws(() => new NotFoundException(It.IsAny<string>()));

            // Act
            var result = await sut.LoginUser(username, password) ;

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
            this.dbConnectionMock.Verify(x => x.LoginUser(username, password), Times.Once);
        }
        [Test]
        public async Task UpdateUser_ValidUser_ReturnsUpdatedUser()
        {
            // Arrange
            string username = "be";
            string email = "be@example.com";
            string password = "password";
          

            // Act
            var userForReturn = new ConcreteUser();
            dbConnectionMock.Setup(x => x.UpdateUser(username, email, password)).Returns(userForReturn);

            // Assert
            var result = await sut.UpdateUser(username,email, password) as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            this.dbConnectionMock.Verify(x => x.UpdateUser(username, email, password), Times.Once);
        }

        [Test]
        public async Task UpdateUser_NotValidUser_ReturnsException()
        {
            // Arrange
            string username = "be";
            string email = "be@example.com";
            string password = "password";
            dbConnectionMock.Setup(x => x.UpdateUser(username, email, password)).Throws(() => new NotFoundException(It.IsAny<string>()));

            // Act
            var result = await sut.UpdateUser(username, email, password);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(404, objectResult.StatusCode);
             this.dbConnectionMock.Verify(x => x.UpdateUser(username, email, password), Times.Once);
        }

        [Test]
        public async Task DeleteUser_WhenValidUserId_ReturnsOK()
        {

            // Arrange
            this.dbConnectionMock.Setup(x => x.DeleteUser(1)).Returns(1);

            // Act
            var result = await this.sut.DeleteUser(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            this.dbConnectionMock.Verify(x => x.DeleteUser(1), Times.Once);

        }

        [Test]
        public async Task DeleteUser_NotValidUserId_ReturnsException()
        {

            // Arrange
            this.dbConnectionMock.Setup(x => x.DeleteUser(42)).Returns(0);

            // Act
            var result = await this.sut.DeleteUser(42);

            // Assert
            Assert.AreEqual(404, ((ObjectResult)result).StatusCode);
            this.dbConnectionMock.Verify(x => x.DeleteUser(42), Times.Once);

        }

        #endregion
    }
}