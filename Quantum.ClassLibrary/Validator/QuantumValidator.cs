using Quantum.ClassLibrary.Constants;
using Quantum.ClassLibrary.CustomException;
using Quantum.ClassLibrary.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quantum.ClassLibrary.Enum;

namespace Quantum.ClassLibrary
{
    public class QuantumValidator
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Validate Map
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <param name="deal"></param>
        public static bool ValidateMap(DealMap dealMap, string flag, DealBusinessObject deal)
        {
            List<QuantumException> quantumExceptions = new List<QuantumException>();

            try
            {
                log.Info("Starting Validor");
                log.Info($"Checking {QuantumException.AClosingPrincipalEventShouldAppearAsTheLastEntryInTheDealMap}");

                if (!IsValidClosingPrincipalEvent(dealMap, flag))
                {
                    quantumExceptions.Add(QuantumException.AClosingPrincipalEventShouldAppearAsTheLastEntryInTheDealMap);
                }
                log.Info($"Checking {QuantumException.TheEffectiveDateOfAmortisationsMustBeAParDate}");

                if (!IsValidAmortisationEvent(dealMap, flag))
                {
                    quantumExceptions.Add(QuantumException.TheEffectiveDateOfAmortisationsMustBeAParDate);
                }
                log.Info($"Checking {QuantumException.InterestIsInvalidException}");

                if (!IsValidInterestsEvent(dealMap, flag, deal))
                {
                    quantumExceptions.Add(QuantumException.InterestIsInvalidException);
                }
             
            }
            catch(Exception ex)
            {
                ///log Error Log4Net
                ///stack trace of this exception will give you the full detail
                log.Error($"Error during runnning of validtor details {ex.Message}");

                throw;
               
            }
            if (quantumExceptions.Count > 0)
            {
                log.Info($"Quantum Exceptions: {string.Join(Environment.NewLine,  quantumExceptions)}");

                throw new ValidationException(quantumExceptions);

            }
            return true;


        }
        /// <summary>
        /// Is Valid Closing Principal Event
        /// A Closing Principal Event Should Appear As The Last Entry In The DealMap
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <returns></returns>

        public static bool IsValidClosingPrincipalEvent(DealMap dealMap, string flag)
        {
            // Check final entry in the map
            DealMapItem dealFinalMapItemEntry = GetLastEntryDealMapItemMatchingFlag(dealMap, flag);

            if (dealFinalMapItemEntry != null)
            {
                //Valid if Last Comment Matching Repay List
                if (dealFinalMapItemEntry.Comment.MatchAny(CommentContants.REPAY_LIST))
                {
                    return true;
                }
                // Check for Close Out Generic Entry
                if (dealFinalMapItemEntry.Comment.MatchFull(CommentContants.CLOSEOUT_GENERIC_ENTRY))
                {
                    return true;
                }
                //Valid if Last Comment Contains UNWIND
                if (dealFinalMapItemEntry.Comment.MatchPartial(CommentContants.PARTIAL_UNWIND))
                {
                    return true;
                }
                //Invalid Throw Exception AClosingPrincipalEventShouldAppearAsTheLastEntryInTheDealMap
                return false;

            }

            return true;
        }
        /// <summary>
        /// 46555 Re-introduce amortisation validation. Was dropped when the GL server was introduced.
        /// Amortisations must occur on Coupon dates
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <returns></returns>

        public static bool IsValidAmortisationEvent(DealMap dealMap, string flag)
        {

            List<DealMapItem> dealMapAmortisationItems = GetDealMapItemMatchPartial(dealMap, flag, CommentContants.PARTIAL_AMORTISATION);
            if (dealMapAmortisationItems.Count > 0)
            {
                foreach (DealMapItem dealMapAmortisationItem in dealMapAmortisationItems)
                {
                    //Look for the previous coupon date
                    DateTime dtCoupDate = CtGlobal.THE_BEGINNING_DATE;

                    DealMapItem previousCouponItem = GetDealMapItemMatchFull(dealMap, flag, CommentContants.COUPON)?.LastOrDefault(d => d.Effect_dt <= dealMapAmortisationItem.Effect_dt);
                    if (previousCouponItem != null)
                    {
                        dtCoupDate = previousCouponItem.Effect_dt.GetValueOrDefault();
                    }

                    //Check if the Amortisation date the same as the coupon date we just found
                    //if not raise invalid entry
                    if (dealMapAmortisationItem.Effect_dt != dtCoupDate)
                    {
                        return false;
                    }
                }
            }


            return true;
        }

        /// <summary>
        /// only one of INTEREST (ACC), INTEREST & CAPITALISE INTEREST can exist on the same effective day AQT-20735
        /// dtInterestDate will only be set if interest already found for this same effective date
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <returns></returns>

        public static bool IsValidInterestsEvent(DealMap dealMap, string flag, DealBusinessObject deal)
        {
            DateTime? dtInterestDate = CtGlobal.THE_BEGINNING_DATE;
            foreach (DealMapItem dealMapItemMatchingFlag in GetDealMapItemMatchingFlag(dealMap, flag))
            {
                //clear/reset when onto a new date
                if (dtInterestDate != CtGlobal.THE_BEGINNING_DATE & dtInterestDate != dealMapItemMatchingFlag.Effect_dt)
                {
                    dtInterestDate = CtGlobal.THE_BEGINNING_DATE;
                }
                /// get Interest list match Effect date
                List<DealMapItem> interestListDealMapItem = GetDealMapItemAnyMatchingList(dealMap, flag, CommentContants.INTEREST_LIST);
                //dtInterestDate will only be set if interest already found for this same effective date
                if (interestListDealMapItem.FirstOrDefault(d => d.Effect_dt == dtInterestDate) != null)
                {
                    return false;

                }
                else
                {
                    dtInterestDate = dealMapItemMatchingFlag.Effect_dt; //set when interest found
                }


            }
            return true;

        }
        /// <summary>
        /// Get Last Deal Map Items matching flag where Comments are non empty
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static DealMapItem GetLastEntryDealMapItemMatchingFlag(DealMap dealMap, string flag)
        {
            return GetDealMapItemMatchingFlag(dealMap, flag)?.LastOrDefault();
        }
        /// <summary>
        /// Get list of all the Deal Map Items matching flag where Comments are non empty
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static List<DealMapItem> GetDealMapItemMatchingFlag(DealMap dealMap, string flag)
        {
            return dealMap.Items.ToList()?.FindAll(d => d.Flag.Equals(flag, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(d.Comment));
        }
        /// <summary>
        /// Get Matching Any DealMapItem where Comment Match List
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<DealMapItem> GetDealMapItemAnyMatchingList(DealMap dealMap, string flag, string list)
        {
            return GetDealMapItemMatchingFlag(dealMap, flag)?.FindAll(a => a.Comment.MatchAny(list));
        }
        /// <summary>
        /// Get All Matching DealMapItem Comment contains partialString
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        private static List<DealMapItem> GetDealMapItemMatchPartial(DealMap dealMap, string flag, string partialString)
        {
            return GetDealMapItemMatchingFlag(dealMap, flag)?.FindAll(a => a.Comment.MatchPartial(partialString));
        }
        /// <summary>
        /// Get All Matching DealMapItem Where Comment equal stringValue
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        private static List<DealMapItem> GetDealMapItemMatchFull(DealMap dealMap, string flag, string stringValue)
        {
            return GetDealMapItemMatchingFlag(dealMap, flag)?.FindAll(a => a.Comment.MatchFull(stringValue));
        }
        /// <summary>
        /// Not used 
        /// </summary>
        /// <param name="dealMap"></param>
        /// <param name="flag"></param>
        /// <param name="deal"></param>
        /// 
        [Obsolete]
        public static void ValidateMapPre(DealMap dealMap, string flag, DealBusinessObject deal)
        {
            int iMap;

            //if (deal.TransactionType == TransTypeEnum.ettSE)
            if (dealMap.Count > 0 && dealMap[0].transtype == TransTypeEnum.ettSE)   //AQT-29941 - Bring code back to v5 otherwise some MM rolled deals become un-customisable.
            {
                //only one of INTEREST (ACC), INTEREST & CAPITALISE INTEREST can exist on the same effective day AQT-20735
                DateTime? dtInterestDate = CtGlobal.THE_BEGINNING_DATE;   //clear/reset
                for (iMap = dealMap.Count - 1; iMap >= 0; iMap += -1)
                {
                    if (!string.IsNullOrEmpty(dealMap[iMap].Comment) & dealMap[iMap].Flag == flag)
                    {
                        if (dtInterestDate != CtGlobal.THE_BEGINNING_DATE & dtInterestDate != dealMap[iMap].Effect_dt)   //clear/reset when onto a new date
                            dtInterestDate = CtGlobal.THE_BEGINNING_DATE;
                        if (dealMap[iMap].Comment.MatchAny(CommentContants.INTEREST_LIST))
                        {
                            if (dtInterestDate == dealMap[iMap].Effect_dt)  //dtInterestDate will only be set if interest already found for this same effective date
                            {
                                throw new ValidationException("Interest is invalid");
                            }
                            dtInterestDate = dealMap[iMap].Effect_dt; //set when interest found
                        }
                    }
                }

            }
        }
    }


}

