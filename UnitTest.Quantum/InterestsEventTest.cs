using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantum.ClassLibrary;
using System;

namespace UnitTest.Quantum
{
    [TestClass]
    public class InteresetsEventTest
    {


        [DataTestMethod]
        [DataRow(false)]
        public void Test_InteresetsEventTest_2InterestWithSameDate(bool expected)
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


            var v = QuantumValidator.IsValidInterestsEvent(dealMap, "PAY", new DealBusinessObject());
            Assert.AreEqual(v, expected);

        }
        [DataTestMethod]
        [DataRow(true)]
        public void Test_InteresetsEventTest_TwoInterestWithNotMatchingDate(bool expected)
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
            new DealMapItem() { Comment = "CAPITALISE INTEREST", Effect_dt = DateTime.Today.AddDays(-20), Flag = "PAY", transtype = TransTypeEnum.ettSE });



            dealMap.Add(
            new DealMapItem() { Comment = "CLOSEOUT", Effect_dt = DateTime.Today.AddDays(-30), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            var v = QuantumValidator.IsValidInterestsEvent(dealMap, "PAY", new DealBusinessObject());
            // QuantumValidator.ValidateMapPre(dealMap, "PAY", new DealBusinessObject());

            ///
            Assert.AreEqual(v, expected);

        }
        [DataTestMethod]
        [DataRow(true)]
        public void Test_InteresetsEventTest_NoInterests(bool expected)
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
            new DealMapItem() { Comment = "CLOSEOUT", Effect_dt = DateTime.Today.AddDays(-30), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-2), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            var v = QuantumValidator.IsValidInterestsEvent(dealMap, "PAY", new DealBusinessObject());
            QuantumValidator.ValidateMapPre(dealMap, "PAY", new DealBusinessObject());

            ///
            Assert.AreEqual(v, expected);

        }

        /// <summary>
        /// Valid Test for AmortisationTest for empty comments
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_InterestsEvent_EmptyComments_ValidTest(bool Expected)
        {
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });


            var v = QuantumValidator.IsValidInterestsEvent(dealMap, "PAY", new DealBusinessObject());

            ///
            Assert.AreEqual(v, Expected);

        }
        /// <summary>
        /// Valid Test for AmortisationTest for empty 
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_InterestsEventTest_Empty_Test(bool Expected)
        {
            DealMap dealMap = new DealMap();

            var v = QuantumValidator.IsValidInterestsEvent(dealMap, "PAY", new DealBusinessObject());

            ///
            Assert.AreEqual(v, Expected);

        }


    }
}
