<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectMyContrt.aspx.cs" Inherits="Common_DivSelectMyContrt" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="contractmanage_usercontrol_contracttype_ascx" Src="~/ContractManage/UserControl/ContractType.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>支出合同</title>

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
        function clickRow(obj, moduleCode, isLeaf, theCode, theName,monyey) {
            $("#hdName").val(theName);
            $("#hdCode").val(theCode);
        }

        function dbClickRow(obj, theCode, theName, acname, acouid, monyey, isLeaf) {
            var method = getRequestParam('Method'); //方法
            parent[method](theCode, theName, acname, acouid, monyey);
            divClose(parent);
        }
        function rowQueryA(path) {
            parent.parent.desktop.PayoutContractEdit = window;
            toolbox_oncommand(path, "查看收入合同");
        }
       
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="1000px">
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
                    <tr style="visibility:hidden">
                        <td class="divFooter" style="text-align: left">
                            <input type="button" id="btnAdd" onclick="addContract();" value="新增" />
                            <input type="button" id="btnAddProtocol" style="width: 100px" value="添加补充协议" disabled="disabled" />
                            <input type="button" id="btnUpdate" value="编辑" disabled="disabled" />
                            <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                            <input type="button" id="btnQuery" disabled="disabled" value="查看" />
                            <input type="button" id="btnPrivilege" disabled="disabled" value="权限" />
                            <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="081" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="pagediv">
                                <asp:GridView ID="gvwContract" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" OnPageIndexChanging="gvwContract_PageIndexChanging" DataKeyNames="ContractID,IsMainContract,PrjGuid,ContractCode,ContractName" runat="server">
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
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="link" onclick="rowQueryA('/ContractManage/PayoutContract/PayoutContractEdit.aspx?Action=Query&ContractId=<%# Eval("ContractID") %>')">
                                                    <%# Eval("ContractName") %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="账号名称"><ItemTemplate>
                                            <asp:HiddenField ID="hdfacid" Value='<%# System.Convert.ToString(base.GetBankNum(Eval("ContractID").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            <asp:Label ID="lblname" Text='<%# System.Convert.ToString(base.GetBankName(Eval("ContractID").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同金额"><ItemTemplate>
                                            <asp:Label ID="lblmony" Text='<%# System.Convert.ToString(Eval("ModifiedMoney"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BalanceModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PayModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignDate").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# GetAnnx(System.Convert.ToString(Eval("ContractID"))) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同性质" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# (Eval("fictitious").ToString() == "0") ? "虚拟合同" : "非虚拟合同" %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hdCode" runat="server" />    
    </form>
</body>
</html>
