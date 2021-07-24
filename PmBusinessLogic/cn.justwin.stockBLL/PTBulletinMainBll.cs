namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL.PTBULLETINMAIN;
    using System;
    using System.Collections.Generic;

    internal class PTBulletinMainBll
    {
        private PTBulletinMain pTBulletinMain = new PTBulletinMain();

        public int Add(PTBulletinMainModel model)
        {
            return this.pTBulletinMain.Add(model);
        }

        public int Delete(string I_BULLETINID)
        {
            return this.pTBulletinMain.Delete(I_BULLETINID);
        }

        public List<PTBulletinMainModel> GetListArray(string strWhere)
        {
            return this.pTBulletinMain.GetListArray(strWhere);
        }

        public PTBulletinMainModel GetModel(string I_BULLETINID)
        {
            return this.pTBulletinMain.GetModel(I_BULLETINID);
        }

        public int Update(PTBulletinMainModel model)
        {
            return this.pTBulletinMain.Update(model);
        }
    }
}

