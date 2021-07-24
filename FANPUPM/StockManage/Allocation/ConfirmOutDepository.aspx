<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmOutDepository.aspx.cs" Inherits="StockManage_Allocation_ConfirmOutDepository" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>确认调出</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('GVAllocationList');
        });

        function chkClick(isSure, checked, ac) {
            if (isSure == "True")
                document.getElementById("btnConfirm").disabled = true;
            else {
                if (checked) {
                    document.getElementById("HdnAcodeList").value = ac;
                }
            }
        }

        function rowClick(isSure, ac) {
            if (isSure == "True")
                document.getElementById("btnConfirm").disabled = true;
            else {
                document.getElementById("btnConfirm").disabled = false;
                document.getElementById("HdnAcode").value = ac;
                document.getElementById("HdnAcodeList").value = ac;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnConfirm" Style="width: 100px;" Text="确认调出" Enabled="false" OnClick="btnConfirm_Click" runat="server" />
                <input type="hidden" id="HdnAcode" style="width: 1px;" runat="server" />

                <input type="hidden" id="HdnAcodeList" style="width: 1px" runat="server" />

            </td>
        </tr>
        <tr>
            <td valign="top">
                <div class="pagediv">
                    <asp:GridView CssClass="gvdata" ID="GVAllocationList" Width="100%" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="GVAllocationList_RowDataBound" OnPageIndexChanging="GVAllocationList_PageIndexChanging" DataKeyNames="acode,tcodea,tcodeb" runat="server">
<EmptyDataTemplate>
                            <table class="tab">
                                <tr class="header">
                                    <th style="width: 20px">
                                    </th>
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        调拨单号
                                    </th>
                                    <th scope="col">
                                        调出库
                                    </th>
                                    <th scope="col">
                                        调入库
                                    </th>
                                    <th scope="col">
                                        调拨时间
                                    </th>
                                    <th scope="col">
                                        是否调出
                                    </th>
                                    <th scope="col">
                                        是否接收
                                    </th>
                                    <th scope="col">
                                        流程状态
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField>
<ItemTemplate>
                                    <asp:CheckBox ID="chkSelectIt" runat="server" />
                                </ItemTemplate>

<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" Visible="false" runat="server" />
                                </HeaderTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调拨单号"><ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl="" Text="" runat="server"></asp:HyperLink>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="调出库" /><asp:BoundField HeaderText="调入库" /><asp:BoundField DataField="intime" HeaderText="调拨时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField HeaderText="是否调出" /><asp:BoundField HeaderText="是否接收" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("flowstate").ToString()) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
