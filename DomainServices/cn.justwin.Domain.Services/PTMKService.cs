namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), NHibernateContext, ServiceContract, AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class PTMKService : Repository<PTMK>
    {
        private void AddChildren(PTMK ptmk, IList<PTMK> allLst)
        {
            List<PTMK> list = (from m in allLst
                where m.C_MKDM.StartsWith(ptmk.C_MKDM) && (m.C_MKDM.Length == (ptmk.C_MKDM.Length + 2))
                select m).ToList<PTMK>();
            if (list.Count == 0)
            {
                ptmk.Attributes = "[{\"url\": \"" + ptmk.V_CDLJ + "\"}]";
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    this.AddChildren(list[i], allLst);
                }
                ptmk.State = "closed";
                ptmk.Children = list;
            }
        }

        [WebGet(UriTemplate="/GetAll/{userCode}/{dm}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<PTMK> GetAll(string userCode, string dm)
        {
            PTYHMCPrivilegeService service = new PTYHMCPrivilegeService();
            List<string> first = (from p in service
                where p.V_YHDM == userCode
                select p.C_MKDM).ToList<string>();
            PrivUserRoleService service2 = new PrivUserRoleService();
            PrivBusiDataRoleService service3 = new PrivBusiDataRoleService();
            List<string> second = (from bdr in service3
                join ur in service2 on bdr.RoleId equals ur.RoleId 
                where (bdr.TableName == "PT_MK") && (ur.UserCode == userCode)
                select bdr.BusiDataId).ToList<string>();
            List<string> allMkList = first.Union<string>(second).Distinct<string>().ToList<string>();
            List<PTMK> allLst = (from m in this
                where allMkList.Contains(m.C_MKDM) && (m.Isdisplay == "1")
                orderby m.I_XH, m.C_MKDM
                select m).ToList<PTMK>();
            List<PTMK> list4 = new List<PTMK>();
            if (dm == "0")
            {
                list4 = (from m in allLst
                    where m.C_MKDM.Length == 2
                    select m).ToList<PTMK>();
            }
            else
            {
                list4 = (from m in allLst
                    where (m.C_MKDM.Length == (dm.Length + 2)) && m.C_MKDM.StartsWith(dm)
                    select m).ToList<PTMK>();
            }
            for (int i = 0; i < list4.Count; i++)
            {
                this.AddChildren(list4[i], allLst);
            }
            return list4;
        }

        public PTMK GetById(string id)
        {
            return (from m in this
                where m.C_MKDM == id
                select m).FirstOrDefault<PTMK>();
        }

        [WebGet(UriTemplate="/GetChildren/{userCode}/{dm}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<PTMK> GetChildren(string userCode, string dm)
        {
            PTYHMCPrivilegeService service = new PTYHMCPrivilegeService();
            return (from m in this
                join p in service on m.C_MKDM equals p.C_MKDM
                where p.V_YHDM == userCode && m.C_MKDM.Substring(0, dm.Length) == dm
                orderby m.I_XH, m.C_MKDM
                select m).ToList<PTMK>();



        }

        public IList<string> GetChildrenAndSelf(string dm)
        {
            List<string> first = new List<string> {
                dm
            };
            List<string> second = (from d in this
                where d.C_MKDM.StartsWith(dm)
                select d.C_MKDM).ToList<string>();
            return first.Union<string>(second).Distinct<string>().ToList<string>();
        }

        [WebGet(UriTemplate="/GetFirstMenu/{userCode}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<PTMK> GetFirstMenu(string userCode)
        {
            PTYHMCPrivilegeService service = new PTYHMCPrivilegeService();
            List<string> first = (from p in service
                where (p.V_YHDM == userCode) && (p.C_MKDM.Length == 2)
                select p.C_MKDM).ToList<string>();
            PrivUserRoleService service2 = new PrivUserRoleService();
            PrivBusiDataRoleService service3 = new PrivBusiDataRoleService();
            List<string> second = (from bdr in service3
                join ur in service2 on bdr.RoleId equals ur.RoleId 
                where ((bdr.TableName == "PT_MK") && (ur.UserCode == userCode)) && (bdr.BusiDataId.Length == 2)
                select bdr.BusiDataId).ToList<string>();
            List<string> allMkList = first.Union<string>(second).Distinct<string>().ToList<string>();
            return (from m in this
                where allMkList.Contains(m.C_MKDM)
                orderby m.I_XH, m.C_MKDM
                select m).ToList<PTMK>();
        }

        public IList<string> GetParentAll(string dm)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                if (dm.Length > 2)
                {
                    dm = dm.Substring(0, dm.Length - 2);
                    list.Add(dm);
                }
            }
            return list;
        }

        public IList<string> GetParentAndChildren(string dm)
        {
            IList<string> parentAll = this.GetParentAll(dm);
            IList<string> childrenAndSelf = this.GetChildrenAndSelf(dm);
            return parentAll.Union<string>(childrenAndSelf).Distinct<string>().ToList<string>();
        }
    }
}

