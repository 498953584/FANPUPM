namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SmWastageStock
    {
        public int Add(SqlTransaction trans, SmWastageStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Wastage_Stock(");
            builder.Append("WastageStockId,WastageCode,ResourceCode,Sprice,Number,Corp)");
            builder.Append(" values (");
            builder.Append("@WastageStockId,@WastageCode,@ResourceCode,@Sprice,@Number,@Corp)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@WastageStockId", SqlDbType.NVarChar, 50), new SqlParameter("@WastageCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50), new SqlParameter("@Sprice", SqlDbType.Decimal, 9), new SqlParameter("@Number", SqlDbType.Decimal, 9), new SqlParameter("@Corp", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.WastageCode;
            commandParameters[2].Value = model.ResourceCode;
            commandParameters[3].Value = model.Sprice;
            commandParameters[4].Value = model.Number;
            commandParameters[5].Value = model.Corp;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string wastageStockId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Wastage_Stock ");
            builder.Append(" where WastageStockId=@WastageStockId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@WastageStockId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = wastageStockId;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Wastage_Stock ");
            builder.Append(strWhere);
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<SmWastageStockModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WastageStockId,WastageCode,ResourceCode,Sprice,Number,Corp ");
            builder.Append(" FROM Sm_Wastage_Stock ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<SmWastageStockModel> list = new List<SmWastageStockModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetListByWastageCode(string wastageCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select p2.WastageStockId,p2.WastageCode,p2.ResourceCode,p2.sprice,(0*p2.sprice) Total, p2.number,0.000 as tnumber,Corp as CorpId,");
            builder.Append(" p7.CorpName as Corp,p3.ResourceName,p3.Specification,");
            builder.Append(" p3.Brand,ModelNumber,TechnicalParameter,");
            builder.Append(" p5.Name from dbo.SM_Wastage as p1 ");
            builder.Append(" join dbo.Sm_Wastage_Stock as p2 on p1.WastageCode=p2.WastageCode ");
            builder.Append(" join dbo.Res_Resource as p3 on p2.ResourceCode=p3.resourceCode");
            builder.Append(" left join Res_Unit as p5 on p3.Unit=p5.UnitID ");
            builder.Append(" left join dbo.XPM_Basic_ContactCorp as p7 on p2.corp=p7.CorpId");
            builder.Append(" where p2.WastageCode in(" + wastageCode + ")");
            builder.Append("  ORDER BY ResourceCode DESC  ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public SmWastageStockModel GetModel(string wastageStockId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WastageStockId,WastageCode,ResourceCode,Sprice,Number,Corp from Sm_Wastage_Stock ");
            builder.Append(" where WastageStockId=@WastageStockId ");
            SmWastageStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@WastageStockId", wastageStockId) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public SmWastageStockModel GetModelByWhere(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select WastageStockId,WastageCode,ResourceCode,Sprice,Number,Corp from Sm_Wastage_Stock ");
            builder.Append(strWhere);
            SmWastageStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetTableByParam(string startTime, string endTime, string wastageCode, string[] ResourceNames, string[] ResourceCodes, string corpName, string category)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p1.WastageCode,p5.WastageId,p4.Name,p2.Specification,p1.sprice,p7.corpName,p9.ResourceTypeName,");
            builder.Append("P2.TechnicalParameter,P2.ModelNumber,P2.Brand,");
            builder.Append(" p2.resourceCode,p2.ResourceName,p1.number,p8.tname,");
            builder.Append(" (p1.sprice*p1.number) as xjsprice,p5.InpuDate from dbo.Sm_Wastage_Stock as p1");
            builder.Append(" inner join Res_Resource as p2 on p1.ResourceCode=p2.resourceCode ");
            builder.Append(" left join Res_Unit as p4 on p2.Unit=p4.UnitID");
            builder.Append(" inner join dbo.SM_Wastage as p5 on p1.WastageCode=p5.WastageCode");
            builder.Append(" inner join dbo.XPM_Basic_ContactCorp as p7 on p1.corp=p7.CorpId");
            builder.Append(" inner join dbo.Sm_Treasury as p8 on p5.tcode=p8.tcode");
            builder.Append(" inner join Res_ResourceType as p9 on p9.ResourceTypeId = p2.ResourceType");
            builder.Append(" where p5.Flowstate = 1");
            if (!string.IsNullOrEmpty(wastageCode))
            {
                builder.Append(" and p1.WastageCode like @WastageCode");
                list.Add(new SqlParameter("@WastageCode", '%' + wastageCode.Trim() + '%'));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p5.InputDate>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p5.InputDate<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (!string.IsNullOrEmpty(corpName))
            {
                builder.Append(" and p7.corpName like @corpName");
                list.Add(new SqlParameter("@corpName", corpName.Trim()));
            }
            if (!string.IsNullOrEmpty(category))
            {
                builder.Append(" and p9.ResourceTypeName like @ResourceTypeName");
                list.Add(new SqlParameter("@ResourceTypeName", '%' + category + '%'));
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
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetTableByWastageCode(string wastageCode, string treasurycode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select number,wastageInfo.ResourceCode,sprice,Corp as CorpId,CorpName,ResourceName, corp, Specification,[Name],(sprice*number) Total,(select sum(snumber) from Sm_Treasury_Stock as sts ").Append(" where sts.tcode=wastageInfo.Treasurycode and sts.sprice=wastageInfo.sprice and  ISNULL(sts.corp,'')=wastageInfo.corp and sts.scode=wastageInfo.ResourceCode ").Append(" group by sts.tcode,sts.sprice,sts.corp ,sts.scode )as snumber ").Append(" ,res.Brand,ModelNumber,TechnicalParameter  ");
            builder.Append(" from ");
            builder.Append(" (select WastageId,wastage.WastageCode,Treasurycode,Flowstate,Isout,InputPerson,InputDate,Explain,WastageStockId,ResourceCode,sprice,number,corp ");
            builder.Append(" from SM_Wastage as wastage ");
            builder.Append(" inner join Sm_Wastage_Stock as wastageStock ");
            builder.Append(" on wastage.WastageCode=wastageStock.WastageCode ");
            builder.Append(" ) as wastageInfo ");
            builder.Append(" inner join Res_Resource as res ");
            builder.Append(" on wastageInfo.ResourceCode=res.resourceCode ");
            builder.Append(" left join Res_Unit as ru ");
            builder.Append(" on ru.UnitID=res.Unit ");
            builder.Append(" left join XPM_Basic_ContactCorp as tbcorp ");
            builder.Append(" on wastageInfo.corp=tbcorp.CorpId ");
            builder.Append(" where wastageCode in(" + wastageCode + ")");
            builder.Append(" ORDER BY  ResourceCode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public SmWastageStockModel ReaderBind(IDataReader dataReader)
        {
            SmWastageStockModel model = new SmWastageStockModel {
                Id = dataReader["WastageStockId"].ToString(),
                WastageCode = dataReader["WastageCode"].ToString(),
                ResourceCode = dataReader["ResourceCode"].ToString()
            };
            object obj2 = dataReader["Sprice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Sprice = (decimal) obj2;
            }
            obj2 = dataReader["Number"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.Number = (decimal) obj2;
            }
            model.Corp = dataReader["Corp"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, SmWastageStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Sm_Wastage_Stock SET ");
            builder.Append("WastageCode=@WastageCode,");
            builder.Append("ResourceCode=@ResourceCode,");
            builder.Append("Sprice=@Sprice,");
            builder.Append("Number=@Number,");
            builder.Append("Corp=@Corp");
            builder.Append(" where WastageStockId=@WastageStockId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@WastageStockId", SqlDbType.NVarChar, 50), new SqlParameter("@WastageCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50), new SqlParameter("@Sprice", SqlDbType.Decimal, 9), new SqlParameter("@Number", SqlDbType.Decimal, 9), new SqlParameter("@Corp", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.WastageCode;
            commandParameters[2].Value = model.ResourceCode;
            commandParameters[3].Value = model.Sprice;
            commandParameters[4].Value = model.Number;
            commandParameters[5].Value = model.Corp;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

