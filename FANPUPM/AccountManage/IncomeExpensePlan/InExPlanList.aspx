<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InExPlanList.aspx.cs" Inherits="AccountManage_IncomeExpensePlan_InExPlanList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>支出合同列表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="../../Script/jw.js"></script>

    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>


    <script type="text/javascript">
        addEvent(window, 'load', function() {
            replaceEmptyTable();
            var contractTable = new JustWinTable('gvwInExPlan');
            setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldContract')
            
            setWfButtonState2(contractTable, 'WF_1');
            contractTable.registClickTrListener(function() {
                //流程状态的列索引
                var flowIndex = 7;

                var flowTd = this.childNodes[flowIndex];

                //trFrame 为存放Frame的TR
                //必需将整个Table的高度设置为100%，且第二个TR的高度为1px
                var trWidth = document.getElementById('trFrame').offsetHeight;
                document.getElementById('framContract').style.height = (trWidth - 34) + 'px';

            });

           
        });

        function add() {
            parent.desktop.flowclass = window;

            var url = "/AccountManage/IncomeExpensePlan/InExPlanEdit.aspx?id=&type=add";
            toolbox_oncommand(url, "新增收支计划");
        }

        function registerDeleteContractEvent() {
            var btnDelete = document.getElementById('btnDelete');
            btnDelete.onclick = function() {
                if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
                    return false;
                }
            }
        }
        function update() {
            var hfldContract = document.getElementById('hfldContract');
            parent.desktop.flowclass = window;
            var url = "/AccountManage/IncomeExpensePlan/InExPlanEdit.aspx?id=" + hfldContract.value + "&type=edit";
            toolbox_oncommand(url, "编辑收支计划");

        }
        function view() {
            var hfldContract = document.getElementById('hfldContract');
            parent.desktop.flowclass = window;
            var url = "/AccountManage/IncomeExpensePlan/InExPlanView.aspx?ic=" + hfldContract.value;
            toolbox_oncommand(url, "查看收支计划");
        }

        function rowQueryA(path) {
            parent.desktop.flowclass = window;
            toolbox_oncommand(path, "查看收支计划");
        }

        function ClickRow(id, state) {
            document.getElementById("framContract").src = "../IEPInfo/IEPInfoList.aspx?id=" + id + "&state=" + state;
            var url = "../IEPInfo/IEPInfoList.aspx?id=" + id + "&state=" + state;
            window.open(url, "info");

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
        height: 100%; vertical-align: top;">
        <tr id="header">
            <td>
                <asp:Label ID="lblTitle" Text="收支计划列表" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <input type="button" id="btnAdd" value="新增" onclick="add()" />
                <input type="button" id="btnUpdate" value="编辑" disabled="disabled" onclick="update()" />
                <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                <input type="button" id="btnQuery" disabled="disabled" value="查看" onclick="view()" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="086" BusiClass="001" runat="server" />
                
            </td>
        </tr>
        <tr>
            <td style="height: 160px; width: 100%;">
                <table class="tab">
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="pagediv">
                                <asp:GridView ID="gvwInExPlan" CssClass="gvdata" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" OnRowDataBound="gvwInExPlan_RowDataBound" OnPageIndexChanging="gvwInExPlan_PageIndexChanging" DataKeyNames="ID,state,IEPNum" runat="server">
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
                                                    计划编号
                                                </th>
                                                <th scope="col">
                                                    计划名称
                                                </th>
                                                <th scope="col">
                                                    指定日期
                                                </th>
                                                <th scope="col">
                                                    计划类型
                                                </th>
                                                <th scope="col">
                                                    项目名称
                                                </th>
                                                <th scope="col">
                                                    流程状态
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
                                                <span class="link" onclick="rowQueryA('/AccountManage/IncomeExpensePlan/InExPlanView.aspx?ic=<%# Eval("id") %>')">
                                                    <%# StringUtility.GetStr(Eval("IEPNum").ToString()) %>
                                                </span>
                                                <asp:HiddenField ID="hidIEPid" Value='<%# Convert.ToString(Eval("id")) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划名称"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("IEPname").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="指定日期"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("IEPdate").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划类型"><ItemTemplate>
                                                <%# Eval("planType") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                <%# Common2.GetState(Eval("state").ToString()) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trFrame">
            <td style="width: 100%; vertical-align: top; padding-top: 10px;">
                <div id="div">
                    <iframe id="framContract" frameborder="0" name="info" src="../IEPInfo/IEPInfoList.aspx?id=&state=" width="100%" runat="server"></iframe>
                </div>
            </td>
        </tr>
    </table>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldContract" runat="server" />
    </form>
</body>
</html>
