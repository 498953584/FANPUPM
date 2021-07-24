namespace cn.justwin.BLL
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SelectProjectHelper
    {
        private static bool ExistParent(string typeCode, List<SelectProject> prjList)
        {
            return ((from p in prjList
                where p.TypeCode == typeCode.Substring(0, 5)
                select p).Count<SelectProject>() > 0);
        }

        public static IList<SelectProject> GetProject(string userCode, IList<string> stateList, string code, string name)
        {
            IList<string> busiDataId = PrivHelper.GetBusiDataId("project", userCode);
            PTPrjInfoService service = new PTPrjInfoService();
            List<SelectProject> first = new List<SelectProject>();
            int num = (busiDataId.Count / 0x5dc) + 1;
            for (int i = 0; i < num; i++)
            {
                List<string> theIdList = busiDataId.Skip<string>((i * 0x5dc)).Take<string>(0x5dc).ToList<string>();
                List<SelectProject> second = (from p in service
                    where (((stateList.Contains(p.PrjState.ToString()) && theIdList.Contains(p.PrjGuid.ToString())) && (p.IsValid == "1")) && p.PrjCode.Contains(code)) && p.PrjName.Contains(name)
                    select new SelectProject { Id = p.PrjGuid.Value.ToString(), Code = p.PrjCode, Name = p.PrjName, TypeCode = p.TypeCode, OwnerCode = p.OwnerCode.ToString(), Place = p.PrjPlace, State = p.PrjState.Value, Order = p.StartDate.Value.ToString("yyyyMMdd"), IsParent = true }).ToList<SelectProject>();
                first = first.Union<SelectProject>(second).ToList<SelectProject>();
            }
            for (int j = 0; j < first.Count; j++)
            {
                SelectProject project = first[j];
                if (project.TypeCode.Length == 10)
                {
                    PTPrjInfo parent = service.GetParent(project.Id);
                    if (parent != null)
                    {
                        project.Order = parent.StartDate.Value.ToString("yyyyMMdd") + project.Order;
                    }
                    if (ExistParent(project.TypeCode, first))
                    {
                        project.IsParent = false;
                    }
                }
                else
                {
                    project.Order = project.Order + "99999999";
                }
            }
            return (from p in first
                orderby p.Order descending
                select p).ToList<SelectProject>();
        }
    }
}

