namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class XPMBasicCodeListService : Repository<XPMBasicCodeList>
    {
        public XPMBasicCodeList GetById(int id)
        {
            return (from c in this
                where c.NoteID == id
                select c).FirstOrDefault<XPMBasicCodeList>();
        }

        public IList<XPMBasicCodeList> GetByTypeId(int typeId)
        {
            return (from c in this
                where ((c.TypeID == typeId) && c.IsValid) && (c.IsVisible == true)
                orderby c.CodeID
                select c).ToList<XPMBasicCodeList>();
        }

        public IList<XPMBasicCodeList> GetByTypeId(int typeId, string userCode)
        {
            IList<string> userList = new BasicPrivilegeService().GetResourceId("XPM_Basic_CodeList", userCode);
            return (from c in this
                where (((c.TypeID == typeId) && c.IsValid) && (c.IsVisible == true)) && userList.Contains(c.NoteID.ToString())
                select c).ToList<XPMBasicCodeList>();
        }

        public XPMBasicCodeList GetNameByCodeId(int codeId, string signCode)
        {
            XPMBasicCodeType type = new XPMBasicCodeTypeService().GetBySignCode(signCode);
            return (from c in this
                where (c.TypeID == type.TypeID) && (c.CodeID == codeId)
                select c).FirstOrDefault<XPMBasicCodeList>();
        }

        public IList<string> GetSubList(string id)
        {
            List<string> list = new List<string>();
            string cmdText = string.Format("\r\n\t\t\t\tWITH cte_CodeList AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT NoteID, CodeID, CodeName, ParentCodeID\r\n\t\t\t\t\tFROM XPM_Basic_CodeList\r\n\t\t\t\t\tWHERE NoteID = '{0}'\r\n\t\t\t\t\tUNION ALL\r\n\t\t\t\t\tSELECT C.NoteID, C.CodeID, C.CodeName, C.ParentCodeID\r\n\t\t\t\t\tFROM XPM_Basic_CodeList AS C\r\n\t\t\t\t\tINNER JOIN cte_CodeList ON C.ParentCodeID = cte_CodeList.CodeID\r\n\t\t\t\t\tWHERE C.IsValid = '1'\r\n\t\t\t\t)\r\n\t\t\t\tSELECT NoteID FROM cte_CodeList\r\n\t\t\t", id);
            foreach (DataRow row in base.ExecuteQuery(cmdText, new SqlParameter[0]).Rows)
            {
                list.Add(row["NoteID"].ToString());
            }
            return list;
        }
    }
}

