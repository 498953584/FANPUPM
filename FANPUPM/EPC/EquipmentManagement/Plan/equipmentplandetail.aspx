<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipmentplandetail.aspx.cs" Inherits="EquipmentPlanDetail" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectresource_ascx" Src="~/StockManage/UserControl/SelectResource.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<html>
<head runat="server"><title>EquipmentPlanDetail</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <base target="_self" />
    <link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />


    <script src="../../../Script/jquery.js" type="text/javascript"></script>

    <script src="../../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../../Script/jquery.tooltip/jquery.tooltip.js" type="text/javascript"></script>

    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        addEvent(window, 'load', function() {
            $('#btnBindResource').hide();
            //            replaceEmptyTable('emptyTable', 'grdDetail');
            var table = new JustWinTable('grdDetail');
            registerSaveEvent();
            //            setBtnState();
            showMoreName();
            showMoreSpecification();
        });
        function registerSaveEvent() {
            document.getElementById('btnSave').onclick = function() {
                if (!validate()) {
                    return false;
                }
            }
        }

        function validate() {
            if (!document.getElementById('grdDetail') || document.getElementById('grdDetail').firstChild.childNodes.length == 1) {
                alert('系统提示：\n\n请选择设备');
                return false;
            }
            return true;
        }
        //名称信息提示
        function showMoreName() {
            var tab = document.getElementById('grdDetail');
            if (tab != null) {
                for (i = 1; i < tab.rows.length; i++) {
                    var cells = tab.rows[i].cells;
                    if (cells.length == 1) return;
                    if (cells[3].children.length == 0) return;
                    var imgId = cells[3].children[0].id;
                    var altLength = document.getElementById(imgId).title.length;
                    if (altLength > 10) {
                        $('#' + imgId).css('display', '');
                        $('#' + imgId).tooltip({
                            track: true,
                            delay: 0,
                            showURL: false,
                            fade: true,
                            showBody: " - ",
                            extraClass: "solid 1px blue",
                            fixPNG: true,
                            left: -200
                        });
                    } else {
                        document.getElementById(imgId).title = '';
                    }
                }
            }
        }

        //规格信息提示
        function showMoreSpecification() {
            var tab = document.getElementById('grdDetail');
            if (tab != null) {
                for (i = 1; i < tab.rows.length; i++) {
                    var cells = tab.rows[i].cells;
                    if (cells.length == 1) return;
                    if (cells[4].children.length == 0) return;
                    var imgId = cells[4].children[0].id;
                    var altLength = document.getElementById(imgId).title.length;
                    if (altLength > 10) {
                        $('#' + imgId).css('display', '');
                        $('#' + imgId).tooltip({
                            track: true,
                            delay: 0,
                            showURL: false,
                            fade: true,
                            showBody: " - ",
                            extraClass: "solid 1px blue",
                            fixPNG: true,
                            left: -200
                        });
                    } else {
                        document.getElementById(imgId).title = '';
                    }
                }
            }
        }
        window.name = "frmdetl"
        function clickRow(dgd, obj, selectedIndex) {
            try {
                document.getElementById('btnDel').disabled = false;
            }
            catch (e) { };
            document.getElementById('hdnSelectedIndex').value = selectedIndex;
            /*在这之前增加你的处理代码*/
            doClick(obj, dgd); //调用样式设置
        }
        function outRow(obj) {
            /*在这之前增加你的处理代码*/
            doMouseOut(obj); //调用样式设置
        }
        function overRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOver(obj); //调用样式设置
        }
        function openPageAdd() {
            return window.showModalDialog('/EPC/Basic/Resource/pickresource.aspx?ses=6A1A7050-1F92-4291-B932-43E1DFCE6E92&rs=3', window, 'dialogHeight:600px;dialogWidth:700px;center:1;help:0;status:0;');
        }
        function CheckInputIsFloat(keyCode, e) {
            if ((keyCode > 95 && keyCode < 106) || (keyCode > 47 && keyCode < 58) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13) {
            }
            else if (keyCode == 110 || keyCode == 190) {
                if (e.value == "" || e.value.indexOf(".") != -1) {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    </script>

    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
<body>
    <form id="Form1" runat="server">
    <div class="divContent2">
        <table class="table-form" id="Table1" height="100%" cellspacing="0" cellpadding="0"
            width="100%" border="0">
            <tr>
                <td class="td-title">
                    机械器具计划编制
                </td>
            </tr>
            <tr>
                <td style="height: 90px" valign="top">
                    <asp:Panel ID="pnlPubInfo" runat="server">
                        <table class="table-form" id="Table2" cellspacing="0" cellpadding="0" width="100%"
                            border="0">
                            <tr>
                                <td class="td-label" align="right" width="15%">
                                    计划编号：
                                </td>
                                <td width="35%">
                                    <asp:TextBox ID="txtPlanCode" CssClass="text-input" Width="100%" runat="server"></asp:TextBox>
                                </td>
                                <td class="td-label" align="right" width="15%">
                                    工程名称：
                                </td>
                                <td width="35%">
                                    <asp:TextBox ID="txtPrjName" CssClass="text-input" Width="90%" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" align="right" width="15%">
                                    进场时间：
                                </td>
                                <td width="35%">
                                    <JWControl:DateBox ID="calEnterDate" CssClass="text-input" Width="100%" runat="server"></JWControl:DateBox>
                                </td>
                                <td class="td-label" align="right" width="15%">
                                    出场时间：
                                </td>
                                <td width="35%">
                                    <JWControl:DateBox ID="calExitDate" CssClass="text-input" Width="100%" runat="server"></JWControl:DateBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" style="height: 18px" align="right" width="15%">
                                    制作人：
                                </td>
                                <td style="height: 18px" width="35%">
                                    <asp:TextBox ID="txtPlanMaker" CssClass="text-input" Width="100%" runat="server"></asp:TextBox>
                                </td>
                                <td class="td-label" style="height: 18px" align="right" width="15%">
                                    计划时间：
                                </td>
                                <td style="height: 18px" width="35%">
                                    <JWControl:DateBox ID="calPlanCreatTime" CssClass="text-input" Width="100%" runat="server"></JWControl:DateBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" valign="top" align="right" width="15%">
                                    备 注：
                                </td>
                                <td valign="top" colspan="3">
                                    <asp:TextBox ID="txtRemark" CssClass="textarea-input" Width="100%" Rows="3" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr id="trhide" runat="server"><td class="td-toolsbar" runat="server">
                    <div class="headerDiv">
                        <MyUserControl:stockmanage_usercontrol_selectresource_ascx ID="SelectResource1" Text="增加机械设备" Width="85.0" ButtonId="btnBindResource" TypeCode="0003" runat="server" />
                        
                        <asp:Button ID="btnDel" CssClass="button-normal" Text="删  除" OnClick="btnDel_Click" runat="server" />
                       <asp:HiddenField ID="hdfCode" runat="server" />
                        <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                        <input id="btnClose" type="button" value="取消" onclick="winclose('equipmentplandetail', 'equipmentplanlist.aspx', false)" runat="server" />

                    </div>
                </td></tr>
            <tr>
                <td valign="top">
                    <div style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="grdDetail" Width="100%" CssClass="grid" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Eval("EquipmentCode")) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        <%# Container.ItemIndex + this.grdDetail.PageSize * this.grdDetail.CurrentPageIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="机械器具编号"><ItemTemplate>
                                        
                                        <asp:Label ID="labCode" Text='<%# Convert.ToString(Eval("EquipmentCode")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="机械器具名称"><ItemTemplate>
                                        
                                        <asp:Label ID="labName" Text='<%# Convert.ToString(Eval("ResourceName")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="数量"><ItemTemplate>
                                        <asp:TextBox ID="txtEquipmentCount" CssClass="text-num" onkeydown="event.returnValue=CheckInputIsFloat(event.keyCode,this)" Text='<%# Convert.ToString(Eval("EquipmentCount")) %>' runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEquipmentCount" Display="Dynamic" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtEquipmentCount" Display="Dynamic" ErrorMessage="类型不对" Type="Double" Operator="DataTypeCheck" runat="server"></asp:CompareValidator>
                                    </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
                                        <asp:TextBox ID="txtRemarkDetail" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnBindResource" OnClick="btnBindResource_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
