namespace cn.justwin.stockBLL.AccountManage.acc_Manage
{
    using cn.justwin.AccountManage.prjaccount;
    using cn.justwin.BLL;
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class accountMange
    {
        private cn.justwin.AccountManage.prjaccount.prjaccount pa = new cn.justwin.AccountManage.prjaccount.prjaccount();

        public int Add(SqlTransaction trans, List<prjaccountModel> lstModel)
        {
            int num = 0;
            foreach (prjaccountModel model in lstModel)
            {
                this.pa.Add(trans, model);
                num++;
            }
            return num;
        }

        public int Edit(prjaccountModel model)
        {
            return this.pa.Update(model);
        }

        public DataTable GetAcclist(string selwhere, string userCode)
        {
            string str = "select f.*,c.contractCode,c.contractName,p.prjCode,p.prjName,(select v_xm from PT_yhmc where v_yhdm=createMan) as cName,(select v_xm from PT_yhmc where v_yhdm=activeMan) as aName from fund_Prjaccount as f left join Con_Payout_Contract as c on f.contractNum=c.contractid left join PT_PrjInfo as p on f.prjNum=p.prjGuid";
            return DBHelper.GetTable(str + selwhere + " order by creatData desc");
        }

        public List<PayoutContractModel> GetConNoList(string strWhere, string userCode)
        {
            return this.pa.GetConList(strWhere).FindAll(contractInfo => JsonHelper.GetListFromJson(contractInfo.UserCodes).Contains(userCode));
        }

        public List<prjaccountModel> GetContractList(string strWhere, string userCode)
        {
            return this.pa.GetAccountList(strWhere).FindAll(contractInfo => JsonHelper.GetListFromJson(contractInfo.countractModel.UserCodes).Contains(userCode));
        }

        public prjaccountModel GetModelByConId(string accountid)
        {
            return this.pa.GetModel(accountid);
        }

        public string GetPrjUserCode(string contractNum)
        {
            return this.pa.GetprjLimits(contractNum);
        }

        public string GetPrjUserCodeByprj(string prjNum)
        {
            DataTable table = DBHelper.GetTable("select podepom from PT_PrjInfo where prjGuid ='" + prjNum + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        public DataTable GetTable(string where)
        {
            return this.pa.GetList(where);
        }

        public string GetUserName(string userCodes)
        {
            return this.pa.GetAuthorizer(userCodes);
        }

        public DataTable SelContract(string selWhere)
        {
            return DBHelper.GetTable(selWhere);
        }

        public DataTable SelPrj(string selWhere)
        {
            string sql = selWhere ?? "";
            return DBHelper.GetTable(sql);
        }

        public int upActivity(SqlTransaction trans, List<prjaccountModel> lstprjaccountModel)
        {
            int num = 0;
            foreach (prjaccountModel model in lstprjaccountModel)
            {
                this.pa.upActivityDal(trans, model);
                num++;
            }
            return num;
        }

        public int upIsnullify(string isnullify)
        {
            int length = isnullify.Length;
            if (length > 0)
            {
                isnullify = isnullify.Substring(1, length - 1);
            }
            return DBHelper.ExecuteNoQuery("update dbo.fund_Prjaccount set isnullify=1 where accountID in(" + isnullify + "'') ");
        }
    }
}

