<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentList.aspx.cs" Inherits="Equ_Equipment_EquipmentList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备台账</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
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
    <script type="text/javascript" src="../Script/Equipment/EquipmentList.js"></script>
    <script type="text/javascript">
        //选择设备类别
        function SetEquType() {
            var url = '/Equ/Equipment/SelectEquipmentType.aspx?' + new Date().getTime();
            var equTypeInfo = { width: 1000, height: 550, url: url, title: '选择设备类别' };
            top.ui.callback = function (equTypeInfo) {
                $('#hfldEquTypeId').val(equTypeInfo.id);
                $('#txtEquTypeName').val(equTypeInfo.name);
                $('#hdButton').click();
            };
            top.ui.openWin(equTypeInfo);
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
                                设备状态
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlState" Width="120px" runat="server"></asp:DropDownList>
                            </td>
                            <td class="descTd">
                                设备编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                设备名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                设备性质
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProperty" Width="120px" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                设备类别
                            </td>
                            <td>
                                <span id="span3" class="spanSelect" style="width: 130px;">
                                    <input type="text" style="width: 80%; height: 15px; border: none; line-height: 16px;
                                        margin: 1px 0px 1px 2px" readonly="readonly" id="txtEquTypeName" runat="server" />

                                    <img alt="选择设备类别" onclick="SetEquType();" src="../../images/icon.bmp" style="float: right;" />
                                </span>
                                <asp:HiddenField ID="hfldEquTypeId" runat="server" />
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
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table class="tab">
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div>
                                    <asp:GridView ID="gvEquipment" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvEquipment_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyEquipment" class="gvdata">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px;">
                                                        <input id="chk1" type="checkbox" />
                                                    </th>
                                                    <th scope="col" style="width: 25px;">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        设备编号
                                                    </th>
                                                    <th scope="col">
                                                        设备名称
                                                    </th>
                                                    <th scope="col">
                                                        设备类别
                                                    </th>
                                                    <th scope="col">
                                                        设备性质
                                                    </th>
                                                    <th scope="col">
                                                        发票号
                                                    </th>
                                                    <th scope="col">
                                                        制造厂家
                                                    </th>
                                                    <th scope="col">
                                                        设备状态
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
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="EquCode" HeaderText="设备编号" /><asp:TemplateField HeaderText="设备名称"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类别"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="设备性质" DataField="PropertyName" /><asp:BoundField HeaderText="发票号" DataField="ReceiptNo" /><asp:TemplateField HeaderText="制造厂家"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
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
    </form>
</body>
</html>
