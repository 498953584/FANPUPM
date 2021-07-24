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
    public class Treasury
    {
        public void AddChildren(Treasury trea, List<SmTreasury> all)
        {
            Func<SmTreasury, bool> predicate = null;
            if (trea.Children == null)
            {
                trea.Children = new List<Treasury>();
            }
            List<SmTreasury> list = new List<SmTreasury>();
            if (trea.Code == "0")
            {
                list = (from t in all
                    where t.tcode.Length == 3
                    orderby t.tcode
                    select t).ToList<SmTreasury>();
            }
            else
            {
                if (predicate == null)
                {
                    predicate = t => (t.tcode.Length == (trea.Code.Length + 3)) && t.tcode.StartsWith(trea.Code);
                }
                list = (from t in all.Where<SmTreasury>(predicate)
                    orderby t.tcode
                    select t).ToList<SmTreasury>();
            }
            foreach (SmTreasury treasury in list)
            {
                Treasury treasury2 = new Treasury {
                    Id = treasury.tid,
                    Code = treasury.tcode,
                    Text = treasury.tname
                };
                if (this.ContainsChildren(treasury.tcode, all))
                {
                    treasury2.State = "open";
                    this.AddChildren(treasury2, all);
                }
                if (treasury.tflag == "1")
                {
                    treasury2.Text = treasury2.Text + "(总库)";
                }
                trea.Children.Add(treasury2);
            }
        }

        private bool ContainsChildren(string code, List<SmTreasury> all)
        {
            return ((from t in all
                where (t.tcode.Length == (code.Length + 3)) && t.tcode.StartsWith(code)
                select t).Count<SmTreasury>() > 0);
        }

        public IList<Treasury> GetTreasury()
        {
            List<Treasury> list = new List<Treasury>();
            List<SmTreasury> all = new SmTreasuryService().ToList<SmTreasury>();
            SmTreasury treasury = (from t in all
                where t.tcode == "0"
                select t).FirstOrDefault<SmTreasury>();
            Treasury trea = new Treasury {
                Id = treasury.tid,
                Code = treasury.tcode,
                Text = treasury.tname,
                State = "open"
            };
            if (treasury.tflag == "1")
            {
                trea.Text = trea.Text + "(总库)";
            }
            this.AddChildren(trea, all);
            list.Add(trea);
            return list;
        }

        [DataMember(Name="children", Order=5, EmitDefaultValue=false, IsRequired=false)]
        public IList<Treasury> Children { get; set; }

        [DataMember(Name="code", Order=6)]
        public string Code { get; set; }

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

