namespace cn.justwin.salaryBLL
{
    using cn.justwin.salaryDAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;

    public class SalaryTemplate
    {
        private readonly cn.justwin.salaryDAL.SalaryTemplate dal = new cn.justwin.salaryDAL.SalaryTemplate();

        public void Add(SalaryTemplateModel model)
        {
            this.dal.Add(model);
        }

        public void Delete(string TemplateID)
        {
            this.dal.Delete(TemplateID);
        }

        public List<SalaryTemplateModel> GetList()
        {
            return this.dal.GetList("");
        }

        public List<SalaryTemplateModel> GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public SalaryTemplateModel GetModel(string TemplateID)
        {
            return this.dal.GetModel(TemplateID);
        }

        public void Update(SalaryTemplateModel model)
        {
            this.dal.Update(model);
        }
    }
}

