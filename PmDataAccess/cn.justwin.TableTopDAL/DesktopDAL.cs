namespace cn.justwin.TableTopDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class DesktopDAL
    {
        public int Add(DeskTopDalModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into frame_Desktop_UserModel(");
            builder.Append("userCode,ModelId,orderId,MNewName)");
            builder.Append(" values (");
            builder.Append("@userCode,@ModelId,@orderId,@MNewName)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50), new SqlParameter("@ModelId", SqlDbType.VarChar, 50), new SqlParameter("@orderId", SqlDbType.Int, 4), new SqlParameter("@MNewName", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = model.userCode;
            commandParameters[1].Value = model.ModelId;
            commandParameters[2].Value = model.orderId;
            commandParameters[3].Value = model.MNewName;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int AddMenu(menuModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into frame_Desktop_Menulink(");
            builder.Append("userCode,ModelId,orderId,MNewName,addTime,sequence)");
            builder.Append(" values (");
            builder.Append("@userCode,@ModelId,@orderId,@MNewName,@addTime,@sequence)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50), new SqlParameter("@ModelId", SqlDbType.VarChar, 50), new SqlParameter("@orderId", SqlDbType.Int, 4), new SqlParameter("@MNewName", SqlDbType.VarChar, 100), new SqlParameter("@addTime", SqlDbType.DateTime, 20), new SqlParameter("@sequence", SqlDbType.VarChar, 2) };
            commandParameters[0].Value = model.userCode;
            commandParameters[1].Value = model.ModelId;
            commandParameters[2].Value = model.orderId;
            commandParameters[3].Value = model.MNewName;
            commandParameters[4].Value = model.AddTime;
            commandParameters[5].Value = model.Sequence;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable allModel(string userCode, string code, string name)
        {
            string str = "select a.v_yhdm,a.c_mkdm,p.v_mkmc from PT_YHMC_Privilege as a inner join pt_mk as p on a.c_mkdm=p.c_mkdm where i_childNum=0 and v_cdlj!='' and v_yhdm='" + userCode + "'  and a.c_mkdm not in (select modelId from frame_Desktop_Menulink where userCode='" + userCode + "')  ";
            if (code != "")
            {
                str = str + " and a.c_mkdm like '%" + code + "%'";
            }
            if (name != "")
            {
                str = str + " and p.v_mkmc like '%" + name + "%'";
            }
            str = str + "  order by c_mkdm";
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public int Delete(string userCode, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from frame_Desktop_UserModel ");
            builder.Append(" where userCode=@userCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = userCode;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteModel(string userCode, string ModelId, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from frame_Desktop_UserModel ");
            builder.Append(" where userCode=@userCode and modelid=@modelid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50), new SqlParameter("@modelid", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = userCode;
            commandParameters[1].Value = ModelId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteWebAddr(string userCode, string LinkId, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from frame_Desktop_Weblink ");
            builder.Append(" where userCode=@userCode and LinkId=@LinkId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50), new SqlParameter("@LinkId", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = userCode;
            commandParameters[1].Value = LinkId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DelMenu(string userCode, string modelId, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from frame_Desktop_Menulink ");
            builder.Append(" where userCode=@userCode and modelId=@modelId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50), new SqlParameter("@modelId", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = userCode;
            commandParameters[1].Value = modelId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable getCheckModel(string userCode)
        {
            string str = string.Format("\r\n\t\t\t\tselect f.*,case f.ModelId \r\n\t\t\t\t\twhen '280305' \r\n\t\t\t\t\t\tthen '公告管理' \r\n\t\t\t\t\t\telse p.v_mkmc \r\n\t\t\t\t\tend as v_mkmc \r\n\t\t\t\tfrom frame_Desktop_UserModel as f \r\n\t\t\t\tinner join pt_mk as p on p.c_mkdm=f.modelid  \r\n\t\t\t\twhere userCode='{0}' and ModelId <> '780501'\r\n\t\t\t\torder by orderId, c_mkdm ASC\r\n\t\t\t", userCode);
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public DataTable getModel(string userCode)
        {
            string str = ("select a.v_yhdm,a.c_mkdm,case a.c_mkdm when '280305' then '公告管理' else p.v_mkmc end as v_mkmc from PT_YHMC_Privilege as a inner join pt_mk as p on a.c_mkdm=p.c_mkdm inner join dbo.frame_Desktop_ModelInfo as f on a.c_mkdm=f.modelID where v_yhdm='" + userCode + "'  and a.c_mkdm not in ( SELECT [ModelId] FROM [frame_Desktop_UserModel] where [userCode]='" + userCode + "')  ") + "  order by c_mkdm";
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public DataTable GetUserModelInfo(string userCode)
        {
            string str = " select m.ModelID, m.tableName, m.colName, m.colTime, m.selWhere, m.moreSrc, m.nameSrc, m.colId ,u.MNewName";
            str = (str + " from frame_Desktop_ModelInfo as m left join frame_Desktop_UserModel as u on m.modelid=u.modelid") + " where u.usercode='" + userCode + "' order by  u.orderid asc";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
            if (table.Rows.Count <= 0)
            {
                string cmdText = "insert into frame_Desktop_UserModel(userCode, ModelId, orderId, MNewName)";
                cmdText = cmdText + " select @userCode, ModelId, orderId, MNewName from frame_Desktop_UserModel where userCode='default'";
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50) };
                commandParameters[0].Value = userCode;
                SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
                str = " select m.ModelID, m.tableName, m.colName, m.colTime, m.selWhere, m.moreSrc, m.nameSrc, m.colId ,u.MNewName";
                table = SqlHelper.ExecuteQuery(CommandType.Text, (str + " from frame_Desktop_ModelInfo as m left join frame_Desktop_UserModel as u on m.modelid=u.modelid" + " where u.usercode='default' order by  u.orderid asc").ToString(), null);
            }
            return table;
        }

        public DataTable GetUserModelInfo(string userCode, string ModelId)
        {
            string cmdText = "select * from dbo.frame_Desktop_UserModel where userCode='" + userCode + "' and modelid='" + ModelId + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable GetUserSet(string userCode, string ault)
        {
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, ("select * from dbo.frame_Desktop_UserSet where userCode='" + userCode + "'").ToString(), null);
            if (table.Rows.Count >= 1)
            {
                return table;
            }
            // TODO 1、进制是否有问题
            if (Convert.ToInt32(ault) < 0x4b0)
            {
                ault = "defaultold";
            }
            else if ((Convert.ToInt32(ault) >= 0x4b0) && (Convert.ToInt32(ault) <= 0x5dc))
            {
                ault = "default";
            }
            else
            {
                ault = "defaultNew";
            }
            string cmdText = "select * from dbo.frame_Desktop_UserSet where userCode='" + ault + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable getWebLinkList(string userCode)
        {
            string str = ("select * from frame_Desktop_weblink  where userCode='" + userCode + "'") + "  order by orderId ASC";
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public int Update(DeskTopDalModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update frame_Desktop_UserModel");
            builder.Append(" set orderId=@orderId,MNewName=@MNewName ");
            builder.Append(" where userCode=@userCode and ModelId=@ModelId");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50), new SqlParameter("@ModelId", SqlDbType.VarChar, 50), new SqlParameter("@orderId", SqlDbType.Int, 4), new SqlParameter("@MNewName", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = model.userCode;
            commandParameters[1].Value = model.ModelId;
            commandParameters[2].Value = model.orderId;
            commandParameters[3].Value = model.MNewName;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int updateMenuOrderID(string orderID)
        {
            int num = 0;
            string[] strArray = orderID.Split(new char[] { '|' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].ToString() != "")
                {
                    string[] strArray2 = strArray[i].ToString().Split(new char[] { ':' });
                    using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            string cmdText = "UPDATE [frame_Desktop_Menulink] SET [orderId] = " + strArray2[0].ToString() + " WHERE  [ModelId] = " + strArray2[1].ToString();
                            num = SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            return num;
        }

        public int updateOrderID(string orderID, string usercode)
        {
            int num = 0;
            string[] strArray = orderID.Split(new char[] { '|' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].ToString() != "")
                {
                    string[] strArray2 = strArray[i].ToString().Split(new char[] { ':' });
                    using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            string cmdText = "UPDATE [frame_Desktop_UserModel] SET [orderId] = " + strArray2[0].ToString() + " WHERE  userCode='" + usercode + "' AND [ModelId] = " + strArray2[1].ToString();
                            num = SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            return num;
        }

        public int UpdateWeblink(Desktop_Weblink model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update frame_Desktop_Weblink set ");
            builder.Append("orderId=@orderId,");
            builder.Append("WebName=@WebName,");
            builder.Append("WebAddr=@WebAddr,");
            builder.Append("Remark=@Remark");
            builder.Append(" where LinkID=@LinkID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@LinkID", SqlDbType.Int, 4), new SqlParameter("@orderId", SqlDbType.Int, 4), new SqlParameter("@WebName", SqlDbType.NVarChar, 50), new SqlParameter("@WebAddr", SqlDbType.NVarChar, 100), new SqlParameter("@Remark", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = model.LinkID;
            commandParameters[1].Value = model.orderId;
            commandParameters[2].Value = model.WebName;
            commandParameters[3].Value = model.WebAddr;
            commandParameters[4].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int UpdateWeblinkByLinkID(string _orderId, string usercode)
        {
            int num = 0;
            string[] strArray = _orderId.Split(new char[] { '|' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].ToString() != "")
                {
                    string[] strArray2 = strArray[i].ToString().Split(new char[] { ':' });
                    using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            string cmdText = "UPDATE [frame_Desktop_Weblink] SET [orderId] = " + strArray2[0].ToString() + " WHERE  userCode='" + usercode + "' AND  [LinkID] = " + strArray2[1].ToString();
                            num = SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            return num;
        }

        public int UpdMenu(menuModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update frame_Desktop_Menulink");
            builder.Append(" set orderId=@orderId,MNewName=@MNewName,sequence=@sequence ");
            builder.Append(" where userCode=@userCode and ModelId=@ModelId");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.VarChar, 50), new SqlParameter("@ModelId", SqlDbType.VarChar, 50), new SqlParameter("@orderId", SqlDbType.Int, 4), new SqlParameter("@MNewName", SqlDbType.VarChar, 100), new SqlParameter("@sequence", SqlDbType.VarChar, 2) };
            commandParameters[0].Value = model.userCode;
            commandParameters[1].Value = model.ModelId;
            commandParameters[2].Value = model.orderId;
            commandParameters[3].Value = model.MNewName;
            commandParameters[4].Value = model.Sequence;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

