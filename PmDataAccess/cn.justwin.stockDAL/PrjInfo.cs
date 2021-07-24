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

    public class PrjInfo
    {
        private string isNewProject = ConfigHelper.Get("IsNewProject");

        public int Add(PrjInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_PrjInfo(");
            builder.Append("TypeCode,i_xh,IsValid,UserCode,RecordDate,i_ChildNum,PrjCode,PrjGuid,PrjName,StartDate,EndDate,ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,Area,PrjKindClass,PrjPlace,Remark1,FileName,FileURL,Owner,Counsellor,Designer,Inspector,PrjInfo,PrjState,OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay,KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,Podepom,IsConfirm,PrjStateRemark)");
            builder.Append(" values (");
            builder.Append("@TypeCode,@i_xh,@IsValid,@UserCode,@RecordDate,@i_ChildNum,@PrjCode,@PrjGuid,@PrjName,@StartDate,@EndDate,@ComputeClass,@RationClass,@PrjCost,@ContractSum,@Duration,@QualityClass,@Area,@PrjKindClass,@PrjPlace,@Remark1,@FileName,@FileURL,@Owner,@Counsellor,@Designer,@Inspector,@PrjInfo,@PrjState,@OwnerCode,@MarketInfoGuid,@Rank,@BudgetWay,@ContractWay,@PayCondition,@TenderWay,@PayWay,@KeyPart,@WorkUnit,@LinkMan,@PrjManager,@BuildingType,@TotalHouseNum,@BuildingArea,@UsegrounArea,@UndergroundArea,@PrjFundInfo,@OtherStatement,@Podepom,@IsConfirm,@PrjStateRemark)");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@TypeCode", SqlDbType.VarChar, 0x18), new SqlParameter("@i_xh", SqlDbType.Int, 4), new SqlParameter("@IsValid", SqlDbType.Char, 1), new SqlParameter("@UserCode", SqlDbType.VarChar, 8), new SqlParameter("@RecordDate", SqlDbType.DateTime), new SqlParameter("@i_ChildNum", SqlDbType.Int, 4), new SqlParameter("@PrjCode", SqlDbType.VarChar, 30), new SqlParameter("@PrjGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PrjName", SqlDbType.VarChar, 100), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@ComputeClass", SqlDbType.VarChar, 30), new SqlParameter("@RationClass", SqlDbType.VarChar, 30), new SqlParameter("@PrjCost", SqlDbType.Float, 8), new SqlParameter("@ContractSum", SqlDbType.Char, 10), new SqlParameter("@Duration", SqlDbType.VarChar, 50), 
                new SqlParameter("@QualityClass", SqlDbType.VarChar, 30), new SqlParameter("@Area", SqlDbType.VarChar, 50), new SqlParameter("@PrjKindClass", SqlDbType.VarChar, 30), new SqlParameter("@PrjPlace", SqlDbType.VarChar, 100), new SqlParameter("@Remark1", SqlDbType.VarChar, 0xfa0), new SqlParameter("@FileName", SqlDbType.VarChar, 200), new SqlParameter("@FileURL", SqlDbType.VarChar, 100), new SqlParameter("@Owner", SqlDbType.VarChar, 100), new SqlParameter("@Counsellor", SqlDbType.VarChar, 100), new SqlParameter("@Designer", SqlDbType.VarChar, 100), new SqlParameter("@Inspector", SqlDbType.VarChar, 100), new SqlParameter("@PrjInfo", SqlDbType.VarChar, 0xfa0), new SqlParameter("@PrjState", SqlDbType.Int, 4), new SqlParameter("@OwnerCode", SqlDbType.Int, 4), new SqlParameter("@MarketInfoGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Rank", SqlDbType.VarChar, 50), 
                new SqlParameter("@BudgetWay", SqlDbType.VarChar, 50), new SqlParameter("@ContractWay", SqlDbType.VarChar, 50), new SqlParameter("@PayCondition", SqlDbType.VarChar, 50), new SqlParameter("@TenderWay", SqlDbType.VarChar, 50), new SqlParameter("@PayWay", SqlDbType.VarChar, 50), new SqlParameter("@KeyPart", SqlDbType.VarChar, 50), new SqlParameter("@WorkUnit", SqlDbType.VarChar, 100), new SqlParameter("@LinkMan", SqlDbType.VarChar, 50), new SqlParameter("@PrjManager", SqlDbType.VarChar, 50), new SqlParameter("@BuildingType", SqlDbType.VarChar, 50), new SqlParameter("@TotalHouseNum", SqlDbType.VarChar, 50), new SqlParameter("@BuildingArea", SqlDbType.VarChar, 50), new SqlParameter("@UsegrounArea", SqlDbType.VarChar, 50), new SqlParameter("@UndergroundArea", SqlDbType.VarChar, 50), new SqlParameter("@PrjFundInfo", SqlDbType.VarChar, 500), new SqlParameter("@OtherStatement", SqlDbType.VarChar, 500), 
                new SqlParameter("@Podepom", SqlDbType.VarChar, 0xfa0), new SqlParameter("@IsConfirm", SqlDbType.Bit, 1), new SqlParameter("@PrjStateRemark", SqlDbType.VarChar, 500)
             };
            commandParameters[0].Value = model.TypeCode;
            commandParameters[1].Value = model.i_xh;
            commandParameters[2].Value = model.IsValid;
            commandParameters[3].Value = model.UserCode;
            commandParameters[4].Value = model.RecordDate;
            commandParameters[5].Value = model.i_ChildNum;
            commandParameters[6].Value = model.PrjCode;
            commandParameters[7].Value = model.PrjGuid;
            commandParameters[8].Value = model.PrjName;
            commandParameters[9].Value = model.StartDate;
            commandParameters[10].Value = model.EndDate;
            commandParameters[11].Value = model.ComputeClass;
            commandParameters[12].Value = model.RationClass;
            commandParameters[13].Value = model.PrjCost;
            commandParameters[14].Value = model.ContractSum;
            commandParameters[15].Value = model.Duration;
            commandParameters[0x10].Value = model.QualityClass;
            commandParameters[0x11].Value = model.Area;
            commandParameters[0x12].Value = model.PrjKindClass;
            commandParameters[0x13].Value = model.PrjPlace;
            commandParameters[20].Value = model.Remark1;
            commandParameters[0x15].Value = model.FileName;
            commandParameters[0x16].Value = model.FileURL;
            commandParameters[0x17].Value = model.Owner;
            commandParameters[0x18].Value = model.Counsellor;
            commandParameters[0x19].Value = model.Designer;
            commandParameters[0x1a].Value = model.Inspector;
            commandParameters[0x1b].Value = model.PrjInfo;
            commandParameters[0x1c].Value = model.PrjState;
            commandParameters[0x1d].Value = model.OwnerCode;
            commandParameters[30].Value = model.MarketInfoGuid;
            commandParameters[0x1f].Value = model.Rank;
            commandParameters[0x20].Value = model.BudgetWay;
            commandParameters[0x21].Value = model.ContractWay;
            commandParameters[0x22].Value = model.PayCondition;
            commandParameters[0x23].Value = model.TenderWay;
            commandParameters[0x24].Value = model.PayWay;
            commandParameters[0x25].Value = model.KeyPart;
            commandParameters[0x26].Value = model.WorkUnit;
            commandParameters[0x27].Value = model.LinkMan;
            commandParameters[40].Value = model.PrjManager;
            commandParameters[0x29].Value = model.BuildingType;
            commandParameters[0x2a].Value = model.TotalHouseNum;
            commandParameters[0x2b].Value = model.BuildingArea;
            commandParameters[0x2c].Value = model.UsegrounArea;
            commandParameters[0x2d].Value = model.UndergroundArea;
            commandParameters[0x2e].Value = model.PrjFundInfo;
            commandParameters[0x2f].Value = model.OtherStatement;
            commandParameters[0x30].Value = model.Podepom;
            commandParameters[0x31].Value = model.IsConfirm;
            commandParameters[50].Value = model.PrjStateRemark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void Add(string _PrjGuid, string _grade, string _businessman, string _telephone)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_PrjInfo_ZTB_Stock(");
            builder.Append("PrjGuid,grade,businessman,telephone");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + _PrjGuid + "',");
            builder.Append("'" + _grade + "',");
            builder.Append("'" + _businessman + "',");
            builder.Append("'" + _telephone + "'");
            builder.Append(")");
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Delete(string TypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PT_PrjInfo ");
            builder.Append(" where TypeCode=@TypeCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeCode", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = TypeCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable getbusinessman(string TypeCode, string _table)
        {
            if (TypeCode != "")
            {
                string cmdText = "SELECT ppi.businessman FROM\t " + _table + " ppi WHERE ppi.TypeCode='" + TypeCode + "' ";
                return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            }
            return null;
        }

        public DataTable getDataTable(string TypeCode, string _table)
        {
            if (TypeCode != "")
            {
                string cmdText = "SELECT ppi.grade,ppi.businessman,ppi.telephone FROM\t" + _table + " ppi WHERE ppi.PrjGuid='" + TypeCode + "' ";
                return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            }
            return null;
        }

        public List<PrjInfoModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeCode,i_xh,IsValid,UserCode,RecordDate,i_ChildNum,PrjCode,PrjGuid,PrjName,StartDate,EndDate,ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,Area,PrjKindClass,PrjPlace,Remark1,FileName,FileURL,Owner,Counsellor,Designer,Inspector,PrjInfo,PrjState,OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay,KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,Podepom,IsConfirm,PrjStateRemark ");
            builder.Append(" FROM PT_PrjInfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<PrjInfoModel> list = new List<PrjInfoModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public PrjInfoModel GetModel(string TypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeCode,i_xh,IsValid,UserCode,RecordDate,i_ChildNum,PrjCode,PrjGuid,PrjName,StartDate,EndDate,ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,Area,PrjKindClass,PrjPlace,Remark1,FileName,FileURL,Owner,Counsellor,Designer,Inspector,PrjInfo,PrjState,OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay,KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,Podepom,IsConfirm,PrjStateRemark from PT_PrjInfo ");
            builder.Append(" where TypeCode=@TypeCode ");
            PrjInfoModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@TypeCode", TypeCode) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public PrjInfoModel GetModelByPrjGuid(string prjGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeCode,i_xh,IsValid,UserCode,RecordDate,i_ChildNum,PrjCode,PrjGuid,PrjName,StartDate,EndDate,ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,Area,PrjKindClass,PrjPlace,Remark1,FileName,FileURL,Owner,Counsellor,Designer,Inspector,PrjInfo,PrjState,OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay,KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,Podepom,IsConfirm,PrjStateRemark from PT_PrjInfo ");
            builder.Append(" where prjGuid=@prjGuid ");
            PrjInfoModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@prjGuid", prjGuid) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public List<string> GetPrjInfoIncoment(string prjId)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT ISNULL(Detail.PrjFundWorkable,'') AS PrjFundWorkable,ISNULL(Detail.ForecastProfitRate,0) AS ForecastProfitRate ");
            builder.AppendLine(",ISNULL(CodeList.CodeName,'') AS QualityClass,PrjFundInfo,PrjType.CodeName FROM PT_PrjInfo  ");
            builder.AppendLine("LEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON PT_PrjInfo.PrjGuid=Detail.PrjGuid ");
            builder.AppendLine("LEFT JOIN (SELECT * FROM dbo.XPM_Basic_CodeList WHERE TypeId = (SELECT TypeId FROM dbo.XPM_Basic_CodeType  ");
            builder.AppendLine("WHERE SignCode='ProjectQuality')) AS CodeList ON PT_PrjInfo.QualityClass=CodeList.CodeId ");
            builder.AppendLine("LEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList Where TypeID=(SELECT TypeID FROM XPM_Basic_CodeType Where SignCode='ProjectType') and ISvalid=1) PrjType on PT_PrjInfo.PrjKindClass=PrjType.CodeID ");
            builder.AppendFormat("WHERE PT_PrjInfo.PrjGuid='{0}' ", prjId).AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(reader["PrjFundWorkable"].ToString());
                    list.Add(reader["ForecastProfitRate"].ToString());
                    list.Add(reader["QualityClass"].ToString());
                    list.Add(reader["PrjFundInfo"].ToString());
                    list.Add(reader["CodeName"].ToString());
                }
            }
            return list;
        }

        public List<string> GetPrjInfoZTBIncoment(string conId)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT ISNULL(Detail.PrjFundWorkable,'') AS PrjFundWorkable,ISNULL(Detail.ForecastProfitRate,0) AS ForecastProfitRate ");
            builder.AppendLine(",ISNULL(CodeList.CodeName,'') AS QualityClass,PrjFundInfo,PT_PrjInfo_ZTB.PrjName,PT_PrjInfo_ZTB.PrjGuid FROM Con_Incomet_Contract ");
            builder.AppendLine("LEFT JOIN PT_PrjInfo_ZTB ON Con_Incomet_Contract.Project=PT_PrjInfo_ZTB.PrjGuid ");
            builder.AppendLine("LEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON PT_PrjInfo_ZTB.PrjGuid=Detail.PrjGuid ");
            builder.AppendLine("LEFT JOIN (SELECT * FROM dbo.XPM_Basic_CodeList WHERE TypeId = (SELECT TypeId FROM dbo.XPM_Basic_CodeType  ");
            builder.AppendLine("WHERE SignCode='ProjectQuality')) AS CodeList ON PT_PrjInfo_ZTB.QualityClass=CodeList.CodeId ");
            builder.AppendFormat("WHERE Con_Incomet_Contract.ContractID='{0}' ", conId).AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(reader["PrjFundWorkable"].ToString());
                    list.Add(reader["ForecastProfitRate"].ToString());
                    list.Add(reader["QualityClass"].ToString());
                    list.Add(reader["PrjFundInfo"].ToString());
                    list.Add(reader["PrjName"].ToString());
                    list.Add(reader["PrjGuid"].ToString());
                }
            }
            return list;
        }

        public DataTable GetProject(string usercode, string prjcode, string prjname, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetProjectCount(usercode, prjcode, prjname);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\t\tSELECT * FROM(select * from PT_PrjInfo where TypeCode in (\r\n\t\t\t\tselect  TypeCode from (\r\n\t\t\t\tSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1 AND PrjState not in('1','17','18') AND   ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )) \r\n\t\t\t\tunion SELECT   *  FROM  PT_PrjInfo \r\n\t\t\t\tWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1 AND PrjState not in('1','17','18')  AND   ( Podepom LIKE '%{2}%' or  PrjManager LIKE '%{3}%' ) ) as project ", new object[] { usercode, usercode, usercode, usercode });
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Append(" )");
            builder.Append(") AS Prj");
            builder.AppendFormat(" UNION SELECT * FROM PT_PrjInfo WHERE LEN(typecode)=5 and isvalid=1  AND PrjState not in('1','17','18')  AND ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )", usercode, usercode);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Insert(0, "\r\n\t\t\t\tSELECT TOP (@pageSize) * FROM (\r\n\t\t\t\tSELECT Row_Number()over(ORDER BY userDefined_Date DESC,TypeCode ASC) as Num,* FROM \r\n\t\t\t\t(SELECT \r\n\t\t\t\tTResult.TypeCode,TResult.PrjCode,TResult.PrjName,TResult.PrjPlace,TResult.Owner,TResult.OwnerCode,TResult.i_childnum,TResult.startdate,TResult.EndDate,TResult.PrjState,TResult.PrjCost,TResult.prjguid \r\n\t\t\t\t,CASE TResult.i_ChildNum\r\n\t\t\t\t\tWHEN '0' THEN (\r\n\t\t\t\t\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\t\t\t\t\tWHEN '5' THEN TResult.StartDate \r\n\t\t\t\t\t\t\t\t\t\tELSE (SELECT PT1.StartDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\t\t\t\t\tEND\r\n\t\t\t\t\t\t\t\t\t)\r\n\t\t\t\t\tELSE TResult.StartDate\r\n\t\t\t\tEND AS userDefined_Date,Detail.SetUpFlowState,PrjKindClass\r\n\t\t\t\tFROM\r\n\t\t\t(");
            builder.Append("\r\n\t\t\t\t) AS TResult LEFT JOIN PT_PrjInfo_ZTB_Detail detail ON TResult.PrjGuid=detail.PrjGuid  WHERE TResult.PrjGuid NOT IN\r\n\t\t\t\t(SELECT info.PrjGuid FROM PT_PrjInfo info left join PT_PrjInfo_ZTB_Detail detail ON info.prjGuid=detail.prjGuid\r\n\t\t\t\twhere SetUpFlowState<>1 AND LEN(TypeCode)=10 AND PrjState not in('1','17','18'))\r\n\r\n\t\t\t\tunion \r\n\t\t\t\tselect '00000' as TypeCode,PrjInfoZTB.PrjCode,PrjInfoZTB.PrjName,PrjInfoZTB.PrjPlace,PrjInfoZTB.Owner,PrjInfoZTB.OwnerCode,0 as i_childnum,\r\n\t\t\t\tPrjInfoZTB.startdate,PrjInfoZTB.EndDate,PrjInfoZTB.PrjState,PrjInfoZTB.PrjCost,PrjInfoZTB.prjguid \r\n\t\t\t\t,PrjInfoZTB.StartDate as userDefined_Date,1 as SetUpFlowState,PrjKindClass\r\n\t\t\t\tfrom \r\n\t\t\t\t(select PT_PrjInfo_ZTB.* from PT_PrjInfo_ZTB \r\n\t\t\t\tjoin PT_PrjInfo_ZTB_Detail on PT_PrjInfo_ZTB.PrjGuid=PT_PrjInfo_ZTB_Detail.PrjGuid\r\n\t\t\t\tand PT_PrjInfo_ZTB_Detail.ProjFlowSate =1\r\n\t\t\t\twhere PT_PrjInfo_ZTB.PrjState=5 and PT_PrjInfo_ZTB.bidFlowState='1' and PT_PrjInfo_ZTB.PrjGuid not in(select PrjGuid from PT_PrjInfo)) PrjInfoZTB\r\n\t\t\t\tjoin Pt_PrjInfo_ZTB_User on PrjInfoZTB.prjguid=Pt_PrjInfo_ZTB_User.prjguid and (Pt_PrjInfo_ZTB_User.UserCode LIKE '%" + usercode + "%')");
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjName like '%{0}%' ", prjname);
            }
            builder.AppendLine();
            builder.Append("\r\n\t\t\t\t) AS tbPrjInfo\r\n\t\t\t\tleft join (SELECT CodeID,CodeName FROM XPM_Basic_CodeList Where TypeID=(SELECT TypeID FROM XPM_Basic_CodeType Where SignCode='ProjectType') and ISvalid=1) PrjType on tbPrjInfo.PrjKindClass=PrjType.CodeID\r\n\t\t\t\t) AS Tab WHERE Num > @pageSize * (@pageIndex - 1)\r\n\t\t\t");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pageSize", SqlDbType.Int, 4), new SqlParameter("@pageIndex", SqlDbType.Int, 4) };
            commandParameters[0].Value = pageSize;
            commandParameters[1].Value = pageIndex;
            DataTable table = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetProjectByCodes(string usercode, string prjcode, string prjname, string codes, int install, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetProjectCountByCodes(usercode, prjcode, prjname, codes, install);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("WITH CTE AS(SELECT * FROM(select * from PT_PrjInfo where TypeCode in (\r\nselect  TypeCode from (\r\nSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1 AND PrjState<>17  AND   ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )) \r\nunion SELECT   *  FROM  PT_PrjInfo \r\nWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND PrjState<>17 AND   ( Podepom LIKE '%{2}%' or  PrjManager LIKE '%{3}%' ) ) as project ", new object[] { usercode, usercode, usercode, usercode });
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            if (!string.IsNullOrEmpty(codes))
            {
                if (install == 1)
                {
                    builder.AppendFormat(" and prjGuid not in ({0})", codes);
                }
                else
                {
                    builder.AppendFormat(" and prjGuid in ({0})", codes);
                }
            }
            builder.Append(" )");
            builder.Append(") AS Prj");
            builder.AppendFormat(" UNION SELECT * FROM PT_PrjInfo WHERE LEN(typecode)=5 and isvalid=1  AND PrjState<>17  AND ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )", usercode, usercode);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            if (!string.IsNullOrEmpty(codes))
            {
                if (install == 1)
                {
                    builder.AppendFormat(" and prjGuid not in ({0})", codes);
                }
                else
                {
                    builder.AppendFormat(" and prjGuid in ({0})", codes);
                }
            }
            builder.Append(")");
            builder.AppendFormat(",CTEDATA AS(\r\nSELECT * FROM PT_PrjInfo WHERE TypeCode IN (SELECT left(TypeCode,5)  FROM CTE WHERE LEN(TypeCode)=10) \r\nUNION \r\nSELECT * FROM CTE WHERE LEN(TypeCode)=5 AND PrjState=17 AND TypeCode IN(\r\nSELECT left(TypeCode,5) FROM CTE WHERE TypeCode IN(SELECT TypeCode FROM CTE WHERE LEN(TypeCode)=5 AND PrjState=17) AND LEN(TypeCode)=10)\r\nUNION \r\nSELECT * FROM CTE WHERE LEN(TypeCode)=5 AND PrjState<>17 \r\nUNION \r\nSELECT * FROM CTE WHERE LEN(TypeCode)=10\r\n)", new object[0]);
            builder.Append("\r\nSELECT TOP (@pageSize) * FROM (\r\nSELECT Row_Number()over(ORDER BY userDefined_Date DESC,TypeCode ASC) as Num,* FROM \r\n(\r\nSELECT \r\nTResult.TypeCode,TResult.PrjCode,TResult.PrjName,TResult.PrjPlace,TResult.Owner,TResult.i_childnum,TResult.startdate,TResult.EndDate,TResult.PrjState,TResult.PrjCost,TResult.prjguid \r\n,CASE TResult.i_ChildNum\r\n\tWHEN '0' THEN (\r\n\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\tWHEN '5' THEN TResult.StartDate \r\n\t\t\t\t\t\tELSE (SELECT PT1.StartDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\tEND\r\n\t\t\t\t\t)\r\n\tELSE TResult.StartDate\r\nEND AS userDefined_Date,Detail.SetUpFlowState,TResult.ISDISPLAY\r\nFROM\r\n(");
            if (!string.IsNullOrEmpty(codes))
            {
                builder.AppendFormat("SELECT *,0 AS ISDISPLAY FROM CTEDATA WHERE prjGuid IN ({0})", codes);
                builder.AppendFormat("UNION ", new object[0]);
                builder.AppendFormat("SELECT *,1 AS ISDISPLAY FROM CTEDATA WHERE prjGuid NOT IN ({0})", codes);
            }
            else
            {
                builder.AppendFormat("SELECT *,1 AS ISDISPLAY FROM CTEDATA", new object[0]);
            }
            builder.Append("\r\n) AS TResult LEFT JOIN PT_PrjInfo_ZTB_Detail detail ON TResult.PrjGuid=detail.PrjGuid  WHERE TResult.PrjGuid NOT IN\r\n(SELECT info.PrjGuid FROM PT_PrjInfo info left join PT_PrjInfo_ZTB_Detail detail ON info.prjGuid=detail.prjGuid\r\nwhere SetUpFlowState<>1 AND LEN(TypeCode)=10 AND PrjState<>17)\r\n) AS tbPrjInfo) AS Tab WHERE Num > @pageSize * (@pageIndex - 1)\r\n");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pageSize", SqlDbType.Int, 4), new SqlParameter("@pageIndex", SqlDbType.Int, 4) };
            commandParameters[0].Value = pageSize;
            commandParameters[1].Value = pageIndex;
            DataTable table = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int GetProjectCount(string usercode, string prjcode, string prjname)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM(select * from PT_PrjInfo where TypeCode in (\r\nselect TypeCode from (\r\nSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1 AND PrjState not in('1','2','3','4','6','14','15','16','17','18') AND   ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )) \r\nunion SELECT   *  FROM  PT_PrjInfo \r\nWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND PrjState not in('1','2','3','4','6','14','15','16','17','18')  AND   ( Podepom LIKE '%{2}%' or  PrjManager LIKE '%{3}%' ) ) as project ", new object[] { usercode, usercode, usercode, usercode });
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Append(" )");
            builder.Append(") AS Prj");
            builder.AppendFormat(" UNION SELECT * FROM PT_PrjInfo WHERE LEN(typecode)=5 and isvalid=1  AND PrjState not in('1','2','3','4','6','14','15','16','17','18')  AND ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )", usercode, usercode);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Insert(0, "\r\nSELECT COUNT(*) FROM ( \r\n\r\nSELECT \r\nTResult.TypeCode,TResult.PrjCode,TResult.PrjName,TResult.PrjPlace,TResult.Owner,TResult.i_childnum,TResult.startdate,TResult.EndDate,TResult.PrjState,TResult.PrjCost,TResult.prjguid \r\n,CASE TResult.i_ChildNum\r\n\tWHEN '0' THEN (\r\n\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\tWHEN '5' THEN TResult.StartDate \r\n\t\t\t\t\t\tELSE (SELECT PT1.StartDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\tEND\r\n\t\t\t\t\t)\r\n\tELSE TResult.StartDate\r\nEND AS userDefined_Date\r\nFROM\r\n(");
            builder.Append("\r\n) AS TResult  WHERE TResult.PrjGuid NOT IN\r\n(SELECT info.PrjGuid FROM PT_PrjInfo info left join PT_PrjInfo_ZTB_Detail detail ON info.prjGuid=detail.prjGuid\r\nwhere SetUpFlowState<>1 AND LEN(TypeCode)=10 AND PrjState not in('1','2','3','4','6','14','15','16','17','18'))\r\n\r\nunion \r\nselect '00000' as TypeCode,PrjInfoZTB.PrjCode,PrjInfoZTB.PrjName,PrjInfoZTB.PrjPlace,PrjInfoZTB.Owner,0 as i_childnum,\r\nPrjInfoZTB.startdate,PrjInfoZTB.EndDate,PrjInfoZTB.PrjState,PrjInfoZTB.PrjCost,PrjInfoZTB.prjguid \r\n,PrjInfoZTB.StartDate as userDefined_Date\r\nfrom \r\n(select PT_PrjInfo_ZTB.* from PT_PrjInfo_ZTB \r\njoin PT_PrjInfo_ZTB_Detail on PT_PrjInfo_ZTB.PrjGuid=PT_PrjInfo_ZTB_Detail.PrjGuid\r\nand PT_PrjInfo_ZTB_Detail.ProjFlowSate =1\r\nwhere PT_PrjInfo_ZTB.PrjState=5 and PT_PrjInfo_ZTB.bidFlowState='1' and  PT_PrjInfo_ZTB.PrjGuid not in(select PrjGuid from PT_PrjInfo)) PrjInfoZTB\r\njoin Pt_PrjInfo_ZTB_User on PrjInfoZTB.prjguid=Pt_PrjInfo_ZTB_User.prjguid and (Pt_PrjInfo_ZTB_User.UserCode LIKE '%" + usercode + "%')");
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjName like '%{0}%' ", prjname);
            }
            builder.AppendLine();
            builder.Append("\r\n) AS Tab ");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null));
        }

        public int GetProjectCountByCodes(string usercode, string prjcode, string prjname, string codes, int install)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("WITH CTE AS(SELECT * FROM(select * from PT_PrjInfo where TypeCode in (\r\nselect  TypeCode from (\r\nSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1  AND PrjState<>17 AND   ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )) \r\nunion SELECT   *  FROM  PT_PrjInfo \r\nWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND PrjState<>17 AND   ( Podepom LIKE '%{2}%' or  PrjManager LIKE '%{3}%' ) ) as project ", new object[] { usercode, usercode, usercode, usercode });
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            if (!string.IsNullOrEmpty(codes))
            {
                if (install == 1)
                {
                    builder.AppendFormat(" and prjGuid not in ({0})", codes);
                }
                else
                {
                    builder.AppendFormat(" and prjGuid in ({0})", codes);
                }
            }
            builder.Append(" )");
            builder.Append(") AS Prj");
            builder.AppendFormat(" UNION SELECT * FROM PT_PrjInfo WHERE LEN(typecode)=5 and isvalid=1  AND PrjState<>17  AND ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )", usercode, usercode);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            if (!string.IsNullOrEmpty(codes))
            {
                if (install == 1)
                {
                    builder.AppendFormat(" and prjGuid not in ({0})", codes);
                }
                else
                {
                    builder.AppendFormat(" and prjGuid in ({0})", codes);
                }
            }
            builder.Append(")");
            builder.AppendFormat(",CTEDATA AS(\r\nSELECT * FROM PT_PrjInfo WHERE TypeCode IN (SELECT left(TypeCode,5)  FROM CTE WHERE LEN(TypeCode)=10) \r\nUNION \r\nSELECT * FROM CTE WHERE LEN(TypeCode)=5 AND PrjState=17 AND TypeCode IN(\r\nSELECT left(TypeCode,5) FROM CTE WHERE TypeCode IN(SELECT TypeCode FROM CTE WHERE LEN(TypeCode)=5 AND PrjState=17) AND LEN(TypeCode)=10)\r\nUNION \r\nSELECT * FROM CTE WHERE LEN(TypeCode)=5 AND PrjState<>17 \r\nUNION \r\nSELECT * FROM CTE WHERE LEN(TypeCode)=10\r\n)", new object[0]);
            builder.Append("\r\nSELECT COUNT(*) FROM (\r\nSELECT \r\nTResult.TypeCode,TResult.PrjCode,TResult.PrjName,TResult.PrjPlace,TResult.Owner,TResult.i_childnum,TResult.startdate,TResult.EndDate,TResult.PrjState,TResult.PrjCost,TResult.prjguid \r\n,CASE TResult.i_ChildNum\r\n\tWHEN '0' THEN (\r\n\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\tWHEN '5' THEN TResult.StartDate \r\n\t\t\t\t\t\tELSE (SELECT PT1.StartDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\tEND\r\n\t\t\t\t\t)\r\n\tELSE TResult.StartDate\r\nEND AS userDefined_Date,TResult.ISDISPLAY\r\nFROM\r\n(");
            if (!string.IsNullOrEmpty(codes))
            {
                builder.AppendFormat("SELECT *,0 AS ISDISPLAY FROM CTEDATA WHERE prjGuid IN ({0})", codes);
                builder.AppendFormat("UNION ", new object[0]);
                builder.AppendFormat("SELECT *,1 AS ISDISPLAY FROM CTEDATA WHERE prjGuid NOT IN ({0})", codes);
            }
            else
            {
                builder.AppendFormat("SELECT *,1 AS ISDISPLAY FROM CTEDATA", new object[0]);
            }
            builder.Append("\r\n) AS TResult  WHERE TResult.PrjGuid NOT IN\r\n(SELECT info.PrjGuid FROM PT_PrjInfo info left join PT_PrjInfo_ZTB_Detail detail ON info.prjGuid=detail.prjGuid\r\nwhere SetUpFlowState<>1 AND LEN(TypeCode)=10 AND PrjState<>17)\r\n) AS tbPrjInfo\r\n");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null));
        }

        public DataTable GetProjectIncoment(string usercode, string prjcode, string prjname, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetProjectIncomentCount(usercode, prjcode, prjname);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM( select * from PT_PrjInfo where TypeCode in (\r\nselect TypeCode from (\r\nSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1  AND PrjState in('5','7','8','9','10','11','12','13')  AND   ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )) \r\nunion SELECT   *  FROM  PT_PrjInfo \r\nWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND PrjState not in('1','2','3','4','6','14','15','16','17','18')  AND   ( Podepom LIKE '%{2}%' or  PrjManager LIKE '%{3}%' ) ) as project ", new object[] { usercode, usercode, usercode, usercode });
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Append(" )");
            builder.Append(") AS Prj");
            builder.AppendFormat(" UNION SELECT * FROM PT_PrjInfo WHERE LEN(typecode)=5 and isvalid=1  AND PrjState in('5','7','8','9','10','11','12','13')  AND ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )", usercode, usercode);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Insert(0, "\r\nSELECT TOP (@pageSize) * FROM (\r\nSELECT Row_Number()over(ORDER BY userDefined_Date DESC,TypeCode ASC) as Num,* FROM \r\n(\r\nSELECT \r\nTResult.TypeCode,TResult.PrjCode,TResult.PrjName,TResult.PrjPlace,TResult.Owner,TResult.i_childnum,TResult.startdate,TResult.EndDate,TResult.PrjState,TResult.PrjCost,TResult.prjguid,TResult.PrjFundInfo\r\n,ISNULL(Detail.PrjFundWorkable,'') AS PrjFundWorkable,ISNULL(Detail.ForecastProfitRate,0) AS ForecastProfitRate\r\n,ISNULL(CodeList.CodeName,'') AS QualityClassName,CASE TResult.i_ChildNum\r\n\tWHEN '0' THEN (\r\n\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\tWHEN '5' THEN TResult.StartDate \r\n\t\t\t\t\t\tELSE (SELECT PT1.StartDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\tEND\r\n\t\t\t\t\t)\r\n\tELSE TResult.StartDate\r\nEND AS userDefined_Date,Detail.SetUpFlowState,PrjKindClass\r\nFROM\r\n(");
            builder.Append("\r\n) AS TResult\r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON TResult.PrjGuid=Detail.PrjGuid\r\nLEFT JOIN (SELECT * FROM dbo.XPM_Basic_CodeList WHERE TypeId = (SELECT TypeId FROM dbo.XPM_Basic_CodeType WHERE SignCode='ProjectQuality')) AS CodeList ON TResult.QualityClass=CodeList.CodeId \r\nWHERE TResult.PrjGuid NOT IN\r\n(SELECT info.PrjGuid FROM PT_PrjInfo info left join PT_PrjInfo_ZTB_Detail detail ON info.prjGuid=detail.prjGuid\r\nwhere SetUpFlowState<>1 AND LEN(TypeCode)=10 AND PrjState not in('1','2','3','4','6','14','15','16','17','18'))\r\n\r\nunion\r\n\r\nselect '00000' as TypeCode,PrjInfoZTB.PrjCode,PrjInfoZTB.PrjName,PrjInfoZTB.PrjPlace,PrjInfoZTB.Owner,0 as i_childnum,\r\nPrjInfoZTB.startdate,PrjInfoZTB.EndDate,PrjInfoZTB.PrjState,PrjInfoZTB.PrjCost,PrjInfoZTB.prjguid,PrjInfoZTB.PrjFundInfo,\r\nISNULL(Detail.PrjFundWorkable,'') AS PrjFundWorkable,ISNULL(Detail.ForecastProfitRate,0) AS ForecastProfitRate,\r\nISNULL(CodeList.CodeName,'') AS QualityClassName\r\n,PrjInfoZTB.StartDate as userDefined_Date,1 as SetUpFlowState,PrjKindClass\r\nfrom \r\n(select PT_PrjInfo_ZTB.* from PT_PrjInfo_ZTB \r\njoin PT_PrjInfo_ZTB_Detail on PT_PrjInfo_ZTB.PrjGuid=PT_PrjInfo_ZTB_Detail.PrjGuid\r\nand PT_PrjInfo_ZTB_Detail.ProjFlowSate =1\r\nwhere PT_PrjInfo_ZTB.PrjState=5 and PT_PrjInfo_ZTB.bidFlowState='1' and  PT_PrjInfo_ZTB.PrjGuid not in(select PrjGuid from PT_PrjInfo)) PrjInfoZTB\r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON PrjInfoZTB.PrjGuid=Detail.PrjGuid\r\nLEFT JOIN (SELECT * FROM dbo.XPM_Basic_CodeList WHERE TypeId = (SELECT TypeId FROM dbo.XPM_Basic_CodeType WHERE SignCode='ProjectQuality')) AS CodeList ON PrjInfoZTB.QualityClass=CodeList.CodeId \r\njoin Pt_PrjInfo_ZTB_User on PrjInfoZTB.prjguid=Pt_PrjInfo_ZTB_User.prjguid and (Pt_PrjInfo_ZTB_User.UserCode LIKE '%" + usercode + "%')");
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjName like '%{0}%' ", prjname);
            }
            builder.AppendLine();
            builder.Append("\r\n) AS tbPrjInfo \r\nleft join (SELECT CodeID,CodeName FROM XPM_Basic_CodeList Where TypeID=(SELECT TypeID FROM XPM_Basic_CodeType Where SignCode='ProjectType') and ISvalid=1) PrjType on tbPrjInfo.PrjKindClass=PrjType.CodeID\r\n)AS Tab WHERE Num > @pageSize * (@pageIndex - 1)\r\n");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pageSize", SqlDbType.Int, 4), new SqlParameter("@pageIndex", SqlDbType.Int, 4) };
            commandParameters[0].Value = pageSize;
            commandParameters[1].Value = pageIndex;
            DataTable table = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int GetProjectIncomentCount(string usercode, string prjcode, string prjname)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM( select * from PT_PrjInfo where TypeCode in (\r\nselect TypeCode from (\r\nSELECT   *  FROM  PT_PrjInfo WHERE  TypeCode in(SELECT left(TYPECODE,5) FROM  PT_PrjInfo WHERE  i_ChildNum=0 and isvalid=1  AND PrjState not in('1','2','3','4','6','14','15','16','17','18')  AND   ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )) \r\nunion SELECT   *  FROM  PT_PrjInfo \r\nWHERE len(typecode)=10 and  i_ChildNum=0 and isvalid=1  AND PrjState not in('1','2','3','4','6','14','15','16','17','18')  AND   ( Podepom LIKE '%{2}%' or  PrjManager LIKE '%{3}%' ) ) as project ", new object[] { usercode, usercode, usercode, usercode });
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Append(" )");
            builder.Append(") AS Prj");
            builder.AppendFormat(" UNION SELECT * FROM PT_PrjInfo WHERE LEN(typecode)=5 and isvalid=1  AND PrjState not in('1','2','3','4','6','14','15','16','17','18')  AND ( Podepom LIKE '%{0}%' or  PrjManager LIKE '%{1}%' )", usercode, usercode);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjName like '%{0}%' ", prjname);
            }
            if (this.isNewProject == "1")
            {
                builder.Append(" AND PrjGuid IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1) ");
            }
            builder.Insert(0, "\r\nSELECT COUNT(*) FROM (\r\nSELECT \r\nTResult.TypeCode,TResult.PrjCode,TResult.PrjName,TResult.PrjPlace,TResult.Owner,TResult.i_childnum,TResult.startdate,TResult.EndDate,TResult.PrjState,TResult.PrjCost,TResult.prjguid,TResult.PrjFundInfo\r\n,ISNULL(Detail.PrjFundWorkable,'') AS PrjFundWorkable,ISNULL(Detail.ForecastProfitRate,0) AS ForecastProfitRate\r\n,ISNULL(CodeList.CodeName,'') AS QualityClassName,CASE TResult.i_ChildNum\r\n\tWHEN '0' THEN (\r\n\t\t\t\t\tCASE LEN(TResult.TypeCode)\r\n\t\t\t\t\t\tWHEN '5' THEN TResult.StartDate \r\n\t\t\t\t\t\tELSE (SELECT PT1.StartDate FROM  PT_PrjInfo  AS PT1 WHERE PT1.TypeCode = LEFT(TResult.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')\r\n\t\t\t\t\tEND\r\n\t\t\t\t\t)\r\n\tELSE TResult.StartDate\r\nEND AS userDefined_Date\r\nFROM\r\n(");
            builder.Append("\r\n) AS TResult \r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON TResult.PrjGuid=Detail.PrjGuid\r\nLEFT JOIN (SELECT * FROM dbo.XPM_Basic_CodeList WHERE TypeId = (SELECT TypeId FROM dbo.XPM_Basic_CodeType WHERE SignCode='ProjectQuality')) AS CodeList ON TResult.QualityClass=CodeList.CodeId \r\nWHERE TResult.PrjGuid NOT IN\r\n(SELECT info.PrjGuid FROM PT_PrjInfo info left join PT_PrjInfo_ZTB_Detail detail ON info.prjGuid=detail.prjGuid\r\nwhere SetUpFlowState<>1 AND LEN(TypeCode)=10 AND PrjState not in('1','2','3','4','6','14','15','16','17','18'))\r\n\r\nunion\r\n\r\nselect '00000' as TypeCode,PrjInfoZTB.PrjCode,PrjInfoZTB.PrjName,PrjInfoZTB.PrjPlace,PrjInfoZTB.Owner,0 as i_childnum,\r\nPrjInfoZTB.startdate,PrjInfoZTB.EndDate,PrjInfoZTB.PrjState,PrjInfoZTB.PrjCost,PrjInfoZTB.prjguid,PrjInfoZTB.PrjFundInfo,\r\nISNULL(Detail.PrjFundWorkable,'') AS PrjFundWorkable,ISNULL(Detail.ForecastProfitRate,0) AS ForecastProfitRate,\r\nISNULL(CodeList.CodeName,'') AS QualityClassName\r\n,PrjInfoZTB.StartDate as userDefined_Date\r\nfrom \r\n(select PT_PrjInfo_ZTB.* from PT_PrjInfo_ZTB \r\njoin PT_PrjInfo_ZTB_Detail on PT_PrjInfo_ZTB.PrjGuid=PT_PrjInfo_ZTB_Detail.PrjGuid\r\nand PT_PrjInfo_ZTB_Detail.ProjFlowSate =1\r\nwhere PT_PrjInfo_ZTB.PrjState=5 and PT_PrjInfo_ZTB.bidFlowState='1' and  PT_PrjInfo_ZTB.PrjGuid not in(select PrjGuid from PT_PrjInfo)) PrjInfoZTB\r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON PrjInfoZTB.PrjGuid=Detail.PrjGuid\r\nLEFT JOIN (SELECT * FROM dbo.XPM_Basic_CodeList WHERE TypeId = (SELECT TypeId FROM dbo.XPM_Basic_CodeType WHERE SignCode='ProjectQuality')) AS CodeList ON PrjInfoZTB.QualityClass=CodeList.CodeId \r\njoin Pt_PrjInfo_ZTB_User on PrjInfoZTB.prjguid=Pt_PrjInfo_ZTB_User.prjguid and (Pt_PrjInfo_ZTB_User.UserCode LIKE '%" + usercode + "%')");
            builder.AppendLine();
            builder.Append(" where 1=1");
            if (!string.IsNullOrEmpty(prjcode))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjCode like '%{0}%' ", prjcode);
            }
            if (!string.IsNullOrEmpty(prjname))
            {
                builder.AppendFormat(" and PrjInfoZTB.PrjName like '%{0}%' ", prjname);
            }
            builder.AppendLine();
            builder.Append("\r\n) AS prjList");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null));
        }

        public DataTable GetTableByPrjGuid(string prjGuid)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from PT_PrjInfo_ZTB where prjGuid=@prjGuid ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@prjGuid", prjGuid) });
        }

        public PrjInfoModel ReaderBind(IDataReader dataReader)
        {
            PrjInfoModel model = new PrjInfoModel {
                TypeCode = dataReader["TypeCode"].ToString()
            };
            object obj2 = dataReader["i_xh"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.i_xh = (int) obj2;
            }
            model.IsValid = dataReader["IsValid"].ToString();
            model.UserCode = dataReader["UserCode"].ToString();
            obj2 = dataReader["RecordDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.RecordDate = (DateTime) obj2;
            }
            obj2 = dataReader["i_ChildNum"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.i_ChildNum = (int) obj2;
            }
            model.PrjCode = dataReader["PrjCode"].ToString();
            obj2 = dataReader["PrjGuid"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.PrjGuid = new Guid(obj2.ToString());
            }
            model.PrjName = dataReader["PrjName"].ToString();
            obj2 = dataReader["StartDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.StartDate = (DateTime) obj2;
            }
            obj2 = dataReader["EndDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.EndDate = (DateTime) obj2;
            }
            model.ComputeClass = dataReader["ComputeClass"].ToString();
            model.RationClass = dataReader["RationClass"].ToString();
            obj2 = dataReader["PrjCost"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.PrjCost = Convert.ToDecimal(obj2);
            }
            model.ContractSum = dataReader["ContractSum"].ToString();
            model.Duration = dataReader["Duration"].ToString();
            model.QualityClass = dataReader["QualityClass"].ToString();
            model.Area = dataReader["Area"].ToString();
            model.PrjKindClass = dataReader["PrjKindClass"].ToString();
            model.PrjPlace = dataReader["PrjPlace"].ToString();
            model.Remark1 = dataReader["Remark1"].ToString();
            model.FileName = dataReader["FileName"].ToString();
            model.FileURL = dataReader["FileURL"].ToString();
            model.Owner = dataReader["Owner"].ToString();
            model.Counsellor = dataReader["Counsellor"].ToString();
            model.Designer = dataReader["Designer"].ToString();
            model.Inspector = dataReader["Inspector"].ToString();
            model.PrjInfo = dataReader["PrjInfo"].ToString();
            obj2 = dataReader["PrjState"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.PrjState = (int) obj2;
            }
            obj2 = dataReader["OwnerCode"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.OwnerCode = (int) obj2;
            }
            obj2 = dataReader["MarketInfoGuid"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.MarketInfoGuid = new Guid(obj2.ToString());
            }
            model.Rank = dataReader["Rank"].ToString();
            model.BudgetWay = dataReader["BudgetWay"].ToString();
            model.ContractWay = dataReader["ContractWay"].ToString();
            model.PayCondition = dataReader["PayCondition"].ToString();
            model.TenderWay = dataReader["TenderWay"].ToString();
            model.PayWay = dataReader["PayWay"].ToString();
            model.KeyPart = dataReader["KeyPart"].ToString();
            model.WorkUnit = dataReader["WorkUnit"].ToString();
            model.LinkMan = dataReader["LinkMan"].ToString();
            model.PrjManager = dataReader["PrjManager"].ToString();
            model.BuildingType = dataReader["BuildingType"].ToString();
            model.TotalHouseNum = dataReader["TotalHouseNum"].ToString();
            model.BuildingArea = dataReader["BuildingArea"].ToString();
            model.UsegrounArea = dataReader["UsegrounArea"].ToString();
            model.UndergroundArea = dataReader["UndergroundArea"].ToString();
            model.PrjFundInfo = dataReader["PrjFundInfo"].ToString();
            model.OtherStatement = dataReader["OtherStatement"].ToString();
            model.Podepom = dataReader["Podepom"].ToString();
            obj2 = dataReader["IsConfirm"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IsConfirm = (bool) obj2;
            }
            model.PrjStateRemark = dataReader["PrjStateRemark"].ToString();
            return model;
        }

        public int update(string PrjGuid, string grade, string businessman, string telephone, string _table)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE " + _table + " SET grade = '" + grade + "',businessman = '" + businessman + "',telephone ='" + telephone + "' WHERE PrjGuid='" + PrjGuid + "';");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Update(PrjInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_PrjInfo set ");
            builder.Append("i_xh=@i_xh,");
            builder.Append("IsValid=@IsValid,");
            builder.Append("UserCode=@UserCode,");
            builder.Append("RecordDate=@RecordDate,");
            builder.Append("i_ChildNum=@i_ChildNum,");
            builder.Append("PrjCode=@PrjCode,");
            builder.Append("PrjGuid=@PrjGuid,");
            builder.Append("PrjName=@PrjName,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("ComputeClass=@ComputeClass,");
            builder.Append("RationClass=@RationClass,");
            builder.Append("PrjCost=@PrjCost,");
            builder.Append("ContractSum=@ContractSum,");
            builder.Append("Duration=@Duration,");
            builder.Append("QualityClass=@QualityClass,");
            builder.Append("Area=@Area,");
            builder.Append("PrjKindClass=@PrjKindClass,");
            builder.Append("PrjPlace=@PrjPlace,");
            builder.Append("Remark1=@Remark1,");
            builder.Append("FileName=@FileName,");
            builder.Append("FileURL=@FileURL,");
            builder.Append("Owner=@Owner,");
            builder.Append("Counsellor=@Counsellor,");
            builder.Append("Designer=@Designer,");
            builder.Append("Inspector=@Inspector,");
            builder.Append("PrjInfo=@PrjInfo,");
            builder.Append("PrjState=@PrjState,");
            builder.Append("OwnerCode=@OwnerCode,");
            builder.Append("MarketInfoGuid=@MarketInfoGuid,");
            builder.Append("Rank=@Rank,");
            builder.Append("BudgetWay=@BudgetWay,");
            builder.Append("ContractWay=@ContractWay,");
            builder.Append("PayCondition=@PayCondition,");
            builder.Append("TenderWay=@TenderWay,");
            builder.Append("PayWay=@PayWay,");
            builder.Append("KeyPart=@KeyPart,");
            builder.Append("WorkUnit=@WorkUnit,");
            builder.Append("LinkMan=@LinkMan,");
            builder.Append("PrjManager=@PrjManager,");
            builder.Append("BuildingType=@BuildingType,");
            builder.Append("TotalHouseNum=@TotalHouseNum,");
            builder.Append("BuildingArea=@BuildingArea,");
            builder.Append("UsegrounArea=@UsegrounArea,");
            builder.Append("UndergroundArea=@UndergroundArea,");
            builder.Append("PrjFundInfo=@PrjFundInfo,");
            builder.Append("OtherStatement=@OtherStatement,");
            builder.Append("Podepom=@Podepom,");
            builder.Append("IsConfirm=@IsConfirm,");
            builder.Append("PrjStateRemark=@PrjStateRemark");
            builder.Append(" where TypeCode=@TypeCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@TypeCode", SqlDbType.VarChar, 0x18), new SqlParameter("@i_xh", SqlDbType.Int, 4), new SqlParameter("@IsValid", SqlDbType.Char, 1), new SqlParameter("@UserCode", SqlDbType.VarChar, 8), new SqlParameter("@RecordDate", SqlDbType.DateTime), new SqlParameter("@i_ChildNum", SqlDbType.Int, 4), new SqlParameter("@PrjCode", SqlDbType.VarChar, 30), new SqlParameter("@PrjGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PrjName", SqlDbType.VarChar, 100), new SqlParameter("@StartDate", SqlDbType.DateTime), new SqlParameter("@EndDate", SqlDbType.DateTime), new SqlParameter("@ComputeClass", SqlDbType.VarChar, 30), new SqlParameter("@RationClass", SqlDbType.VarChar, 30), new SqlParameter("@PrjCost", SqlDbType.Float, 8), new SqlParameter("@ContractSum", SqlDbType.Char, 10), new SqlParameter("@Duration", SqlDbType.VarChar, 50), 
                new SqlParameter("@QualityClass", SqlDbType.VarChar, 30), new SqlParameter("@Area", SqlDbType.VarChar, 50), new SqlParameter("@PrjKindClass", SqlDbType.VarChar, 30), new SqlParameter("@PrjPlace", SqlDbType.VarChar, 100), new SqlParameter("@Remark1", SqlDbType.VarChar, 0xfa0), new SqlParameter("@FileName", SqlDbType.VarChar, 200), new SqlParameter("@FileURL", SqlDbType.VarChar, 100), new SqlParameter("@Owner", SqlDbType.VarChar, 100), new SqlParameter("@Counsellor", SqlDbType.VarChar, 100), new SqlParameter("@Designer", SqlDbType.VarChar, 100), new SqlParameter("@Inspector", SqlDbType.VarChar, 100), new SqlParameter("@PrjInfo", SqlDbType.VarChar, 0xfa0), new SqlParameter("@PrjState", SqlDbType.Int, 4), new SqlParameter("@OwnerCode", SqlDbType.Int, 4), new SqlParameter("@MarketInfoGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Rank", SqlDbType.VarChar, 50), 
                new SqlParameter("@BudgetWay", SqlDbType.VarChar, 50), new SqlParameter("@ContractWay", SqlDbType.VarChar, 50), new SqlParameter("@PayCondition", SqlDbType.VarChar, 50), new SqlParameter("@TenderWay", SqlDbType.VarChar, 50), new SqlParameter("@PayWay", SqlDbType.VarChar, 50), new SqlParameter("@KeyPart", SqlDbType.VarChar, 50), new SqlParameter("@WorkUnit", SqlDbType.VarChar, 100), new SqlParameter("@LinkMan", SqlDbType.VarChar, 50), new SqlParameter("@PrjManager", SqlDbType.VarChar, 50), new SqlParameter("@BuildingType", SqlDbType.VarChar, 50), new SqlParameter("@TotalHouseNum", SqlDbType.VarChar, 50), new SqlParameter("@BuildingArea", SqlDbType.VarChar, 50), new SqlParameter("@UsegrounArea", SqlDbType.VarChar, 50), new SqlParameter("@UndergroundArea", SqlDbType.VarChar, 50), new SqlParameter("@PrjFundInfo", SqlDbType.VarChar, 500), new SqlParameter("@OtherStatement", SqlDbType.VarChar, 500), 
                new SqlParameter("@Podepom", SqlDbType.VarChar, 0xfa0), new SqlParameter("@IsConfirm", SqlDbType.Bit, 1), new SqlParameter("@PrjStateRemark", SqlDbType.VarChar, 500)
             };
            commandParameters[0].Value = model.TypeCode;
            commandParameters[1].Value = model.i_xh;
            commandParameters[2].Value = model.IsValid;
            commandParameters[3].Value = model.UserCode;
            commandParameters[4].Value = model.RecordDate;
            commandParameters[5].Value = model.i_ChildNum;
            commandParameters[6].Value = model.PrjCode;
            commandParameters[7].Value = model.PrjGuid;
            commandParameters[8].Value = model.PrjName;
            commandParameters[9].Value = model.StartDate;
            commandParameters[10].Value = model.EndDate;
            commandParameters[11].Value = model.ComputeClass;
            commandParameters[12].Value = model.RationClass;
            commandParameters[13].Value = model.PrjCost;
            commandParameters[14].Value = model.ContractSum;
            commandParameters[15].Value = model.Duration;
            commandParameters[0x10].Value = model.QualityClass;
            commandParameters[0x11].Value = model.Area;
            commandParameters[0x12].Value = model.PrjKindClass;
            commandParameters[0x13].Value = model.PrjPlace;
            commandParameters[20].Value = model.Remark1;
            commandParameters[0x15].Value = model.FileName;
            commandParameters[0x16].Value = model.FileURL;
            commandParameters[0x17].Value = model.Owner;
            commandParameters[0x18].Value = model.Counsellor;
            commandParameters[0x19].Value = model.Designer;
            commandParameters[0x1a].Value = model.Inspector;
            commandParameters[0x1b].Value = model.PrjInfo;
            commandParameters[0x1c].Value = model.PrjState;
            commandParameters[0x1d].Value = model.OwnerCode;
            commandParameters[30].Value = model.MarketInfoGuid;
            commandParameters[0x1f].Value = model.Rank;
            commandParameters[0x20].Value = model.BudgetWay;
            commandParameters[0x21].Value = model.ContractWay;
            commandParameters[0x22].Value = model.PayCondition;
            commandParameters[0x23].Value = model.TenderWay;
            commandParameters[0x24].Value = model.PayWay;
            commandParameters[0x25].Value = model.KeyPart;
            commandParameters[0x26].Value = model.WorkUnit;
            commandParameters[0x27].Value = model.LinkMan;
            commandParameters[40].Value = model.PrjManager;
            commandParameters[0x29].Value = model.BuildingType;
            commandParameters[0x2a].Value = model.TotalHouseNum;
            commandParameters[0x2b].Value = model.BuildingArea;
            commandParameters[0x2c].Value = model.UsegrounArea;
            commandParameters[0x2d].Value = model.UndergroundArea;
            commandParameters[0x2e].Value = model.PrjFundInfo;
            commandParameters[0x2f].Value = model.OtherStatement;
            commandParameters[0x30].Value = model.Podepom;
            commandParameters[0x31].Value = model.IsConfirm;
            commandParameters[50].Value = model.PrjStateRemark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

