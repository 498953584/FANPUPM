namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.web.WebControls;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class StuffFirstAction
    {
        public int GetFirstPagecount(int iPageSize, string SqlWhere)
        {
            return (((publicDbOpClass.GetRecordCount("v_Stuff_PartyABill", SqlWhere) - 1) / iPageSize) + 1);
        }

        public DataTable GetFirstPageData(int iPageSize, PaginationControl PageCtrl, string strWhere)
        {
            return publicDbOpClass.GetRecordFromPage("v_Stuff_PartyABill", "FirstStuffID", iPageSize, PageCtrl.CurrentPageIndex, 1, strWhere);
        }

        public int GetPageCount(int iPageSize, string SqlWhere)
        {
            return (((publicDbOpClass.GetRecordCount("EPM_Stuff_PartyABill", SqlWhere) - 1) / iPageSize) + 1);
        }

        public DataTable GetPageData(int iPageSize, PaginationControl PageCtrl, string strWhere)
        {
            return publicDbOpClass.GetRecordFromPage("EPM_Stuff_PartyABill", "FirstID", iPageSize, PageCtrl.CurrentPageIndex, 1, strWhere);
        }

        public DataTable GetPrjCode(string strPrjCode)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_PrjInfo where prjguid='" + strPrjCode + "'");
        }

        public DataTable GetScheduleList(Guid projectCode, string sccode)
        {
            new WBSBidTaskCollection();
            return publicDbOpClass.DataTableQuary("select * from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and IsValid = 1 and TaskCode='" + sccode + "'");
        }

        public int StuffFirstInsert(StuffFirstMain sm)
        {
            string str = "";
            string str2 = str + "insert into EPM_Stuff_PartyABill(ProjectCode,FirstClass,Maker,FirstSummary,Remark,FirstAddDate,WarrantCode) values ('";
            string str3 = str2 + sm.ProjectCode + " ','" + sm.FirstClass + "','";
            return publicDbOpClass.ExecSqlString(str3 + sm.Maker + "','" + sm.FirstSummary + "','" + sm.Remark + "','" + sm.FirstAddDate + "','" + sm.WarrantCode + "')");
        }

        public int StuffFirstMainDel(int firstid)
        {
            string str = " begin";
            return publicDbOpClass.ExecSqlString(((str + " delete EPM_Stuff_PartyABill where FirstID=" + firstid) + "delete EPM_Stuff_PartyADetail where FirstID=" + firstid) + " end");
        }

        public DataTable StuffFirstSel(int firstid)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "select * from EPM_Stuff_PartyABill where FirstID =" + firstid);
        }

        public int StuffFirstStuffEdit(ArrayList stufflist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("begin ");
            if (stufflist.Count > 0)
            {
                int count = stufflist.Count;
                for (int i = 0; i < count; i++)
                {
                    builder.Append(((((((" update EPM_Stuff_PartyADetail set StuffNumber = " + ((StuffFirstStuff) stufflist[i]).StuffNumber + " ,") + " StuffPrice = '" + ((StuffFirstStuff) stufflist[i]).StuffPrice + "',") + " FirstBalance = '" + ((StuffFirstStuff) stufflist[i]).FirstBalance + "',") + " FirstUnit = '" + ((StuffFirstStuff) stufflist[i]).FirstUnit + "',") + " FirstMoney = '" + ((StuffFirstStuff) stufflist[i]).FirstMoney + "'") + " where FirstStuffID = " + ((StuffFirstStuff) stufflist[i]).FirstStuffID) + ";");
                }
            }
            builder.Append(" end");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable StuffFirstStuffSel(int firstid)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "select * from EPM_Stuff_PartyADetail where FirstID = " + firstid);
        }

        public int StuffFirstUp(StuffFirstMain sm)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString((((((((str + "update EPM_Stuff_PartyABill set ProjectCode = '" + sm.ProjectCode + "',") + " FirstAddDate = '" + sm.FirstAddDate + "' ,") + " Maker = '" + sm.Maker + "',") + " Remark = '" + sm.Remark + "',") + " FirstClass = '" + sm.FirstClass + "',") + " FirstSummary = '" + sm.FirstSummary + "',") + " WarrantCode = '" + sm.WarrantCode + "'") + " where FirstID = " + sm.FirstID);
        }

        public int StuffFistDel(int firstStuffID)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + "delete EPM_Stuff_PartyADetail where FirstStuffID = " + firstStuffID);
        }

        public bool StuffFistList(int firstid, string sessioinCode, string userCode, DateTime dt)
        {
            string str = "";
            object obj2 = str + " begin " + " insert into EPM_Stuff_PartyADetail (FirstID,StuffName,StuffCode,StuffUnit,StuffSpec,StuffNumber,FirstAddDate,StuffPrice,FirstMoney) ";
            string str2 = string.Concat(new object[] { obj2, " select ", firstid, ",ResourceName,ResourceCode,UnitName,Specification,0,'", dt, "',Price,0 from EPM_Res_TempResource " });
            string str3 = str2 + " where SessionCode='" + sessioinCode + "' and UserCode='" + userCode + "' ";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from EPM_Res_TempResource where SessionCode='" + sessioinCode + "' and UserCode='" + userCode + "' ") + " end ");
        }
    }
}

