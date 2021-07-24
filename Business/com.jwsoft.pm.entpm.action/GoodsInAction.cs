namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class GoodsInAction
    {
        public static bool AddInBill(GoodsInBill gib)
        {
            string str2 = "";
            object obj2 = str2 + " insert into EPM_Stuff_MaterialIn(StockInCode,StockInBillCode,InDate,ProjectCode,SupplyMan,Sender,Checker,Reciver,Remark,BuyDept,ContractCode,PlanID,InAddr) values('" + gib.StockInCode.ToString() + "','" + gib.StockInBillCode + "','" + gib.InDate.ToShortDateString() + "','" + gib.ProjectCode.ToString() + "'," + gib.SupplyMan.ToString() + ",'" + gib.Sender + "'";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, ",'", gib.Checker, "','", gib.Reciver, "','", gib.Remark, "',", gib.BuyDept, ",'", gib.ContractCode.ToString(), "',", gib.PlanID.ToString(), ",'", gib.InAddr.ToString(), "') " }));
        }

        public static bool AddInMaterial(Guid inCode, int planId, string resourceCode, decimal quantity, decimal price)
        {
            string str = "";
            string str2 = str + " begin ";
            return publicDbOpClass.NonQuerySqlString((str2 + " insert into EPM_Stuff_MaterialInDetial values('" + inCode.ToString() + "','" + resourceCode + "'," + quantity.ToString() + "," + price.ToString() + "," + planId.ToString() + ",'','') ") + " end ");
        }

        public static string ContractName(string cc)
        {
            string str2 = "";
            DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_CON_CONtractmain where contractcode='" + cc + "' and contracttype=3");
            if (table.Rows.Count == 1)
            {
                str2 = table.Rows[0]["ContractName"].ToString();
            }
            return str2;
        }

        public static bool DelInBill(Guid inCode)
        {
            DataTable table = QueryInMaterial(inCode);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DelInMaterial(inCode, table.Rows[i]["resourceCode"].ToString());
                }
            }
            return publicDbOpClass.NonQuerySqlString(" delete from EPM_Stuff_MaterialIn where stockincode='" + inCode.ToString() + "' ");
        }

        public static bool DelInMaterial(Guid inCode, string resourceCode)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(quantity,0) from EPM_Stuff_MaterialInDetial where stockInCode='" + inCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " select @quantity = isnull(@quantity,0) ";
            string str4 = str3 + " delete from EPM_Stuff_MaterialInDetial where stockincode='" + inCode.ToString() + "' and resourceCode='" + resourceCode + "' ";
            return publicDbOpClass.NonQuerySqlString(((str4 + " if not exists(select 1 from EPM_Stuff_MaterialInDetial where stockincode='" + inCode.ToString() + "' and resourceCode='" + resourceCode + "') ") + " update EPM_Stuff_MaterialIn set PlanID=0 where stockincode='" + inCode.ToString() + "' ") + " end ");
        }

        public static string getcatecode(string resourceCode)
        {
            string sqlString = "SELECT TOP (1) CategoryCode FROM EPM_Res_Resource WHERE (ResourceCode = '" + resourceCode + "')";
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary(sqlString).Rows[0]["CategoryCode"].ToString();
        }

        public static DataTable GetStockList(string stockCode, string goodsType, string tabaleName)
        {
            switch (goodsType)
            {
                case "ru":
                    return publicDbOpClass.DataTableQuary("select *,(SELECT  ProjectCode FROM [EPM_Stuff_MaterialIn] WHERE [StockInCode]='" + stockCode.ToString() + "') AS prjcode from EPM_Stuff_MaterialInDetial where StockInCode='" + stockCode.ToString() + "'");

                case "chu":
                    return publicDbOpClass.DataTableQuary("select *,(SELECT  ProjectCode FROM [EPM_Stuff_MaterialOut] WHERE [OutCode]='" + stockCode.ToString() + "') AS prjcode from EPM_Stuff_MaterialOutDetial where OutCode='" + stockCode.ToString() + "'");

                case "tui":
                    return publicDbOpClass.DataTableQuary("select *,(SELECT  ProjectCode FROM [EPM_Stuff_BackBill] WHERE [BackCode]='" + stockCode.ToString() + "') AS prjcode from EPM_Stuff_BackDetails where BackCode='" + stockCode.ToString() + "'");

                case "bo":
                    return publicDbOpClass.DataTableQuary("SELECT dbo.EPM_Stuff_AttemperDetail.OutCode, dbo.EPM_Stuff_AttemperDetail.ResourceCode, dbo.EPM_Stuff_AttemperDetail.Fact, dbo.EPM_Stuff_AttemperDetail.Price, dbo.EPM_Stuff_AttemperDetail.OutCode AS Expr1, dbo.EPM_Stuff_Attemper.LLDept, dbo.EPM_Stuff_Attemper.ProjectCode, dbo.EPM_Stuff_Attemper.AuditResult, dbo.EPM_Stuff_Attemper.OutBillCode FROM dbo.EPM_Stuff_AttemperDetail RIGHT OUTER JOIN dbo.EPM_Stuff_Attemper ON dbo.EPM_Stuff_AttemperDetail.OutCode =dbo.EPM_Stuff_Attemper.OutCode WHERE (dbo.EPM_Stuff_AttemperDetail.OutCode = '" + stockCode.ToString() + "')");
            }
            return null;
        }

        public static int ifAddorEdit(string resourceCode, string procode, string price)
        {
            string sqlString = "select isnull(NoteID,0) from EPM_Stuff_Stock where resourceCode='" + resourceCode + "'and  ProjectCode='" + procode + "' and price=" + price;
            DataTable table = new DataTable();
            return Convert.ToInt16(publicDbOpClass.DataTableQuary(sqlString).Rows.Count);
        }

        public static int QueryGoodsInCount(string sqlWhere)
        {
            return publicDbOpClass.GetRecordCount("v_Stuff_InProjectStock", sqlWhere);
        }

        public static int QueryGoodsInCount(string projectCode, string billCode, string inDate)
        {
            string strWhere = Stockwhere(projectCode, billCode, inDate);
            return publicDbOpClass.GetRecordCount("v_Stuff_InProjectStock", strWhere);
        }

        public static DataTable QueryGoodsInList(string sqlWhere)
        {
            return publicDbOpClass.GetPageData(sqlWhere, "v_Stuff_InProjectStock", "InDate desc");
        }

        public static DataTable QueryGoodsInList(string projectCode, string billCode, string inDate, int pageSize, int pageIndex)
        {
            string strWhere = Stockwhere(projectCode, billCode, inDate);
            return publicDbOpClass.GetRecordFromPage("v_Stuff_InProjectStock", "InDate", pageSize, pageIndex, 1, strWhere);
        }

        public static DataTable QueryInMaterial(Guid inCode)
        {
            return publicDbOpClass.DataTableQuary("select a.resourceCode,a.resourceName,a.specification,a.UnitName,b.quantity,b.unitprice,b.SumPrice,b.Factory,b.FactoryDate from EPM_Stuff_MaterialInDetial b left join v_Res_Resource a on b.resourceCode = a.resourceCode where b.stockincode='" + inCode.ToString() + "'");
        }

        public static string QueryInMaterial(string flowguid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT stockincode FROM EPM_Stuff_MaterialIn WHERE FlowGuid='" + flowguid + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            Guid guid2 = new Guid();
            return guid2.ToString();
        }

        public static DataTable QueryStockBill(Guid stockCode)
        {
            return publicDbOpClass.DataTableQuary("select * from v_Stuff_InProjectStock where stockInCode='" + stockCode.ToString() + "'");
        }

        public static DataTable QueryStockBillforGuid(string wlguid)
        {
            return publicDbOpClass.DataTableQuary("select * from v_Stuff_InProjectStock where flowguid='" + wlguid + "'");
        }

        public static string Stockwhere(string projectCode, string billCode, string inDate)
        {
            string str = "";
            str = " (1=1) ";
            if (projectCode != "")
            {
                str = str + " and (projectCode='" + projectCode + "') ";
            }
            if (billCode != "")
            {
                str = str + " and (stockinbillcode like '%" + billCode + "%') ";
            }
            if (inDate != "")
            {
                str = str + " and (InDate='" + inDate + "')";
            }
            return str;
        }

        public static bool Tui_ifAddorEdit(string resourceCode, string procode)
        {
            string sqlString = "select * from EPM_Stuff_Stock where resourceCode='" + resourceCode + "'and ProjectCode='" + procode + "'";
            new DataTable();
            try
            {
                publicDbOpClass.DataTableQuary(sqlString);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static bool UpdInBill(GoodsInBill gib)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { 
                obj2, "update EPM_Stuff_MaterialIn set InDate='", gib.InDate.ToShortDateString(), "',ProjectCode='", gib.ProjectCode.ToString(), "',InAddr='", gib.InAddr.ToString(), "',Sender='", gib.Sender, "',Checker='", gib.Checker, "',Reciver='", gib.Reciver, "',Remark='", gib.Remark, "',BuyDept=", 
                gib.BuyDept
             }) + " where StockInCode='" + gib.StockInCode.ToString() + "'");
        }

        public static bool UpdInMaterial(Guid inCode, string resourceCode, decimal quantity, decimal price, string TFactory, string TFactoryDate)
        {
            string str = "";
            string str2 = str + " begin " + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(quantity,0) from EPM_Stuff_MaterialInDetial where stockInCode='" + inCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " select @quantity = isnull(@quantity,0) ";
            return publicDbOpClass.NonQuerySqlString((str3 + " update EPM_Stuff_MaterialInDetial set quantity=" + quantity.ToString() + ",UnitPrice=" + price.ToString() + ",Factory='" + TFactory + "',FactoryDate='" + TFactoryDate + "' where stockInCode='" + inCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " end ");
        }
    }
}

