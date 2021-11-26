using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantum.ClassLibrary;
using Quantum.ClassLibrary.ExtensionMethod;
using System;
using System.Linq;

namespace UnitTest.Quantum
{
    [TestClass]
    public class ClosingPrincipalEventTest
    {

        /// <summary>
        /// Invalid Test for Closing Principal Event Invalid as AMORTISATION is last Entry
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_IsValidClosingPrincipalEvent_AMORTISATION_Last(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            //Action
            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Test for Closing Principal Event Valid as REPAY PRINCIPAL is last Entry
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_IsValidClosingPrincipalEvent_validTest_REPAY_PRINCIPAL(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "REPAY PRINCIPAL", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Invalid Test for Closing Principal Event Valid as REPAY PRINCIPAL is last Entry
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_IsValidClosingPrincipalEvent_REPAYPRINCIPAL(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "REPAYPRINCIPAL", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Invalid Test for Closing Principal Event Valid as REPAY PRINCIPAL is last Entry
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_IsValidClosingPrincipalEvent_UNWIND(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "result UNWIND ", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            //Action
            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Invalid Test for Closing Principal Event Valid as REPAY PRINCIPAL is last Entry
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_IsValidClosingPrincipalEvent_UNWIND1(bool expected)
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
            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///Assert
            Assert.AreEqual(v, expected);

        }
        /// <summary>
        /// Valid Test for Closing Principal Event for empty comments
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_IsValidClosingPrincipalEvent_EmptyComments_ValidTest(bool Expected)
        {
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///
            Assert.AreEqual(v, Expected);

        }
        /// <summary>
        /// Valid Test for Closing Principal Event for empty 
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_IsValidClosingPrincipalEvent_Empty_Test(bool Expected)
        {
            DealMap dealMap = new DealMap();

            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///
            Assert.AreEqual(v, Expected);

        }
        [DataTestMethod]
        [DataRow(false)]
        public void Test_IsValidClosingPrincipalEvent_Test1(bool expected)
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


            var v = QuantumValidator.IsValidClosingPrincipalEvent(dealMap, "PAY");

            ///
            Assert.AreEqual(v, expected);

        }
    }
}
