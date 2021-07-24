<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SuperResultDetail.aspx.cs" Inherits="EPC_SupplierGrade_SuperResultDetail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>

    <script src="../../Script/jquery.js" type="text/javascript"></script>

    <script src="../../SysFrame/jscript/JsControlWindow.js" type="text/javascript"></script>

    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>
    
     <script type="text/javascript">
        window.onload = function() {
        var purchasePlanTable = new JustWinTable('GridView1');
    }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent">
        <table border="1" style="border-collapse: collapse; width: 100%">
            <tr>
                <td  class="divHeader">
                    详细评估记录
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="id" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="时间" DataField="gradeTime" /><asp:BoundField HeaderText="手续齐全度" DataField="fileisall" /><asp:BoundField HeaderText="数量/重量/体积" DataField="numisover" /><asp:BoundField HeaderText="外观质量" DataField="lookisgood" /><asp:BoundField HeaderText="规格型号" DataField="tpyeisright" /><asp:BoundField HeaderText="及时性" DataField="timeisquk" /><asp:BoundField HeaderText="人员态度" DataField="smallisok" /><asp:TemplateField HeaderText="总分数"><ItemTemplate>
                                    <asp:Label ID="labCount" Text="" runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
