namespace cn.justwin.Model
{
    using System;
    using System.Collections.Generic;

    public class DepartmentInfo
    {
        public List<DepartmentInfo> Children = new List<DepartmentInfo>();
        public int ID;
        public string Name;
    }
}

