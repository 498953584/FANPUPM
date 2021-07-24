namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class EquEquipmentTypeService : Repository<EquEquipmentType>
    {
        public EquEquipmentType GetById(string id)
        {
            return (from t in this
                where t.Id == id
                select t).FirstOrDefault<EquEquipmentType>();
        }

        public bool IsShip(string id)
        {
            string format = "WITH parentType AS(SELECT * FROM Equ_EquipmentType WHERE id = '{0}'\r\n                            UNION ALL SELECT Equ_EquipmentType.* FROM parentType, Equ_EquipmentType WHERE parentType.ParentId = Equ_EquipmentType.id)\r\n                           SELECT CASE Flag WHEN 'Ship' THEN '1' ELSE '0' END AS IsShip FROM parentType WHERE ParentId is null;";
            DataTable table = base.ExecuteQuery(string.Format(format, id), new SqlParameter[0]);
            return (((table != null) && (table.Rows.Count > 0)) && "1".Equals(table.Rows[0][0]));
        }
    }
}

