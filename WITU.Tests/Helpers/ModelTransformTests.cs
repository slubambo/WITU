//using System;
//using System.Linq;
//using WITU.Core.Model;
//using WITU.Core.Repositories.Interfaces;
//using WITU.Helper.Impls;
//using WITU.Models;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace WITU.Tests.Helpers
//{
//    [TestClass]
//    public class ModelTransformTests
//    {
//        private ModelTransformHelper transformHelper;
//        private Mock<ICoreRepository> _coreRepo;
//        private Mock<IFinancialRepository> _financialRepo;

//        [TestInitialize]
//        public void SetUp()
//        {
//            transformHelper = new ModelTransformHelper();
//            _coreRepo = new Mock<ICoreRepository>();
//            _financialRepo = new Mock<IFinancialRepository>();

//            transformHelper.CoreRepository = _coreRepo.Object;
//            transformHelper.FinancialRepository = _financialRepo.Object;
//        }

//        [TestMethod]
//        public void GenerateFunctionalFeeDetail_Test()
//        {
//            //arrange 
//            var addOrEditModel = new AddOrEditFee()
//            {
//                StartAcademicYear = 1,
//                Fees = 10000,
//                FunctionalFeeId = 4,
//                Id = 0,
//                YearOfStudy = 2
//            };

//            //act
//            var result = transformHelper.GenerateFunctionalFeeDetail(addOrEditModel);

//            //assert
//            Assert.IsNotNull(result, "The Actual outcome of this result is null");
//            //Assert.IsNotNull(result.StartAcademicYear, "Start Academic Year is Null");
//            //Assert.AreEqual(result.StartAcademicYear.GetType(), typeof(AcademicYear));
//            Assert.AreEqual(result.YearOfStudy, addOrEditModel.YearOfStudy);
//            Assert.AreEqual(result.Fee, addOrEditModel.Fees);
//            Assert.AreEqual(result.Id, addOrEditModel.Id);
//        }

//        [TestMethod]
//        public void GenerateSpecialCharge_Test()
//        {
//            //arrange
//            var model = new AddOrEditSpecialCharge()
//            {
//                StartAcademicYear = 1,
//                Description = "This is the Description",
//                SpecialChargeId = 5,
//                Particular = "Foo",
//                Rate = 9000,
//                DetailId = 8
//            };

//            _financialRepo.Setup(x => x.ActiveSpecialChargeDetail(model.SpecialChargeId));


//            //act
//            var result = transformHelper.GenerateSpecialCharge(model);

//            //assert
//            Assert.IsNotNull(result, "The Special Charge was not generated");
//            Assert.AreEqual(result.Id, model.SpecialChargeId);
//            Assert.IsNotNull(result.SpecialChargeDetails);
//            Assert.IsTrue(result.SpecialChargeDetails.Count > 0, 
//                "At least the Special Charge Detail should have been created.");

//            var resultDetail = result.SpecialChargeDetails.FirstOrDefault();
//            if (resultDetail != null)
//            {
//                Assert.AreEqual(resultDetail.Rate, model.Rate, "The rates are not matching");
//            }
//        }
//    }

    
//}
