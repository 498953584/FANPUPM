namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using cn.justwin.PrjManager;
    using cn.justwin.Tender;
    using System;
    using System.Data;
    using System.Text;

    public class BasicProjectCode : IProjectCode
    {
        private string CreateCode(int count)
        {
            int num = count + 1;
            return (DateTime.Now.ToString("yyyyMMdd") + num.ToString().PadLeft(3, '0'));
        }

        public string CreateProjectCode()
        {
            int projectCountToday = this.GetProjectCountToday();
            string prjCode = "P" + this.CreateCode(projectCountToday);
            int num2 = 0;
            while (ProjectInfo.IsSameCode(prjCode))
            {
                num2++;
                prjCode = "P" + this.CreateCode(projectCountToday + num2);
            }
            return prjCode;
        }

        public string CreateTenderCode()
        {
            int tenderCountToday = this.GetTenderCountToday();
            string prjCode = "T" + this.CreateCode(tenderCountToday);
            int num2 = 0;
            while (TenderInfo.IsSameCode(prjCode))
            {
                num2++;
                prjCode = "T" + this.CreateCode(tenderCountToday + num2);
            }
            return prjCode;
        }

        private int GetProjectCountToday()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM PT_PrjInfo \n");
            builder.Append("WHERE PrjGuid IN( \n");
            builder.Append("\t\tSELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail \n");
            builder.Append("\t\t\tWHERE  CONVERT(varchar(8), InputDate, 112) = CONVERT(varchar(8), GETDATE(), 112) \n");
            builder.Append(") \n");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()));
        }

        private int GetTenderCountToday()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM PT_PrjInfo_ZTB \n");
            builder.Append("WHERE PrjGuid IN( \n");
            builder.Append("\t\tSELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail \n");
            builder.Append("\t\t\tWHERE  CONVERT(varchar(8), InputDate, 112) = CONVERT(varchar(8), GETDATE(), 112) \n");
            builder.Append(") \n");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()));
        }
    }
}

