<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DayOutputMenu.aspx.cs" Inherits="Equ_DayOutputMenu_DayOutputMenu" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>日运转情况</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/wf.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwProductionReport');
            replaceEmptyTable('emptyProductionReport', 'gvwProductionReport');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldDayId');
            $('#btnAdd').click(rowAdd);
            $('#btnEdit').click(rowEdit);
            $('#btnQuery').click(queryProductionReport);
            function rowAdd() {
                switch ($('#hfldEquEnum').val()) {
                    case "1":
                        parent.desktop.ShipGrapReportEdit = window;
                        var url = "/Equ/ShipGrapReport/ShipGrapReportEdit.aspx?equId=" + $('#hfldEquId').val();
                        toolbox_oncommand(url, "新增抓扬式挖泥船产量上报");
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    default:
                        break;
                }
            }
            function rowEdit() {
                parent.desktop.ShipGrapReportEdit = window;
                var url = "/Equ/ShipGrapReport/ShipGrapReportEdit.aspx?equId=" + $('#hfldEquId').val() + "&dayId=" + $("#hfldDayId").val();
                toolbox_oncommand(url, "编辑抓斗船产量上报");
            }
            function queryProductionReport() {
                viewopen('/Equ/ShipGrapReport/ShipGrapReportView.aspx?ic=' + $('#hfldId').val(), 820, 500, '查看抓斗船产量上报');
            }
            setWfButtonState2(jwTable, 'WF1');
            //显示被截取的信息
            jw.tooltip();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 100%">
        <table cellpadding="3px" cellspacing="0px" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 20%; vertical-align: top; border-right: 2px solid #CADEED">
                    <div class="pagediv" style="width: 100%; height: 100%;" id="div1" runat="server">
                        <asp:TreeView ID="trvwEquipmentType" ShowLines="true" ExpandDepth="2" OnSelectedNodeChanged="trvwEquipmentType_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                    </div>
                </td>
                <td style="vertical-align: top">
                    <table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
                        height: 100%; vertical-align: top;">
                        <tr>
                            <td class="divFooter" style="text-align: left;">
                                <input type="button" id="btnAdd" value="新增" />
                                <input type="button" id="btnEdit" value="编辑" disabled="disabled" />
                                <input type="button" id="btnQuery" value="查看" disabled="disabled" />
                                <asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="144" BusiClass="001" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <table class="tab">
                                    <tr>
                                        <td style="height: 100%; vertical-align: top;">
                                            <div class="">
                                                <asp:GridView ID="gvwProductionReport" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwProductionReport_RowDataBound" DataKeyNames="DayId" runat="server">
<EmptyDataTemplate>
                                                        <table id="emptyProductionReport" class="gvdata">
                                                            <tr class="header">
                                                                <th scope="col" style="width: 20px;">
                                                                    <input id="chk1" type="checkbox" />
                                                                </th>
                                                                <th scope="col" style="width: 25px;">
                                                                    序号
                                                                </th>
                                                                <th scope="col" style="width: 25px;">
                                                                    日期
                                                                </th>
                                                                <th scope="col">
                                                                    设备名称
                                                                </th>
                                                                <th scope="col">
                                                                    项目名称
                                                                </th>
                                                                <th scope="col">
                                                                    设备性质
                                                                </th>
                                                                <th scope="col">
                                                                    上报日期
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
                                                                
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="日期"><ItemTemplate>
                                                                
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备名称"><ItemTemplate>
                                                                <span class="tooltip" title=''>
                                                                    
                                                                </span>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                                                <span class="tooltip" title=''>
                                                                    
                                                                </span>
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备性质"><ItemTemplate>
                                                                
                                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上报日期"><ItemTemplate>
                                                                
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
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldEquId" runat="server" />
    <asp:HiddenField ID="hfldMonth" runat="server" />
    <asp:HiddenField ID="hfldNodeValuePath" runat="server" />
    <asp:HiddenField ID="hfldEquEnum" runat="server" />
    <asp:HiddenField ID="hfldDayId" runat="server" />
    </form>
</body>
</html>
