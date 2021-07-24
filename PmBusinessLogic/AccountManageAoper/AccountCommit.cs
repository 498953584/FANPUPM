namespace AccountManageAoper
{
    using AccountManage.BLL;
    using AccountManage.DAL;
    using AccountManage.Model;
    using cn.justwin.AccountManage.prjaccount;
    using cn.justwin.BLL;
    using cn.justwin.stockBLL;
    using cn.justwin.stockBLL.AccountManage.acc_Manage;
    using cn.justwin.stockModel;
    using PmBusinessLogic;
    using System;
    using System.Data;

    public class AccountCommit : NBasePage, ISelfEvent
    {
        private readonly fund_ReqinfoBll bll = new fund_ReqinfoBll();
        private readonly fund_AccountOperateAcccess dal = new fund_AccountOperateAcccess();

        public void CommitEvent(object key)
        {
            fund_ReqinfoModle model = this.bll.GetModel(key.ToString());
            fund_AccountOperateModle modle2 = new fund_AccountOperateModle {
                Acredence = Guid.NewGuid().ToString(),
                AccounType = model.reqType,
                AccountMony = new decimal?(Convert.ToDecimal(model.amount.ToString())),
                RealMony = new decimal?(Convert.ToDecimal(model.amount.ToString()))
            };
            PtYhmc modelById = new PtYhmc();
            modelById = new PtYhmcBll().GetModelById(base.UserCode);
            modle2.DepID = modelById.i_bmdm.ToString();
            modle2.SumitMan = base.UserCode;
            modle2.SumiTime = new DateTime?(DateTime.Now);
            modle2.IsAccount = 0;
            if (model.IsContr == 0)
            {
                modle2.contracnum = model.PrjNum;
                modle2.projnum = "";
                modle2.AccountNum = this.GetConBankNum(model.PrjNum);
            }
            else
            {
                modle2.projnum = model.PrjNum;
                modle2.contracnum = "";
                modle2.AccountNum = this.GetPriBankNum(model.PrjNum);
            }
            modle2.AccountMan = string.Empty;
            modle2.AccountMark = model.remark.ToString();
            new fund_AccountOperateBLL().Add(modle2);
        }

        protected string GetConBankNum(string contenum)
        {
            new prjaccountModel();
            DataTable table = new accountMange().GetTable(" contractNum='" + contenum + "'");
            if (table.Rows.Count == 1)
            {
                return table.Rows[0]["accountID"].ToString();
            }
            return string.Empty;
        }

        protected string GetPriBankNum(string contenum)
        {
            new prjaccountModel();
            DataTable table = new accountMange().GetTable(" prjNum='" + contenum + "'");
            if (table.Rows.Count == 1)
            {
                return table.Rows[0]["accountID"].ToString();
            }
            return string.Empty;
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

