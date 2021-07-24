namespace cn.justwin.BLL.Privilege
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Privilege
    {
        private Privilege()
        {
        }

        public static int Add(List<string> userCodes, string relationsTable, string relationsKey)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Basic_Privilege privilege in (from m in entities.Basic_Privilege
                    where (m.RelationsTable == relationsTable) && (m.RelationsKey == relationsKey)
                    select m).ToList<Basic_Privilege>())
                {
                    entities.DeleteObject(privilege);
                }
                if (userCodes != null)
                {
                    foreach (string str in userCodes)
                    {
                        string str2 = Guid.NewGuid().ToString();
                        Basic_Privilege privilege2 = new Basic_Privilege {
                            PrivilegeId = str2,
                            RelationsTable = relationsTable,
                            RelationsKey = relationsKey,
                            UserCode = str
                        };
                        entities.AddToBasic_Privilege(privilege2);
                    }
                }
                return entities.SaveChanges();
            }
        }

        public static List<string> GetUserCodes(string relationsTable, string relationsKey)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Basic_Privilege
                    where (m.RelationsTable == relationsTable) && (m.RelationsKey == relationsKey)
                    select m.UserCode).ToList<string>();
            }
        }
    }
}

