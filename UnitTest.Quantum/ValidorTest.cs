using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantum.ClassLibrary;
using Quantum.ClassLibrary.ExtensionMethod;
using System;
using System.Linq;

namespace UnitTest.Quantum
{
    [TestClass]
    public class ValidorTest
    {

        /// <summary>
        /// Test_ValidateMap
        /// </summary>
        [DataTestMethod]
        [DataRow(false)]
        public void Test_ValidateMap(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

            dealMap.Add(
            new DealMapItem() { Comment = "COUPON", Effect_dt = DateTime.Today.AddDays(-6), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            dealMap.Add(
              new DealMapItem() { Comment = "AMORTISATION test2 dfd", Effect_dt = DateTime.Today.AddDays(-3), Flag = "PAY", transtype = TransTypeEnum.ettSE });

            //Action
            bool test=false;
            try
            {
                test = QuantumValidator.ValidateMap(dealMap, "PAY", new DealBusinessObject());
            }catch(Exception ex)
            {
                Assert.AreEqual(ex == null, expected);
            }

            ///Assert
            Assert.AreEqual(test, expected);

        }
        /// <summary>
        /// Test_ValidateMap
        /// </summary>
        [DataTestMethod]
        [DataRow(true)]
        public void Test_ValidateMap_Empty(bool expected)
        {
            //Arrange
            DealMap dealMap = new DealMap();

          

            //Action
            bool test = false;
            try
            {
                test = QuantumValidator.ValidateMap(dealMap, "PAY", new DealBusinessObject());
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex == null, expected);
            }

            ///Assert
            Assert.AreEqual(test, expected);

        }

    }
}
