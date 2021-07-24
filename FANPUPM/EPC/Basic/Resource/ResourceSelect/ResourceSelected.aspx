<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceSelected.aspx.cs" Inherits="EPC_Basic_Resource_ResourceSelect_ResourceSelected" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window,'load',function(){var jwTable = new JustWinTable('GVMaterial');});
    </script>
</head>
<body >
    <form id="form1" runat="server">
   
        <asp:GridView ID="GVMaterial" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" Width="100%" OnRowDataBound="GVMaterial_RowDataBound" OnPageIndexChanging="GVMaterial_PageIndexChanging" runat="server">
<EmptyDataTemplate>
            <table class="gvdata" >
            <tr class="header">
             <th scope="col">序号</th>
             <th scope="col">编码</th>
             <th scope="col">资源名称</th>
             <th scope="col">规格</th>
             <th scope="col">单价</th>
             <th scope="col">图片</th>
            </tr>
            </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField>
<ItemTemplate>
                        <asp:CheckBox ID="chkSelectIt" runat="server" />
                    </ItemTemplate>

<HeaderTemplate>
                    <asp:CheckBox ID="chkSelectIt1" onclick="parent.selectCancelCHK(this.checked);" runat="server" />
                    </HeaderTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceCode" HeaderText="编码" /><asp:BoundField DataField="ResourceName" HeaderText="资源名称" /><asp:BoundField DataField="Specification" HeaderText="规格" /><asp:TemplateField HeaderText="单价"><ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server"></asp:Label>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server"></asp:Label>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="图片"><ItemTemplate>                      
                        <%# WebUtil.GetImg(Eval("imgurl").ToString()) %>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
 
    </form>
</body>
</html>
