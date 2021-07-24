namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public class inputReceiptAction
    {
        public bool CheckDataIsAtDb(inputReceiptYear objInfo)
        {
            return (publicDbOpClass.DataTableQuary(string.Format("select * from Prj_IncomeDevotionPlan where PrjCode='{0}' and PlanYear='{1}'", objInfo.PrjCode, objInfo.PlanYear)).Rows.Count > 0);
        }

        public inputReceiptItem CreateNewItem(int MainID)
        {
            inputReceiptItem item = new inputReceiptItem {
                ChildMainID = this.GetNewItemId(),
                MainId = MainID
            };
            string str2 = "insert into Prj_IncomeDevotionPlanChild(ChildMainID,MainId) values(";
            if (publicDbOpClass.NonQuerySqlString(str2 + item.ChildMainID.ToString() + "," + item.MainId.ToString() + ")"))
            {
                return item;
            }
            return null;
        }

        public static bool DelItemInfo(int ItemId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("delete from Prj_IncomeDevotionPlanChild where ChildMainID={0}", ItemId));
        }

        public static bool DelYearPlan(int MainID)
        {
            return publicDbOpClass.NonQuerySqlString(("delete Prj_IncomeDevotionPlanChild where MainID = " + MainID.ToString()) + "delete Prj_IncomeDevotionPlan where mainid=" + MainID.ToString());
        }

        private inputReceiptItem FormatToItemModel(DataRow dr)
        {
            inputReceiptItem item = new inputReceiptItem();
            string str = "";
            if (publicDbOpClass.ExecuteScalar("select V_BMMC from pt_d_bm where i_bmdm=" + dr["DepartmentName"].ToString()) != null)
            {
                str = publicDbOpClass.ExecuteScalar("select V_BMMC from pt_d_bm where i_bmdm=" + dr["DepartmentName"].ToString()).ToString();
            }
            item.ChildMainID = int.Parse(dr["ChildMainID"].ToString());
            item.DepartCode = dr["DepartmentName"].ToString();
            item.DepartmentName = str;
            item.DevotionMoney = decimal.Parse(dr["DevotionMoney"].ToString());
            item.MainId = int.Parse(dr["MainId"].ToString());
            item.IncomeMoney = decimal.Parse(dr["IncomeMoney"].ToString());
            return item;
        }

        private inputReceiptYear FormatToModel(DataRow dr)
        {
            return new inputReceiptYear { MainId = int.Parse(dr["MainId"].ToString()), PlanYear = dr["PlanYear"].ToString(), SerialNumber = dr["SerialNumber"].ToString(), Remark = dr["Remark"].ToString(), ExaminePeople = dr["ExaminePeople"].ToString(), ExamineTime = DateTime.Parse(dr["ExamineTime"].ToString()), TabPeople = dr["TabPeople"].ToString(), TabTime = DateTime.Parse(dr["TabTime"].ToString()), PrjCode = dr["PrjCode"].ToString() };
        }

        public static DataTable GetExistPlanYear(string prjCode)
        {
            return publicDbOpClass.DataTableQuary(string.Format("select distinct planyear from Prj_IncomeDevotionPlan", new object[0]));
        }

        public int GetNewItemId()
        {
            string sqlString = "select isnull(max(ChildMainID),0)+1 from Prj_IncomeDevotionPlanChild";
            return int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
        }

        public int GetNewMainId()
        {
            string sqlString = "select isnull(max(mainid),0)+1 from Prj_IncomeDevotionPlan";
            return int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
        }

        public inputReceiptItemCollectin GetPlanItemInfos(int MainID)
        {
            inputReceiptItemCollectin collectin = new inputReceiptItemCollectin();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(string.Format("select * from Prj_IncomeDevotionPlanChild where MainID={0}", MainID)).Rows)
            {
                collectin.Add(this.FormatToItemModel(row));
            }
            return collectin;
        }

        public static inputReceiptReportCollectin GetReportInfo(int PlanId, out string PlanYear)
        {
            string str;
            PlanYear = DateTime.Today.Year.ToString();
            inputReceiptReportCollectin collectin = new inputReceiptReportCollectin();
            DataTable table = publicDbOpClass.DataTableQuary(string.Format("select * from Prj_IncomeDevotionPlan where MainID={0}", PlanId));
            if (table.Rows.Count > 0)
            {
                PlanYear = table.Rows[0]["PlanYear"].ToString();
                foreach (DataRow row in publicDbOpClass.DataTableQuary(("select sum(IncomeMoney) as IncomeMoney,sum(DevotionMoney) as DevotionMoney,DepartmentName  from Prj_IncomeDevotionPlanChild where MainID=" + PlanId.ToString()) + " and DepartmentName not in (select ProjectDepName from Prj_ProjectInfo)" + " group by DepartmentName").Rows)
                {
                    str = string.Format("select v_bmmc from pt_d_bm where i_bmdm={0}", row["DepartmentName"].ToString());
                    inputReceiptReportInfo info = new inputReceiptReportInfo();
                    object obj2 = publicDbOpClass.ExecuteScalar(str);
                    if (obj2 == null)
                    {
                        info.IsProject = true;
                        object obj3 = publicDbOpClass.ExecuteScalar("select prjName from Prj_ProjectInfo where prjCode='" + row["DepartmentName"].ToString() + "'");
                        if (obj3 != null)
                        {
                            info.PrjName = obj3.ToString();
                        }
                    }
                    else
                    {
                        info.IsProject = false;
                        info.PrjName = obj2.ToString();
                    }
                    info.InputAmount = decimal.Parse(row["DevotionMoney"].ToString());
                    info.ReceiptAmount = decimal.Parse(row["IncomeMoney"].ToString());
                    str = ("select isnull(sum(ScienceEmpolderMoney),0)/10000 from Prj_ScienceEmpolderDevotion where year(FillTime)=" + PlanYear) + " and prjCode='" + row["DepartmentName"].ToString() + "'";
                    info.InputCompAmount = decimal.Parse(publicDbOpClass.ExecuteScalar(str).ToString());
                    str = ("select isnull(sum(ComAuditValue),0)/10000 from Prj_TechnologyAdvancementIncome where year(EndDate)=" + PlanYear) + " and prjCode='" + row["DepartmentName"].ToString() + "'";
                    info.ReceiptCompAmount = decimal.Parse(publicDbOpClass.ExecuteScalar(str).ToString());
                    collectin.Add(info);
                }
            }
            table.Dispose();
            foreach (DataRow row2 in publicDbOpClass.DataTableQuary(("select isnull(sum(ScienceEmpolderMoney),0)/10000,prjCode from Prj_ScienceEmpolderDevotion where year(FillTime)=" + PlanYear) + " and prjCode not in(select DepartmentName from Prj_IncomeDevotionPlanChild where MainID=" + PlanId.ToString() + ") group by prjCode").Rows)
            {
                inputReceiptReportInfo info2 = new inputReceiptReportInfo {
                    InputCompAmount = decimal.Parse(row2[0].ToString()),
                    IsProject = true
                };
                str = ("select isnull(sum(ComAuditValue),0)/10000 from Prj_TechnologyAdvancementIncome where year(EndDate)=" + PlanYear) + " and prjCode='" + row2["prjCode"].ToString() + "'";
                info2.ReceiptCompAmount = decimal.Parse(publicDbOpClass.ExecuteScalar(str).ToString());
                info2.PrjName = publicDbOpClass.ExecuteScalar("select prjName from Prj_ProjectInfo where prjCode='" + row2["prjCode"].ToString() + "'").ToString();
                DataTable table2 = publicDbOpClass.DataTableQuary("select a.PrjName,b.v_bmmc,c.* from Prj_ProjectInfo a,pt_d_bm b,Prj_IncomeDevotionPlanChild c where cast(isnull(a.projectDepName,0) as int)=b.i_bmdm and a.projectDepName=c.DepartmentName and a.PrjCode='" + row2["prjCode"].ToString() + "'");
                if (table2.Rows.Count > 0)
                {
                    info2.InputAmount = decimal.Parse(table2.Rows[0]["DevotionMoney"].ToString());
                    info2.ReceiptAmount = decimal.Parse(table2.Rows[0]["IncomeMoney"].ToString());
                }
                table2.Dispose();
                collectin.Add(info2);
            }
            return collectin;
        }

        public inputReceiptYear GetYearPlanInfo(string MainID)
        {
            string sqlString = "select * from Prj_IncomeDevotionPlan  where MainID=" + MainID;
            inputReceiptYear year = new inputReceiptYear();
            if (MainID != "")
            {
                foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
                {
                    year = this.FormatToModel(row);
                }
            }
            return year;
        }

        public inputReceiptYearCollectin GetYearPlanInfos(string prjCode)
        {
            string sqlString = "select * from Prj_IncomeDevotionPlan ";
            inputReceiptYearCollectin collectin = new inputReceiptYearCollectin();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                collectin.Add(this.FormatToModel(row));
            }
            return collectin;
        }

        public bool SaveYearPlan(inputReceiptYear objInfo)
        {
            string sqlString = "";
            if (objInfo.MainId == 0)
            {
                objInfo.MainId = this.GetNewMainId();
                string str2 = "insert into Prj_IncomeDevotionPlan values(" + objInfo.MainId.ToString();
                string str3 = str2 + ",'" + objInfo.PrjCode + "','" + objInfo.SerialNumber + "','" + objInfo.PlanYear;
                string str4 = str3 + "','" + objInfo.TabPeople + "','" + objInfo.TabTime.ToShortDateString() + "','";
                sqlString = (str4 + objInfo.ExaminePeople + "','" + objInfo.ExamineTime.ToShortDateString() + "','") + objInfo.Remark + "')";
            }
            else
            {
                string str5 = "update Prj_IncomeDevotionPlan set SerialNumber='" + objInfo.SerialNumber;
                string str6 = str5 + "',PlanYear='" + objInfo.PlanYear + "',TabPeople='" + objInfo.TabPeople + "',TabTime='";
                string str7 = str6 + objInfo.TabTime.ToString() + "',ExaminePeople='" + objInfo.ExaminePeople + "',ExamineTime='";
                sqlString = (str7 + objInfo.ExamineTime.ToString() + "',Remark='" + objInfo.Remark + "' where mainid=") + objInfo.MainId.ToString();
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool UpdateItemInfo(inputReceiptItem objInfo)
        {
            return publicDbOpClass.NonQuerySqlString((("update Prj_IncomeDevotionPlanChild set DepartmentName='" + objInfo.DepartCode) + "',IncomeMoney=" + objInfo.IncomeMoney.ToString() + ",DevotionMoney=") + objInfo.DevotionMoney.ToString() + " where ChildMainID=" + objInfo.ChildMainID.ToString());
        }
    }
}

