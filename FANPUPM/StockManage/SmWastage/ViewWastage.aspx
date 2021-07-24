<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewWastage.aspx.cs" Inherits="StockManage_SmWastage_ViewWastage" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>报损出库单</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        //		addEvent(window, 'load', function () {
        //			setAnnxReadOnly('FileLink1');
        //		});  
     
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
                查看报损出库
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
                            选择仓库
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblTerasuryName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            录入人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPeople" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            录入时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblInTime" runat="server"></asp:Label>
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
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td colspan="3" class="td-content" id="FileUpload1" runat="server">
                        </td>
                    </tr>
                </table>
                <table style="vertical-align: top; width: 100%;">
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:GridView ID="gvWastage" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" OnRowDataBound="gvWastage_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                            <div style="word-break: break-all; min-width: 50px;">
                                                <%# Eval("ResourceCode") %>
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
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存数量">
<ItemTemplate>
                                            <%# Eval("snumber") %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="出库数量">
<ItemTemplate>
                                            <%# Eval("number") %>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价">
<ItemTemplate>
                                            <div align="right">
                                                <%# Eval("sprice") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="出库小计">
<ItemTemplate>
                                            <div align="right">
                                                <%# (Eval("Total").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("CorpName") %>
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
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="125" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdwzId" runat="server" />
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdTsid" runat="server" />
    <asp:HiddenField ID="hdflowstate" Value="-1" runat="server" />
    </form>
</body>
</html>
