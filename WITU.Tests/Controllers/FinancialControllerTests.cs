//using System;
//using System.Collections.Generic;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using WITU.Controllers;
//using WITU.Core.Model;
//using WITU.Core.Repositories.Interfaces;
//using WITU.Helper.Impls;
//using WITU.Helper.Interfaces;
//using WITU.Models;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace WITU.Tests.Controllers
//{
//    [TestClass]
//    public class FinancialControllerTests
//    {
//        //private FinancialController financialController;

//        private Mock<ICoreRepository> _coreRepo;
//        private Mock<IGeneralHelper> _generalHelper;
//       // private Mock<IFinancialRepository> _financialRepo;
//        private Mock<IStudentRepository> _studentRepository;
//        private Mock<ISemesterRepository> _semesterRepository;
//        //private Mock<IModelTransformHelper> _modelTransformHelper;
//        private Mock<IStaffRepository> _staffRepository;
//        //private Mock<IFinancialHelper> _financialHelper;
//        //private Mock<IRegistrationHelper> _registrationHelper;
//        private Mock<ISemesterRegistrationRepository> _semesterRegistrationRepository;

//        [TestInitialize]
//        public void SetUp()
//        {
//            _coreRepo = new Mock<ICoreRepository>();
//            _generalHelper = new Mock<IGeneralHelper>();
//            _studentRepository = new Mock<IStudentRepository>();
//            //_financialRepo = new Mock<IFinancialRepository>();
//            //_modelTransformHelper = new Mock<IModelTransformHelper>();
//            _semesterRepository = new Mock<ISemesterRepository>();
//            _staffRepository = new Mock<IStaffRepository>();
//            //_financialHelper = new Mock<IFinancialHelper>();
//            //_registrationHelper = new Mock<IRegistrationHelper>();
//            _semesterRegistrationRepository = new Mock<ISemesterRegistrationRepository>();

//            financialController = new FinancialController(_coreRepo.Object, _generalHelper.Object, _financialRepo.Object, _modelTransformHelper.Object, _studentRepository.Object, _semesterRepository.Object, _staffRepository.Object, _financialHelper.Object, _registrationHelper.Object, _semesterRegistrationRepository.Object);
//        }

//        [TestCategory("Financial Controller Tests")]
//        [TestMethod]
//        public void SpecialCharges_Test()
//        {
//            //act
//            var result = financialController.SpecialCharges() as ViewResult;

//            //assert
//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Model);
//            Assert.AreEqual(typeof(List<SpecialCharge>), result.Model.GetType());
//        }

//        [TestMethod]
//        public void AddOrEditSpecialChaarge_Test_View()
//        {
//            //arrange
            
//            //act 
//            var result = financialController.AddOrEditSpecialCharge() as ViewResult;

//            //assert 
//            Assert.IsNotNull(result);
//            Assert.IsNotNull(result.Model);
//            Assert.AreEqual(typeof(AddOrEditSpecialCharge), result.Model.GetType());
//        }

//        [TestMethod]
//        public void AddOrEditSpecialCharge_Invalidated_ModelState_Post()
//        {
//            //arrange
//            var model = new AddOrEditSpecialCharge();
//            financialController.ModelState.AddModelError("", "Error"); //enabling failed Validation

//            //act
//            var result = financialController.AddOrEditSpecialCharge(model) as ViewResult;

//            //assert..
//            Assert.IsNotNull(result);
//            Assert.AreEqual(typeof(AddOrEditSpecialCharge), result.Model.GetType());
//        }

//        [TestMethod]
//        public void AddOrEditSpecialCharge_Valid_ModelState_Post()
//        {
//            //arrange
//            var model = new AddOrEditSpecialCharge();
//            var outCome = new SpecialCharge();
//            financialController.ModelState.Clear(); //clearing any possible validations passed..

//            //setting up the default values..
//            _modelTransformHelper.Setup(x => x.GenerateSpecialCharge(model)).Returns(outCome);
//            _coreRepo.Setup(x => x.SaveOrUpdate(outCome)).Returns(true);

//            //act
//            var result = financialController.AddOrEditSpecialCharge(model) as RedirectToRouteResult;

//            //assert
//            Assert.IsNotNull(result, "The Outcome of the Post was not successful.");
//            Assert.AreEqual("SpecialCharges", result.RouteValues["action"]); //proving that the redirect was a success
//        }

//        [TestMethod]
//        public void SpecialCharge_Validation_Test()
//        {
//            //arrange
//            var id = 1;
//            _coreRepo.Setup(x => x.Get<SpecialCharge>(id)).Returns(new SpecialCharge()
//            {
//                Id = id,
//                Name = "Application",
//            });

//            //act
//            var result = financialController.SpecialCharge(id) as ViewResult;

//            //assert..
//            Assert.IsNotNull(result);
//            Assert.AreEqual("SpecialCharge", result.ViewName, "The View Name is not as expected");
//            Assert.IsNotNull(result.Model);
//            Assert.AreEqual(typeof(SpecialCharge), result.Model.GetType(), "The model should be Special Charge");
//            Assert.AreEqual(id, ((SpecialCharge)result.Model).Id, "The item picked doesn't correspond to the id requested.");
//        }
//    }
//}
