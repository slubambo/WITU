using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WITU.Controllers;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;
using WITU.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WITU.Tests.Controllers
{
    [TestClass]
    public class InformationDeskTests
    {
        private InformationDeskController informationDeskController;

        private Mock<ICoreRepository> _coreRepo;
        private Mock<IGeneralHelper> _generalHelper;

        [TestInitialize]
        public void SetUp()
        {
            _coreRepo = new Mock<ICoreRepository>();
            _generalHelper = new Mock<IGeneralHelper>();

            informationDeskController = new InformationDeskController(_coreRepo.Object, _generalHelper.Object);
        }

        [TestMethod]
        public void AllAttachedFiles_Tests()
        {
            //arrange 
            var id = 1;
            var type = InformationDeskType.GeneralInformation;
            var path = "";

            var generalInformation = new GeneralInformation()
            {
                Id = 1
            };
            
            var generalInformationAttachements = new List<GeneralInformationAttachment>()
            {
                new GeneralInformationAttachment()
                {
                    FileName = "fileName",
                    UserFriendlyName = "Friendly Name",
                    Id = 15,
                    FileType = "pdf",
                    GeneralInformation = generalInformation,

                }
            };
            generalInformation.GeneralInformationAttachments.AddAll(generalInformationAttachements);
            
            _coreRepo.Setup(x => x.Get<GeneralInformation>(id)).Returns(generalInformation);
            _generalHelper.Setup(x => x.GetFolderAndUrlPath(AttachmentType.GeneralInformation, out path, out path));

            //act
            var result = informationDeskController.AllAttachedFiles(id, type) as ViewResult;

            //assert
            Assert.IsNotNull(result, "A View is Important");
            Assert.IsNotNull(result.Model, "We need to have a Model Present");
        }
    }
}
