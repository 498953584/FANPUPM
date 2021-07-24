namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class PriceItemAction
    {
        private const string DB_TABLE_NAME = "EPM_Res_PriceItem";

        public int AddPriceItem(PriceItem pi)
        {
            string str = "";
            str = " begin ";
            if (pi.IsDefault)
            {
                str = str + " update EPM_Res_PriceItem set IsDefault = 0,VersionTime = getdate()  where IsDefault=1";
            }
            return publicDbOpClass.ExecSqlString(string.Format(((str + " insert into EPM_Res_PriceItem(PriceItemID,PriceItemName,IsFixed,IsDefault,IsReadonly,IsValid,Owner,VersionTime) values({0},'{1}',0,{2},{3},{4},'000000',getdate())" + " insert into EPM_Res_PriceRelations (VersionCode,ResourceCode,PriceItemID,Price,Owner,VersionTime) ") + " select VersionCode,ResourceCode,{0},0,'000000',getdate() from EPM_Res_Resource " + " insert into EPM_Ration_ItemSynthPrice(ItemCode,VersionCode,PriceItemID,SynthPrice) ") + " select ItemCode,VersionCode,{0},0 from EPM_Ration_PrivateItem" + " end ", new object[] { this.GetNextID(), pi.PriceItemName, pi.IsDefault ? 1 : 0, pi.IsValid ? 1 : 0, pi.IsReadonly ? 1 : 0 }));
        }

        public int DelPriceItem(int priceItemID)
        {
            string format = "";
            format = "update EPM_Res_PriceItem set IsValid = 0,VersionTime = getdate() where PriceItemID = {0} and IsFixed = 0 and IsDefault = 0";
            return publicDbOpClass.ExecSqlString(string.Format(format, priceItemID));
        }

        private int GetNextID()
        {
            return BasicHelperAction.GetNextID("EPM_Res_PriceItem");
        }

        private PriceItem GetPriceItemFromDataRow(DataRow dr)
        {
            return new PriceItem { PriceItemID = (int) dr["PriceItemID"], PriceItemName = dr["PriceItemName"].ToString(), IsFixed = (bool) dr["IsFixed"], IsDefault = (bool) dr["IsDefault"], IsReadonly = (bool) dr["IsReadonly"], IsValid = (bool) dr["IsValid"] };
        }

        public bool IsInUse(int priceItemID)
        {
            object obj2 = new object();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PriceItemID", priceItemID) };
            obj2 = publicDbOpClass.ExecuteScalar("EPM_P_Res_IsInUsePriceItem", commandParameters);
            if (obj2 == DBNull.Value)
            {
                return false;
            }
            return (((int) obj2) > 0);
        }

        public PriceItem QueryDefaultPriceItem()
        {
            string sqlString = "";
            sqlString = "select PriceItemID,PriceItemName,IsFixed,IsDefault,IsReadonly,IsValid from EPM_Res_PriceItem where IsDefault = 1";
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                if (table.Rows.Count == 1)
                {
                    return this.GetPriceItemFromDataRow(table.Rows[0]);
                }
            }
            return new PriceItem();
        }

        public PriceItemCollection QueryPriceItemList(ValidState vs)
        {
            PriceItemCollection items = new PriceItemCollection();
            string sqlString = "";
            sqlString = "select PriceItemID,PriceItemName,IsFixed,IsDefault,IsReadonly,IsValid from EPM_Res_PriceItem ";
            switch (vs)
            {
                case ValidState.InValid:
                    sqlString = sqlString + " where IsValid = 0";
                    break;

                case ValidState.Valid:
                    sqlString = sqlString + " where IsValid =1 ";
                    break;
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                if (table.Rows.Count <= 0)
                {
                    return items;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    items.Add(this.GetPriceItemFromDataRow(table.Rows[i]));
                }
            }
            return items;
        }

        public PriceItem QuerySinglePriceItem(int priceItemID)
        {
            string format = "";
            format = "select PriceItemID,PriceItemName,IsFixed,IsDefault,IsReadonly,IsValid from EPM_Res_PriceItem where PriceItemID = {0}";
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Format(format, priceItemID)))
            {
                if (table.Rows.Count == 1)
                {
                    return this.GetPriceItemFromDataRow(table.Rows[0]);
                }
            }
            return new PriceItem();
        }

        public int UpdPriceItem(PriceItem pi)
        {
            string str = "";
            str = " begin ";
            if (pi.IsDefault)
            {
                str = str + " update EPM_Res_PriceItem set IsDefault = 0,VersionTime=getdate() where IsDefault = 1 and PriceItemID <> {3} ";
            }
            return publicDbOpClass.ExecSqlString(string.Format(str + " update EPM_Res_PriceItem set PriceItemName = '{0}',IsDefault={1},IsValid={2},VersionTime=getdate() where PriceItemID = {3} " + " end ", new object[] { pi.PriceItemName, pi.IsDefault ? 1 : 0, pi.IsValid ? 1 : 0, pi.PriceItemID }));
        }
    }
}

