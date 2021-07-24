namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OAFileLendAction
    {
        public int Add(OAFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_File_Lend(");
            builder.Append("RecordID,BorrowMan,FileRecordID,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.RecordID + ",");
            builder.Append("'" + model.BorrowMan + "',");
            builder.Append(model.FileRecordID + ",");
            builder.Append("'" + model.LendDate + "',");
            builder.Append("'" + model.PlanReturnDate + "',");
            builder.Append("'" + model.ReturnApplyDate + "',");
            builder.Append("'" + model.ReturnDate + "',");
            builder.Append("'" + model.LendState + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Add(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < arr.Count; i++)
            {
                builder.Append(" if not exists(select top 1 RecordID from OA_File_Lend where RecordID='" + ((OAFileLend) arr[i]).RecordID + "') ");
                builder.Append(" begin ");
                builder.Append(" insert into OA_File_Lend( ");
                builder.Append(" BorrowMan,FileRecordID,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState ");
                builder.Append(" ) ");
                builder.Append(" values ( ");
                builder.Append(" '" + ((OAFileLend) arr[i]).BorrowMan + "', ");
                builder.Append(" " + ((OAFileLend) arr[i]).FileRecordID + ", ");
                builder.Append(" '" + ((OAFileLend) arr[i]).LendDate + "', ");
                builder.Append(" '" + ((OAFileLend) arr[i]).PlanReturnDate + "', ");
                builder.Append(" '" + ((OAFileLend) arr[i]).ReturnApplyDate + "', ");
                builder.Append(" '" + ((OAFileLend) arr[i]).ReturnDate + "',");
                builder.Append(" '" + ((OAFileLend) arr[i]).LendState + "' ");
                builder.Append(" ) ");
                builder.Append(" end ");
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_File_Lend ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_File_Lend where RecordID=" + RecordID);
            if (publicDbOpClass.ExecuteScalar(builder.ToString()) == DBNull.Value)
            {
                return false;
            }
            return true;
        }

        public int FileBack(int recordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_File_Lend set ");
            builder.Append(" ReturnDate='" + DateTime.Now + "', ");
            builder.Append(" LendState='3' ");
            builder.Append(" where RecordID='" + recordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int FileConfirm(int recordID, int intFlag, DateTime dtTime)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_File_Lend set ");
            if (intFlag == 3)
            {
                builder.Append(" ReturnApplyDate='" + dtTime + "', ");
            }
            else if (intFlag == 4)
            {
                builder.Append(" ReturnDate='" + dtTime + "', ");
            }
            builder.Append(" LendState='" + intFlag + "' ");
            builder.Append(" where RecordID='" + recordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetFileCatalogList(string strFileCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM V_OA_File_Catalog_Lend ");
            builder.Append(" where CHARINDEX(FileCode,'" + strFileCode + "')>0 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM V_OA_File_Lend ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_File_Lend", "RecordID");
        }

        public OAFileLend GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RecordID,BorrowMan,FileRecordID,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState ");
            builder.Append(" from OA_File_Lend ");
            builder.Append(" where RecordID=" + RecordID);
            OAFileLend lend = new OAFileLend();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                lend.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            lend.BorrowMan = set.Tables[0].Rows[0]["BorrowMan"].ToString();
            if (set.Tables[0].Rows[0]["FileRecordID"].ToString() != "")
            {
                lend.FileRecordID = int.Parse(set.Tables[0].Rows[0]["FileRecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["LendDate"].ToString() != "")
            {
                lend.LendDate = DateTime.Parse(set.Tables[0].Rows[0]["LendDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["PlanReturnDate"].ToString() != "")
            {
                lend.PlanReturnDate = DateTime.Parse(set.Tables[0].Rows[0]["PlanReturnDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["ReturnApplyDate"].ToString() != "")
            {
                lend.ReturnApplyDate = DateTime.Parse(set.Tables[0].Rows[0]["ReturnApplyDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["ReturnDate"].ToString() != "")
            {
                lend.ReturnDate = DateTime.Parse(set.Tables[0].Rows[0]["ReturnDate"].ToString());
            }
            lend.LendState = set.Tables[0].Rows[0]["LendState"].ToString();
            return lend;
        }

        public int Update(OAFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_File_Lend set ");
            builder.Append("LendDate='" + model.LendDate + "',");
            builder.Append("PlanReturnDate='" + model.PlanReturnDate + "',");
            builder.Append("ReturnApplyDate='" + model.ReturnApplyDate + "',");
            builder.Append("ReturnDate='" + model.ReturnDate + "',");
            builder.Append("LendState='" + model.LendState + "'");
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

