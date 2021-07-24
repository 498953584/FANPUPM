namespace AccountManage.BLL
{
    using AccountManage.DAL;
    using AccountManage.Model;
    using cn.justwin.BLL;
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.stockBLL;
    using cn.justwin.stockBLL.AccountManage.accBaise;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class fund_AccountOperateBLL : NBasePage
    {
        private readonly fund_AccountOperateAcccess dal = new fund_AccountOperateAcccess();

        public int Add(fund_AccountOperateModle model)
        {
            return this.dal.Add(model);
        }

        public int addIncomePayment(SqlTransaction trans, string id)
        {
            IncometPaymentModel model = new IncometPayment().GetModel(id);
            fund_AccountOperateModle modle = new fund_AccountOperateModle {
                AccountNum = Guid.NewGuid().ToString(),
                Acredence = Guid.NewGuid().ToString(),
                AccounType = 1,
                AccountMony = model.CllectionPrice,
                RealMony = model.CllectionPrice
            };
            PtYhmcBll bll = new PtYhmcBll();
            modle.DepID = bll.GetModelById(base.UserCode).i_bmdm.ToString();
            modle.SumitMan = base.UserCode;
            modle.SumiTime = model.CllectionTime;
            modle.IsAccount = 0;
            modle.contracnum = model.ContractID;
            modle.projnum = "";
            cn.justwin.stockBLL.AccountManage.accBaise.accBaise baise = new cn.justwin.stockBLL.AccountManage.accBaise.accBaise();
            if (baise.GetModel(1).accAllot == 0)
            {
                PayoutContractModel model3 = new PayoutContract().GetModel(model.ContractID);
                modle.projnum = model3.PrjGuid;
                modle.contracnum = "";
            }
            modle.AccountMan = string.Empty;
            return this.dal.AddList(trans, modle);
        }

        public List<fund_AccountOperateModle> DataTableToList(DataTable dt)
        {
            List<fund_AccountOperateModle> list = new List<fund_AccountOperateModle>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    fund_AccountOperateModle item = new fund_AccountOperateModle();
                    if (dt.Rows[i]["AoID"].ToString() != "")
                    {
                        item.AoID = int.Parse(dt.Rows[i]["AoID"].ToString());
                    }
                    item.AccountNum = dt.Rows[i]["AccountNum"].ToString();
                    item.Acredence = dt.Rows[i]["Acredence"].ToString();
                    if (dt.Rows[i]["AccounType"].ToString() != "")
                    {
                        item.AccounType = new int?(int.Parse(dt.Rows[i]["AccounType"].ToString()));
                    }
                    if (dt.Rows[i]["AccountMony"].ToString() != "")
                    {
                        item.AccountMony = new decimal?(decimal.Parse(dt.Rows[i]["AccountMony"].ToString()));
                    }
                    if (dt.Rows[i]["RealMony"].ToString() != "")
                    {
                        item.RealMony = new decimal?(decimal.Parse(dt.Rows[i]["RealMony"].ToString()));
                    }
                    item.DepID = dt.Rows[i]["DepID"].ToString();
                    item.SumitMan = dt.Rows[i]["SumitMan"].ToString();
                    if (dt.Rows[i]["SumiTime"].ToString() != "")
                    {
                        item.SumiTime = new DateTime?(DateTime.Parse(dt.Rows[i]["SumiTime"].ToString()));
                    }
                    if (dt.Rows[i]["IsAccount"].ToString() != "")
                    {
                        item.IsAccount = new int?(int.Parse(dt.Rows[i]["IsAccount"].ToString()));
                    }
                    if (dt.Rows[i]["AccounTime"].ToString() != "")
                    {
                        item.AccounTime = new DateTime?(DateTime.Parse(dt.Rows[i]["AccounTime"].ToString()));
                    }
                    item.AccountMan = dt.Rows[i]["AccountMan"].ToString();
                    item.projnum = dt.Rows[i]["projnum"].ToString();
                    item.contracnum = dt.Rows[i]["contracnum"].ToString();
                    item.AccountMark = dt.Rows[i]["AccountMark"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int AoID)
        {
            return this.dal.Delete(AoID);
        }

        public bool Exists(string Acredence)
        {
            return this.dal.Exists(Acredence);
        }

        public DataTable GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public fund_AccountOperateModle GetModel(int AoID)
        {
            return this.dal.GetModel(AoID);
        }

        public List<fund_AccountOperateModle> GetModelList(string strWhere)
        {
            DataTable list = this.dal.GetList(strWhere);
            return this.DataTableToList(list);
        }

        public bool Update(fund_AccountOperateModle model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateBoth(string usercode, DateTime nowtime, decimal monye, int id)
        {
            return this.dal.UpdateBoth(usercode, nowtime, monye, id);
        }
    }
}

