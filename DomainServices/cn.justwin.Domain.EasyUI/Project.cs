namespace cn.justwin.Domain.EasyUI
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class Project
    {
        public IList<Project> GetOnelevelByParent(int year, IList<string> idList, IList<string> stateList, string pcode, string pname)
        {
            IList<Project> list = new List<Project>();
            Project item = new Project {
                Id = "root",
                Text = year + "年度项目",
                State = "open"
            };
            IList<Project> list2 = this.GetProjectByYear(year, idList, stateList, pcode, pname);
            item.Children = list2;
            list.Add(item);
            return list;
        }

        public IList<Project> GetOnelevelByState(int year, IList<string> idList, IList<string> stateList, string pcode, string pname)
        {
            IList<Project> list = new List<Project>();
            Project project = new Project {
                Id = "root",
                Text = year + "年度项目",
                State = "open"
            };
            PTPrjInfoService service = new PTPrjInfoService();
            new PTPrjInfoZTBService();
            BasicCodeListService service2 = new BasicCodeListService();
            IList<BasicCodeList> first = new List<BasicCodeList>();
            int num = (idList.Count / 0x5dc) + 1;
            for (int i = 0; i < num; i++)
            {
                List<string> theIdList = idList.Skip<string>((i * 0x5dc)).Take<string>(0x5dc).ToList<string>();
                List<BasicCodeList> second = (from cl in service2
                    join p in service on cl.ItemCode equals p.PrjState 
                    where (((((cl.TypeCode == "ProjectState") && (p.StartDate.HasValue && (p.StartDate.Value.Year <= year))) && (!p.EndDate.HasValue || (p.EndDate.Value.Year >= year))) && theIdList.Contains(p.PrjGuid.ToString())) && stateList.Contains(cl.ItemCode.ToString())) && (p.PrjCode.Contains(pcode) || p.PrjName.Contains(pname))
                    orderby cl.ItemCode
                    select cl).Distinct<BasicCodeList>().ToList<BasicCodeList>();
                first = first.Union<BasicCodeList>(second).ToList<BasicCodeList>();
            }
            foreach (BasicCodeList list4 in (from c in first
                orderby c.ItemCode
                select c).ToList<BasicCodeList>())
            {
                Project item = new Project {
                    Id = list4.ItemCode.ToString(),
                    Text = list4.ItemName,
                    State = "closed"
                };
                list.Add(item);
            }
            return list;
        }

        private IList<PTPrjInfo> GetProjectByLevel(int year, IList<string> idList, IList<string> stateList, int level, string pcode, string pname)
        {
            PTPrjInfoService service = new PTPrjInfoService();
            List<PTPrjInfo> first = new List<PTPrjInfo>();
            int num = (idList.Count / 0x5dc) + 1;
            for (int i = 0; i < num; i++)
            {
                List<string> theIdList = idList.Skip<string>((i * 0x5dc)).Take<string>(0x5dc).ToList<string>();
                List<PTPrjInfo> second = (from p in service
                    where (((((stateList.Contains(p.PrjState.Value.ToString()) && (p.TypeCode.Length == (level * 5))) && (p.StartDate.HasValue && (p.StartDate.Value.Year <= year))) && (!p.EndDate.HasValue || (p.EndDate.Value.Year >= year))) && theIdList.Contains(p.PrjGuid.ToString())) && (p.IsValid == "1")) && (p.PrjCode.Contains(pcode) || p.PrjName.Contains(pname))
                    select p).ToList<PTPrjInfo>();
                first = first.Union<PTPrjInfo>(second).ToList<PTPrjInfo>();
            }
            return first;
        }

        private IList<Project> GetProjectByYear(int year, IList<string> idList, IList<string> stateList, string pcode, string pname)
        {
            IList<Project> list = new List<Project>();
            IList<PTPrjInfo> first = this.GetProjectByLevel(year, idList, stateList, 1, pcode, pname);
            IList<PTPrjInfo> subList = this.GetProjectByLevel(year, idList, stateList, 2, pcode, pname);
            IList<PTPrjInfo> parent = new PTPrjInfoService().GetParent(subList);
            foreach (PTPrjInfo info in (from p in first.Union<PTPrjInfo>(parent).Distinct<PTPrjInfo>()
                orderby p.StartDate descending
                select p).ToList<PTPrjInfo>())
            {
                string item = info.PrjGuid.Value.ToString().ToUpper();
                string prjName = info.PrjName;
                if (!idList.Contains(item))
                {
                    prjName = prjName + "(无权限)";
                }
                Project project = new Project {
                    Id = item,
                    Text = prjName
                };
                if (this.IsExitsSubProject(info.TypeCode, subList))
                {
                    project.State = "closed";
                }
                else
                {
                    project.State = "open";
                }
                project.Children = null;
                list.Add(project);
            }
            return list;
        }

        public IList<Project> GetSublevelByParent(int year, IList<string> idList, IList<string> stateList, string prjId)
        {
            IList<PTPrjInfo> subproject = new PTPrjInfoService().GetSubproject(prjId);
            List<PTPrjInfo> first = new List<PTPrjInfo>();
            int num = (idList.Count / 0x5dc) + 1;
            for (int i = 0; i < num; i++)
            {
                List<string> theIdList = idList.Skip<string>((i * 0x5dc)).Take<string>(0x5dc).ToList<string>();
                List<PTPrjInfo> second = subproject.Where<PTPrjInfo>(delegate (PTPrjInfo p) {
                    if ((!stateList.Contains(p.PrjState.ToString()) || !theIdList.Contains(p.PrjGuid.ToString().ToUpper())) || (!p.StartDate.HasValue || (p.StartDate.Value.Year > year)))
                    {
                        return false;
                    }
                    if (p.EndDate.HasValue)
                    {
                        return (p.EndDate.Value.Year >= year);
                    }
                    return true;
                }).ToList<PTPrjInfo>();
                first = first.Union<PTPrjInfo>(second).ToList<PTPrjInfo>();
            }
            IList<Project> list4 = new List<Project>();
            foreach (PTPrjInfo info in first)
            {
                Project item = new Project {
                    Id = info.PrjGuid.Value.ToString(),
                    Text = info.PrjName,
                    State = "open",
                    Children = null
                };
                list4.Add(item);
            }
            return list4;
        }

        public IList<Project> GetSublevelByState(int year, IList<string> idList, string state, string pcode, string pname)
        {
            IList<Project> list = new List<Project>();
            PTPrjInfoService service = new PTPrjInfoService();
            List<PTPrjInfo> first = new List<PTPrjInfo>();
            int num = (idList.Count / 0x5dc) + 1;
            for (int i = 0; i < num; i++)
            {
                List<string> theIdList = idList.Skip<string>((i * 0x5dc)).Take<string>(0x5dc).ToList<string>();
                List<PTPrjInfo> second = (from p in service
                    where (((theIdList.Contains(p.PrjGuid.ToString()) && (p.PrjState.ToString() == state)) && (p.StartDate.HasValue && (p.StartDate.Value.Year <= year))) && (!p.EndDate.HasValue || (p.EndDate.Value.Year >= year))) && (p.PrjCode.Contains(pcode) || p.PrjName.Contains(pname))
                    select p).ToList<PTPrjInfo>();
                first = first.Union<PTPrjInfo>(second).ToList<PTPrjInfo>();
            }
            foreach (PTPrjInfo info in first)
            {
                Project item = new Project {
                    Id = info.PrjGuid.Value.ToString(),
                    Text = info.PrjName,
                    State = "open",
                    Children = null
                };
                list.Add(item);
            }
            return list;
        }

        private bool IsExitsSubProject(string typeCode, IList<PTPrjInfo> twolevelPrjList)
        {
            return ((from sp in twolevelPrjList
                where sp.TypeCode.Substring(0, 5) == typeCode
                select sp).Count<PTPrjInfo>() > 0);
        }

        [DataMember(Name="children", Order=5, EmitDefaultValue=false, IsRequired=false)]
        public IList<Project> Children { get; set; }

        [DataMember(Name="iconCls", Order=3, EmitDefaultValue=false, IsRequired=false)]
        public string IconCls { get; set; }

        [DataMember(Name="id", Order=1)]
        public string Id { get; set; }

        [DataMember(Name="state", Order=4, EmitDefaultValue=false, IsRequired=false)]
        public string State { get; set; }

        [DataMember(Name="text", Order=2)]
        public string Text { get; set; }
    }
}

