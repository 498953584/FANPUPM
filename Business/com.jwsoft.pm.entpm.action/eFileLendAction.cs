namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class eFileLendAction
    {
        public int Add(eFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_eFile_Lend(");
            builder.Append("RecordID,AuditState,BorrowMan,FileRecordID,PlanReturnDate,LendState,ReturnType");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.BorrowMan + "',");
            builder.Append(model.FileRecordID + ",");
            builder.Append("'" + model.PlanReturnDate + "',");
            builder.Append("'" + model.LendState + "',");
            builder.Append(model.ReturnType);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int BorrowUpdate(eFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Lend set ");
            builder.Append("LendDate='" + model.LendDate + "',");
            builder.Append("LendState='" + model.LendState + "'");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int CompelReturnUpdate(eFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Lend set ");
            builder.Append("LendDate='" + model.LendDate + "',");
            builder.Append("LendState='" + model.LendState + "',");
            builder.Append("ReturnType='" + model.ReturnType + "'");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_eFile_Lend ");
            builder.Append(" where RecordID='" + RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,BorrowMan,FileRecordID,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState ");
            builder.Append(" FROM OA_eFile_Lend ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public eFileLend GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(" RecordID,AuditState,BorrowMan,FileRecordID,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState ");
            builder.Append(" from OA_eFile_Lend ");
            builder.Append(" where RecordID='" + RecordID + "'");
            eFileLend lend = new eFileLend();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                lend.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                lend.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
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

        public int LendStateUpdate(eFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Lend set ");
            builder.Append("LendState='" + model.LendState + "'");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int ReturnUpdate(eFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Lend set ");
            builder.Append("ReturnDate='" + model.ReturnDate + "',");
            builder.Append("LendState='" + model.LendState + "',");
            builder.Append("ReturnType='" + model.ReturnType + "'");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(eFileLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_eFile_Lend set ");
            builder.Append("AuditState=" + model.AuditState + ",");
            builder.Append("BorrowMan='" + model.BorrowMan + "',");
            builder.Append("FileRecordID=" + model.FileRecordID + ",");
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

