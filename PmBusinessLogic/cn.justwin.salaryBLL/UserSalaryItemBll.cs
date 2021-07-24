namespace cn.justwin.salaryBLL
{
    using cn.justwin.salaryDAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class UserSalaryItemBll
    {
        private UserSalaryItem dal = new UserSalaryItem();

        public int Add(UserSalaryItemModel model)
        {
            return this.dal.Add(null, model);
        }

        public int Add(SqlTransaction trans, UserSalaryItemModel model)
        {
            return this.dal.Add(trans, model);
        }

        public int Delete(string UserItemID)
        {
            return this.dal.Delete(UserItemID);
        }

        public int DeleteByCode(string userCode)
        {
            return this.dal.DeleteByCode(userCode);
        }

        public List<UserSalaryItemModel> GetListArray(string strWhere)
        {
            return this.dal.GetListArray(strWhere);
        }

        public UserSalaryItemModel GetModel(string UserItemID)
        {
            return this.dal.GetModel(UserItemID);
        }

        public DataTable GetTbByItmeCode(string userCode, string code)
        {
            return this.dal.GetTbByItmeCode(userCode, code);
        }

        public int Update(UserSalaryItemModel model)
        {
            return this.dal.Update(model);
        }
    }
}

