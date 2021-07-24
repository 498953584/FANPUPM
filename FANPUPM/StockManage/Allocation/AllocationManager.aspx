<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllocationManager.aspx.cs" Inherits="StockManage_Allocation_AllocationManager" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>调拨管理</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnSertch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var allocationTable = new JustWinTable('GVAllocationList');
            setButton(allocationTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldPurchaseChecked');
            addEvent($('#btnQuery').get(0), 'click', rowQuery);
            addEvent($('#btnAdd').get(0), 'click', rowAdd);
            setWfButtonState2(allocationTable, 'WF1');
            // 设置只读
            $('#txtAllocationDateStarts').attr('readonly', 'readonly');
            $('#txtAllocationDateEnd').attr('readonly', 'readonly');
        });

        function rowAdd() {
            top.ui._transferringOrdre = window;
            var url = "/StockManage/Allocation/TransferringOrder.aspx?op=Add&ac=0";
            toolbox_oncommand(url, "新增调拨单");
        }
        function rowEdit() {
            top.ui._transferringOrdre = window;
            var url = "/StockManage/Allocation/TransferringOrder.aspx?op=Edit&ac=" + $("#hfldPurchaseChecked").val();
            toolbox_oncommand(url, "编辑调拨单");
        }
        function rowQuery() {
            var url = 'AuditPage.aspx?ic=' + this.guid + '&BusiClass=001&BusiCode=075';
            viewopen(url, 820, 500);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%;">
        
        <tr>
            <td style="height: 30px; padding-left: 2px;">
                调拨单号<asp:TextBox ID="txtAllocationBill" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                调拨日期
                <asp:TextBox ID="txtAllocationDateStarts" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                至
                <asp:TextBox ID="txtAllocationDateEnd" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                <asp:Button ID="btnSertch" Text="查 询" OnClick="btnSertch_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="height: 25px; text-align: left;" align="left">
                <input type="button" id="btnAdd" value="新增" />
                <input type="button" id="btnUpdate" value="编辑" onclick="rowEdit();" disabled="disabled" />
                <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
                <input type="button" id="btnQuery" disabled="disabled" value="查看" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" BusiCode="075" runat="server" />
                <input type="hidden" id="HdnAcode" style="width: 1px;" runat="server" />

                <input type="hidden" id="HdnAcodeList" style="width: 1px" runat="server" />

            </td>
        </tr>
        <tr>
            <td>
                <div class="pagediv">
                    <asp:GridView CssClass="gvdata" ID="GVAllocationList" Width="100%" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="GVAllocationList_RowDataBound" OnPageIndexChanging="GVAllocationList_PageIndexChanging" DataKeyNames="acode,tcodea,tcodeb" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("acode")) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调拨单号"><ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl="" Text="" runat="server"></asp:HyperLink>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="调出库" /><asp:BoundField HeaderText="调入库" /><asp:BoundField DataField="intime" HeaderText="调拨日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("flowstate").ToString()) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="HdnIsPage" style="width: 1px;" value="" runat="server" />

    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
