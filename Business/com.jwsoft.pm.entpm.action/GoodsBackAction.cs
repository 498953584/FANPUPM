namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class GoodsBackAction
    {
        public static bool AddAttemperMaterial(Guid outCode, string resourceCode, decimal apply, decimal unitPrice)
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
            string str5 = str4 + " if not exists(select 1 from EPM_Stuff_AttemperDetail where OutCode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "') ";
            return publicDbOpClass.NonQuerySqlString((str5 + " insert into EPM_Stuff_AttemperDetail values('" + outCode.ToString() + "','" + resourceCode + "'," + apply.ToString() + ",0,'" + unitPrice.ToString() + "','" + str2 + "','') ") + " end ");
        }

        public static bool AddBackBill(MaterialBackInfo mbi)
        {
            string str2 = "";
            string str3 = str2 + " insert into EPM_Stuff_BackBill(BackCode,BackBillCode,BackDate,BackDept,Sender,ProjectCode,Checker,Remark,isBackToEnt) values('" + mbi.OutCode.ToString() + "','" + mbi.OutBillCode + "','" + mbi.OutDate.ToShortDateString() + "','" + mbi.LLDept + "','" + mbi.LLMan + "',";
            return publicDbOpClass.NonQuerySqlString(str3 + "'" + mbi.ProjectCode.ToString() + "','" + mbi.Sender + "','" + mbi.Remark + "','0') ");
        }

        public static bool AddMaterial(Guid backCode, Guid pageSession, string userCode)
        {
            string str = " begin ";
            string str2 = str + "insert into EPM_Stuff_BackDetails";
            string str3 = (str2 + " select '" + backCode.ToString() + "',resourcecode,0,(SELECT TOP 1 Factory  FROM EPM_Stuff_MaterialOutDetial   WHERE (ResourceCode = EPM_Res_TempResource.ResourceCode) AND (Factory <> '') ORDER BY NoteID DESC) as f,'',0,0 from EPM_Res_TempResource where SessionCode='" + pageSession.ToString() + "' and UserCode='" + userCode + "' and (ResourceCode not in(") + "select ResourceCode from EPM_Stuff_BackDetails where backcode='" + backCode.ToString() + "')) ";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from epm_res_tempresource where  SessionCode='" + pageSession.ToString() + "' and UserCode='" + userCode + "' ") + " end ");
        }

        public static bool AddMaterial(Guid outCode, string resourceCode, decimal apply)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT TOP 1 Factory  FROM EPM_Stuff_MaterialOutDetial   WHERE (ResourceCode = '" + resourceCode + "') AND (Factory <> '') ORDER BY NoteID DESC");
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            string str3 = "";
            str3 = " begin ";
            string str4 = str3;
            string str5 = str4 + " if not exists(select 1 from EPM_Stuff_BackDetails where backCode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "') ";
            return publicDbOpClass.NonQuerySqlString((str5 + " insert into EPM_Stuff_BackDetails values('" + outCode.ToString() + "','" + resourceCode + "'," + apply.ToString() + ",'" + str2 + "','') ") + " end ");
        }

        public static bool AddMaterials(Guid outCode, string resourceCode, decimal apply, decimal price, decimal Fact)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "SELECT TOP 1 Factory  FROM EPM_Stuff_MaterialOutDetial   WHERE (ResourceCode = '", resourceCode, "') AND (Factory <> '') and Price='", price, "' ORDER BY NoteID DESC" }));
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            string str3 = "";
            str3 = " begin ";
            object obj2 = str3;
            object obj3 = string.Concat(new object[] { obj2, " if not exists(select 1 from EPM_Stuff_BackDetails where backCode='", outCode.ToString(), "' and resourceCode='", resourceCode, "' and Price='", price, "') " });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj3, " insert into EPM_Stuff_BackDetails values('", outCode.ToString(), "','", resourceCode, "',", apply.ToString(), ",'", str2, "','','", price, "','", Fact, "') " }) + " end ");
        }

        public static bool DelBackBill(Guid inCode)
        {
            DataTable table = QueryBackMaterial(inCode);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DelBackMaterial(inCode, table.Rows[i]["resourceCode"].ToString());
                }
            }
            return publicDbOpClass.NonQuerySqlString(" delete from EPM_Stuff_BackBill where backcode='" + inCode.ToString() + "' ");
        }

        public static bool DelBackMaterial(Guid outCode, string resourceCode)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " declare @quantity decimal ";
            string str3 = (str2 + " select @quantity=isnull(quantity,0) from EPM_Stuff_BackDetails where backcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " select @quantity = isnull(@quantity,0) ";
            string str4 = str3 + " delete from EPM_Stuff_BackDetails where backcode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "' ";
            return publicDbOpClass.NonQuerySqlString(((str4 + " if not exists(select 1 from EPM_Stuff_MaterialInDetial where stockincode='" + outCode.ToString() + "' and resourceCode='" + resourceCode + "') ") + " update EPM_Stuff_MaterialIn set PlanID=0 where stockincode='" + outCode.ToString() + "' ") + " end ");
        }

        public static string GetBillCode(string flowguid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT backcode FROM EPM_Stuff_BackBill WHERE FlowGuid='" + flowguid + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            Guid guid2 = new Guid();
            return guid2.ToString();
        }

        public static DataTable QueryBackMaterial(Guid inCode)
        {
            return publicDbOpClass.DataTableQuary("select a.resourceCode,a.resourceName,a.specification,a.UnitName,b.quantity,b.Factory,b.FactoryDate,b.Price,b.Apply from EPM_Stuff_BackDetails b left join v_Res_Resource a on b.resourceCode = a.resourceCode where b.backcode='" + inCode.ToString() + "'");
        }

        public static int QueryGoodsBackCount(string sqlWhere)
        {
            return publicDbOpClass.GetRecordCount("v_Stuff_BackBill", sqlWhere);
        }

        public static int QueryGoodsBackCount(string dept, string inDate)
        {
            string strWhere = Stockwhere(dept, inDate);
            return publicDbOpClass.GetRecordCount("v_Stuff_BackBill", strWhere);
        }

        public static DataTable QueryGoodsBackList(string sqlWhere)
        {
            return publicDbOpClass.GetPageData(sqlWhere, "v_Stuff_BackBill", "BackDate desc");
        }

        public static DataTable QueryGoodsBackList(string dept, string inDate, int pageSize, int pageIndex)
        {
            string strWhere = Stockwhere(dept, inDate);
            return publicDbOpClass.GetRecordFromPage("v_Stuff_BackBill", "BackDate", pageSize, pageIndex, 1, strWhere);
        }

        public static DataTable QueryStockBill(Guid outCode)
        {
            return publicDbOpClass.DataTableQuary("select * from v_Stuff_BackBill where backCode='" + outCode.ToString() + "'");
        }

        public static string Stockwhere(string dept, string outDate)
        {
            string str = "";
            str = " (1=1) ";
            if (dept != "")
            {
                str = str + " and (backdept like ='%" + dept + "%') ";
            }
            if (outDate != "")
            {
                str = str + " and (backDate='" + outDate + "')";
            }
            return str;
        }

        public static bool UpdBackBill(MaterialBackInfo mbi)
        {
            string str2 = "";
            string str3 = str2 + " update EPM_Stuff_BackBill set BackDate='" + mbi.OutDate.ToShortDateString() + "',BackDept='" + mbi.LLDept + "',";
            return publicDbOpClass.NonQuerySqlString((str3 + "Sender='" + mbi.LLMan + "',ProjectCode='" + mbi.ProjectCode.ToString() + "',Checker='" + mbi.Sender + "',Remark='" + mbi.Remark + "'") + " where BackCode='" + mbi.OutCode.ToString() + "'");
        }

        public static int UpdMaterial(string projectcode, Guid outCode, string resourceCode, decimal apply, decimal quantity, string TFactory, string TFactoryDate, decimal price)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@projectcode", SqlDbType.NVarChar, 50), new SqlParameter("@outCode", SqlDbType.NVarChar, 50), new SqlParameter("@ResourceCode", SqlDbType.NVarChar, 50), new SqlParameter("@Quantity", SqlDbType.Decimal, 0x12), new SqlParameter("@Factory", SqlDbType.NVarChar, 50), new SqlParameter("@FactoryDate", SqlDbType.NVarChar, 50), new SqlParameter("@Price", SqlDbType.Decimal, 0x12), new SqlParameter("@Apply", SqlDbType.Decimal, 0x12), new SqlParameter("@i_Ret", SqlDbType.Int, 4) };
            commandParameters[0].Value = projectcode;
            commandParameters[1].Value = outCode.ToString();
            commandParameters[2].Value = resourceCode;
            commandParameters[3].Value = quantity;
            commandParameters[4].Value = TFactory;
            commandParameters[5].Value = TFactoryDate;
            commandParameters[6].Value = price;
            commandParameters[7].Value = apply;
            commandParameters[8].Direction = ParameterDirection.Output;
            publicDbOpClass.ExecuteProcedure("pro_Stuff_UpdateBackMaterial", commandParameters);
            return Convert.ToInt32(commandParameters[8].Value);
        }
    }
}

