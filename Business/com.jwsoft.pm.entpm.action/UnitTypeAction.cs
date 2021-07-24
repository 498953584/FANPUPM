namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class UnitTypeAction
    {
        private const string DB_TABLE_NAME = "EPM_Res_Unit";

        public int ActiveUnitName(string UnitName)
        {
            return publicDbOpClass.ExecSqlString("update EPM_Res_Unit set IsValid = 1 where UnitName='" + UnitName + "'");
        }

        public int AddUnitName(string UnitName)
        {
            return publicDbOpClass.ExecSqlString("insert into EPM_Res_Unit(UnitName,IsValid,Owner,VersionTime) values('" + UnitName + "',1,'000000',getdate())");
        }

        public int AddUnitType(UnitType ut)
        {
            string format = "";
            format = "insert into EPM_Res_Unit(UnitID,UnitName,IsValid,Owner,VersionTime) values({0},'{1}',1,'000000',getdate())";
            return publicDbOpClass.ExecSqlString(string.Format(format, this.GetNextID(), ut.UnitName));
        }

        public int DelUnitType(int unitID)
        {
            string format = "";
            format = " update EPM_Res_Unit set IsValid = 0,VersionTime=getdate() where UnitID = {0}";
            return publicDbOpClass.ExecSqlString(string.Format(format, unitID));
        }

        private int GetNextID()
        {
            return BasicHelperAction.GetNextID("EPM_Res_Unit");
        }

        private UnitType GetUnitTypeFromDataRow(DataRow dr)
        {
            return new UnitType { UnitID = (int) dr["UnitID"], UnitName = dr["UnitName"].ToString(), IsValid = (bool) dr["IsValid"] };
        }

        public bool IsInUse(int unitID)
        {
            return (Convert.ToInt32(publicDbOpClass.DataTableQuary("select count(1) from EPM_Res_Gauge where UnitID = '" + unitID + "' and IsValid = 1").Rows[0][0].ToString()) > 0);
        }

        public UnitType QuerySingleUnitType(int unitID)
        {
            string format = "";
            format = "select UnitID,UnitName,IsValid from EPM_Res_Unit where UnitID = {0}";
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Format(format, unitID)))
            {
                if (table.Rows.Count == 1)
                {
                    return this.GetUnitTypeFromDataRow(table.Rows[0]);
                }
            }
            return new UnitType();
        }

        public UnitTypeCollection QueryUnitTypeList(ValidState vs)
        {
            UnitTypeCollection types = new UnitTypeCollection();
            string sqlString = "";
            sqlString = "select distinct UnitID,UnitName,IsValid from EPM_Res_Unit ";
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
                    return types;
                }
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    types.Add(this.GetUnitTypeFromDataRow(table.Rows[i]));
                }
            }
            return types;
        }

        public string UNITisEXITS(string unitname)
        {
            return publicDbOpClass.DataTableQuary("select count(UnitName) from EPM_Res_Unit where UnitName='" + unitname + "'").Rows[0][0].ToString();
        }

        public int UpdUnitType(UnitType ut)
        {
            string format = "";
            format = " update EPM_Res_Unit set UnitName = '{0}',IsValid = {1},VersionTime=getdate() where UnitID = {2}";
            return publicDbOpClass.ExecSqlString(string.Format(format, ut.UnitName, ut.IsValid ? 1 : 0, ut.UnitID));
        }
    }
}

