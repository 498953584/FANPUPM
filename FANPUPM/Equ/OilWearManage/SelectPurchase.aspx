<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPurchase.aspx.cs" Inherits="Equ_OilWearManage_SelectPurchase" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择采购单</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwPurchase');
            replaceEmptyTable('emptyPurchase', 'gvwPurchase');
            jwTable.registClickTrListener(function () {
                $('#hfldPurchaseId').val($(this).attr('pid'));
                $('#hfldPurchaseCode').val($(this).attr('pcode'));
                $('#btnSave').removeAttr('disabled');
            });
            // 取消
            $('#btnCancel').click(function () {
                top.ui.closeWin();
            });
            // 保存
            $('#btnSave').click(function () {
                var purchaseInfo = { pid: $('#hfldPurchaseId').val(), pcode: $('#hfldPurchaseCode').val() };
                top.ui.callback(purchaseInfo);
                top.ui.callback = null;
                top.ui.closeWin();
            });
            // 显示被截取的信息
            jw.tooltip();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
            height: 91%; vertical-align: top;">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td>
                                起始日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" Width="120px" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                结束日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">
                                采购单编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPcode" Width="80px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table class="tab">
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvwPurchase" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPurchase_RowDataBound" DataKeyNames="pid,pcode" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyPurchase" class="gvdata">
                                                <tr class="header">
                                                    <th scope="col" style="width: 25px;">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        采购编号
                                                    </th>
                                                    <th scope="col">
                                                        采购合同
                                                    </th>
                                                    <th scope="col">
                                                        录入人
                                                    </th>
                                                    <th scope="col">
                                                        录入时间
                                                    </th>
                                                    <th scope="col">
                                                        是否受理
                                                    </th>
                                                    <th scope="col">
                                                        附件
                                                    </th>
                                                    <th scope="col">
                                                        说明
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                                    
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购编号"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购合同"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否受理" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明" HeaderStyle-Width="50px"><ItemTemplate>
                                                    <span class="tooltip" title=''>
                                                        
                                                    </span>
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 98%; height: 25px; float: left; text-align: right; vertical-align: middle"
        class="divFooter2">
        <input type="button" id="btnSave" disabled="disabled" value="保存" />
        <input type="button" id="btnCancel" value="取消" />
    </div>
    
    <asp:HiddenField ID="hfldPurchaseId" runat="server" />
    
    <asp:HiddenField ID="hfldPurchaseCode" runat="server" />
    </form>
</body>
</html>
