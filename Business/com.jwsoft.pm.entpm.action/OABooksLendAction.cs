namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OABooksLendAction
    {
        public int Add(OABooksLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Books_Lend(");
            builder.Append("RecordID,BorrowMan,ISBN,BookTitle,Copy,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.RecordID + ",");
            builder.Append("'" + model.BorrowMan + "',");
            builder.Append("'" + model.ISBN + "',");
            builder.Append("'" + model.BookTitle + "',");
            builder.Append(model.Copy + ",");
            builder.Append("'" + model.LendDate + "',");
            builder.Append("'" + model.PlanReturnDate + "',");
            builder.Append("'" + model.ReturnApplyDate + "',");
            builder.Append("'" + model.ReturnDate + "',");
            builder.Append("'" + model.LendState + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int AddData(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            for (int i = 0; i < arr.Count; i++)
            {
                builder.Append(" insert into OA_Books_Lend(");
                builder.Append("BorrowMan,ISBN,BookTitle,Copy,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState");
                builder.Append(") ");
                builder.Append(" values (");
                builder.Append(" '" + ((OABooksLend) arr[i]).BorrowMan + "', ");
                builder.Append(" '" + ((OABooksLend) arr[i]).ISBN + "',");
                builder.Append(" '" + ((OABooksLend) arr[i]).BookTitle + "', ");
                builder.Append(" " + ((OABooksLend) arr[i]).Copy + ",");
                builder.Append(" '" + ((OABooksLend) arr[i]).LendDate + "', ");
                builder.Append(" '" + ((OABooksLend) arr[i]).PlanReturnDate + "',");
                builder.Append(" '" + ((OABooksLend) arr[i]).ReturnApplyDate + "', ");
                builder.Append(" '" + ((OABooksLend) arr[i]).ReturnDate + "', ");
                builder.Append(" '" + ((OABooksLend) arr[i]).LendState + "' ");
                builder.Append(")");
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int BookBack(int recordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_Books_Lend set ");
            builder.Append(" ReturnApplyDate='" + DateTime.Now + "', ");
            builder.Append(" LendState='5' ");
            builder.Append(" where RecordID='" + recordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int BookConfirm(int recordID, string strFlag)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_Books_Lend set ");
            builder.Append(" ReturnDate='" + DateTime.Now + "', ");
            builder.Append(" LendState='" + strFlag + "' ");
            builder.Append(" where RecordID='" + recordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int BookRenewLendApply(int recordID, DateTime dateRenew, string remark)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_Books_Lend set ");
            builder.Append(" PlanReturnDate='" + dateRenew + "', ");
            builder.Append(" LendState='3', ");
            builder.Append(" Content='" + remark + "' ");
            builder.Append(" where RecordID='" + recordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Books_Lend ");
            builder.Append(" where RecordID=" + ID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 ID from OA_Books_Lend where RecordID=" + ID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string UserCode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(((str + "SELECT     a.RecordID, a.BorrowMan, a.ISBN, a.BookTitle, a.Copy, a.LendDate, a.PlanReturnDate, a.ReturnApplyDate, a.ReturnDate, a.LendState, a.[Content], " + " c.Manager FROM dbo.OA_Books_Lend AS a INNER JOIN dbo.OA_Books_Storage AS b ON a.ISBN = b.ISBN INNER JOIN ") + " dbo.OA_Books_Library AS c ON b.LibraryCode = c.LibraryCode and Manager = '" + UserCode + "'").ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Books_Lend", "RecordID");
        }

        public OABooksLend GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RecordID,BorrowMan,ISBN,BookTitle,Copy,LendDate,PlanReturnDate,ReturnApplyDate,ReturnDate,LendState ");
            builder.Append(" from OA_Books_Lend ");
            builder.Append(" where RecordID=" + ID);
            OABooksLend lend = new OABooksLend();
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
            lend.ISBN = set.Tables[0].Rows[0]["ISBN"].ToString();
            lend.BookTitle = set.Tables[0].Rows[0]["BookTitle"].ToString();
            if (set.Tables[0].Rows[0]["Copy"].ToString() != "")
            {
                lend.Copy = int.Parse(set.Tables[0].Rows[0]["Copy"].ToString());
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

        public int Update(OABooksLend model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Books_Lend set ");
            builder.Append("RecordID=" + model.RecordID + ",");
            builder.Append("BorrowMan='" + model.BorrowMan + "',");
            builder.Append("ISBN='" + model.ISBN + "',");
            builder.Append("BookTitle='" + model.BookTitle + "',");
            builder.Append("Copy=" + model.Copy + ",");
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

