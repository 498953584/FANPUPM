namespace AccountManageBLL
{
    using AccountManage.BLL;
    using AccountManage.Model;
    using cn.justwin.BLL;
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.stockBLL;
    using cn.justwin.stockBLL.AccountManage.accBaise;
    using PmBusinessLogic;
    using System;

    public class accOperMSClass : NBasePage, ISelfEvent
    {
        private PayoutPayment pm = new PayoutPayment();

        public void CommitEvent(object key)
        {
            PayoutPaymentModel model = this.pm.GetModel(key.ToString());
            fund_AccountOperateModle modle = new fund_AccountOperateModle {
                AccountNum = Guid.NewGuid().ToString(),
                Acredence = Guid.NewGuid().ToString(),
                AccounType = 1,
                AccountMony = new decimal?(Convert.ToDecimal("-" + model.PaymentMoney.ToString())),
                RealMony = new decimal?(Convert.ToDecimal("-" + model.PaymentMoney.ToString()))
            };
            PtYhmcBll bll = new PtYhmcBll();
            modle.DepID = bll.GetModelById(base.UserCode).i_bmdm.ToString();
            modle.SumitMan = base.UserCode;
            modle.SumiTime = model.InputDate;
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
            modle.AccountMark = string.Empty;
            new fund_AccountOperateBLL().Add(modle);
        }

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
        }
    }
}

