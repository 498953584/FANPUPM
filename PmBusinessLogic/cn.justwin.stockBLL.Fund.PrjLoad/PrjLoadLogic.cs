namespace cn.justwin.stockBLL.Fund.PrjLoad
{
    using cn.justwin.Fund.PrjLoad;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class PrjLoadLogic
    {
        private readonly PrjLoadService dal = new PrjLoadService();

        public bool Add(PrjLoadModel model)
        {
            if (model == null)
            {
                return false;
            }
            return (this.dal.Add(model) > 0);
        }

        public List<PrjLoadModel> DataTableToList(DataTable dt)
        {
            List<PrjLoadModel> list = new List<PrjLoadModel>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    PrjLoadModel item = new PrjLoadModel();
                    if ((dt.Rows[i]["LoanID"] != null) && (dt.Rows[i]["LoanID"].ToString() != ""))
                    {
                        item.LoanID = new Guid(dt.Rows[i]["LoanID"].ToString());
                    }
                    if ((dt.Rows[i]["LoanCode"] != null) && (dt.Rows[i]["LoanCode"].ToString() != ""))
                    {
                        item.LoanCode = dt.Rows[i]["LoanCode"].ToString();
                    }
                    if ((dt.Rows[i]["PrjGuid"] != null) && (dt.Rows[i]["PrjGuid"].ToString() != ""))
                    {
                        item.PrjGuid = new Guid(dt.Rows[i]["PrjGuid"].ToString());
                    }
                    if ((dt.Rows[i]["LoanDate"] != null) && (dt.Rows[i]["LoanDate"].ToString() != ""))
                    {
                        item.LoanDate = new DateTime?(DateTime.Parse(dt.Rows[i]["LoanDate"].ToString()));
                    }
                    if ((dt.Rows[i]["LoanMan"] != null) && (dt.Rows[i]["LoanMan"].ToString() != ""))
                    {
                        item.LoanMan = dt.Rows[i]["LoanMan"].ToString();
                    }
                    if ((dt.Rows[i]["LoanFund"] != null) && (dt.Rows[i]["LoanFund"].ToString() != ""))
                    {
                        item.LoanFund = new decimal?(decimal.Parse(dt.Rows[i]["LoanFund"].ToString()));
                    }
                    if ((dt.Rows[i]["LoanCause"] != null) && (dt.Rows[i]["LoanCause"].ToString() != ""))
                    {
                        item.LoanCause = dt.Rows[i]["LoanCause"].ToString();
                    }
                    if ((dt.Rows[i]["PlanReturnDate"] != null) && (dt.Rows[i]["PlanReturnDate"].ToString() != ""))
                    {
                        item.PlanReturnDate = new DateTime?(DateTime.Parse(dt.Rows[i]["PlanReturnDate"].ToString()));
                    }
                    if ((dt.Rows[i]["LoanRate"] != null) && (dt.Rows[i]["LoanRate"].ToString() != ""))
                    {
                        item.LoanRate = new decimal?(decimal.Parse(dt.Rows[i]["LoanRate"].ToString()));
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    if ((dt.Rows[i]["FlowState"] != null) && (dt.Rows[i]["FlowState"].ToString() != ""))
                    {
                        item.FlowState = new int?(int.Parse(dt.Rows[i]["FlowState"].ToString()));
                    }
                    if ((dt.Rows[i]["ReturnDate"] != null) && (dt.Rows[i]["ReturnDate"].ToString() != ""))
                    {
                        item.ReturnDate = new DateTime?(DateTime.Parse(dt.Rows[i]["ReturnDate"].ToString()));
                    }
                    if ((dt.Rows[i]["ReturnMan"] != null) && (dt.Rows[i]["ReturnMan"].ToString() != ""))
                    {
                        item.ReturnMan = dt.Rows[i]["ReturnMan"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(Guid LoanID, SqlTransaction trans)
        {
            return (!string.IsNullOrEmpty(LoanID.ToString()) && this.dal.Delete(LoanID, trans));
        }

        public bool DeleteList(string LoanIDlist)
        {
            return (!string.IsNullOrEmpty(LoanIDlist.ToString()) && this.dal.DeleteList(LoanIDlist));
        }

        public bool Exists(Guid LoanID)
        {
            return this.dal.Exists(LoanID);
        }

        public DataTable GetLoadListByPrjGuid(string _PrjGuid)
        {
            if (!string.IsNullOrEmpty(_PrjGuid.ToString()))
            {
                return this.dal.GetLoadListByPrjGuid(_PrjGuid);
            }
            return null;
        }

        public PrjLoadModel GetModel(Guid LoanID)
        {
            if (!string.IsNullOrEmpty(LoanID.ToString()))
            {
                return this.dal.GetModel(LoanID);
            }
            return null;
        }

        public List<PrjLoadModel> GetModelList(string _PrjGuid)
        {
            DataTable loadListByPrjGuid = this.dal.GetLoadListByPrjGuid(_PrjGuid);
            return this.DataTableToList(loadListByPrjGuid);
        }

        public bool Update(PrjLoadModel model)
        {
            return ((model != null) && this.dal.Update(model));
        }

        public int updateReturnState(SqlTransaction trans, Guid LoadId, int state)
        {
            return this.dal.updateReturnState(trans, LoadId, state);
        }
    }
}

