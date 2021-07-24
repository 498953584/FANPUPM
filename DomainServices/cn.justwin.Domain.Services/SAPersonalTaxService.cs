namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class SAPersonalTaxService : Repository<SAPersonalTax>
    {
        public SAPersonalTax GetById(string id)
        {
            return (from spt in this
                where spt.Id == id
                select spt).FirstOrDefault<SAPersonalTax>();
        }

        public decimal GetDeduct(decimal taxable)
        {
            foreach (SAPersonalTax tax in this.ToList<SAPersonalTax>())
            {
                if ((taxable > tax.FloorLevel) && (taxable <= tax.TopLevel))
                {
                    return tax.Deduct;
                }
            }
            return 0M;
        }

        public decimal GetTaxRate(decimal taxable)
        {
            foreach (SAPersonalTax tax in this.ToList<SAPersonalTax>())
            {
                if ((taxable > tax.FloorLevel) && (taxable <= tax.TopLevel))
                {
                    return tax.TaxRate;
                }
            }
            return 0M;
        }
    }
}

