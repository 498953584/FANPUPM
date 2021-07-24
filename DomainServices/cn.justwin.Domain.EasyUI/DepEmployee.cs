namespace cn.justwin.Domain.EasyUI
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.Serialize;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DepEmployee
    {
        private void AddChildren(DepEmployee depEmp, IList<PTdbm> allDepLst, int level)
        {
            if (level != 0)
            {
                List<PTyhmc> allEmp = (from e in new PTYhmcService()
                    where e.c_sfyx == "y"
                    select e).ToList<PTyhmc>();
                List<PTdbm> list2 = (from d in allDepLst.Where<PTdbm>(delegate (PTdbm d) {
                    int? nullable = d.I_sjdm;
                    int num = Convert.ToInt32(depEmp.Id);
                    return ((nullable.GetValueOrDefault() == num) && nullable.HasValue) && (d.C_sfyx == "y");
                })
                    orderby d.I_xh, d.I_bmdm
                    select d).ToList<PTdbm>();
                if (list2.Count <= 0)
                {
                    this.AddEmployee(depEmp, allEmp);
                }
                else
                {
                    int num = level - 1;
                    for (int i = 0; i < list2.Count; i++)
                    {
                        DepEmployee byDepment = this.GetByDepment(list2[i]);
                        this.AddChildren(byDepment, allDepLst, num);
                        if (depEmp.Children == null)
                        {
                            depEmp.Children = new List<DepEmployee>();
                        }
                        depEmp.Children.Add(byDepment);
                    }
                    this.AddEmployee(depEmp, allEmp);
                }
            }
        }

        private void AddEmployee(DepEmployee depEmp, IList<PTyhmc> allEmp)
        {
            List<PTyhmc> list = (from e in allEmp
                where e.i_bmdm == Convert.ToInt32(depEmp.Id)
                orderby e.v_yhdm descending
                select e).ToList<PTyhmc>();
            for (int i = 0; i < list.Count; i++)
            {
                DepEmployee byEmployee = this.GetByEmployee(list[i]);
                if (depEmp.Children == null)
                {
                    depEmp.Children = new List<DepEmployee>();
                }
                depEmp.Children.Add(byEmployee);
            }
        }

        private DepEmployee GetByDepment(PTdbm depment)
        {
            DepEmployee employee = new DepEmployee {
                Id = depment.I_bmdm.ToString()
            };
            string str = depment.V_BMMC;
            if (str.Length > 20)
            {
                str = str.Substring(0, 20);
            }
            employee.Text = str;
            if (this.GetChildren(employee.Id).Count > 0)
            {
                employee.State = "closed";
            }
            if (employee.Id == "1")
            {
                employee.State = "open";
            }
            return employee;
        }

        private DepEmployee GetByEmployee(PTyhmc emp)
        {
            return new DepEmployee { Id = emp.v_yhdm, Text = emp.v_xm, IconCls = "icon-member" };
        }

        public IList<DepEmployee> GetChildren(string id)
        {
            List<DepEmployee> list = new List<DepEmployee>();
            PTdbmService service = new PTdbmService();
            List<PTdbm> list2 = (from d in service
                where (d.I_sjdm == Convert.ToInt32(id)) && (d.C_sfyx == "y")
                orderby d.I_xh, d.I_bmdm
                select d).ToList<PTdbm>();
            for (int i = 0; i < list2.Count; i++)
            {
                DepEmployee byDepment = this.GetByDepment(list2[i]);
                list.Add(byDepment);
            }
            List<PTyhmc> list3 = (from e in new PTYhmcService()
                where (e.i_bmdm == Convert.ToInt32(id)) && (e.c_sfyx == "y")
                orderby e.v_yhdm descending
                select e).ToList<PTyhmc>();
            for (int j = 0; j < list3.Count; j++)
            {
                DepEmployee byEmployee = this.GetByEmployee(list3[j]);
                list.Add(byEmployee);
            }
            return list;
        }

        public IList<DepEmployee> GetDepEmployee(int level)
        {
            IList<DepEmployee> list = new List<DepEmployee>();
            List<PTdbm> allDepLst = (from d in new PTdbmService()
                where d.C_sfyx == "y"
                select d).ToList<PTdbm>();
            PTdbm depment = (from d in allDepLst
                where d.I_bmdm == 1
                select d).First<PTdbm>();
            DepEmployee byDepment = this.GetByDepment(depment);
            this.AddChildren(byDepment, allDepLst, level - 1);
            list.Add(byDepment);
            return list;
        }

        public string GetJson(int level)
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Serialize<DepEmployee[]>(this.GetDepEmployee(level).ToArray<DepEmployee>());
        }

        [DataMember(Name="children", Order=5, EmitDefaultValue=false, IsRequired=false)]
        public IList<DepEmployee> Children { get; set; }

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

