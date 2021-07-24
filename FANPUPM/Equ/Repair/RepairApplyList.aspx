<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepairApplyList.aspx.cs" Inherits="Equ_Repair_RepairApplyList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>维修保养申请</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvRepairApply');
            replaceEmptyTable('emptyRepairApply', 'gvRepairApply');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldIdsChecked');
            jw.tooltip();
            //新增
            $('#btnAdd').click(function () {
                openNewTab('add');
            });
            //编辑
            $('#btnEdit').click(function () {
                openNewTab('edit');
            });
            //查看维修申请
            $('#btnQuery').click(function () {
                var url = '/Equ/Repair/RepairApplyView.aspx?ic=' + $('#hfldIdsChecked').val();
                viewopen(url, '查看维修保养申请');
            });
            setWfButtonState2(jwTable, 'WF1');
        });
        //打开新的标签页 新增/编辑
        function openNewTab(action) {
            var title = '新增维修保养申请';
            parent.desktop.RepairApplyEdit = window;
            var urlStr = '/Equ/Repair/RepairApplyEdit.aspx?' + new Date().getTime() + '&action=' + action + '&equipmentType=' + $('#hfldEquipmentType').val();
            if (action == 'edit') {
                urlStr = urlStr + '&id=' + $('#hfldIdsChecked').val();
                title = '编辑维修保养申请';
            }
            toolbox_oncommand(urlStr, title);
        }
        //标签页查看
        function Query(id) {
            parent.desktop.RepairPlanView = window;
            var url = '/Equ/Repair/RepairApplyView.aspx?ic=' + id;
            toolbox_oncommand(url, "查看维修保养申请");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
            height: 97%; vertical-align: top;">
            <tr style="height: 1px;">
                <td style="vertical-align: top;">
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">
                                申请日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtApplyDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                维修申请编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                设备编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtEquCode" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="divFooter" style="text-align: left;">
                    <input type="button" id="btnAdd" value="新增" />
                    <input type="button" id="btnEdit" value="编辑" disabled="disabled" />
                    <input type="button" id="btnQuery" value="查看" disabled="disabled" />
                    <asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" BusiCode="147" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table class="tab">
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div class="">
                                    <asp:GridView ID="gvRepairApply" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvRepairApply_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyRepairApply" class="gvdata">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px;">
                                                        <input id="chk1" type="checkbox" />
                                                    </th>
                                                    <th scope="col" style="width: 25px;">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        维修申请编号
                                                    </th>
                                                    <th scope="col">
                                                        设备编号
                                                    </th>
                                                    <th scope="col">
                                                        施工区域
                                                    </th>
                                                    <th scope="col">
                                                        申请部门
                                                    </th>
                                                    <th scope="col">
                                                        申请日期
                                                    </th>
                                                    <th scope="col">
                                                        本次维修内容
                                                    </th>
                                                    <th scope="col">
                                                        维修方式
                                                    </th>
                                                    <th scope="col">
                                                        维保标识
                                                    </th>
                                                    <th scope="col">
                                                        流程状态
                                                    </th>
                                                    <th scope="col">
                                                        备注
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" />
                                                </HeaderTemplate><ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="维修申请编号"><ItemTemplate>
                                                    <span class="link tooltip" title='' onclick="Query('')">
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备编号"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工区域"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本次维修内容"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请部门"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="维修方式" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="维保标识" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldIdsChecked" runat="server" />
    
    <asp:HiddenField ID="hfldEquipmentType" runat="server" />
    </form>
</body>
</html>
