namespace cn.justwin.Domain
{
    using cn.justwin.BLL.Domain.HumanResources.Services;
    using System;
    using System.Runtime.CompilerServices;

    public class Department
    {
        private User chargeMan;

        public User ChargeMan
        {
            get
            {
                if (this.chargeMan == null)
                {
                    this.chargeMan = new UserServices().GetChargeMan(this.DepartmentId);
                }
                return this.chargeMan;
            }
        }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}

