namespace cn.justwin.BLL.Domain.HumanResources.Services
{
    using cn.justwin.DAL;
    using cn.justwin.Domain;
    using System;
    using System.Linq;

    public class UserServices
    {
        private User ConvertToUser(PT_yhmc pt_user)
        {
            User user = null;
            if (pt_user != null)
            {
                user = new User {
                    UserCode = pt_user.v_yhdm,
                    UserName = pt_user.v_xm,
                    DepartmentId = pt_user.i_bmdm.Value
                };
            }
            return user;
        }

        public User GetByCode(string userCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                PT_yhmc _yhmc = (from u in entities.PT_yhmc
                    where u.v_yhdm == userCode
                    select u).FirstOrDefault<PT_yhmc>();
                return this.ConvertToUser(_yhmc);
            }
        }

        public User GetChargeMan(int departmentId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                PT_yhmc _yhmc = (from u in entities.PT_yhmc
                    where (u.i_bmdm == departmentId) && u.IsChargeMan
                    select u).FirstOrDefault<PT_yhmc>();
                return this.ConvertToUser(_yhmc);
            }
        }
    }
}

