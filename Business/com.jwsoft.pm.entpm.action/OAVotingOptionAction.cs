namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAVotingOptionAction
    {
        public int Add(OAVotingOption model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Voting_Option(");
            builder.Append("VotingInfoID,Options,Poll,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.VotingInfoID + ",");
            builder.Append("'" + model.Option + "',");
            builder.Append(model.Poll + ",");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Voting_Option ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Voting_Option where RecordID=" + RecordID);
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
            builder.Append("select * from OA_Voting_Option ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by RecordID ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_Voting_Option", "RecordID");
        }

        public OAVotingOption GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_Voting_Option ");
            builder.Append(" where RecordID=" + RecordID);
            OAVotingOption option = new OAVotingOption();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            option.RecordID = RecordID;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["VotingInfoID"].ToString() != "")
            {
                option.VotingInfoID = int.Parse(set.Tables[0].Rows[0]["VotingInfoID"].ToString());
            }
            option.Option = set.Tables[0].Rows[0]["Option"].ToString();
            if (set.Tables[0].Rows[0]["Poll"].ToString() != "")
            {
                option.Poll = int.Parse(set.Tables[0].Rows[0]["Poll"].ToString());
            }
            return option;
        }

        public DataTable GetPercentList(string strWhere)
        {
            int num = 0;
            DataTable table = publicDbOpClass.DataTableQuary("select sum(Poll) as zh from OA_Voting_Option where " + strWhere);
            if (table.Rows[0]["zh"] != DBNull.Value)
            {
                num = Convert.ToInt32(table.Rows[0]["zh"]);
            }
            if (num != 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("select RecordID,VotingInfoID, Options,Poll,PPercent=");
                builder.Append("convert(varchar,(convert(numeric(18,2),convert(numeric(18,2),Poll)/(select sum(Poll) from OA_Voting_Option where " + strWhere + ")*100)))+'%' ");
                builder.Append(" from OA_Voting_Option ");
                if (strWhere.Trim() != "")
                {
                    builder.Append(" where " + strWhere);
                }
                builder.Append(" order by RecordID ");
                return publicDbOpClass.DataTableQuary(builder.ToString());
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("select RecordID,VotingInfoID, Options,Poll,PPercent=");
            builder2.Append("'0.00% '");
            builder2.Append(" from OA_Voting_Option ");
            if (strWhere.Trim() != "")
            {
                builder2.Append(" where " + strWhere);
            }
            builder2.Append(" order by RecordID ");
            return publicDbOpClass.DataTableQuary(builder2.ToString());
        }

        public int Update(OAVotingOption model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Voting_Option set ");
            builder.Append("Options='" + model.Option + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdatePoll(string strWhere)
        {
            string str = "update OA_Voting_Option set ";
            return publicDbOpClass.ExecSqlString((str + " Poll=Poll+1") + " where " + strWhere);
        }
    }
}

