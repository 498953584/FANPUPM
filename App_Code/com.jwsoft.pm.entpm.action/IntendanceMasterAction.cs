namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class IntendanceMasterAction
    {
        public int Add(IntendanceMaster intendanceMaster, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Begin ");
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            Guid intendanceGuid = intendanceMaster.IntendanceGuid;
            builder2.Append("IntendanceGuid,");
            builder3.Append("'" + intendanceMaster.IntendanceGuid + "',");
            Guid prjGuid = intendanceMaster.PrjGuid;
            builder2.Append("PrjGuid,");
            builder3.Append("'" + intendanceMaster.PrjGuid + "',");
            if (intendanceMaster.QuestionTitle != null)
            {
                builder2.Append("QuestionTitle,");
                builder3.Append("'" + intendanceMaster.QuestionTitle + "',");
            }
            DateTime bookInDate = intendanceMaster.BookInDate;
            builder2.Append("BookInDate,");
            builder3.Append("'" + intendanceMaster.BookInDate + "',");
            int questionTypeId = intendanceMaster.QuestionTypeId;
            builder2.Append("QuestionTypeId,");
            builder3.Append("'" + intendanceMaster.QuestionTypeId + "',");
            if (intendanceMaster.SettleYhdm != null)
            {
                builder2.Append("SettleYhdm,");
                builder3.Append("'" + intendanceMaster.SettleYhdm + "',");
            }
            if (intendanceMaster.SettleToPerson != null)
            {
                builder2.Append("SettleToPerson,");
                builder3.Append("'" + intendanceMaster.SettleToPerson + "',");
            }
            if (intendanceMaster.OpYhdm != null)
            {
                builder2.Append("OpYhdm,");
                builder3.Append("'" + intendanceMaster.OpYhdm + "',");
            }
            int settleState = intendanceMaster.SettleState;
            builder2.Append("SettleState,");
            builder3.Append(intendanceMaster.SettleState + ",");
            int questionTag = intendanceMaster.QuestionTag;
            builder2.Append("QuestionTag,");
            builder3.Append("'" + intendanceMaster.QuestionTag + "',");
            builder.Append("insert into OPM_EPCM_IntendanceMaster(");
            builder.Append(builder2.ToString().Remove(builder2.Length - 1));
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(builder3.ToString().Remove(builder3.Length - 1));
            builder.Append(")");
            builder.Append(";select @@IDENTITY ");
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int AddDoList(string typeCode, string TargetNoteID, string Operater)
        {
            return publicDbOpClass.ExecSqlString("insert into OPM_PT_ToDoList([TypeCode],[TargetNoteID],[Operater]) values('" + typeCode + "','" + TargetNoteID + "','" + Operater + "')");
        }

        public int Del(string IntendanceGuid)
        {
            return publicDbOpClass.ExecSqlString(("delete OPM_EPCM_IntendanceMaster  where  IntendanceGuid='" + IntendanceGuid + "'").ToString());
        }

        public int DelDoList(string typeCode, string TargetNoteID, string Operater)
        {
            return publicDbOpClass.ExecSqlString(" DELETE FROM OPM_PT_ToDoList where typeCode='" + typeCode + "' and TargetNoteID='" + TargetNoteID + "' and Operater='" + Operater + "'");
        }

        public IntendanceMaster GetIntendanceMaster(Guid IntendanceGuid)
        {
            string str = "select * from OPM_EPCM_IntendanceMaster  where  IntendanceGuid='" + IntendanceGuid + "'";
            IntendanceMaster master = new IntendanceMaster();
            DataSet set = publicDbOpClass.DataSetQuary(str.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["IntendanceGuid"].ToString() != "")
            {
                master.IntendanceGuid = new Guid(set.Tables[0].Rows[0]["IntendanceGuid"].ToString());
            }
            if (set.Tables[0].Rows[0]["prjGuid"].ToString() != "")
            {
                master.PrjGuid = new Guid(set.Tables[0].Rows[0]["prjGuid"].ToString());
            }
            if (set.Tables[0].Rows[0]["QuestionTitle"].ToString() != "")
            {
                master.QuestionTitle = set.Tables[0].Rows[0]["QuestionTitle"].ToString();
            }
            if (set.Tables[0].Rows[0]["BookInDate"].ToString() != "")
            {
                master.BookInDate = Convert.ToDateTime(set.Tables[0].Rows[0]["BookInDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["SettleState"].ToString() != "")
            {
                master.SettleState = int.Parse(set.Tables[0].Rows[0]["SettleState"].ToString());
            }
            if (set.Tables[0].Rows[0]["QuestionTypeId"].ToString() != "")
            {
                master.QuestionTypeId = int.Parse(set.Tables[0].Rows[0]["QuestionTypeId"].ToString());
            }
            if (set.Tables[0].Rows[0]["SettleYhdm"].ToString() != "")
            {
                master.SettleYhdm = set.Tables[0].Rows[0]["SettleYhdm"].ToString();
            }
            if (set.Tables[0].Rows[0]["SettleToPerson"].ToString() != "")
            {
                master.SettleToPerson = set.Tables[0].Rows[0]["SettleToPerson"].ToString();
            }
            if (set.Tables[0].Rows[0]["OpYhdm"].ToString() != "")
            {
                master.OpYhdm = set.Tables[0].Rows[0]["OpYhdm"].ToString();
            }
            if (set.Tables[0].Rows[0]["QuestionTag"].ToString() != "")
            {
                master.QuestionTag = int.Parse(set.Tables[0].Rows[0]["QuestionTag"].ToString());
            }
            return master;
        }

        public DataTable GetList(Guid prjGuid, string strWhere)
        {
            string str = "SELECT prjGuid FROM PT_PrjInfo ppi WHERE ppi.TypeCode LIKE(SELECT typeCode FROM PT_PrjInfo ppi WHERE ppi.PrjGuid ='" + prjGuid + "')+'%'";
            string sqlString = "select a.*,b.v_xm,c.PrjName,c.PrjGuid ,d.CodeName from OPM_EPCM_IntendanceMaster  as a inner join PT_yhmc as b on a.OpYhdm=b.v_yhdm inner join pt_prjinfo as c on a.PrjGuid=c.PrjGuid INNER JOIN dbo.XPM_Basic_CodeList as d ON a.QuestionTypeId = d.CodeID ";
            if (prjGuid == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                sqlString = sqlString + " \r\n\t\t\t\twhere  d.TypeID = (\r\n\t\t\t\tSELECT TypeId FROM dbo.XPM_Basic_CodeType\r\n\t\t\t\twhere signCode='ProblemSupervise') " + strWhere + "  order by a.BookInDate desc";
            }
            else
            {
                sqlString = (sqlString + " where  a.prjGuid in (" + str + ") and d.TypeID = (SELECT TypeId FROM dbo.XPM_Basic_CodeType") + " where signCode='ProblemSupervise') " + strWhere + "  order by a.BookInDate desc";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetPrjChildNum(Guid prjGuid)
        {
            return publicDbOpClass.DataTableQuary("select i_childNum from pt_prjInfo where prjGuid='" + prjGuid + "'");
        }

        public string GetQuestionNameById(int typeId)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select CodeName from XPM_Basic_CodeList where typeId =(select TypeId from XPM_Basic_CodeType WHERE SignCode='ProblemSupervise') and isValid =1 and CodeId=" + typeId);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        public DataTable GetTypeList()
        {
            string sqlString = "select codeId,codeName from XPM_Basic_CodeList where typeId =(select TypeId from XPM_Basic_CodeType WHERE SignCode='ProblemSupervise') and isvalid='true'";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int Update(IntendanceMaster intendanceMaster)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Begin ");
            builder.Append("update OPM_EPCM_IntendanceMaster  set ");
            Guid intendanceGuid = intendanceMaster.IntendanceGuid;
            builder.Append("IntendanceGuid='" + intendanceMaster.IntendanceGuid + "',");
            Guid prjGuid = intendanceMaster.PrjGuid;
            builder.Append("PrjGuid='" + intendanceMaster.PrjGuid + "',");
            if (intendanceMaster.QuestionTitle != null)
            {
                builder.Append("QuestionTitle='" + intendanceMaster.QuestionTitle + "',");
            }
            int questionTypeId = intendanceMaster.QuestionTypeId;
            builder.Append("QuestionTypeId='" + intendanceMaster.QuestionTypeId + "',");
            if (intendanceMaster.SettleYhdm != null)
            {
                builder.Append("SettleYhdm='" + intendanceMaster.SettleYhdm + "',");
            }
            if (intendanceMaster.SettleToPerson != null)
            {
                builder.Append("SettleToPerson='" + intendanceMaster.SettleToPerson + "',");
            }
            DateTime bookInDate = intendanceMaster.BookInDate;
            builder.Append("BookInDate='" + intendanceMaster.BookInDate + "',");
            int settleState = intendanceMaster.SettleState;
            builder.Append("SettleState=" + intendanceMaster.SettleState);
            builder.Append(" where IntendanceGuid='" + intendanceMaster.IntendanceGuid + "'");
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(string IntendanceGuid, int state)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { " update OPM_EPCM_IntendanceMaster  set SettleState=", state, ",QuestionTag=1 where IntendanceGuid='", IntendanceGuid, "'" }));
        }

        public int Update(string IntendanceGuid, int state, string toperson)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { " update OPM_EPCM_IntendanceMaster  set SettleState=", state, ",QuestionTag=1,SettleToPerson='", toperson, "' where IntendanceGuid='", IntendanceGuid, "'" }));
        }
    }
}

