namespace com.jwsoft.pm.entpm.action.sl
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class StockAction
    {
        public static bool AddResource(Guid billCode, Guid pageSession, string userCode, Guid pc)
        {
            string str = " begin ";
            string str2 = str + " insert into EPM_Stuff_Stock(StockCode,ResourceCode,CategoryCode,Quantity,Price,ModifyDate) ";
            object obj2 = ((str2 + " select '" + billCode.ToString() + "',ResourceCode,CategoryCode,0,Price,'" + DateTime.Today.ToShortDateString() + "' from EPM_Res_TempResource where SessionCode='" + pageSession.ToString() + "' and UserCode='" + userCode + "' and ResourceCode not in(") + "select ResourceCode from EPM_Stuff_Stock where stockcode='" + billCode.ToString() + "') ") + " insert into EPM_Stuff_StockCheck ";
            return publicDbOpClass.NonQuerySqlString((string.Concat(new object[] { obj2, " select '", pc, "' , '", billCode.ToString(), "',ResourceCode,CategoryCode,0,Price,'", DateTime.Today.ToShortDateString(), "' from EPM_Res_TempResource where SessionCode='", pageSession.ToString(), "' and UserCode='", userCode, "' and ResourceCode not in(" }) + "select ResourceCode from EPM_Stuff_StockCheck where stockcode='" + billCode.ToString() + "') ") + " end ");
        }

        public static bool AddStock(Guid ProjectCode, Guid billCode, DateTime theDate, DateTime addDate)
        {
            string str = "";
            str = " begin ";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, " if not exists(select 1 from EPM_Stuff_StockBill where ProjectCode='", ProjectCode, "') " }) + " begin ";
            string str2 = string.Concat(new object[] { obj3, " insert into EPM_Stuff_StockBill values('", ProjectCode, "','", billCode.ToString(), "','", addDate.ToShortDateString(), "',", theDate.Year.ToString(), ",", theDate.Month.ToString(), ",1,'", theDate.ToShortDateString(), "') " }) + " insert into EPM_Stuff_Stock (StockCode,ResourceCode,CategoryCode,Quantity,Price,ModifyDate)";
            object obj4 = str2 + " select '" + billCode.ToString() + "',ResourceCode,CategoryCode,0,Price,'" + DateTime.Today.ToShortDateString() + "' from v_Res_Resource ";
            object obj5 = string.Concat(new object[] { obj4, " insert into EPM_Stuff_StockBill values('", ProjectCode, "','", billCode.ToString(), "','", addDate.ToShortDateString(), "',", theDate.Year.ToString(), ",", theDate.Month.ToString(), ",2,'", theDate.ToShortDateString(), "') " }) + " insert into EPM_Stuff_StockCheck ";
            object obj6 = (string.Concat(new object[] { obj5, " select '", ProjectCode, "',StockCode,Resourcecode,CategoryCode,Quantity,Price,ModifyDate from EPM_Stuff_Stock where stockCode='", billCode.ToString(), "' " }) + " end ") + " else " + " begin ";
            object obj7 = string.Concat(new object[] { obj6, " insert into EPM_Stuff_StockBill values('", ProjectCode, "','", billCode.ToString(), "','", addDate.ToShortDateString(), "',", theDate.Year.ToString(), ",", theDate.Month.ToString(), ",1,'", theDate.ToShortDateString(), "') " }) + " insert into EPM_Stuff_Stock (StockCode,ResourceCode,CategoryCode,Quantity,Price,ModifyDate)";
            object obj8 = string.Concat(new object[] { obj7, " select '", billCode.ToString(), "',ResourceCode,CategoryCode,Quantity,Price,'", DateTime.Today.ToShortDateString(), "' from EPM_Stuff_StockCheck where (StockCode=(select StockCode from EPM_Stuff_StockBill where  ProjectCode='", ProjectCode, "' and typeid=1 and thedate='", theDate.AddMonths(-1).ToShortDateString(), "')) " });
            object obj9 = string.Concat(new object[] { obj8, " insert into EPM_Stuff_StockBill values('", ProjectCode, "','", billCode.ToString(), "','", addDate.ToShortDateString(), "',", theDate.Year.ToString(), ",", theDate.Month.ToString(), ",2,'", theDate.ToShortDateString(), "') " }) + " insert into EPM_Stuff_StockCheck ";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj9, " select '", ProjectCode, "',StockCode,Resourcecode,CategoryCode,Quantity,Price,ModifyDate from EPM_Stuff_Stock where  stockCode='", billCode.ToString(), "' " }) + " end " + " end ");
        }

        public static void delCheckNote(string pp, Guid prj)
        {
            string str = "begin ";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, "delete EPM_Stuff_StockCheck_Detail where CheckBillCode='", pp, "'AND ProjectCode='", prj, "' " });
            publicDbOpClass.ExecuteSQL(string.Concat(new object[] { obj3, "delete EPM_Stuff_StockCheck_Main where CheckBillCode='", pp, "' AND ProjectCode='", prj, "' " }) + "end");
        }

        public static bool DelResource(Guid billCode, string resourceCode)
        {
            string str = "";
            str = " begin ";
            string str2 = str;
            string str3 = str2 + " delete from EPM_Stuff_StockCheck where StockCode='" + billCode.ToString() + "' and resourceCode='" + resourceCode + "' ";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from EPM_Stuff_Stock where StockCode='" + billCode.ToString() + "' and resourceCode='" + resourceCode + "' ") + " end ");
        }

        public static bool DelStock(Guid stockCode)
        {
            string str = " begin ";
            return publicDbOpClass.NonQuerySqlString((((str + " delete from EPM_Stuff_Stock where stockCode='" + stockCode.ToString() + "' ") + " delete from EPM_Stuff_StockCheck where stockCode='" + stockCode.ToString() + "' ") + " delete from EPM_Stuff_StockBill where stockCode='" + stockCode.ToString() + "' ") + " end ");
        }

        public static DataTable getCheckTable(Guid theprj, int yy, int mm)
        {
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { "SELECT dbo.EPM_V_Res_ResourceBasic.ResourceName, dbo.EPM_Stuff_StockCheck_Detail.ResourceCode,dbo.EPM_Stuff_StockCheck_Detail.Quantity,dbo.EPM_V_Res_ResourceBasic.Specification,dbo.EPM_V_Res_ResourceBasic.UnitName,dbo.EPM_Stuff_StockCheck_Main.TheYear,dbo.EPM_Stuff_StockCheck_Main.TheMonth, dbo.EPM_Stuff_StockCheck_Main.ProjectCode,dbo.EPM_Stuff_StockCheck_Detail.Price FROM  dbo.EPM_V_Res_ResourceBasic INNER JOIN dbo.EPM_Stuff_StockCheck_Detail ON dbo.EPM_V_Res_ResourceBasic.ResourceCode = dbo.EPM_Stuff_StockCheck_Detail.ResourceCode LEFT OUTER JOIN dbo.EPM_Stuff_StockCheck_Main ON dbo.EPM_Stuff_StockCheck_Detail.ProjectCode = dbo.EPM_Stuff_StockCheck_Main.ProjectCode AND dbo.EPM_Stuff_StockCheck_Detail.CheckBillCode = dbo.EPM_Stuff_StockCheck_Main.CheckBillCode WHERE (dbo.EPM_Stuff_StockCheck_Main.ProjectCode ='", theprj, "') AND (dbo.EPM_Stuff_StockCheck_Main.TheYear ='", yy, "') AND (dbo.EPM_Stuff_StockCheck_Main.TheMonth = '", mm, "')" }));
        }

        public static int getCheckType(int year, int mon, Guid prj)
        {
            return Convert.ToInt32(publicDbOpClass.DataTableQuary(string.Concat(new object[] { "SELECT TheType FROM EPM_Stuff_StockCheck_Main  WHERE TheYear='", year, "' AND TheMonth='", mon, "' and ProjectCode='", prj, "'" })).Rows[0][0].ToString());
        }

        public static int getMaxMonthFromYear(int theyear, Guid prj)
        {
            string str2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "SELECT max(TheMonth) FROM EPM_Stuff_StockCheck_Main WHERE TheYear=", theyear, " AND ProjectCode='", prj, "'" })).Rows[0][0].ToString();
            if (str2.Equals(""))
            {
                return 0;
            }
            return Convert.ToInt16(str2);
        }

        public static int getMaxYear(Guid prj)
        {
            string str2 = publicDbOpClass.DataTableQuary("SELECT max(TheYear) FROM EPM_Stuff_StockCheck_Main WHERE  ProjectCode='" + prj + "'").Rows[0][0].ToString();
            if (str2.Equals(""))
            {
                return 0;
            }
            return Convert.ToInt16(str2);
        }

        public static DataTable GetProject(string usercode)
        {
            return publicDbOpClass.DataTableQuary(" select PrjName,prjGuid from PT_PrjInfo where   IsValid=1 and  ( Podepom LIKE '%" + usercode + "%' or  PrjManager LIKE '%" + usercode + "%')  and prjState<>3 ");
        }

        public static DataTable GetResource(Guid ProjectCode, Guid stockCode, string strCategoryCode, int typeid, bool isView)
        {
            string sqlString = "";
            if (typeid == 1)
            {
                sqlString = string.Concat(new object[] { "select a.ResourceCode,a.ResourceName,a.CategoryCode,a.Specification,a.UnitName, b.Quantity,b.Price,b.ModifyDate from EPM_Stuff_Stock b join EPM_Stuff_StockBill c on b.StockCode=c.StockCode and c.TypeId=1 left join v_Res_Resource a on a.ResourceCode=b.ResourceCode where c.ProjectCode='", ProjectCode, "' and b.StockCode='", stockCode.ToString(), "' and a.CategoryCode like '", strCategoryCode, "%' " });
            }
            else
            {
                sqlString = string.Concat(new object[] { "select a.ResourceCode,a.ResourceName,a.CategoryCode,a.Specification,a.UnitName, b.Quantity,b.Price,b.ModifyDate from EPM_Stuff_StockCheck b left join v_Res_Resource a on a.ResourceCode=b.ResourceCode where b.ProjectCode='", ProjectCode, "' and b.StockCode='", stockCode.ToString(), "' and a.CategoryCode like '", strCategoryCode, "%' " });
            }
            if (isView)
            {
                sqlString = sqlString + "and b.Quantity<>0";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static int getServerMonth()
        {
            string sqlString = "select convert(varchar(10),getdate(),110)";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            string str2 = table.Rows[0][0].ToString().Substring(0, 2);
            Convert.ToInt32(table.Rows[0][0].ToString().Substring(table.Rows[0][0].ToString().Length - 4));
            return Convert.ToInt32(str2);
        }

        public static bool Insert_EPM_Stuff_StockCheck_Detail(string billcode, Guid prj, string res, decimal nnum)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "Insert EPM_Stuff_StockCheck_Detail (CheckBillCode,ProjectCode,ResourceCode,Quantity) VALUES ('", billcode, "','", prj, "','", res, "','", nnum, "')" }));
        }

        public static bool insert_stuff_Stockcheck_main(string billcode, Guid prjcode, int strYear, int strMouth, DateTime datanow, string checkMan, int thetype)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "insert EPM_Stuff_StockCheck_Main(CheckBillCode,ProjectCode,TheYear,TheMonth,TheDate,CheckMan,TheType) values('", billcode, "','", prjcode, "','", strYear, "','", strMouth, "','", datanow, "','", checkMan, "','", thetype, "')" }));
        }

        public static bool ischeckORpans(int year, int mon, Guid prj)
        {
            return (publicDbOpClass.DataTableQuary(string.Concat(new object[] { "SELECT isnull(TheYear,0) FROM EPM_Stuff_StockCheck_Main  WHERE TheYear='", year, "' AND TheMonth='", mon, "' and ProjectCode='", prj, "'" })).Rows.Count != 0);
        }

        public static DataTable QueryMaxMonth(int type)
        {
            string sqlString = "";
            sqlString = "select max(theDate) as theDate from EPM_Stuff_StockBill where typeid=2";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryMonthList(Guid ProjectCode, int type)
        {
            string sqlString = "";
            if (type == 1)
            {
                object obj2 = ("select thedate,stockcode,typeid from EPM_Stuff_StockBill where ProjectCode='" + ProjectCode + "' and typeid=1 ") + " union ";
                sqlString = string.Concat(new object[] { obj2, "select dateadd(m,1,thedate) as theDate,stockcode,typeid from EPM_Stuff_StockBill where ProjectCode='", ProjectCode, "' and typeid=1 order by TheDate desc" });
            }
            else
            {
                sqlString = "select  thedate,stockcode,typeid from EPM_Stuff_StockBill where ProjectCode='" + ProjectCode + "' and typeid=1 order by TheDate desc";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryPrjName(string usercode)
        {
            return publicDbOpClass.DataTableQuary("SELECT PrjCode,PrjGuid,PrjName FROM PT_PrjInfo where IsValid=1 and  Podepom like '%" + usercode + "%'order by RecordDate");
        }

        public static DataTable QueryProjectStock(Guid projectCode)
        {
            return publicDbOpClass.DataTableQuary("SELECT dbo.EPM_Stuff_Stock.ProjectCode,dbo.EPM_Stuff_Stock.Quantity,dbo.EPM_Stuff_Stock.Price,dbo.EPM_Stuff_Stock.ResourceCode, dbo.EPM_Stuff_Stock.ProjectCode,dbo.EPM_V_Res_ResourceBasic.ResourceName, dbo.EPM_V_Res_ResourceBasic.Specification, dbo.EPM_V_Res_ResourceBasic.UnitName FROM dbo.EPM_Stuff_Stock INNER JOIN dbo.EPM_V_Res_ResourceBasic ON dbo.EPM_Stuff_Stock.ResourceCode = dbo.EPM_V_Res_ResourceBasic.ResourceCode WHERE  ProjectCode='" + projectCode + "'");
        }

        public static DataTable QueryProjectStockALL()
        {
            string sqlString = "SELECT dbo.EPM_Stuff_Stock.ProjectCode,dbo.EPM_Stuff_Stock.Quantity,dbo.EPM_Stuff_Stock.Price, dbo.EPM_Stuff_Stock.ResourceCode, dbo.EPM_Stuff_Stock.ProjectCode,dbo.EPM_V_Res_ResourceBasic.ResourceName, dbo.EPM_V_Res_ResourceBasic.Specification, dbo.EPM_V_Res_ResourceBasic.UnitName FROM dbo.EPM_Stuff_Stock INNER JOIN dbo.EPM_V_Res_ResourceBasic ON dbo.EPM_Stuff_Stock.ResourceCode = dbo.EPM_V_Res_ResourceBasic.ResourceCode";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable QueryStock(Guid billCode, int type, bool isAll)
        {
            string sqlString = "";
            if (type == 1)
            {
                if (isAll)
                {
                    sqlString = " select a.ResourceCode,b.ResourceName,b.specification,b.UnitName,a.quantity,a.price from (select * from EPM_Stuff_Stock where StockCode='" + billCode.ToString() + "') a left join v_Res_Resource b on a.ResourceCode=b.ResourceCode";
                }
                else
                {
                    sqlString = " select a.ResourceCode,b.ResourceName,b.specification,b.UnitName,a.quantity,a.price from (select * from EPM_Stuff_Stock where StockCode='" + billCode.ToString() + "') a left join v_Res_Resource b on a.ResourceCode=b.ResourceCode where a.Quantity>0";
                }
            }
            else if (isAll)
            {
                sqlString = " select a.ResourceCode,b.ResourceName,b.specification,b.UnitName,a.quantity,a.price from (select * from EPM_Stuff_StockCheck where StockCode='" + billCode.ToString() + "') a left join v_Res_Resource b on a.ResourceCode=b.ResourceCode";
            }
            else
            {
                sqlString = " select a.ResourceCode,b.ResourceName,b.specification,b.UnitName,a.quantity,a.price from (select * from EPM_Stuff_StockCheck where StockCode='" + billCode.ToString() + "') a left join v_Res_Resource b on a.ResourceCode=b.ResourceCode where a.Quantity>0";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static bool UP_EPM_Stuff_StockCheck_Detail(string billcode, Guid Prjc, string resc, decimal nnum)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update EPM_Stuff_StockCheck_Detail set Quantity='", nnum, "' where CheckBillCode='", billcode, "' AND ProjectCode='", Prjc, "' AND ResourceCode='", resc, "'" }));
        }

        public static void upCheckMain(string billcode)
        {
            publicDbOpClass.ExecuteSQL(string.Concat(new object[] { "update EPM_Stuff_StockCheck_Main set TheDate='", DateTime.Now, "' where CheckBillCode='", billcode, "' " }));
        }

        public static bool UpdaStuffStock(Guid prj, decimal quantity, string resourceCode)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "UPDATE EPM_Stuff_Stock SET Quantity='", quantity, "' where ProjectCode='", prj, "' and ResourceCode='", resourceCode, "'" }));
        }

        public static bool UpdOneResource(Guid billCode, string resourceCode, decimal quantity, DateTime modDate)
        {
            string str = "";
            str = " begin ";
            string str2 = str;
            return publicDbOpClass.NonQuerySqlString((str2 + " update EPM_Stuff_StockCheck set Quantity=" + quantity.ToString() + " where Stockcode='" + billCode.ToString() + "' and ResourceCode='" + resourceCode + "'") + " end ");
        }
    }
}

