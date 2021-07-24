namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class OrganizationMonthBudget
    {
        private OrganizationMonthBudget()
        {
        }

        public void Add(OrganizationMonthBudget indirMonthBudget)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_OrganizationBudget budget = (from m in entities.Bud_OrganizationBudget
                    where m.Id == indirMonthBudget.IndirectBudgetId
                    select m).FirstOrDefault<Bud_OrganizationBudget>();
                Bud_OrganizationMonthBudget budget2 = new Bud_OrganizationMonthBudget {
                    Id = indirMonthBudget.Id,
                    Amount = new decimal?(indirMonthBudget.Amount),
                    Month = indirMonthBudget.Month,
                    Year = indirMonthBudget.Year,
                    Bud_OrganizationBudget = budget
                };
                entities.AddToBud_OrganizationMonthBudget(budget2);
                entities.SaveChanges();
            }
        }

        public static OrganizationMonthBudget Create(string id, string indirectBudgetId, decimal amount, int year, int month)
        {
            string paramName = "间接成本月度预算";
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(paramName, "Id不能为空!");
            }
            if (string.IsNullOrEmpty(indirectBudgetId))
            {
                throw new ArgumentNullException(paramName, "间接成本预算Id不能为空!");
            }
            return new OrganizationMonthBudget { Id = id, Amount = amount, Month = month, Year = year, IndirectBudgetId = indirectBudgetId };
        }

        public static void DelAll(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Bud_OrganizationMonthBudget budget in (from m in entities.Bud_OrganizationMonthBudget
                    where m.Bud_OrganizationBudget.Id == id
                    select m).ToList<Bud_OrganizationMonthBudget>())
                {
                    entities.DeleteObject(budget);
                }
                entities.SaveChanges();
            }
        }

        public static List<OrganizationMonthBudget> GetAll(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_OrganizationMonthBudget
                    where m.Bud_OrganizationBudget.Id == id
                    orderby m.Year descending, m.Month descending
                    select new OrganizationMonthBudget { Id = m.Id, Amount = (decimal) m.Amount, Year = m.Year, Month = m.Month, IndirectBudgetId = m.Bud_OrganizationBudget.Id }).ToList<OrganizationMonthBudget>();
            }
        }

        public decimal Amount { get; set; }

        public string Id { get; set; }

        public OrganizationBudget IndirectBudget
        {
            get
            {
                return OrganizationBudget.GetById(this.IndirectBudgetId);
            }
        }

        public string IndirectBudgetId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}

