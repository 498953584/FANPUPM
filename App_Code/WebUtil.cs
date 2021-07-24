using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public class WebUtil
{
    public static DropDownList AddItem(DropDownList ddl, string msg)
    {
        ListItem item = new ListItem {
            Text = "--请选择" + msg + "--",
            Value = ""
        };
        ddl.Items.Insert(0, item);
        return ddl;
    }

    public static string ConvertDateTime(object date)
    {
        string str = "";
        if (date.ToString() != "")
        {
            str = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
        }
        return str;
    }

    public static string ConvertSize(string fileSize)
    {
        if (!string.IsNullOrEmpty(fileSize))
        {
            double num2 = (Convert.ToDouble(fileSize) / 1024.0) / 1024.0;
            return (num2.ToString("0.000") + " MB");
        }
        return "0.000 MB";
    }

    public static void DataBindBalanceMode(DropDownList drop)
    {
        drop.DataValueField = "NoteID";
        drop.DataTextField = "CodeName";
        drop.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=27 and IsValid = 1 and ParentCodeID=0");
        drop.DataBind();
        DropDownlistUtil.AddHintItem(drop, "结算方式");
    }

    public static void DataBindPayMode(DropDownList drop)
    {
        drop.DataValueField = "NoteID";
        drop.DataTextField = "CodeName";
        drop.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=25 and IsValid = 1 and ParentCodeID=0");
        drop.DataBind();
        DropDownlistUtil.AddHintItem(drop, "付款方式");
    }

    public static double GetBSize(string MbSize)
    {
        double num = 0.0;
        if (!string.IsNullOrEmpty(MbSize))
        {
            num = (Convert.ToDouble(MbSize) * 1024.0) * 1024.0;
        }
        return num;
    }

    public static string GetCode(string num)
    {
        return (DateTime.Now.ToString("yyyyMMddHHmmsss") + new Random().Next(100) + num);
    }

    public static string GetConState(string conState)
    {
        switch (conState)
        {
            case "0":
                return "执 行";

            case "1":
                return "中 止";

            case "2":
                return "保 内";

            case "3":
                return "保 外";

            case "4":
                return "解 除";

            case "5":
                return "终 止";
        }
        return "----";
    }

    public static string GetContrast(string Price1, string Price2)
    {
        if (string.IsNullOrEmpty(Price1) || string.IsNullOrEmpty(Price2))
        {
            return "0.000%";
        }
        if (Convert.ToDecimal(Price2) == 0M)
        {
            return "0.000%";
        }
        decimal num = (Convert.ToDecimal(Price1) / Convert.ToDecimal(Price2)) * 100M;
        return (num.ToString("0.000") + "%");
    }

    public static string GetCorpName(string corpId)
    {
        DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", "where CorpId='" + corpId + "'");
        if (table.Rows.Count > 0)
        {
            return table.Rows[0]["CorpName"].ToString();
        }
        return string.Empty;
    }

    public static string GetEnPrice(string price, string contractId)
    {
        if (string.IsNullOrEmpty(price))
        {
            return "";
        }
        decimal num = Convert.ToDecimal(price);
        foreach (DataRow row in Common2.GetTable("Con_Incomet_Modify", " where contractId='" + contractId + "'").Rows)
        {
            num += Convert.ToDecimal(row["ChangePrice"].ToString());
        }
        return num.ToString("0.000");
    }

    public static string GetFeedBackState(string state)
    {
        switch (state)
        {
            case "0":
                return "<span style='color:#050505;'>未打开</span>";

            case "1":
                return "<span style='color:#030310;'>已打开(未反馈)</span>";

            case "2":
                return "<span style='color:#ff0000;'>暂存</span>";

            case "3":
                return "<span style='color:#008B45;'>已反馈</span>";
        }
        return "<span style='color:#050505;'>未打开</span>";
    }

    public static string GetImg(string imgUrl)
    {
        if (!string.IsNullOrEmpty(imgUrl))
        {
            return ("<a href='" + imgUrl + "' Target='_blank'><img src='/images1/31.gif' border='0'/></a>");
        }
        return "";
    }

    public static string GetImgUrl(string isValid)
    {
        if (isValid == "1")
        {
            return "/EPC/imgs/ok.png";
        }
        return "/EPC/imgs/empty.jpg";
    }

    public static string GetInvoiceContrast(string CllectionPrice, string InvoicePrice)
    {
        string str = "0.00%";
        if (string.IsNullOrEmpty(CllectionPrice) || string.IsNullOrEmpty(InvoicePrice))
        {
            return "0.00%";
        }
        if (Convert.ToDecimal(CllectionPrice) != 0M)
        {
            str = (((Convert.ToDecimal(InvoicePrice) / Convert.ToDecimal(CllectionPrice)) * 100M)).ToString("0.00") + "%";
        }
        return str;
    }

    public static string GetPaymentSum(string contractId, string tbName, string columns)
    {
        double num = 0.0;
        foreach (DataRow row in Common2.GetTable(tbName, " where contractId='" + contractId + "'").Rows)
        {
            num += Convert.ToDouble(row[columns].ToString());
        }
        return num.ToString("0.000");
    }

    public static string GetPrjNameByproject(object project)
    {
        string cmdText = string.Empty;
        if (project != null)
        {
            cmdText = "SELECT PrjName FROM PT_PrjInfo WHERE PrjGuid='" + project.ToString() + "'";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["PrjName"].ToString();
            }
        }
        return string.Empty;
    }

    public static string GetQuestionState(int state)
    {
        switch (state)
        {
            case 0:
                return "<span style='color:#ff0000;'>未解决</span>";

            case 1:
                return "<span style='color:##008845;'>解决中</span>";

            case 2:
                return "<span style='color:#050505;'>已解决</span>";

            case 3:
                return "<span style='color:#008B45;'>已解决并验证</span>";
        }
        return "<span style='color:#050505;'>未打开</span>";
    }

    public static decimal GetStockNumberByCode(string code)
    {
        string contrastTreasuryCode = new cn.justwin.stockBLL.Treasury().GetContrastTreasuryCode();
        TreasuryStock stock = new TreasuryStock();
        if (!string.IsNullOrEmpty(contrastTreasuryCode))
        {
            return stock.GetNumber(code, contrastTreasuryCode);
        }
        return stock.GetNumber(code);
    }

    public static string GetUnitNameByResId(string resourceId)
    {
        string cmdText = "Select Name from Res_Unit join Res_Resource on Res_Resource.Unit=Res_Unit.UnitId and Res_Resource.ResourceId='" + resourceId + "'";
        DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        if (table.Rows.Count > 0)
        {
            return table.Rows[0]["Name"].ToString();
        }
        return string.Empty;
    }

    public static string GetUserNames(string userCodes)
    {
        List<string> listFromJson = JsonHelper.GetListFromJson(userCodes);
        PtYhmcBll bll = new PtYhmcBll();
        return StringUtility.GetUserNames(bll.GetNames(listFromJson));
    }

    public static string GetUserNamesComma(string userCodes)
    {
        List<string> codes = new List<string>();
        if (userCodes != null)
        {
            if (userCodes.ToString().Contains("["))
            {
                codes = JsonHelper.GetListFromJson(userCodes);
            }
            else
            {
                foreach (string str in userCodes.Trim(new char[] { ',' }).Split(new char[] { ',' }))
                {
                    codes.Add(str);
                }
            }
        }
        PtYhmcBll bll = new PtYhmcBll();
        return StringUtility.GetUserNames(bll.GetNames(codes));
    }

    public static string SapRealContractMoney(string contractCode, string type)
    {
        decimal num = 0M;
        if (!string.IsNullOrEmpty(contractCode) && !string.IsNullOrEmpty(type))
        {
            foreach (DataRow row in Common2.GetTable("MiddleTable", "WHERE Tag='" + type + "' AND ContractCode='" + contractCode + "'").Rows)
            {
                decimal num2 = (row["MoneyAmount"] != null) ? Convert.ToDecimal(row["MoneyAmount"]) : 0M;
                num += num2;
            }
        }
        return string.Format("{0:f3}", num);
    }

    public static string SapRealCostDiary(object serialnumber, string flowSate)
    {
        string str2 = (serialnumber == null) ? string.Empty : serialnumber.ToString();
        if (string.IsNullOrEmpty(str2))
        {
            return string.Empty;
        }
        decimal num = 0M;
        if (flowSate == "1")
        {
            foreach (DataRow row in Common2.GetTable("MiddleTable", "WHERE SerialNumber='" + str2 + "'").Rows)
            {
                num += (row["MoneyAmount"] != null) ? Convert.ToDecimal(row["MoneyAmount"]) : 0M;
            }
        }
        return ((num == 0M) ? string.Empty : string.Format("{0:f3}", num));
    }

    public static string SapRealPayMoney(object serialnumber)
    {
        string str = string.Empty;
        string str2 = (serialnumber == null) ? string.Empty : serialnumber.ToString();
        if (string.IsNullOrEmpty(str2))
        {
            return string.Empty;
        }
        foreach (DataRow row in Common2.GetTable("MiddleTable", "WHERE SerialNumber='" + str2 + "'").Rows)
        {
            str = (row["MoneyAmount"] != null) ? row["MoneyAmount"].ToString() : string.Empty;
        }
        return string.Format("{0:f3}", str);
    }

    public static string SetUrl(string paramName, string paramValue)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("if (window.location.href.indexOf('" + paramName + "') == -1) {").AppendLine();
        builder.Append("    window.location.href+='&" + paramName + "=" + paramValue + "';").AppendLine();
        builder.Append("} else {").AppendLine();
        builder.Append("    var index = window.location.href.indexOf('" + paramName + "');").AppendLine();
        builder.Append("    var newUrl = window.location.href.substring(0,index);").AppendLine();
        builder.Append("    newUrl += '" + paramName + "=" + paramValue + "';").AppendLine();
        builder.Append("    window.location.href = newUrl;").AppendLine();
        builder.Append("}").AppendLine();
        return builder.ToString();
    }

    public static DataTable UpdateDataTable(DataTable dt, string type, string partyColumns)
    {
        DataColumn column = new DataColumn {
            ColumnName = "百分比"
        };
        dt.Columns.Add(column);
        dt.Columns.Remove(dt.Columns["Date"]);
        if (type == "Contrast")
        {
            column.ColumnName = "合同结算比率";
            dt.Columns.Add(new DataColumn("合同收款比率"));
            dt.Columns.Add(new DataColumn("收款结算比率"));
            dt.Columns.Add(new DataColumn("已开发票收款比率"));
        }
        else if (type == "Invoice")
        {
            column.ColumnName = "已开发票收款比率";
        }
        else if (type == "BalancePrice")
        {
            column.ColumnName = "合同结算比率";
        }
        else if (type == "CllectionPrice")
        {
            column.ColumnName = "合同收款比率";
        }
        foreach (DataRow row in dt.Rows)
        {
            row[partyColumns] = row[partyColumns].ToString().Split(new char[] { ',' })[1];
            if ((type == "BalancePrice") || (type == "CllectionPrice"))
            {
                row[column.ColumnName] = GetContrast(row[type].ToString(), row["EndPrice"].ToString());
            }
            else if (type == "Invoice")
            {
                row[column.ColumnName] = GetInvoiceContrast(GetPaymentSum(row["ContractID"].ToString(), "Con_Incomet_Payment", "CllectionPrice"), row["InvoicePrice"].ToString());
            }
            else if (type == "Contrast")
            {
                row[column.ColumnName] = GetContrast(row["BalancePrice"].ToString(), row["EndPrice"].ToString());
                row["合同收款比率"] = GetContrast(row["PaymentPrice"].ToString(), row["EndPrice"].ToString());
                row["收款结算比率"] = GetContrast(row["PaymentPrice"].ToString(), row["BalancePrice"].ToString());
                row["已开发票收款比率"] = GetInvoiceContrast(GetPaymentSum(row["ContractID"].ToString(), "Con_Incomet_Payment", "CllectionPrice"), row["InvoicePrice"].ToString());
            }
        }
        return dt;
    }
}

