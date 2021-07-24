namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class ResourceItemAction
    {
        public int AddNewBindResource(Guid operateSession, string userCode, ArrayList resourceCodeList)
        {
            string str = this.ContactResourceCode(resourceCodeList);
            if (str == "")
            {
                return 1;
            }
            string str2 = "";
            return publicDbOpClass.ExecSqlString(string.Format((((str2 + " begin ") + " update EPM_Res_TempResource set State=1 where (SessionCode='{0}')and(UserCode='{1}')and(ResourceCode in ({2}))and(State=0)" + " insert into EPM_Res_TempResource(ResourceCode,SessionCode,UserCode,ResourceName,Specification,UnitID,UnitName,PriceItemID,PriceItemName,Price,State,CategoryCode,categoryName,ResourceStyle) ") + " select * from (select distinct isnull(a.ResourceCode,'') as ResourceCode,'{0}' as SessionCode ,'{1}' as UserCode ,isnull(a.ResourceName,'') as ResourceName,isnull(b.Specification,'') as Specification,isnull(b.UnitID,0) as UnitID,isnull(b.UnitName,'') as UnitName,isnull(PriceItemID,0) as PriceItemID,isnull(b.PriceItemName,'') as PriceItemName,isnull(b.Price,0) as Price,2 as State,isnull(b.categorycode,'') as categorycode,isnull(b.categoryname,'') as categoryname,isnull(a.resourcestyle,0) as resourcestyle " + " from EPM_Task_Resource a left join EPM_V_Res_ResourceBasic b on a.ResourceCode=b.ResourceCode and a.ResourceName=b.ResourceName where a.ResourceCode in ({2})) a") + " where a.ResourceCode not in (select ResourceCode from EPM_Res_TempResource where (SessionCode='{0}')and(UserCode='{1}')and(State=1)) " + " end ", operateSession.ToString(), userCode, str));
        }

        public int AddNewBindResource(Guid operateSession, string userCode, ArrayList resourceCodeList, string prjcode)
        {
            string str = this.ContactResourceCode(resourceCodeList);
            if (str == "")
            {
                return 1;
            }
            string str2 = "";
            return publicDbOpClass.ExecSqlString(string.Format((((str2 + " begin " + " update EPM_Res_TempResource set State=1 where (SessionCode='{0}')and(UserCode='{1}')and(ResourceCode in ({2}))and(State=0)") + " insert into EPM_Res_TempResource(ResourceCode,SessionCode,UserCode,ResourceName,Specification,UnitID,UnitName,PriceItemID,PriceItemName,Price,State,CategoryCode,categoryName,ResourceStyle) " + " select * from (select distinct isnull(a.ResourceCode,'') as ResourceCode,'{0}' as SessionCode ,'{1}' as UserCode ,isnull(a.ResourceName,'') as ResourceName,isnull(b.Specification,'') as Specification,isnull(b.UnitID,0) as UnitID,isnull(b.UnitName,'') as UnitName,isnull(PriceItemID,0) as PriceItemID,isnull(b.PriceItemName,'') as PriceItemName,isnull(b.Price,0) as Price,2 as State,isnull(b.categorycode,'') as categorycode,isnull(b.categoryname,'') as categoryname,isnull(a.resourcestyle,0) as resourcestyle ") + " from (select * from EPM_Task_Resource where projectcode='" + prjcode + "') a left join EPM_V_Res_ResourceBasic b on a.ResourceCode=b.ResourceCode and a.ResourceName=b.ResourceName where a.ResourceCode in ({2})) a") + " where a.ResourceCode not in (select ResourceCode from EPM_Res_TempResource where (SessionCode='{0}')and(UserCode='{1}')and(State=1)) " + " end ", operateSession.ToString(), userCode, str));
        }

        public int AddNewResource(Guid operateSession, string userCode, ArrayList resourceCodeList)
        {
            string str = this.ContactResourceCode(resourceCodeList);
            if (str == "")
            {
                return 1;
            }
            string str2 = "";
            return publicDbOpClass.ExecSqlString(string.Format((((str2 + " begin ") + " update EPM_Res_TempResource set State=1 where (SessionCode='{0}')and(UserCode='{1}')and(ResourceCode in ({2}))and(State=0)" + " insert into EPM_Res_TempResource ") + " select * from (select '{0}' as SessionCode ,'{1}' as UserCode ,ResourceCode,ResourceName,Specification,UnitID,UnitName,PriceItemID,PriceItemName,Price,2 as State,categorycode,categoryname,resourcestyle " + " from EPM_V_Res_ResourceBasic where ResourceCode in ({2})) a") + " where a.ResourceCode not in (select ResourceCode from EPM_Res_TempResource where (SessionCode='{0}')and(UserCode='{1}')and(State=1)) " + " end ", operateSession.ToString(), userCode, str));
        }

        public int AddResourcrItem(ResourceItem objMaterialIteml, ArrayList objArrUnit)
        {
            string str = "";
            string str2 = str + " begin " + " insert into EPM_Res_Resource(VersionCode,ResourceCode,CategoryCode,ResourceName,Specification,ResourceStyle,ResourceType,IsValid,Owner,VersionTime)";
            string str3 = str2 + " values('" + objMaterialIteml.VersionCode.ToString() + "','" + objMaterialIteml.ResourceCode + "', '" + objMaterialIteml.CategoryCode + "', ";
            string str4 = ((str3 + " '" + objMaterialIteml.ResourceName + "', '" + objMaterialIteml.Specification + "'," + Convert.ToInt32(objMaterialIteml.RStyle).ToString() + ", " + objMaterialIteml.ResourceType.ToString() + ",") + " '" + Convert.ToInt32(objMaterialIteml.IsValid).ToString() + "','000000',getdate()) ") + " insert into EPM_Res_PriceRelations (VersionCode,ResourceCode,PriceItemID,Price,Owner,VersionTime) ";
            str = str4 + " select '" + objMaterialIteml.VersionCode.ToString() + "','" + objMaterialIteml.ResourceCode + "',PriceItemID,0,'000000',getdate() from EPM_Res_PriceItem ";
            if (objArrUnit.Count > 0)
            {
                for (int i = 0; i < objArrUnit.Count; i++)
                {
                    object obj2 = ((str + " insert into  EPM_Res_Gauge(VersionCode,ResourceCode,UnitID,Quotiety,IsDefault,IsValid,Owner,VersionTime) ") + " values( '" + objMaterialIteml.VersionCode.ToString() + "',") + " '" + ((ResourceUnit) objArrUnit[i]).ResourceCode + "', ";
                    object obj3 = string.Concat(new object[] { obj2, " '", ((ResourceUnit) objArrUnit[i]).UnitID, "', " });
                    str = ((string.Concat(new object[] { obj3, " '", ((ResourceUnit) objArrUnit[i]).Quotiety, "', " }) + " '" + Convert.ToInt32(((ResourceUnit) objArrUnit[i]).IsDefault).ToString() + "',") + " '" + Convert.ToInt32(((ResourceUnit) objArrUnit[i]).IsValid).ToString() + "', ") + " '000000',getdate()) ";
                }
            }
            return publicDbOpClass.ExecSqlString(str + " end ");
        }

        public int AddResourcrItem(ResourceItem objMaterialIteml, decimal price, string unitid)
        {
            string str = "";
            string str2 = str + " begin " + " insert into EPM_Res_Resource(VersionCode,ResourceCode,CategoryCode,ResourceName,Specification,ResourceStyle,ResourceType,IsValid,Owner,VersionTime)";
            string str3 = str2 + " values('" + objMaterialIteml.VersionCode.ToString() + "','" + objMaterialIteml.ResourceCode + "', '" + objMaterialIteml.CategoryCode + "', ";
            object obj2 = ((((((((str3 + " '" + objMaterialIteml.ResourceName + "', '" + objMaterialIteml.Specification + "'," + Convert.ToInt32(objMaterialIteml.RStyle).ToString() + ", " + objMaterialIteml.ResourceType.ToString() + ",") + " '" + Convert.ToInt32(objMaterialIteml.IsValid).ToString() + "','000000',getdate()) ") + " insert into  EPM_Res_Gauge(VersionCode,ResourceCode,UnitID,Quotiety,IsDefault,IsValid,Owner,VersionTime) ") + " values( '" + objMaterialIteml.VersionCode.ToString() + "',") + " '" + objMaterialIteml.ResourceCode + "', ") + " '" + unitid + "', ") + " 1, ") + " '1'," + " '1', ") + " '000000',getdate()) " + " insert into EPM_Res_PriceRelations (VersionCode,ResourceCode,PriceItemID,Price,Owner,VersionTime) ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " select '", objMaterialIteml.VersionCode.ToString(), "','", objMaterialIteml.ResourceCode, "',PriceItemID,'", price, "','000000',getdate() from EPM_Res_PriceItem " }) + " end ");
        }

        public int AddResourcrItem(ResourceItem objMaterialIteml, decimal price, string unitid, Guid stockCode, string pc)
        {
            string str = "";
            string str2 = str + " begin " + " insert into EPM_Res_Resource(VersionCode,ResourceCode,CategoryCode,ResourceName,Specification,ResourceStyle,ResourceType,IsValid,Owner,VersionTime)";
            string str3 = str2 + " values('" + objMaterialIteml.VersionCode.ToString() + "','" + objMaterialIteml.ResourceCode + "', '" + objMaterialIteml.CategoryCode + "', ";
            object obj2 = ((((((((str3 + " '" + objMaterialIteml.ResourceName + "', '" + objMaterialIteml.Specification + "'," + Convert.ToInt32(objMaterialIteml.RStyle).ToString() + ", " + objMaterialIteml.ResourceType.ToString() + ",") + " '" + Convert.ToInt32(objMaterialIteml.IsValid).ToString() + "','000000',getdate()) ") + " insert into  EPM_Res_Gauge(VersionCode,ResourceCode,UnitID,Quotiety,IsDefault,IsValid,Owner,VersionTime) ") + " values( '" + objMaterialIteml.VersionCode.ToString() + "',") + " '" + objMaterialIteml.ResourceCode + "', ") + " '" + unitid + "', ") + " 1, ") + " '1'," + " '1', ") + " '000000',getdate()) " + " insert into EPM_Res_PriceRelations (VersionCode,ResourceCode,PriceItemID,Price,Owner,VersionTime) ";
            string str4 = string.Concat(new object[] { obj2, " select '", objMaterialIteml.VersionCode.ToString(), "','", objMaterialIteml.ResourceCode, "',PriceItemID,'", price, "','000000',getdate() from EPM_Res_PriceItem " });
            string str5 = (str4 + " insert into EPM_Stuff_Stock(StockCode,ResourceCode,CategoryCode,Quantity,Price,ModifyDate) values('" + stockCode.ToString() + "','" + objMaterialIteml.ResourceCode + "','" + objMaterialIteml.CategoryCode + "',0," + price.ToString() + ",'" + DateTime.Today.ToShortDateString() + "') ") + " insert into  EPM_Stuff_StockCheck ";
            return publicDbOpClass.ExecSqlString((str5 + " select '" + pc + "',StockCode,ResourceCode,CategoryCode,Quantity,Price,ModifyDate from EPM_Stuff_Stock where StockCode='" + stockCode.ToString() + "' and ResourceCode='" + objMaterialIteml.ResourceCode + "' and CategoryCode='" + objMaterialIteml.CategoryCode + "'") + " end ");
        }

        public int ClearTempResource(Guid operateSession, string userCode)
        {
            string format = "";
            format = "delete from EPM_Res_TempResource where SessionCode = '{0}' and userCode = '{1}'";
            return publicDbOpClass.ExecSqlString(string.Format(format, operateSession, userCode));
        }

        private string ContactResourceCode(ArrayList resourceCodeList)
        {
            string str = "";
            int count = resourceCodeList.Count;
            for (int i = 0; i < count; i++)
            {
                if (resourceCodeList[i].ToString().Trim() != "")
                {
                    if (i == (count - 1))
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, "'", resourceCodeList[i], "'" });
                    }
                    else
                    {
                        object obj3 = str;
                        str = string.Concat(new object[] { obj3, "'", resourceCodeList[i], "'," });
                    }
                }
            }
            return str;
        }

        private ResourceItem DataRowToMaterialItemInfo(DataRow dr)
        {
            return new ResourceItem { CategoryCode = dr["CategoryCode"].ToString(), ResourceCode = dr["ResourceCode"].ToString(), ResourceName = dr["ResourceName"].ToString(), Specification = dr["Specification"].ToString(), ResourceType = (int) dr["ResourceType"], IsValid = (bool) dr["IsValid"] };
        }

        private ResourceUnit DataRowToResourceGauge(DataRow dr)
        {
            return new ResourceUnit { ResourceCode = dr["ResourceCode"].ToString(), UnitID = (int) dr["UnitID"], UnitName = dr["UnitName"].ToString(), Quotiety = (decimal) dr["Quotiety"], IsDefault = (bool) dr["IsDefault"], IsValid = (bool) dr["IsValid"] };
        }

        private ResourcePrice DataRowToResourcePriceRelation(DataRow dr)
        {
            return new ResourcePrice { Price = (decimal) dr["Price"], PriceItemName = dr["PriceItemName"].ToString(), PriceItemID = (int) dr["PriceItemID"], ResourceCode = dr["ResourceCode"].ToString() };
        }

        public int DeleteResourceItem(string strResourceCode)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(((str + "delete EPM_Res_Resource where ResourceCode='" + strResourceCode + "'") + " delete EPM_Res_Gauge where ResourceCode='" + strResourceCode + "'") + " delete EPM_Res_PriceRelations where ResourceCode='" + strResourceCode + "'");
        }

        public int DeleteResourceItem(string strResourceCode, bool bl)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(((str + "delete EPM_Res_Resource where ResourceCode='" + strResourceCode + "'") + " delete EPM_Res_Gauge where ResourceCode='" + strResourceCode + "'") + " delete EPM_Res_PriceRelations where ResourceCode='" + strResourceCode + "'");
        }

        public int DelSelectedResource(Guid operateSession, string userCode, ArrayList resourceCodeList)
        {
            string str = this.ContactResourceCode(resourceCodeList);
            if (str == "")
            {
                return 1;
            }
            string str2 = "";
            return publicDbOpClass.ExecSqlString(string.Format((str2 + " begin " + " update EPM_Res_TempResource set State=0 where (SessionCode='{0}')and(UserCode='{1}')and(ResourceCode in ({2}))and(State=1) ") + " delete from EPM_Res_TempResource where (SessionCode='{0}')and(UserCode='{1}')and(ResourceCode in ({2}))and(State=2) " + " end ", operateSession.ToString(), userCode, str));
        }

        public static string GetAutoId(string cateGory)
        {
            return publicDbOpClass.ExecuteScalar("select  '" + cateGory + "'+replicate('0', ((9-len('" + cateGory + "'))-len(cast(max(cast(right(resourcecode,(len(resourcecode)-len(categorycode))) as int))+1 as varchar(9)))))+(select cast(max(cast(right(resourcecode,(len(resourcecode)-len(categorycode))) as int))+1 as varchar(9)) from epm_res_resource where categorycode='" + cateGory + "') from epm_res_resource where categorycode='" + cateGory + "'").ToString();
        }

        public DataTable GetBindResource(string strWhere)
        {
            return publicDbOpClass.DataTableQuary(" select distinct a.ResourceCode,a.ResourceName,a.ResourceUnit,a.ResourceStyle,b.Specification from EPM_Task_Resource a left join v_Res_Resource b on a.ResourceCode=b.ResourceCode where a.ResourceCode<>'' and " + strWhere.Trim());
        }

        public DataTable GetBindTypeResource(string strWhere)
        {
            return publicDbOpClass.DataTableQuary(" select distinct ResourceCode,ResourceName,UnitName as ResourceUnit ,ResourceStyle,Specification from  v_Res_Resource a where ResourceCode<>'' and CateGoryCode like '0206%' and " + strWhere.Trim());
        }

        public Hashtable GetDistinctRegisterBind(string Field)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Format("select distinct {0} from EPM_Res_Unit", Field));
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                hashtable.Add(table.Rows[i][0].ToString(), table.Rows[i][0].ToString());
            }
            return hashtable;
        }

        private ResourceBasicInfo GetResourceBasicInfoFromDataRow(DataRow dr)
        {
            ResourceBasicInfo info = new ResourceBasicInfo {
                ResourceCode = dr["ResourceCode"].ToString(),
                ResourceName = dr["ResourceName"].ToString(),
                Specification = dr["Specification"].ToString()
            };
            if (dr["UnitID"] != DBNull.Value)
            {
                info.UnitID = (int) dr["UnitID"];
            }
            if (dr["UnitName"] != DBNull.Value)
            {
                info.UnitName = dr["UnitName"].ToString();
            }
            if (dr["PriceItemID"] != DBNull.Value)
            {
                info.PriceItemID = (int) dr["PriceItemID"];
            }
            if (dr["PriceItemName"] != DBNull.Value)
            {
                info.PriceItemName = dr["PriceItemName"].ToString();
            }
            if (dr["Price"] != DBNull.Value)
            {
                info.Price = (decimal) dr["Price"];
            }
            if (dr["CategoryCode"] != DBNull.Value)
            {
                info.CategoryCode = dr["CategoryCode"].ToString();
            }
            if (dr["CategoryName"] != DBNull.Value)
            {
                info.CategoryName = dr["CategoryName"].ToString();
            }
            return info;
        }

        public ResourceItem GetResourceItem(string strResourceCode)
        {
            ResourceItem item = new ResourceItem();
            string str = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str + "select * from EPM_Res_Resource where ResourceCode='" + strResourceCode + "' and IsValid=1 "))
            {
                if (table.Rows.Count > 0)
                {
                    item = this.DataRowToMaterialItemInfo(table.Rows[0]);
                }
            }
            return item;
        }

        public ArrayList GetResourcePriceList(string strResourceCode)
        {
            ArrayList list = new ArrayList();
            string str = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str + " select a.*,b.PriceItemName as PriceItemName from EPM_Res_PriceRelations a join EPM_Res_PriceItem b on a.PriceItemID=b.PriceItemID and b.IsValid=1 where a.ResourceCode='" + strResourceCode + "' "))
            {
                if (table.Rows.Count <= 0)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.DataRowToResourcePriceRelation(table.Rows[i]));
                }
            }
            return list;
        }

        public ArrayList GetResourceUnitList(string strResourceCode)
        {
            ArrayList list = new ArrayList();
            string str = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str + " select a.*,b.UnitName from EPM_Res_Gauge a join EPM_Res_Unit b on a.UnitID=b.UnitID where a.ResourceCode='" + strResourceCode + "' and a.IsValid=1 "))
            {
                if (table.Rows.Count <= 0)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.DataRowToResourceGauge(table.Rows[i]));
                }
            }
            return list;
        }

        public decimal GetResourceUnitPrice(string strResourceCode)
        {
            string str = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str + " select isnull(Price,0) as Price from EPM_Res_PriceRelations a join EPM_Res_PriceItem b on a.PriceItemID=b.PriceItemID and b.IsDefault=1 where a.ResourceCode='" + strResourceCode + "' "))
            {
                if (table.Rows.Count > 0)
                {
                    return ((table.Rows[0]["Price"].ToString() == "") ? 1M : Convert.ToDecimal(table.Rows[0]["Price"].ToString()));
                }
                return 1M;
            }
        }

        public decimal GetResourceUnitQuotiety(string strResourceCode, int intUnitID)
        {
            string str = "";
            object obj2 = str;
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " select isnull(Quotiety,0) as Quotiety from EPM_Res_Gauge where ResourceCode='", strResourceCode, "' and UnitID='", intUnitID, "' and  IsValid=1 " })))
            {
                if (table.Rows.Count > 0)
                {
                    return ((table.Rows[0]["Quotiety"].ToString() == "") ? 1M : Convert.ToDecimal(table.Rows[0]["Quotiety"].ToString()));
                }
                return 1M;
            }
        }

        public bool IsHaveResource(string strResourceCode)
        {
            bool flag = true;
            string str = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str + "select * from EPM_Res_Resource where ResourceCode='" + strResourceCode + "' "))
            {
                if (table.Rows.Count > 0)
                {
                    flag = false;
                }
            }
            return flag;
        }

        public ResourceBasicCollection QueryResourceBasicList(string categoryCode, string ResourceStyle)
        {
            ResourceBasicCollection basics = new ResourceBasicCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_V_Res_ResourceBasic where CategoryCode = '" + categoryCode + "' and ResourceStyle = '" + ResourceStyle + "'"))
            {
                if (table.Rows.Count <= 0)
                {
                    return basics;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    basics.Add(this.GetResourceBasicInfoFromDataRow(table.Rows[i]));
                }
            }
            return basics;
        }

        public ResourceBasicCollection QueryResourceBasicList(string category, string cateName, string resCode, string resName, string ResourceStyle)
        {
            ResourceBasicCollection basics = new ResourceBasicCollection();
            string str = "";
            str = "select * from EPM_V_Res_ResourceBasic where (ResourceStyle = '" + ResourceStyle + "')";
            if (category != "")
            {
                str = str + " and categoryCode like'" + category + "%'";
            }
            if (cateName != "")
            {
                str = str + " and CategoryName like'%" + cateName + "%'";
            }
            if (resCode != "")
            {
                str = str + " and ResourceCode like'%" + resCode + "%'";
            }
            if (resName != "")
            {
                str = str + " and ResourceName like'%" + resName + "%'";
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(str + "order by ResourceCode asc"))
            {
                if (table.Rows.Count <= 0)
                {
                    return basics;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    basics.Add(this.GetResourceBasicInfoFromDataRow(table.Rows[i]));
                }
            }
            return basics;
        }

        public ResourceBasicCollection QueryValidTempResourceList(Guid SessionCode)
        {
            ResourceBasicCollection basics = new ResourceBasicCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Res_TempResource where (SessionCode = '" + SessionCode + "') and State = 2"))
            {
                if (table.Rows.Count <= 0)
                {
                    return basics;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    basics.Add(this.GetResourceBasicInfoFromDataRow(table.Rows[i]));
                }
            }
            return basics;
        }

        public ResourceBasicCollection QueryValidTempResourceList(Guid operateSession, string userCode)
        {
            ResourceBasicCollection basics = new ResourceBasicCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from EPM_Res_TempResource where (SessionCode = '", operateSession, "') and (userCode = '", userCode, "') and (State>=1)" })))
            {
                if (table.Rows.Count <= 0)
                {
                    return basics;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    basics.Add(this.GetResourceBasicInfoFromDataRow(table.Rows[i]));
                }
            }
            return basics;
        }

        public int selectUnitID(string UnitName)
        {
            int num = 0;
            DataTable table = publicDbOpClass.DataTableQuary((((((((("begin " + " if (select top 1 UnitName from EPM_Res_Unit where UnitName = '" + UnitName + "') <> ''") + "  begin") + " select UnitID from EPM_Res_Unit where UnitName = '" + UnitName + "' ") + " end " + " else ") + " begin " + " declare @Ui int ") + " select @Ui = Max(UnitID) +1 from EPM_Res_Unit " + " insert into EPM_Res_Unit (UnitID,UnitName,IsValid,Owner,VersionTime) ") + " values (@Ui,'" + UnitName + "',1,'000000',getdate()) ") + " select UnitID from EPM_Res_Unit where UnitName = '" + UnitName + "' ") + " end " + " end");
            if (table.Rows.Count > 0)
            {
                num = Convert.ToInt32(table.Rows[0]["UnitID"]);
            }
            return num;
        }

        public int UndoModifyTempResource(Guid operateSession, string userCode)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(string.Format((str + " begin " + " update EPM_Res_TempResource set state = 1 where (SessionCode='{0}') and (UserCode='{1}') and (State=0) ") + " delete from EPM_Res_TempResource where (SessionCode = '{0}') and (userCode = '{1}') and (State=2) " + " end ", operateSession.ToString(), userCode));
        }

        public int UpdResourceItem(ResourceItem objMaterialIteml, ArrayList objArrUnit, ArrayList objArrPrice)
        {
            string sqlString = "";
            sqlString = ((((((sqlString + " update EPM_Res_Resource " + " set ") + " ResourceName='" + objMaterialIteml.ResourceName + "', ") + " ResourceType=" + objMaterialIteml.ResourceType.ToString() + ",") + " Specification='" + objMaterialIteml.Specification + "', ") + " VersionTime = getdate() " + " where ") + " ResourceCode='" + objMaterialIteml.ResourceCode + "' ") + " delete from EPM_Res_Gauge where ResourceCode='" + ((ResourceUnit) objArrUnit[0]).ResourceCode + "' ";
            if (objArrUnit.Count > 0)
            {
                for (int i = 0; i < objArrUnit.Count; i++)
                {
                    object obj2 = ((sqlString + " insert into  EPM_Res_Gauge(VersionCode,ResourceCode,UnitID,Quotiety,IsDefault,IsValid,Owner,VersionTime) ") + " values( '" + objMaterialIteml.VersionCode.ToString() + "',") + " '" + ((ResourceUnit) objArrUnit[i]).ResourceCode + "', ";
                    object obj3 = string.Concat(new object[] { obj2, " '", ((ResourceUnit) objArrUnit[i]).UnitID, "', " });
                    sqlString = ((string.Concat(new object[] { obj3, " '", ((ResourceUnit) objArrUnit[i]).Quotiety, "', " }) + " '" + Convert.ToInt32(((ResourceUnit) objArrUnit[i]).IsDefault).ToString() + "',") + " '" + Convert.ToInt32(((ResourceUnit) objArrUnit[i]).IsValid).ToString() + "', ") + " '000000',getdate()) ";
                }
            }
            if (objArrPrice.Count > 0)
            {
                for (int j = 0; j < objArrPrice.Count; j++)
                {
                    object obj4 = sqlString + " update  EPM_Res_PriceRelations set ";
                    object obj5 = (string.Concat(new object[] { obj4, " Price='", ((ResourcePrice) objArrPrice[j]).Price, "', " }) + " VersionTime = getdate() ") + " where ResourceCode='" + ((ResourcePrice) objArrPrice[j]).ResourceCode + "' and ";
                    sqlString = string.Concat(new object[] { obj5, " PriceItemID='", ((ResourcePrice) objArrPrice[j]).PriceItemID, "' " });
                }
            }
            return publicDbOpClass.ExecSqlString(sqlString);
        }

        public int UpdResourceItem(ResourceItem objMaterialIteml, decimal price, string unitid)
        {
            string str2 = ((((("" + " update EPM_Res_Resource " + " set ") + " ResourceName='" + objMaterialIteml.ResourceName + "', ") + " ResourceType=" + objMaterialIteml.ResourceType.ToString() + ",") + " Specification='" + objMaterialIteml.Specification + "', ") + " VersionTime = getdate() " + " where ") + " ResourceCode='" + objMaterialIteml.ResourceCode + "' ";
            object obj2 = str2 + " update EPM_Res_Gauge set UnitID='" + unitid + "' where ResourceCode='" + objMaterialIteml.ResourceCode + "' ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " update EPM_Res_PriceRelations set Price='", price, "' where ResourceCode='", objMaterialIteml.ResourceCode, "' " }));
        }
    }
}

