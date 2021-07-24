namespace cn.justwin.Domain.Services
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PrivilegePersonServices
    {
        public void Add(string table, string key, IList<string> userCodes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                using (IEnumerator<string> enumerator = userCodes.GetEnumerator())
                {
                    string code;
                    while (enumerator.MoveNext())
                    {
                        code = enumerator.Current;
                        if ((from p in entities.Basic_Privilege
                            where ((p.RelationsTable == table) && (p.RelationsKey == key)) && (p.UserCode == code)
                            select p).Count<Basic_Privilege>() == 0)
                        {
                            Basic_Privilege entity = new Basic_Privilege {
                                PrivilegeId = Guid.NewGuid().ToString(),
                                RelationsTable = table,
                                RelationsKey = key,
                                UserCode = code
                            };
                            entities.Basic_Privilege.AddObject(entity);
                        }
                    }
                }
                entities.SaveChanges();
            }
        }

        public void Delete(string table, string key)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("DELETE FROM Basic_Privilege ");
                builder.AppendFormat("WHERE RelationsTable = '{0}' AND RelationsKey = '{1}'", table, key);
                entities.ExecuteStoreCommand(builder.ToString(), new object[0]);
                entities.SaveChanges();
            }
        }

        public IList<string> GetKeyList(string userCode, string table)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.Basic_Privilege
                    where (p.RelationsTable == table) && (p.UserCode == userCode)
                    select p.RelationsKey).ToList<string>();
            }
        }

        public void Update(string table, string key, IList<string> userCodes)
        {
            this.Delete(table, key);
            this.Add(table, key, userCodes);
        }
    }
}

