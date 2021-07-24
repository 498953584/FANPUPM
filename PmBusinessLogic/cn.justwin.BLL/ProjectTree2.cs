namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.EasyUI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;

    public static class ProjectTree2
    {
        public static void BindPrjYear(DropDownList drop, string userCode, IList<string> prjStatList)
        {
            new Project();
            IList<string> busiDataId = PrivHelper.GetBusiDataId("project", userCode);
            int startYear = GetStartYear(busiDataId, prjStatList);
            int endYear = GetEndYear(busiDataId, prjStatList);
            if (endYear < startYear)
            {
                endYear = startYear;
            }
            for (int i = startYear; i <= endYear; i++)
            {
                string text = i + "年";
                ListItem item = new ListItem(text, i.ToString());
                if (i == DateTime.Now.Year)
                {
                    item.Selected = true;
                }
                drop.Items.Add(item);
            }
        }

        private static int GetEndYear(IList<string> idList, IList<string> stateList)
        {
            int year = DateTime.Now.Year;
            try
            {
                string cmdText = string.Format("\r\n\t\t\t\t\tSELECT YEAR(MAX(ISNULL(EndDate, GETDATE()))) FROM (\r\n\t\t\t\t\t\tSELECT EndDate, PrjGuid, PrjState FROM PT_PrjInfo\r\n\t\t\t\t\t\tUNION \r\n\t\t\t\t\t\tSELECT EndDate, PrjGuid, PrjState FROM PT_PrjInfo_ZTB\r\n\t\t\t\t\t) AS P\r\n\t\t\t\t\tWHERE PrjGuid IN ({0}) AND PrjState IN ({1})\r\n\t\t\t\t", DBHelper.GetInParameterSql(idList), DBHelper.GetInParameterSql(stateList));
                year = DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText));
            }
            catch
            {
            }
            return year;
        }

        public static IList<Project> GetOnelevelPrj(int year, string userCode, string showType, IList<string> stateList, string pcode, string pname)
        {
            IList<string> busiDataId = PrivHelper.GetBusiDataId("project", userCode);
            Project project = new Project();
            IList<Project> list2 = new List<Project>();
            if (showType == "0")
            {
                return project.GetOnelevelByParent(year, busiDataId, stateList, pcode, pname);
            }
            return project.GetOnelevelByState(year, busiDataId, stateList, pcode, pname);
        }

        private static int GetStartYear(IList<string> idList, IList<string> stateList)
        {
            int year = DateTime.Now.Year;
            try
            {
                string cmdText = string.Format("\r\n\t\t\t\t\tSELECT YEAR(MIN(StartDate)) FROM (\r\n\t\t\t\t\t\tSELECT StartDate, PrjGuid, PrjState FROM PT_PrjInfo\r\n\t\t\t\t\t\tUNION \r\n\t\t\t\t\t\tSELECT StartDate, PrjGuid, PrjState FROM PT_PrjInfo_ZTB\r\n\t\t\t\t\t) AS P\r\n\t\t\t\t\tWHERE PrjGuid IN ({0}) AND PrjState IN ({1})\r\n\t\t\t\t", DBHelper.GetInParameterSql(idList), DBHelper.GetInParameterSql(stateList));
                year = DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText));
            }
            catch
            {
            }
            return year;
        }

        public static IList<Project> GetSublevelByParent(int year, string userCode, string prjId, IList<string> stateList)
        {
            Project project = new Project();
            IList<string> busiDataId = PrivHelper.GetBusiDataId("project", userCode);
            return project.GetSublevelByParent(year, busiDataId, stateList, prjId);
        }

        public static IList<Project> GetSublevelByState(int year, string userCode, string state, string pcode, string pname)
        {
            Project project = new Project();
            IList<string> busiDataId = PrivHelper.GetBusiDataId("project", userCode);
            return project.GetSublevelByState(year, busiDataId, state, pcode, pname);
        }
    }
}

