<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InExPlanReport.aspx.cs" Inherits="AccountManage_PlanReport_InExPlanReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目收支计划报表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="/Script/jw.js"></script>

    <script type="text/javascript" src="/Script/jwJson.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">

        window.onload = function() {
            var aa = new JustWinTable('gvConract');
            formateTable('gvConract', 2, true);
            addPregressBar('percent');
        }
        //选择项目
        function openProjPicker() {
            document.getElementById("divFram").title = "选择项目";
            document.getElementById("ifFram").src = "/Common/DivSelectProject.aspx?Method=returnPrj";
            selectnEvent('divFram');
        }
        //选择项目返回值
        function returnPrj(id, name) {
            document.getElementById('hdPrjCode').value = id;
            document.getElementById('txtPrjName').value = name;
        }
        //往来单位
        function pickCorp() {
            var corp = new Array();
            corp[0] = "";
            corp[1] = "";
            var url = "/CommonWindow/PickCorp.aspx";
            window.showModalDialog(url, corp, "dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
            if (corp[0] != "") {
                document.getElementById('hdParty').value = corp[0];
                document.getElementById('txtParty').value = corp[1];
            }
        }
           
    </script>

</head>
<body>
    <form id="form2" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr style="height: 20px;">
            <td class="divHeader">
                <asp:Label ID="lblTitle" Text="项目收支计划报表" runat="server"></asp:Label>
                <asp:HiddenField ID="hdfIncomSum" runat="server" />
            </td>
        </tr>
        <tr style="height: 30px;">
            <td style="height: 20px;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td>
                            <span id="spanPrj" class="spanSelect" style="width: 122px;">
                                <input type="text" style="width: 100px; height: 15px; border: none; line-height: 16px;
                                    margin: 1px 2px" id="txtPrjName" runat="server" />

                                <img src="/images1/iconSelect.gif" alt="选择项目" id="imgPrj" onclick="openProjPicker();" />
                            </span>
                            <asp:HiddenField ID="hdPrjCode" runat="server" />
                        </td>
                        <td class="descTd">
                            计划编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractCode" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            计划名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            起始时间
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtStartTime" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            结束时间
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTime" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                            <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                            <asp:Button ID="btnWord" CssClass="button-normal" Text="导出Word" Width="80px" OnClick="btnWord_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" valign="top">
                            <div class="pagediv" style="width:100%">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ID,IEPNum" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                                <asp:Label ID="Label1" ToolTip='<%# Convert.ToString(Eval("prjName").ToString()) %>' Text='<%# Convert.ToString(StringUtility.GetStr(Eval("prjName").ToString())) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
                                                <%# Eval("IEPNum") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划名称"><ItemTemplate>
                                                <%# Eval("IEPname") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="支出金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                <%# Eval("PayMoney") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收入金额" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                                <%# Eval("IncomMoney") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("planType").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="制定时间"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("IEPdate").ToString()) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdfsummony" runat="server" />
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
