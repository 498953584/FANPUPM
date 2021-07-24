namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IPrivilege
    {
        void AddDefaultPrivilege(string key);
        void ChangeTable(string tableName);
        void Delete(string key);
        IList<string> GetKeyList();
        void Update(string key, IList<string> userCodeList);
    }
}

