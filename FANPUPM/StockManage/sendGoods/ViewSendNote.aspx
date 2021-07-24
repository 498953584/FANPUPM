<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSendNote.aspx.cs" Inherits="StockManage_sendGoods_ViewSendNote" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>发货单查看</title>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script src="../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            //            setAnnxReadOnly('FileLink1');
        });   
    </script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" cellpadding="0" cellspacing="0" style="vertical-align: top; min-width:800px;">
        <tr>
            <td class="divHeader">
                查看发货单
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
                            发货单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblsnCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            录入人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblsnAddUser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            录入时间
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblsnAddTime" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            有效收货人
                        </td>
                        <td colspan="3">
                            <asp:Label ID="labLimit" Text="Label" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            备注
                        </td>
                        <td colspan="3">
                            <div style="width: 100%; word-break: break-all;">
                                <asp:Label ID="lblremark" runat="server"></asp:Label></div>
                        </td>
                    </tr>
                </table>
                <table style="vertical-align: top; width: 100%;">
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                            <div style="word-break: break-all; min-width:50px;">
                                                <%# Eval("scode") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
                                            <div style="word-break: break-all; min-width:80px;">
                                                <%# Eval("ResourceName") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("Specification") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
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
                                        <div style=" word-break: break-all;">
                                            <%# Eval("TechnicalParameter") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <%# Eval("Name") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                            <%# Eval("number") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格"><ItemTemplate>
                                            <%# Eval("price") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                        <div style=" word-break: break-all;">
                                            <asp:Label ID="labCrop" ToolTip="" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdGuid" runat="server" />
    </form>
</body>
</html>
