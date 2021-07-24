<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IFCostDetails.aspx.cs" Inherits="BudgetManage_Cost_IFCostDetails" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvDiaryDetails');
            replaceEmptyTable('gvEmpty', 'gvDiaryDetails')
            displayLookAdjuncts();
        });

        //查看附件
        function queryAdjunct(id) {
            var path = $('#hfldAdjunctPath').val() + '/' + id;
            parent.window.showFiles(path, 'divOpenAdjunct');
        }
        //附件显示
        function displayLookAdjuncts() {
            var tab = document.getElementById('gvDiaryDetails');
            if (tab.rows.length > 0) {
                for (i = 1; i < tab.rows.length; i++) {
                    var id = tab.rows[i].id;
                    var path = $('#hfldAdjunctPath').val() + '/' + id;
                    var showCount = getFilesCount(path);
                    if (showCount == 0)
                        tab.rows[i].cells[4].innerText = '';
                }
            }
        }
        function getFilesCount(path) {
            var count = 0;
            $.ajax({
                type: "GET",
                url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
                async: false,
                dataType: "JSON",
                success: function (data) {
                    count = data.length;
                }
            });
            return count;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 170px; overflow: auto;">
        <asp:GridView CssClass="gvdata" ID="gvDiaryDetails" AutoGenerateColumns="false" OnRowDataBound="gvDiaryDetails_RowDataBound" DataKeyNames="InDiaryDetailsId" runat="server">
<EmptyDataTemplate>
                <table id="gvEmpty" class="gvdata">
                    <tr class="header">
                        <th scope="col" style="width: 25px;">
                            序号
                        </th>
                        <th scope="col">
                            名称
                        </th>
                        <th scope="col">
                            金额
                        </th>
                        <th scope="col">
                            归属成本科目
                        </th>
                        <th scope="col">
                            附件
                        </th>
                        <th scope="col">
                            备注
                        </th>
                        <th scope="col">
                            核销金额
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
<EmptyDataRowStyle CssClass="header"></EmptyDataRowStyle><EmptyDataRowStyle CssClass="header"></EmptyDataRowStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="名称" DataField="Name" /><asp:BoundField HeaderText="金额" DataField="Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" DataFormatString="{0:#,##0.000}" /><asp:TemplateField HeaderText="归属成本科目"><ItemTemplate>
                        <%# CBSName(Eval("CBSCode").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" HeaderStyle-Width="50px">
<ItemTemplate>
                        <span class="link" onclick="queryAdjunct('<%# Eval("InDiaryDetailsId") %>')">
                            <img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
                        </span>
                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="备注" DataField="Note" /><asp:BoundField HeaderText="核销金额" DataField="AuditAmount" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" DataFormatString="{0:#,##0.000}" /></Columns><HeaderStyle CssClass="header"></HeaderStyle><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle></asp:GridView>
    </div>
    <asp:HiddenField ID="hfldAdjunctPath" runat="server" />
    </form>
</body>
</html>
