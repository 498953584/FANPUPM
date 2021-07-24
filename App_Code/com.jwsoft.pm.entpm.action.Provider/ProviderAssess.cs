namespace com.jwsoft.pm.entpm.action.Provider
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;

    public class ProviderAssess
    {
        public bool addAssessInfo(string ItemClassID, string AssessDate, string OriginateDate, string Organiger, string Remark, string UserCode, string RecordDate)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_provider_AssessMain", "AssessID")) + 1;
            return publicDbOpClass.NonQuerySqlString("insert into pm_provider_AssessMain (AssessID,ItemClassID,AssessDate,OriginateDate,Organiger,Remark,UserCode,RecordDate,TransactState,AuditState) values (" + num.ToString() + "," + ItemClassID + ",'" + AssessDate + "','" + OriginateDate + "'," + Organiger + "," + Remark + ",'" + UserCode + "','" + RecordDate + "','0',0)");
        }

        public bool addAssessItemInfo(string ItemClassID, string AssessItemName, string AssessCriterion, string AssessPersonCode, string UserCode, string RecordDate, string IsValid, string AssessItem)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_provider_AssessItem", "AssessItemID")) + 1;
            return publicDbOpClass.NonQuerySqlString("insert into pm_provider_AssessItem (AssessItemID,ItemClassID,AssessItemName,AssessCriterion,AssessPersonCode,UserCode,RecordDate,IsValid,AssessItem) values (" + num.ToString() + ",'" + ItemClassID + "'," + AssessItemName + "," + AssessCriterion + ",'" + AssessPersonCode + "','" + UserCode + "','" + RecordDate + "','" + IsValid + "','" + AssessItem + "')");
        }

        public bool AssessTransact(string AssessID)
        {
            bool flag = false;
            string sqlString = "select count(1) from pm_provider_AssessItemDetail  where TransactState = '0' and AssessDetailID in (select AssessDetailID from pm_provider_AssessDetail where AssessID =" + AssessID + ")";
            int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
            if (int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString()) == 0)
            {
                flag = publicDbOpClass.NonQuerySqlString("update pm_provider_AssessMain set TransactState = '2' where AssessID =" + AssessID);
            }
            return flag;
        }

        public bool delAssessInfo(string AssessID)
        {
            string str = "begin ";
            return publicDbOpClass.NonQuerySqlString((((str + " delete pm_provider_AssessItemDetail where AssessDetailID in (select AssessDetailID from pm_provider_AssessDetail where AssessID=" + AssessID + ")") + " delete pm_provider_AssessDetail where AssessID=" + AssessID) + " delete pm_provider_AssessMain where AssessID=" + AssessID) + " end");
        }

        public bool delAssessItem(string AssessItemID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_provider_AssessItem where AssessItemID=" + AssessItemID);
        }

        public bool delCheckAssessItem(string AssessItemID)
        {
            bool flag = false;
            int num = 0;
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_provider_AssessItemDetail where AssessItemID=" + AssessItemID);
            if (num > 0)
            {
                flag = true;
            }
            return flag;
        }

        public DataTable GetAssessDetail(string AssessID)
        {
            return publicDbOpClass.DataTableQuary("select b.*,StateName =  (case b.State  when '0' then '已删除' when '1' then '已准入' when '2' then '未准入' when '3' then '已注销' end)   ,dbo.f_Prj_ProvAssessScoring(a.AssessID,a.ProviderId) as AssessScoring from pm_provider_AssessDetail a left join pm_Provider_Info b on a.ProviderId = b.ProviderId where a.AssessID =" + AssessID);
        }

        public DataTable GetAssessInfo(string AssessID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_provider_AssessMain where AssessID=" + AssessID);
        }

        public DataTable GetAssessItemClass()
        {
            string sqlString = "select * from pm_provider_AssessItemClass";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetAssessItemInfo(string AssessItemID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.v_xm from pm_provider_AssessItem a left join  pt_yhmc b on a.AssessPersonCode = b.v_yhdm where a.AssessItemID=" + AssessItemID);
        }

        public DataTable GetAssessItemList(int flag, string ItemClassID)
        {
            string sqlString = "";
            if (flag == 0)
            {
                sqlString = "select a.*,b.v_xm from pm_provider_AssessItem a left join  pt_yhmc b on a.AssessPersonCode = b.v_yhdm where a.ItemClassID=" + ItemClassID + " order by AssessItem";
            }
            else if (flag == 1)
            {
                sqlString = "select a.*,b.v_xm from pm_provider_AssessItem a left join  pt_yhmc b on a.AssessPersonCode = b.v_yhdm where a.IsValid = '1' and a.ItemClassID=" + ItemClassID + " order by AssessItem";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetAssessList(string SqlWhere)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.ItemClassName ,CONVERT(char(10), a.AssessDate, 111) as VAssessDate ,CONVERT(char(10), a.OriginateDate, 111) as VOriginateDate ,TransactStateName =  ( case a.TransactState  when '0' then '未提请办理' when '1' then '正在办理' when '2' then '已办理' end ) ,AuditStateName =  ( case a.AuditState  when -1 then '未启动' when 0 then '流转中' when 1 then '审核通过' when -2 then '驳回' end ) from pm_provider_AssessMain a  left join pm_provider_AssessItemClass b on a.ItemClassID = b.ItemClassID " + SqlWhere + " order by a.OriginateDate desc");
        }

        public DataTable GetAssessListOnProviderId(string providerId)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.ItemClassName,c.Score,c.AssessResult ,CONVERT(char(10), a.AssessDate, 111) as VAssessDate ,CONVERT(char(10), a.OriginateDate, 111) as VOriginateDate ,TransactStateName =  ( case a.TransactState  when '0' then '未提请办理' when '1' then '正在办理' when '2' then '已办理' end ) ,AuditStateName =  ( case a.AuditState  when -1 then '未启动' when 0 then '流转中' when 1 then '审核通过' when -2 then '驳回' end ) from pm_provider_AssessMain a  left join pm_provider_AssessItemClass b on a.ItemClassID = b.ItemClassID  left join pm_provider_AssessDetail c on a.AssessID=c.AssessID where c.ProviderId=" + providerId + " and a.AssessID in(select c.AssessID from pm_provider_AssessDetail c where c.ProviderId=" + providerId + ") and a.TransactState=2 order by a.RecordDate desc");
        }

        public string GetAssessProviderIDs(string AssessID)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select ProviderId from pm_provider_AssessDetail where AssessID=" + AssessID);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (str == "")
                    {
                        str = table.Rows[i]["ProviderId"].ToString().Trim();
                    }
                    else
                    {
                        str = str + "," + table.Rows[i]["ProviderId"].ToString().Trim();
                    }
                }
            }
            return str;
        }

        public static Hashtable GetDistinctItem(string Field, string Table)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Format("select distinct {0} from {1}", Field, Table));
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString() != "")
                {
                    hashtable.Add(table.Rows[i][0].ToString(), table.Rows[i][0].ToString());
                }
            }
            return hashtable;
        }

        public static DataTable GetGrade(string itemClassID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_provider_AssessMarkLevel where ItemClassID=" + itemClassID);
        }

        public DataTable GetProvAssessFraction(string assessID, string providerId)
        {
            return publicDbOpClass.DataTableQuary("select a.ItemDetailID,a.AssessFraction,a.TransactState,a.TransactState,b.v_xm ,c.AssessItemName,c.AssessCriterion,a.AssessItem,c.ItemClassID from pm_provider_AssessItemDetail a left join pt_yhmc b on a.AssessPersonCode = b.v_yhdm  left join pm_provider_AssessItem c on a.AssessItemID = c.AssessItemID where a.AssessDetailID in  (select top 1 AssessDetailID from pm_provider_AssessDetail where AssessID = " + assessID + " and ProviderId = " + providerId + ")");
        }

        public DataTable GetProvAssessFraction(string AssessID, string ProviderId, string AssessPersonCode)
        {
            return publicDbOpClass.DataTableQuary("select a.ItemDetailID,a.AssessFraction,a.TransactState,a.TransactState,b.v_xm ,c.AssessItemName,c.AssessCriterion from pm_provider_AssessItemDetail a left join pt_yhmc b on a.AssessPersonCode = b.v_yhdm  left join pm_provider_AssessItem c on a.AssessItemID = c.AssessItemID where  a.AssessPersonCode ='" + AssessPersonCode.Trim() + "' and  a.AssessDetailID in  (select top 1 AssessDetailID from pm_provider_AssessDetail where AssessID = " + AssessID + " and ProviderId = " + ProviderId + ")");
        }

        public static int GetRowspan(string AssessItem, string ItemClassID)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(*) from pm_provider_AssessItem where AssessItem='" + AssessItem + "' and  ItemClassID=" + ItemClassID));
        }

        public static DataTable GetStandered(string itemClassID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_provider_AssessMarkLevel where ItemClassID=" + itemClassID);
        }

        public static DataTable GetTotalMarks(string AssessID, string ProviderId)
        {
            return publicDbOpClass.DataTableQuary("select dbo.f_Prj_ProvAssessScoring(" + AssessID + "," + ProviderId + ") as score");
        }

        public string GetTransactState(string AssessID)
        {
            return (string) publicDbOpClass.ExecuteScalar("select top 1 TransactState from pm_provider_AssessMain where AssessID=" + AssessID);
        }

        public bool SetAssessProvider(string AssessID, string ProviderIds)
        {
            string str = "";
            string[] strArray = ProviderIds.Split(new char[] { ',' });
            publicDbOpClass.NonQuerySqlString("delete pm_provider_AssessDetail where AssessID = " + AssessID);
            str = " begin";
            if (ProviderIds.Trim() != "")
            {
                int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_provider_AssessDetail", "AssessDetailID"));
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].Trim() != "0")
                    {
                        num++;
                        string str2 = str;
                        str = str2 + " insert into pm_provider_AssessDetail (AssessDetailID,AssessID,ProviderId) values (" + num.ToString() + "," + AssessID + "," + strArray[i] + ") ";
                    }
                }
            }
            return publicDbOpClass.NonQuerySqlString(str + " select getdate() end");
        }

        public static bool SetGrade(string itemClassID, string markLevel1, string markLevel2, string markLevel3, string markLevel4)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_provider_AssessMarkLevel (RecordID,ItemClassID,MarkLevel1,MarkLevel2,MarkLevel3,MarkLevel4) values (newid()," + itemClassID + "," + markLevel1 + "," + markLevel2 + "," + markLevel3 + "," + markLevel4 + ")");
        }

        public bool Transact(string AssessID)
        {
            string str = "";
            string str2 = "";
            str2 = publicDbOpClass.ExecuteScalar("select ItemClassID from pm_provider_AssessMain where AssessID =" + AssessID).ToString();
            DataTable table = publicDbOpClass.DataTableQuary("select AssessDetailID,ProviderId from pm_provider_AssessDetail where AssessID = " + AssessID);
            str = " begin  ";
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, "insert into pm_provider_AssessItemDetail (AssessDetailID,AssessItemID,AssessPersonCode,TransactState,AssessItem) select ", row["AssessDetailID"], ",AssessItemID,AssessPersonCode,'0',AssessItem  from pm_provider_AssessItem where IsValid ='1' and  ItemClassID=", str2 });
                }
            }
            return publicDbOpClass.NonQuerySqlString((str + "update pm_provider_AssessMain set TransactState = '1' where AssessID =" + AssessID) + "  end ");
        }

        public bool updAssessInfo(string AssessID, string AssessDate, string OriginateDate, string Organiger, string Remark, string UserCode, string RecordDate)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_provider_AssessMain set AssessDate='" + AssessDate + "',OriginateDate='" + OriginateDate + "',Organiger=" + Organiger + ",Remark=" + Remark + ",UserCode='" + UserCode + "',RecordDate='" + RecordDate + "' where AssessID=" + AssessID);
        }

        public bool updAssessItemInfo(string AssessItemID, string AssessItemName, string AssessCriterion, string AssessPersonCode, string UserCode, string RecordDate, string IsValid, string AssessItem)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_provider_AssessItem set  AssessItemName =" + AssessItemName + ", AssessCriterion =" + AssessCriterion + ", AssessPersonCode ='" + AssessPersonCode + "', UserCode ='" + UserCode + "', RecordDate ='" + RecordDate + "', IsValid ='" + IsValid + "', AssessItem='" + AssessItem + "' where AssessItemID =" + AssessItemID);
        }

        public static void UpdateAssessDetail(string assessID, string providerId, string score, string assessResult)
        {
            publicDbOpClass.NonQuerySqlString("update pm_provider_AssessDetail set Score=" + score + ",AssessResult='" + assessResult + "' where AssessID=" + assessID + " and ProviderId=" + providerId);
        }

        public static bool UpdGrade(string itemClassID, string markLevel1, string markLevel2, string markLevel3, string markLevel4)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_provider_AssessMarkLevel set MarkLevel1=" + markLevel1 + ",MarkLevel2=" + markLevel2 + ",MarkLevel3=" + markLevel3 + ",MarkLevel4=" + markLevel4 + " where ItemClassID=" + itemClassID);
        }
    }
}

