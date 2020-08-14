using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using milstone3;

namespace Kanban_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void setUp()
        {
            UserController.initiateProg();
        }

        [TestMethod]
        public void register_password_Test1()
        {
            
            bool ans= UserController.register("testUser1@gmail.com", "12345or");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void register_password_Test2()
        {
            bool ans = UserController.register("testUser2@gmail.com", "orNoNum");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void register_username_Test3()
        {
            bool ans = UserController.register("testUser3gmail.com", "123456Or");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void register_password_Test4()
        {
            bool ans = UserController.register("testUser4@gmail.com", "");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void register_password_Test5()
        {
            bool ans = UserController.register("testUser5@gmail.com", null);
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void register_password_Test6()
        {
            bool ans = UserController.register("testUser6@gmail.com", "OR12345");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void register_Test1()
        {
            bool ans = UserController.register("testUser7@gmail.com", "123456Es");
            Assert.AreEqual(true, ans);
        }

        [TestMethod]
        public void register_Test2()
        {
            UserController.register("testUser8@gmail.com", "123456Or");
            bool ans = UserController.register("testUser8@gmail.com", "123456Or");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void login_Test1()
        {
            UserController.register("testUser9@gmail.com", "123456Es");
            bool ans = UserController.login("testUser9@gmail.com", "123456Es");
            Assert.AreEqual(true, ans);
        }

        [TestMethod]
        public void login_Test2()
        {
            UserController.register("testUser10@gmail.com", "123456Es");
            bool ans = UserController.login("testUser10@gmail.com", "hellO123");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void login_Test3()
        {
            bool ans = UserController.login("testUser11@gmail.com", "123456Test");
            Assert.AreEqual(false, ans);
        }

        [TestMethod]
        public void login_Test4()
        {
            UserController.register("testUser12@gmail.com", "123456Test");
            UserController.login("testUser12@gmail.com", "123456Test");
            bool ans = UserController.login("testUser12@gmail.com", "123456Test");
            Assert.AreEqual(false, ans);
        }



    }
}
