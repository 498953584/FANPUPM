namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class EquipmentMaintainAction
    {
        public static string AddAndDeleteRecordList(EquipmentMaintainFittingCollection templist, Guid maintainUniqueCode)
        {
            string str = "";
            object obj2 = str;
            str = string.Concat(new object[] { obj2, " delete from Ent_Ept_MaintainFittings where MaintainUniqueCode='", maintainUniqueCode, "' " });
            EquipmentMaintainFittingInfo info = new EquipmentMaintainFittingInfo();
            for (int i = 0; i < templist.Count; i++)
            {
                info = templist[i];
                if (info.FittingsName.Trim() != "")
                {
                    object obj3 = str + " insert into Ent_Ept_MaintainFittings(MaintainUniqueCode,FittingsName,Spec,Quality,Operation,Remark) ";
                    str = string.Concat(new object[] { obj3, "values('", maintainUniqueCode, "','", info.FittingsName, "','", info.Spec, "',", info.Quality.ToString(), ",'", info.Operation, "','", info.Remark, "') " });
                }
            }
            return str;
        }

        public static int AddMaintainInfo(EquipmentMaintainInfo eptinfo, EquipmentMaintainFittingCollection templist)
        {
            string str = "";
            string str2 = str + " begin " + " insert into Ent_Ept_Maintain";
            string str3 = str2 + " values('" + eptinfo.MaintainUniqueCode.ToString() + "','" + eptinfo.EquipmentUniqueCode.ToString() + "',";
            string str4 = str3 + "'" + eptinfo.MaintainType + "'," + eptinfo.MaintainCost.ToString() + ",'" + eptinfo.Fault + "',";
            string str5 = str4 + "'" + eptinfo.MaintainContent + "','" + eptinfo.MaintainceMan + "','" + eptinfo.MaintainDate.ToShortDateString() + "',";
            return publicDbOpClass.ExecSqlString(((str5 + "'" + eptinfo.RecordDate.ToShortDateString() + "','" + eptinfo.Appraisal + "','" + eptinfo.Appraiser + "',") + "'" + eptinfo.AppraisalDate.ToShortDateString() + "') ") + AddAndDeleteRecordList(templist, eptinfo.MaintainUniqueCode) + " end ");
        }

        public static int DelMaintainInfo(Guid maintainUniqueCode)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.ExecSqlString(((str + "  delete from Ent_Ept_MaintainFittings where MaintainUniqueCode='" + maintainUniqueCode.ToString() + "' ") + "  delete from Ent_Ept_Maintain where MaintainUniqueCode='" + maintainUniqueCode.ToString() + "'") + " end;");
        }

        public static EquipmentMaintainCollection GetEquipmentMaintainList(Guid equipmentUnqiueCode)
        {
            EquipmentMaintainCollection maintains = new EquipmentMaintainCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Maintain where EquipmentUniqueCode = '" + equipmentUnqiueCode.ToString() + "' order by MaintainDate desc"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    maintains.Add(GetMaintainInfoFrowDataRow(table.Rows[i]));
                }
            }
            return maintains;
        }

        private static EquipmentMaintainFittingInfo GetFittingInfoFrowDataRow(DataRow dr)
        {
            return new EquipmentMaintainFittingInfo { MaintainUniqueCode = (Guid) dr["MaintainUniqueCode"], FittingsName = dr["FittingsName"].ToString(), Spec = dr["Spec"].ToString(), Quality = (int) dr["Quality"], Operation = dr["Operation"].ToString(), Remark = dr["Remark"].ToString() };
        }

        public static EquipmentMaintainFittingCollection GetMaintainFittingsList(Guid maintainUniqueCode)
        {
            EquipmentMaintainFittingCollection fittings = new EquipmentMaintainFittingCollection();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_MaintainFittings where MaintainUniqueCode = '" + maintainUniqueCode.ToString() + "'"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    fittings.Add(GetFittingInfoFrowDataRow(table.Rows[i]));
                }
            }
            return fittings;
        }

        private static EquipmentMaintainInfo GetMaintainInfoFrowDataRow(DataRow dr)
        {
            return new EquipmentMaintainInfo { MaintainUniqueCode = (Guid) dr["MaintainUniqueCode"], EquipmentUniqueCode = (Guid) dr["EquipmentUniqueCode"], MaintainType = dr["MaintainType"].ToString(), MaintainCost = (decimal) dr["MaintainCost"], Fault = dr["Fault"].ToString(), MaintainContent = dr["MaintainContent"].ToString(), MaintainceMan = dr["MaintainceMan"].ToString(), MaintainDate = (DateTime) dr["MaintainDate"], RecordDate = (DateTime) dr["RecordDate"], Appraisal = dr["Appraisal"].ToString(), Appraiser = dr["Appraiser"].ToString(), AppraisalDate = (DateTime) dr["AppraisalDate"] };
        }

        public static EquipmentMaintainInfo GetSingleEquipemntMaintainInfo(Guid maintainUniqueCode)
        {
            EquipmentMaintainInfo maintainInfoFrowDataRow = new EquipmentMaintainInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Ept_Maintain where MaintainUniqueCode = '" + maintainUniqueCode.ToString() + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    maintainInfoFrowDataRow = GetMaintainInfoFrowDataRow(table.Rows[0]);
                }
            }
            return maintainInfoFrowDataRow;
        }

        public static int UpdMaintainInfo(EquipmentMaintainInfo eptinfo, EquipmentMaintainFittingCollection templist, Guid maintainUniqueCode)
        {
            object obj2 = ((((((((((" begin " + " update Ent_Ept_Maintain ") + " set MaintainType = '" + eptinfo.MaintainType.ToString() + "',") + " MaintainCost = " + eptinfo.MaintainCost.ToString() + ",") + " Fault = '" + eptinfo.Fault.ToString() + "',") + " MaintainContent = '" + eptinfo.MaintainContent + "',") + " MaintainceMan = '" + eptinfo.MaintainceMan + "',") + " MaintainDate = '" + eptinfo.MaintainDate.ToShortDateString() + "',") + " RecordDate = '" + eptinfo.RecordDate.ToShortDateString() + "',") + " Appraisal = '" + eptinfo.Appraisal.ToString() + "',") + " Appraiser = '" + eptinfo.Appraiser.ToString() + "',") + " AppraisalDate = '" + eptinfo.AppraisalDate.ToShortDateString() + "' ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " where MaintainUniqueCode = '", maintainUniqueCode, "'" }) + AddAndDeleteRecordList(templist, maintainUniqueCode) + " end ");
        }
    }
}

