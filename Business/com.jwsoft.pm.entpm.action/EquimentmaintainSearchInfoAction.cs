namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class EquimentmaintainSearchInfoAction
    {
        public static DataTable GetEquipmentMaintainList(string dtBegin, string dtEnd)
        {
            return publicDbOpClass.GetPageData("(MaintainDate >= '" + dtBegin + "')and(MaintainDate <= '" + dtEnd + "')", "v_Ept_EquipmentMaintain", "NoteSequenceID desc");
        }

        public static int GetMaintainCount(string dtBegin, string dtEnd)
        {
            return (int) publicDbOpClass.ExecuteScalar(" select count(1) from v_Ept_EquipmentMaintain where (MaintainDate >= '" + dtBegin + "')and(MaintainDate <= '" + dtEnd + "')");
        }
    }
}

