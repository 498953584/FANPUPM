namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Specialized;
    using System.Data;

    public class basicSetBLL
    {
        public static int add(basicSetModel model)
        {
            return basicSetDal.addSm_Set(model);
        }

        public static int delete(string paraid)
        {
            return basicSetDal.deleteSm_Set(paraid);
        }

        public static NameValueCollection GetBasicParameters()
        {
            NameValueCollection values = new NameValueCollection();
            foreach (DataRow row in getTable().Rows)
            {
                values.Add(row["paraname"].ToString(), row["paravalue"].ToString());
            }
            return values;
        }

        public static DataTable getTable()
        {
            return basicSetDal.getSm_Table();
        }

        public static string getValue(string paraname)
        {
            return basicSetDal.getSm_setValue(paraname);
        }

        public static int update(basicSetModel model)
        {
            return basicSetDal.updateSm_Set(model);
        }
    }
}

