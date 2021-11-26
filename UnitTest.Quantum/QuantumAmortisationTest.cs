using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantum.ClassLibrary;
using Quantum.ClassLibrary.ExtensionMethod;
using System;
using System.Linq;

namespace UnitTest.Quantum
{
    [TestClass]
    public class QuantumAmortisationTest
    {

        /// <summary>
        /// Invalid Test 
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_QuantumAmortisationTest_1AuthorisationWith1_NotMatching_Coupon(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Test for AmortisationTest Valid  
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_QuantumAmortisationTest_1AuthorisationWith1MatchingCoupon(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "REPAY PRINCIPAL", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Test for 2 Authorisation with 2 Matching Coupon
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_QuantumAmortisationTest_2AuthorisationWith2MatchingCoupon(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-60), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test1 dfd", Effect_dt = DateTime.Today.AddDays(-60), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
                new DealMapItem() { Comment = "REPAYPRINCIPAL", Effect_dt = DateTime.Today.AddDays(-30), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
                 new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
               new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });



            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        ///  Test for Test_QuantumAmortisationTest 1 Coupon with 2 AMORTISATION but Second not matching
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_QuantumAmortisationTest_1CouponWith2AMORTISATION4(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
           new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test1 dfd", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            dealMap.Add(
              new DealMapItem() { Comment = "result UNWIND ", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        ///  Test for Test_QuantumAmortisationTest 2 Coupon with 1 AMORTISATION but Second not matching
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_QuantumAmortisationTest_2CouponWith1AMORTISATION4(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
           new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test1 dfd", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            dealMap.Add(
              new DealMapItem() { Comment = "result UNWIND ", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        ///  Test for Test_QuantumAmortisationTest 2 Coupon with 2 AMORTISATION but Second not matching
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_QuantumAmortisationTest_2CouponWith2AMORTISATIONButSecondNotMatching(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
           new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test1 dfd", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-1), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "result UNWIND ", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        ///  Test for Test_QuantumAmortisationTest 2 Coupon with 2 AMORTISATION but First not matching
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_QuantumAmortisationTest_2CouponWith2AMORTISATIONButFirstNotMatching(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
           new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test1 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-10), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-10), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "result UNWIND ", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Invalid Test for AmortisationTest  
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_QuantumAmortisationTest_1CouponWith1AMORTISATIONButNotMatching(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "UNWIND1", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Valid Test for AmortisationTest for empty comments
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_QuantumAmortisationTest_EmptyComments_ValidTest(bool Expected)
        {
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///
            Assert.AreEqual(v, Expected);

        }
        /// <summary>
        /// Valid Test for AmortisationTest for empty 
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_QuantumAmortisationTest_Empty_Test(bool Expected)
        {
            DealMap dealMap = new DealMap();

            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///
            Assert.AreEqual(v, Expected);

        }
        [DataTestMethod]
        [DataRow(false)]
        public void Test_QuantumAmortisationTest_LotsItem_NotMatching(bool expected)
        {
            DealMap dealMap = new DealMap();
            dealMap.Add(
                new DealMapItem() { Comment = "REPAY NOTIONAL", Effect_dt = DateTime.Today, Flag = "PAY", transtype = TransTypeEnum.ettSE });
            dealMap.Add(
              new DealMapItem() { Comment = "REPAY PRINCIPAL", Effect_dt = DateTime.Today, Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
               new DealMapItem() { Comment = "UNWIND", Effect_dt = DateTime.Today, Flag = "PAY", transtype = TransTypeEnum.ettSE });
            dealMap.Add(
               new DealMapItem() { Comment = "REPAY NOTIONAL", Effect_dt = DateTime.Today.AddDays(-5), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
               new DealMapItem() { Comment = "AMORTISATION test1", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "INTEREST(ACC)", Effect_dt = DateTime.Today.AddDays(-30), Flag = "PAY", transtype = TransTypeEnum.ettSE });
            dealMap.Add(
            new DealMapItem() { Comment = "CAPITALISE INTEREST", Effect_dt = DateTime.Today.AddDays(-30), Flag = "PAY", transtype = TransTypeEnum.ettSE });



            dealMap.Add(
            new DealMapItem() { Comment = "CLOSEOUT", Effect_dt = DateTime.Today.AddDays(-30), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            var v = QuantumValidator.IsValidAmortisationEvent(dealMap, "PAY");

            ///
            Assert.AreEqual(v, expected);

        }
    }
}
