namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ResourceCategoryAction
    {
        public int AddResourceAssort(ResourceCategory objResource)
        {
            string str = "";
            string str2 = str + " begin ";
            string str3 = str2 + " if exists(select 1 from EPM_Res_Category where VersionCode = '" + objResource.VersionCode.ToString() + "' and CategoryCode='" + objResource.CategoryCode + "' and IsValid = 0) ";
            string str4 = str3 + "update EPM_Res_Category set CategoryParentCode='" + objResource.CategoryParentCode + "',CategoryName = '" + objResource.CategoryName + "',";
            object obj2 = (((((((str4 + "ResourceType=" + objResource.ResourceType.ToString() + ",ChildNumber=0,IsValid = 1,VersionTime = getdate() where (CategoryCode='" + objResource.CategoryCode + "') and (VersionCode = '" + objResource.VersionCode.ToString() + "') and (IsValid = 0)") + " else ") + " insert into EPM_Res_Category(VersionCode,CategoryCode,CategoryParentCode,CategoryName,ResourceStyle,ResourceType,ChildNumber,IsValid,Owner,VersionTime) " + " values( ") + " '" + objResource.VersionCode.ToString() + "', ") + " '" + objResource.CategoryCode + "', ") + " '" + objResource.CategoryParentCode + "', ") + " '" + objResource.CategoryName + "', ") + " " + Convert.ToInt32(objResource.ResourceStyle).ToString() + ", ";
            string str5 = (string.Concat(new object[] { obj2, " '", objResource.ResourceType, "', " }) + " 0,") + " '" + Convert.ToInt32(objResource.IsValid).ToString() + "','000000',getdate()) ";
            return publicDbOpClass.ExecSqlString((str5 + " update EPM_Res_Category set ChildNumber = (select count(1) from EPM_Res_Category where (VersionCode ='" + objResource.VersionCode.ToString() + "') and (CategoryParentCode = '" + objResource.CategoryParentCode + "') and (IsValid =1)),VersionTime = getdate() where (VersionCode='" + objResource.VersionCode.ToString() + "') and (CategoryCode = '" + objResource.CategoryParentCode + "') ") + " end ");
        }

        public ResourceStyle BelongToResourceStyle(Guid versionCode, string categoryCode)
        {
            int num = 0;
            num = (int) publicDbOpClass.ExecuteScalar("select ResourceStyle from EPM_Res_Category where (VersionCode='" + versionCode.ToString() + "')and(CategoryCode = '" + categoryCode + "')");
            return (ResourceStyle) Enum.Parse(typeof(ResourceStyle), num.ToString());
        }

        private ResourceCategory DataRowToResourceCategoryInfo(DataRow dr)
        {
            return new ResourceCategory { VersionCode = (Guid) dr["VersionCode"], CategoryParentCode = dr["CategoryParentCode"].ToString(), CategoryCode = dr["CategoryCode"].ToString(), CategoryName = dr["CategoryName"].ToString(), ResourceStyle = (ResourceStyle) Enum.Parse(typeof(ResourceStyle), dr["ResourceStyle"].ToString()), ResourceType = (int) dr["ResourceType"], ChildNumber = (int) dr["ChildNumber"], IsValid = (bool) dr["IsValid"] };
        }

        public int DeleteResourceAssort(Guid versionCode, string strCategoryCode)
        {
            ResourceCategory singleResourceCategory = new ResourceCategory();
            singleResourceCategory = this.GetSingleResourceCategory(versionCode, strCategoryCode);
            string str = "";
            string str2 = str + " begin ";
            string str3 = str2 + " update EPM_Res_Category set IsValid=0,VersionTime=getdate() where (VersionCode='" + versionCode.ToString() + "')and(CategoryCode='" + strCategoryCode + "') ";
            return publicDbOpClass.ExecSqlString((str3 + " update EPM_Res_Category set ChildNumber = (select count(1) from EPM_Res_Category where (VersionCode='" + versionCode.ToString() + "')and(CategoryParentCode = '" + singleResourceCategory.CategoryParentCode + "')and(IsValid =1)),VersionTime = getdate() where (VersionCode='" + versionCode.ToString() + "')and(CategoryCode = '" + singleResourceCategory.CategoryParentCode + "')") + " end ");
        }

        public DataTable GetResource(Guid versionCode, string strCategoryCode)
        {
            string str2 = "";
            return publicDbOpClass.DataTableQuary(str2 + "select * from EPM_V_Res_ResourceBasic where (VersionCode ='" + versionCode.ToString() + "')and(CategoryCode='" + strCategoryCode + "') order by dbo.EPM_Res_Resource.ResourceCode");
        }

        public DataTable GetResource(Guid versionCode, string strCategoryCode, int ResourceStyle)
        {
            string str2 = "";
            object obj2 = str2 + "select * from EPM_V_Res_ResourceBasic where (VersionCode ='" + versionCode.ToString() + "')and(CategoryCode='" + strCategoryCode + "') ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " and ResourceStyle = ", ResourceStyle, " order by ResourceCode" }));
        }

        public DataTable GetResourceAssort()
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "select * from EPM_Res_Category where IsValid=1");
        }

        public DataTable GetResourceAssort(Guid versionCode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "select * from EPM_Res_Category where VersionCode='" + versionCode.ToString() + "' and IsValid=1");
        }

        public DataTable GetResourceAssort(Guid versionCode, ResourceStyle resStyle)
        {
            string str2 = "";
            return publicDbOpClass.DataTableQuary(str2 + "select * from EPM_Res_Category where (VersionCode='" + versionCode.ToString() + "')and(ResourceStyle=" + Convert.ToInt32(resStyle).ToString() + ")and(IsValid=1)");
        }

        public ResourceCategory GetSingleResourceCategory(Guid versionCode, string strCategoryCode)
        {
            ResourceCategory category = new ResourceCategory();
            string str2 = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str2 + "select * from EPM_Res_Category where (VersionCode='" + versionCode.ToString() + "')and(CategoryCode='" + strCategoryCode + "')and(IsValid=1)"))
            {
                if (table.Rows.Count > 0)
                {
                    category = this.DataRowToResourceCategoryInfo(table.Rows[0]);
                }
            }
            return category;
        }

        public bool IsHaveCategoryCode(Guid versionCode, string strCategoryCode)
        {
            bool flag = true;
            string str2 = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(str2 + " select * from EPM_Res_Category where (VersionCode='" + versionCode.ToString() + "')and(CategoryCode='" + strCategoryCode + "')and(IsValid = 1)"))
            {
                if (table.Rows.Count > 0)
                {
                    flag = false;
                }
            }
            return flag;
        }

        public bool IsHaveResources(Guid versionCode, string categoryCode)
        {
            return (((int) publicDbOpClass.ExecuteScalar("select count(1) from EPM_Res_Resource where (VersionCode='" + versionCode.ToString() + "')and(CategoryCode = '" + categoryCode + "')")) > 0);
        }

        public ResourceCategory QueryFirstCategory(Guid versionCode, ResourceStyle rStyle)
        {
            ResourceCategory category = new ResourceCategory();
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from EPM_Res_Category where (VersionCode='", versionCode.ToString(), "')and(ResourceStyle=", Convert.ToInt32(rStyle), ")and(CategoryParentCode = '')" })))
            {
                if (table.Rows.Count == 1)
                {
                    category = this.DataRowToResourceCategoryInfo(table.Rows[0]);
                }
            }
            return category;
        }

        public ResourceCategoryCollection QueryOtherFirstCategoryList(Guid versionCode)
        {
            ResourceCategoryCollection categorys = new ResourceCategoryCollection();
            new ResourceCategory();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Res_Category where (VersionCode='" + versionCode.ToString() + "')and(ResourceStyle not in (1,2,3))and(CategoryParentCode = '')"))
            {
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    categorys.Add(this.DataRowToResourceCategoryInfo(table.Rows[i]));
                }
            }
            return categorys;
        }

        public ResourceCategoryCollection QuerySecondLevelCategoryList(Guid versionCode, ResourceStyle resStyle)
        {
            ResourceCategoryCollection categorys = new ResourceCategoryCollection();
            string str = "";
            str = "select VersionCode,CategoryCode, CategoryParentCode,CategoryName, ResourceStyle, ResourceType,ChildNumber,IsValid ";
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Format(str + " from EPM_Res_Category where VersionCode='{0}' and ResourceStyle = {1} and CategoryParentCode in " + "(select CategoryCode from EPM_Res_Category where (VersionCode='{0}') and (CategoryParentCode = '') and (ResourceStyle = {1}) and (IsValid = 1)) and IsValid = 1", versionCode.ToString(), Convert.ToInt32(resStyle))))
            {
                if (table.Rows.Count <= 0)
                {
                    return categorys;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    categorys.Add(this.DataRowToResourceCategoryInfo(table.Rows[i]));
                }
            }
            return categorys;
        }

        public ResourceCategoryCollection QuerySubCategoryList(Guid versionCode, string parentCategoryCode)
        {
            ResourceCategoryCollection categorys = new ResourceCategoryCollection();
            string str = "";
            str = "select VersionCode,CategoryCode, CategoryParentCode,CategoryName, ResourceStyle, ResourceType,ChildNumber,IsValid ";
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Format(str + " from EPM_Res_Category where (VersionCode ='{0}') and (CategoryParentCode = '{1}') and (IsValid = 1)", versionCode.ToString(), parentCategoryCode)))
            {
                if (table.Rows.Count <= 0)
                {
                    return categorys;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    categorys.Add(this.DataRowToResourceCategoryInfo(table.Rows[i]));
                }
            }
            return categorys;
        }

        public DataTable QuerySubResList(Guid versionCode, string parentCategoryCode)
        {
            string sqlString = "";
            sqlString = "SELECT a.VersionCode, a.CategoryCode, a.CategoryName, a.IsValid, b.ResourceName,  b.ResourceCode,b.Specification ";
            sqlString = string.Format(sqlString + " FROM dbo.EPM_Res_Category a RIGHT OUTER JOIN       dbo.EPM_Res_Resource b ON a.VersionCode = b.VersionCode AND       a.CategoryCode = b.CategoryCode " + " where (a.VersionCode ='{0}') and (a.CategoryCode = '{1}') and (a.IsValid = 1)", versionCode.ToString(), parentCategoryCode);
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int UpdResourceAssort(ResourceCategory objResource, string oldCategoryCode)
        {
            object obj2 = (("" + " begin " + " update EPM_Res_Category set ") + " CategoryCode='" + objResource.CategoryCode + "',") + " CategoryName='" + objResource.CategoryName + "', ";
            string str2 = (string.Concat(new object[] { obj2, " ResourceType='", objResource.ResourceType, "', " }) + " IsValid='" + Convert.ToInt32(objResource.IsValid).ToString() + "',") + " VersionTime = getdate() ";
            string str = str2 + " where (VersionCode = '" + objResource.VersionCode.ToString() + "') and (CategoryCode='" + oldCategoryCode + "') ";
            if (!objResource.IsValid)
            {
                string str3 = str;
                str = str3 + " update EPM_Res_Category set ChildNum = (select count(1) from EPM_Res_Category where (VersioinCode ='" + objResource.VersionCode.ToString() + "') and (CategoryParentCode = '" + objResource.CategoryParentCode + "') and (IsValid =1)),VersionTime = getdate() where (VersionCode='" + objResource.VersionCode.ToString() + "') CategoryCode = '" + objResource.CategoryParentCode + "' ";
            }
            return publicDbOpClass.ExecSqlString(str + " end ");
        }
    }
}

