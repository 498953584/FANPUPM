namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ModuleAction
    {
        private static DataTable moduleDT = new DataTable();

        private static void AddChildNode(string ParentId, ModuleCollection SourceCollection, ModuleCollection TargetCollection)
        {
            ModuleCollection modules = new ModuleCollection();
            foreach (Module module in SourceCollection)
            {
                if (((module.ModuleCode.Length - 2) == ParentId.Length) && (module.ModuleCode.Substring(0, ParentId.Length) == ParentId))
                {
                    modules.Add(module);
                }
            }
            if (modules.Count != 0)
            {
                foreach (Module module2 in modules)
                {
                    TargetCollection.Add(module2);
                    AddChildNode(module2.ModuleCode, SourceCollection, TargetCollection);
                }
            }
        }

        private static Module DataTableConvertModule(DataRow dr)
        {
            Module module = new Module();
            int num = 2;
            module.ModuleName = dr["v_mkmc"].ToString();
            module.ModuleCode = dr["c_mkdm"].ToString();
            module.FilePath = dr["v_cdlj"].ToString();
            module.ImagePath = dr["v_img"].ToString();
            module.ChildNum = dr["i_childNum"].ToString();
            module.ID = dr["i_id"].ToString();
            module.SerialNo = dr["i_xh"].ToString();
            module.Level = module.ModuleCode.Length / num;
            module.BottomNode = IsBottomNode(moduleDT, dr);
            return module;
        }

        public static ModuleCollection GetOneLevel()
        {
            string sqlString = "select * from pt_mk where len(c_mkdm) = 2 order by i_xh,c_mkdm";
            ModuleCollection modules = new ModuleCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                foreach (DataRow row in table.Rows)
                {
                    modules.Add(DataTableConvertModule(row));
                }
            }
            return modules;
        }

        public static ModuleCollection GetOneLevelByID(string strModuleID)
        {
            string sqlString = string.Concat(new object[] { "select * from pt_mk where ( len(c_mkdm) = ", strModuleID.Length, " or len(c_mkdm) = 2 + ", strModuleID.Length.ToString(), ") and substring(c_mkdm,1,", strModuleID.Length, ") = '", strModuleID, "' order by c_mkdm,i_xh" });
            ModuleCollection modules = new ModuleCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                foreach (DataRow row in table.Rows)
                {
                    modules.Add(DataTableConvertModule(row));
                }
            }
            return modules;
        }

        public static ModuleCollection GetUserModule(string userCode, string ptver)
        {
            moduleDT = SystemModule.GetUserPurviewModules(userCode, ptver);
            ModuleCollection modules = new ModuleCollection();
            foreach (DataRow row in moduleDT.Rows)
            {
                modules.Add(DataTableConvertModule(row));
            }
            return modules;
        }

        private static bool IsBottomNode(DataTable dt, DataRow dr)
        {
            string strB = dr["c_mkdm"].ToString();
            string filterExpression = "len(c_mkdm) = len('" + strB + "') and c_mkdm like ''+ substring('" + strB + "',1,len('" + strB + "')-2)+'%'";
            DataRow[] rowArray = dt.Select(filterExpression);
            bool flag = true;
            foreach (DataRow row in rowArray)
            {
                if (int.Parse(row["i_xh"].ToString()) > int.Parse(dr["i_xh"].ToString()))
                {
                    return false;
                }
                if ((int.Parse(row["i_xh"].ToString()) == int.Parse(dr["i_xh"].ToString())) && (row["c_mkdm"].ToString().CompareTo(strB) > 0))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static ModuleCollection RenderTreeModuleListGather(string strUserID, string ptver)
        {
            ModuleCollection userModule = GetUserModule(strUserID, ptver);
            ModuleCollection targetCollection = new ModuleCollection();
            for (int i = 0; i < userModule.Count; i++)
            {
                if (userModule[i].ModuleCode.Length == 2)
                {
                    targetCollection.Add(userModule[i]);
                    AddChildNode(userModule[i].ModuleCode, userModule, targetCollection);
                }
            }
            return targetCollection;
        }
    }
}

