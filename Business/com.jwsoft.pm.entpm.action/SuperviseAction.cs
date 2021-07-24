namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class SuperviseAction
    {
        public static bool Delete(string pk)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Prj_ItemSupervise where ID='" + pk + "'");
        }

        public static DataTable GetSuperviseCollections(string strwhere)
        {
            return publicDbOpClass.GetPageData(strwhere, "Prj_V_ItemSupervise", "id desc");
        }

        public static int GetSuperviseCount(string strwhere)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_ItemSupervise where " + strwhere);
        }

        public static SuperviseInfo GetSuperviseInfo(string ID)
        {
            SuperviseInfo info = new SuperviseInfo();
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_V_ItemSupervise where ID=" + ID);
            info.StandNapeUnit = table.Rows[0]["StandNapeUnit"].ToString();
            if (table.Rows[0]["StandNapeDate"].ToString() != "")
            {
                info.StandNapeDate = DateTime.Parse(table.Rows[0]["StandNapeDate"].ToString());
            }
            info.StandNapeName = table.Rows[0]["StandNapeName"].ToString();
            info.StandItemRecord = table.Rows[0]["StandItemRecord"].ToString();
            info.SuperviseAdvice = table.Rows[0]["SuperviseAdvice"].ToString();
            info.CompleteApply = table.Rows[0]["CompleteApply"].ToString();
            if (table.Rows[0]["KnotItemDate"].ToString() != "")
            {
                info.KnotItemDate = DateTime.Parse(table.Rows[0]["KnotItemDate"].ToString());
            }
            info.SuperviseEffciency = table.Rows[0]["SuperviseEffciency"].ToString();
            info.ApprovePerson = table.Rows[0]["ApprovePerson"].ToString();
            info.ApproveResult = int.Parse(table.Rows[0]["ApproveResult"].ToString());
            if (table.Rows[0]["ApproveDate"].ToString() != "")
            {
                info.ApproveDate = DateTime.Parse(table.Rows[0]["ApproveDate"].ToString());
            }
            info.ApproveIdea = table.Rows[0]["ApproveIdea"].ToString();
            info.PrjName = table.Rows[0]["PrjName"].ToString();
            return info;
        }

        public static bool SuperviseInfoOp(SuperviseInfo objinfo, string OpType)
        {
            string sqlString = "";
            if (OpType == "Insert")
            {
                sqlString = string.Concat(new object[] { 
                    "Insert into Prj_ItemSupervise(PrjCode,StandNapeUnit,StandNapeDate,StandNapeName,StandItemRecord,SuperviseAdvice,CompleteApply,KnotItemDate,SuperviseEffciency,ApproveResult) values('", objinfo.PrjCode, "','", objinfo.StandNapeUnit, "','", objinfo.StandNapeDate, "','", objinfo.StandNapeName, "','", objinfo.StandItemRecord, "','", objinfo.SuperviseAdvice, "','", objinfo.CompleteApply, "','", objinfo.KnotItemDate, 
                    "','", objinfo.SuperviseEffciency, "',", objinfo.ApproveResult.ToString(), ")"
                 });
            }
            else if (OpType == "Update")
            {
                sqlString = string.Concat(new object[] { 
                    "update Prj_ItemSupervise set StandNapeUnit='", objinfo.StandNapeUnit, "',StandNapeDate='", objinfo.StandNapeDate, "',StandNapeName='", objinfo.StandNapeName, "',StandItemRecord='", objinfo.StandItemRecord, "',SuperviseAdvice='", objinfo.SuperviseAdvice, "',CompleteApply='", objinfo.CompleteApply, "',KnotItemDate='", objinfo.KnotItemDate, "',SuperviseEffciency='", objinfo.SuperviseEffciency, 
                    "' where ID=", objinfo.ID
                 });
            }
            else if (OpType == "Sp")
            {
                sqlString = string.Concat(new object[] { "update Prj_ItemSupervise set ApprovePerson='", objinfo.ApprovePerson, "',ApproveResult=", objinfo.ApproveResult, ",ApproveDate='", objinfo.ApproveDate, "',ApproveIdea='", objinfo.ApproveIdea, "' where ID=", objinfo.ID });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

