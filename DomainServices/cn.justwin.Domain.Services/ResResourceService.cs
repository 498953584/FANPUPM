namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ResResourceService : Repository<ResResource>
    {
        public ResResource GetById(string id)
        {
            return (from r in this
                where r.ResourceId == id
                select r).FirstOrDefault<ResResource>();
        }

        private string GetId(string name, string brand, string specification, string modelNumber)
        {
            return (from r in this
                where (((r.ResourceName == name) && (r.Brand == brand)) && (r.Specification == specification)) && (r.ModelNumber == modelNumber)
                select r.ResourceId).FirstOrDefault<string>();
        }

        public string GetId(string name, string brand, string specification, string modelNumber, string note, char sep)
        {
            string str = this.GetId(name, brand, specification, modelNumber);
            if (!string.IsNullOrEmpty(str) || string.IsNullOrEmpty(note))
            {
                return str;
            }
            List<string> list = note.Split(new char[] { sep }).ToList<string>();
            while (list.Count < 3)
            {
                list.Add("");
            }
            return this.GetId(name, list[0], list[1], list[2]);
        }

        public bool IsExists(string resCode)
        {
            return ((from r in this
                where r.ResourceCode == resCode
                select r).Count<ResResource>() > 0);
        }
    }
}

