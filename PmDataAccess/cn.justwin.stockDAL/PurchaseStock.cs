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

    public class PurchaseStock
    {
        private static string PurchasePriceType = ConfigHelper.Get("PurchasePriceType");

        public int Add(SqlTransaction trans, PurchaseStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Purchase_Stock(");
            builder.Append("psid,scode,pscode,number,sprice,corp,arrivalDate)");
            builder.Append(" values (");
            builder.Append("@psid,@scode,@pscode,@number,@sprice,@corp,@arrivalDate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@psid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@pscode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@arrivalDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.psid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.pscode;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.sprice;
            commandParameters[5].Value = model.corp;
            if ((model.ArrivalDate != null) && (model.ArrivalDate.ToString() != ""))
            {
                commandParameters[6].Value = DateTime.Parse(model.ArrivalDate);
            }
            else
            {
                commandParameters[6].Value = DBNull.Value;
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string psid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Purchase_Stock ");
            builder.Append(" where psid=@psid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@psid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = psid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string psid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Sm_Purchase_Stock ");
            builder.Append(" where psid=@psid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@psid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = psid;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public void Delete(SqlTransaction trans, List<string> psids)
        {
            foreach (string str in psids)
            {
                this.Delete(trans, str);
            }
        }

        public int DeleteByPscode(SqlTransaction trans, string pscode)
        {
            string cmdText = "delete from Sm_Purchase_Stock where pscode = @pscode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pscode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = pscode;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetBalanceStockByContractId(string contractId, string balanceId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("WITH ctePurchaseStock AS(   --采购单信息").AppendLine();
            builder.Append("    SELECT psid, scode, pscode, SUM(number) ContractNumber, sprice, corp").AppendLine();
            builder.Append("    FROM ( SELECT *  FROM Sm_Purchase_Stock WHERE pscode in ( SELECT Pcode FROM Sm_Purchase WHERE [Contract]=@contractId AND FlowState=1) ) PurchaseStock ").AppendLine();
            builder.Append("\tGROUP BY psid,scode,pscode,sprice,corp),").AppendLine();
            builder.Append("cteAllInStorgeStock AS(   --采购但材料已经入库的信息").AppendLine();
            builder.Append("    SELECT scode,ISNULL(SUM(number),0.00) AS ArrivaledQuantity,sprice,corp").AppendLine();
            builder.Append("    FROM ( SELECT storgeStock.*  FROM ctePurchaseStock").AppendLine();
            builder.Append("           INNER JOIN Sm_Storage_Stock storgeStock ON storgeStock.linkCode=ctePurchaseStock.pscode").AppendLine();
            builder.Append("\t\t\tAND storgeStock.Scode=ctePurchaseStock.Scode  AND storgeStock.sprice=ctePurchaseStock.sprice AND storgeStock.corp=ctePurchaseStock.corp").AppendLine();
            builder.Append("\t\t   LEFT JOIN Sm_Storage storage ON storgeStock.stCode=storage.sCode").AppendLine();
            builder.Append("           WHERE FlowState=1 AND Inflag=1 ) allInStorgeStock GROUP BY scode,sprice,corp),").AppendLine();
            builder.Append("conAlreadyBalanceInfo AS(  --已结算的材料信息").AppendLine();
            builder.Append("   SELECT PurchaseId,ISNULL(SUM(ArrivaledQuantity),0.00) AS AlreadyBalanceQuantity FROM Con_Balance_Stock").AppendLine();
            builder.Append("   WHERE BalanceId IN ( SELECT balanceId FROM Con_Payout_Balance WHERE ContractID=@contractId AND FlowState = 1 ) GROUP BY PurchaseId),").AppendLine();
            builder.Append("conPayoutBalanceInfo AS(  --本次结算的材料信息").AppendLine();
            builder.Append("   SELECT PurchaseId, ISNULL(ArrivaledQuantity,0.00) AS ThisTimeArrivaledQuantity,Balance.FlowState FROM Con_Balance_Stock AS Stock,Con_Payout_Balance AS Balance WHERE Stock.BalanceId = Balance.BalanceId AND Stock.BalanceId=@balanceId)").AppendLine();
            builder.Append("SELECT ctePurchaseStock.*,Res_Resource.ResourceId,Res_Resource.ResourceCode,ResourceName,Specification,Res_Unit.Name,CorpName,").AppendLine();
            builder.Append("\t   Res_Resource.Brand,ModelNumber,TechnicalParameter,cteAllInStorgeStock.ArrivaledQuantity,").AppendLine();
            builder.Append("       (ContractNumber-ISNULL(cteAllInStorgeStock.ArrivaledQuantity,0.00)) NoArrivaledQuantity,").AppendLine();
            builder.Append("       conPayoutBalanceInfo.ThisTimeArrivaledQuantity,").AppendLine();
            builder.Append("       CASE conPayoutBalanceInfo.FlowState WHEN 1 THEN ISNULL(conAlreadyBalanceInfo.AlreadyBalanceQuantity,0.00) - ISNULL(conPayoutBalanceInfo.ThisTimeArrivaledQuantity,0.00) ELSE ISNULL(conAlreadyBalanceInfo.AlreadyBalanceQuantity,0.00) END AS AllAlreadyBalanceQuantity,").AppendLine();
            builder.Append("       CASE conPayoutBalanceInfo.FlowState WHEN 1 THEN (ISNULL(conAlreadyBalanceInfo.AlreadyBalanceQuantity,0.00) - ISNULL(conPayoutBalanceInfo.ThisTimeArrivaledQuantity,0.00))*ctePurchaseStock.sprice ELSE ISNULL(conAlreadyBalanceInfo.AlreadyBalanceQuantity,0.00)*ctePurchaseStock.sprice END AS AlreadyTotal,").AppendLine();
            builder.Append("       ISNULL(conPayoutBalanceInfo.ThisTimeArrivaledQuantity,0.00)*ctePurchaseStock.sprice AS ThisTimeTotal").AppendLine();
            builder.Append("FROM ctePurchaseStock INNER JOIN Res_Resource ON ctePurchaseStock.scode = Res_Resource.ResourceCode").AppendLine();
            builder.Append("  LEFT JOIN Res_Unit ON Res_Unit.UnitID = Res_Resource.Unit").AppendLine();
            builder.Append("  LEFT JOIN XPM_Basic_ContactCorp ON ctePurchaseStock.corp = XPM_Basic_ContactCorp.CorpID").AppendLine();
            builder.Append("  LEFT JOIN cteAllInStorgeStock ON ResourceCode=cteAllInStorgeStock.scode AND ctePurchaseStock.sprice=cteAllInStorgeStock.sprice AND ctePurchaseStock.corp=cteAllInStorgeStock.corp ").AppendLine();
            builder.Append("  LEFT JOIN conAlreadyBalanceInfo ON psid=conAlreadyBalanceInfo.PurchaseId").AppendLine();
            builder.Append("  LEFT JOIN conPayoutBalanceInfo ON psid=conPayoutBalanceInfo.PurchaseId").AppendLine();
            builder.Append("WHERE pscode in ( SELECT Pcode FROM Sm_Purchase WHERE [Contract]=@contractId AND FlowState=1 )").AppendLine();
            builder.Append("ORDER BY pscode,ResourceCode DESC").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contractId", SqlDbType.NVarChar, 70), new SqlParameter("@balanceId", SqlDbType.NVarChar, 70) };
            commandParameters[0].Value = contractId;
            commandParameters[1].Value = balanceId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetDiff(string contractId, string pscode, string isWBSRelevance)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n            --DECLARE @PurchaseCode NVARCHAR(50), @ContractId NVARCHAR(50),@PriceTypeId NVARCHAR(50);\r\n            --set @ContractId='a10507d2-1193-4b32-8e56-086c0cf72bc2';\r\n            --SET @PurchaseCode='20130121115952';\r\n            --SET @PriceTypeId='ABC233A8-6C1E-4200-91EC-9223DBCDE390';\r\n            WITH ctePurchase AS\t\t--本次采购单信息\r\n            (\r\n\t            SELECT Resource.ResourceId,Stock.scode,r2.ResourceCode AS ParentResCode,r2.ResourceId AS ParentResId,\r\n\t            Stock.number AS PurNumber,Stock.sprice AS Tsprice,\r\n\t            Resource.ResourceName,Resource.Specification,Resource.Brand,Contract.PrjGuid FROM Sm_Purchase_Stock Stock \r\n\t            JOIN Sm_Purchase Purchase ON Stock.pscode=Purchase.pcode AND Purchase.contract=@ContractId\r\n\t            AND Stock.pscode=@PurchaseCode \r\n\t            JOIN dbo.Con_Payout_Contract Contract ON Purchase.Contract=Contract.ContractId\r\n\t            INNER JOIN Res_Resource Resource ON Stock.scode = Resource.ResourceCode\r\n\t            LEFT JOIN Res_Mapping m ON m.ResourceId = Resource.ResourceId\r\n\t            LEFT JOIN Res_Resource r2 ON r2.ResourceId = m.ParentId\r\n            ), cteContractRes AS\t\t\t--合同预算信息\r\n            (\r\n\t            select Bud_ContractResource.ResourcePrice,Bud_ContractTask.PrjId,Bud_ContractResource.Resourceid \r\n\t            from Bud_ContractTask \r\n\t            JOIN Bud_ContractResource on Bud_ContractResource.Taskid=Bud_ContractTask.Taskid\r\n\t            JOIN Bud_ContractTaskAudit ON Bud_ContractTaskAudit.PrjId=Bud_ContractTask.PrjId \r\n\t            AND Bud_ContractTaskAudit.flowstate=1 WHERE TaskType=''\r\n            ), ctePurchasedPrice AS\t\t\t--以往物资价信息\r\n            (\r\n\t            SELECT * FROM Res_Price WHERE PriceTypeId = @PriceTypeId\r\n            ), ctePurchasePlan AS\t\t\t--采购计划信息\r\n            (\r\n\t            SELECT Sm_Purchaseplan_Stock.scode,sum(Sm_Purchaseplan_Stock.number) AS number,Sm_Purchaseplan.project\r\n\t            FROM Sm_Purchaseplan_Stock \r\n\t            JOIN Sm_Purchaseplan ON Sm_Purchaseplan_Stock.ppcode=Sm_Purchaseplan.ppcode AND Sm_Purchaseplan.flowstate=1\r\n\t            GROUP BY Sm_Purchaseplan_Stock.scode,Sm_Purchaseplan.project\r\n            ), cteBud AS\t\t\t\t\t--预算信息\r\n            (\r\n            ");
            if (isWBSRelevance == "1")
            {
                builder.Append("\r\n\t\t\t\t    SELECT SUM(ResQuantity) ResourceQuantity,ISNULL(SUM(ResTotal)/NULLIF(SUM(ResQuantity),0),0) ResourcePrice,\r\n\t\t\t\t    ResourceId,PrjId FROM \r\n\t\t\t\t    (\r\n\t\t\t\t\t    SELECT (SUM(Bud_TaskResource.ResourceQuantity)*Bud_TaskResource.ResourcePrice) ResTotal,\r\n\t\t\t\t\t    SUM(Bud_TaskResource.ResourceQuantity) ResQuantity,Bud_TaskResource.ResourceId,Bud_Task.PrjId\r\n\t\t\t\t\t    FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n\t\t\t\t\t    JOIN  vGetCurBudVersion ON Bud_Task.PrjId=vGetCurBudVersion.PrjId \r\n\t\t\t\t\t    AND Bud_Task.version=vGetCurBudVersion.curversion WHERE TaskType=''\r\n\t\t\t\t\t    GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId,Bud_Task.PrjId\r\n\t\t\t\t\t    UNION ALL\r\n\t\t\t\t\t    SELECT (SUM(ResourceQuantity)*ResourcePrice) ReseTotal,SUM(ResourceQuantity) ResQuantity,\r\n\t\t\t\t\t    ResourceId,modify.PrjId FROM Bud_ModifyTaskRes modifyTaskRes\r\n\t\t\t\t\t    JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n\t\t\t\t\t    JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n\t\t\t\t\t    WHERE modify.FlowState='1' \r\n\t\t\t\t\t    GROUP BY ResourcePrice,ResourceId,modify.PrjId\r\n\t\t\t\t    ) allBudResInfo GROUP BY ResourceId,PrjId\r\n                ");
            }
            else
            {
                builder.Append("\r\n                    SELECT SUM(Bud_TaskResource.ResourceQuantity) ResourceQuantity,Bud_TaskResource.ResourcePrice,\r\n                    Bud_TaskResource.ResourceId,PrjGuid PrjId\r\n                    FROM Bud_TaskResource \r\n                    JOIN  vGetCurBudVersion ON PrjGuid=PrjId AND versions=curversion\r\n                    GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId,PrjGuid\r\n                ");
            }
            builder.Append("\r\n            ), cteParentRes AS\t\t\t\t--父节点信息\r\n            (\r\n\t            SELECT ParentResCode AS resCode,ISNULL(cteContractRes.ResourcePrice,0) ConResPrice,\r\n\t            0 AS PuredPrice,ISNULL(cteBud.ResourcePrice,0) AS BudPrice,\r\n\t            SUM(ISNULL(number,0)) AS PurPlanNumber,SUM(ISNULL(PurNumber,0)) AS PurNumber,\r\n\t            ISNULL(cteBud.ResourceQuantity,0) AS BudQuantity,0 AS PurPrice\r\n\t            FROM ctePurchase\r\n\t            LEFT JOIN cteContractRes ON ctePurchase.PrjGuid =cteContractRes.PrjId\r\n\t            AND ctePurchase.ParentResId=cteContractRes.ResourceId\r\n\t            LEFT JOIN ctePurchasedPrice ON ctePurchase.ParentResId = ctePurchasedPrice.ResourceId\r\n\t            LEFT JOIN ctePurchasePlan ON ctePurchase.ParentResCode=ctePurchasePlan.scode \r\n\t            AND ctePurchase.PrjGuid=ctePurchasePlan.project\r\n\t            LEFT JOIN cteBud ON ctePurchase.PrjGuid=cteBud.PrjId AND ctePurchase.ParentResId=cteBud.ResourceId\r\n\t            GROUP BY ctePurchase.ParentResId,ctePurchase.ParentResCode,cteContractRes.ResourcePrice,\r\n\t            PriceValue,cteBud.ResourcePrice,cteBud.ResourceQuantity\r\n\t            HAVING ParentResId IS NOT NULL\r\n            ), cteRes AS\t\t\t\t\t--资源信息\r\n            (\r\n\t            SELECT ctePurchase.scode AS resCode,ISNULL(cteContractRes.ResourcePrice,0) ConResPrice,\r\n\t            ISNULL(PriceValue,0) AS PuredPrice,ISNULL(cteBud.ResourcePrice,0) AS BudPrice,\r\n\t            ISNULL(number,0) AS PurPlanNumber,ISNULL(PurNumber,0) AS PurNumber,\r\n\t            ISNULL(cteBud.ResourceQuantity,0) AS BudQuantity,ISNULL(Tsprice,0) AS PurPrice\r\n\t            FROM ctePurchase\r\n\t            LEFT JOIN cteContractRes ON ctePurchase.PrjGuid =cteContractRes.PrjId\r\n\t            AND ctePurchase.ResourceId=cteContractRes.ResourceId\r\n\t            LEFT JOIN ctePurchasedPrice ON ctePurchase.ResourceId = ctePurchasedPrice.ResourceId\r\n\t            LEFT JOIN ctePurchasePlan ON ctePurchase.scode=ctePurchasePlan.scode \r\n\t            AND ctePurchase.PrjGuid=ctePurchasePlan.project\r\n\t            LEFT JOIN cteBud ON ctePurchase.PrjGuid=cteBud.PrjId AND ctePurchase.ResourceId=cteBud.ResourceId\r\n            ), cteAll AS\t\t\t\t\t--汇总\r\n            (\r\n\t            SELECT * FROM cteParentRes\r\n\t            UNION \r\n\t            SELECT * FROM cteRes\r\n            ), cte_no AS\t\t\t\t\t--用于排序\r\n            (\r\n                    SELECT * FROM\r\n                    (\r\n\t                    SELECT m.ResourceId,'' ResourceCode,m.ParentId + m.ResourceId AS ParentId\r\n\t                    FROM Res_Mapping AS m\r\n\t                    UNION\r\n\t                    SELECT r.ResourceId,r.ResourceCode, r.ResourceId AS ParentId\r\n\t                    FROM Res_Resource r\r\n\t                    WHERE r.ResourceId IN (SELECT ParentId FROM Res_Mapping)\r\n                    ) TA WHERE ResourceCode IN (SELECT resCode FROM cteAll)\r\n            )\r\n            SELECT resCode,ResourceName,Resource.Specification,\r\n            Resource.Brand,cteAll.ConResPrice,cteAll.PuredPrice,cteAll.BudPrice,cteAll.PurPlanNumber,cteAll.PurNumber,\r\n            cteAll.BudQuantity,cteAll.PurPrice FROM cteAll\r\n            INNER JOIN Res_Resource Resource ON cteAll.resCode = Resource.ResourceCode\r\n            LEFT JOIN cte_no AS no ON no.ResourceId = Resource.ResourceId\r\n            ORDER BY no.ParentId\r\n            ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PurchaseCode", pscode), new SqlParameter("@ContractId", contractId), new SqlParameter("@PriceTypeId", PurchasePriceType) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<PurchaseStockModel> GetList()
        {
            StringBuilder builder = new StringBuilder("SELECT * FROM Sm_Purchase_Stock");
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        private List<PurchaseStockModel> GetList(IDataReader dr)
        {
            List<PurchaseStockModel> list = new List<PurchaseStockModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        private PurchaseStockModel GetModel(IDataReader dr)
        {
            return new PurchaseStockModel { psid = DBHelper.GetString(dr["psid"]), scode = DBHelper.GetString(dr["scode"]), pscode = DBHelper.GetString(dr["pscode"]), number = DBHelper.GetDecimal(dr["number"]), sprice = DBHelper.GetDecimal(dr["sprice"]), corp = DBHelper.GetString(dr["corp"]), ArrivalDate = DBHelper.GetString(dr["arrivalDate"]) };
        }

        public PurchaseStockModel GetModel(string psid)
        {
            string cmdText = "select * from Sm_Purchase_Stock where psid = @psid";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@psid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = psid;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, cmdText, commandParameters))
            {
                if (reader.Read())
                {
                    return this.GetModel(reader);
                }
                return null;
            }
        }

        public DataTable GetModifyStockByContractId(string contractId, string modifyId)
        {
            string str = string.Empty;
            str = string.Format(" AND ModifyStock.ModifyId='{0}' ", modifyId);
            StringBuilder builder = new StringBuilder();
            builder.Append("select ModifyStockId,(ModifyStock.PurchaseId) psid,ModifyStock.scode,ModifyStock.pscode,ModifyStock.ArrivalDate, ").AppendLine();
            builder.Append("ISNULL(ModifyStock.Quantity,0) number,ISNULL(ModifyStock.sprice,0) sprice,ISNULL(ModifyStock.corp,'') corp, ").AppendLine();
            builder.Append("Res_Resource.ResourceId,Res_Resource.ResourceCode,ResourceName,Specification,Res_Unit.Name,ISNULL(CorpName,'') CorpName,arrivalDate,").AppendLine();
            builder.Append("ISNULL(ModifyStock.Quantity,0)*ISNULL(ModifyStock.sprice,0) Total ").AppendLine();
            builder.Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ").AppendLine();
            builder.Append(" from Con_Modify_Stock ModifyStock ").AppendLine();
            builder.Append("inner join Res_Resource on ModifyStock.scode = Res_Resource.ResourceCode  ");
            builder.Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ");
            builder.Append("left join XPM_Basic_ContactCorp on ModifyStock.corp = XPM_Basic_ContactCorp.CorpID ");
            builder.AppendFormat("where pscode in (SELECT Pcode FROM Sm_Purchase WHERE [Contract]=@contractId  AND FlowState=1 ) {0} ORDER BY pscode,ResourceCode DESC", str);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contractId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = contractId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetPrjID(string pscode)
        {
            string cmdText = "select Con_Payout_Contract.PrjGuid,Con_Payout_Contract.ContractID from Sm_Purchase left join Con_Payout_Contract on Sm_Purchase.contract = Con_Payout_Contract.ContractID where pcode = '" + pscode + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public DataTable GetPurchaseInfoByStorgeStock(string[] pscodes)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("WITH ctePurchaseStock AS\r\n                                    (\r\n\t                                    --采购单信息\r\n                                        SELECT scode,pscode,SUM(number) ContractNumber,sprice,corp FROM\r\n\t                                    (\r\n\t\t                                    SELECT * FROM Sm_Purchase_Stock\r\n\t\t                                    WHERE pscode in ({0})\r\n\t                                    ) PurchaseStock GROUP BY scode,pscode,sprice,corp\r\n                                    )\r\n                                    , cteAllInStorgeStock AS\r\n                                    (\r\n\t                                    --采购但材料已经入库的信息\r\n                                        SELECT scode,SUM(number) AllInNumber,sprice,corp FROM\r\n\t                                    (\r\n\t\t                                    SELECT storgeStock.* FROM ctePurchaseStock INNER JOIN Sm_Storage_Stock storgeStock \r\n\t\t                                    ON storgeStock.linkCode=ctePurchaseStock.pscode\r\n\t\t                                    AND storgeStock.Scode=ctePurchaseStock.Scode \r\n\t\t                                    AND storgeStock.sprice=ctePurchaseStock.sprice \r\n\t\t                                    AND storgeStock.corp=ctePurchaseStock.corp\r\n\t\t                                    LEFT JOIN Sm_Storage storage ON storgeStock.stCode=storage.sCode\r\n\t\t                                    WHERE FlowState=1 AND Inflag=1\r\n\t                                    ) allInStorgeStock GROUP BY scode,sprice,corp\r\n                                    )", DBHelper.GetInParameterSql(pscodes)).AppendLine();
            builder.AppendLine("SELECT ResourceId, currentStorageStock.pscode,ISNULL(ContractNumber,0) ContractNumber,ISNULL(AllInNumber,0) AllInNumber,");
            builder.AppendLine("currentStorageStock.number,currentStorageStock.sprice, (currentStorageStock.number*currentStorageStock.sprice) Total, ResourceCode, ResourceName, ");
            builder.AppendLine("Specification, Name as UnitName,currentStorageStock.Corp, CorpName,arrivalDate ,Res_Resource.Brand,ModelNumber,TechnicalParameter ");
            builder.AppendLine("FROM Res_Resource ");
            builder.AppendLine("INNER JOIN Sm_Purchase_Stock currentStorageStock ON currentStorageStock.scode = Res_Resource.ResourceCode ");
            builder.AppendLine("LEFT JOIN Res_Unit on Res_Unit.UnitId = Res_Resource.Unit ");
            builder.AppendLine("LEFT JOIN XPM_Basic_ContactCorp on currentStorageStock.corp = XPM_Basic_ContactCorp.CorpID ");
            builder.AppendLine("LEFT JOIN ctePurchaseStock ON ResourceCode=ctePurchaseStock.scode \r\n                                AND currentStorageStock.sprice=ctePurchaseStock.sprice \r\n                                AND currentStorageStock.corp=ctePurchaseStock.corp \r\n                                LEFT JOIN cteAllInStorgeStock ON ResourceCode=cteAllInStorgeStock.scode \r\n                                AND currentStorageStock.sprice=cteAllInStorgeStock.sprice \r\n                                AND currentStorageStock.corp=cteAllInStorgeStock.corp ");
            builder.AppendLine("WHERE currentStorageStock.pscode IN(").Append(DBHelper.GetInParameterSql(pscodes)).Append(")");
            builder.AppendLine(" ORDER BY ResourceCode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetPurchaseStockByContractId(string contractId, bool isConPurchase)
        {
            string str = string.Empty;
            if (isConPurchase)
            {
                str = " and IsConPurchase='true' ";
            }
            else
            {
                str = " AND FlowState=1 ";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("select Sm_Purchase_Stock.*,Res_Resource.ResourceId, Res_Resource.ResourceCode,ResourceName,Specification,Res_Unit.Name,CorpName,arrivalDate,Sm_Purchase_Stock.number*Sm_Purchase_Stock.sprice Total ").AppendLine();
            builder.Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter").AppendLine();
            builder.Append("from Sm_Purchase_Stock ").AppendLine();
            builder.Append("inner join Res_Resource on Sm_Purchase_Stock.scode = Res_Resource.ResourceCode  ");
            builder.Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ");
            builder.Append("left join XPM_Basic_ContactCorp on Sm_Purchase_Stock.corp = XPM_Basic_ContactCorp.CorpID ");
            builder.AppendFormat("where pscode in (SELECT Pcode FROM Sm_Purchase WHERE [Contract]=@contractId {0}) ORDER BY pscode,ResourceCode DESC", str);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contractId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = contractId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetPurchaseStockByPscode(string pscode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Sm_Purchase_Stock.*,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Res_Unit.Name,CorpName,Sm_Purchase_Stock.number*Sm_Purchase_Stock.sprice Total ,arrivalDate ").AppendLine();
            builder.Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ").AppendLine();
            builder.Append("from Sm_Purchase_Stock ").AppendLine();
            builder.Append("inner join Res_Resource on Sm_Purchase_Stock.scode = Res_Resource.ResourceCode  ");
            builder.Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ");
            builder.Append("left join XPM_Basic_ContactCorp on Sm_Purchase_Stock.corp = XPM_Basic_ContactCorp.CorpID ");
            builder.Append("where pscode = @pscode  ORDER BY ResourceCode DESC");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pscode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = pscode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetPurchaseStockByPscode(string pscode, string strPrjId, string isWBSRelevance)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Sm_Purchase_Stock.*,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Res_Unit.Name,CorpName,Sm_Purchase_Stock.number*Sm_Purchase_Stock.sprice Total ,arrivalDate ").AppendLine();
            builder.Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ").AppendLine();
            builder.Append(",isnull(data.ReadyNumber,0) as ReadyNumber,isnull(PriceValue,0) AS Price ");
            builder.Append("from Sm_Purchase_Stock ").AppendLine();
            builder.Append("inner join Res_Resource on Sm_Purchase_Stock.scode = Res_Resource.ResourceCode  ");
            builder.Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ");
            builder.Append("left join XPM_Basic_ContactCorp on Sm_Purchase_Stock.corp = XPM_Basic_ContactCorp.CorpID ");
            builder.Append("LEFT JOIN(");
            if (isWBSRelevance == "1")
            {
                builder.AppendFormat("\r\n\t                SELECT SUM(ResQuantity) AS ReadyNumber,ResourceCode FROM \r\n\t                (\r\n\t\t                SELECT (SUM(Bud_TaskResource.ResourceQuantity)*Bud_TaskResource.ResourcePrice) ResTotal,\r\n\t\t                SUM(Bud_TaskResource.ResourceQuantity) ResQuantity,Bud_TaskResource.ResourceId\r\n\t\t                FROM Bud_TaskResource JOIN Bud_Task ON Bud_TaskResource.TaskId=Bud_Task.TaskId\r\n\t\t                JOIN  vGetCurBudVersion ON Bud_Task.PrjId=vGetCurBudVersion.PrjId \r\n\t\t                AND Bud_Task.version=vGetCurBudVersion.curversion AND TaskType=''\r\n\t\t                AND Bud_Task.PrjId='{0}'\r\n\t\t                GROUP BY Bud_TaskResource.ResourcePrice,Bud_TaskResource.ResourceId\r\n\t\t                UNION ALL\r\n\t\t                SELECT (SUM(ResourceQuantity)*ResourcePrice) ReseTotal,SUM(ResourceQuantity) ResQuantity,\r\n\t\t                ResourceId FROM Bud_ModifyTaskRes modifyTaskRes\r\n\t\t                JOIN Bud_ModifyTask modifyTask ON modifyTaskRes.ModifyTaskId=modifyTask.ModifyTaskId\r\n\t\t                JOIN Bud_MOdify modify ON modifyTask.modifyId=modify.modifyId\r\n\t\t                WHERE modify.FlowState='1' AND modify.PrjId='{0}'\r\n\t\t                GROUP BY ResourcePrice,ResourceId\r\n\t                ) allBudResInfo \r\n\t                INNER JOIN Res_Resource AS r ON allBudResInfo.ResourceId=r.ResourceId\r\n\t                GROUP BY r.ResourceCode\r\n               ", strPrjId);
            }
            else
            {
                builder.Append("SELECT SUM(ResourceQuantity) AS ReadyNumber ,ResourceCode FROM Bud_TaskResource AS res  ");
                builder.Append("INNER JOIN Res_Resource AS r ON r.ResourceId=res.ResourceId ");
                builder.Append("WHERE PrjGuid = '" + strPrjId + "' ");
                builder.Append("GROUP BY ResourceCode ");
            }
            builder.Append(") AS data ON Res_Resource.ResourceCode = data.ResourceCode ");
            builder.Append("LEFT JOIN (SELECT * FROM Res_Price\tWHERE PriceTypeId = '" + PurchasePriceType + "') AS price ON price.ResourceId = Res_Resource.ResourceId ");
            builder.Append("where pscode = @pscode  ORDER BY ResourceCode DESC");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pscode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = pscode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetPurchaseStockByResourceCodes(string[] resourceCodes, string purchaseCode)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in resourceCodes)
            {
                builder.Append("','").Append(str);
            }
            builder.Remove(0, 2);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("select Res_Resource.ResourceCode,ResourceName,Specification,Name,number,sprice,").Append("Convert(decimal(18,3),(number * sprice)) as total,corp,CorpName ").Append("from Res_Resource ").Append("join Res_Unit on Res_Resource.Unit = Res_Unit.UnitID ").Append("join Sm_Purchase_Stock on Res_Resource.ResourceCode = Sm_Purchase_Stock.scode ").Append("join XPM_Basic_ContactCorp on Sm_Purchase_Stock.corp = XPM_Basic_ContactCorp.CorpID ").Append("where Res_Resource.ResourceCode in (").Append(builder.ToString()).Append("') ").Append("and Sm_Purchase_Stock.pscode = @pscode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pscode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = purchaseCode;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), commandParameters);
        }

        public DataTable GetReportDataSource(string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select row_number() over (order by psid) as RowNumber,pid, psid,scode, ResourceName,Specification,Name as UnitName, sprice, number,").AppendLine();
            builder.Append("Convert(decimal(18,3),number*sprice) as total, corp,CorpName,contract,convert(nvarchar(10),intime,121) as intime,Project,ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) AS PrjName,pscode ").AppendLine();
            builder.Append(",Res_Resource.TechnicalParameter,Res_Resource.ModelNumber,Res_Resource.Brand,Res_ResourceType.ResourceTypeName ").AppendLine();
            builder.Append("from Sm_Purchase_Stock ").AppendLine();
            builder.Append("left join Sm_Purchase on Sm_Purchase_Stock.pscode = Sm_Purchase.pcode ").AppendLine();
            builder.Append("inner join Res_Resource on Sm_Purchase_Stock.scode = Res_Resource.ResourceCode ").AppendLine();
            builder.Append(" LEFT JOIN Con_Payout_Contract ON Con_Payout_Contract.ContractId=Sm_Purchase.Contract ").AppendLine();
            builder.Append("left join PT_PrjInfo on Sm_Purchase.Project = Convert(nvarchar(64),PT_PrjInfo.PrjGuid) ").AppendLine();
            builder.Append("left join PT_PrjInfo_ZTB on Sm_Purchase.Project = Convert(nvarchar(64),PT_PrjInfo_ZTB.PrjGuid) ");
            builder.Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ").AppendLine();
            builder.Append("left join XPM_Basic_ContactCorp on Sm_Purchase_Stock.corp = XPM_Basic_ContactCorp.CorpID ").AppendLine();
            builder.Append("join Res_ResourceType on Res_ResourceType.ResourceTypeId = Res_Resource.ResourceType ").AppendLine();
            builder.Append("where Sm_Purchase.flowstate = 1").AppendLine();
            builder.Append(condition).AppendLine();
            builder.Append("order by pcode desc ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetReportDataSource(DateTime? startDate, DateTime? endDate, string resourceNames, string resourceCodes, string purchaseCode, string corpName, string projectName, string category, string specification, string brand, string modelNumber)
        {
            StringBuilder builder = new StringBuilder();
            if (startDate.HasValue)
            {
                builder.AppendFormat(" AND intime >= '{0}' ", startDate.Value);
            }
            if (endDate.HasValue)
            {
                builder.AppendFormat(" AND intime < '{0}' ", endDate.Value);
            }
            builder.Append(DBHelper.GetQuerySql("ResourceName", resourceNames)).Append(" ");
            builder.Append(DBHelper.GetQuerySql("scode", resourceCodes)).Append(" ");
            if (!string.IsNullOrEmpty(purchaseCode))
            {
                builder.AppendFormat("and pscode like '%{0}%' ", purchaseCode);
            }
            if (!string.IsNullOrEmpty(corpName))
            {
                builder.AppendFormat("and CorpName like '%{0}%' ", corpName);
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                builder.AppendFormat("and ISNULL(PT_PrjInfo.prjName,PT_PrjInfo_ZTB.prjName) like '%{0}%' ", projectName);
            }
            if (!string.IsNullOrEmpty(category))
            {
                builder.AppendFormat("and ResourceTypeName like '%{0}%' ", category);
            }
            if (!string.IsNullOrEmpty(specification))
            {
                builder.AppendFormat("and Res_Resource.Specification like '%{0}%' ", specification);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                builder.AppendFormat("and Res_Resource.Brand like '%{0}%' ", brand);
            }
            if (!string.IsNullOrEmpty(modelNumber))
            {
                builder.AppendFormat("and Res_Resource.ModelNumber like '%{0}%' ", modelNumber);
            }
            return this.GetReportDataSource(builder.ToString());
        }

        public DataTable GetTableByPurchaseCodes(string[] pscodes)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ResourceId, pscode,number,sprice, (number*sprice) Total, ResourceCode, ResourceName, Specification, Name as UnitName,Corp, CorpName,arrivalDate ");
            builder.Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ");
            builder.Append("FROM Res_Resource ");
            builder.Append("INNER JOIN Sm_Purchase_Stock ON Sm_Purchase_Stock.scode = Res_Resource.ResourceCode ");
            builder.Append("LEFT JOIN Res_Unit on Res_Unit.UnitId = Res_Resource.Unit ");
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp on Sm_Purchase_Stock.corp = XPM_Basic_ContactCorp.CorpID ");
            builder.Append("WHERE pscode IN(").Append(DBHelper.GetInParameterSql(pscodes)).Append(")");
            builder.Append(" ORDER BY ResourceCode DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Update(SqlTransaction trans, PurchaseStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Purchase_Stock set ");
            builder.Append("scode=@scode,");
            builder.Append("pscode=@pscode,");
            builder.Append("number=@number,");
            builder.Append("sprice=@sprice,");
            builder.Append("corp=@corp,");
            builder.Append("arrivalDate=@arrivalDate");
            builder.Append(" where psid=@psid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@psid", SqlDbType.NVarChar, 50), new SqlParameter("@scode", SqlDbType.VarChar, 50), new SqlParameter("@pscode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@sprice", SqlDbType.Decimal, 9), new SqlParameter("@corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@arrivalDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.psid;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.pscode;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.sprice;
            commandParameters[5].Value = model.corp;
            if ((model.ArrivalDate != null) && (model.ArrivalDate.ToString() != ""))
            {
                commandParameters[6].Value = DateTime.Parse(model.ArrivalDate);
            }
            else
            {
                commandParameters[6].Value = DBNull.Value;
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

