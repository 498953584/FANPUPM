using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;

public class StorageInfo
{
    public bool AddStroageInfo(storageInfo storage)
    {
        string str = "";
        string str2 = str + "insert into storage_register" + "(supplier,itemname,validatedepartment,instorageid,outstoragename,outstroageid,pmid)";
        return publicDbOpClass.NonQuerySqlString(str2 + "values('" + storage.Supplier + "','" + storage.ItemName + "','" + storage.ValidateDepartment + "','" + storage.InStorageId + "','" + storage.OutStorageName + "','" + storage.OutStroageid + "','" + storage.Pmid + "')");
    }

    public bool DelStroageInfo(int id)
    {
        return publicDbOpClass.NonQuerySqlString("delete storage_register where id=" + id);
    }

    public DataTable GetProjectsName(string pimd)
    {
        return publicDbOpClass.DataTableQuary("select * from pm_projects where ProjectId=" + Convert.ToInt32(pimd));
    }

    public DataSet GetStorage(int id, string pmid)
    {
        return publicDbOpClass.DataSetQuary(string.Concat(new object[] { "select * from storage_register where id=", id, " and pmid='", pmid, "'" }).ToString());
    }

    public DataTable GetStroageList(string strWhere)
    {
        string str = "select * from storage_register ";
        return publicDbOpClass.DataTableQuary(str + strWhere);
    }

    public bool UpdateStroageInfo(storageInfo storage)
    {
        object obj2 = (((((("" + "update storage_register set ") + "supplier='" + storage.Supplier + "',") + "itemName='" + storage.ItemName + "',") + "validateDepartment='" + storage.ValidateDepartment + "',") + "inStorageId='" + storage.InStorageId + "',") + "outStorageName='" + storage.OutStorageName + "',") + "outStroageid='" + storage.OutStroageid + "'";
        return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, "where id=", storage.Id, " and pmid='", storage.Pmid, "'" }).ToString());
    }
}

