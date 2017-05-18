using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;
using WITU.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WITU;
using WITU.Controllers;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace WITU.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private IGeneralHelper _generalHelper;
        private ICoreRepository _coreRepository;

        private HomeController controller;
        
        [TestInitialize]
        public void SetUp()
        {
            _generalHelper = new Mock<IGeneralHelper>().Object;
            _coreRepository = new Mock<ICoreRepository>().Object;
            controller = new HomeController(_coreRepository, _generalHelper);
        }

        [TestCategory("Home Controller Tests")]
        [TestMethod]
        public void Index()
        {
            //Arrange
            // Mock the Products Repository using Moq
            

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(HomeView), result.Model.GetType());
        }

        [TestCategory("Home Controller Tests")]
        [TestMethod]
        public void About()
        {

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestCategory("Home Controller Tests")]
        [TestMethod]
        public void Contact()
        {
            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
