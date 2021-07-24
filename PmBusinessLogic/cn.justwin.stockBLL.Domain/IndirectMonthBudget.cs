namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class IndirectMonthBudget
    {
        private IndirectMonthBudget()
        {
        }

        public void Add(IndirectMonthBudget indirMonthBudget)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_IndirectBudget budget = (from m in entities.Bud_IndirectBudget
                    where m.Id == indirMonthBudget.IndirectBudgetId
                    select m).FirstOrDefault<Bud_IndirectBudget>();
                Bud_IndirectMonthBudget budget2 = new Bud_IndirectMonthBudget {
                    Id = indirMonthBudget.Id,
                    Amount = new decimal?(indirMonthBudget.Amount),
                    Month = indirMonthBudget.Month,
                    Year = indirMonthBudget.Year,
                    Bud_IndirectBudget = budget
                };
                entities.AddToBud_IndirectMonthBudget(budget2);
                entities.SaveChanges();
            }
        }

        public static IndirectMonthBudget Create(string id, string indirectBudgetId, decimal amount, int year, int month)
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
            return new IndirectMonthBudget { Id = id, Amount = amount, Month = month, Year = year, IndirectBudgetId = indirectBudgetId };
        }

        public static void DelAll(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Bud_IndirectMonthBudget budget in (from m in entities.Bud_IndirectMonthBudget
                    where m.Bud_IndirectBudget.Id == id
                    select m).ToList<Bud_IndirectMonthBudget>())
                {
                    entities.DeleteObject(budget);
                }
                entities.SaveChanges();
            }
        }

        public static List<IndirectMonthBudget> GetAll(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_IndirectMonthBudget
                    where m.Bud_IndirectBudget.Id == id
                    orderby m.Year descending, m.Month descending
                    select new IndirectMonthBudget { Id = m.Id, Amount = (decimal) m.Amount, Year = m.Year, Month = m.Month, IndirectBudgetId = m.Bud_IndirectBudget.Id }).ToList<IndirectMonthBudget>();
            }
        }

        public decimal Amount { get; set; }

        public string Id { get; set; }

        public cn.justwin.stockBLL.Domain.IndirectBudget IndirectBudget
        {
            get
            {
                return cn.justwin.stockBLL.Domain.IndirectBudget.GetById(this.IndirectBudgetId);
            }
        }

        public string IndirectBudgetId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}

