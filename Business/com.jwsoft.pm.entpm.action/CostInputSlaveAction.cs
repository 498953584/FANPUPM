namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class CostInputSlaveAction
    {
        public static int deleteCostInputSlave(int ChildID)
        {
            return publicDbOpClass.ExecSqlString("delete EPM_CostImportChild where ChildID = '" + ChildID + "'");
        }

        public static DataTable getCostInputSlaveTable(Guid RecordID)
        {
            return publicDbOpClass.DataTableQuary("select * from EPM_V_CostImportChild where RecordID = '" + RecordID.ToString() + "'");
        }

        public static DataTable GetPageData(string strWhere)
        {
            return publicDbOpClass.DataTableQuary("select * from EPM_V_CostImportChild where " + strWhere);
        }

        public static int insertCostInputSlave(CostInputSlave objInfo)
        {
            string str = "insert into EPM_CostImportChild(RecordID,ItemName,Price,CostCode,Remark) values('";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, objInfo.RecordID, "' ,'", objInfo.ItemName, "',", objInfo.Price, ",'", objInfo.CostCode, "','", objInfo.Remark, "')" }));
        }

        public static int updateCostInputSlave(CostInputSlave objInfo)
        {
            object obj2 = string.Concat(new object[] { "update EPM_CostImportChild set ItemName = '", objInfo.ItemName, ", Price = ", objInfo.Price, ", CostCode= '", objInfo.CostCode, "', Remark= '", objInfo.Remark, "'" });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " where ChildID = '", objInfo.ChildID, "'" }));
        }
    }
}

