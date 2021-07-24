namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class ProjectForepartAction
    {
        public int AddProjectForepart(ProjectForepart pf)
        {
            string str = "";
            object obj2 = str + "insert into EPM_Prj_Forepart(ProjectCode,ProjectType,ProjectManualCode,ProjectName,OwnerCode,Contact,Locus,ProjectIntro,MainWork,FundQuarry,RekonPrice,Remark,State,AddDate,PrjState)";
            string str2 = string.Concat(new object[] { obj2, " values('", pf.ProjectCode, "',", pf.ProjectType.ToString() });
            string str3 = str2 + ",'" + pf.ProjectManualCode + "','" + pf.ProjectName + "'," + pf.OwnerCode.ToString();
            object obj3 = str3 + ",'" + pf.Contact + "','" + pf.Locus + "','" + pf.ProjectIntro + "','" + pf.MainWork + "'";
            object obj4 = string.Concat(new object[] { obj3, ",'", pf.FundQuarry, "',", pf.RekonPrice.ToString(), ",'", pf.Remark, "',", Convert.ToInt32(pf.State) });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, ",'", pf.AddDate.ToString(), "','", pf.Prjtate, "')" }));
        }

        public int DelProjectForepart(Guid projectCode)
        {
            return publicDbOpClass.ExecSqlString("delete from EPM_Prj_Forepart where ProjectCode = '" + projectCode.ToString() + "' ");
        }

        public int GetMarketInfoCount(string strWhere)
        {
            return publicDbOpClass.GetRecordCount("EPM_V_Bid_MarketSearch", strWhere);
        }

        public int GetMarketInfoCount(string type, string strWhere)
        {
            int recordCount = 0;
            if (type == "InviteBid")
            {
                recordCount = publicDbOpClass.GetRecordCount("EPM_V_Bid_InviteBidMarket", strWhere);
            }
            if (type == "BingoBid")
            {
                recordCount = publicDbOpClass.GetRecordCount("EPM_V_Bid_OpenBidMarket", strWhere);
            }
            if (type == "BuildProject")
            {
                recordCount = publicDbOpClass.GetRecordCount("EPM_V_Bid_ProjectInfoMarket", strWhere);
            }
            return recordCount;
        }

        public ArrayList GetMarketInfoList(int intPageSize, int intCurentPage, string strWhere)
        {
            ArrayList list = new ArrayList();
            using (DataTable table = publicDbOpClass.GetRecordFromPage("EPM_V_Bid_MarketSearch", "AddDate", intPageSize, intCurentPage, 1, strWhere))
            {
                if (table.Rows.Count <= 0)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.GetProjectForepartFromDataRow(table.Rows[i]));
                }
            }
            return list;
        }

        public ArrayList GetMarketInfoList(int intPageSize, int intCurentPage, string strWhere, string type)
        {
            ArrayList list = new ArrayList();
            if (type == "InviteBid")
            {
                using (DataTable table = publicDbOpClass.GetRecordFromPage("EPM_V_Bid_InviteBidMarket", "AddDate", intPageSize, intCurentPage, 0, strWhere))
                {
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            list.Add(this.GetProjectForepartFromDataRow(table.Rows[i]));
                        }
                    }
                }
            }
            if (type == "BingoBid")
            {
                using (DataTable table2 = publicDbOpClass.GetRecordFromPage("EPM_V_Bid_OpenBidMarket", "AddDate", intPageSize, intCurentPage, 0, strWhere))
                {
                    if (table2.Rows.Count > 0)
                    {
                        for (int j = 0; j < table2.Rows.Count; j++)
                        {
                            list.Add(this.GetProjectForepartFromDataRow(table2.Rows[j]));
                        }
                    }
                }
            }
            if (type == "BuildProject")
            {
                using (DataTable table3 = publicDbOpClass.GetRecordFromPage("EPM_V_Bid_ProjectInfoMarket", "AddDate", intPageSize, intCurentPage, 0, strWhere))
                {
                    if (table3.Rows.Count <= 0)
                    {
                        return list;
                    }
                    for (int k = 0; k < table3.Rows.Count; k++)
                    {
                        list.Add(this.GetProjectForepartFromDataRow(table3.Rows[k]));
                    }
                }
            }
            return list;
        }

        private ProjectForepartExtend GetProjectForepartExtendFromDataRow(DataRow dr)
        {
            return new ProjectForepartExtend { ProjectCode = new Guid(dr["ProjectCode"].ToString()), ProjectType = (int) dr["ProjectType"], ProjectManualCode = dr["ProjectManualCode"].ToString(), ProjectName = dr["ProjectName"].ToString(), OwnerCode = (int) dr["OwnerCode"], Contact = dr["Contact"].ToString(), Locus = dr["Locus"].ToString(), ProjectIntro = dr["ProjectIntro"].ToString(), MainWork = dr["MainWork"].ToString(), FundQuarry = dr["FundQuarry"].ToString(), Remark = dr["Remark"].ToString(), State = (ProjectState) Enum.Parse(typeof(ProjectState), dr["State"].ToString()), RekonPrice = (decimal) dr["RekonPrice"], AddDate = (DateTime) dr["AddDate"], InviteBidCode = new Guid(dr["InviteBidCode"].ToString()) };
        }

        private ProjectForepart GetProjectForepartFromDataRow(DataRow dr)
        {
            ProjectForepart forepart = new ProjectForepart {
                ProjectCode = new Guid(dr["ProjectCode"].ToString()),
                ProjectType = (int) dr["ProjectType"],
                ProjectManualCode = dr["ProjectManualCode"].ToString(),
                ProjectName = dr["ProjectName"].ToString(),
                OwnerCode = (int) dr["OwnerCode"],
                Contact = dr["Contact"].ToString(),
                Locus = dr["Locus"].ToString(),
                ProjectIntro = dr["ProjectIntro"].ToString(),
                MainWork = dr["MainWork"].ToString(),
                FundQuarry = dr["FundQuarry"].ToString(),
                Remark = dr["Remark"].ToString(),
                State = (ProjectState) Enum.Parse(typeof(ProjectState), dr["State"].ToString()),
                RekonPrice = (decimal) dr["RekonPrice"],
                AddDate = (DateTime) dr["AddDate"]
            };
            try
            {
                forepart.PrjFollow = dr["PrjFollow"].ToString();
            }
            catch
            {
            }
            return forepart;
        }

        public bool IsAllowDelete(Guid projectCode)
        {
            string str = "";
            return (((int) publicDbOpClass.ExecuteScalar(str + " select Count(1) from EPM_Prj_Forepart where (ProjectCode = '" + projectCode.ToString() + "')and(state=1)")) == 0);
        }

        public ProjectForepartCollection QueryAllProject()
        {
            ProjectForepartCollection foreparts = new ProjectForepartCollection();
            string sqlString = "select * from EPM_Prj_Forepart";
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                if (table.Rows.Count <= 0)
                {
                    return foreparts;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    foreparts.Add(this.GetProjectForepartFromDataRow(table.Rows[i]));
                }
            }
            return foreparts;
        }

        public ProjectForepart QuerySingleProjectForepart(Guid projectCode)
        {
            ProjectForepart projectForepartFromDataRow = null;
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Prj_Forepart where ProjectCode = '" + projectCode.ToString() + "'"))
            {
                if (table.Rows.Count > 0)
                {
                    projectForepartFromDataRow = this.GetProjectForepartFromDataRow(table.Rows[0]);
                }
            }
            return projectForepartFromDataRow;
        }

        public int UpdProjectForepart(ProjectForepart pf, string type)
        {
            string str = "";
            str = ((((((str + " update EPM_Prj_ForePart set ") + " ProjectType = " + pf.ProjectType.ToString() + ",") + " ProjectManualCode = '" + pf.ProjectManualCode + "',") + " ProjectName = '" + pf.ProjectName + "',") + " OwnerCode = " + pf.OwnerCode.ToString() + ",") + " Locus = '" + pf.Locus + "',") + " ProjectIntro = '" + pf.ProjectIntro + "' ";
            if (type == "Forepart")
            {
                str = ((((((str + " ,MainWork = '" + pf.MainWork + "',") + " Contact = '" + pf.Contact + "',") + " FundQuarry = '" + pf.FundQuarry + "',") + " RekonPrice = " + pf.RekonPrice.ToString() + ",") + " Remark = '" + pf.Remark + "',") + " AddDate = '" + pf.AddDate.ToString() + "',") + " PrjFollow = '" + pf.PrjFollow + "' ";
            }
            return publicDbOpClass.ExecSqlString(str + " where ProjectCode = '" + pf.ProjectCode.ToString() + "'");
        }

        public int UpdProjectForepart(Guid ProjectCode, int State)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { "update EPM_Prj_ForePart set State = ", State, " where ProjectCode = '", ProjectCode, "'" }));
        }
    }
}

