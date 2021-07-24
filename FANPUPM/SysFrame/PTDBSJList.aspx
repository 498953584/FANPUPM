<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PTDBSJList.aspx.cs" Inherits="SysFrame_PTDBSJList" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>待办工作</title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#GVDBSJ a').click(function () {
                var id = $(this).parent().parent().attr('id');
                deleteDbsj(id);
            });
        });

        function deleteDbsj(id) {
            var url = '/TableTop/Handler/UpdateTopFlag.ashx?' + new Date().getTime() + '&modelId=' + 2801 + '&pk=' + id;
            $.ajax({
                type: 'GET',
                async: true,
                url: url
            });
        }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" style="width: 100%; height: 100%"
            align="center" border="0" class="table-normal">
            <tr>
                <td class="td-title">
                    待办工作
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvDeptBox" class="div-scroll" style="width: 100%; height: 100%">
                        <asp:GridView ID="GVDBSJ" AutoGenerateColumns="false" PageSize="15" ShowHeader="false" Width="95%" AllowPaging="true" GridLines="None" OnRowDataBound="GVDBSJ_RowDataBound" OnPageIndexChanging="GVDBSJ_PageIndexChanging" DataKeyNames="I_DBSJ_ID" runat="server"><Columns><asp:TemplateField></asp:TemplateField><asp:TemplateField>
<ItemTemplate>
                                     <asp:LinkButton ID="lblContent" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle Font-Size="Large"></RowStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
