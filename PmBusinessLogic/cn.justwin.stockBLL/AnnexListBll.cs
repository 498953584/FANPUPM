namespace cn.justwin.stockBLL
{
    using System;
    using System.Collections.Generic;

    public class AnnexListBll
    {
        private AnnexList annexList = new AnnexList();

        public int Add(AnnexListModel model)
        {
            return this.annexList.Add(model);
        }

        public int Delete(Guid AnnexCode)
        {
            return this.annexList.Delete(AnnexCode);
        }

        public List<AnnexListModel> GetListArray(string strWhere)
        {
            return this.annexList.GetListArray(strWhere);
        }

        public AnnexListModel GetModel(Guid AnnexCode)
        {
            return this.annexList.GetModel(AnnexCode);
        }

        public int Update(AnnexListModel model)
        {
            return this.annexList.Update(model);
        }
    }
}

