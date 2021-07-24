namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using System;
    using System.Data;

    public class ResourceSelectBllAction
    {
        public DataTable GetCategoryList(int resStyle)
        {
            return ResourceSelectAction.GetResCategoryList(resStyle);
        }

        public DataTable GetMaterialList(string strWhere)
        {
            return ResourceSelectAction.GetResMaterialList(strWhere);
        }

        public DataTable GetMaterialList(int resStyle, string categoryCode)
        {
            return ResourceSelectAction.GetResMaterialList(resStyle, categoryCode);
        }

        public DataTable GetResMaterialListByResCode(string resCode)
        {
            DataTable resMaterialListByResCode = new DataTable();
            if (resCode != "")
            {
                resMaterialListByResCode = ResourceSelectAction.GetResMaterialListByResCode(resCode);
            }
            return resMaterialListByResCode;
        }
    }
}

