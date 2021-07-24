namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTWarningService : Repository<PTWarning>
    {
        public void DeleteInvalidData()
        {
            try
            {
                foreach (PTWarning warning in this.ToList<PTWarning>())
                {
                    try
                    {
                        string sql = "SELECT COUNT(*) FROM " + warning.RelationsTable + " WHERE " + warning.RelationsColumn + " = '" + warning.RelationsKey + "'";
                        object obj2 = base.ExcuteSql(sql).FirstOrDefault<object>();
                        if ((obj2 != null) && (Convert.ToInt32(obj2) == 0))
                        {
                            base.Delete(warning);
                        }
                    }
                    catch
                    {
                        base.Delete(warning);
                    }
                }
            }
            catch
            {
            }
        }

        public PTWarning GetById(int id)
        {
            return (from w in this
                where w.WarningId == id
                select w).FirstOrDefault<PTWarning>();
        }
    }
}

