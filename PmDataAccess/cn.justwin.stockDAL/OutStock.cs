namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OutStock
    {
        public int Add(SqlTransaction trans, OutStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_out_Stock(");
            builder.Append("orsid,orcode,scode,sprice,number,corp,TaskId)");
            builder.Append(" values (");
            builder.Append("@orsid,@orcode,@scode,@sprice,@number,@corp,@TaskId)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@orsid", SqlDbType.NVarChar, 50), new SqlParameter("@orcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@sprice", SqlDbType.Decimal, 0x12), new SqlParameter("@number", SqlDbType.Decimal, 0x12), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaskId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.orsid;
            commandParameters[1].Value = model.orcode;
            commandParameters[2].Value = model.scode;
            commandParameters[3].Value = model.sprice;
            commandParameters[4].Value = model.number;
            commandParameters[5].Value = model.corp;
            commandParameters[6].Value = model.TaskId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string orsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_out_Stock ");
            builder.Append(" where orsid=@orsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@orsid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = orsid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_out_Stock ");
            builder.Append(strWhere);
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<OutStockModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select orsid,orcode,scode,sprice,number,corp ");
            builder.Append(" FROM Sm_out_Stock ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<OutStockModel> list = new List<OutStockModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetListByOrcode(string orcode)
        {
            string cmdText = "\r\n                    WITH cteBudTask AS \r\n                    (\r\n\t                    SELECT TaskId,TaskCode,TaskName\r\n\t                    FROM Bud_Task\t\r\n                    ),cteBudModify AS \r\n                    (\r\n\t                    SELECT modifyTask.ModifyTaskId AS TaskId,ModifyTaskCode AS TaskCode,ModifyTaskContent AS TaskName\r\n\t                    FROM Bud_ModifyTask modifyTask\t\r\n\t                    JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n\t                    WHERE ModifyType=0 AND FlowState=1 \r\n                    ),cteTask AS\r\n                    (\r\n\t                    SELECT TaskId,TaskCode,TaskName FROM cteBudTask\r\n\t                    UNION\r\n\t                    SELECT TaskId,TaskCode,TaskName FROM cteBudModify\r\n                    )\r\n                    SELECT DISTINCT p2.orsid,p2.orcode,p2.scode,p2.sprice,(0*p2.sprice) Total, \r\n                    p2.number,0.000 AS tnumber,Corp AS CorpId, p7.CorpName AS Corp,\r\n                    p3.ResourceName,p3.Specification, p3.Brand,ModelNumber,TechnicalParameter, \r\n                    p5.Name,p2.TaskId,ISNULL(cteTask.TaskCode,'') TaskCode,ISNULL(cteTask.TaskName,'') TaskName\r\n                    FROM dbo.Sm_OutReserve AS p1  \r\n                    JOIN dbo.Sm_out_Stock AS p2 ON p1.orcode=p2.orcode  \r\n                    JOIN dbo.Res_Resource AS p3 ON p2.scode=p3.resourceCode \r\n                    LEFT JOIN cteTask ON p2.TaskId = cteTask.TaskId\r\n                    LEFT JOIN Res_Unit AS p5 ON p3.Unit=p5.UnitID  \r\n                    LEFT JOIN dbo.XPM_Basic_ContactCorp AS p7 ON p2.corp=p7.CorpId ";
            cmdText = cmdText + "WHERE p2.orcode in(" + orcode + ") ORDER BY scode DESC";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public OutStockModel GetModel(string orsid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select orsid,orcode,scode,sprice,number,corp from Sm_out_Stock ");
            builder.Append(" where orsid=@orsid ");
            OutStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@orsid", orsid) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public OutStockModel GetModelByWhere(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select orsid,orcode,scode,sprice,number,corp from Sm_out_Stock ");
            builder.Append(strWhere);
            OutStockModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetTableByOrcode(string orcode, string tcode)
        {
            string cmdText = "\r\n                    WITH cteBudTask AS \r\n                    (\r\n                        SELECT TaskId,TaskName FROM Bud_Task\r\n                    ),cteBudModify AS \r\n                    (\r\n                        SELECT ModifyTaskId AS TaskId,ModifyTaskContent AS TaskName\r\n                        FROM Bud_ModifyTask modifyTask\r\n                        JOIN Bud_Modify budModify ON modifyTask.modifyId=budModify.modifyId\r\n                        WHERE ModifyType=0 AND FlowState=1 \r\n                    ),cteTask AS\r\n                    (\r\n                        SELECT TaskId,TaskName FROM cteBudTask\r\n                        UNION\r\n                        SELECT TaskId,TaskName FROM cteBudModify\r\n                    )\r\n                    SELECT number,scode,sprice,Corp AS CorpId,CorpName corp,ResourceName, \r\n                    Specification,[Name],(sprice*number) Total,TaskId,TaskName,\r\n                    (\tSELECT sum(snumber) \r\n\t                    FROM Sm_Treasury_Stock AS sts\r\n\t                    WHERE sts.tcode=outinfo.tcode AND sts.sprice=outinfo.sprice \r\n\t                    AND sts.corp=outinfo.corp AND sts.scode=outinfo.scode  \r\n\t                    GROUP BY sts.tcode,sts.sprice,sts.corp ,sts.scode \r\n                    )AS snumber,res.Brand,ModelNumber,TechnicalParameter   \r\n                    FROM  \r\n                    (\r\n\t                    SELECT orid,so.orcode,procode,tcode,PickingPeople,PickingSector,flowstate,\r\n\t                    isout,person,intime,explain,orsid,scode,sprice,number,corp,sos.TaskId,TaskName  \r\n\t                    FROM Sm_out_Stock AS sos  \r\n\t                    INNER JOIN Sm_OutReserve AS so ON so.orcode=sos.orcode  \r\n\t                    LEFT JOIN cteTask ON sos.TaskId = cteTask.TaskId \r\n                    ) AS outinfo  \r\n                    INNER JOIN Res_Resource AS res ON outinfo.scode=res.resourceCode  \r\n                    LEFT JOIN Res_Unit AS ru ON ru.UnitID=res.Unit  \r\n                    LEFT JOIN XPM_Basic_ContactCorp AS tbcorp  ON outinfo.corp=tbcorp.CorpId  \r\n                    WHERE orcode IN(" + orcode + ") ORDER BY  scode DESC ";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public DataTable GetTableByParam(string startTime, string endTime, string orcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string corpName, string category, string specification, string brand, string modelNumber)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            builder.Append("select p1.orcode,p5.orid,p4.Name,p2.Specification,p1.sprice,p7.corpName,p9.ResourceTypeName,");
            builder.Append("P2.TechnicalParameter,P2.ModelNumber,P2.Brand,");
            builder.Append(" ISNULL(p6.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,p2.resourceCode,p2.ResourceName,p1.number,p8.tname,");
            builder.Append(" Convert(decimal(18,3),(p1.sprice*p1.number)) as xjsprice,p5.intime from dbo.Sm_out_Stock as p1");
            builder.Append(" inner join Res_Resource as p2 on p1.scode=p2.resourceCode ");
            builder.Append(" left join Res_Unit as p4 on p2.Unit=p4.UnitID");
            builder.Append(" inner join dbo.Sm_OutReserve as p5 on p1.orcode=p5.orcode and p5.isout=1");
            builder.Append(" left join dbo.PT_PrjInfo as p6 on p5.procode=p6.prjGuid");
            builder.Append(" left join PT_PrjInfo_ZTB on p5.procode=PT_PrjInfo_ZTB.prjGuid");
            builder.Append(" left join dbo.XPM_Basic_ContactCorp as p7 on p1.corp=p7.CorpId");
            builder.Append(" inner join dbo.Sm_Treasury as p8 on p5.tcode=p8.tcode");
            builder.Append(" inner join Res_ResourceType as p9 on p9.ResourceTypeId = p2.ResourceType");
            builder.Append(" where p5.flowstate = 1");
            if (!string.IsNullOrEmpty(orcode))
            {
                builder.Append(" and p1.orcode like @orcode");
                list.Add(new SqlParameter("@orcode", '%' + orcode.Trim() + '%'));
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

        public OutStockModel ReaderBind(IDataReader dataReader)
        {
            OutStockModel model = new OutStockModel {
                orsid = dataReader["orsid"].ToString(),
                orcode = dataReader["orcode"].ToString(),
                scode = dataReader["scode"].ToString()
            };
            object obj2 = dataReader["sprice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.sprice = (decimal) obj2;
            }
            obj2 = dataReader["number"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.number = (decimal) obj2;
            }
            model.corp = dataReader["corp"].ToString();
            return model;
        }

        public int Update(SqlTransaction trans, OutStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_out_Stock set ");
            builder.Append("orcode=@orcode,");
            builder.Append("scode=@scode,");
            builder.Append("sprice=@sprice,");
            builder.Append("number=@number,");
            builder.Append("corp=@corp");
            builder.Append(" where orsid=@orsid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@orsid", SqlDbType.NVarChar, 50), new SqlParameter("@orcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.orsid;
            commandParameters[1].Value = model.orcode;
            commandParameters[2].Value = model.scode;
            commandParameters[3].Value = model.sprice;
            commandParameters[4].Value = model.number;
            commandParameters[5].Value = model.corp;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

