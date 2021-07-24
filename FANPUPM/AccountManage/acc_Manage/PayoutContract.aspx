<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutContract.aspx.cs" Inherits="ContractManage_PayoutContract_PayoutContract" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="contractmanage_usercontrol_contracttype_ascx" Src="~/ContractManage/UserControl/ContractType.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同</title>
    <style type="text/css">
        #spanContractType
        {
            width: 120px;
        }
    </style>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript" src="../../Script/wf.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            replaceEmptyTable();
            var contractTable = new JustWinTable('gvwContract');
            formateTable('gvwContract', 3);
        });



        function rowQueryA(path) {
            parent.desktop.accountList = window;
            toolbox_oncommand(path, "查看收入合同");
        }

        //选择项目
        function openProjPicker() {
            document.getElementById("divFram").title = "选择项目";
            document.getElementById("ifFram").src = "/Common/DivSelectProject.aspx?Method=returnPrj";
            selectnEvent('divFram');
        }
        //选择项目返回值
        function returnPrj(id, name) {
            document.getElementById('hdnProjectCode').value = id;
            document.getElementById('txtProject').value = name;
        }

        function replaceEmptyTable() {
            //当数据量为空时，修改样式
            if (!document.getElementById('gvwContract')) return;
            if (!document.getElementById('emptyContractType')) return;
            var gvwContractType = document.getElementById('gvwContract');
            var emptyContractType = document.getElementById('emptyContractType');
            if (gvwContractType.firstChild.childNodes.length == 1) {
                gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr id="header">
            <td>
                支出合同
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
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
                        <td class="descTd">
                            起始金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartMoney" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            结束金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndMoney" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractCode" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            合同名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            合同类型
                        </td>
                        <td>
                            <MyUserControl:contractmanage_usercontrol_contracttype_ascx ID="ContractType1" Width="124px" runat="server" />
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px">
                                <asp:TextBox ID="txtProject" CssClass="txtreadonly" Style="width: 97px;
                                    height: 15px; border: none; line-height: 16px; margin: 1px 2px;" runat="server"></asp:TextBox>
                                <img alt="选择录入人" onclick="openProjPicker();" src="/images1/iconSelect.gif" />
                            </span>
                            <asp:HiddenField ID="hdnProjectCode" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: right">
                            <asp:Button ID="btnOK" Text="确定" OnClick="btnOK_Click" runat="server" />
                            <asp:Button ID="btnCancle" Text="取消" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="pagediv">
                                <asp:GridView ID="gvwContract" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" OnPageIndexChanging="gvwContract_PageIndexChanging" DataKeyNames="ContractID,IsMainContract,PrjGuid,ContractCode" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyContractType" class="gvdata">
                                            <tr class="header">
                                                <th scope="col" style="width: 20px;">
                                                    <input id="chk1" type="checkbox" />
                                                </th>
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    合同编号
                                                </th>
                                                <th scope="col">
                                                    合同名称
                                                </th>
                                                <th scope="col">
                                                    合同金额
                                                </th>
                                                <th scope="col">
                                                    合同类型
                                                </th>
                                                <th scope="col">
                                                    乙方
                                                </th>
                                                <th scope="col">
                                                    结算方式
                                                </th>
                                                <th scope="col">
                                                    付款方式
                                                </th>
                                                <th scope="col">
                                                    流程状态
                                                </th>
                                                <th scope="col">
                                                    签约时间
                                                </th>
                                                <th scope="col">
                                                    项目
                                                </th>
                                                <th scope="col">
                                                    附件
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" />
                                            </HeaderTemplate>

<ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                                <asp:HiddenField ID="hdfuserCodes" Value='<%# Convert.ToString(Eval("userCodes")) %>' runat="server" />
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="link" onclick="rowQueryA('/ContractManage/PayoutContract/PayoutContractEdit.aspx?Action=Query&ContractId=<%# Eval("ContractID") %>')">
                                                    <%# Eval("ContractName") %>
                                                     <asp:HiddenField ID="hdfConNum" Value='<%# Convert.ToString(Eval("ContractID")) %>' runat="server" />
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
                                            </ItemTemplate><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="合同金额" /><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方" Visible="false"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式" Visible="false"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BalanceModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式" Visible="false"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PayModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" Visible="false"><ItemTemplate>
                                                <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignDate").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" Visible="false"></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramRole" title="" style="display: none">
        <iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldContract" runat="server" />
    </form>
</body>
</html>
