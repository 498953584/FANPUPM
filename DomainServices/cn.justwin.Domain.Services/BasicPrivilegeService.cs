namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BasicPrivilegeService : Repository<BasicPrivilege>
    {
        public void Delete(string tableName, string key)
        {
            string sql = string.Format("DELETE FROM Basic_Privilege WHERE RelationsTable = '{0}' AND RelationsKey = '{1}'", tableName, key);
            base.ExcuteSql(sql);
        }

        public bool Exists(BasicPrivilege bp)
        {
            return ((from p in this
                where ((p.RelationsTable == bp.RelationsTable) && (p.RelationsKey == bp.RelationsKey)) && (p.UserCode == bp.UserCode)
                select p).Count<BasicPrivilege>() > 0);
        }

        public BasicPrivilege GetById(string id)
        {
            return (from p in this
                where p.PrivilegeId == id
                select p).FirstOrDefault<BasicPrivilege>();
        }

        public IList<string> GetResourceId(string tableName, string userCode)
        {
            return (from p in this
                where (p.RelationsTable == tableName) && (p.UserCode == userCode)
                select p.RelationsKey).ToList<string>();
        }

        public IList<string> GetUser(string tableName, string key)
        {
            return (from p in this
                where (p.RelationsTable == tableName) && (p.RelationsKey == key)
                select p.UserCode).ToList<string>();
        }
    }
}

