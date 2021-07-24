namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public class DevelopmentInputAction
    {
        public static bool AddNewInput(DevelopmentInputInfo objInfo, out int MainId)
        {
            objInfo.MainID = GetNewMainID();
            string str2 = "insert into Prj_ScienceEmpolderDevotion values(" + objInfo.MainID.ToString();
            string str3 = str2 + ",'" + objInfo.PrjCode + "','" + objInfo.FillTime.ToShortDateString() + "','" + objInfo.WeavePeople + "','";
            string sqlString = str3 + "',''," + objInfo.ScienceEmpolderMoney.ToString() + ",'',0,'','" + objInfo.UnitCode + "')";
            MainId = objInfo.MainID;
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool AuditInputInfo(DevelopmentInputInfo objInfo)
        {
            string str2 = "update Prj_ScienceEmpolderDevotion set AuditPeople='" + objInfo.AuditPeople + "',";
            string str3 = str2 + "AuditTime='" + objInfo.AuditTime.ToString() + "',AuditResult=" + (objInfo.AuditResult ? "1" : "0");
            return publicDbOpClass.NonQuerySqlString(str3 + ",AuditIdea='" + objInfo.AuditIdea + "' where MainID=" + objInfo.MainID.ToString());
        }

        public static bool CancelAudit(int MainID)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("update Prj_ScienceEmpolderDevotion set AuditResult=-1,auditPeople='' where MainID={0}", MainID));
        }

        public static bool DelInput(int MainID)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("Delete Prj_ScienceEmpolderDevotionChild where MainID={0}", MainID) + string.Format("Delete Prj_ScienceEmpolderDevotion where MainID={0}", MainID));
        }

        public static bool DelInputItem(int ChildMainID)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("Delete Prj_ScienceEmpolderDevotionChild where ChildMainID={0}", ChildMainID));
        }

        private InputItemInfo FormatToItemInfo(DataRow dr)
        {
            return null;
            //InputItemInfo info;
            //return new InputItemInfo { ChildMainId = int.Parse(dr["ChildMainId"].ToString()), MainID = int.Parse(dr["MainID"].ToString()), DevotionMoney = decimal.Round(decimal.Parse(dr["DevotionMoney"].ToString()), 2), Remark = dr["Remark"].ToString(), SubjectID = int.Parse(dr["SubjectID"].ToString()), SubjectName = publicDbOpClass.ExecuteScalar("select SubjectName from Prj_Costsubjects where SubjectID=" + info.SubjectID.ToString()).ToString(), FirstNum = int.Parse(publicDbOpClass.ExecuteScalar("select FirstNum from Prj_Costsubjects where SubjectID=" + info.SubjectID.ToString()).ToString()), SecNum = int.Parse(publicDbOpClass.ExecuteScalar("select SecNum from Prj_Costsubjects where SubjectID=" + info.SubjectID.ToString()).ToString()) };
        }

        private DevelopmentInputInfo FormatToMainInfo(DataRow dr)
        {
            DevelopmentInputInfo info;
            info = new DevelopmentInputInfo {
                AuditIdea = dr["AuditIdea"].ToString(),
                AuditPeople = dr["AuditPeople"].ToString(),
                AuditResult = (dr["AuditResult"].ToString() == "True") ? true : false,
                AuditTime = DateTime.Parse((dr["AuditTime"].ToString() == "") ? DateTime.Today.ToString() : dr["AuditTime"].ToString()),
                FillTime = DateTime.Parse(dr["FillTime"].ToString()),
                MainID = int.Parse(dr["MainID"].ToString()),
                PrjCode = dr["PrjCode"].ToString(),
                PrjName = publicDbOpClass.ExecuteScalar("select PrjName from Prj_ProjectInfo where prjCode='" + dr["PrjCode"].ToString() + "'").ToString(),
                ScienceEmpolderMoney = decimal.Round(decimal.Parse(dr["ScienceEmpolderMoney"].ToString()), 2),
                UnitCode = dr["UnitCode"].ToString(),
                WeavePeople = dr["WeavePeople"].ToString()
            };
            object obj2 = publicDbOpClass.ExecuteScalar("select V_BMMC from pt_d_bm where i_bmdm=" + info.UnitCode);
            if (obj2 != null)
            {
                info.UnitName = obj2.ToString();
            }
            return info;
        }

        public InputItemCollectin GetCostSubjectInfos(int MainID)
        {
            InputItemCollectin collectin = new InputItemCollectin();
            foreach (DataRow row in publicDbOpClass.DataTableQuary("select * from prj_V_CostInputItem where SecNum=0 and MainID=" + MainID.ToString()).Rows)
            {
                InputItemInfo info = new InputItemInfo {
                    SubjectID = int.Parse(row["SubjectID"].ToString()),
                    FirstNum = int.Parse(row["FirstNum"].ToString()),
                    SubjectName = row["SubjectName"].ToString(),
                    MainID = int.Parse(row["MainID"].ToString()),
                    ChildMainId = int.Parse(row["ChildMainId"].ToString()),
                    Remark = row["Remark"].ToString(),
                    DevotionMoney = decimal.Parse(row["DevotionMoney"].ToString())
                };
                if (int.Parse(publicDbOpClass.ExecuteScalar(string.Format("select isNull(Count(*),0) from prj_V_CostInputItem where firstNum={0} and secNum<>0 and MainID={1}", info.FirstNum, MainID)).ToString()) > 0)
                {
                    info.IsHaveChild = true;
                }
                collectin.Add(info);
                DataTable table = publicDbOpClass.DataTableQuary(string.Format("select * from prj_V_CostInputItem where FirstNum={0} and secnum>0  and MainID={1} order by secNum", info.FirstNum, MainID));
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    InputItemInfo info2 = new InputItemInfo {
                        SubjectID = int.Parse(table.Rows[i]["SubjectID"].ToString()),
                        FirstNum = int.Parse(table.Rows[i]["FirstNum"].ToString()),
                        SubjectName = table.Rows[i]["SubjectName"].ToString(),
                        SecNum = int.Parse(table.Rows[i]["SecNum"].ToString()),
                        MainID = int.Parse(table.Rows[i]["MainID"].ToString()),
                        ChildMainId = int.Parse(table.Rows[i]["ChildMainID"].ToString()),
                        Remark = table.Rows[i]["Remark"].ToString(),
                        DevotionMoney = decimal.Parse(table.Rows[i]["DevotionMoney"].ToString())
                    };
                    collectin.Add(info2);
                }
                table.Dispose();
            }
            return collectin;
        }

        public InputItemCollectin GetItemInfos(int MainID)
        {
            InputItemCollectin costSubjectInfos = new InputItemCollectin();
            if (this.SubjectMoveToItem(MainID))
            {
                costSubjectInfos = this.GetCostSubjectInfos(MainID);
            }
            return costSubjectInfos;
        }

        public DevelopmentInputInfo GetMainInputInfo(int MainId)
        {
            string sqlString = string.Format("select * from Prj_ScienceEmpolderDevotion where MainId={0}", MainId);
            DevelopmentInputInfo info = new DevelopmentInputInfo();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                info = this.FormatToMainInfo(row);
            }
            return info;
        }

        public DevelopmentInputCollectin GetMainInputInfos(string prjCode)
        {
            string sqlString = string.Format("select * from Prj_ScienceEmpolderDevotion where PrjCode='{0}'", prjCode);
            DevelopmentInputCollectin collectin = new DevelopmentInputCollectin();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                collectin.Add(this.FormatToMainInfo(row));
            }
            return collectin;
        }

        private string GetNewItemID()
        {
            return publicDbOpClass.ExecuteScalar("select isnull(max(ChildMainID),0)+1 from Prj_ScienceEmpolderDevotionChild").ToString();
        }

        public static int GetNewMainID()
        {
            string sqlString = "select isNull(Max(MainID),0)+1 from Prj_ScienceEmpolderDevotion";
            return int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
        }

        public bool isHaveItemInfos(int MainID)
        {
            return (publicDbOpClass.GetRecordCount("Prj_ScienceEmpolderDevotionChild", "MainId=" + MainID.ToString()) > 0);
        }

        public static bool ItemIsAtDb(int MainID, int SubjectId)
        {
            return (publicDbOpClass.ExecuteScalar(("select ChildMainID Prj_ScienceEmpolderDevotionChild where MainID=" + MainID.ToString()) + " and SubjectID = " + SubjectId.ToString()) != null);
        }

        public static bool SaveItems(InputItemCollectin objInfos)
        {
            string sqlString = "";
            decimal num = 0M;
            for (int i = 0; i < objInfos.Count; i++)
            {
                string str2 = sqlString + " update Prj_ScienceEmpolderDevotionChild set DevotionMoney=" + objInfos[i].DevotionMoney.ToString();
                sqlString = str2 + ",Remark='" + objInfos[i].Remark + "' where ChildMainID=" + objInfos[i].ChildMainId.ToString();
                num += objInfos[i].DevotionMoney;
                if (i == (objInfos.Count - 1))
                {
                    string str3 = sqlString;
                    sqlString = str3 + " update Prj_ScienceEmpolderDevotion set ScienceEmpolderMoney=" + num.ToString() + " where MainID=" + objInfos[0].MainID.ToString();
                }
            }
            return ((sqlString != "") && publicDbOpClass.NonQuerySqlString(sqlString));
        }

        private bool SubjectMoveToItem(int MainID)
        {
            string sqlString = "select subjectID from dbo.Prj_Costsubjects where subjectID not in(select subjectID ";
            sqlString = sqlString + "from Prj_ScienceEmpolderDevotionChild where MainID=" + MainID.ToString() + ") and isvalid=1";
            string str2 = "";
            string newItemID = this.GetNewItemID();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                str2 = str2 + " insert into Prj_ScienceEmpolderDevotionChild values(" + newItemID;
                string str4 = str2;
                str2 = str4 + "," + MainID.ToString() + "," + row["subjectID"].ToString() + ",0,'') ";
                newItemID = Convert.ToString((int) (int.Parse(newItemID) + 1));
            }
            if (str2 != "")
            {
                return publicDbOpClass.NonQuerySqlString(str2);
            }
            return true;
        }

        public static bool UpdateInput(DevelopmentInputInfo objInfo)
        {
            string str2 = "update Prj_ScienceEmpolderDevotion set prjCode='" + objInfo.PrjCode + "',UnitCode='" + objInfo.UnitCode;
            return publicDbOpClass.NonQuerySqlString((str2 + "',FillTime='" + objInfo.FillTime.ToShortDateString() + "',WeavePeople='" + objInfo.WeavePeople + "',ScienceEmpolderMoney=") + objInfo.ScienceEmpolderMoney.ToString() + " where MainID=" + objInfo.MainID.ToString());
        }
    }
}

