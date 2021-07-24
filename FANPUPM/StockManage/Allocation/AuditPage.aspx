<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditPage.aspx.cs" Inherits="StockManage_Allocation_AuditPage" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>调拨单审核</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            var aa = new JustWinTable('GVMaterialList');
            replaceEmptyTable('emptyContractType', 'GVMaterialList');
            document.getElementById('FileLink1_But_UpFile').style.display = 'none';
            document.getElementById('FileLink1_Btn_Upload').style.display = 'none';
        }

        function AnnexManage() {
            var lblno = document.getElementById("HdnAcode").value;
            url = "/CommonWindow/Annex/AnnexList.aspx?mid=89&rc=" + lblno + "&at=0&type=1";
            return window.showModalDialog(url, 'win', 'dialogHeight:260px;dialogWidth:560px;location:no;help:no;srcoll:no;resizable:no;center:yes;status:no');
        } 
    
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
    <table class="tab" cellpadding="0" cellspacing="0" style="vertical-align: top; min-width: 800px;">
        <tr>
            <td class="divHeader">
                调拨单
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
            <td style="height: 406px; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            调拨单号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAllocationNo" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            调拨日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtInDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            调出库
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtOutDepository" Width="205px" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            调入库
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtInDepository" Width="205px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            拨出人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtOutAllocationPerson" Width="205px" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            接收人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtInAllocationPerson" Width="205px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            附件管理
                        </td>
                        <td colspan="3">
                            <input type="button" id="btnAnnex" value="附件管理" onclick="AnnexManage();" style="display: none;" />&nbsp;
                            <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" MID="89" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3">
                            <div style="width: 100%; word-break: break-all;">
                                <asp:Label ID="txtRemark" runat="server"></asp:Label></div>
                        </td>
                    </tr>
                </table>
                <table style="vertical-align: top; width: 100%;">
                    <tr>
                        <td style="padding-top: 10px;">
                            <div class="pagediv">
                                <asp:GridView CssClass="viewTable" ID="GVMaterialList" Width="100%" AutoGenerateColumns="false" OnRowDataBound="GVMaterialList_RowDataBound" DataKeyNames="asid" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyContractType" class="gvdata" style="border: solid 0px;">
                                            <tr class="header">
                                                <th style="width: 20px">
                                                    <input type="checkbox" />
                                                </th>
                                                <th style="width: 25px">
                                                    序号
                                                </th>
                                                <th>
                                                    物资编号
                                                </th>
                                                <th>
                                                    物资名称
                                                </th>
                                                <th>
                                                    规格
                                                </th>
                                                <th>
                                                    单位
                                                </th>
                                                <th>
                                                    采购价
                                                </th>
                                                <th>
                                                    调拨数量
                                                </th>
                                                <th>
                                                    调拨小计
                                                </th>
                                                <th>
                                                    供应商
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField Visible="false">
<ItemTemplate>
                                                <asp:CheckBox ID="chkSelectIt" runat="server" />
                                            </ItemTemplate>

<HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectIt1" runat="server" />
                                            </HeaderTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资编号">
<ItemTemplate>
                                                <div style="word-break: break-all; min-width: 50px;">
                                                    <%# Eval("ResourceCode") %>
                                                </div>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
                                                <div style="word-break: break-all; min-width: 80px;">
                                                    <%# Eval("ResourceName") %>
                                                </div>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <%# Eval("specification") %>
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
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购价" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px"><ItemTemplate>
                                                <%# Eval("sprice") %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="snumber" HeaderText="库存量" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="调拨数量">
<ItemTemplate>
                                                <asp:Label ID="txtAllocationOutNum" Text="0.000" runat="server"></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计">
<ItemTemplate>
                                                <asp:Label ID="txtTotal" Text="0.000" runat="server"></asp:Label>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <asp:Label ID="txSupplytMaterial" Text='<%# Convert.ToString(Eval("corpName")) %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="075" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <input id="HdnTCodea" type="hidden" style="width: 1px" runat="server" />

    <input id="HdnAcode" type="hidden" style="width: 1px" runat="server" />

    </form>
</body>
</html>
