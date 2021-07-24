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
    public class DatumClass
    {
        private void AddChildren(DatumClass theDatum, IList<EPMDatumClass> all)
        {
            List<DatumClass> source = (from d in all
                where d.ParentId == Convert.ToInt32(theDatum.Id)
                select new DatumClass { Id = d.TypeId.ToString(), Text = d.TypeName, State = "open" }).ToList<DatumClass>();
            for (int i = 0; i < source.Count<DatumClass>(); i++)
            {
                if (theDatum.Children == null)
                {
                    theDatum.Children = new List<DatumClass>();
                }
                theDatum.Children.Add(source[i]);
                this.AddChildren(source[i], all);
            }
        }

        public IList<DatumClass> GetDatumClass(int rootTypeId)
        {
            List<DatumClass> list = new List<DatumClass>();
            List<EPMDatumClass> all = new EPMDatumClassService().ToList<EPMDatumClass>();
            DatumClass item = (from d in all
                where d.TypeId == rootTypeId
                select new DatumClass { Id = d.TypeId.ToString(), Text = d.TypeName, State = "open" }).FirstOrDefault<DatumClass>();
            list.Add(item);
            this.AddChildren(item, all);
            return list;
        }

        [DataMember(Name="children", Order=5, EmitDefaultValue=false, IsRequired=false)]
        public IList<DatumClass> Children { get; set; }

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

