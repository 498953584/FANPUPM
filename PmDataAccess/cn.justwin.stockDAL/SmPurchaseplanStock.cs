namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SmPurchaseplanStock
    {
        private static string PurchasePriceType = ConfigHelper.Get("PurchasePriceType");

        public int Add(SqlTransaction trans, SmPurchaseplanStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Purchaseplan_Stock(");
            builder.Append("wpsid,scode,ppcode,number,arrivalDate,remark)");
            builder.Append(" values (");
            builder.Append("@wpsid,@scode,@ppcode,@number,@arrivalDate,@remark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@ppcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@arrivalDate", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.NVarChar) };
            commandParameters[0].Value = model.wpsid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.ppcode;
            commandParameters[3].Value = model.number;
            if ((model.ArrivalDate == "") || (model.ArrivalDate == null))
            {
                commandParameters[4].Value = DBNull.Value;
            }
            else
            {
                commandParameters[4].Value = model.ArrivalDate;
            }
            commandParameters[5].Value = model.remark;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string wpsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Purchaseplan_Stock ");
            builder.Append(" where wpsid=@wpsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = wpsid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Purchaseplan_Stock ");
            builder.Append(strWhere);
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<SmPurchaseplanStockModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select wpsid,scode,ppcode,number,arrivalDate,remark ");
            builder.Append(" FROM Sm_Purchaseplan_Stock ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<SmPurchaseplanStockModel> list = new List<SmPurchaseplanStockModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public string GetMinArrivalDate(string resourceCode, string ppcodes)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP 1 ArrivalDate FROM Sm_Purchaseplan_Stock WHERE Ppcode IN ({0}) AND Scode='{1}' AND ArrivalDate is not null ", ppcodes, resourceCode);
            builder.Append("order by ArrivalDate");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                if (reader.Read())
                {
                    str = reader["ArrivalDate"].ToString();
                }
            }
            return str;
        }

        public SmPurchaseplanStockModel GetModel(string wpsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select wpsid,scode,ppcode,number,arrivalDate,remark from Sm_Purchaseplan_Stock ");
            builder.Append(" where wpsid=@wpsid ");
            SmPurchaseplanStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@wpsid", wpsid) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetResourceByPpcodes(string[] ppcodes)
        {
            string str = string.Empty;
            if (ppcodes.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in ppcodes)
                {
                    builder.Append("','").Append(str2);
                }
                builder.Append("'");
                str = builder.ToString().Substring(2);
            }
            else
            {
                str = "''";
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("select scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name, Sm_Purchaseplan_Stock.arrivalDate, ").Append("Sm_Purchaseplan_Stock.remark,sum(number) as number,0.0 as sprice,'' as corp, '' as CorpName ").Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ").Append("from Sm_Purchaseplan_Stock ").Append("inner join Res_Resource on Sm_Purchaseplan_Stock.scode = Res_Resource.ResourceCode ").Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ").Append("where ppcode in (").Append(str).Append(") group by scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name,Sm_Purchaseplan_Stock.arrivalDate ").Append(",Sm_Purchaseplan_Stock.remark,Res_Resource.Brand,ModelNumber,TechnicalParameter ORDER BY scode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), null);
        }

        public DataTable GetResourceByPpcodes(string[] ppcodes, string strPrjId, string isWBSRelevance)
        {
            string str = string.Empty;
            if (ppcodes.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in ppcodes)
                {
                    builder.Append("','").Append(str2);
                }
                builder.Append("'");
                str = builder.ToString().Substring(2);
            }
            else
            {
                str = "''";
            }
            StringBuilder builder2 = new StringBuilder();
            if (isWBSRelevance == "1")
            {
                builder2.Append("select scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name, Sm_Purchaseplan_Stock.arrivalDate, ").Append("Sm_Purchaseplan_Stock.remark,sum(number) as number,0.0 as sprice,'' as corp, '' as CorpName ").Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ").Append(",isnull(data.ReadyNumber,0) as ReadyNumber,isnull(PriceValue,0) AS Price ").Append("from Sm_Purchaseplan_Stock ").Append("inner join Res_Resource on Sm_Purchaseplan_Stock.scode = Res_Resource.ResourceCode ").Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ").AppendFormat("LEFT JOIN( SELECT SUM(ResQuantity) AS ReadyNumber,ResourceCode FROM \r\n\t                                (\r\n\t\t                                SELECT (SUM(Bud_TaskResource.ResourceQuantity)*Bud_TaskResource.ResourcePrice) ResTotal,\r\n\t\t                                SUM(Bud_TaskResource.ResourceQuantity) ResQuantity,Bud_TaskResource.ResourceId\r\n\t\t                                FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n\t\t                                JOIN  vGetCurBudVersion ON Bud_Task.PrjId=vGetCurBudVersion.PrjId \r\n\t\t                                AND Bud_Task.version=vGetCurBudVersion.curversion AND TaskType=''\r\n\t\t                                AND Bud_Task.PrjId='{0}'\r\n\t\t                                GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId\r\n\t\t                                UNION ALL\r\n\t\t                                SELECT (SUM(ResourceQuantity)*ResourcePrice) ReseTotal,SUM(ResourceQuantity) ResQuantity,\r\n\t\t                                ResourceId FROM Bud_ModifyTaskRes modifyTaskRes\r\n\t\t                                JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n\t\t                                JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n\t\t                                WHERE modify.FlowState='1' AND modify.PrjId='{0}'\r\n\t\t                                GROUP BY ResourcePrice,ResourceId\r\n\t                                ) allBudResInfo \r\n\t                                INNER JOIN Res_Resource AS r ON allBudResInfo.ResourceId=r.ResourceId\r\n\t                                GROUP BY r.ResourceCode", strPrjId).Append(") AS data ON Res_Resource.ResourceCode = data.ResourceCode ").Append("LEFT JOIN (SELECT * FROM Res_Price\tWHERE PriceTypeId = '" + PurchasePriceType + "') AS price ON price.ResourceId = Res_Resource.ResourceId ").Append("where ppcode in (").Append(str).Append(") group by scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name,Sm_Purchaseplan_Stock.arrivalDate ").Append(",Sm_Purchaseplan_Stock.remark,Res_Resource.Brand,ModelNumber,TechnicalParameter,data.ReadyNumber,PriceValue ORDER BY scode DESC ");
            }
            else
            {
                builder2.AppendFormat("\r\n                        select scode,Res_Resource.ResourceId,Res_Resource.ResourceCode ,ResourceName,Specification,Name, \r\n                        Sm_Purchaseplan_Stock.arrivalDate, Sm_Purchaseplan_Stock.remark,sum(number) as number\r\n                        ,0.0 as sprice,'' as corp, '' as CorpName ,Res_Resource.Brand,ModelNumber,TechnicalParameter,\r\n                        isnull(data.ReadyNumber,0) as ReadyNumber,isnull(PriceValue,0) AS Price from Sm_Purchaseplan_Stock \r\n                        inner join Res_Resource on Sm_Purchaseplan_Stock.scode = Res_Resource.ResourceCode \r\n                        left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit \r\n                        LEFT JOIN(\r\n\t                        SELECT SUM(ResourceQuantity) AS ReadyNumber ,ResourceCode FROM Bud_TaskResource AS res \r\n\t                        INNER JOIN Res_Resource AS r ON r.ResourceId=res.ResourceId \r\n\t                        WHERE PrjGuid = '{0}'  \r\n\t                        GROUP BY ResourceCode \r\n                        ) AS data ON Res_Resource.ResourceCode = data.ResourceCode \r\n                        LEFT JOIN (\r\n\t                        SELECT * FROM Res_Price\tWHERE PriceTypeId = '{2}'\r\n                        ) AS price ON price.ResourceId = Res_Resource.ResourceId \r\n                        where ppcode in ({3}) \r\n                        group by scode,Res_Resource.ResourceId, Res_Resource.ResourceCode,ResourceName,Specification\r\n                        ,Name,Sm_Purchaseplan_Stock.arrivalDate ,Sm_Purchaseplan_Stock.remark,Res_Resource.Brand,\r\n                        ModelNumber,TechnicalParameter,data.ReadyNumber,PriceValue ORDER BY scode DESC \r\n                ", strPrjId, PurchasePriceType, str);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), null);
        }

        public DataTable GetResourceByPurchasePcodes(string[] ppcodes)
        {
            string str = string.Empty;
            if (ppcodes.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in ppcodes)
                {
                    builder.Append("','").Append(str2);
                }
                builder.Append("'");
                str = builder.ToString().Substring(2);
            }
            else
            {
                str = "''";
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("select scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name, ").Append("sum(number) as number,0.0 as sprice,'' as corp, '' as CorpName ").Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter,Sm_Purchaseplan_Stock.remark ").Append("from Sm_Purchaseplan_Stock ").Append("inner join Res_Resource on Sm_Purchaseplan_Stock.scode = Res_Resource.ResourceCode ").Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ").Append("where ppcode in (").Append(str).Append(") group by scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name ").Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter,Sm_Purchaseplan_Stock.remark  ORDER BY scode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), null);
        }

        public DataTable GetTableByParam(string startTime, string endTime, string ppcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string category, string specification, string brand, string modelNumber)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p1.ppcode,p5.ppid,ISNULL(p6.prjCode,PT_PrjInfo_ZTB.prjCode) AS prjCode,ISNULL(p6.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,p2.resourceCode,p7.ResourceTypeName,");
            builder.Append("P2.Specification,P2.TechnicalParameter,P2.ModelNumber,P2.Brand,");
            builder.Append(" p2.ResourceName,p1.number,p5.intime from dbo.Sm_Purchaseplan_Stock as p1 ");
            builder.Append(" inner join Res_Resource as p2 on p1.scode=p2.resourceCode");
            builder.Append(" inner join Res_Unit as p4 on p2.Unit=p4.UnitID");
            builder.Append(" inner join dbo.Sm_Purchaseplan as p5 on p1.ppcode=p5.ppcode");
            builder.Append(" left join dbo.PT_PrjInfo as p6 on p5.Project=p6.prjGuid");
            builder.Append(" left join PT_PrjInfo_ZTB on PT_PrjInfo_ZTB.prjGuid=p5.Project");
            builder.Append(" inner join Res_ResourceType as p7 on p7.ResourceTypeId = p2.ResourceType");
            builder.Append(" where p5.flowstate = 1");
            if (!string.IsNullOrEmpty(ppcode))
            {
                builder.Append(" and p1.ppcode like @ppcode");
                list.Add(new SqlParameter("@ppcode", '%' + ppcode.Trim() + '%'));
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
            if (!string.IsNullOrEmpty(category))
            {
                builder.Append(" and p7.ResourceTypeName like @ResourceTypeName");
                list.Add(new SqlParameter("@ResourceTypeName", '%' + category + '%'));
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.Append(" and P2.Specification like @specification");
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
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public SmPurchaseplanStockModel ReaderBind(IDataReader dataReader)
        {
            SmPurchaseplanStockModel model = new SmPurchaseplanStockModel {
                wpsid = dataReader["wpsid"].ToString(),
                scode = dataReader["scode"].ToString(),
                ppcode = dataReader["ppcode"].ToString()
            };
            object obj2 = dataReader["number"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.number = (decimal) obj2;
            }
            obj2 = dataReader["arrivalDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ArrivalDate = (string) obj2;
            }
            return model;
        }

        public int Update(SqlTransaction trans, SmPurchaseplanStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Purchaseplan_Stock set ");
            builder.Append("scode=@scode,");
            builder.Append("ppcode=@ppcode,");
            builder.Append("number=@number,");
            builder.Append("arrivalDate=@arrivalDate, ");
            builder.Append("remark=@remark ");
            builder.Append(" where wpsid=@wpsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@wpsid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@ppcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@arrivalDate", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.NVarChar) };
            commandParameters[0].Value = model.wpsid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.ppcode;
            commandParameters[3].Value = model.number;
            if (model.ArrivalDate == "")
            {
                commandParameters[4].Value = DBNull.Value;
            }
            else
            {
                commandParameters[4].Value = model.ArrivalDate;
            }
            commandParameters[5].Value = model.remark;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

