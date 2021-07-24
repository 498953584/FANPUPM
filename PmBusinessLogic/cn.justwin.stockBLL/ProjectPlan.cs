namespace cn.justwin.stockBLL
{
    using cn.justwin.Domain;
    using System;
    using System.Collections.Generic;

    public class ProjectPlan
    {
        private string projectCode;

        public ProjectPlan(string projectCode)
        {
            this.projectCode = projectCode;
        }

        public virtual Dictionary<string, decimal> GetFactResource()
        {
            return BudTask.GetResourceQuantity(this.projectCode);
        }

        public virtual List<string> GetPrjUsers(string projectCode)
        {
            EPMTaskResource resource = new EPMTaskResource();
            return resource.GetPrjUsers(projectCode);
        }

        public virtual string GetResourceNameByCode(string resourceCode)
        {
            EPMTaskResource resource = new EPMTaskResource();
            return resource.GetResourceNameByCode(resourceCode);
        }

        public string ProjectCode
        {
            get
            {
                return this.projectCode;
            }
        }
    }
}

