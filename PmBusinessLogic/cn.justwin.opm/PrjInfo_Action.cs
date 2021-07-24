namespace cn.justwin.opm
{
    using cn.justwin.opm.PM.prj_plan;
    using com.jwsoft.pm.data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PrjInfo_Action
    {
        public bool AddRefCorp(List<PrjInfoCorpModel> modellist, Guid prjguid)
        {
            if (modellist.Count == 0)
            {
                return true;
            }
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            int num = 0;
            int num2 = 0;
            foreach (PrjInfoCorpModel model in modellist)
            {
                builder.Append("insert into PT_PrjInfo_Corp(");
                builder.Append("UID,PrjGuid,CorpType,CorpId,CorpName,LinkMan,LinkTel,Remark,I_xh,IsValid,AddUser,AddTime)");
                builder.Append(" values (");
                builder.AppendFormat("@UID{0},@PrjGuid{0},@CorpType{0},@CorpId{0},@CorpName{0},@LinkMan{0},@LinkTel{0},@Remark{0},@I_xh{0},@IsValid{0},@AddUser{0},@AddTime{0})  ;  ", num);
                list.Add(new SqlParameter("@UID" + num.ToString(), SqlDbType.UniqueIdentifier, 0x10));
                list.Add(new SqlParameter("@PrjGuid" + num.ToString(), SqlDbType.UniqueIdentifier, 0x10));
                list.Add(new SqlParameter("@CorpType" + num.ToString(), SqlDbType.NChar, 10));
                list.Add(new SqlParameter("@CorpId" + num.ToString(), SqlDbType.Int, 4));
                list.Add(new SqlParameter("@CorpName" + num.ToString(), SqlDbType.NVarChar, 100));
                list.Add(new SqlParameter("@LinkMan" + num.ToString(), SqlDbType.NVarChar, 50));
                list.Add(new SqlParameter("@LinkTel" + num.ToString(), SqlDbType.NVarChar, 100));
                list.Add(new SqlParameter("@Remark" + num.ToString(), SqlDbType.NVarChar, 0x3e8));
                list.Add(new SqlParameter("@I_xh" + num.ToString(), SqlDbType.Int, 4));
                list.Add(new SqlParameter("@IsValid" + num.ToString(), SqlDbType.Char, 1));
                list.Add(new SqlParameter("@AddUser" + num.ToString(), SqlDbType.VarChar, 50));
                list.Add(new SqlParameter("@AddTime" + num.ToString(), SqlDbType.DateTime));
                list[num2].Value = Guid.NewGuid();
                list[1 + num2].Value = prjguid;
                list[2 + num2].Value = model.CorpType;
                list[3 + num2].Value = model.CorpId;
                list[4 + num2].Value = model.CorpName;
                list[5 + num2].Value = model.LinkMan;
                list[6 + num2].Value = model.LinkTel;
                list[7 + num2].Value = model.Remark;
                list[8 + num2].Value = model.I_xh;
                list[9 + num2].Value = model.IsValid;
                list[10 + num2].Value = model.AddUser;
                list[11 + num2].Value = DateTime.Now;
                num2 = (num + 1) * 12;
                num++;
            }
            return (publicDbOpClass.ExecuteNonQuery(CommandType.Text, GetTranscationStr(builder.ToString()), list.ToArray()) > 0);
        }

        public static string CorpType(string typecode)
        {
            string str = typecode;
            if (str == null)
            {
                return typecode;
            }
            if (!(str == "sjdw"))
            {
                if (str == "jldw")
                {
                    return "监理单位";
                }
                if (str == "sgdw")
                {
                    return "施工单位";
                }
                if (str == "jsdw")
                {
                    return "建设单位";
                }
                if (str == "yzdw")
                {
                    return "业主单位";
                }
                return typecode;
            }
            return "设计单位";
        }

        public DataTable fatherPrj_Bind()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select typeCode,prjName from pt_prjinfo where len(typeCode)=3 ");
            return publicDbOpClass.ExecuteDataTable(CommandType.Text, builder.ToString(), null);
        }

        public static DataTable GetPrjinfo(string prjid)
        {
            return publicDbOpClass.DataTableQuary(" select top 1 * from PT_PrjInfo WHERE PrjGuid='" + prjid + "'");
        }

        public static DataTable GetPrJinfoMap(string usercode)
        {
            string spName = "select PrjGuid,PrjCode,PrjName,PrjPlace,PrjStateName,OPM_Dept.podepom,OPM_Dept.DeptName,OPM_Dept.DeptID from vProject LEFT JOIN OPM_Dept ON  OPM_Dept.PrjId=vProject.PrjGuid  ";
            return publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, null);
        }

        public static DataTable GetPrjInfomember(string vyhdm)
        {
            string spName = string.Format(" select v_xm,v_yhdm from PT_yhmc where v_yhdm in ({0}) ", vyhdm);
            return publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, null);
        }

        public DataTable GetRefCorp(string prjguid)
        {
            string spName = " select *  from PT_PrjInfo_Corp where    PrjGuid=@PrjGuid ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjGuid", prjguid) };
            return publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters);
        }

        public DataTable GetTenderPrjReport(string whereStr, string userCode, int pageNo, int pageSize, string IsTender, ref int refRowCount)
        {
            DataTable table = new DataTable();
            string str = "TypeCode,Primit,i_ChildNum,PrjGuid,PrjCode,PrjName,StartDate,EndDate,PrjCost,Duration,Owner,PrjMangerName,PrjStateName,PrjKindName,PrjState,PrjPlace,AreaName,StoreCode,PlanOpeningDate";
            SqlParameter parameter = new SqlParameter("@rowCount", SqlDbType.Int) {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@isTender", IsTender), new SqlParameter("@columns", str), new SqlParameter("@condition", whereStr), new SqlParameter("@pageIndex", pageNo), new SqlParameter("@pageSize", pageSize), parameter };
            table = publicDbOpClass.ExecuteDataTable(CommandType.StoredProcedure, "uspGetProject", commandParameters);
            refRowCount = Convert.ToInt32(parameter.Value);
            return table;
        }

        public static string GetTranscationStr(string sql)
        {
            return ("begin try  begin transaction  " + sql + " COMMIT TRANSACTION end try begin catch rollback transaction end catch");
        }

        public string UpdateRecordDate(PrjInfoModel model)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_PrjInfo set TypeCode=@TypeCode+ ");
            builder.Append("(SELECT TypeCode FROM   PT_PrjInfo ppi WHERE  ppi.PrjGuid = @PrjGuid1 ),");
            builder.Append("RecordDate=@RecordDate");
            builder.Append(" where PrjGuid=@PrjGuid AND [IsValid]=1 ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjGuid1", SqlDbType.VarChar, 0x40), new SqlParameter("@PrjGuid", SqlDbType.VarChar, 0x40), new SqlParameter("@RecordDate", SqlDbType.DateTime), new SqlParameter("@TypeCode", SqlDbType.VarChar, 0x40) };
            commandParameters[0].Value = model.PrjGuid;
            commandParameters[1].Value = model.PrjGuid;
            if (!string.IsNullOrEmpty(model.RecordDate))
            {
                commandParameters[2].Value = model.RecordDate;
            }
            else
            {
                commandParameters[2].Value = DBNull.Value;
            }
            commandParameters[3].Value = model.TypeCode;
            str = publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters).ToString();
            if (str == "1")
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("UPDATE PT_PrjInfo SET  ");
                builder2.Append(" i_ChildNum = i_ChildNum +1  ");
                builder2.Append(" WHERE TypeCode =@TypeCode AND [IsValid]=1");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TypeCode", SqlDbType.VarChar, 0x40) };
                parameterArray2[0].Value = model.TypeCode;
                str = publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder2.ToString(), parameterArray2).ToString();
            }
            return str;
        }

        public bool UpdateRefCorp(List<PrjInfoCorpModel> modellist, string prjguid)
        {
            if (modellist.Count == 0)
            {
                return true;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from  PT_PrjInfo_Corp where PrjGuid='" + prjguid + "';");
            List<SqlParameter> list = new List<SqlParameter>();
            int num = 0;
            int num2 = 0;
            foreach (PrjInfoCorpModel model in modellist)
            {
                builder.Append("insert into PT_PrjInfo_Corp(");
                builder.Append("UID,PrjGuid,CorpType,CorpId,CorpName,LinkMan,LinkTel,Remark,I_xh,IsValid,AddUser,AddTime)");
                builder.Append(" values (");
                builder.AppendFormat("@UID{0},@PrjGuid{0},@CorpType{0},@CorpId{0},@CorpName{0},@LinkMan{0},@LinkTel{0},@Remark{0},@I_xh{0},@IsValid{0},@AddUser{0},@AddTime{0})  ;  ", num);
                list.Add(new SqlParameter("@UID" + num.ToString(), SqlDbType.UniqueIdentifier, 0x10));
                list.Add(new SqlParameter("@PrjGuid" + num.ToString(), SqlDbType.UniqueIdentifier, 0x10));
                list.Add(new SqlParameter("@CorpType" + num.ToString(), SqlDbType.NChar, 10));
                list.Add(new SqlParameter("@CorpId" + num.ToString(), SqlDbType.Int, 4));
                list.Add(new SqlParameter("@CorpName" + num.ToString(), SqlDbType.NVarChar, 100));
                list.Add(new SqlParameter("@LinkMan" + num.ToString(), SqlDbType.NVarChar, 50));
                list.Add(new SqlParameter("@LinkTel" + num.ToString(), SqlDbType.NVarChar, 100));
                list.Add(new SqlParameter("@Remark" + num.ToString(), SqlDbType.NVarChar, 0x3e8));
                list.Add(new SqlParameter("@I_xh" + num.ToString(), SqlDbType.Int, 4));
                list.Add(new SqlParameter("@IsValid" + num.ToString(), SqlDbType.Char, 1));
                list.Add(new SqlParameter("@AddUser" + num.ToString(), SqlDbType.VarChar, 50));
                list.Add(new SqlParameter("@AddTime" + num.ToString(), SqlDbType.DateTime));
                list[num2].Value = Guid.NewGuid();
                list[1 + num2].Value = new Guid(prjguid);
                list[2 + num2].Value = model.CorpType;
                list[3 + num2].Value = model.CorpId;
                list[4 + num2].Value = model.CorpName;
                list[5 + num2].Value = model.LinkMan;
                list[6 + num2].Value = model.LinkTel;
                list[7 + num2].Value = model.Remark;
                list[8 + num2].Value = model.I_xh;
                list[9 + num2].Value = model.IsValid;
                list[10 + num2].Value = model.AddUser;
                list[11 + num2].Value = DateTime.Now;
                num2 = (num + 1) * 12;
                num++;
            }
            return (publicDbOpClass.ExecuteNonQuery(CommandType.Text, GetTranscationStr(builder.ToString()), list.ToArray()) > 0);
        }

        public bool upDateStartItemByPrjGuid(PrjInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE [PT_PrjInfo] SET ");
            builder.Append(" [PrjState] = @PrjState,[ProjStartDate] = @ProjStartDate ");
            builder.Append(" ,[BusinessManager] = @BusinessManager,[StartManager] = @StartManager,[StartRemark] = @StartRemark ");
            builder.Append("  WHERE [PrjGuid] = @PrjGuid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjState", SqlDbType.Int, 4), new SqlParameter("@ProjStartDate", SqlDbType.DateTime), new SqlParameter("@BusinessManager", SqlDbType.VarChar, 100), new SqlParameter("@StartManager", SqlDbType.VarChar, 100), new SqlParameter("@StartRemark", SqlDbType.VarChar), new SqlParameter("@PrjGuid", SqlDbType.VarChar, 0x40) };
            commandParameters[0].Value = model.PrjState;
            commandParameters[1].Value = model.ProjStartDate;
            commandParameters[2].Value = model.BusinessManager;
            commandParameters[3].Value = model.StartManager;
            commandParameters[4].Value = model.StartRemark;
            commandParameters[5].Value = model.PrjGuid;
            return (publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) == 1);
        }
    }
}

