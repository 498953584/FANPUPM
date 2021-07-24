<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceDetails.aspx.cs" Inherits="EPC_Basic_Resource_ResourceSelect_ResourceDetails" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () { var jwTable = new JustWinTable('GVMaterial'); });

        function selectAllCheckBox(chked, TbName) {
            var pt = document.getElementById("HdnPriceType").value;
            var ispage = document.getElementById("HdnIsPage").value;
            parent.selectAllChk(chked, 'GVMaterial', pt, ispage);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 20px; vertical-align: bottom;">
                物资编号<asp:TextBox ID="txtResCode" Width="100px" runat="server"></asp:TextBox>
                物资名称<asp:TextBox ID="txtResName" Width="100px" runat="server"></asp:TextBox>
                <asp:Button ID="btnSertch" Text="查询" OnClick="btnSertch_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv" style="height: 180px; width: 395px; overflow: auto; vertical-align: top;">
                    <asp:GridView ID="GVMaterial" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="GVMaterial_RowDataBound" OnPageIndexChanging="GVMaterial_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata">
                                <tr class="header">
                                    <th style="width: 20px">
                                        <input type="checkbox" id="hselectit" />
                                    </th>
                                    <th style="width: 25px;">
                                        序号
                                    </th>
                                    <th>
                                        编码
                                    </th>
                                    <th>
                                        资源名称
                                    </th>
                                    <th>
                                        规格
                                    </th>
                                    <th>
                                        单价
                                    </th>
                                    <th>
                                        图片
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField>
<ItemTemplate>
                                    <asp:CheckBox ID="chkSelectIt" runat="server" />
                                </ItemTemplate>

<HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectIt1" onclick="selectAllCheckBox(this.checked,'GVMaterial')" runat="server" />
                                </HeaderTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceCode" HeaderText="编码" /><asp:BoundField DataField="ResourceName" HeaderText="资源名称" /><asp:BoundField DataField="Specification" HeaderText="规格" /><asp:TemplateField HeaderText="单价">
<ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="图片">
<ItemTemplate>
                                    <%# WebUtil.GetImg(Eval("imgurl").ToString()) %>
                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header" Height="18px"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <input type="hidden" id="HdnPriceType" style="width: 1px" runat="server" />

    <input type="hidden" id="HdnIsPage" style="width: 1px" runat="server" />

    
    </form>
</body>
</html>
