namespace cn.justwin.stockBLL.AccountManage.accBaise
{
    using cn.justwin.AccountManage;
    using cn.justwin.AccountManage.accBaise;
    using System;

    public class accBaise
    {
        private fund_baise fb = new fund_baise();

        public basieModel GetModel(int id)
        {
            return this.fb.GetModel(id);
        }

        public int upBaise(basieModel model)
        {
            return this.fb.Update(model);
        }
    }
}

