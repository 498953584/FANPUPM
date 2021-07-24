namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class TenderAction
    {
        public int AddTender(BidCorpCollection bc)
        {
            string sqlString = " ";
            if (bc.Count > 0)
            {
                object obj2 = sqlString;
                sqlString = string.Concat(new object[] { obj2, " delete from EPM_Bid_BidCorp where InviteBidCode='", bc[0].InviteBidCode, "' " });
                for (int i = 0; i < bc.Count; i++)
                {
                    object obj3 = sqlString + " insert into EPM_Bid_BidCorp(InviteBidCode,CorpCode,SubmitBidDate,BidPrice,WorkDay,IsWin,IsJoin,ManagerTitle,Summary,ResultCode) " + " values( ";
                    object obj4 = string.Concat(new object[] { obj3, " '", bc[i].InviteBidCode, "', " });
                    object obj5 = string.Concat(new object[] { obj4, " '", bc[i].CorpCode, "', " });
                    object obj6 = string.Concat(new object[] { obj5, " '", bc[i].SubmitBidDate, "', " });
                    object obj7 = string.Concat(new object[] { obj6, " '", bc[i].BidPrice, "', " });
                    object obj8 = (string.Concat(new object[] { obj7, " '", bc[i].WorkDay, "', " }) + " '" + Convert.ToInt32(bc[i].IsWin).ToString() + "', ") + " '" + Convert.ToInt32(bc[i].IsJoin).ToString() + "', ";
                    object obj9 = string.Concat(new object[] { obj8, " '", bc[i].ManagerTitle, "', " }) + " '" + bc[i].Summary + "', ";
                    sqlString = string.Concat(new object[] { obj9, " '", bc[i].ResultCode, "' " }) + " ) ";
                }
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteTender(Guid guidProjectCode)
        {
            string sqlString = " ";
            object obj2 = sqlString + " begin ";
            object obj3 = string.Concat(new object[] { obj2, " delete from EPM_Prj_Forepart where ProjectCode='", guidProjectCode, "' " });
            object obj4 = string.Concat(new object[] { obj3, " delete from EPM_Bid_InviteBid where ProjectCode='", guidProjectCode, "' " });
            sqlString = string.Concat(new object[] { obj4, " delete from EPM_Bid_OpenBid where InvitedBidCode='", guidProjectCode, "' " }) + " end ";
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int GetTenderCount(string strWhere)
        {
            return publicDbOpClass.GetRecordCount("EPM_V_Bid_OpenSearch", strWhere);
        }

        public DataTable GetTenderList(int intPageSize, int intCurentPage, string strWhere)
        {
            return publicDbOpClass.GetRecordFromPage("EPM_V_Bid_OpenSearch", "AddDate", intPageSize, intCurentPage, 0, strWhere);
        }
    }
}

