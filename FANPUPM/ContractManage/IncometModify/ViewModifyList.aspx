<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewModifyList.aspx.cs" Inherits="ContractManage_IncometModify_ViewModifyList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收入合同变更</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.extension.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        function rowQueryA(path) {
            parent.parent.desktop.AddIncometBalance = window;
            toolbox_oncommand(path, "查看收入合同结算");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="" style="border: solid 0px red;">
        <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更编号"><ItemTemplate>
                        <a href="#" class="al" onclick="rowQueryA('/ContractManage/IncometModify/AddIncometModify.aspx?t=1&id=<%# Eval("ID") %>&contractId=<%# Eval("ContractId") %>')">
                            <%# Eval("ChangeCode") %></a>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更时间"><ItemTemplate>
                        <%# Common2.GetTime(Eval("ChangeTime").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                        <%# Eval("ChangePrice") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="变更原因"><ItemTemplate>
                        <span class="tooltip" title='<%# Eval("ChangeReason").ToString() %>'>
                            <%# StringUtility.GetStr(Eval("ChangeReason").ToString(), 35) %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="办理人"><ItemTemplate>
                        <%# Eval("Transactor") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                        <%# GetAnnx(Convert.ToString(Eval("ID"))) %>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    </form>
</body>
</html>
