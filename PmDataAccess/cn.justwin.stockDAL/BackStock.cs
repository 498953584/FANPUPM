namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BackStock
    {
        public int Add(SqlTransaction trans, BackStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_back_Stock(");
            builder.Append("rsid,rcode,scode,number,sprice,corp,intype,linkcode,TaskId)");
            builder.Append(" values (");
            builder.Append("@rsid,@rcode,@scode,@number,@sprice,@corp,@intype,@linkcode,@TaskId)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rsid", SqlDbType.NVarChar, 50), new SqlParameter("@rcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@intype", SqlDbType.NVarChar, 0x40), new SqlParameter("@linkcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaskId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.rsid;
            commandParameters[1].Value = model.rcode;
            commandParameters[2].Value = model.scode;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.sprice;
            commandParameters[5].Value = model.corp;
            commandParameters[6].Value = model.intype;
            commandParameters[7].Value = model.linkcode;
            commandParameters[8].Value = model.taskId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string rsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_back_Stock ");
            builder.Append(" where rsid=@rsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = rsid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_back_Stock ");
            builder.Append(strWhere);
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<BackStockModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select rsid,rcode,scode,number,sprice,corp,intype,linkcode ");
            builder.Append(" FROM Sm_back_Stock ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<BackStockModel> list = new List<BackStockModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public BackStockModel GetModel(string rsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select rsid,rcode,scode,number,sprice,corp,intype,linkcode from Sm_back_Stock ");
            builder.Append(" where rsid=@rsid ");
            BackStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@rsid", rsid) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public string GetNumByParam(string ocorde, decimal sprice, string scode, string corp, string rcode, string flowstate)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sum(number) as number from dbo.Sm_back_Stock as p1");
            builder.Append(" join dbo.Sm_OutReserve as p2 on linkcode=p2.orcode");
            builder.Append(" where p1.linkcode='" + ocorde + "'");
            builder.Append(" and p1.sprice='" + sprice + "'");
            builder.Append(" and p1.scode='" + scode + "'");
            builder.Append(" and p1.corp='" + corp + "'");
            builder.Append(" and p1.rcode !='" + rcode + "'");
            builder.Append(" and p2.flowstate !='" + flowstate + "'");
            return Convert.ToString(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[0]));
        }

        public DataTable GetTableByParam(string startTime, string endTime, string rcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string corpName, string category, string specification, string brand, string modelNumber)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p1.rcode,p5.rid,p4.Name,p2.Specification,p1.sprice,p7.corpName,p9.ResourceTypeName,");
            builder.Append("P2.TechnicalParameter,P2.ModelNumber,P2.Brand,");
            builder.Append(" ISNULL(p6.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,p2.resourceCode,p2.ResourceName,p1.number,p8.tname,");
            builder.Append("  Convert(decimal(18,3),(p1.sprice*p1.number)) as xjsprice,p5.intime from dbo.Sm_back_Stock as p1");
            builder.Append(" inner join Res_Resource as p2 on p1.scode=p2.resourceCode ");
            builder.Append(" inner join Res_Unit as p4 on p2.Unit=p4.UnitID");
            builder.Append(" inner join dbo.Sm_Refunding as p5 on p1.rcode=p5.rcode and p5.isin=1");
            builder.Append(" left join dbo.PT_PrjInfo as p6 on p5.procode=p6.prjGuid");
            builder.Append(" left join PT_PrjInfo_ZTB on p5.procode=PT_PrjInfo_ZTB.prjGuid");
            builder.Append(" left join dbo.XPM_Basic_ContactCorp as p7 on p1.corp=p7.CorpId");
            builder.Append(" inner join dbo.Sm_Treasury as p8 on p5.tcode=p8.tcode");
            builder.Append(" inner join Res_ResourceType as p9 on p9.ResourceTypeId = p2.ResourceType");
            builder.Append(" where p5.flowstate=1");
            if (!string.IsNullOrEmpty(rcode))
            {
                builder.Append(" and p1.rcode like @rcode");
                list.Add(new SqlParameter("@rcode", '%' + rcode.Trim() + '%'));
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append(" and ISNULL(p6.prjName,PT_PrjInfo_ZTB.prjName) like @prjName");
                list.Add(new SqlParameter("@prjName", '%' + prjName.Trim() + '%'));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p5.intime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p5.intime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (!string.IsNullOrEmpty(corpName))
            {
                builder.Append(" and p7.corpName like @corpName");
                list.Add(new SqlParameter("@corpName", '%' + corpName.Trim() + '%'));
            }
            if (!string.IsNullOrEmpty(category))
            {
                builder.Append(" and p9.ResourceTypeName like @ResourceTypeName");
                list.Add(new SqlParameter("@ResourceTypeName", '%' + category + '%'));
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.Append(" and p2.Specification like @specification");
                list.Add(new SqlParameter("@specification", '%' + specification + '%'));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.Append(" and P2.Brand like @brand");
                list.Add(new SqlParameter("@brand", '%' + brand + '%'));
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.Append(" and P2.ModelNumber like @modelNumber");
                list.Add(new SqlParameter("@modelNumber", '%' + modelNumber + '%'));
            }
            for (int i = 0; i < ResourceNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(ResourceNames[i]))
                {
                    num = 1;
                    if (i == 0)
                    {
                        builder.Append(" and (p2.ResourceName like @ResourceName" + i);
                    }
                    else
                    {
                        builder.Append(" or p2.ResourceName like @ResourceName" + i);
                    }
                    list.Add(new SqlParameter("@ResourceName" + i, "%" + ResourceNames[i].Trim() + "%"));
                }
            }
            if (num == 1)
            {
                builder.Append(")");
                num = 0;
            }
            for (int j = 0; j < ResourceCodes.Length; j++)
            {
                if (!string.IsNullOrEmpty(ResourceCodes[j]))
                {
                    num = 1;
                    if (j == 0)
                    {
                        builder.Append(" and (p2.resourceCode like @ResourceCode" + j);
                    }
                    else
                    {
                        builder.Append(" or p2.resourceCode like @ResourceCode" + j);
                    }
                    list.Add(new SqlParameter("@ResourceCode" + j, "%" + ResourceCodes[j].Trim() + "%"));
                }
            }
            if (num == 1)
            {
                builder.Append(")");
                num = 0;
            }
            builder.Append(" order by p5.intime desc ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetTableByRcode(string rcode)
        {
            string cmdText = "\r\n                    WITH cteBudTask AS \r\n                    (\r\n\t                    SELECT TaskId,TaskCode,TaskName\r\n\t                    FROM Bud_Task\t\r\n                    ),cteBudModify AS \r\n                    (\r\n\t                    SELECT modifyTask.ModifyTaskId AS TaskId,ModifyTaskCode AS TaskCode,ModifyTaskContent AS TaskName\r\n\t                    FROM Bud_ModifyTask modifyTask\t\r\n\t                    JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                    WHERE ModifyType=0 AND FlowState=1 \r\n                    ),cteTask AS\r\n                    (\r\n\t                    SELECT TaskId,TaskCode,TaskName FROM cteBudTask\r\n\t                    UNION\r\n\t                    SELECT TaskId,TaskCode,TaskName FROM cteBudModify\r\n                    )\r\n                    SELECT DISTINCT p2.rsid AS orsid,p2.scode,p6.orcode,p2.sprice,(p2.number*p2.sprice) Total,\r\n                    p6.number AS number,p2.number AS tnumber,p2.Corp AS CorpId, p7.CorpName AS Corp,\r\n                    p3.ResourceName,p3.Specification, p3.Brand,ModelNumber,TechnicalParameter, p5.Name,\r\n                    p2.TaskId,ISNULL(cteTask.TaskCode,'') TaskCode,ISNULL(cteTask.TaskName,'') TaskName \r\n                    FROM dbo.Sm_Refunding AS p1 \r\n                    LEFT JOIN dbo.Sm_back_Stock AS p2 ON p1.rcode=p2.rcode \r\n                    INNER JOIN Res_Resource AS p3 ON p2.scode=p3.resourceCode \r\n                    LEFT JOIN Res_Unit AS p5 on p3.Unit=p5.UnitID \r\n                    LEFT JOIN dbo.Sm_out_Stock AS p6 ON p6.orcode=p2.linkcode AND p2.scode=p6.scode AND p2.sprice=p6.sprice AND p2.corp=p6.corp \r\n                    LEFT JOIN dbo.XPM_Basic_ContactCorp AS p7 ON p2.corp=p7.CorpId  \r\n                    LEFT JOIN cteTask ON p2.TaskId = cteTask.TaskId ";
            cmdText = (cmdText + "WHERE p2.rcode IN('" + rcode + "') ") + "ORDER BY scode DESC";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public BackStockModel ReaderBind(IDataReader dataReader)
        {
            BackStockModel model = new BackStockModel {
                rsid = dataReader["rsid"].ToString(),
                rcode = dataReader["rcode"].ToString(),
                scode = dataReader["scode"].ToString()
            };
            object obj2 = dataReader["number"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.number = (decimal) obj2;
            }
            obj2 = dataReader["sprice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.sprice = (decimal) obj2;
            }
            model.corp = dataReader["corp"].ToString();
            model.intype = dataReader["intype"].ToString();
            model.linkcode = dataReader["linkcode"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, BackStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_back_Stock set ");
            builder.Append("rcode=@rcode,");
            builder.Append("scode=@scode,");
            builder.Append("number=@number,");
            builder.Append("sprice=@sprice,");
            builder.Append("corp=@corp,");
            builder.Append("intype=@intype,");
            builder.Append("linkcode=@linkcode");
            builder.Append(" where rsid=@rsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rsid", SqlDbType.NVarChar, 50), new SqlParameter("@rcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@intype", SqlDbType.NVarChar, 0x40), new SqlParameter("@linkcode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.rsid;
            commandParameters[1].Value = model.rcode;
            commandParameters[2].Value = model.scode;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.sprice;
            commandParameters[5].Value = model.corp;
            commandParameters[6].Value = model.intype;
            commandParameters[7].Value = model.linkcode;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

