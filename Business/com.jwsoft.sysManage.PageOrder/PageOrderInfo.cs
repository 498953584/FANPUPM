namespace com.jwsoft.sysManage.PageOrder
{
    using System;
    using System.Collections;
    using System.Text;

    public class PageOrderInfo : CollectionBase
    {
        private string mainfiled;
        private int mainorder;

        public void Add(string DbFiled, int OrderType)
        {
            if ((DbFiled != null) & (DbFiled != ""))
            {
                string str = (OrderType != 0) ? " desc ," : " asc ,";
                base.List.Add(DbFiled + str);
                if (base.Count == 1)
                {
                    this.mainfiled = DbFiled;
                    this.mainorder = OrderType;
                }
            }
        }

        public int GetMainFiledOrderType()
        {
            return this.mainorder;
        }

        public string GetMainOrderField()
        {
            return this.mainfiled;
        }

        public string GetOrderString()
        {
            if (base.Count <= 0)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < base.Count; i++)
            {
                builder.Append((string) base.List[i]);
            }
            string str = builder.ToString();
            return str.Substring(0, str.Length - 1);
        }
    }
}

