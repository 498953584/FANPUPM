namespace cn.justwin.salaryBLL
{
    using cn.justwin.salaryDAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;

    public class Formula
    {
        private readonly cn.justwin.salaryDAL.Formula dal = new cn.justwin.salaryDAL.Formula();

        public void Add(FormulaModel model)
        {
            this.dal.Add(model);
        }

        public void Delete(string FormulaID)
        {
            this.dal.Delete(FormulaID, null);
        }

        public bool Delete(List<string> formulaIDs)
        {
            return this.dal.Delete(formulaIDs);
        }

        public List<FormulaModel> GetAllList()
        {
            return this.GetList("");
        }

        public List<FormulaModel> GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public FormulaModel GetModel(string FormulaID)
        {
            return this.dal.GetModel(FormulaID);
        }

        public void Update(FormulaModel model)
        {
            this.dal.Update(model);
        }
    }
}

