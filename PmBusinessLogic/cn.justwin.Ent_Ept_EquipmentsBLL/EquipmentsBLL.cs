namespace cn.justwin.Ent_Ept_EquipmentsBLL
{
    using cn.justwin.Ent_Ept_EquipmentsDAL;
    using System;
    using System.Data;
    using System.Text;

    public class EquipmentsBLL
    {
        private EquipmentsDAL dal = new EquipmentsDAL();

        public void Add(string EquipmentUniqueCode, string project)
        {
            this.dal.Add(EquipmentUniqueCode, project);
        }

        public bool Exists(string EquipmentUniqueCode)
        {
            return this.dal.Exists(EquipmentUniqueCode);
        }

        public DataTable GetList(string deptId, string equipmentCode, string equipmentName, string equipmentType, string _OriginalPriceA, string _OriginalPriceB, int _state, string _order)
        {
            StringBuilder builder = new StringBuilder();
            if (deptId != "")
            {
                builder.Append("");
            }
            if (_state > 0)
            {
                builder.Append(" ").Append(" State=").Append(_state).Append(" AND ");
            }
            if (equipmentCode != "")
            {
                builder.Append(" EquipmentManualCode like '%").Append(equipmentCode).Append("%'  AND");
            }
            if (equipmentName != "")
            {
                builder.Append(" EquipmentName like '%").Append(equipmentName).Append("%'  AND");
            }
            if (equipmentType != "")
            {
                builder.Append(" EquipmentType = '").Append(equipmentType).Append("' AND ");
            }
            if ((_OriginalPriceA.ToString() != "") && (_OriginalPriceB.ToString() != ""))
            {
                builder.Append("  OriginalPrice >=").Append(int.Parse(_OriginalPriceA)).Append(" ");
                builder.Append(" AND OriginalPrice <= ").Append(int.Parse(_OriginalPriceB)).Append(" AND ");
            }
            if (_order != "")
            {
                builder.Append(" ").Append(_order).Append(" ");
            }
            return this.dal.GetList(builder.ToString());
        }

        public string projectName(string EquipmentUniqueCode)
        {
            return this.dal.projectName(EquipmentUniqueCode);
        }

        public int Update(string EquipmentUniqueCode, string project)
        {
            return this.dal.Update(EquipmentUniqueCode, project);
        }
    }
}

