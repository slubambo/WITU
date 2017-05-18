using System;
using WITU.Core.Model;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WITU.Tests.Helpers
{
    [TestClass]
    public class GeneralHelperTests
    {

        private GeneralHelper _generalHelper;

        private Mock<IGeneralHelper> _generalHelperMock;

        [TestInitialize]
        public void SetUp()
        {
            _generalHelper = new GeneralHelper();
        }

        [TestMethod]
        public void ConvertToPlainText_Test()
        {
            //arrange
            var htmlString = "<p>Hello <b>World</b></p>";

            //act
            var result = _generalHelper.ConvertHtmlToPlainText(htmlString);

            //assert
            Assert.AreEqual(result.Trim(), "Hello World", "The Html Conversion was not successful.");
            Assert.AreNotEqual(result, htmlString, "The exact html string was returned!");
        }

        [TestMethod]
        public void ConvertToPlainText_SubString_Test()
        {
            //arrange
            var htmlString = "<p>Hello <b>World</b></p>";

            //act
            var resultShort = _generalHelper.ConvertHtmlToPlainText(htmlString, 5);
            var resultLong = _generalHelper.ConvertHtmlToPlainText(htmlString, 20);

            //assert
            Assert.AreEqual(resultShort, "Hello", "The Long String was not correctly truncated.");
            Assert.AreNotEqual(resultShort, "Hello World", "The number of characters exceeded after trancation");

            Assert.AreEqual(resultLong, "Hello World", "The Short string to be converted should have returned the whole string");

        }

        [TestMethod]
        public void GetEnumDescriptionName_Test()
        {
            //arrange


            //act
            var result1 = _generalHelper.GetEnumDescriptionIrName(ResourceFolders.Thumbnail);
            var result2 = _generalHelper.GetEnumDescriptionIrName(UserTypes.ProspectiveStudent);

            //assert
            Assert.AreEqual(result1, ResourceFolders.Thumbnail.ToString(), 
                "The actual return for Enum type without a desrciption should be the actual string");
            Assert.AreEqual(result2, "Prospective Student", "The result should be the description of the enum.");
            Assert.AreNotEqual(result2, UserTypes.ProspectiveStudent.ToString(), 
                "Result should be the description and not the string of the enum itself.");

        }
    }
}
