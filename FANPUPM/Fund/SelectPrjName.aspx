<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPrjName.aspx.cs" Inherits="Fund_SelectPrjName" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
   <script src="/Script/jquery.js" type="text/javascript"></script>
 <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function() {
            var aa = new JustWinTable('grdModules');
        });
        function clickRow(theCode, theName) {
            $("#hdName").val(theName);
            $("#hdCode").val(theCode);
            $("#btnSave").removeAttr('disabled');
        }

        function dbClickRow(theCode, theName) {
            var method = getRequestParam('Method'); //方法
            parent[method](theCode, theName);
            divClose(parent);
        }
        //点确定
        function btnOk() {
            if ($("#hdCode").val() != null && $("#hdName").val() != null) {
                var method = getRequestParam('Method'); //方法
                parent[method]($("#hdCode").val(), $("#hdName").val());
            }
            divClose(parent);
        }
				
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="hdCode" type="hidden" />
    <input id="hdName" type="hidden" />
    <div>
    <asp:GridView ID="GridView9" CssClass="gvdata" AutoGenerateColumns="false" OnDataBound="GridView9_DataBound" OnRowDataBound="GridView9_RowDataBound" runat="server">
<EmptyDataTemplate>
            <table class ="gvdata" id="gvTable"  style =" width :100%; text-align :center ;" >
                <tr class="header">
                    <td style =" width :25px;">序号</td>
                    <td>项目名称</td>                   
                </tr>
            </table>
            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目名称" ItemStyle-Width="100px"><ItemTemplate>
            <%# Eval("PrjName") %>
            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    <div class="td-submit" align="right" style="border:solid 0px red;">
     <input id="btnSave" type="button" value="确 定" onclick="btnOk();" disabled="disabled" />
                <input id="btnCancel" type="button" value="取 消" onclick="divClose(parent);" />
    </div>
    </form>
</body>
</html>
