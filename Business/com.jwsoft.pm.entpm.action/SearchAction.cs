namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.entpm.model;
    using System;

    public class SearchAction
    {
        public static string returnQueryFilter(SearchInfo si, string strFlag)
        {
            string str = "";
            string[] strArray = si.Price.Split(new char[] { '-' });
            if (strFlag == "Market")
            {
                if (si.BeginDate.Trim() != "")
                {
                    str = str + " AddDate >= '" + si.BeginDate + "' and";
                }
                if (si.EndDate.Trim() != "")
                {
                    str = str + " AddDate <= '" + si.EndDate + "' and";
                }
                string str2 = str;
                str = ((str2 + " ProjectManualCode like '%" + si.InfoCode + "%' and ProjectName like '%" + si.PrjName + "%'") + " and OwnerCode = '" + si.PrjCorp + "' ") + " and RekonPrice >= " + strArray[0];
                if (strArray[1].ToString().Trim() != "*")
                {
                    str = str + " and RekonPrice < " + strArray[1].ToString().Trim();
                }
                if (si.Status != "%")
                {
                    str = str + " and state = " + si.Status;
                }
                if (si.PrjAddress != "9999")
                {
                    str = str + " and Locus = " + si.PrjAddress;
                }
                if (si.PrjType != "9999")
                {
                    str = str + " and ProjectType = '" + si.PrjType + "'";
                }
                if (si.InviteBidType != "9999")
                {
                    str = str + " and BidType = " + si.InviteBidType;
                }
                if (si.InviteBidMode != "9999")
                {
                    str = str + "  and BidMode = " + si.InviteBidMode;
                }
                return str;
            }
            if (strFlag == "InviteBid")
            {
                if (si.BeginDate.Trim() != "")
                {
                    str = str + " IssueDate >= '" + si.BeginDate + "' and";
                }
                if (si.EndDate.Trim() != "")
                {
                    str = str + " IssueDate <= '" + si.EndDate + "' and";
                }
                string str3 = str;
                str = ((str3 + " ProjectManualCode like '%" + si.InfoCode + "%' and ProjectName like '%" + si.PrjName + "%'") + " and OwnerCode = '" + si.PrjCorp + "'") + " and BidPrice >= " + strArray[0];
                if (strArray[1].ToString().Trim() != "*")
                {
                    str = str + " and BidPrice < " + strArray[1].ToString().Trim();
                }
                if (si.InviteBidDept != "")
                {
                    str = str + " and BidDeptID = " + si.InviteBidDept;
                }
                if (si.PrjAddress != "9999")
                {
                    str = str + " and Locus = " + si.PrjAddress;
                }
                if (si.PrjType != "9999")
                {
                    str = str + " and ProjectType = '" + si.PrjType + "'";
                }
                if (si.InviteBidType != "9999")
                {
                    str = str + " and BidType = " + si.InviteBidType;
                }
                if (si.InviteBidMode != "9999")
                {
                    str = str + "  and BidMode = " + si.InviteBidMode;
                }
                return str;
            }
            if (strFlag == "BingoBid")
            {
                if (si.BeginDate.Trim() != "")
                {
                    str = str + " OpenBidDate >= '" + si.BeginDate + "' and";
                }
                if (si.EndDate.Trim() != "")
                {
                    str = str + " OpenBidDate <= '" + si.EndDate + "' and";
                }
                string str4 = str;
                str = ((str4 + " ProjectManualCode like '%" + si.InfoCode + "%' and ProjectName like '%" + si.PrjName + "%'") + " and OwnerCode = '" + si.PrjCorp + "' ") + " and BidPrice >= " + strArray[0];
                if (strArray[1].ToString().Trim() != "*")
                {
                    str = str + " and BidPrice < " + strArray[1].ToString().Trim();
                }
                if (si.BingoCorp != "")
                {
                    str = str + " and BingoCorp = '" + si.BingoCorp + "'";
                }
                if (si.PrjAddress != "9999")
                {
                    str = str + " and Locus = " + si.PrjAddress;
                }
                if (si.PrjType != "9999")
                {
                    str = str + " and ProjectType = '" + si.PrjType + "'";
                }
                if (si.InviteBidType != "9999")
                {
                    str = str + " and BidType = " + si.InviteBidType;
                }
                if (si.InviteBidMode != "9999")
                {
                    str = str + "  and BidMode = " + si.InviteBidMode;
                }
            }
            return str;
        }
    }
}

