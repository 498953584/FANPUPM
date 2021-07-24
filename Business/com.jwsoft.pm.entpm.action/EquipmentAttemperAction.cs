namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class EquipmentAttemperAction
    {
        public int AddAttemper(EquipmentAttemperInfo ea)
        {
            string str2 = (" BEGIN " + " INSERT INTO Ent_Ept_Attemper (AttemperBillCode,EquipmentUniqueCode,") + " ToProjectUniqueCode,Suite,IntendingTime,UnitPrice,BeginDate," + " EndDate,Remark)";
            object obj2 = (str2 + " VALUES ('" + ea.AttemperBillCode + "','" + ea.EquipmentUniqueCode.ToString() + "',") + " '" + ea.ToProjectUniqueCode.ToString() + "',";
            object obj3 = string.Concat(new object[] { obj2, " '", ea.Suite, "','", ea.IntendingTime, "',", ea.UnitPrice, ",'", ea.BeginDate, "'," });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " '", ea.EndDate, "','", ea.Remark, "')" }) + " END ");
        }

        public static int DelEqiupmentAttemperInfo(int ID, string equipmentCode)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "delete from Ent_Ept_Attemper where NoteSequenceID = '", ID, "'" }));
        }

        public int EDITAttemper(EquipmentAttemperInfo ea)
        {
            object obj2 = ((" BEGIN " + " UPDATE Ent_Ept_Attemper set AttemperBillCode = '" + ea.AttemperBillCode + "',") + " EquipmentUniqueCode = '" + ea.EquipmentUniqueCode.ToString() + "',") + " ToProjectUniqueCode = '" + ea.ToProjectUniqueCode.ToString() + "',";
            object obj3 = string.Concat(new object[] { obj2, " Suite = '", ea.Suite, "', IntendingTime = '", ea.IntendingTime, "'," });
            object obj4 = string.Concat(new object[] { obj3, " UnitPrice = ", ea.UnitPrice, "," });
            object obj5 = string.Concat(new object[] { obj4, " BeginDate = '", ea.BeginDate, "',EndDate = '", ea.EndDate, "'," }) + " Remark = '" + ea.Remark + "' ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj5, " WHERE NoteSequenceID = '", ea.NoteSequenceID, "'" }) + " END");
        }

        public static int GetAttemperCount(DateTime dtBegin, DateTime dtEnd)
        {
            return (int) publicDbOpClass.ExecuteScalar(" select count(1) from v_Ept_EquipmentAttemper where not ((enddate < '" + dtBegin.ToShortDateString() + "')or(begindate>'" + dtEnd.ToShortDateString() + "'))");
        }

        public static EquipmentAttemperCollection GetAttemperList(string equipmentCode)
        {
            EquipmentAttemperCollection attempers = new EquipmentAttemperCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Attemper where EquipmentUniqueCode = '" + equipmentCode + "' order by BeginDate desc"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    attempers.Add(GetEquipmentAttemperInfoFromDataRow(table.Rows[i]));
                }
            }
            return attempers;
        }

        public static EquipmentAttemperInfo GetEquipmentAttemperInfo(int ID)
        {
            EquipmentAttemperInfo equipmentAttemperInfoFromDataRow = new EquipmentAttemperInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Attemper where NoteSequenceID = '" + ID + "'"))
            {
                if (table.Rows.Count > 0)
                {
                    equipmentAttemperInfoFromDataRow = GetEquipmentAttemperInfoFromDataRow(table.Rows[0]);
                }
            }
            return equipmentAttemperInfoFromDataRow;
        }

        private static EquipmentAttemperInfo GetEquipmentAttemperInfoFromDataRow(DataRow dr)
        {
            return new EquipmentAttemperInfo { NoteSequenceID = (int) dr["NoteSequenceID"], AttemperBillCode = dr["AttemperBillCode"].ToString(), EquipmentUniqueCode = new Guid(dr["EquipmentUniqueCode"].ToString()), ToProjectUniqueCode = new Guid(dr["ToProjectUniqueCode"].ToString()), Suite = dr["Suite"].ToString(), IntendingTime = (int) dr["IntendingTime"], UnitPrice = Convert.ToDecimal(dr["UnitPrice"]), BeginDate = (DateTime) dr["BeginDate"], EndDate = (DateTime) dr["EndDate"], Remark = dr["Remark"].ToString() };
        }

        public static DataTable GetEquipmentAttemperList(DateTime dtBegin, DateTime dtEnd)
        {
            return publicDbOpClass.GetPageData("  not ((enddate < '" + dtBegin.ToShortDateString() + "')or(begindate>'" + dtEnd.ToShortDateString() + "'))", "v_Ept_EquipmentAttemper", "NoteSequenceID desc");
        }

        public static string QuerySubProject(Guid guidPrjCode)
        {
            string str = string.Empty;
            object obj2 = string.Empty;
            string sqlString = string.Concat(new object[] { obj2, "select PrjName from pt_PrjInfo where Prjguid='", guidPrjCode, "' " });
            try
            {
                using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
                {
                    if (table.Rows.Count > 0)
                    {
                        str = table.Rows[0]["PrjName"].ToString();
                    }
                }
            }
            catch
            {
            }
            return str;
        }
    }
}

