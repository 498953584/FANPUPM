namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class RoleProjectAction
    {
        public int Add(RoleProjectModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into WF_RoleProject(");
            builder.Append("RoleID,UserCode,ProjectCodes");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.RoleID + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.ProjectCodes + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        private int CreateLevel(DataTable dtPrjClassTemp, string ClassCode)
        {
            DataRow[] rowArray = dtPrjClassTemp.Select("ClassCode='" + ClassCode + "'");
            if (rowArray[0]["ParentClassCode"].ToString() == "")
            {
                return 0;
            }
            return (this.CreateLevel(dtPrjClassTemp, rowArray[0]["ParentClassCode"].ToString()) + 1);
        }

        public int Delete(int RoleProjectID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete WF_RoleProject ");
            builder.Append(" where RoleProjectID=" + RoleProjectID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RoleProjectID,RoleID,UserCode,ProjectCodes ");
            builder.Append(" FROM WF_RoleProject ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public RoleProjectModel GetModel(int RoleProjectID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1  ");
            builder.Append(" RoleProjectID,RoleID,UserCode,ProjectCodes ");
            builder.Append(" from WF_RoleProject ");
            builder.Append(" where RoleProjectID=" + RoleProjectID + " ");
            RoleProjectModel model = new RoleProjectModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RoleProjectID"].ToString() != "")
            {
                model.RoleProjectID = int.Parse(set.Tables[0].Rows[0]["RoleProjectID"].ToString());
            }
            if (set.Tables[0].Rows[0]["RoleID"].ToString() != "")
            {
                model.RoleID = new int?(int.Parse(set.Tables[0].Rows[0]["RoleID"].ToString()));
            }
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            model.ProjectCodes = set.Tables[0].Rows[0]["ProjectCodes"].ToString();
            return model;
        }

        public DataTable GetPrjClassStru(string ClassCode)
        {
            DataTable dtPrjClassStru = new DataTable();
            dtPrjClassStru.Columns.Add("ClassCode", typeof(string));
            dtPrjClassStru.Columns.Add("ClassName", typeof(string));
            DataTable projClassList = this.GetProjClassList();
            DataRow[] rowArray = projClassList.Select("ParentClassCode = '" + ClassCode + "'");
            if (rowArray.Length > 0)
            {
                for (int i = 0; i < rowArray.Length; i++)
                {
                    DataRow row = dtPrjClassStru.NewRow();
                    row["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                    row["ClassName"] = rowArray[i]["ClassName"].ToString();
                    dtPrjClassStru.Rows.Add(row);
                    if (this.GetSubClassCount(projClassList, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_GenerateFirst(dtPrjClassStru, projClassList, rowArray[i]["ClassCode"].ToString());
                    }
                }
                return dtPrjClassStru;
            }
            DataRow row2 = dtPrjClassStru.NewRow();
            row2["ClassCode"] = "";
            row2["ClassName"] = "---没有项目分类---";
            dtPrjClassStru.Rows.Add(row2);
            return dtPrjClassStru;
        }

        public DataTable GetPrjNameList(string ProjectId)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + " select * from PT_PrjInfo where i_xh in (" + ProjectId + ")");
        }

        public DataTable GetProjClassList()
        {
            string sqlString = "select * from PT_MultiClasses where ClassTypeCode = '009' order by ClassCode ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetProjectList(string ProjectClassCode)
        {
            string sqlString = "";
            if (ProjectClassCode != "0")
            {
                sqlString = "select * from PT_PrjInfo where IsValid = 1  and PrjKindClass ='" + ProjectClassCode.Trim() + "' order by i_xh";
            }
            else
            {
                sqlString = "select * from PT_PrjInfo where IsValid = 1   order by i_xh";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        private int GetSubClassCount(DataTable dtPrjClassTemp, string ClassCode)
        {
            return dtPrjClassTemp.Select("ParentClassCode='" + ClassCode + "'").Length;
        }

        private void PrjClassTree_Generate(DataTable dtPrjClassStru, DataTable dtPrjClassTemp, string ParentClassCode, bool isEnd, int iLevel, string strHead)
        {
            DataRow[] rowArray = dtPrjClassTemp.Select("ParentClassCode='" + ParentClassCode + "'");
            if (isEnd)
            {
                for (int i = 0; i < rowArray.Length; i++)
                {
                    if (i != (rowArray.Length - 1))
                    {
                        DataRow row = dtPrjClassStru.NewRow();
                        row["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                        row["ClassName"] = strHead + this.RepeatString("│", iLevel) + "├" + rowArray[i]["ClassName"].ToString();
                        dtPrjClassStru.Rows.Add(row);
                        if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                        {
                            this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), false, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                        }
                    }
                    else
                    {
                        DataRow row2 = dtPrjClassStru.NewRow();
                        row2["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                        row2["ClassName"] = strHead + this.RepeatString("│", iLevel) + "└" + rowArray[i]["ClassName"].ToString();
                        dtPrjClassStru.Rows.Add(row2);
                        if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                        {
                            this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), true, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < rowArray.Length; j++)
                {
                    DataRow row3 = dtPrjClassStru.NewRow();
                    row3["ClassCode"] = rowArray[j]["ClassCode"].ToString();
                    if (j != (rowArray.Length - 1))
                    {
                        row3["ClassName"] = strHead + this.RepeatString("│", iLevel) + "├" + rowArray[j]["ClassName"].ToString();
                    }
                    else
                    {
                        row3["ClassName"] = strHead + this.RepeatString("│", iLevel) + "└" + rowArray[j]["ClassName"].ToString();
                    }
                    dtPrjClassStru.Rows.Add(row3);
                    if (this.GetSubClassCount(dtPrjClassTemp, rowArray[j]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[j]["ClassCode"].ToString(), true, this.CreateLevel(dtPrjClassTemp, rowArray[j]["ClassCode"].ToString()), strHead);
                    }
                }
            }
        }

        private void PrjClassTree_GenerateFirst(DataTable dtPrjClassStru, DataTable dtPrjClassTemp, string ClassCode)
        {
            DataRow[] rowArray = dtPrjClassTemp.Select("ParentClassCode='" + ClassCode + "'");
            for (int i = 0; i < rowArray.Length; i++)
            {
                if (i != (rowArray.Length - 1))
                {
                    DataRow row = dtPrjClassStru.NewRow();
                    row["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                    row["ClassName"] = "├" + rowArray[i]["ClassName"].ToString();
                    dtPrjClassStru.Rows.Add(row);
                    if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), false, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), "");
                    }
                }
                else
                {
                    DataRow row2 = dtPrjClassStru.NewRow();
                    row2["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                    row2["ClassName"] = "└" + rowArray[i]["ClassName"].ToString();
                    dtPrjClassStru.Rows.Add(row2);
                    if (this.GetSubClassCount(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.PrjClassTree_Generate(dtPrjClassStru, dtPrjClassTemp, rowArray[i]["ClassCode"].ToString(), true, this.CreateLevel(dtPrjClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                    }
                }
            }
        }

        private string RepeatString(string strStr, int iNumber)
        {
            string str = "";
            for (int i = 0; i < iNumber; i++)
            {
                str = str + strStr;
            }
            return str;
        }

        public int Update(RoleProjectModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update WF_RoleProject set ");
            builder.Append("RoleID=" + model.RoleID + ",");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("ProjectCodes='" + model.ProjectCodes + "'");
            builder.Append(" where RoleProjectID=" + model.RoleProjectID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

