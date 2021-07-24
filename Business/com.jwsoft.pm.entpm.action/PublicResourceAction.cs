namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class PublicResourceAction
    {
        public int GetResourceItemCount(string strWhere)
        {
            return publicDbOpClass.GetRecordCount("EPM_Res_PublicResource", strWhere);
        }

        public DataTable GetResourceItemData(int intPageSize, int intPageCount, string strWhere)
        {
            return publicDbOpClass.GetRecordFromPage("EPM_Res_PublicResource", "ResourceCode", intPageSize, intPageCount, 0, strWhere);
        }
    }
}

