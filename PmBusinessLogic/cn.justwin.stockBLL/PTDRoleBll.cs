namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;

    public class PTDRoleBll
    {
        private PTDRoleService pTDRoleService = new PTDRoleService();

        public int Add(PTDRole model)
        {
            return this.pTDRoleService.Add(model);
        }

        public int Delete(string RoleTypeCode)
        {
            return this.pTDRoleService.Delete(RoleTypeCode);
        }

        public IList<PTDRole> GetListArray(string strWhere)
        {
            return this.pTDRoleService.GetListArray(strWhere);
        }

        public PTDRole GetModel(string RoleTypeCode)
        {
            return this.pTDRoleService.GetModel(RoleTypeCode);
        }

        public int Update(PTDRole model)
        {
            return this.pTDRoleService.Update(model);
        }
    }
}

