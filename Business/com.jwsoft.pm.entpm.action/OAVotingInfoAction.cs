namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAVotingInfoAction
    {
        public int Add(OAVotingInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Voting_Info(");
            builder.Append("UserCode,RecordDate,Title,Range,VoteType,Content,IsValid,State");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.Title + "',");
            builder.Append("'" + model.Range + "',");
            builder.Append("'" + model.VoteType + "',");
            builder.Append("'" + model.Content + "',");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("'" + model.State + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Voting_Record where VotingRecordID= " + RecordID);
            builder.Append(" delete OA_Voting_Info ");
            builder.Append(" where RecordID=" + RecordID + "and State in (0,2)");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Voting_Info where RecordID=" + RecordID);
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

        public static string getDept(int deptCode)
        {
            return Convert.ToString(publicDbOpClass.ExecuteScalar("select V_BMMC from pt_d_bm where i_bmdm = " + deptCode.ToString() + " and c_sfyx='y'"));
        }

        public static string getDeptName(string deptCode)
        {
            int length = 0;
            string str = "";
            string str2 = "";
            length = deptCode.IndexOf(",");
            if (length > 0)
            {
                while (length > 0)
                {
                    str2 = deptCode.Substring(0, length);
                    str = str + getDept(Convert.ToInt32(str2)) + ",";
                    deptCode = deptCode.Substring(length + 1, (deptCode.Length - length) - 1);
                    length = deptCode.IndexOf(",");
                }
                return (str + getDept(Convert.ToInt32(deptCode)));
            }
            return (str + getDept(Convert.ToInt32(deptCode)));
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_Voting_Info ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by RecordID ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAVotingInfo GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_Voting_Info ");
            builder.Append(" where RecordID=" + RecordID);
            OAVotingInfo info = new OAVotingInfo();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            info.RecordID = RecordID;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            info.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                info.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            info.Title = set.Tables[0].Rows[0]["Title"].ToString();
            info.Range = set.Tables[0].Rows[0]["Range"].ToString();
            info.VoteType = set.Tables[0].Rows[0]["VoteType"].ToString();
            info.Content = set.Tables[0].Rows[0]["Content"].ToString();
            info.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            info.State = set.Tables[0].Rows[0]["State"].ToString();
            return info;
        }

        public DataTable GetSelfVoting(string DeptCode)
        {
            string str;
            object obj2 = publicDbOpClass.ExecuteScalar("select v_bmbm from pt_d_bm where i_bmdm = " + DeptCode + " and c_sfyx = 'y'");
            if (obj2.ToString() == "")
            {
                str = " select * from OA_Voting_Info  where IsValid='y' and State='1' ";
            }
            else
            {
                str = " select * from OA_Voting_Info  where IsValid='y' and State='1' ";
                str = ((str + " and charindex('," + obj2.ToString().Substring(0, 2) + "',','+Range+',')>0 and Range <> ''") + " union ") + " select * from OA_Voting_Info  where IsValid='y' and State='1' " + " and  Range = ''";
            }
            return publicDbOpClass.DataTableQuary(str);
        }

        public int Update(OAVotingInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Voting_Info set ");
            builder.Append("Title='" + model.Title + "',");
            builder.Append("Range='" + model.Range + "',");
            builder.Append("VoteType='" + model.VoteType + "',");
            builder.Append("Content='" + model.Content + "'");
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateStart(int RecordID)
        {
            return publicDbOpClass.ExecSqlString(("update OA_Voting_Info set State='1' where RecordID=" + RecordID).ToString());
        }

        public int UpdateStop(int RecordID)
        {
            return publicDbOpClass.ExecSqlString(("update OA_Voting_Info set State='2' where RecordID=" + RecordID).ToString());
        }
    }
}

