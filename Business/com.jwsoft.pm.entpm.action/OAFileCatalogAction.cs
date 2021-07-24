namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAFileCatalogAction
    {
        public int Add(OAFileCatalog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_File_Catalog(");
            builder.Append("LibraryCode,UserCode,RecordDate,FileAge,TimeLimit,ClassID,FileName,Volume,BoxNumber,FileNumber,Principal,SecretLevel,PrjCode,PrjName,SavePlace,PageNumber,FileCode,PigeonholeDate,FileType,Content,LendState,IsValid");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.LibraryCode + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append(model.FileAge + ",");
            builder.Append(model.TimeLimit + ",");
            builder.Append(model.ClassID + ",");
            builder.Append("'" + model.FileName + "',");
            builder.Append("'" + model.Volume + "',");
            builder.Append("'" + model.BoxNumber + "',");
            builder.Append("'" + model.FileNumber + "',");
            builder.Append("'" + model.Principal + "',");
            builder.Append(model.SecretLevel + ",");
            builder.Append("'" + model.PrjCode + "',");
            builder.Append("'" + model.PrjName + "',");
            builder.Append("'" + model.SavePlace + "',");
            builder.Append(model.PageNumber + ",");
            builder.Append("'" + model.FileCode + "',");
            builder.Append("'" + model.PigeonholeDate + "',");
            builder.Append("'" + model.FileType + "',");
            builder.Append("'" + model.Content + "',");
            builder.Append("'" + model.LendState + "',");
            builder.Append("'" + model.IsValid + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_File_Catalog ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_File_Catalog where RecordID=" + RecordID);
            if (publicDbOpClass.ExecuteScalar(builder.ToString()) == DBNull.Value)
            {
                return false;
            }
            return true;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,LibraryCode,UserCode,RecordDate,FileAge,TimeLimit,ClassID,[FileName],Volume,BoxNumber,FileNumber,Principal,SecretLevel,PrjCode,PrjName,SavePlace,PageNumber,FileCode,PigeonholeDate,FileType,Content,LendState,IsValid ");
            builder.Append(" FROM OA_File_Catalog ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetMaxAndMinYear(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select distinct FileAge ");
            builder.Append(" FROM OA_File_Catalog ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAFileCatalog GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(" RecordID,LibraryCode,UserCode,RecordDate,FileAge,TimeLimit,ClassID,FileName,Volume,BoxNumber,FileNumber,Principal,SecretLevel,PrjCode,PrjName,SavePlace,PageNumber,FileCode,PigeonholeDate,FileType,Content,LendState,IsValid ");
            builder.Append(" from OA_File_Catalog ");
            builder.Append(" where RecordID=" + RecordID);
            OAFileCatalog catalog = new OAFileCatalog();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                catalog.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            catalog.LibraryCode = set.Tables[0].Rows[0]["LibraryCode"].ToString();
            catalog.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                catalog.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["FileAge"].ToString() != "")
            {
                catalog.FileAge = int.Parse(set.Tables[0].Rows[0]["FileAge"].ToString());
            }
            if (set.Tables[0].Rows[0]["TimeLimit"].ToString() != "")
            {
                catalog.TimeLimit = int.Parse(set.Tables[0].Rows[0]["TimeLimit"].ToString());
            }
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                catalog.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            catalog.FileName = set.Tables[0].Rows[0]["FileName"].ToString();
            catalog.Volume = set.Tables[0].Rows[0]["Volume"].ToString();
            catalog.BoxNumber = set.Tables[0].Rows[0]["BoxNumber"].ToString();
            catalog.FileNumber = set.Tables[0].Rows[0]["FileNumber"].ToString();
            catalog.Principal = set.Tables[0].Rows[0]["Principal"].ToString();
            if (set.Tables[0].Rows[0]["SecretLevel"].ToString() != "")
            {
                catalog.SecretLevel = int.Parse(set.Tables[0].Rows[0]["SecretLevel"].ToString());
            }
            if (set.Tables[0].Rows[0]["PrjCode"].ToString() != "")
            {
                catalog.PrjCode = new Guid(set.Tables[0].Rows[0]["PrjCode"].ToString());
            }
            catalog.PrjName = set.Tables[0].Rows[0]["PrjName"].ToString();
            catalog.SavePlace = set.Tables[0].Rows[0]["SavePlace"].ToString();
            if (set.Tables[0].Rows[0]["PageNumber"].ToString() != "")
            {
                catalog.PageNumber = int.Parse(set.Tables[0].Rows[0]["PageNumber"].ToString());
            }
            catalog.FileCode = set.Tables[0].Rows[0]["FileCode"].ToString();
            if (set.Tables[0].Rows[0]["PigeonholeDate"].ToString() != "")
            {
                catalog.PigeonholeDate = DateTime.Parse(set.Tables[0].Rows[0]["PigeonholeDate"].ToString());
            }
            catalog.FileType = set.Tables[0].Rows[0]["FileType"].ToString();
            catalog.Content = set.Tables[0].Rows[0]["Content"].ToString();
            catalog.LendState = set.Tables[0].Rows[0]["LendState"].ToString();
            catalog.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            return catalog;
        }

        public int Update(OAFileCatalog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_File_Catalog set ");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("FileAge=" + model.FileAge + ",");
            builder.Append("TimeLimit=" + model.TimeLimit + ",");
            builder.Append("ClassID=" + model.ClassID + ",");
            builder.Append("FileName='" + model.FileName + "',");
            builder.Append("Volume='" + model.Volume + "',");
            builder.Append("BoxNumber='" + model.BoxNumber + "',");
            builder.Append("FileNumber='" + model.FileNumber + "',");
            builder.Append("Principal='" + model.Principal + "',");
            builder.Append("SecretLevel=" + model.SecretLevel + ",");
            builder.Append("PrjCode='" + model.PrjCode + "',");
            builder.Append("PrjName='" + model.PrjName + "',");
            builder.Append("SavePlace='" + model.SavePlace + "',");
            builder.Append("PageNumber=" + model.PageNumber + ",");
            builder.Append("FileCode='" + model.FileCode + "',");
            builder.Append("PigeonholeDate='" + model.PigeonholeDate + "',");
            builder.Append("FileType='" + model.FileType + "',");
            builder.Append("Content='" + model.Content + "',");
            builder.Append("LendState='" + model.LendState + "',");
            builder.Append("IsValid='" + model.IsValid + "'");
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

