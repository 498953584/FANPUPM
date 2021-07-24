<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckPlan.aspx.cs" Inherits="oa_WorkPlanAndSummary_CheckPlan" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <style type="text/css">
        .divFooter2
        {
	        height: 24px;
	        text-align: right;
	        background: url(/images1/divBottomBack.jpg) repeat-x;
	        vertical-align: middle;
	        position: absolute;
	        bottom: 0px;
	        width: 100%;
        }
        .tableFooter2
        {
	        width: 100%;
	        text-align: right;
        }
    </style>
    <script type="text/javascript">
        function winclose(tobj, url, rebool) {
            if (rebool) {
                parent.desktop[tobj].location = url;
            };
            parent.desktop[tobj] = null;

            top.frameWorkArea.window.desktop.getActive().close();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
          <td class="divHeader">
          工作计划
          </td>
        </tr>
    </table>
    </div>
        <div class="pagediv" style="vertical-align:top; height:96%;">
                <asp:GridView ID="GvShowPlan" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" Width="100%" PageSize="25" RowStyle-Wrap="true" OnRowDataBound="GvShowPlan_RowDataBound" OnPageIndexChanging="GvShowPlan_PageIndexChanging" DataKeyNames="WkpId" runat="server">
<EmptyDataTemplate>
                <table class="gvdata" cellspacing="0" rules="all" border="0" cellpadding="0" style="border-collapse:collapse;">
                <tr>
                <th>序号</th>
                <th>计划内容</th>
                <th>负责人</th>
                <th>责任人</th>
                <th>开始时间</th>
                <th>结束时间</th>
                </tr>
                </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="WkpContents" HeaderText="计划内容" ItemStyle-Wrap="true" ItemStyle-Width="80px" /><asp:TemplateField HeaderText="负责人"><ItemTemplate>
                    <%# Eval("WkpChief").ToString() %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="责任人"><ItemTemplate>
                    <%# Eval("WkpPersons").ToString() %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="开始时间"><ItemTemplate>
                    <center><%# DateTime.Parse(Eval("WkpStartTime").ToString()).ToShortDateString() %></center>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间"><ItemTemplate>
                        <center><%# DateTime.Parse(Eval("WkpEndTime").ToString()).ToShortDateString() %></center>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            <div class="divHeader" style="bottom:24px">
                    <table>
                        <tr>
                            <td>
                            审核结果:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbtnState" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server"><asp:ListItem Value="1" Selected="true" Text="合格" /><asp:ListItem Value="-1" Text="驳回修改" /></asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
            </div>
        </div>
        <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input ID="btnSave" Name="submit" type="submit" Value="保存" OnServerClick="btnSave_Click" runat="server" />
                    
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('CheckPlan','LeaderView.aspx',true)"/>
                </td>
            </tr>
        </table>
    </div>
    <input id="hdnname" type="hidden" runat="server" />

    </form>
</body>
</html>
