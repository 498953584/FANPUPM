<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowInvoiceList.aspx.cs" Inherits="ContractManage_IncometInvoice_ShowInvoiceList" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同发票列表</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
      <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var aa = new JustWinTable('gvConract');
            if (document.getElementById("hdContractID").value == "") {
                document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                document.getElementById("btnEdit").setAttribute('disabled', 'disabled');
                document.getElementById("btnLook").setAttribute('disabled', 'disabled');
            }
            else {
                setButton(aa, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
                document.getElementById("btnAdd").removeAttribute('disabled');
            }

            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
        });
        function rowEdit() {
            parent.parent.desktop.AddIncometInvoice = window;
            var url = "/ContractManage/IncometInvoice/AddIncometInvoice.aspx?id=" + document.getElementById("hfldPurchaseChecked").value + "&ContractID=" + document.getElementById("hdContractID").value;
            toolbox_oncommand(url, "编辑发票");
        }
        function rowAdd() {
            parent.parent.desktop.AddIncometInvoice = window;
            var url = "/ContractManage/IncometInvoice/AddIncometInvoice.aspx?ContractID=" + document.getElementById("hdContractID").value;
            toolbox_oncommand(url, "新增发票");
        }
        function rowQuery() {
            // parent.parent.desktop.AddIncometInvoice = window;
            var url = "/ContractManage/IncometInvoice/QueryInvoice.aspx?t=1&id=" + document.getElementById("hfldPurchaseChecked").value + "&ContractID=" + document.getElementById("hdContractID").value;
            //toolbox_oncommand(url, "查看发票");
            viewopen(url);
        } 
           
  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <input type="button" id="btnAdd" value="新增" />
                            <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                            <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                            <input type="button" id="btnLook" disabled="disabled" value="查看" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="pagediv">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="InvoiceID" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("InvoiceID")) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发票号码"><ItemTemplate>
                                                <a href="#" class="al" onclick="viewopen('/ContractManage/IncometInvoice/QueryInvoice.aspx?t=1&id=<%# Eval("InvoiceID") %>&contractId=<%# Eval("ContractId") %>')">
                                                    <%# Eval("InvoiceNo") %></a>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收款累计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# WebUtil.GetPaymentSum(Eval("ContractID").ToString(), "Con_Incomet_Payment", "CllectionPrice") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="累计已开发票" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# WebUtil.GetPaymentSum(Eval("ContractID").ToString(), "Con_Incomet_Invoice", "Amount") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本次开票金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# Eval("Amount") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="实际开票金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# WebUtil.SapRealPayMoney(Eval("InvoiceNo")) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收款单位"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Payee").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款单位"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Payer").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="日期"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("InvoiceDate").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="办理人"><ItemTemplate>
                                                <%# Eval("Transactor") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                <%# GetAnnx(Convert.ToString(Eval("InvoiceID"))) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hdContractID" runat="server" />
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
