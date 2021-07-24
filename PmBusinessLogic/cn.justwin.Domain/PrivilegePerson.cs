namespace cn.justwin.Domain
{
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;

    public class PrivilegePerson : IPrivilege
    {
        private string adminCode;
        private string currentUserCode;
        private string relationsTable;

        private PrivilegePerson()
        {
            this.adminCode = "00000000";
        }

        public PrivilegePerson(string userCode, string relationsTable)
        {
            this.adminCode = "00000000";
            this.currentUserCode = userCode;
            this.relationsTable = relationsTable;
        }

        public void AddDefaultPrivilege(string key)
        {
            IList<string> userCodes = new List<string>();
            if (!userCodes.Contains(this.currentUserCode))
            {
                userCodes.Add(this.currentUserCode);
            }
            if (!userCodes.Contains(this.adminCode))
            {
                userCodes.Add(this.adminCode);
            }
            new PrivilegePersonServices().Add(this.relationsTable, key, userCodes);
        }

        public void ChangeTable(string tableName)
        {
            this.relationsTable = tableName;
        }

        public void Delete(string key)
        {
            new PrivilegePersonServices().Delete(this.relationsTable, key);
        }

        public IList<string> GetKeyList()
        {
            PrivilegePersonServices services = new PrivilegePersonServices();
            return services.GetKeyList(this.currentUserCode, this.relationsTable);
        }

        public void Update(string key, IList<string> userCodeList)
        {
            new PrivilegePersonServices().Update(this.relationsTable, key, userCodeList);
        }
    }
}

