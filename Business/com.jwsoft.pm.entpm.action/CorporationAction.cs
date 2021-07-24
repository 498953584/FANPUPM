namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class CorporationAction
    {
        public static ArrayList GetContractCorpList(string typeCode)
        {
            ArrayList list = new ArrayList();
            using (DataTable table = publicDbOpClass.DataTableQuary("select CorpID,CorpName,Corporation from XPM_Basic_ContactCorp where CorpTypeID = '" + typeCode + "' and IsValid=1 "))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(GetCorporationFromDataRow(table.Rows[i]));
                }
            }
            return list;
        }

        private static CorporationInfo GetCorporationFromDataRow(DataRow dr)
        {
            return new CorporationInfo { CorpID = (int) dr["CorpID"], CorporationName = dr["CorpName"].ToString(), Corporation = dr["Corporation"].ToString() };
        }
    }
}

