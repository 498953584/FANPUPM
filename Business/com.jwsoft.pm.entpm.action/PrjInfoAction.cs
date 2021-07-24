namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.web.WebControls;
    using System;
    using System.Data;

    public class PrjInfoAction
    {
        public static int ADD(PrjInfoModel PIM)
        {
            string str = " insert into PT_PrjInfo_ZTB values ( ";
            object obj2 = str;
            object obj3 = string.Concat(new object[] { obj2, " '", PIM.PrjCode, "','", PIM.PrjGuid, "','", PIM.PrjName, "','", PIM.StartDate, "', " });
            string str2 = string.Concat(new object[] { obj3, " '", PIM.EndDate, "','", PIM.ComputeClass, "','", PIM.RationClass, "', " });
            string str3 = str2 + " '" + PIM.PrjCost + "','" + PIM.ContractSum + "','" + PIM.Duration + "', ";
            string str4 = str3 + " '" + PIM.QualityClass + "','" + PIM.Area + "','" + PIM.PrjKindClass + "', ";
            string str5 = str4 + " '" + PIM.PrjPlace + "','" + PIM.Remark + "','" + PIM.FileName + "','" + PIM.FileURL + "' ,";
            object obj4 = str5 + " '" + PIM.Owner + "','" + PIM.Counsellor + "','" + PIM.Designer + "',";
            string str6 = string.Concat(new object[] { obj4, " '", PIM.Inspector, "','", PIM.PrjInfo, "','", PIM.PrjState, "','", PIM.OwnerCode, "','", Guid.Empty, "'," });
            string str7 = str6 + " '" + PIM.Rank + "','" + PIM.BudgetWay + "','" + PIM.ContractWay + "',";
            string str8 = str7 + " '" + PIM.PayCondition + "','" + PIM.TenderWay + "','" + PIM.PayWay + "',";
            string str9 = str8 + " '" + PIM.KeyPart + "','" + PIM.WorkUnit + "','" + PIM.LinkMan + "',";
            string str10 = str9 + " '" + PIM.PrjManager + "','" + PIM.BuildingType + "','" + PIM.TotalHouseNum + "',";
            string str11 = str10 + " '" + PIM.BuildingArea + "','" + PIM.UsegrounArea + "','" + PIM.UndergroundArea + "',";
            return publicDbOpClass.ExecSqlString((str11 + " '" + PIM.PrjFundInfo + "','" + PIM.OtherStatement + "'") + "\t)");
        }

        public static int ADD2(string TypeCode, string UserCode, string RecordDate, PrjInfoModel PIM)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("PT_PrjInfo", "i_XH")) + 1;
            string str2 = (" begin " + " insert into PT_PrjInfo (TypeCode,i_xh,UserCode,RecordDate,IsValid ,i_childNum,PrjCode,PrjGuid,PrjName,StartDate,EndDate," + "ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,Area,PrjKindClass,PrjPlace,Remark1,FileName,FileURL,") + "Owner,Counsellor,Designer,Inspector,PrjInfo,PrjState,OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay," + "KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement";
            object obj2 = str2 + " ) values ('" + TypeCode + "'," + num.ToString() + ",'" + UserCode + "','" + RecordDate + "','1',0,";
            object obj3 = string.Concat(new object[] { obj2, " '", PIM.PrjCode, "','", PIM.PrjGuid, "','", PIM.PrjName, "','", PIM.StartDate, "', " });
            string str3 = string.Concat(new object[] { obj3, " '", PIM.EndDate, "','", PIM.ComputeClass, "','", PIM.RationClass, "', " });
            string str4 = str3 + " '" + PIM.PrjCost + "','" + PIM.ContractSum + "','" + PIM.Duration + "', ";
            string str5 = str4 + " '" + PIM.QualityClass + "','" + PIM.Area + "','" + PIM.PrjKindClass + "', ";
            string str6 = str5 + " '" + PIM.PrjPlace + "','" + PIM.Remark + "','" + PIM.FileName + "','" + PIM.FileURL + "' ,";
            object obj4 = str6 + " '" + PIM.Owner + "','" + PIM.Counsellor + "','" + PIM.Designer + "',";
            string str7 = string.Concat(new object[] { obj4, " '", PIM.Inspector, "','", PIM.PrjInfo, "','", PIM.PrjState, "','", PIM.OwnerCode, "','", Guid.Empty, "'," });
            string str8 = str7 + " '" + PIM.Rank + "','" + PIM.BudgetWay + "','" + PIM.ContractWay + "',";
            string str9 = str8 + " '" + PIM.PayCondition + "','" + PIM.TenderWay + "','" + PIM.PayWay + "',";
            string str10 = str9 + " '" + PIM.KeyPart + "','" + PIM.WorkUnit + "','" + PIM.LinkMan + "',";
            string str11 = str10 + " '" + PIM.PrjManager + "','" + PIM.BuildingType + "','" + PIM.TotalHouseNum + "',";
            string str12 = str11 + " '" + PIM.BuildingArea + "','" + PIM.UsegrounArea + "','" + PIM.UndergroundArea + "',";
            object obj5 = (str12 + " '" + PIM.PrjFundInfo + "','" + PIM.OtherStatement + "'") + "\t)";
            string str13 = string.Concat(new object[] { obj5, " INSERT INTO PT_PrjInfo_ZTB_Detail(PrjGuid, IsTender,SetUpFlowState) VALUES('", PIM.PrjGuid, "', '1', '1')" });
            return publicDbOpClass.ExecSqlString((str13 + " update PT_PrjInfo set i_childNum = (select count(1) from PT_PrjInfo where (TypeCode like '" + TypeCode.Substring(0, TypeCode.Length - 5) + "%') and (len(TypeCode)= " + TypeCode.Length.ToString() + ")) where TypeCode = '" + TypeCode.Substring(0, TypeCode.Length - 5) + "'") + " end");
        }

        public static bool CheckCode(string Code)
        {
            bool flag = true;
            if (publicDbOpClass.DataTableQuary("select 1 from PT_PrjInfo_ZTB where PrjCode = '" + Code + "'").Rows.Count > 0)
            {
                flag = false;
            }
            return flag;
        }

        public static int Del(string Code)
        {
            string str2 = "";
            publicDbOpClass.ExecuteScalar("select PrjCode from PT_PrjInfo_ZTB where PrjGuid = '" + Code + "'").ToString();
            str2 = "begin ";
            return publicDbOpClass.ExecSqlString((str2 + " delete from PT_PrjInfo_ZTB where PrjGuid = '" + Code + "'") + " end");
        }

        public static DataTable GetDateList(string SqlWhere)
        {
            string str = "select PrjCode,PrjName from pt_PrjInfo where ";
            return publicDbOpClass.DataTableQuary(str + SqlWhere);
        }

        public static string getMaxCode()
        {
            string sqlString = "select max(TypeCode) from PT_PrjInfo where len(TypeCode)=5";
            return Convert.ToString((int) (int.Parse(publicDbOpClass.DataTableQuary(sqlString).Rows[0][0].ToString()) + 1));
        }

        public static DataTable GetPageData(string SqlWhere, string TableName)
        {
            if (SqlWhere.Trim() != "")
            {
                SqlWhere = " where " + SqlWhere;
            }
            return publicDbOpClass.DataTableQuary("select * from " + TableName + SqlWhere);
        }

        public static DataTable GetPageData(string SqlWhere, object PageControl, int iPageSize, string TableName)
        {
            int num = 1;
            PaginationControl control = (PaginationControl) PageControl;
            num = ((publicDbOpClass.GetRecordCount(TableName, SqlWhere) - 1) / iPageSize) + 1;
            control.PageCount = num;
            return publicDbOpClass.GetRecordFromPage(TableName, "PrjCode", iPageSize, control.CurrentPageIndex, 1, SqlWhere);
        }

        public static string GetProjectNameOfCode(string PrjCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select PrjName from PT_PRJInfo where  PrjGUID ='" + PrjCode + "'");
            if (obj2 != null)
            {
                return obj2.ToString();
            }
            return null;
        }

        public static DataTable GetSingle(string Code)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_PrjInfo_ZTB where  PrjGuid = '" + Code + "'");
        }

        public static PrjInfoModel GetSingleInfo(string Code)
        {
            PrjInfoModel model = new PrjInfoModel();
            foreach (DataRow row in GetSingle(Code).Rows)
            {
                model.PrjCode = row["PrjCode"].ToString();
                model.PrjGuid = new Guid(row["PrjGuid"].ToString());
                model.PrjName = row["PrjName"].ToString();
                try
                {
                    model.StartDate = (DateTime) row["StartDate"];
                    model.EndDate = (DateTime) row["EndDate"];
                }
                catch (Exception)
                {
                }
                model.ComputeClass = row["ComputeClass"].ToString();
                model.RationClass = row["RationClass"].ToString();
                model.PrjCost = row["PrjCost"].ToString();
                model.ContractSum = row["ContractSum"].ToString();
                model.Duration = row["Duration"].ToString();
                model.QualityClass = row["QualityClass"].ToString();
                model.Area = row["Area"].ToString();
                model.PrjKindClass = row["PrjKindClass"].ToString();
                model.PrjPlace = row["PrjPlace"].ToString();
                model.Remark = row["Remark"].ToString();
                model.FileName = row["FileName"].ToString();
                model.FileURL = row["FileURL"].ToString();
                model.Owner = row["Owner"].ToString();
                model.Counsellor = row["Counsellor"].ToString();
                model.Designer = row["Designer"].ToString();
                model.Inspector = row["Inspector"].ToString();
                model.PrjInfo = row["PrjInfo"].ToString();
                model.PrjState = row["PrjState"].ToString();
                model.OwnerCode = row["OwnerCode"].ToString();
                model.Rank = row["Rank"].ToString();
                model.BudgetWay = row["BudgetWay"].ToString();
                model.ContractWay = row["ContractWay"].ToString();
                model.PayCondition = row["PayCondition"].ToString();
                model.TenderWay = row["TenderWay"].ToString();
                model.PayWay = row["PayWay"].ToString();
                model.KeyPart = row["KeyPart"].ToString();
                model.WorkUnit = row["WorkUnit"].ToString();
                model.LinkMan = row["LinkMan"].ToString();
                model.PrjManager = row["PrjManager"].ToString();
                model.BuildingType = row["BuildingType"].ToString();
                model.TotalHouseNum = row["TotalHouseNum"].ToString();
                model.BuildingArea = row["BuildingArea"].ToString();
                model.UsegrounArea = row["UsegrounArea"].ToString();
                model.UndergroundArea = row["UndergroundArea"].ToString();
                model.PrjFundInfo = row["PrjFundInfo"].ToString();
                model.OtherStatement = row["OtherStatement"].ToString();
            }
            return model;
        }

        public static DataTable GetSingleOfCodeGuid(string Code)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_PrjInfo where  prjguid ='" + Code + "'");
            if (table.Rows.Count > 0)
            {
                return table;
            }
            return null;
        }

        public static string GetSqlWhere(PrjInfoModel PIM)
        {
            string str = "( 1=1)";
            if (((((PIM.StartDate == PIM.EndDate) && (PIM.StartDate == DateTime.Today)) && ((PIM.PrjCode.Trim() == "") && (PIM.PrjName.Trim() == ""))) && (((PIM.Owner.Trim() == "") && (PIM.PrjKindClass.Trim() == "9999")) && ((PIM.PrjPlace.Trim() == "9999") && (PIM.PrjState.Trim() == "9999")))) && (PIM.PrjCost.Trim() == "0"))
            {
                return str;
            }
            if (((PIM.Remark.Trim() == "1") && (PIM.StartDate != PIM.EndDate)) && (PIM.StartDate != DateTime.Today))
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "and (StartDate BETWEEN  '", PIM.StartDate, "' AND '", PIM.EndDate, "') or  (EndDate BETWEEN  '", PIM.StartDate, "' AND '", PIM.EndDate, "') " });
            }
            if (PIM.PrjCode.Trim() != "")
            {
                str = str + " and PrjCode like '%25" + PIM.PrjCode + "%'";
            }
            if (PIM.PrjName.Trim() != "")
            {
                str = str + " and PrjName like '%25" + PIM.PrjName + "%'";
            }
            if (PIM.Owner.Trim() != "")
            {
                str = str + " and Owner like '%" + PIM.Owner + "%'";
            }
            if ((PIM.PrjKindClass.Trim() != "") && (PIM.PrjKindClass.Trim() != "9999"))
            {
                str = str + " and PrjKindClass = '" + PIM.PrjKindClass + "'";
            }
            if ((PIM.PrjPlace.Trim() != "") && (PIM.PrjPlace.Trim() != "9999"))
            {
                str = str + " and Area='" + PIM.PrjPlace.Trim() + "'";
            }
            if ((PIM.PrjState.Trim() != "") && (PIM.PrjState.Trim() != "9999"))
            {
                str = str + "and PrjState = '" + PIM.PrjState + "'";
            }
            string prjCost = PIM.PrjCost;
            if (prjCost == null)
            {
                return str;
            }
            if (!(prjCost == "1"))
            {
                if (prjCost != "2")
                {
                    if (prjCost == "3")
                    {
                        return (str + " and (PrjCost >= 500 and PrjCost <= 1000)");
                    }
                    if (prjCost == "4")
                    {
                        return (str + " and ( PrjCost >=1000 and PrjCost <=5000)");
                    }
                    if (prjCost != "5")
                    {
                        return str;
                    }
                    return (str + " and ( PrjCost >5000 )");
                }
            }
            else
            {
                return (str + " and ( PrjCost < 100 )");
            }
            return (str + " and ( PrjCost >= 100 and PrjCost<= 500)");
        }

        public static int Update(PrjInfoModel PIM)
        {
            object obj2 = ("update PT_PrjInfo_ZTB set " + " PrjName= '" + PIM.PrjName + "'") + ",PrjCode= '" + PIM.PrjCode + "'";
            object obj3 = string.Concat(new object[] { obj2, ",StartDate= '", PIM.StartDate, "'" });
            string str2 = ((((((((((((((((((string.Concat(new object[] { obj3, ",EndDate= '", PIM.EndDate, "'" }) + ",ComputeClass= '" + PIM.ComputeClass + "'") + ",RationClass= '" + PIM.RationClass + "'") + ",PrjCost= '" + PIM.PrjCost + "'") + ",ContractSum= '" + PIM.ContractSum + "'") + ",Duration= '" + PIM.Duration + "'") + ",QualityClass= '" + PIM.QualityClass + "'") + ",Area= '" + PIM.Area + "'") + ",PrjKindClass= '" + PIM.PrjKindClass + "'") + ",PrjPlace= '" + PIM.PrjPlace + "      '") + ",Remark= '" + PIM.Remark + "'") + ",FileName= '" + PIM.FileName + "'") + ",FileURL= '" + PIM.FileURL + "'") + ",Owner= '" + PIM.Owner + "'") + ",Counsellor= '" + PIM.Counsellor + "'") + ",Designer= '" + PIM.Designer + "'") + ",Inspector= '" + PIM.Inspector + "'") + ",PrjInfo= '" + PIM.PrjInfo + "'") + ",PrjState= '" + PIM.PrjState + "' ") + ",OwnerCode= '" + PIM.OwnerCode + "' ";
            string str3 = str2 + ",Rank= '" + PIM.Rank + "',BudgetWay='" + PIM.BudgetWay + "',ContractWay='" + PIM.ContractWay + "',";
            string str4 = str3 + "PayCondition= '" + PIM.PayCondition + "',TenderWay='" + PIM.TenderWay + "',PayWay='" + PIM.PayWay + "',";
            string str5 = str4 + " KeyPart='" + PIM.KeyPart + "',WorkUnit='" + PIM.WorkUnit + "',LinkMan='" + PIM.LinkMan + "',";
            string str6 = str5 + "PrjManager= '" + PIM.PrjManager + "',BuildingType='" + PIM.BuildingType + "',TotalHouseNum='" + PIM.TotalHouseNum + "',";
            string str7 = str6 + "BuildingArea= '" + PIM.BuildingArea + "',UsegrounArea='" + PIM.UsegrounArea + "',UndergroundArea='" + PIM.UndergroundArea + "',";
            object obj4 = str7 + "PrjFundInfo= '" + PIM.PrjFundInfo + "',OtherStatement='" + PIM.OtherStatement + "'";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " where PrjGuid= '", PIM.PrjGuid, "'" }));
        }
    }
}

