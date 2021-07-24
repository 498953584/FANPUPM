namespace cn.justwin.Domain.EasyUI
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class Department
    {
        private void AddChildren(Department depEmp, IList<PTdbm> allDepList, int level)
        {
            if (level != 0)
            {
                List<PTdbm> list = (from d in allDepList.Where<PTdbm>(delegate (PTdbm d) {
                    int? nullable = d.I_sjdm;
                    int num = Convert.ToInt32(depEmp.Id);
                    return ((nullable.GetValueOrDefault() == num) && nullable.HasValue) && (d.C_sfyx == "y");
                })
                    orderby d.I_xh, d.I_bmdm
                    select d).ToList<PTdbm>();
                int numx = level - 1;
                for (int i = 0; i < list.Count; i++)
                {
                    Department byDepment = this.GetByDepment(list[i]);
                    this.AddChildren(byDepment, allDepList, numx);
                    if (depEmp.Children == null)
                    {
                        depEmp.Children = new List<Department>();
                    }
                    depEmp.Children.Add(byDepment);
                }
            }
        }

        private Department GetByDepment(PTdbm depment)
        {
            Department department = new Department {
                Id = depment.I_bmdm.ToString()
            };
            string str = depment.V_BMMC;
            if (str.Length > 20)
            {
                str = str.Substring(0, 20);
            }
            department.Text = str;
            if (this.GetChildren(department.Id).Count > 0)
            {
                department.State = "closed";
            }
            if (department.Id == "1")
            {
                department.State = "open";
            }
            return department;
        }

        public IList<Department> GetChildren(string id)
        {
            List<Department> list = new List<Department>();
            PTdbmService service = new PTdbmService();
            List<PTdbm> list2 = (from d in service
                where (d.I_sjdm == Convert.ToInt32(id)) && (d.C_sfyx == "y")
                orderby d.I_xh, d.I_bmdm
                select d).ToList<PTdbm>();
            for (int i = 0; i < list2.Count; i++)
            {
                Department byDepment = this.GetByDepment(list2[i]);
                list.Add(byDepment);
            }
            return list;
        }

        public IList<Department> GetDepartment(int level = 10)
        {
            IList<Department> list = new List<Department>();
            List<PTdbm> allDepList = (from d in new PTdbmService()
                where d.C_sfyx == "y"
                select d).ToList<PTdbm>();
            PTdbm depment = (from d in allDepList
                where d.I_bmdm == 1
                select d).First<PTdbm>();
            Department byDepment = this.GetByDepment(depment);
            this.AddChildren(byDepment, allDepList, level - 1);
            list.Add(byDepment);
            return list;
        }

        [DataMember(Name="children", Order=5, EmitDefaultValue=false, IsRequired=false)]
        public IList<Department> Children { get; set; }

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

