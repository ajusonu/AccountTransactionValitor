using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.ClassLibrary.Constants
{

    /// <summary>
    /// constants
    /// </summary>
    public static class CommentContants
    {
        #region Constants for Comments
        /// <summary>
        /// 
        /// </summary>
        public const string REPAY_NOTIONAL = "REPAY NOTIONAL";
        /// <summary>
        /// 
        /// </summary>
        public const string REPAY_PRINCIPAL = "REPAY PRINCIPAL";
        /// <summary>
        /// 
        /// </summary>
        public const string PARTIAL_UNWIND = "UNWIND";
        /// <summary>
        /// 
        /// </summary>
        public const string PARTIAL_AMORTISATION = "AMORTISATION";
        /// <summary>
        /// 
        /// </summary>
        public const string CLOSEOUT_GENERIC_ENTRY = "CLOSEOUT";

        /// <summary>
        /// 
        /// </summary>
        public const string INTEREST_ACC = "INTEREST(ACC)";
        public const string INTEREST = "INTEREST";
        /// <summary>
        /// 
        /// </summary>
        public const string CAPITALISE_INTEREST = "CAPITALISE INTEREST";
        /// <summary>
        /// 
        /// </summary>
        public const string COUPON = "COUPON";

        public const string INTEREST_LIST = CAPITALISE_INTEREST + "," + INTEREST + "," + INTEREST_ACC;
        public const string REPAY_LIST = REPAY_NOTIONAL + "," + REPAY_PRINCIPAL;

       
        #endregion
    }
}
