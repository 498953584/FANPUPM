namespace cn.justwin.PrjManager
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class PrjMemberCommit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            Guid prjId = new Guid(key.ToString());
            PTPrjMemberService service = new PTPrjMemberService();
            List<string> list = (from m in service
                where m.PrjGuid.Value == prjId
                select m.MemberCode).ToList<string>();
            PTPrjInfoZTBUserService service2 = new PTPrjInfoZTBUserService();
            foreach (string str in list)
            {
                PTPrjInfoZTBUser item = new PTPrjInfoZTBUser {
                    Id = Guid.NewGuid().ToString(),
                    PrjGuid = prjId.ToString(),
                    UserCode = str
                };
                service2.Add(item);
            }
        }

        public void RefuseEvent(object key)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            string cmdText = "DELETE FROM PT_PrjMember WHERE PrjGuid='" + key + "'";
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText);
            Guid prjGuid = new Guid(key.ToString());
            PrjMemberWF.Add(prjGuid);
        }
    }
}

