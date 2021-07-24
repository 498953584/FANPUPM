<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseApplyView.aspx.cs" Inherits="Equ_PurchaseApply_PurchaseApplyView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>固定资产采购申请查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyApplyDetail', 'gvwApplyDetail');
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; width: 800px; margin: 0 auto">
        <tr style="height: 1px;">
            <td class="divHeader">
                固定资产采购申请查看
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
            <td>
                <table class="viewTable" cellpadding="0px" cellspacing="0">
                    <tr>
                        <td class="descTd">
                            申请单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblApplyCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备调配计划单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDeployPlan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            申请人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblApplicant" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            申请部门
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblApplyDep" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            申请日期
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblApplyDate" runat="server"></asp:Label>
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
                            <asp:GridView ID="gvwApplyDetail" Width="800px" AutoGenerateColumns="false" CssClass="gvdata" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                    <table id="emptyApplyDetail" class="tab">
                                        <tr class="header">
                                            <th scope="col" style="width: 25px;">
                                                序号
                                            </th>
                                            <th scope="col">
                                                资源名称
                                            </th>
                                            <th scope="col">
                                                申请数量
                                            </th>
                                            <th scope="col">
                                                预计单价
                                            </th>
                                            <th scope="col">
                                                建议厂家
                                            </th>
                                            <th scope="col">
                                                需求原因
                                            </th>
                                            <th scope="col">
                                                计划到货日期
                                            </th>
                                            <th scope="col">
                                                到货地点
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请数量" ControlStyle-CssClass="txt mustInput"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预计单价" ControlStyle-CssClass="txt mustInput"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建议厂家"><ItemTemplate>
                                            <div style="word-break: break-all; word-wrap: break-word; width: 100px;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="需求原因"><ItemTemplate>
                                            <div style="word-break: break-all; word-wrap: break-word; width: 100px;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划到货日期"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="到货地点"><ItemTemplate>
                                            <div style="word-break: break-all; word-wrap: break-word; width: 100px;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="155" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
