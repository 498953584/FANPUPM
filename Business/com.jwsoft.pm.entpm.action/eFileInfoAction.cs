namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class eFileInfoAction
    {
        public int Add(eFileInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_eFile_Info(");
            builder.Append("RecordType,CorpCode,PrjGuid,FileTitle,SubmitMan,SubmitDate,Remark,FileCode,ClassID,UserCode,RecordDate,SaveLimit,SecretLevel,FileType,FileCopy,FilePath,OriginalName");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordType + "',");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.PrjGuid + "',");
            builder.Append("'" + model.FileTitle + "',");
            builder.Append("'" + model.SubmitMan + "',");
            builder.Append("'" + model.SubmitDate + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.FileCode + "',");
            builder.Append(model.ClassID + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.SaveLimit + "',");
            builder.Append("'" + model.SecretLevel + "',");
            builder.Append("'" + model.FileType + "',");
            builder.Append("'" + model.FileCopy + "',");
            builder.Append("'" + model.FilePath + "',");
            builder.Append("'" + model.OriginalName + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_eFile_Info ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public string GetFileCode(string front)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select max(substring(FileCode,12,3))+1 from OA_eFile_Info where substring(FileCode,1,11) = '" + front + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString().PadLeft(3, '0');
            }
            return "001";
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,RecordType,CorpCode,PrjGuid,FileTitle,SubmitMan,SubmitDate,Remark,FileCode,ClassID,UserCode,RecordDate,SaveLimit,SecretLevel,FileType,FileCopy,FilePath,OriginalName ");
            builder.Append(" FROM OA_eFile_Info ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public eFileInfo GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" RecordID,RecordType,CorpCode,PrjGuid,FileTitle,SubmitMan,SubmitDate,Remark,FileCode,ClassID,UserCode,RecordDate,SaveLimit,SecretLevel,FileType,FileCopy,FilePath,OriginalName ");
            builder.Append(" from OA_eFile_Info ");
            builder.Append(" where RecordID=" + RecordID + " ");
            eFileInfo info = new eFileInfo();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                info.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            info.RecordType = set.Tables[0].Rows[0]["RecordType"].ToString();
            info.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            if (set.Tables[0].Rows[0]["PrjGuid"].ToString() != "")
            {
                info.PrjGuid = new Guid(set.Tables[0].Rows[0]["PrjGuid"].ToString());
            }
            info.FileTitle = set.Tables[0].Rows[0]["FileTitle"].ToString();
            info.SubmitMan = set.Tables[0].Rows[0]["SubmitMan"].ToString();
            if (set.Tables[0].Rows[0]["SubmitDate"].ToString() != "")
            {
                info.SubmitDate = DateTime.Parse(set.Tables[0].Rows[0]["SubmitDate"].ToString());
            }
            info.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            info.FileCode = set.Tables[0].Rows[0]["FileCode"].ToString();
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                info.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            info.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                info.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            info.SaveLimit = set.Tables[0].Rows[0]["SaveLimit"].ToString();
            info.SecretLevel = set.Tables[0].Rows[0]["SecretLevel"].ToString();
            info.FileType = set.Tables[0].Rows[0]["FileType"].ToString();
            info.FileCopy = set.Tables[0].Rows[0]["FileCopy"].ToString();
            info.FilePath = set.Tables[0].Rows[0]["FilePath"].ToString();
            info.OriginalName = set.Tables[0].Rows[0]["OriginalName"].ToString();
            return info;
        }

        public int ReturnUpdate(eFileInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Info set ");
            builder.Append("PrjGuid='" + model.PrjGuid + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("FileCode='" + model.FileCode + "',");
            builder.Append("ClassID=" + model.ClassID + ",");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("SaveLimit='" + model.SaveLimit + "',");
            builder.Append("SecretLevel='" + model.SecretLevel + "',");
            builder.Append("FileType='" + model.FileType + "',");
            if (model.FilePath != "0")
            {
                builder.Append("FilePath='" + model.FilePath + "',");
                builder.Append("OriginalName='" + model.OriginalName + "',");
            }
            builder.Append("FileCopy='" + model.FileCopy + "'");
            builder.Append(" where RecordID=" + model.RecordID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(eFileInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Info set ");
            builder.Append("FileTitle='" + model.FileTitle + "',");
            builder.Append("SubmitMan='" + model.SubmitMan + "',");
            builder.Append("SubmitDate='" + model.SubmitDate + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("FileCode='" + model.FileCode + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("SaveLimit='" + model.SaveLimit + "',");
            builder.Append("SecretLevel='" + model.SecretLevel + "',");
            builder.Append("FileType='" + model.FileType + "',");
            if (model.FilePath != "0")
            {
                builder.Append("FilePath='" + model.FilePath + "',");
                builder.Append("OriginalName='" + model.OriginalName + "',");
            }
            builder.Append("FileCopy='" + model.FileCopy + "'");
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

