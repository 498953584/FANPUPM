namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class GoodsOutAction
    {
        public static bool AddAttemperBill(GoodsOutBill gob)
        {
            string str2 = "";
            string str3 = str2 + " insert into EPM_Stuff_Attemper(OutCode,OutBillCode,OutDate,LLDept,LLMan,ProjectCode,Sender,Remark) values('" + gob.OutCode.ToString() + "','" + gob.OutBillCode + "','" + gob.OutDate.ToShortDateString() + "','" + gob.LLDept + "','" + gob.LLMan + "',";
            return publicDbOpClass.NonQuerySqlString(str3 + "'" + gob.ProjectCode.ToString() + "','" + gob.Sender + "','" + gob.Remark + "') ");
        }

        public static bool AddAttemperMaterial(Guid outCode, Guid pageSession, string userCode, string procode)
        {
            string str2 = ((" begin " + "insert into EPM_Stuff_AttemperDetail") + " SELECT '" + outCode.ToString() + "' as outcode, EPM_Stuff_Stock.ResourceCode,EPM_Stuff_Stock.Quantity,0 as fact, EPM_Stuff_Stock.Price,EPM_Stuff_Stock.Factory, EPM_Stuff_Stock.FactoryDate FROM EPM_Stuff_Stock ") + "inner join  EPM_Res_TempResource on EPM_Res_TempResource.ResourceCode=EPM_Stuff_Stock.ResourceCode ";
            string str3 = (str2 + " where EPM_Stuff_Stock.ProjectCode='" + procode + "' and EPM_Res_TempResource.SessionCode='" + pageSession.ToString() + "' and EPM_Res_TempResource.UserCode='" + userCode + "' and EPM_Stuff_Stock.ResourceCode not in(") + "select ResourceCode from EPM_Stuff_AttemperDetail where outcode='" + outCode.ToString() + "') ";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from epm_res_tempresource where  SessionCode='" + pageSession.ToString() + "' and UserCode='" + userCode + "' ") + " end ");
        }

        public static bool AddMaterial(Guid outCode, Guid pageSession, string userCode, string procode)
        {
            string str2 = ((" begin " + "insert into EPM_Stuff_MaterialOutDetial ") + " SELECT '" + outCode.ToString() + "' as outcode, EPM_Stuff_Stock.ResourceCode,EPM_Stuff_Stock.Quantity,0 as fact, EPM_Stuff_Stock.Price,EPM_Stuff_Stock.Factory, EPM_Stuff_Stock.FactoryDate,'' FROM EPM_Stuff_Stock ") + "inner join  EPM_Res_TempResource on EPM_Res_TempResource.ResourceCode=EPM_Stuff_Stock.ResourceCode ";
            string str3 = (str2 + " where EPM_Stuff_Stock.ProjectCode='" + procode + "' and EPM_Res_TempResource.SessionCode='" + pageSession.ToString() + "' and EPM_Res_TempResource.UserCode='" + userCode + "' and EPM_Stuff_Stock.ResourceCode not in(") + " select ResourceCode from EPM_Stuff_MaterialOutDetial where outcode='" + outCode.ToString() + "')";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from epm_res_tempresource where  SessionCode='" + pageSession.ToString() + "' and UserCode='" + userCode + "' ") + " end ");
        }

        public static bool AddMaterial(Guid outCode, string resourceCode, decimal apply, decimal fact, decimal price)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT TOP 1 Factory  FROM EPM_Stuff_MaterialInDetial   WHERE (ResourceCode = '" + resourceCode + "') AND (Factory <> '') ORDER BY NoteID DESC");
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            string str3 = "";
            str3 = " begin ";
            string str4 = str3;
            string str5 = str4 + " if not exists(select 1 from EPM_Stuff_MaterialOutDetial where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "') ";
            return publicDbOpClass.NonQuerySqlString((str5 + " insert into EPM_Stuff_MaterialOutDetial values('" + outCode.ToString() + "','" + resourceCode + "'," + apply.ToString() + "," + fact.ToString() + "," + price.ToString() + ",'" + str2 + "','') ") + " end ");
        }

        public static bool AddMaterialli(Guid outCode, string resourceCode, decimal apply, decimal fact, decimal price, Guid pc)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT TOP 1 Factory  FROM EPM_Stuff_MaterialInDetial   WHERE (ResourceCode = '" + resourceCode + "') AND (Factory <> '') ORDER BY NoteID DESC");
            if (table.Rows.Count > 0)
            {
                table.Rows[0][0].ToString();
            }
            string str2 = "";
            str2 = " begin ";
            object obj2 = str2;
            object obj3 = string.Concat(new object[] { obj2, " if not exists(select 1 from EPM_Stuff_MaterialOutDetial where outcode='", outCode.ToString(), "' and resourceCode='", resourceCode, "' and Price='", price, "') " }) + " insert into EPM_Stuff_MaterialOutDetial " + " select * from (";
            object obj4 = string.Concat(new object[] { obj3, " SELECT  '", outCode, "' as OutCode,dbo.EPM_Stuff_Stock.ResourceCode AS ResourceCode,dbo.EPM_Stuff_Stock.Quantity AS Apply,0.000 as Fact,dbo.EPM_Stuff_Stock.Price as Price,null as Factory,null as FactoryDate,EPM_Stuff_MaterialIn.InAddr as Inaddr  " }) + " from EPM_Stuff_Stock left  JOIN dbo.EPM_V_Res_ResourceBasic ON dbo.EPM_Stuff_Stock.ResourceCode = dbo.EPM_V_Res_ResourceBasic.ResourceCode LEFT OUTER JOIN dbo.PT_PrjInfo ON dbo.EPM_Stuff_Stock.ProjectCode = dbo.PT_PrjInfo.PrjGuid left join EPM_Stuff_MaterialIn on EPM_Stuff_MaterialIn.stockincode=EPM_Stuff_Stock.stockcode ";
            return publicDbOpClass.NonQuerySqlString((string.Concat(new object[] { obj4, " left join XPM_Basic_ContactCorp on XPM_Basic_ContactCorp.corpid=EPM_Stuff_MaterialIn.supplyman where convert(decimal,dbo.EPM_Stuff_Stock.Quantity) !=0 and PT_PrjInfo.PrjGuid='", pc, "' " }) + " ) a  where ResourceCode='" + resourceCode + "'") + " end ");
        }

        public static bool AddOutBill(GoodsOutBill gob)
        {
            string str2 = "";
            string str3 = str2 + " insert into EPM_Stuff_MaterialOut(OutCode,OutBillCode,OutDate,LLDept,LLMan,ProjectCode,Sender,Remark) values('" + gob.OutCode.ToString() + "','" + gob.OutBillCode + "','" + gob.OutDate.ToShortDateString() + "','" + gob.LLDept + "','" + gob.LLMan + "',";
            return publicDbOpClass.NonQuerySqlString(str3 + "'" + gob.ProjectCode.ToString() + "','" + gob.Sender + "','" + gob.Remark + "') ");
        }

        public static bool AddRemoverMaterialFromIn(Guid outCode, string resourceCode, decimal apply, decimal fact, decimal price, Guid pc)
        {
            string str = "";
            str = " begin ";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, " if not exists(select 1 from EPM_Stuff_MaterialOutDetial where outcode='", outCode.ToString(), "' and resourceCode='", resourceCode, "' and Price='", price, "') " }) + " insert into EPM_Stuff_AttemperDetail " + " select * from (";
            object obj4 = string.Concat(new object[] { obj3, " SELECT  '", outCode, "' as OutCode,dbo.EPM_Stuff_Stock.ResourceCode AS ResourceCode,dbo.EPM_Stuff_Stock.Quantity AS Apply,0.000 as Fact,dbo.EPM_Stuff_Stock.Price as Price,null as Factory,null as FactoryDate  " }) + " from EPM_Stuff_Stock left  JOIN dbo.EPM_V_Res_ResourceBasic ON dbo.EPM_Stuff_Stock.ResourceCode = dbo.EPM_V_Res_ResourceBasic.ResourceCode LEFT OUTER JOIN dbo.PT_PrjInfo ON dbo.EPM_Stuff_Stock.ProjectCode = dbo.PT_PrjInfo.PrjGuid left join EPM_Stuff_MaterialIn on EPM_Stuff_MaterialIn.stockincode=EPM_Stuff_Stock.stockcode ";
            return publicDbOpClass.NonQuerySqlString((string.Concat(new object[] { obj4, " left join XPM_Basic_ContactCorp on XPM_Basic_ContactCorp.corpid=EPM_Stuff_MaterialIn.supplyman where convert(decimal,dbo.EPM_Stuff_Stock.Quantity) !=0 and PT_PrjInfo.PrjGuid='", pc, "' " }) + " ) a  where ResourceCode='" + resourceCode + "'") + " end ");
        }

        public static bool AnalyzeLeaveMaterial(Guid pageSession, string userCode)
        {
            return false;
        }

        public static bool DelAttemperBill(Guid inCode)
        {
            DataTable table = QueryAttemperMaterial(inCode);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DelAttemperMaterial(inCode, table.Rows[i]["resourceCode"].ToString());
                }
            }
            return publicDbOpClass.NonQuerySqlString(" delete from EPM_Stuff_Attemper where Outcode='" + inCode.ToString() + "' ");
        }

        public static bool DelAttemperMaterial(Guid outCode, string resourceCode)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(fact,0) from EPM_Stuff_AttemperDetail where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " select @quantity=isnull(@quantity,0) ";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from EPM_Stuff_AttemperDetail where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " end ");
        }

        public static bool DelAttemperMaterial(Guid outCode, string resourceCode, string price)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(fact,0) from EPM_Stuff_AttemperDetail where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' and price=" + price) + " select @quantity=isnull(@quantity,0) ";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from EPM_Stuff_AttemperDetail where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' and price=" + price) + " end ");
        }

        public static bool DelOutBill(Guid inCode)
        {
            DataTable table = QueryOutMaterial(inCode);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DelOutMaterial(inCode, table.Rows[i]["resourceCode"].ToString());
                }
            }
            return publicDbOpClass.NonQuerySqlString(" delete from EPM_Stuff_MaterialOut where Outcode='" + inCode.ToString() + "' ");
        }

        public static bool DelOutMaterial(Guid outCode, string resourceCode)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(fact,0) from EPM_Stuff_MaterialOutDetial where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " select @quantity=isnull(@quantity,0) ";
            string str4 = str3 + " delete from EPM_Stuff_MaterialOutDetial where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' ";
            return publicDbOpClass.NonQuerySqlString(((str4 + " if not exists(select 1 from EPM_Stuff_MaterialInDetial where stockincode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "') ") + " update EPM_Stuff_MaterialIn set PlanID=0 where stockincode='" + outCode.ToString() + "' ") + " end ");
        }

        public static bool DelOutMaterial(Guid outCode, string resourceCode, string price)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(fact,0) from EPM_Stuff_MaterialOutDetial where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' and Price='" + price + "' ") + " select @quantity=isnull(@quantity,0) ";
            string str4 = str3 + " delete from EPM_Stuff_MaterialOutDetial where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' and Price='" + price + "'";
            return publicDbOpClass.NonQuerySqlString(((str4 + " if not exists(select 1 from EPM_Stuff_MaterialInDetial where stockincode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "') ") + " update EPM_Stuff_MaterialIn set PlanID=0 where stockincode='" + outCode.ToString() + "' ") + " end ");
        }

        public static string GetAttmeperOutCode(string flowguid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select OutCode from dbo.EPM_Stuff_Attemper where FlowGuid='" + flowguid + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["OutCode"].ToString();
            }
            return null;
        }

        public static DataTable QueryAttemperBill(Guid outCode)
        {
            return publicDbOpClass.DataTableQuary("select * from V_Stuff_AttemperBill where outCode='" + outCode.ToString() + "'");
        }

        public static int QueryAttemperCount(string sqlWhere)
        {
            return publicDbOpClass.GetRecordCount("V_Stuff_AttemperBill", sqlWhere);
        }

        public static int QueryAttemperCount(string dept, string inDate)
        {
            string strWhere = Stockwhere(dept, inDate);
            return publicDbOpClass.GetRecordCount("V_Stuff_AttemperBill", strWhere);
        }

        public static DataTable QueryAttemperList(string sqlWhere)
        {
            return publicDbOpClass.GetPageData(sqlWhere, "V_Stuff_AttemperBill", "OutDate desc");
        }

        public static DataTable QueryAttemperList(string dept, string inDate, int pageSize, int pageIndex)
        {
            string strWhere = Stockwhere(dept, inDate);
            return publicDbOpClass.GetRecordFromPage("V_Stuff_AttemperBill", "OutDate", pageSize, pageIndex, 1, strWhere);
        }

        public static DataTable QueryAttemperMaterial(Guid inCode)
        {
            return publicDbOpClass.DataTableQuary("select a.resourceCode,a.resourceName,a.specification,a.UnitName,b.apply,b.fact,b.noteid,b.price,b.Factory,b.FactoryDate from EPM_Stuff_AttemperDetail b left join v_Res_Resource a on b.resourceCode = a.resourceCode where b.outcode='" + inCode.ToString() + "'");
        }

        public static int QueryGoodsOutCount(string sqlWhere)
        {
            return publicDbOpClass.GetRecordCount("v_Stuff_GoodsOutBill", sqlWhere);
        }

        public static int QueryGoodsOutCount(string dept, string inDate)
        {
            string strWhere = Stockwhere(dept, inDate);
            return publicDbOpClass.GetRecordCount("v_Stuff_GoodsOutBill", strWhere);
        }

        public static DataTable QueryGoodsOutList(string sqlWhere)
        {
            return publicDbOpClass.GetPageData(sqlWhere, "v_Stuff_GoodsOutBill", "OutDate desc");
        }

        public static DataTable QueryGoodsOutList(string dept, string inDate, int pageSize, int pageIndex)
        {
            string strWhere = Stockwhere(dept, inDate);
            return publicDbOpClass.GetRecordFromPage("v_Stuff_GoodsOutBill", "OutDate", pageSize, pageIndex, 1, strWhere);
        }

        public static DataTable QueryOutList(string projectCode)
        {
            return publicDbOpClass.DataTableQuary("select * from EPM_Stuff_MaterialOut where AuditResult=1 and projectcode='" + projectCode + "' order by outdate desc");
        }

        public static DataTable QueryOutMaterial(Guid inCode)
        {
            return publicDbOpClass.DataTableQuary("SELECT dbo.v_Res_Resource.ResourceName, dbo.v_Res_Resource.UnitName, dbo.v_Res_Resource.Specification, EPM_Stuff_Stock.Quantity as 'Apply',EPM_Stuff_MaterialOutDetial_1.noteId,EPM_Stuff_MaterialOutDetial_1.Fact, EPM_Stuff_MaterialOutDetial_1.Price,EPM_Stuff_MaterialOutDetial_1.Factory, EPM_Stuff_MaterialOutDetial_1.FactoryDate, EPM_Stuff_MaterialOutDetial_1.ResourceCode, dbo.EPM_Stuff_Stock.Quantity FROM dbo.EPM_Stuff_MaterialOutDetial AS EPM_Stuff_MaterialOutDetial_1 INNER JOIN dbo.v_Res_Resource ON EPM_Stuff_MaterialOutDetial_1.ResourceCode = dbo.v_Res_Resource.ResourceCode INNER JOIN dbo.EPM_Stuff_Stock ON EPM_Stuff_MaterialOutDetial_1.ResourceCode = dbo.EPM_Stuff_Stock.ResourceCode and  EPM_Stuff_MaterialOutDetial_1.price=EPM_Stuff_Stock.price INNER JOIN dbo.EPM_Stuff_MaterialOut ON EPM_Stuff_MaterialOutDetial_1.OutCode = dbo.EPM_Stuff_MaterialOut.OutCode AND dbo.EPM_Stuff_Stock.ProjectCode = dbo.EPM_Stuff_MaterialOut.ProjectCode where  EPM_Stuff_MaterialOutDetial_1.outcode='" + inCode.ToString() + "'");
        }

        public static string QueryProjectName(string strProjectCode)
        {
            string str = string.Empty;
            DataTable table = publicDbOpClass.DataTableQuary(" select PrjName from PT_PrjInfo where prjguid='" + strProjectCode + "' ");
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["PrjName"].ToString();
            }
            return str;
        }

        public static DataTable QueryStockBill(Guid outCode)
        {
            return publicDbOpClass.DataTableQuary("select * from v_Stuff_GoodsOutBill where outCode='" + outCode.ToString() + "'");
        }

        public static decimal StockQuantity(string projectcode, string resourcecode)
        {
            string str2 = (((" begin" + " declare @res table") + " (" + " quantity decimal(18,2)") + " )" + " declare @q decimal(18,2)") + " declare @q1 decimal(18,2)" + " declare @q2 decimal(18,2)";
            string str3 = str2 + " select @q = isnull(sum(quantity),0) from v_Stuff_ProjectStock where projectcode='" + projectcode + "' and resourcecode='" + resourcecode + "'";
            string str4 = str3 + " select @q1 = isnull(sum(fact),0) from v_Stuff_GoodsOutDetails where projectcode='" + projectcode + "' and resourcecode='" + resourcecode + "'";
            return Convert.ToDecimal(publicDbOpClass.DataTableQuary(((str4 + " select @q2 = isnull(sum(quantity),0) from v_Stuff_BackDetails where projectcode='" + projectcode + "' and resourcecode='" + resourcecode + "'") + " insert into @res values (@q- @q1 + @q2)") + " select quantity  from @res" + " end").Rows[0][0].ToString());
        }

        public static string Stockwhere(string dept, string outDate)
        {
            string str = "";
            str = " (1=1) ";
            if (dept != "")
            {
                str = str + " and (lldept like '%" + dept + "%') ";
            }
            if (outDate != "")
            {
                str = str + " and (OutDate='" + outDate + "')";
            }
            return str;
        }

        public static bool UpdAttemperBill(GoodsOutBill gob)
        {
            string str2 = "";
            string str3 = str2 + " update EPM_Stuff_Attemper set OutDate='" + gob.OutDate.ToShortDateString() + "',LLDept='" + gob.LLDept + "',";
            return publicDbOpClass.NonQuerySqlString((str3 + "LLMan='" + gob.LLMan + "',ProjectCode='" + gob.ProjectCode.ToString() + "',Sender='" + gob.Sender + "',Remark='" + gob.Remark + "'") + " where OutCode='" + gob.OutCode.ToString() + "'");
        }

        public static bool UpdAttemperMaterial(Guid outCode, string resourceCode, decimal apply, decimal fact, string TFactory, string TFactoryDate, string noteId)
        {
            string str = "";
            string str2 = str + " begin " + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(fact,0) from EPM_Stuff_AttemperDetail where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " select @quantity=isnull(@quantity,0) ";
            return publicDbOpClass.NonQuerySqlString((str3 + " update EPM_Stuff_AttemperDetail set apply=" + apply.ToString() + ",fact=" + fact.ToString() + ",Factory='" + TFactory + "',FactoryDate='" + TFactoryDate + "' where OutCode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' and noteId=" + noteId) + " end ");
        }

        public static bool UpdMaterial(Guid outCode, string resourceCode, decimal apply, decimal fact, decimal price, string TFactory, string TFactoryDate, string noteId)
        {
            string str = "";
            string str2 = str + " begin " + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(fact,0) from EPM_Stuff_MaterialOutDetial where outcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "'") + " select @quantity=isnull(@quantity,0) ";
            return publicDbOpClass.NonQuerySqlString((str3 + " update EPM_Stuff_MaterialOutDetial set apply=" + apply.ToString() + ",fact=" + fact.ToString() + ",Price=" + price.ToString() + ",Factory='" + TFactory + "',FactoryDate='" + TFactoryDate + "' where OutCode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' and NoteId=" + noteId) + " end ");
        }

        public static bool UpdOutBill(GoodsOutBill gob)
        {
            string str2 = "";
            string str3 = str2 + " update EPM_Stuff_MaterialOut set OutDate='" + gob.OutDate.ToShortDateString() + "',LLDept='" + gob.LLDept + "',";
            return publicDbOpClass.NonQuerySqlString((str3 + "LLMan='" + gob.LLMan + "',ProjectCode='" + gob.ProjectCode.ToString() + "',Sender='" + gob.Sender + "',Remark='" + gob.Remark + "'") + " where OutCode='" + gob.OutCode.ToString() + "'");
        }
    }
}

