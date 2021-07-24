namespace cn.justwin.stockBLL.Fund.MonthPlan
{
    using cn.justwin.Fund.MonthPlan;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class MonthDetailLogic
    {
        private readonly MonthDetailService dal = new MonthDetailService();

        public bool Add(MonthDetailModel model)
        {
            return this.dal.Add(model);
        }

        public bool Delete(Guid UID, SqlTransaction trans)
        {
            return this.dal.Delete(UID, trans);
        }

        public bool DeleteList(string UIDlist, SqlTransaction trans)
        {
            return this.dal.DeleteList(UIDlist, trans);
        }

        public bool Exists(Guid UID)
        {
            return this.dal.Exists(UID);
        }

        public DataTable GetList(string table, string id, string rowName)
        {
            return this.dal.GetList(table, id, rowName);
        }

        public MonthDetailModel GetModel(Guid UID)
        {
            return this.dal.GetModel(UID);
        }

        public DataTable GetPlanDetailobject(string PlanType)
        {
            return this.dal.GetPlanDetailobject(PlanType);
        }

        public bool Update(MonthDetailModel model, SqlTransaction trans)
        {
            return this.dal.Update(model, trans);
        }

        public bool verificationIsPlansubject(string _MonthPlanID, string _Plansubject)
        {
            return this.dal.verificationIsPlansubject(_MonthPlanID, _Plansubject);
        }

        public bool verificationIsPrjGuid(string _MonthPlanID, string _PrjGuid)
        {
            return this.dal.verificationIsPrjGuid(_MonthPlanID, _PrjGuid);
        }
    }
}

