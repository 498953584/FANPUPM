namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ItemProgAction
    {
        public static bool ActionItemProg(int ItemId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("update Prj_ItemProg set IsAction=1 where ID={0}", ItemId));
        }

        public static bool CancelActionItemProg(int ItemId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("update Prj_ItemProg set IsAction=0 where ID={0}", ItemId));
        }

        public static bool Delete(string pk)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Prj_ItemProg where ID in(" + pk + ")");
        }

        public static string GetIdByUID(string uid)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("SELECT ID FROM Prj_ItemProg WHERE UID = '" + uid + "'");
            string str2 = string.Empty;
            if (obj2 != DBNull.Value)
            {
                str2 = obj2.ToString();
            }
            return str2;
        }

        public static DataTable GetItemProgCollections(string strwhere)
        {
            return publicDbOpClass.GetPageData(strwhere, "Prj_V_ItemProg", "id desc");
        }

        public static int GetItemProgCount(string strwhere)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_ItemProg where " + strwhere);
        }

        public static ItemProgInfo GetItemProgInfo(string ID)
        {
            ItemProgInfo info = new ItemProgInfo();
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_ItemProg where ID=" + ID);
            if (table.Rows[0]["ApproveDate"].ToString() != "")
            {
                info.ApproveDate = DateTime.Parse(table.Rows[0]["ApproveDate"].ToString());
            }
            info.ApproveIdea = table.Rows[0]["ApproveIdea"].ToString();
            info.ApprovePerson = table.Rows[0]["ApprovePerson"].ToString();
            if (table.Rows[0]["ApproveResult"].ToString() != "")
            {
                info.ApproveResult = int.Parse(table.Rows[0]["ApproveResult"].ToString());
            }
            info.ByProgObject = table.Rows[0]["ByProgObject"].ToString();
            if (table.Rows[0]["ID"].ToString() != "")
            {
                info.ID = int.Parse(table.Rows[0]["ID"].ToString());
            }
            info.PrjCode = table.Rows[0]["PrjCode"].ToString();
            info.ProgCause = table.Rows[0]["ProgCause"].ToString();
            info.ProgDate = DateTime.Parse(table.Rows[0]["ProgDate"].ToString());
            info.ProgMoney = decimal.Parse(table.Rows[0]["ProgMoney"].ToString());
            info.ProgGist = table.Rows[0]["ProgGist"].ToString();
            info.ProgBurstunit = table.Rows[0]["ProgBurstunit"].ToString();
            info.ProgSortCode = int.Parse(table.Rows[0]["ProgSortCode"].ToString());
            info.ProgUnit = table.Rows[0]["ProgUnit"].ToString();
            info.Remark = table.Rows[0]["Remark"].ToString();
            info.Principal = table.Rows[0]["Principal"].ToString();
            return info;
        }

        public static bool ItemProgInfoOp(ItemProgInfo objinfo, string OpType)
        {
            string sqlString = "";
            if (OpType == "Insert")
            {
                sqlString = string.Concat(new object[] { 
                    "Insert into Prj_ItemProg(PrjCode,ProgUnit,ByProgObject,ProgGist,ProgCause,ProgMoney,ProgSortCode,ProgDate,Remark,ProgBurstunit,ProgSign,Principal,ApproveResult) values('", objinfo.PrjCode, "','", objinfo.ProgUnit, "','", objinfo.ByProgObject, "','", objinfo.ProgGist, "','", objinfo.ProgCause, "',", objinfo.ProgMoney, ",", objinfo.ProgSortCode, ",'", objinfo.ProgDate, 
                    "','", objinfo.Remark, "','", objinfo.ProgBurstunit, "',", objinfo.ProgSign, ",'", objinfo.Principal, "',", objinfo.ApproveResult, ")"
                 });
            }
            else if (OpType == "Update")
            {
                sqlString = string.Concat(new object[] { 
                    "update Prj_ItemProg set ProgUnit='", objinfo.ProgUnit, "',ByProgObject='", objinfo.ByProgObject, "',ProgGist='", objinfo.ProgGist, "',ProgCause='", objinfo.ProgCause, "',ProgMoney=", objinfo.ProgMoney, ",ProgSortCode=", objinfo.ProgSortCode, ",ProgDate='", objinfo.ProgDate, "',Remark='", objinfo.Remark, 
                    "',ProgBurstunit='", objinfo.ProgBurstunit, "',Principal='", objinfo.Principal, "' where ID=", objinfo.ID
                 });
            }
            else if (OpType == "Sp")
            {
                sqlString = string.Concat(new object[] { "update Prj_ItemProg set ApprovePerson='", objinfo.ApprovePerson, "',ApproveResult=", objinfo.ApproveResult, ",ApproveDate='", objinfo.ApproveDate, "',ApproveIdea='", objinfo.ApproveIdea, "' where ID=", objinfo.ID });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

