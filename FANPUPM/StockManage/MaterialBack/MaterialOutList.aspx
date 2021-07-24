<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialOutList.aspx.cs" Inherits="StockManage_MaterialBack_MaterialOutList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"> </script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
 <script type="text/javascript">
     window.onload = function()
     {
     var chk=new JustWinTable('gvOutReserve');
     }
     
     </script>
     <script type="text/javascript">
     function getCodeList()
     {
         window.opener.document.getElementById('hdnCodeList').value = document.getElementById('hdnCodeList').value;
        window.opener.document.getElementById('btnShowMaterial').click();
        window.close();
     }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%">
    <tr>
    <td>
    <div class="pagediv">
                  <asp:GridView ID="gvOutReserve" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
                                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                                    
                                                </HeaderTemplate>

<ItemTemplate>
                                                    <asp:CheckBox ID="chkBox" AutoPostBack="true" ToolTip='<%# Convert.ToString(Eval("orcode")) %>' OnCheckedChanged="cbBox_CheckedChanged" runat="server" />
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="出库编号"><ItemTemplate>
                                                  <%# Eval("orcode") %>
                                              </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
                                                    <%# Eval("person") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入时间"><ItemTemplate>
                                                    <%# string.Format("{0:d}", Eval("intime")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                    <%# Eval("flowstate") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                    <%# Eval("annx") %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明"><ItemTemplate>
                                                    <%# Eval("explain") %>
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                </div>
    </td>
    </tr>
    <tr class="header"><td>详细物资信息
        <input id="hdnCodeList" type="hidden" runat="server" />
</td></tr>
    <tr>
    <td>
     <div class="pagediv">
                                <asp:GridView ID="gvSmMaterialBack" AutoGenerateColumns="false" CssClass="gvdata" runat="server">
<EmptyDataTemplate>
                                                        <table class="tab" width="100%">
                                                            <tr class="header">
                                                                <td style="width: 30px">
                                                                    <asp:CheckBox ID="chkAll" runat="server" /></td>
                                                                <td style="width: 20px">
                                                                </td>
                                                                <td style="width: 100px">
                                                                    资源编号</td>
                                                                <td style="width: 150px">
                                                                   资源名称</td>
                                                                <td>
                                                                    规格</td>
                                                                     <td>
                                                                    单位</td>
                                                                     <td>
                                                                    单价</td>
                                                                    <td>
                                                                    供应商</td>
                                                                   <td>
                                                                    退库数量</td>
                                                            </tr>
                                                            <tr><td colspan="9" style="height:200px">暂无数据</td></tr>
                                                        </table>
                                                    </EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
                            <asp:CheckBox Width="20px" ID="chkAll" runat="server" />
                            </HeaderTemplate>

<ItemTemplate>
                            <asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Eval("scode")) %>' OnCheckedChanged="chkBox_CheckedChanged" runat="server" />
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="资源编号"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "scode") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "sprice") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "corp") %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="退库数量"><ItemTemplate>
                            <asp:TextBox ID="txtNumber" Text='<%# Convert.ToString(Eval("number")) %>' runat="server"></asp:TextBox>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                                </div>
    </td>
    </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" Text="保存" OnClientClick="getCodeList()" runat="server" />
                <asp:Button ID="btnClose" Text="关闭" runat="server" /></td>
        </tr>
    </table>
     
                               
    </form>
</body>
</html>
