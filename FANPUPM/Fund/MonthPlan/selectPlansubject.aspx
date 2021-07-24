<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectPlansubject.aspx.cs" Inherits="Fund_MonthPlan_selectPlansubject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
         <script src="../../Script/jquery.js" type="text/javascript"></script>
        <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
        <script type="text/ecmascript" src="/StockManage/script/Config2S.js"></script>
        <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
        <script language="javascript" type="text/javascript">
            $(function() {
                var GridView1Table = new JustWinTable('GridView1');
            });
            function dbClickRow(obj) {
                var method = getRequestParam('Method'); //方法
                parent[method](obj);
                divClose(parent);
            }

            //点确定
            function btnOk() {
                divClose(parent);
            }
            //Div关闭方法
            function divClose(obj) {
                $(obj.document).find('.ui-icon-closethick').each(function() {
                    this.click();
                });
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" runat="server">
<EmptyDataTemplate>
      <table id="emptyContractType" class="gvdata">
            <tr class="header">                                        
                <th scope="col" style="width: 25px;">
                    序号
                </th>          
                <th scope="col">
                所属科目
                    </th>    
                    </tr>
                    </table>                              
        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
<ItemTemplate>
					     <%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Plansubject" HeaderText="所属科目" /></Columns><RowStyle CssClass="rowa"></RowStyle><HeaderStyle CssClass="header"></HeaderStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
     <div id="divFooter" class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <input id="Button1" type="button" value="关闭" class="noprint" onclick="btnOk()" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
