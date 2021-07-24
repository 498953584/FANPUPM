<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInvoiceList.aspx.cs" Inherits="ContractManage_IncometInvoice_ViewInvoiceList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同发票</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        function rowQueryA(path) {
            parent.parent.desktop.AddIncometInvoice = window;
            toolbox_oncommand(path, "查看发票");
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="pagediv">
            <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发票编号"><ItemTemplate>
                            <a href="#" class="al" onclick="rowQueryA('/ContractManage/IncometInvoice/AddIncometInvoice.aspx?t=1&id=<%# Eval("InvoiceID") %>&contractId=<%# Eval("ContractId") %>')">
                                <%# Eval("InvoiceNo") %></a>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="日期"><ItemTemplate>
                            <%# Common2.GetTime(Eval("InvoiceDate").ToString()) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="办理入"><ItemTemplate>
                            <%# Eval("Transactor") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                            <%# Eval("Amount") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                            <%# GetAnnx(Convert.ToString(Eval("InvoiceID"))) %>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
    </div>
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
