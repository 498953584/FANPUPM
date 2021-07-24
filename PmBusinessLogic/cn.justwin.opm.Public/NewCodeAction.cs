namespace cn.justwin.opm.Public
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text.RegularExpressions;

    public class NewCodeAction
    {
        public string GetBillCode(string StoreCode)
        {
            string sqlString = "";
            int num = 0;
            sqlString = "select max(substring(BillCode,len(BillCode)-3,len(BillCode))) from OPM_FinancialBill";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if ((table.Rows[0][0] != null) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToInt32(table.Rows[0][0].ToString());
            }
            num++;
            switch (num.ToString().Length)
            {
                case 1:
                    return (StoreCode + "000" + num.ToString());

                case 2:
                    return (StoreCode + "00" + num.ToString());

                case 3:
                    return (StoreCode + "0" + num.ToString());
            }
            return (StoreCode + num.ToString());
        }

        public string GetBusinessCode(string TableName, string ColName, string PrjCode, string BusinessName, string PrjID)
        {
            string str2 = "";
            int num = 0;
            DataTable table = publicDbOpClass.DataTableQuary("SELECT COUNT(PrjID) FROM OPM_Business_Data WHERE PrjID= '" + PrjID + "'");
            if ((table.Rows[0][0] != null) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            num++;
            switch (num.ToString().Length)
            {
                case 1:
                    return (str2 + PrjCode + "TZ000" + num.ToString());

                case 2:
                    return (str2 + PrjCode + "TZ00" + num.ToString());

                case 3:
                    return (str2 + PrjCode + "TZ0" + num.ToString());
            }
            return (str2 + PrjCode + "TZ" + num.ToString());
        }

        public string GetBusinessCode1(string TableName, string ColName, string PrjCode, string BusinessName, string prjid)
        {
            string sqlString = "";
            string str2 = "";
            int num = 0;
            sqlString = "select max(substring(" + ColName + ",len(" + ColName + ")-2,len(" + ColName + "))) from " + TableName;
            if (prjid != "")
            {
                sqlString = sqlString + " where prjid='" + prjid + "'";
            }
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (((table.Rows[0][0] != null) && (table.Rows[0][0].ToString() != "")) && this.isNum(table.Rows[0][0].ToString()))
            {
                num = Convert.ToInt32(table.Rows[0][0].ToString());
            }
            num++;
            switch (num.ToString().Length)
            {
                case 1:
                    return (str2 + PrjCode + BusinessName + "00" + num.ToString());

                case 2:
                    return (str2 + PrjCode + BusinessName + "0" + num.ToString());
            }
            return (str2 + PrjCode + BusinessName + num.ToString());
        }

        public string GetCode(string TableName, string ColName)
        {
            int num = 0;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select max(", ColName, ")  from ", TableName, "  where datediff(day,(select max(addtime) from ", TableName, "),'", DateTime.Now, "')='0'" }));
            if ((table.Rows[0][0] != null) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToInt16(table.Rows[0][0].ToString().Remove(0, 8));
            }
            num++;
            if (num.ToString().Length == 1)
            {
                return (DateTime.Today.ToString("yyyyMMdd") + "00" + num.ToString());
            }
            if (num.ToString().Length == 2)
            {
                return (DateTime.Today.ToString("yyyyMMdd") + "0" + num.ToString());
            }
            return (DateTime.Today.ToString("yyyyMMdd") + num.ToString());
        }

        public string GetMaintainCode(string TableName, string ColName, string StoreCode)
        {
            string str2 = "W";
            int num = 0;
            DataTable table = publicDbOpClass.DataTableQuary("select max(substring(" + ColName + ",len(" + ColName + ")-3,len(" + ColName + "))) from " + TableName);
            if ((table.Rows[0][0] != null) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToInt32(table.Rows[0][0].ToString());
            }
            num++;
            switch (num.ToString().Length)
            {
                case 1:
                    return string.Concat(new object[] { str2, StoreCode, DateTime.Now.Year, "000", num.ToString() });

                case 2:
                    return string.Concat(new object[] { str2, StoreCode, DateTime.Now.Year, "00", num.ToString() });

                case 3:
                    return string.Concat(new object[] { str2, StoreCode, DateTime.Now.Year, "0", num.ToString() });
            }
            return string.Concat(new object[] { str2, StoreCode, DateTime.Now.Year, num.ToString() });
        }

        public bool isNum(string strTemp)
        {
            Regex regex = new Regex(@"^[+-]?\d*(,\d{3})*(\.\d+)?$");
            return regex.IsMatch(strTemp);
        }
    }
}

