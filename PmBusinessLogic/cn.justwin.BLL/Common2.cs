namespace cn.justwin.BLL
{
    using cn.justwin.contractBLL;
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.stockBll;
    using cn.justwin.stockBLL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Common2
    {
        private static AlarmNumBll alarmNumBll = new AlarmNumBll();
        private static IncometContractBll incometContractBll = new IncometContractBll();
        private static PTBulletinMainBll pTBulletinMainBll = new PTBulletinMainBll();
        private static PTDbsjBll pTDbsjBll = new PTDbsjBll();
        private static PtYhmcBll ptYhmcBll = new PtYhmcBll();
        private static TreasuryPermitBll treasuryPermitBll = new TreasuryPermitBll();
        private static TreasuryStockBll treasuryStockBll = new TreasuryStockBll();

        public static void AddAlarm()
        {
            AddContractAlarm();
        }

        private static void AddContractAlarm()
        {
            AddPayoutContractAlarm();
            IncometContractAlarm();
        }

        private static void AddPayoutContractAlarm()
        {
            ConConfigContractService service = new ConConfigContractService();
            PayoutPayment payment = new PayoutPayment();
            using (List<PayoutPaymentModel>.Enumerator enumerator = payment.GetList().GetEnumerator())
            {
                PayoutPaymentModel item;
                while (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    if (!pTDbsjBll.Exists(item.ID))
                    {
                        int? payoutAlarmDays = (from p in service
                            where p.ContractId == item.ContractID
                            select p).FirstOrDefault<ConConfigContract>().PayoutAlarmDays;
                        PTDbsjModel model = new PTDbsjModel {
                            C_OpenFlag = "0",
                            DTM_DBSJ = DateTime.Now,
                            I_XGID = item.ID
                        };
                        StringBuilder builder = new StringBuilder();
                        builder.AppendFormat("付款编号为{0}", item.PaymentCode);
                        builder.Append("的合同需要在");
                        builder.AppendFormat("{0}日", payoutAlarmDays);
                        builder.Append("内付款");
                        model.V_Content = builder.ToString();
                        model.V_DBLJ = "StockManage/basicset/ShowView.aspx?i=" + item.ID;
                        model.V_LXBM = "021";
                        model.V_TPLJ = "new_Mail.gif";
                        model.V_YHDM = "00000000";
                        pTDbsjBll.Add(model);
                    }
                }
            }
        }

        public static void AlarmMethod(string tcode, string scode)
        {
            string str = DateTime.Now.ToString("MMddHHmmsss");
            if (StockParameter.IsLowAlarm)
            {
                string str2 = treasuryStockBll.AlarmMethod(tcode, scode).ToString();
                if (string.IsNullOrEmpty(str2))
                {
                    str2 = "0";
                }
                string str3 = alarmNumBll.AlarmMethod(tcode, scode).ToString();
                TreasuryModel model = new Treasury().GetModel(tcode);
                if ((!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3)) && (Convert.ToDecimal(str2) <= Convert.ToDecimal(str3)))
                {
                    foreach (TreasuryPermit permit in treasuryPermitBll.GetAllTreasuryPermitByWhere(" where tcode='" + tcode + "' and ptype='Person'"))
                    {
                        if (permit.pcode != "")
                        {
                            PTDbsjModel model2 = new PTDbsjModel {
                                C_OpenFlag = "0",
                                DTM_DBSJ = DateTime.Now,
                                I_XGID = str,
                                V_Content = model.tname + "中的" + scode + "库存量不足,最低库存量不得少于" + str3,
                                V_DBLJ = "StockManage/basicset/ShowView.aspx?gid=" + str,
                                V_LXBM = "021",
                                V_TPLJ = "new_Mail.gif",
                                V_YHDM = permit.pcode
                            };
                            pTDbsjBll.Add(model2);
                        }
                    }
                }
            }
        }

        public static void BindGvTable(DataTable storageDataSource, GridView gv, bool showFister)
        {
            if (storageDataSource.Rows.Count == 0)
            {
                storageDataSource.Rows.Add(storageDataSource.NewRow());
                gv.DataSource = storageDataSource;
                gv.DataBind();
                if (!showFister)
                {
                    gv.HeaderRow.Cells[0].Visible = false;
                }
                gv.Rows[0].Visible = false;
            }
            else
            {
                gv.DataSource = storageDataSource;
                gv.DataBind();
            }
        }

        public static DateTime? ConverToDateTime(string dateStr)
        {
            DateTime? nullable = null;
            if (string.IsNullOrEmpty(dateStr) || !(dateStr.Trim() != ""))
            {
                return nullable;
            }
            try
            {
                return new DateTime?(Convert.ToDateTime(dateStr));
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ConvertToDecimal(string str)
        {
            decimal? nullable = null;
            try
            {
                nullable = new decimal?(Convert.ToDecimal(str.Trim()));
            }
            catch
            {
            }
            return nullable;
        }

        public static int DelByStrWhere(string tbName, string strWhere)
        {
            return DBHelper.DelByStrWhere(tbName, strWhere);
        }

        public static int ExecuteNoQuery(string sql)
        {
            return DBHelper.ExecuteNoQuery(sql);
        }

        public void ExportToExecelAll(DataTable dtData, string FileName, string browser)
        {
            new ExcelExporter().Export(dtData, FileName, browser);
        }

        public void ExportToWordAll(DataTable dtData, string FileName)
        {
            DataGrid grid = null;
            HttpContext current = HttpContext.Current;
            StringWriter writer = null;
            HtmlTextWriter writer2 = null;
            current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
            current.Response.Charset = "utf-8";
            current.Response.HeaderEncoding = Encoding.GetEncoding("GB2312");
            if (dtData.Rows.Count == 0)
            {
                current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            }
            writer = new StringWriter();
            writer2 = new HtmlTextWriter(writer);
            grid = new DataGrid {
                DataSource = dtData.DefaultView,
                AllowPaging = false
            };
            grid.DataBind();
            grid.RenderControl(writer2);
            current.Response.Write(writer.ToString());
            current.Response.End();
        }

        public void ExportToWordAll(DataTable dtData, string FileName, string header)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(FileName));
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding("GB2312");
            if (dtData.Rows.Count == 0)
            {
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            }
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
            CultureInfo formatProvider = new CultureInfo("ZH-CN", true);
            StringWriter writer = new StringWriter(formatProvider);
            new HtmlTextWriter(writer);
            StringBuilder builder = new StringBuilder("<table>");
            builder.Append(header);
            foreach (DataRow row in dtData.Rows)
            {
                builder.Append("<tr>");
                for (int i = 0; i < dtData.Columns.Count; i++)
                {
                    builder.Append("<td style='text-align: center'>");
                    builder.Append(row[i].ToString());
                    builder.Append("</td>");
                }
                builder.Append("</td>");
            }
            builder.Append("</table>");
            HttpContext.Current.Response.Write(builder.ToString());
            HttpContext.Current.Response.End();
        }

        public static string FormatDecimal(object obj)
        {
            if ((obj != null) && (obj != DBNull.Value))
            {
                return string.Format("{0:F3}", obj);
            }
            return "0.000";
        }

        public static string FormatRate(object obj)
        {
            if (obj == DBNull.Value)
            {
                return "0.000";
            }
            return string.Format("{0:P}", Convert.ToDecimal(obj));
        }

        public static string GetAcceptState(string state)
        {
            if (!string.IsNullOrEmpty(state) && (state == "0"))
            {
                return "否";
            }
            return "是";
        }

        public static List<string> GetAuditUserByGuid(string guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Operater ");
            builder.Append("FROM WF_TemplateNode AS t ");
            builder.Append("JOIN WF_Instance_Main AS i ON t.TemplateID = i.TemplateID ");
            builder.AppendFormat("WHERE InstanceCode = '{0}'", guid);
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
            List<string> list = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(row["Operater"].ToString());
            }
            return list;
        }

        public static Color GetColorByLevel(string level)
        {
            Color black = Color.Black;
            if (level == "高")
            {
                return Parameters.HighColor;
            }
            if (level == "中")
            {
                return Parameters.MidColor;
            }
            if (level == "低")
            {
                black = Parameters.LowColor;
            }
            return black;
        }

        public static Color GetColorByState(string state)
        {
            if (state == "高")
            {
                return Parameters.HighColor;
            }
            if (state == "中")
            {
                return Parameters.MidColor;
            }
            if (state == "低")
            {
                return Parameters.LowColor;
            }
            return Color.Black;
        }

        public static string GetContractName(string contractId)
        {
            IncometContractModel model = incometContractBll.GetModel(contractId);
            if (model == null)
            {
                return "";
            }
            if (!(model.FCode == "0"))
            {
                return ("<img src='/images/tree/i.gif' style='vertical-align:middle;' /><img src='/images/tree/l.gif' style='vertical-align:middle;' />" + model.ContractName);
            }
            if (incometContractBll.GetListArray(" where fcode='" + contractId + "'").Count > 0)
            {
                return ("<img src='/images/tree/tminus.gif' style='vertical-align:middle;' />" + model.ContractName);
            }
            return ("<img src='/images/tree/t.gif' style='vertical-align:middle;' />" + model.ContractName);
        }

        public static string GetDepartmentById(object id)
        {
            string str = string.Empty;
            PTdbm byId = new PTdbmService().GetById(id);
            if (byId != null)
            {
                str = byId.V_BMMC;
            }
            return str;
        }

        public static DateTime GetEndDate(TextBox txtEndDate)
        {
            DateTime minValue = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                return new DateTime(0x81f, 6, 6);
            }
            try
            {
                minValue = Convert.ToDateTime(txtEndDate.Text.Trim());
            }
            catch (FormatException)
            {
            }
            return minValue;
        }

        public static DataTable GetFieldAndRemark(string tableName)
        {
            return DBHelper.GetRemark(tableName);
        }

        public static string GetIndirectState(string state)
        {
            string str = "";
            string str2 = state;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 == "2")
                    {
                        return "<span style='color:#008B45;' state=2>已审核</span>";
                    }
                    if (str2 == "3")
                    {
                        return "<span style='color:#ff0000;' state=3>取消上报</span>";
                    }
                    if (str2 != "4")
                    {
                        return str;
                    }
                    return "<span style='color:#914229;' state=4>取消审核</span>";
                }
            }
            else
            {
                return "<span style='color:#050505;' state=0>未上报</span>";
            }
            return "<span style='color:#030310;' state=1>上报中</span>";
        }

        public static string GetPrjState(string state, bool isProject)
        {
            string str = "";
            if (!isProject)
            {
                string str2 = state;
                if (str2 == null)
                {
                    return str;
                }
                if (!(str2 == "1"))
                {
                    if (str2 != "2")
                    {
                        if (str2 == "3")
                        {
                            return "<span  state='3'>启动</span>";
                        }
                        if (str2 == "4")
                        {
                            return "<span  state='4'>投标</span>";
                        }
                        if (str2 == "5")
                        {
                            return "<span  state='5'>中标</span>";
                        }
                        if (str2 != "6")
                        {
                            return str;
                        }
                        return "<span  state='6'>落标</span>";
                    }
                }
                else
                {
                    return "<span  state='1'>信息预立项</span>";
                }
                return "<span  state='2'>信息立项</span>";
            }
            switch (state)
            {
                case "5":
                    return "<span  state='5'>中标</span>";

                case "7":
                    return "<span  state='7'>在建</span>";

                case "8":
                    return "<span  state='8'>停工</span>";

                case "9":
                    return "<span  state='9'>验收</span>";

                case "10":
                    return "<span  state='10'>竣工</span>";

                case "11":
                    return "<span  state='11'>保内</span>";

                case "12":
                    return "<span  state='12'>保外</span>";

                case "13":
                    return "<span  state='13'>解除</span>";
            }
            return str;
        }

        public static string GetProjectState(string state)
        {
            StringBuilder builder = new StringBuilder();
            if ((state == "1") || string.IsNullOrEmpty(state))
            {
                builder.Append("信息预立项");
            }
            else if (state == "2")
            {
                builder.Append("信息立项");
            }
            else if (state == "3")
            {
                builder.Append("启动");
            }
            else if (state == "4")
            {
                builder.Append("投标");
            }
            else if (state == "5")
            {
                builder.Append("中标");
            }
            else if (state == "6")
            {
                builder.Append("落标");
            }
            else if (state == "7")
            {
                builder.Append("在建");
            }
            else if (state == "8")
            {
                builder.Append("停工");
            }
            else if (state == "9")
            {
                builder.Append("验收");
            }
            else if (state == "10")
            {
                builder.Append("竣工");
            }
            else if (state == "11")
            {
                builder.Append("保内");
            }
            else if (state == "12")
            {
                builder.Append("保外");
            }
            else if (state == "13")
            {
                builder.Append("解除");
            }
            return builder.ToString();
        }

        public static string GetRepairType(object repairType)
        {
            string str = string.Empty;
            string str2 = repairType.ToString();
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    return str;
                }
            }
            else
            {
                return "自行维修";
            }
            return "委外维修";
        }

        public static string GetRmFlag(object flag)
        {
            string str = string.Empty;
            string str2 = flag.ToString();
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "R"))
            {
                if (str2 != "M")
                {
                    return str;
                }
            }
            else
            {
                return "维修";
            }
            return "保养";
        }

        public static DateTime GetStartDate(TextBox txtStartDate)
        {
            DateTime minValue = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
            {
                return new DateTime(0x76c, 1, 1);
            }
            try
            {
                minValue = Convert.ToDateTime(txtStartDate.Text.Trim());
            }
            catch (FormatException)
            {
            }
            return minValue;
        }

        public static string GetState(string state)
        {
            switch (state)
            {
                case "-1":
                    return "<span style='color:#050505;' state='-1'>未提交</span>";

                case "0":
                    return "<span style='color:#030310;' state='0'>审核中</span>";

                case "1":
                    return "<span style='color:#008B45;' state='1'>已审核</span>";

                case "-2":
                    return "<span style='color:#ff0000;' state='-2'>驳回</span>";

                case "-3":
                    return "<span style='color:#914229;' state='-3'>重报</span>";
            }
            return "<span style='color:#050505;'>未审核</span>";
        }

        public static string GetState(string state, string primit)
        {
            string str2 = string.Empty;
            switch (state)
            {
                case "-1":
                    if (primit == "1")
                    {
                        str2 = "state='-1'";
                    }
                    return ("<span style='color:#050505;' " + str2 + ">未提交</span>");

                case "0":
                    if (primit == "1")
                    {
                        str2 = "state='0'";
                    }
                    return ("<span style='color:#030310;' " + str2 + ">审核中</span>");

                case "1":
                    if (primit == "1")
                    {
                        str2 = "state='1'";
                    }
                    return ("<span style='color:#008B45;' " + str2 + ">已审核</span>");

                case "-2":
                    if (primit == "1")
                    {
                        str2 = "state='-2'";
                    }
                    return ("<span style='color:#ff0000;' " + str2 + ">驳回</span>");

                case "-3":
                    if (primit == "1")
                    {
                        str2 = "state='-3'";
                    }
                    return ("<span style='color:#914229;' " + str2 + ">重报</span>");
            }
            return "<span style='color:#050505;'>未审核</span>";
        }

        public static string GetStateNoColor(string state)
        {
            switch (state)
            {
                case "-1":
                    return "未提交";

                case "0":
                    return "审核中";

                case "1":
                    return "已审核";

                case "-2":
                    return "驳回";

                case "-3":
                    return "重报";
            }
            return "未审核";
        }

        public static string GetStateNullString(string state)
        {
            switch (state)
            {
                case "-1":
                    return "<span style='color:#050505;' state='-1'>未提交</span>";

                case "0":
                    return "<span style='color:#030310;' state='0'>审核中</span>";

                case "1":
                    return "<span style='color:#008B45;' state='1'>已审核</span>";

                case "-2":
                    return "<span style='color:#ff0000;' state='-2'>驳回</span>";

                case "-3":
                    return "<span style='color:#914229;' state='-3'>重报</span>";
            }
            return "";
        }

        public static DataTable GetTable(string sql)
        {
            return DBHelper.GetTable(sql);
        }

        public static DataTable GetTable(string tbName, string Condition)
        {
            return DBHelper.GetTable(tbName, Condition);
        }

        public static string GetTaskTypeName(string orderNumber)
        {
            try
            {
                int num = (orderNumber.Length / 3) - 1;
                XpmCodeServices services = new XpmCodeServices();
                return services.GetBySignCode(XpmCodeTypeParam.TaskType)[num].CodeName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetTime(object time)
        {
            if ((time != null) && (time.ToString() != ""))
            {
                return Convert.ToDateTime(time).ToString("yyyy-MM-dd");
            }
            return "";
        }

        public static string GetUrlBySpId(string spId)
        {
            switch (spId)
            {
                case "spModify":
                    return "/ContractManage/IncometModify/ShowModifyList.aspx";

                case "spBalance":
                    return "/ContractManage/IncometBalance/ShowBalanceList.aspx";

                case "spPayment":
                    return "/ContractManage/IncometPayment/ShowPaymentList.aspx";

                case "spPaymentApply":
                    return "/ContractManage/PaymentApply/ShowPaymentApplyList.aspx";

                case "spInvoice":
                    return "/ContractManage/IncometInvoice/ShowInvoiceList.aspx";

                case "spPayoutModify":
                    return "/ContractManage/PayoutModify/PayoutModifyEdit.aspx";

                case "spPayoutBalance":
                    return "/ContractManage/PayoutBalance/PayoutBalanceEdit.aspx";

                case "spPayoutPayment":
                    return "/ContractManage/PayoutPayment/PayoutPaymentEdit.aspx";

                case "spPayoutInvoice":
                    return "/ContractManage/PayoutInvoice/PayoutInvoiceEdit.aspx";
            }
            return "/ContractManage/IncometModify/ViewModifyList.aspx";
        }

        private void gvwExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                }
            }
        }

        private static void IncometContractAlarm()
        {
            ConConfigContractService service = new ConConfigContractService();
            IncometPaymentBll bll = new IncometPaymentBll();
            using (List<IncometPaymentModel>.Enumerator enumerator = bll.GetListByWhere(string.Empty).GetEnumerator())
            {
                IncometPaymentModel item;
                while (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    if (!pTDbsjBll.Exists(item.ID))
                    {
                        int? incomeAlarmDays = (from p in service
                            where p.ContractId == item.ContractID
                            select p).FirstOrDefault<ConConfigContract>().IncomeAlarmDays;
                        PTDbsjModel model = new PTDbsjModel {
                            C_OpenFlag = "0",
                            DTM_DBSJ = DateTime.Now,
                            I_XGID = item.ID
                        };
                        StringBuilder builder = new StringBuilder();
                        builder.AppendFormat("收款编号为{0}", item.CllectionCode);
                        builder.Append("的合同需要在");
                        builder.Append(incomeAlarmDays);
                        builder.Append("日内付款");
                        model.V_Content = builder.ToString();
                        model.V_DBLJ = "StockManage/basicset/ShowView.aspx?i=" + item.ID;
                        model.V_LXBM = "021";
                        model.V_TPLJ = "new_Mail.gif";
                        model.V_YHDM = "00000000";
                        pTDbsjBll.Add(model);
                    }
                }
            }
        }

        public static DataTable UniteDataTable(DataTable dt1, DataTable dt2)
        {
            if (((dt1.Rows.Count > 0) && (dt1.Rows[0][0].ToString() == "")) && ((dt1.Rows[0][1].ToString() == "") && (dt1.Rows[0][2].ToString() == "")))
            {
                dt1.Rows.Remove(dt1.Rows[0]);
            }
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int num2 = 0; num2 < dt2.Rows.Count; num2++)
                {
                    if (((dt1.Rows.Count > 0) && (dt2.Rows.Count > 0)) && (dt1.Rows[i]["scode"].ToString() == dt2.Rows[num2]["scode"].ToString()))
                    {
                        dt1.Rows.Remove(dt1.Rows[i]);
                    }
                }
            }
            DataTable table = dt1.Clone();
            for (int j = 0; j < dt2.Columns.Count; j++)
            {
                if (!table.Columns.Contains(dt2.Columns[j].ColumnName))
                {
                    table.Columns.Add(dt2.Columns[j].ColumnName);
                }
            }
            object[] array = new object[table.Columns.Count];
            for (int k = 0; k < dt1.Rows.Count; k++)
            {
                dt1.Rows[k].ItemArray.CopyTo(array, 0);
                table.Rows.Add(array);
            }
            for (int m = 0; m < dt2.Rows.Count; m++)
            {
                DataRow row = table.NewRow();
                table.Rows.Add(row);
            }
            for (int n = 0; n < dt2.Rows.Count; n++)
            {
                for (int num7 = 0; num7 < dt2.Columns.Count; num7++)
                {
                    for (int num8 = 0; num8 < table.Columns.Count; num8++)
                    {
                        if (dt2.Columns[num7].ColumnName.ToUpper() == table.Columns[num8].ColumnName.ToUpper())
                        {
                            if ((dt2.Rows[n][num7] != null) && (dt2.Rows[n][num7].ToString().Length > 0))
                            {
                                table.Rows[n + dt1.Rows.Count][num8] = dt2.Rows[n][num7].ToString();
                            }
                            else
                            {
                                table.Rows[n + dt1.Rows.Count][num8] = DBNull.Value;
                            }
                        }
                    }
                }
            }
            return table;
        }
    }
}

