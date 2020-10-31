using System;
using System.Linq;
using EmployeeManager.Data;
using EmployeeManager.Data.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GorestClient.Tests
{
    [TestClass]
    public class UserClientTests
    {
        private const string userClientUrl = "https://gorest.co.in/public-api";

        [TestMethod]
        [DataRow(2)]
        [DataRow(5)]
        public void GetAll_ShouldReturnSpecificPage(int pageNum)
        {
            var response = Client().GetAll(pageNum);

            Assert.AreEqual(pageNum, response.Meta.Pagination.Page);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void GetByName_ShouldFilterByName()
        {
            var name = "test";

            var response = Client().GetByName(name);

            Assert.IsTrue(response.Meta.Pagination.Total > 0);
            Assert.IsTrue(response.Data.All(x => x.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        [TestMethod]
        public void GetById_ShouldFilterById()
        {
            var id = 8;

            var user = Client().GetById(id);

            Assert.AreEqual(id, user.Id);
        }

        [TestMethod]
        public void GetById_ShouldContainAllPropertiesWithNotDefaultValues_WhenJsonConvertDeserializeObjectWorksFine()
        {
            var defaultObject = new User();

            var user = Client().GetById(1123);

            Assert.AreNotEqual(defaultObject.Id, user.Id);
            Assert.AreNotEqual(defaultObject.Name, user.Name);
            Assert.AreNotEqual(defaultObject.Created, user.Created);
            Assert.AreNotEqual(defaultObject.Updated, user.Updated);
            Assert.AreNotEqual(defaultObject.Email, user.Email);
            Assert.AreNotEqual(defaultObject.Gender, user.Gender);
            Assert.AreNotEqual(defaultObject.Status, user.Status);
        }

        [TestMethod]
        public void Create_ShouldCreateUser()
        {
            var random = new Random().Next(10000, 20000);
            var email = $"test{random}@ua.ua";
            var user = GetUser(email);

            var result = Client().Create(user);

            Assert.IsTrue(result.IsSuccess, $"Failed to create user with email={email}. {result.Message}");
        }

        [TestMethod]
        public void Create_ShouldReturnError_WhenUserWithEmailAlreadyExist()
        {
            var email = "test1@ua.ua";
            var user = GetUser(email);

            var result = Client().Create(user);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("email-has already been taken", result.Message);
        }

        [TestMethod]
        public void Update_ShouldReturnNoError()
        {
            var user = Client().GetById(1123);
            user.Name += "1";

            var result = Client().Update(user);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(user.Name, result.Result.Name);
        }


        [TestMethod]
        public void CreateAndDelete_ShouldReturnError_WhenUserNotFound()
        {
            var email = "test123@ua.ua";
            var user = GetUser(email);

            var createResult = Client().Create(user);
            var deleteResult = Client().Delete(createResult.Result.Id);
            var deleteResult2 = Client().Delete(createResult.Result.Id);

            Assert.IsTrue(deleteResult.IsSuccess);
            Assert.IsFalse(deleteResult2.IsSuccess);
            Assert.AreEqual("Resource not found", deleteResult2.Message);
        }

        private static User GetUser(string email)
        {
            return new User
            {
                Name = "test",
                Email = email,
                Status = UserActivityStatus.Active,
                Gender = Gender.Female
            };
        }

        private UserClient Client()
        {
            return new UserClient(new ClientConfig
            {
                BaseUrl = userClientUrl,
                APIToken = "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56"
            });
        }
    }
}
