<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanView.aspx.cs" Inherits="ProgressManagement_Plan_PlanView" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>进度计划流程审核查看</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />


    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setIfFrameSrc();
            showTooltip('tooltip', 25);
        });

        function setIfFrameSrc() {
            //计划版本Id
            var progressVerId = '';
            var id = getRequestParam('ic');
            $.ajax({
                type: "GET",
                dataType: "text",
                async: false,
                url: "../../ProgressManagement/Handler/GetProgressVerId.ashx?" + new Date().getTime() + '&id=' + id,
                success: function(data) {
                    progressVerId = data;
                    loadGantt(progressVerId);
                },
                error: function() {
                    alert("error");
                }
            });
        }

        function loadGantt(progressVerId) {
            var url = "/ProgressManagement/Gantt/PlanView.htm?id=" + progressVerId;
            $('#ifPlusGantt').attr("src", url);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divBudget" class="pagediv" style="overflow: auto; width: 100%; height: 100%;">
        <asp:GridView ID="gvwPlans" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwPlans_RowDataBound" DataKeyNames="ProgressId" runat="server"><Columns><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="320px"><ItemTemplate>
                        <a style="text-decoration: none;" class="tooltip" title='<%# Eval("PrjName") %>'>
                            <%# StringUtility.GetStr(Eval("PrjName"), 25) %></a>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划名称" HeaderStyle-Width="320px"><ItemTemplate>
                        <a style="text-decoration: none;" class="tooltip" title='<%# Eval("ProgressName") %>'"'>
                            <%# StringUtility.GetStr(Eval("ProgressName"), 25) %></a>
                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="版本号" DataField="VersionCode" /><asp:BoundField HeaderText="主计划" DataField="Main" HeaderStyle-Width="30px" /><asp:BoundField HeaderText="录入人" DataField="UserName" /><asp:BoundField HeaderText="录入日期" HeaderStyle-Width="80px" DataField="InputDate" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        <iframe id="ifPlusGantt" style="width: 100%; height: 90%;" frameborder="0" scrolling="auto">
        </iframe>
    </div>
    </form>
</body>
</html>
