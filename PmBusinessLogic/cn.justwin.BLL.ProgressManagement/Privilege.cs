namespace cn.justwin.BLL.ProgressManagement
{
    using cn.justwin.DAL;
    using com.jwsoft.pm.data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class Privilege
    {
        public static void AddPrivilege(string progressId, IList<string> userCodes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<plus_progress_privilege> list = (from m in entities.plus_progress_privilege
                    where m.plus_progress.ProgressId == progressId
                    select m).ToList<plus_progress_privilege>();
                if (list != null)
                {
                    foreach (plus_progress_privilege _privilege in list)
                    {
                        entities.DeleteObject(_privilege);
                    }
                }
                plus_progress progress = (from m in entities.plus_progress
                    where m.ProgressId == progressId
                    select m).FirstOrDefault<plus_progress>();
                AddPrivilege(userCodes, progress, entities);
                entities.SaveChanges();
            }
        }

        public static void AddPrivilege(IList<string> userCodes, plus_progress progress, pm2Entities context)
        {
            if (userCodes != null)
            {
                using (IEnumerator<string> enumerator = userCodes.GetEnumerator())
                {
                    string userCode;
                    while (enumerator.MoveNext())
                    {
                        userCode = enumerator.Current;
                        if (!IsExist(progress.ProgressId, userCode))
                        {
                            string str = Guid.NewGuid().ToString();
                            plus_progress_privilege _privilege = new plus_progress_privilege {
                                PrivilegeId = str,
                                plus_progress = progress,
                                PT_yhmc = (from m in context.PT_yhmc
                                    where m.v_yhdm == userCode
                                    select m).FirstOrDefault<PT_yhmc>()
                            };
                            context.AddToplus_progress_privilege(_privilege);
                        }
                    }
                }
            }
        }

        public static void AddPrivilegenew(string progressId, List<string> userCodes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (string str2 in userCodes)
                {
                    publicDbOpClass.ExecuteSQL("DELETE FROM plus_progress_privilege WHERE ProgressId='" + progressId + "' and UserCode='" + str2 + "'");
                }
                plus_progress progress = (from m in entities.plus_progress
                    where m.ProgressId == progressId
                    select m).FirstOrDefault<plus_progress>();
                AddPrivilege(userCodes, progress, entities);
                entities.SaveChanges();
            }
        }

        public static void AddPrivilegeNoDelete(string progressId, IList<string> userCodes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                plus_progress progress = (from m in entities.plus_progress
                    where m.ProgressId == progressId
                    select m).FirstOrDefault<plus_progress>();
                AddPrivilege(userCodes, progress, entities);
                entities.SaveChanges();
            }
        }

        public static void Delete(string progressId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<plus_progress_privilege> list = (from m in entities.plus_progress_privilege
                    where m.plus_progress.ProgressId == progressId
                    select m).ToList<plus_progress_privilege>();
                if (list != null)
                {
                    foreach (plus_progress_privilege _privilege in list)
                    {
                        entities.DeleteObject(_privilege);
                    }
                }
                entities.SaveChanges();
            }
        }

        public static List<string> GetUserCodes(string progressId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> list = new List<string>();
                return (from m in entities.plus_progress_privilege
                    where m.plus_progress.ProgressId == progressId
                    select m.PT_yhmc.v_yhdm).Distinct<string>().ToList<string>();
            }
        }

        public static bool IsExist(string progressId, string user)
        {
            string cmdText = string.Format("SELECT COUNT(*) FROM plus_progress_privilege WHERE ProgressId = '{0}' AND UserCode = '{1}'", progressId, user);
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText)) > 0);
        }
    }
}

