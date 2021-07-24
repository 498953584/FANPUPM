namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OAOfficeResApplicationCollectDetailAction
    {
        public int Add(OAOfficeResApplicationCollectDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_ApplicationCollectDetail(");
            builder.Append("ACRecordID,MaterialID,ApplyNumber");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ACRecordID + "',");
            builder.Append(model.MaterialID + ",");
            builder.Append(model.ApplyNumber);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Add(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    builder.Append("insert into OA_OfficeRes_ApplicationCollectDetail(");
                    builder.Append("ACRecordID,MaterialID,ApplyNumber");
                    builder.Append(")");
                    builder.Append(" values (");
                    builder.Append("'" + ((OAOfficeResApplicationCollectDetail) arr[i]).ACRecordID + "',");
                    builder.Append(((OAOfficeResApplicationCollectDetail) arr[i]).MaterialID + ",");
                    builder.Append(((OAOfficeResApplicationCollectDetail) arr[i]).ApplyNumber);
                    builder.Append(")");
                }
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ACDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_ApplicationCollectDetail ");
            builder.Append(" where ACDetailID=" + ACDetailID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int ACDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 ACDetailID from OA_OfficeRes_ApplicationCollectDetail where ACDetailID=" + ACDetailID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.AuditState,a.iYear,a.iMonth,a.ApplyType,a.CorpCode,a.SumMoney,a.IsSubmit,a.UserCode,a.RecordDate,a.Remark,b.ACDetailID,b.ACRecordID,b.MaterialID,b.ApplyNumber,c.ResName,c.UseType,c.GetType,c.Unit,c.PlanFee ");
            builder.Append(" FROM OA_OfficeRes_ApplicationCollect a ");
            builder.Append(" join OA_OfficeRes_ApplicationCollectDetail b on a.ACRecordID=b.ACRecordID ");
            builder.Append(" join OA_OfficeRes_Resources c on b.MaterialID=c.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResApplicationCollectDetail GetModel(int ACDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" ACDetailID,ACRecordID,MaterialID,ApplyNumber ");
            builder.Append(" from OA_OfficeRes_ApplicationCollectDetail ");
            builder.Append(" where ACDetailID=" + ACDetailID);
            OAOfficeResApplicationCollectDetail detail = new OAOfficeResApplicationCollectDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ACDetailID"].ToString() != "")
            {
                detail.ACDetailID = int.Parse(set.Tables[0].Rows[0]["ACDetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["ACRecordID"].ToString() != "")
            {
                detail.ACRecordID = new Guid(set.Tables[0].Rows[0]["ACRecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialID"].ToString() != "")
            {
                detail.MaterialID = int.Parse(set.Tables[0].Rows[0]["MaterialID"].ToString());
            }
            if (set.Tables[0].Rows[0]["ApplyNumber"].ToString() != "")
            {
                detail.ApplyNumber = int.Parse(set.Tables[0].Rows[0]["ApplyNumber"].ToString());
            }
            return detail;
        }

        public string strApplyDepartmentName(Guid ACRecordID, int MaterialID)
        {
            string str = "";
            object obj2 = str + " select ApplyDepartment from OA_OfficeRes_DepartmentApplication a right join " + " OA_OfficeRes_DepartmentApplicationDetail b on a.DARecordID = b.DARecordID";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where  ACRecordID = '", ACRecordID, "' " }) + " and MaterialID = " + MaterialID);
            if (table.Rows.Count > 0)
            {
                userManageDb db = new userManageDb();
                return db.depName(table.Rows[0]["ApplyDepartment"].ToString());
            }
            return "";
        }

        public string strApplyManName(Guid ACRecordID, int MaterialID)
        {
            string str = "";
            object obj2 = str + " select ApplyMan from OA_OfficeRes_PersonalApplication a " + " right join OA_OfficeRes_PersonalApplicationDetail b ";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " on a.PARecordID = b.PARecordID where ACRecordID = '", ACRecordID, "' " }) + " and MaterialID = " + MaterialID);
            if (table.Rows.Count > 0)
            {
                userManageDb db = new userManageDb();
                return db.GetUserName(table.Rows[0]["ApplyMan"].ToString());
            }
            return "";
        }

        public int Update(OAOfficeResApplicationCollectDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_ApplicationCollectDetail set ");
            builder.Append("ApplyNumber=" + model.ApplyNumber);
            builder.Append(" where ACDetailID=" + model.ACDetailID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

