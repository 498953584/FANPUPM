<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_WorkUser.aspx.cs" Inherits="BudgetManage_Report_Report_WorkUser" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>分项工程消耗对比表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="/Script/jw.js"></script>

    <script type="text/javascript" src="/Script/jwJson.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" >
        addEvent(window, 'load', function() {
        replaceEmptyTable('emptyBudget', 'gvBudget');
        var table = new JustWinTable('gvBudget');
        formateTable('gvBudget', 2, true);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
     <table width="100%"  Height="100%"  cellpadding ="0" cellspacing ="0" >
     <tr>
     <td  rowspan="2" id="td_Left" style="width: 195px; vertical-align: top; height: 100%;">
                    <div>
                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    </div>
                    <div id="div_project" class="pagediv" style="width: 195px; height: 100%;">
                        <asp:TreeView ID="tvBudget" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="tvBudget_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                    </div>
     </td>
     <td  style ="vertical-align: top; border-left: solid 2px #CADEED;  ">
      <table class="queryTable" cellpadding="3px" cellspacing="0px">
           <tr>
           <td>分项工程编码:</td>
           <td><asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox></td>
           <td>分项工程名称:</td>
           <td><asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox></td>
           </tr>
       </table>
     <table class ="tab" >
       <tr>
     <td class="divFooter"  style="text-align: left; padding-top: 2px;"  >
          <asp:Button ID="btnQuery" Width="80px" Text="查询" OnClick="btnQuery_Click" runat="server" />
          <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" OnClick="btnExcel_Click" runat="server" />
     </td>
     </tr>
      <tr>
      <td style="vertical-align: top; width:100%">
      <div id="div1" class="pagediv">
     <asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnDataBound="gvBudget_DataBound" runat="server">
<EmptyDataTemplate>
     <table   class ="gvdata" cellspacing="0" rules="all" border="1" id="emptyBudget" style="width:100%;border-collapse:collapse;">
     <tr class ="header"  >
     <th  scope ="col" >序号</th>
     <th  scope ="col" >分项工程编码</th>
     <th scope="col" >分项工程名称</th>
     <th scope="col" >材料名称</th>
     <th scope="col" >规格</th>
     <th scope="col" >单位</th>
     <th scope="col" >预算量</th>
     <th scope="col" >实际量</th>
     <th scope="col" >节超</th>
     </tr></table></EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderText="序号"></asp:TemplateField><asp:BoundField HeaderText="分项工程编码" DataField="TaskCode" ItemStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="分项工程名称" DataField="TaskName" ItemStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="材料名称" DataField="ResName" ItemStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="规格" DataField="Specification" ItemStyle-HorizontalAlign="Left" /><asp:BoundField HeaderText="单位" DataField="UnitName" ItemStyle-HorizontalAlign="Center" /><asp:BoundField HeaderText="预算量" DataField="TotalBudget" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="实际量" DataField="TotalAcc" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="节超" DataField="JieChao" ItemStyle-HorizontalAlign="Right" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
     <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
        </webdiyer:AspNetPager></div></td></tr></table></td> </tr> </table> 
    </div>
    </form>
</body>
</html>
