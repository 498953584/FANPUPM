namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAVotingRecrodAction
    {
        public int Add(OAVotingRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Voting_Record(");
            builder.Append("VotingRecordID,Voter,VoteDate,SelectOptions");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.VotingRecordID + ",");
            builder.Append("'" + model.Voter + "',");
            builder.Append("'" + model.VoteDate + "',");
            builder.Append("'" + model.SelectOptions + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Voting_Record ");
            builder.Append(" where VotingRecordID=" + ID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int ID)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Voting_Record where VotingRecordID=" + ID);
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
            builder.Append("select * from OA_Voting_Record ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by VotingRecordID ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Voting_Record", "VotingRecordID");
        }

        public OAVotingRecord GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_Voting_Record ");
            builder.Append(" where VotingRecordID=" + ID);
            OAVotingRecord record = new OAVotingRecord();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            record.VotingRecordID = ID;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["VotingRecordID"].ToString() != "")
            {
                record.VotingRecordID = int.Parse(set.Tables[0].Rows[0]["VotingRecordID"].ToString());
            }
            record.Voter = set.Tables[0].Rows[0]["Voter"].ToString();
            if (set.Tables[0].Rows[0]["VoteDate"].ToString() != "")
            {
                record.VoteDate = DateTime.Parse(set.Tables[0].Rows[0]["VoteDate"].ToString());
            }
            record.SelectOptions = set.Tables[0].Rows[0]["SelectOptions"].ToString();
            return record;
        }

        public DataTable GetVotingRecordID(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select VotingRecordID from OA_Voting_Record ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(OAVotingRecord model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Voting_Record set ");
            builder.Append("VotingRecordID=" + model.VotingRecordID + ",");
            builder.Append("Voter='" + model.Voter + "',");
            builder.Append("VoteDate='" + model.VoteDate + "',");
            builder.Append("SelectOptions='" + model.SelectOptions + "'");
            builder.Append(" where VotingRecordID=" + model.VotingRecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

