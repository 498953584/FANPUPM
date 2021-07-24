<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipmentPlanView.aspx.cs" Inherits="EPC_EquipmentManagement_Plan_equipmentPlanView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
            var table = new JustWinTable('grdDetail');
        });
        </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="divContent2">
        <table class="table-form" id="Table1" height="100%" cellspacing="0" cellpadding="0"
            width="100%" border="0">
            <tr>
                <td class="td-title">
                    机械器具计划查看
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
                                    <asp:TextBox ID="txtPlanCode" CssClass="text-input" Width="100%" Enabled="false" runat="server"></asp:TextBox>
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
                                    <JWControl:DateBox ID="calEnterDate" CssClass="text-input" Width="100%" Enabled="false" runat="server"></JWControl:DateBox>
                                </td>
                                <td class="td-label" align="right" width="15%">
                                    出场时间：
                                </td>
                                <td width="35%">
                                    <JWControl:DateBox ID="calExitDate" CssClass="text-input" Width="100%" Enabled="false" runat="server"></JWControl:DateBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" style="height: 18px" align="right" width="15%">
                                    制作人：
                                </td>
                                <td style="height: 18px" width="35%">
                                    
                                     <asp:Label ID="txtPlanMaker" CssClass="text-input" Width="100%" runat="server"></asp:Label>
                                </td>
                                <td class="td-label" style="height: 18px" align="right" width="15%">
                                    计划时间：
                                </td>
                                <td style="height: 18px" width="35%">
                                    <JWControl:DateBox ID="calPlanCreatTime" CssClass="text-input" Width="100%" Enabled="false" runat="server"></JWControl:DateBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td-label" valign="top" align="right" width="15%">
                                    备 注：
                                </td>
                                <td valign="top" colspan="3">
                                    <asp:TextBox ID="txtRemark" CssClass="textarea-input" Width="100%" Rows="3" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
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
                                       
                                        <%# Eval("EquipmentCount") %>
                                    </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
                                        
                                         <%# Eval("Remark") %>
                                    </ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </div>
   
    </form>
</body>
</html>
