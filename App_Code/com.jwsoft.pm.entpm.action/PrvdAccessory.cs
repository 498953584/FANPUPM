namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PrvdAccessory
    {
        public int Add(PrvdAccessoryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_prvd_accessory(");
            builder.Append("AccessoryId,ProviderId,AccessoryName,UserCode,OriginalName,FilePath,RecordDate,Comment");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.AccessoryId + ",");
            builder.Append(model.ProviderId + ",");
            builder.Append("'" + model.AccessoryName + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.OriginalName + "',");
            builder.Append("'" + model.FilePath + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.Comment + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool AddProAccessory(string ProviderId, string Code, string Name, string FileName, string FilePath, string UserCode, string RecordDate)
        {
            string str = "";
            str = " begin ";
            string str2 = str;
            return publicDbOpClass.NonQuerySqlString((str2 + " insert into pm_prvd_accessory (ProviderId,Code, Name, AccessoryName, AccessoryPath,UserCode,RecordDate)values (" + ProviderId + "," + Code + "," + Name + ",'" + FileName + "','" + FilePath + "','" + UserCode + "','" + RecordDate + "')") + " end");
        }

        public int Delete(int AccessoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_prvd_accessory ");
            builder.Append(" where AccessoryId=" + AccessoryId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int AccessoryId)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_prvd_accessory where AccessoryId=" + AccessoryId);
            object objA = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(objA.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from pm_prvd_accessory ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by AccessoryId ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("pm_prvd_accessory", "AccessoryId");
        }

        public PrvdAccessoryModel GetModel(int AccessoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from pm_prvd_accessory ");
            builder.Append(" where AccessoryId=" + AccessoryId);
            PrvdAccessoryModel model = new PrvdAccessoryModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            model.AccessoryId = AccessoryId;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ProviderId"].ToString() != "")
            {
                model.ProviderId = int.Parse(set.Tables[0].Rows[0]["ProviderId"].ToString());
            }
            model.AccessoryName = set.Tables[0].Rows[0]["AccessoryName"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            model.Comment = set.Tables[0].Rows[0]["Comment"].ToString();
            return model;
        }

        public DataTable GetProPrvdAccessory(string AccessoryId)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_prvd_accessory where AccessoryId = " + AccessoryId);
        }

        public int Update(PrvdAccessoryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_prvd_accessory set ");
            builder.Append("ProviderId=" + model.ProviderId + ",");
            builder.Append("AccessoryName='" + model.AccessoryName + "',");
            builder.Append("Comment='" + model.Comment + "'");
            builder.Append(" where AccessoryId=" + model.AccessoryId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool UpdProAccessory(string AccessoryId)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.NonQuerySqlString((str + " update pm_prvd_accessory set AccessoryName='',AccessoryPath=''   where AccessoryId=" + AccessoryId + " ") + " end ");
        }

        public bool UpdProAccessory2(string AccessoryId, string Code, string Name, string FileName, string FilePath, string UserCode, string RecordDate)
        {
            string str = "";
            str = " begin ";
            string str2 = str;
            return publicDbOpClass.NonQuerySqlString((str2 + " update pm_prvd_accessory set Code=" + Code + ",Name=" + Name + ",AccessoryName='" + FileName + "',AccessoryPath='" + FilePath + "',UserCode='" + UserCode + "',RecordDate='" + RecordDate + "'   where AccessoryId=" + AccessoryId + " ") + " end ");
        }
    }
}

