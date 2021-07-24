namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class BidQuoteAction
    {
        public int BidQuoteCount(string strWhere)
        {
            return publicDbOpClass.GetRecordCount("EPM_V_Bid_InviteBidSearch", strWhere);
        }

        public DataTable BidquoteList(string prjm, string prjn)
        {
            string sqlString = "";
            sqlString = "select top 1 ProjectCode,ProjectManualCode,ProjectName from EPM_Prj_Forepart where (1=1)";
            if (prjm != "")
            {
                sqlString = sqlString + " and ProjectManualCode like '%" + prjm + "%'";
            }
            else if (prjn != "")
            {
                sqlString = sqlString + " and ProjectName like '%" + prjn + "%'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable BidQuoteList()
        {
            string sqlString = "";
            sqlString = "select *,isnull(BidDeptID,0) as BidDeptID from EPM_V_Bid_InviteBidSearch";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable BidQuoteList(int parm)
        {
            return publicDbOpClass.DataTableQuary("select *,isnull(BidDeptID,0) as BidDeptID from EPM_V_Bid_InviteBidSearch where State in (" + parm + ")");
        }

        public DataTable BidQuoteList(string strWhere, int iPageSize, int iPageIndex)
        {
            return publicDbOpClass.GetRecordFromPage("EPM_V_Bid_InviteBidSearch", "AddDate", iPageSize, iPageIndex, 0, strWhere);
        }

        public int ProjectInfoAdd(BidInfo Bid, ProjectForepart pf, InviteBid objBid)
        {
            object obj2 = (((((((("begin" + " insert into EPM_Bid_BidList ") + " (" + " InviteBidCode, ") + " Principal, " + " BidDeptID, ") + " SubmitCetificateDate, " + " SubmitBailDate, ") + " SubmitBidDate, " + " Envelopment, ") + " OriginalSum, " + " CopySum, ") + " BidProjectManager," + " BidPrice,") + " BidRemark,ProjectCode" + " ) ") + " values " + " (";
            object obj3 = (string.Concat(new object[] { obj2, " '", Bid.InviteBidCode, "', " }) + " '" + Bid.Principal + "', ") + "\t " + Bid.BidDeptID.ToString() + ",";
            object obj4 = string.Concat(new object[] { obj3, " '", Bid.SubmitCetificateDate, "', " });
            object obj5 = string.Concat(new object[] { obj4, " '", Bid.SubmitBailDate, "', " });
            object obj6 = (((string.Concat(new object[] { obj5, " '", Bid.SubmitBidDate, "', " }) + " '" + Bid.Envelopment + "', ") + " '" + Bid.OriginalSum + "', ") + " '" + Bid.CopySum + "', ") + " '" + Bid.BidProjectManager + "', ";
            object obj7 = string.Concat(new object[] { obj6, " '", Bid.BidPrice, "'," });
            object obj8 = string.Concat(new object[] { obj7, " '", Bid.BidRemark, "','", pf.ProjectCode, "'" }) + " ) ";
            string str2 = string.Concat(new object[] { obj8, "insert into EPM_Prj_Forepart values('", pf.ProjectCode, "',", pf.ProjectType.ToString() });
            string str3 = str2 + ",'" + pf.ProjectManualCode + "','" + pf.ProjectName + "'," + pf.OwnerCode.ToString();
            object obj9 = str3 + ",'" + pf.Contact + "','" + pf.Locus + "','" + pf.ProjectIntro + "','" + pf.MainWork + "'";
            object obj10 = string.Concat(new object[] { obj9, ",'", pf.FundQuarry, "',", pf.RekonPrice.ToString(), ",'", pf.Remark, "',", Convert.ToInt32(pf.State) });
            object obj11 = ((((((((string.Concat(new object[] { obj10, ",'", pf.AddDate.ToString(), "',", pf.Prjtate, ",'", pf.PrjFollow, "')" }) + " insert into EPM_Bid_InviteBid " + " ( ") + " InviteBidCode, " + " ProjectCode, ") + " IssueDate, " + " BidType, ") + " BidMode, " + " BidRequest, ") + " BidLocus, " + " CloseDate, ") + " AgentInfo, " + " BidPaperPrice, ") + " Remark, " + " AddDate, ") + " CollectMode " + " ) ") + " values " + " ( ";
            object obj12 = string.Concat(new object[] { obj11, " '", objBid.InviteBidCode, "', " });
            object obj13 = string.Concat(new object[] { obj12, " '", objBid.ProjectCode, "', " });
            object obj14 = string.Concat(new object[] { obj13, " '", objBid.IssueDate, "', " });
            object obj15 = string.Concat(new object[] { obj14, " '", objBid.BidType, "', " });
            object obj16 = (string.Concat(new object[] { obj15, " '", objBid.BidMode, "', " }) + " '" + objBid.BidRequest + "', ") + " '" + objBid.BidLocus + "', ";
            object obj17 = string.Concat(new object[] { obj16, " '", objBid.CloseDate, "', " }) + " '" + objBid.AgentInfo + "', ";
            object obj18 = string.Concat(new object[] { obj17, " '", objBid.BidPaperPrice, "', " }) + " '" + objBid.Remark + "', ";
            return publicDbOpClass.ExecSqlString((string.Concat(new object[] { obj18, " '", objBid.AddDate, "', " }) + " '" + objBid.CollectMode + "' ") + " ) " + " end");
        }

        public int ProjectInfoDel(Guid Prj)
        {
            string str = " begin";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, " delete EPM_Bid_BidList where InviteBidCode='", Prj, "'" });
            object obj4 = string.Concat(new object[] { obj3, " delete EPM_Prj_ForePart where ProjectCode='", Prj, "'" });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " delete EPM_Bid_InviteBid where ProjectCode='", Prj, "'" }) + " end");
        }

        public int ProjectInfoUp(BidInfo Bid, ProjectForepart pf, InviteBid objBid)
        {
            object obj2 = ((" begin" + " update EPM_Bid_BidList set ") + " Principal='" + Bid.Principal + "', ") + " BidDeptID=" + Bid.BidDeptID.ToString() + ", ";
            object obj3 = string.Concat(new object[] { obj2, " SubmitCetificateDate='", Bid.SubmitCetificateDate, "', " });
            object obj4 = string.Concat(new object[] { obj3, " SubmitBailDate='", Bid.SubmitBailDate, "', " });
            object obj5 = (((string.Concat(new object[] { obj4, " SubmitBidDate='", Bid.SubmitBidDate, "', " }) + " Envelopment='" + Bid.Envelopment + "', ") + " OriginalSum='" + Bid.OriginalSum + "', ") + " CopySum='" + Bid.CopySum + "', ") + " BidProjectManager='" + Bid.BidProjectManager + "', ";
            object obj6 = (string.Concat(new object[] { obj5, " BidPrice='", Bid.BidPrice, "'," }) + " BidRemark='" + Bid.BidRemark + "'") + " where ";
            object obj7 = ((((((((string.Concat(new object[] { obj6, " InviteBidCode='", Bid.InviteBidCode, "' " }) + " update EPM_Prj_ForePart set ") + " ProjectType = " + pf.ProjectType.ToString() + ",") + " ProjectManualCode = '" + pf.ProjectManualCode + "',") + " ProjectName = '" + pf.ProjectName + "',") + " OwnerCode = " + pf.OwnerCode.ToString() + ",") + " Contact = '" + pf.Contact + "',") + " Locus = '" + pf.Locus + "',") + " ProjectIntro = '" + pf.ProjectIntro + "',") + " AddDate = '" + pf.AddDate.ToString() + "',";
            object obj8 = (string.Concat(new object[] { obj7, " PrjState = ", pf.Prjtate, " " }) + " where ProjectCode = '" + pf.ProjectCode.ToString() + "'") + " update EPM_Bid_InviteBid set";
            object obj9 = string.Concat(new object[] { obj8, " IssueDate='", objBid.IssueDate, "', " });
            object obj10 = string.Concat(new object[] { obj9, " BidType='", objBid.BidType, "', " });
            object obj11 = (string.Concat(new object[] { obj10, " BidMode='", objBid.BidMode, "', " }) + " BidRequest='" + objBid.BidRequest + "', ") + " BidLocus='" + objBid.BidLocus + "', ";
            object obj12 = string.Concat(new object[] { obj11, " CloseDate='", objBid.CloseDate, "', " }) + " AgentInfo='" + objBid.AgentInfo + "', ";
            object obj13 = string.Concat(new object[] { obj12, " BidPaperPrice='", objBid.BidPaperPrice, "', " });
            object obj14 = string.Concat(new object[] { obj13, " AddDate='", objBid.AddDate, "' " }) + " where ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj14, " ProjectCode='", objBid.ProjectCode, "' " }) + " end");
        }
    }
}

