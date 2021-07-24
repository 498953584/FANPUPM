namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AllocationAction
    {
        public static DataTable AllocationReportData(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            DataTable table = new DataTable();
            try
            {
                builder.Append(" select sa.acode,ress.resourcecode,ress.ResourceName,sa.aid, sas.scode,p9.ResourceTypeName,Specification,unit.Name,sas.sprice,sas.number,");
                builder.Append(" Convert(decimal(18,3),sas.sprice*sas.number) as total,");
                builder.Append("Bcorp.CorpName,sa.intime,sa.tcodea,sa.tcodeb ");
                builder.Append(",ress.TechnicalParameter,ress.ModelNumber,ress.Brand,TCodeA.tName AS TAName,TCodeB.TName TBName ");
                builder.Append("from Sm_Allocation as sa ");
                builder.Append("inner join Sm_Allocation_stock as sas on sa.acode=sas.acode ");
                builder.Append("left join XPM_Basic_ContactCorp as Bcorp on Bcorp.CorpID=sas.corp ");
                builder.Append("inner join res_resource as ress on ress.ResourceCode=sas.scode ");
                builder.Append("inner join Res_Unit as unit on unit.unitid=ress.unit ");
                builder.Append(" inner join Res_ResourceType as p9 on p9.ResourceTypeId = ress.ResourceType");
                builder.Append(" inner join dbo.Sm_Treasury as TCodeA on sa.tcodea=TCodeA.tcode ");
                builder.Append(" inner join dbo.Sm_Treasury as TCodeB on sa.tcodeb=TCodeB.tcode  ");
                builder.Append(" where flowstate=1 and isouta=1 and isinb=1 ");
                if (strWhere != "")
                {
                    builder.Append(strWhere);
                }
                builder.Append(" order by sa.intime desc");
                table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
            }
            catch
            {
            }
            return table;
        }

        public static int DelAllocationStock(string acode)
        {
            string cmdText = "delete sm_allocation_stock where asid in(" + acode + ")";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
        }

        public static int DelAllocationStockBill(string acode)
        {
            string cmdText = "delete from Sm_Allocation_Stock ";
            if (acode != "")
            {
                cmdText = cmdText + " where acode='" + acode + "' ";
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
        }

        public static int DelAllocationStockByAcode(SqlTransaction trans, string acode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete sm_allocation_stock where acode in(" + acode + ") ");
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), null);
        }

        public static int Delete(SqlTransaction trans, string acode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete sm_allocation_stock where acode in(" + acode + ") ");
            builder.Append("delete sm_allocation where acode in (" + acode + ")");
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), null);
        }

        public static object ExistData(string acode, string scode, string corp, decimal price)
        {
            string cmdText = "select count(*) from sm_allocation_stock where acode=@acode and scode=@scode and corp=@corp and sprice=@price";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@acode", SqlDbType.NVarChar, 0x40), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@price", SqlDbType.Decimal), new SqlParameter("@scode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = acode;
            commandParameters[1].Value = corp;
            commandParameters[2].Value = price;
            commandParameters[3].Value = scode;
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetAllocation_StockList(string strWhere)
        {
            string cmdText = "select * from sm_allocation as sma inner join sm_allocation_stock as sms on sma.acode=sms.acode ";
            if (strWhere != "")
            {
                cmdText = cmdText + " where " + strWhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static DataTable GetAllocationList(string strWhere)
        {
            string cmdText = "select aid,acode,flowstate,tcodea,tcodeb,isouta,isinb,person,intime,annx,explain,OutAllocationPerson,InAllocationPerson from sm_allocation ";
            if (strWhere != "")
            {
                cmdText = cmdText + " where " + strWhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static DataTable GetAllocationStockList(string depositoryid, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *,sprice*number Total,( ");
            builder.Append("\tselect sum(snumber) ");
            builder.Append("\tfrom Sm_Treasury_Stock as sts ");
            builder.Append("\twhere sts.tcode=allcinfo.tcodea and sts.sprice=allcinfo.sprice and sts.corp=allcinfo.corp and sts.scode=allcinfo.scode ");
            builder.Append("\tgroup by sts.tcode,sts.sprice,sts.corp,sts.scode ) as snumber ");
            builder.Append("from ( ");
            builder.Append("\tselect aid,sa.acode,flowstate,tcodea,tcodeb,isouta,isinb,person,intime,explain,outallocationperson,inallocationperson,asid,scode,sprice,corp,number  ");
            builder.Append("\tfrom sm_allocation as sa ");
            builder.Append("\tinner join sm_allocation_stock as sas");
            builder.Append("\ton sa.acode = sas.acode");
            builder.Append(") as allcinfo ");
            builder.Append("inner join res_resource as res on allcinfo.scode=res.ResourceCode ");
            builder.Append("left join XPM_Basic_ContactCorp as tbCorp on allcinfo.corp=tbcorp.corpid ");
            if (strWhere != "")
            {
                builder.Append(" where ");
                builder.Append(strWhere);
            }
            builder.Append(" ORDER BY ResourceCode DESC");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public static DataTable GetMaterialDetailsOfDeposity(string strWhere)
        {
            string cmdText = "select * from sm_treasury_stock ";
            if (strWhere != "")
            {
                cmdText = cmdText + " where " + strWhere + " ";
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static object GetMaterialNumberOfDepository(string depositoryId, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select number from (");
            builder.Append("select sum(snumber) as number,scode,sprice,corp ");
            builder.Append("from Sm_Treasury_Stock where tcode='" + depositoryId + "' ");
            builder.Append("group by scode,sprice,corp) as stockinfo ");
            builder.Append("inner join res_resource as res on stockinfo.scode=res.ResourceCode  ");
            if (strWhere != "")
            {
                builder.Append("where " + strWhere + " ");
            }
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null);
        }

        public static DataTable GetMaterialOfDepositoryList(string depositoryId, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from (");
            builder.Append("select sum(snumber) as snumber,scode,sprice,corp ");
            builder.Append("from Sm_Treasury_Stock where tcode='" + depositoryId + "' ");
            builder.Append("group by scode,sprice,corp) as stockinfo ");
            builder.Append("inner join res_resource as res on stockinfo.scode=res.ResourceCode ");
            builder.Append("left join XPM_Basic_ContactCorp as tbCorp on tbcorp.corpID=stockinfo.corp ");
            if (strWhere != "")
            {
                builder.Append(" where ");
                builder.Append(strWhere);
            }
            builder.Append("  ORDER BY ResourceCode DESC  ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public static DataTable GetTreasuryList(string tflag)
        {
            string cmdText = "select tid,tcode,tname,tflag,taddress,texplain from Sm_Treasury ";
            if (tflag != null)
            {
                cmdText = cmdText + " where tflag='" + tflag + "'";
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static DataTable GetTreasuryNameByCode(string code)
        {
            string cmdText = "select tname,tflag from sm_treasury where tcode=@tcode";
            SqlParameter parameter = new SqlParameter("@tcode", SqlDbType.NVarChar, 0x200) {
                Value = code
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public static object GetUserNameByCode(string code)
        {
            string cmdText = "select v_xm from pt_yhmc where v_yhdm=@yhdm and c_sfyx='y'";
            SqlParameter parameter = new SqlParameter("@yhdm", SqlDbType.VarChar, 8) {
                Value = code
            };
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public static int Insert(SqlTransaction trans, AllocationModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into Sm_Allocation(aid,acode,flowstate,tcodea,tcodeb,isouta,isinb,person,intime,annx,explain,OutAllocationPerson,InAllocationPerson, ");
            builder.Append("isouttime,isintime ) ");
            builder.Append(" values(@aid,@acode,@flowstate,@tcodea,@tcodeb,@isouta,@isinb,@person,");
            builder.Append("@intime,@annx,@explain,@OutAllocationPerson,@InAllocationPerson, ");
            builder.Append("@isOutTime,@isInTime) ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@acode", SqlDbType.NVarChar, 0x40), new SqlParameter("@flowstate", SqlDbType.Int), new SqlParameter("@tcodea", SqlDbType.NVarChar, 0x200), new SqlParameter("@tcodeb", SqlDbType.NVarChar, 0x200), new SqlParameter("@isouta", SqlDbType.Bit), new SqlParameter("@isinb", SqlDbType.Bit), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.VarChar, 0x10), new SqlParameter("@annx", SqlDbType.NVarChar, 0x80), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@OutAllocationPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InAllocationPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@aid", SqlDbType.NVarChar, 0x40), new SqlParameter("@isOutTime", SqlDbType.NVarChar, 0x40), new SqlParameter("@isInTime", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.Acode;
            commandParameters[1].Value = model.FlowState;
            commandParameters[2].Value = model.TCodea;
            commandParameters[3].Value = model.TCodeb;
            commandParameters[4].Value = model.IsOutA;
            commandParameters[5].Value = model.IsInB;
            commandParameters[6].Value = model.Person;
            commandParameters[7].Value = model.InTime;
            commandParameters[8].Value = model.Annex;
            commandParameters[9].Value = model.Explain;
            commandParameters[10].Value = model.OutAllocationPerson;
            commandParameters[11].Value = model.InAllocationPerson;
            commandParameters[12].Value = model.Aid;
            if (!string.IsNullOrEmpty(model.IsOutTime))
            {
                commandParameters[13].Value = model.IsOutTime;
            }
            else
            {
                commandParameters[13].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(model.IsInTime))
            {
                commandParameters[14].Value = model.IsInTime;
            }
            else
            {
                commandParameters[14].Value = DBNull.Value;
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int Insert(SqlTransaction trans, AllocationStockModel stockModel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into Sm_Allocation_Stock(asid,scode,acode,sprice,corp,number) ");
            builder.Append(" values(newid(),@scode,@acode,@sprice,@corp,@number) ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@acode", SqlDbType.NVarChar, 0x40), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@sprice", SqlDbType.Decimal), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal) };
            commandParameters[0].Value = stockModel.ACode;
            commandParameters[1].Value = stockModel.SCode;
            commandParameters[2].Value = stockModel.Sprice;
            commandParameters[3].Value = stockModel.Corp;
            commandParameters[4].Value = stockModel.Number;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int UpdataStatusOutDepo(string acode)
        {
            string cmdText = "update sm_allocation set isouta=@isouta where acode=@acode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@isouta", SqlDbType.Bit), new SqlParameter("@acode", SqlDbType.NVarChar, 0x200) };
            commandParameters[0].Value = true;
            commandParameters[1].Value = acode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static int Update(AllocationStockModel stockModel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Allocation_Stock set ");
            builder.Append("number=@number where asid=@asid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@number", SqlDbType.Int), new SqlParameter("@asid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = stockModel.Number;
            commandParameters[1].Value = stockModel.Asid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int Update(SqlTransaction trans, AllocationModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sm_allocation set ");
            builder.Append("tcodea=@tcodea,tcodeb=@tcodeb,");
            builder.Append("person=@person,intime=@intime,annx=@annx,explain=@explain,");
            builder.Append("OutAllocationPerson=@OutAllocationPerson,InAllocationPerson=@InAllocationPerson ");
            builder.Append("where acode=@acode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@tcodea", SqlDbType.NVarChar, 0x200), new SqlParameter("@tcodeb", SqlDbType.NVarChar, 0x200), new SqlParameter("@person", SqlDbType.NVarChar, 0x40), new SqlParameter("@intime", SqlDbType.VarChar, 30), new SqlParameter("@annx", SqlDbType.NVarChar, 0x63), new SqlParameter("@explain", SqlDbType.NVarChar, 0x800), new SqlParameter("@OutAllocationPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InAllocationPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@acode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.TCodea;
            commandParameters[1].Value = model.TCodeb;
            commandParameters[2].Value = model.Person;
            commandParameters[3].Value = model.InTime;
            commandParameters[4].Value = model.Annex;
            commandParameters[5].Value = model.Explain;
            commandParameters[6].Value = model.OutAllocationPerson;
            commandParameters[7].Value = model.InAllocationPerson;
            commandParameters[8].Value = model.Acode;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int UpdateNumberOfTreasury(string tsid, decimal num)
        {
            string cmdText = "update sm_treasury_stock set snumber=@snumber where tsid=@tsid";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@snumber", SqlDbType.Decimal), new SqlParameter("@tsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = num;
            commandParameters[1].Value = tsid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static int UpdateState(bool outState, bool inState, string acode, string isInOrOut)
        {
            string cmdText = "update sm_allocation set isouta=@isouta,isinb=@isinb, ";
            if (isInOrOut == "Out")
            {
                cmdText = cmdText + " isouttime=@time ";
            }
            if (isInOrOut == "In")
            {
                cmdText = cmdText + " isintime=@time ";
            }
            cmdText = cmdText + " where acode=@acode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@isouta", SqlDbType.Bit), new SqlParameter("@isinb", SqlDbType.Bit), new SqlParameter("@acode", SqlDbType.NVarChar, 0x40), new SqlParameter("@time", SqlDbType.DateTime) };
            commandParameters[0].Value = outState;
            commandParameters[1].Value = inState;
            commandParameters[2].Value = acode;
            commandParameters[3].Value = DateTime.Now;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
    }
}

