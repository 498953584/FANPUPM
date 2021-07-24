<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequirePlanView.aspx.cs" Inherits="Equ_RequirePlan_RequirePlanView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备需求计划查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyProgressDetail', 'gvProgressDetail');
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                设备需求计划查看
                <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table class="viewTable" cellpadding="0px" cellspacing="0">
                    <tr>
                        <td class="descTd">
                            编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPrjName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            分部分项工程
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Label ID="lblTaskName" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr style="vertical-align: top;">
                        <td>
                            <asp:GridView ID="gvProgressDetail" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="id" runat="server">
<EmptyDataTemplate>
                                    <table class="tab" id="emptyProgressDetail">
                                        <tr class="header">
                                            <th scope="col" style="width: 25px;">
                                                序号
                                            </th>
                                            <th scope="col">
                                                编制年月
                                            </th>
                                            <th scope="col">
                                                设备类别名称
                                            </th>
                                            <th scope="col">
                                                预计进场时间
                                            </th>
                                            <th scope="col">
                                                预计退场时间
                                            </th>
                                            <th scope="col">
                                                预计进场地点
                                            </th>
                                            <th scope="col">
                                                设备来源
                                            </th>
                                            <th scope="col">
                                                预算费用
                                            </th>
                                            <th scope="col">
                                                数量
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编制年月"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                -
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类别名称"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预计进场日期" HeaderStyle-Width="80px">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预计出场日期" HeaderStyle-Width="80px">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预计进场地点">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备来源">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预算费用" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" ItemStyle-Width="80px"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="150" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
