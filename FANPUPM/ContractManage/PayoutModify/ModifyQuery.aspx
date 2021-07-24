<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyQuery.aspx.cs" Inherits="ContractManage_PayoutModify_ModifyQuery" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
        table tr
        {
            border-color: Black;
            background-color: Black;
        }
        .fontsize
        {
            font-size: 13px;
        }
        #print
        {
            max-width: 670px;
            margin: 0 auto;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
       
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="tab" style="vertical-align: top; max-width: 1000px;">
        <tr>
            <td class="divHeader">
                支出合同变更
                <input type="button" id="Button1" style="float: right;" class="noprint" onclick="winPrint()"
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
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable" style="width: 100%;
                    margin: auto;">
                    <tr>
                        <td colspan="4" class="groupInfo">
                            合同基本信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LblProjectCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="LblProjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            合同名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同最终金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContractMoney" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            签订时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSignedDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="groupInfo">
                            合同变更信息
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            变更编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblModifyCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            变更金额
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblModifyMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="descTd">
                            办理人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblModifyPerson" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            变更日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblModifyDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            变更原因
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblModifyReason" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblNotes" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            录入人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInputPerson" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            录入时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInputDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" style="padding-right: 0px;">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trPurchase" style="vertical-align: top; height: 1px;">
            <td style="padding-top: 10px;">
                <div style="font-size: 13px; font-weight: bold; text-align: center;">
                    <asp:Label ID="lblTitalPurchase" Font-Size="13px" Font-Bold="true" runat="server"></asp:Label></div>
                
                <asp:GridView ID="gvwPurchaseplanStock" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="scode" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px"><ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号">
<ItemTemplate>
                                <%# Eval("ResourceCode") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
                                <%# Eval("ResourceName") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
                                <%# Eval("Specification") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                                <%# Eval("Name") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                <%# WebUtil.GetStockNumberByCode(Eval("scode").ToString()).ToString() %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                <%# Eval("number") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                <%# Eval("sprice") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                <%# Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                <%# Eval("CorpName") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                <%# Eval("ModelNumber") %>
                                </span>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                <%# Eval("brand") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                <%# Eval("TechnicalParameter") %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购单号">
<ItemTemplate>
                                <div style="width: 100%; word-break: break-all;">
                                    <%# Eval("pscode").ToString() %>
                                </div>
                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td colspan="4">
                <div>
                    <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="082" BusiClass="001" runat="server" />
                </div>
            </td>
        </tr>
        
    </table>
    </form>
</body>
</html>
