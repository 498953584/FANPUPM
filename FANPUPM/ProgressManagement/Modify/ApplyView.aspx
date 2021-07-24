<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyView.aspx.cs" Inherits="ProgressManagement_Modify_ApplyView" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>进度计划调整审核查看</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />


    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setIfFrameSrc();
            showTooltip('tooltip', 25);
        });

        function setWidthHeight() {
            $('#ifPlusGantt').height($(this).height()-$('#gvwPlans').height());
        }

        function setIfFrameSrc() {
            var id = getRequestParam('ic');
            loadGantt(id);
        }

        function loadGantt(progressVerId) {
            var url = "/ProgressManagement/Gantt/PlanView.htm?id=" + progressVerId;
            $('#ifPlusGantt').attr("src", url);
        }
    </script>

</head>
<body >
    <form id="form1" runat="server">
    <asp:GridView ID="gvwPlans" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="ProgressVersionId" runat="server"><Columns><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                    <a style="text-decoration: none;" class="tooltip" title='<%# Eval("PrjName") %>'>
                        <%# StringUtility.GetStr(Eval("PrjName"), 25) %></a>
                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划名称"><ItemTemplate>
                    <span style="text-decoration: none;" class="tooltip" title='<%# Eval("VersionName") %>'"'>
                        <%# StringUtility.GetStr(Eval("VersionName"), 25) %></span>
                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" ItemStyle-Width="50px" /><asp:TemplateField HeaderText="原计划名称"><ItemTemplate>
                    <span style="text-decoration: none;" class="tooltip" title='<%# Eval("PVersionName") %>'"'>
                        <%# StringUtility.GetStr(Eval("PVersionName"), 25) %></span>
                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="原版本号" DataField="PVersionCode" ItemStyle-Width="50px" /><asp:BoundField HeaderText="申请人" DataField="UserName" HeaderStyle-Width="50px" /><asp:TemplateField HeaderText="调整原因"><ItemTemplate>
                    <span class="tooltip" title='<%# Eval("Note") %>'>
                        <%# StringUtility.GetStr(Eval("Note"), 25) %></span>
                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="调整日期" HeaderStyle-Width="70px" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    <iframe id="ifPlusGantt" style="width: 100%; height: 90%;" frameborder="0" scrolling="auto">
    </iframe>
    </form>
</body>
</html>
