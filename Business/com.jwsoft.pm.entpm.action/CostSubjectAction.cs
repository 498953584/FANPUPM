namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class CostSubjectAction
    {
        public static CostSubjectCollection GetCostSubjectInfos()
        {
            CostSubjectCollection subjects = new CostSubjectCollection();
            string sqlString = "select * from Prj_Costsubjects where IsValid=1 and SecNum=0";
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                CostSubjectInfo info = new CostSubjectInfo {
                    SubjectID = int.Parse(row["SubjectID"].ToString()),
                    FirstNum = int.Parse(row["FirstNum"].ToString()),
                    SubjectName = row["SubjectName"].ToString()
                };
                if (int.Parse(publicDbOpClass.ExecuteScalar(string.Format("select isNull(Count(*),0) from Prj_Costsubjects where firstNum={0} and secNum<>0 and IsValid=1", info.FirstNum)).ToString()) > 0)
                {
                    info.IsHaveChild = true;
                }
                subjects.Add(info);
                DataTable table = publicDbOpClass.DataTableQuary(string.Format("select * from Prj_Costsubjects where FirstNum={0} and IsValid=1 and secnum>0 order by secNum", info.FirstNum));
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    CostSubjectInfo info2 = new CostSubjectInfo {
                        SubjectID = int.Parse(table.Rows[i]["SubjectID"].ToString()),
                        FirstNum = int.Parse(table.Rows[i]["FirstNum"].ToString()),
                        SubjectName = table.Rows[i]["SubjectName"].ToString(),
                        SecNum = int.Parse(table.Rows[i]["SecNum"].ToString())
                    };
                    subjects.Add(info2);
                }
                table.Dispose();
            }
            return subjects;
        }

        public static CostSubjectInfo GetFistNewItem()
        {
            CostSubjectInfo info = new CostSubjectInfo();
            string sqlString = string.Format("select isnull(Max(subjectId),0)+1 from Prj_Costsubjects", new object[0]);
            info.SubjectID = int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
            info.SubjectName = "";
            sqlString = string.Format("select isNull(Max(FirstNum),0)+1 from Prj_Costsubjects where isvalid=1", new object[0]);
            info.FirstNum = int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
            info.SecNum = 0;
            info.IsEdit = true;
            info.IsValid = true;
            return info;
        }

        public static CostSubjectInfo GetNewItem(int FirstNum)
        {
            CostSubjectInfo info = new CostSubjectInfo();
            string sqlString = string.Format("select Max(subjectId)+1 from Prj_Costsubjects", new object[0]);
            info.SubjectID = int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
            info.SubjectName = "";
            info.FirstNum = FirstNum;
            sqlString = string.Format("select isNull(Max(secNum),0)+1 from Prj_Costsubjects where FirstNum={0} and isvalid=1", FirstNum);
            info.SecNum = int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
            info.IsEdit = true;
            info.IsValid = true;
            return info;
        }

        public static bool UpdateSubjectInfo(CostSubjectInfo objInfo)
        {
            string str;
            if (publicDbOpClass.DataTableQuary("select * from Prj_Costsubjects where subjectId = " + objInfo.SubjectID.ToString()).Rows.Count > 0)
            {
                str = ("update Prj_Costsubjects set SubjectName='" + objInfo.SubjectName + "',IsValid=") + (objInfo.IsValid ? "1" : "0") + " where subjectId=" + objInfo.SubjectID.ToString();
            }
            else
            {
                str = "insert into Prj_Costsubjects(FirstNum,SecNum,SubjectName,IsValid) values(";
                string str2 = str;
                str = str2 + objInfo.FirstNum.ToString() + "," + objInfo.SecNum.ToString() + ",'" + objInfo.SubjectName + "',1);";
            }
            return publicDbOpClass.NonQuerySqlString(str);
        }
    }
}

