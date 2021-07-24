namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;

    public class MaterialBack
    {
        private cn.justwin.stockDAL.MaterialBack mtBack = new cn.justwin.stockDAL.MaterialBack();

        public int add(MaterialBackModel model)
        {
            return this.mtBack.add(model);
        }

        public int delete(string rcode)
        {
            return this.mtBack.Delete(rcode);
        }

        public int delMore(string sql)
        {
            return this.mtBack.ExcuteSql(sql);
        }

        public DataTable getMaterialListBySql(string strWhere)
        {
            return this.mtBack.getMaterialListBySql(strWhere);
        }

        public MaterialBackModel getModel(string rcode)
        {
            return this.mtBack.getSmRefoundRow(rcode);
        }

        public DataTable getSmRefoundByPro(string procode)
        {
            return this.mtBack.getTableSmRefoundByPro(procode);
        }

        public DataTable search(string sql)
        {
            return this.mtBack.Search(sql);
        }

        public int update(MaterialBackModel model)
        {
            return this.mtBack.Update(model);
        }
    }
}

