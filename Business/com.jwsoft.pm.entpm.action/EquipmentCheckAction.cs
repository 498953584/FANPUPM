namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class EquipmentCheckAction
    {
        public static int AddEquipmentCheck(EquipmentCheckInfo ec)
        {
            string str = "";
            str = " BEGIN ";
            object obj2 = str + " INSERT INTO Ent_Ept_Checkinfo (EquipmentUniqueCode,InOrOut,CheckBillCode,Result,CheckDate, " + " CheckDept,CheckPerson,ResultDescript,Remark,IsLast) ";
            object obj3 = string.Concat(new object[] { obj2, " VALUES ('", ec.EquipmentUniqueCode.ToString(), "','", ec.InOrOut, "','", ec.CheckBillCode, "','", ec.Result, "', " });
            return publicDbOpClass.ExecSqlString(((((string.Concat(new object[] { obj3, " '", ec.CheckDate, "','", ec.CheckDept, "','", ec.CheckPerson, "','", ec.ResultDescript, "','", ec.Remark, "','", ec.IsLast, "') " }) + " update Ent_Ept_CheckInfo set IsLast = 0 where (EquipmentUniqueCode = '" + ec.EquipmentUniqueCode.ToString() + "') ") + " UPDATE Ent_Ept_Checkinfo set IsLast = 1 ") + " where (NoteSequenceId = (select max(NoteSequenceId) from Ent_Ept_CheckInfo where (EquipmentUniqueCode = '" + ec.EquipmentUniqueCode.ToString() + "')and(CheckDate = ") + " (select max(CheckDate) from Ent_Ept_CheckInfo where EquipmentUniqueCode = '" + ec.EquipmentUniqueCode.ToString() + "'))))  ") + " END ");
        }

        public static int DelEqiupmentCheckInfo(int noteSequenceID)
        {
            EquipmentCheckInfo singleEquipmentCheckInfo = new EquipmentCheckInfo();
            singleEquipmentCheckInfo = GetSingleEquipmentCheckInfo(noteSequenceID);
            string str = "";
            str = " begin ";
            str = str + " delete from Ent_Ept_Checkinfo where NoteSequenceID = " + noteSequenceID;
            if (singleEquipmentCheckInfo.IsLast == 1)
            {
                str = ((str + " UPDATE Ent_Ept_Checkinfo set IsLast = 1 ") + " where (NoteSequenceId = (select max(NoteSequenceId) from Ent_Ept_CheckInfo where (EquipmentUniqueCode = '" + singleEquipmentCheckInfo.EquipmentUniqueCode.ToString() + "')and(CheckDate = ") + " (select max(CheckDate) from Ent_Ept_CheckInfo where EquipmentUniqueCode = '" + singleEquipmentCheckInfo.EquipmentUniqueCode.ToString() + "'))))  ";
            }
            return publicDbOpClass.ExecSqlString(str + " end ");
        }

        public static int EditEquipmentCheck(EquipmentCheckInfo ec)
        {
            object obj2 = (" BEGIN " + " UPDATE Ent_Ept_Checkinfo ") + " SET CheckDept = '" + ec.CheckDept + "',";
            object obj3 = string.Concat(new object[] { obj2, " InOrOut = '", ec.InOrOut, "',CheckBillCode = '", ec.CheckBillCode, "'," });
            string str2 = string.Concat(new object[] { obj3, " Result = '", ec.Result, "',CheckDate = '", ec.CheckDate, "'," });
            object obj4 = (str2 + " CheckPerson = '" + ec.CheckPerson + "',ResultDescript = '" + ec.ResultDescript + "',") + " Remark = '" + ec.Remark + "' ";
            return publicDbOpClass.ExecSqlString(((((string.Concat(new object[] { obj4, " WHERE NoteSequenceID = '", ec.NoteSequenceID, "' " }) + " update Ent_Ept_CheckInfo set IsLast = 0 where (EquipmentUniqueCode = '" + ec.EquipmentUniqueCode.ToString() + "') ") + " UPDATE Ent_Ept_Checkinfo set IsLast = 1 ") + " where (NoteSequenceId = (select max(NoteSequenceId) from Ent_Ept_CheckInfo where (EquipmentUniqueCode = '" + ec.EquipmentUniqueCode.ToString() + "')and(CheckDate = ") + " (select max(CheckDate) from Ent_Ept_CheckInfo where EquipmentUniqueCode = '" + ec.EquipmentUniqueCode.ToString() + "'))))  ") + " END");
        }

        public static int GetCheckCount(DateTime dtBegin, DateTime dtEnd, int iType)
        {
            string str = "";
            string str2 = "";
            str = " select count(1) from v_Ept_EquipmentCheckList where (1=1) ";
            if (iType == 2)
            {
                str2 = str2 + " ";
            }
            else
            {
                object obj2 = str2;
                str2 = string.Concat(new object[] { obj2, " and InOrOut = ", iType, " " });
            }
            string str3 = str2;
            str2 = str3 + " and (CheckDate >='" + dtBegin.ToShortDateString() + "') and (CheckDate <='" + dtEnd.ToShortDateString() + "')";
            return (int) publicDbOpClass.ExecuteScalar(str + str2);
        }

        private static EquipmentCheckInfo GetEquipmentCheckInfoFromDataRow(DataRow dr)
        {
            return new EquipmentCheckInfo { NoteSequenceID = (int) dr["NoteSequenceID"], EquipmentUniqueCode = new Guid(dr["EquipmentUniqueCode"].ToString()), InOrOut = (int) dr["InOrOut"], CheckBillCode = dr["CheckBillCode"].ToString(), Result = (int) dr["Result"], CheckDate = (DateTime) dr["CheckDate"], CheckDept = dr["CheckDept"].ToString(), CheckPerson = dr["CheckPerson"].ToString(), ResultDescript = dr["ResultDescript"].ToString(), IsLast = (int) dr["IsLast"], Remark = dr["Remark"].ToString() };
        }

        public static EquipmentCheckCollection GetEquipmentCheckList(Guid equipmentUniqueCode)
        {
            EquipmentCheckCollection checks = new EquipmentCheckCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_CheckInfo where EquipmentUniqueCode = '" + equipmentUniqueCode.ToString() + "' order by checkdate desc"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    checks.Add(GetEquipmentCheckInfoFromDataRow(table.Rows[i]));
                }
            }
            return checks;
        }

        public static DataTable GetEquipmentCheckList(DateTime dtBegin, DateTime dtEnd, int iType)
        {
            string str = "";
            str = "(1=1) ";
            if (iType == 2)
            {
                str = str + " ";
            }
            else
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, " and InOrOut = ", iType, " " });
            }
            string str2 = str;
            return publicDbOpClass.GetPageData(str2 + " and (CheckDate >='" + dtBegin.ToShortDateString() + "') and (CheckDate <='" + dtEnd.ToShortDateString() + "')", "v_Ept_EquipmentCheckList", "NoteSequenceID desc");
        }

        public static EquipmentCheckInfo GetSingleEquipmentCheckInfo(int ID)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Checkinfo where NoteSequenceID = '" + ID + "'"))
            {
                return GetEquipmentCheckInfoFromDataRow(table.Rows[0]);
            }
        }
    }
}

