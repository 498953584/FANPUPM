<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRefunding.aspx.cs" Inherits="StockManage_Refunding_ViewRefunding" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>材料退库</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            setAnnxReadOnly('FileLink1');
        }); 		
    </script>
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
    <table class="tab" style="min-width: 800px;">
        <tr style="height: 20px;">
            <td class="divHeader">
                退库单
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                </div>
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
        <tr>
            <td style="height: 406px; width: 100%;" valign="top">
                <table cellpadding="0" cellspacing="2" style="width: 100%;" class="viewTable" border="0">
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
                        <td>
                            <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                            <input id="hdnProjectCode" style="width: 10px" type="hidden" name="hdnProjectCode" runat="server" />

                        </td>
                        <td class="descTd">
                            仓库
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTreasuryName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
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
                            <asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" OnRowDataBound="gvNeedNote_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                            <div style="word-break: break-all; min-width: 50px;">
                                                <%# Eval("scode") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
                                            <div style="word-break: break-all; min-width: 80px;">
                                                <%# Eval("ResourceName") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("Specification") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("Name") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="剩余量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                            <%# GetNumber(Eval("number").ToString(), Eval("orcode").ToString(), Eval("sprice").ToString(), Eval("scode").ToString(), Eval("CorpId").ToString()) %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="退库数量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                            <%# Eval("tnumber") %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                            <%# Eval("sprice") %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="退库小计" HeaderStyle-Width="80px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                            
                                            <%# (Eval("Total").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("corp") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("ModelNumber") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("brand") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("TechnicalParameter") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="077" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdwzId" runat="server" />
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdflowstate" Value="-1" runat="server" />
    <asp:HiddenField ID="hdTnumId" runat="server" />
    </form>
</body>
</html>
