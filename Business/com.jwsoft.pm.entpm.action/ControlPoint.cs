namespace com.jwsoft.pm.entpm.action
{
    using BonZe.DataAccess;
    using com.jwsoft.common.data;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    public class ControlPoint
    {
        private DataRow _InfoRow;
        private int _MainID;

        public ControlPoint(int MainID)
        {
            this._MainID = MainID;
        }

        public void ChangeValidOption(int OptionID)
        {
            string format = "if not exists(select 1 from prj_prsentiment_options where MainID={0} and OptionID={1} and IsValid=1) begin\tupdate prj_prsentiment_options set IsValid=0 where MainID={0} and IsValid=1  update prj_prsentiment_options set IsValid=1 where OptionID={1} end";
            publicDbOpClass.NonQuerySqlString(string.Format(format, this._MainID, OptionID));
        }

        private DataRow GetInfoRow()
        {
            DataTable table = publicDbOpClass.DataTableQuary("select MainID,Code,Title,Depict,ToPeople,[ToPeopleNames]=dbo.prj_f_getUsersName(ToPeople),[ValidTimeLong]=isnull(ValidTimeLong,3)  from prj_presentiment a where MainID=" + this._MainID);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0];
            }
            return null;
        }

        public static decimal GetOneResourceBudgetPrice(string ScheduleCode, string PrjCode, string ResourceCode, int ResourceType)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(string.Format("select isnull(Price,0) from Prj_PeopleDatumEngine where PrjCode='{0}' and", PrjCode) + string.Format(" ScheduleCode='{0}' and ResourceOrCostCode='{1}' and SmallFlags={2} and BigFlags=2", ScheduleCode, ResourceCode, ResourceType));
            if (obj2 != null)
            {
                return decimal.Parse(obj2.ToString());
            }
            return 0M;
        }

        public static decimal GetOneResourceBudgetVolume(string ScheduleCode, string PrjCode, string ResourceCode, int ResourceType)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(string.Format("select isnull(Amount,0) from Prj_PeopleDatumEngine where PrjCode='{0}' and", PrjCode) + string.Format(" ScheduleCode='{0}' and ResourceOrCostCode='{1}' and SmallFlags={2} and BigFlags=2", ScheduleCode, ResourceCode, ResourceType));
            if (obj2 != null)
            {
                return decimal.Parse(obj2.ToString());
            }
            return 0M;
        }

        public DataTable GetOptions()
        {
            return publicDbOpClass.DataTableQuary("select OptionID,OptionText,OptionCode,MainID,IsValid from prj_prsentiment_options where MainID=" + this._MainID + " order by OptionCode asc");
        }

        public static DataTable GetPresentiments(int pgSize, int pgIndex, out int pgCount)
        {
            pgCount = 0;
            string strSql = "select MainID,Code,Title,Depict,ToPeople from prj_presentiment order by Code asc";
            return publicDbOpClass.GetRecordFromPage(ref pgCount, strSql, "MainID", pgSize, pgIndex);
        }

        public static decimal GetSchCompletionrate(string ScheduleCode, string PrjCode)
        {
            decimal num = 0M;
            object obj2 = publicDbOpClass.ExecuteScalar(string.Format("select isnull(Quantity,0) from Prj_ScheduleInfo where isValid=1 and prjcode='{0}' and ScheduleCode='{1}'", PrjCode, ScheduleCode));
            if (obj2 != null)
            {
                string sqlString = string.Format("select isnull(sum(TimberingValue),0) from Prj_V_ProdValue where prjcode='{0}' and ScheduleCode='{1}'", PrjCode, ScheduleCode);
                if (decimal.Parse(obj2.ToString()) > 0M)
                {
                    num = decimal.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString()) / decimal.Parse(obj2.ToString());
                }
            }
            return num;
        }

        public void Insert(string[] OptionCodes, string[] OptionTexts, int SelectedIndex, params SqlParameter[] parms)
        {
            SqlParameter[] commandParameters = new SqlParameter[OptionTexts.Length + parms.Length];
            string str2 = "";
            string str = "insert into prj_presentiment(Code,Title,Depict,ToPeople,ValidTimeLong,RiskID) values(@Code,@Title,@Depict,@ToPeople,@ValidTimeLong,@RiskID)";
            str = str + "declare @pk int ; set @pk = @@identity ;";
            for (int i = 0; i < OptionTexts.Length; i++)
            {
                int num2 = 0;
                if (i == SelectedIndex)
                {
                    num2 = 1;
                }
                object obj2 = str2;
                str2 = string.Concat(new object[] { obj2, " insert into prj_prsentiment_options(OptionText,OptionCode,MainID,IsValid) values(@OptionText", i, ",'", OptionCodes[i], "',@pk,", num2, ")" });
                commandParameters[i] = new SqlParameter("@OptionText" + i, OptionTexts[i]);
            }
            for (int j = 0; j < parms.Length; j++)
            {
                commandParameters[OptionTexts.Length + j] = parms[j];
            }
            SqlHelper.ExecuteNonQuery(Conn.aptSqlConn(), CommandType.Text, str + str2, commandParameters);
        }

        public static bool IsCodeUsed(string code, int ExceptID)
        {
            return (bool) publicDbOpClass.ExecuteScalar(string.Format("if exists(select 1 from prj_presentiment where Code='{0}' and MainID<>{1}) select cast(1 as bit) else select cast(0 as bit)", code, ExceptID));
        }

        public void Update(string[] OptionCodes, string[] OptionTexts, int SelectedIndex, params SqlParameter[] parms)
        {
            SqlParameter[] commandParameters = new SqlParameter[OptionTexts.Length + parms.Length];
            string str2 = "";
            string str = "update prj_presentiment set Code=@Code,Title=@Title,Depict=@Depict,ToPeople=@ToPeople,ValidTimeLong=@ValidTimeLong,RiskID=@RiskID where MainID=" + this._MainID;
            str2 = " delete prj_prsentiment_options where MainID=" + this._MainID;
            for (int i = 0; i < OptionTexts.Length; i++)
            {
                int num2 = 0;
                if (i == SelectedIndex)
                {
                    num2 = 1;
                }
                object obj2 = str2;
                str2 = string.Concat(new object[] { obj2, " insert into prj_prsentiment_options(OptionText,OptionCode,MainID,IsValid) values(@OptionText", i, ",'", OptionCodes[i], "',", this._MainID, ",", num2, ")" });
                commandParameters[i] = new SqlParameter("@OptionText" + i, OptionTexts[i]);
            }
            for (int j = 0; j < parms.Length; j++)
            {
                commandParameters[OptionTexts.Length + j] = parms[j];
            }
            SqlHelper.ExecuteNonQuery(Conn.aptSqlConn(), CommandType.Text, str + str2, commandParameters);
        }

        public DataRow InfoRow
        {
            get
            {
                if (this._InfoRow == null)
                {
                    this._InfoRow = this.GetInfoRow();
                }
                return this._InfoRow;
            }
        }
    }
}

