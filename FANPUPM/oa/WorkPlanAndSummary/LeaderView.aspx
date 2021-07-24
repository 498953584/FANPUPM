<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaderView.aspx.cs" Inherits="oa_WorkPlanAndSummary_LeaderView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script type="text/javascript" src="/Script/jquery.js"></script>

        <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
        <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
        <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
        <script type="text/javascript">
        addEvent(window, 'load', function() {
            addEvent(document.getElementById('btnCheck'), 'click', CheckPage);
            var jwtable = new JustWinTable('GvShowPlan');

        });
        function CheckPage() {
            parent.desktop.CheckPlan = window;
            var url = '/oa/WorkPlanAndSummary/CheckPlan.aspx?WkpId='+document.getElementById('hdnWkpId').value;
            toolbox_oncommand(url, "工作计划审核");
        }
        function rowClick(result,WkpId) {
            if (result == "1") {
                document.getElementById('btnCheck').disabled = true;
            } else {
                document.getElementById('btnCheck').disabled = false;
                document.getElementById('hdnWkpId').value = WkpId;
            }
            //alert(document.getElementById('hdnWkpId').value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="tab">
       <tr>
        <td class="divHeader">工作计划审核</td>
       </tr>
       <tr>
        <td style="text-align:left;" class="divFooter" >
        <input type="button" id="btnCheck" value="审核" disabled="disabled"/>
        <input type="button" id="btnComment" value="点评" disabled="disabled"/>
        </td>
      </tr>  
      <tr> 
        <td valign="top">
        <div class="pagediv">
                <asp:GridView ID="GvShowPlan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" Width="100%" PageSize="25" OnRowDataBound="GvShowPlan_RowDataBound" OnPageIndexChanging="GvShowPlan_PageIndexChanging" DataKeyNames="WkpId,WkpIsReport" runat="server">
<EmptyDataTemplate>
                <table class="gvdata" cellspacing="0" rules="all" border="0" cellpadding="0" style="border-collapse:collapse;">
                <tr>
                <th>序号</th>
                <th>计划编号</th>
                <th>填报人</th>
                <th>填报人部门</th>
                <th>计划类型</th>
                <th>是否合格</th>
                <th>填报日期</th>
                </tr>
                </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
                    <%# Eval("WkpUserCode") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="填报人"><ItemTemplate>
                    <%# new MainPlanAndAction().BackUserName(Eval("WkpReportUser").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="填报人部门"><ItemTemplate>
                    <%# new MainPlanAndAction().BackDept(Eval("WkpDeptId").ToString()) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划类型"><ItemTemplate>
                    <%# (Eval("WkpReportType").ToString() == "0") ? "周计划" : "月计划" %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="计划状态" DataField="wkpistrue" /><asp:BoundField HeaderText="填报日期" DataField="WkpRecordDate" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
        </td>    
       </tr>
    </table>
    <input id="hdnWkpId" type="hidden" runat="server" />

    </form>
</body>
</html>
