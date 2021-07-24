namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OAFileDestroyAction
    {
        public int Add(OAFileDestroy model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_File_Destroy(");
            builder.Append("DestroyRecordID,DestroyFileID");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.DestroyRecordID + "',");
            builder.Append(model.DestroyFileID);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID, ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_File_Destroy ");
            builder.Append(" where DestroyRecordID='" + RecordID + "' ");
            for (int i = 0; i < arr.Count; i++)
            {
                if (((OAFileDestroy) arr[i]).DestroyFileID != -2)
                {
                    builder.Append(string.Concat(new object[] { " if not exists(select top 1 DestroyFileID from OA_File_Destroy where DestroyRecordID='", ((OAFileDestroy) arr[i]).DestroyRecordID, "' and DestroyFileID='", ((OAFileDestroy) arr[i]).DestroyFileID, "') " }));
                    builder.Append(" begin ");
                    builder.Append(" insert into OA_File_Destroy( ");
                    builder.Append(" DestroyRecordID,DestroyFileID) ");
                    builder.Append(" values ( ");
                    builder.Append(" '" + ((OAFileDestroy) arr[i]).DestroyRecordID + "', ");
                    builder.Append(" '" + ((OAFileDestroy) arr[i]).DestroyFileID + "' ");
                    builder.Append(" ) ");
                    builder.Append(" end ");
                }
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID, string DestroyFileID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_File_Destroy ");
            builder.Append(" where DestroyRecordID='" + RecordID + "' ");
            builder.Append(" and DestroyFileID='" + DestroyFileID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid DestroyRecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_File_Destroy where DestroyRecordID=" + DestroyRecordID);
            if (publicDbOpClass.ExecuteScalar(builder.ToString()) == DBNull.Value)
            {
                return false;
            }
            return true;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM V_OA_File_Destroy ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_File_Destroy", "DestroyRecordID");
        }

        public ArrayList GetModel(string strWhere)
        {
            ArrayList list = new ArrayList();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM V_OA_File_Destroy ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    OAFileDestroy destroy = new OAFileDestroy();
                    if (table.Rows[i]["DestroyRecordID"].ToString() != "")
                    {
                        destroy.DestroyRecordID = new Guid(table.Rows[i]["DestroyRecordID"].ToString());
                    }
                    if (table.Rows[i]["DestroyFileID"].ToString() != "")
                    {
                        destroy.DestroyFileID = int.Parse(table.Rows[i]["DestroyFileID"].ToString());
                    }
                    destroy.OAFileCatalog.FileName = table.Rows[i]["FileName"].ToString();
                    destroy.OAFileCatalog.FileCode = table.Rows[i]["FileCode"].ToString();
                    destroy.OAFileCatalog.PrjName = table.Rows[i]["PrjName"].ToString();
                    if (table.Rows[i]["SecretLevel"].ToString() != "")
                    {
                        destroy.OAFileCatalog.SecretLevel = int.Parse(table.Rows[i]["SecretLevel"].ToString());
                    }
                    list.Add(destroy);
                }
            }
            return list;
        }

        public int Update(OAFileDestroy model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_File_Destroy set");
            builder.Append(" where DestroyRecordID=" + model.DestroyRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

