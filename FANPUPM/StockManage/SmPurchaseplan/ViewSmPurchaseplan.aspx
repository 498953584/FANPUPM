<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSmPurchaseplan.aspx.cs" Inherits="StockManage_SmPurchaseplan_ViewSmPurchaseplan" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>添加采购计划单</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            setAnnxReadOnly('FileLink1');
        });   
    </script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <style type="text/css">
        #FileLink1_But_UpFile
        {
            width: auto;
        }
        #FileLink1_Btn_Upload
        {
            width: auto;
        }
    </style>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" cellpadding="0" cellspacing="0" style=" min-width:800px">
        <tr>
            <td class="divHeader">
                查看采购计划单
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
        <tr style="vertical-align: top;">
            <td>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            单据号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPPCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            录入人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPeople" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            录入时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInTime" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="elemTd">
                            <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            说明
                        </td>
                        <td colspan="3">
                            <div style="width: 100%; word-break: break-all;">
                                <asp:Label ID="lblExplain" runat="server"></asp:Label></div>
                        </td>
                    </tr>
                </table>
                <table style="vertical-align: top; width: 100%;">
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="50px"><ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <%# Eval("scode") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="80px"><ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <%# Eval("ResourceName") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="50px"><ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <%# Eval("Specification") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <%# Eval("ModelNumber") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <%# Eval("brand") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <div style="word-break: break-all;">
                                            <%# Eval("TechnicalParameter") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <%# Eval("Name") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# WebUtil.GetStockNumberByCode(Eval("scode").ToString()).ToString() %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购数量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# Eval("number") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="到货日期" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <%# (Eval("arrivalDate").ToString() == "") ? "" : Eval("arrivalDate").ToString().Substring(0, Eval("arrivalDate").ToString().LastIndexOf(" ") + 1) %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# Eval("ReadyNumber") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# Eval("Price") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Left">
<ItemTemplate>
                                        <div style="word-break: break-all;">
                                            <%# Eval("Remark") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="072" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdwzId" runat="server" />
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdnCodeList" runat="server" />
    <asp:HiddenField ID="hdflowstate" Value="-1" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
