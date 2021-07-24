using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace cn.justwin.Tender
{
    public class TenderUser
    {
        public string Id
        {
            get;
            set;
        }
        public System.Guid PrjGuid
        {
            get;
            set;
        }
        public string UserCode
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        private TenderUser()
        {
        }
       	public static System.Collections.Generic.List<TenderUser> GetById(string prjGuid)
		{
			System.Collections.Generic.List<TenderUser> users = new System.Collections.Generic.List<TenderUser>();
			System.Guid guid = new System.Guid(prjGuid);
			using (pm2Entities context = new pm2Entities())
			{
                users = (
                    from m in context.PT_PrjInfo_ZTB_User
                    join yh in context.PT_yhmc on m.UserCode equals yh.v_yhdm
                    where m.PrjGuid == (System.Guid?)guid
                    select new TenderUser { 
                    Id=m.Id,
                    PrjGuid=(System.Guid)m.PrjGuid,
                    UserCode=m.UserCode,
                    UserName=yh.v_xm
                    }).ToList<TenderUser>();
			}
			return users;
		}
        public static System.Collections.Generic.List<string> GetUserCodes(System.Guid guid)
        {
            System.Collections.Generic.List<string> result;
            using (pm2Entities context = new pm2Entities())
            {
                System.Collections.Generic.List<string> userCodes = (
                    from m in context.PT_PrjInfo_ZTB_User
                    where m.PrjGuid == (System.Guid?)guid
                    select m.UserCode).ToList<string>();
                result = userCodes;
            }
            return result;
        }
        public static void AddUser(string prjGuid, System.Collections.Generic.List<string> userCodes)
        {
            System.Guid guid = new System.Guid(prjGuid);
            using (pm2Entities context = new pm2Entities())
            {
                PTPrjInfoZTBUserService prjUserSer = new PTPrjInfoZTBUserService();
                prjUserSer.Delete(prjGuid);
                if (userCodes != null)
                {
                    foreach (string code in userCodes)
                    {
                        PT_yhmc yh = (
                            from m in context.PT_yhmc
                            where m.v_yhdm == code
                            select m).FirstOrDefault<PT_yhmc>();
                        if (yh != null)
                        {
                            PT_PrjInfo_ZTB_User user = new PT_PrjInfo_ZTB_User
                            {
                                Id = System.Guid.NewGuid().ToString(),
                                PrjGuid = new System.Guid?(guid),
                                UserCode = code
                            };
                            context.AddToPT_PrjInfo_ZTB_User(user);
                        }
                    }
                }
                context.SaveChanges();
            }
        }
        public static void AddUser(System.Collections.Generic.List<string> prjGuids, System.Collections.Generic.List<string> userCodes)
        {
            using (new pm2Entities())
            {
                foreach (string id in prjGuids)
                {
                    System.Guid guid = new System.Guid(id);
                    System.Collections.Generic.List<string> oldCodes = TenderUser.GetUserCodes(guid);
                    foreach (string code in userCodes)
                    {
                        if (!oldCodes.Contains(code))
                        {
                            TenderUser.Add(id, code);
                        }
                    }
                }
            }
        }
        public static void Add(string prjGuid, string userCode)
        {
            using (pm2Entities context = new pm2Entities())
            {
                PT_PrjInfo_ZTB_User user = new PT_PrjInfo_ZTB_User
                {
                    Id = System.Guid.NewGuid().ToString(),
                    PrjGuid = new System.Guid?(new System.Guid(prjGuid)),
                    UserCode = userCode
                };
                context.AddToPT_PrjInfo_ZTB_User(user);
                context.SaveChanges();
            }
        }
        public static void Del(System.Guid prjGuid, string userCode)
        {
            using (pm2Entities context = new pm2Entities())
            {
                PT_PrjInfo_ZTB_User pt_user = (
                    from m in context.PT_PrjInfo_ZTB_User
                    where m.PrjGuid == (System.Guid?)prjGuid && m.UserCode == userCode
                    select m).FirstOrDefault<PT_PrjInfo_ZTB_User>();
                if (pt_user != null)
                {
                    context.DeleteObject(pt_user);
                    context.SaveChanges();
                }
            }
        }
        public static bool isQuote(string code)
        {
            bool quote = false;
            using (pm2Entities context = new pm2Entities())
            {
                int count = (
                    from m in context.PT_PrjInfo_ZTB_Detail
                    where m.PrjDutyPerson == code || m.BusinessManager == code || m.ProgAgent == code
                    select m).ToList<PT_PrjInfo_ZTB_Detail>().Count<PT_PrjInfo_ZTB_Detail>();
                int prjManager = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjManager == code
                    select m).ToList<PT_PrjInfo_ZTB>().Count<PT_PrjInfo_ZTB>();
                if (count + prjManager > 1)
                {
                    quote = true;
                }
            }
            return quote;
        }
        public static bool isQuote(string code, System.Guid prjGuid)
        {
            bool quote = false;
            using (pm2Entities context = new pm2Entities())
            {
                int count = (
                    from m in context.PT_PrjInfo_ZTB_Detail
                    where (m.PrjDutyPerson == code || m.BusinessManager == code || m.ProgAgent == code) && m.PrjGuid == prjGuid
                    select m).ToList<PT_PrjInfo_ZTB_Detail>().Count<PT_PrjInfo_ZTB_Detail>();
                int prjManager = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjManager == code && m.PrjGuid == prjGuid
                    select m).ToList<PT_PrjInfo_ZTB>().Count<PT_PrjInfo_ZTB>();
                if (count + prjManager > 1)
                {
                    quote = true;
                }
            }
            return quote;
        }
        public static bool isExist(System.Guid guid, string code)
        {
            bool exist = false;
            using (pm2Entities context = new pm2Entities())
            {
                int existCount = (
                    from m in context.PT_PrjInfo_ZTB_User
                    where m.PrjGuid == (System.Guid?)guid && m.UserCode == code
                    select m.UserCode).Count<string>();
                if (existCount > 0)
                {
                    exist = true;
                }
            }
            return exist;
        }
    }
}
