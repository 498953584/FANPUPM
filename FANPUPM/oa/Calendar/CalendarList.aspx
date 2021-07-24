<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarList.aspx.cs" Inherits="oa_Calendar_CalendarList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>   
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div><table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            
            <tr>
                <td >
                <div id="dvgv" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                     <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="sqlBulletin" CssClass="grid" Width="100%" PageSize="6" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:70px;">添加日期</th><th scope="col">标 题</th><th scope="col" style="width:70px;">详细内容</th><th scope="col" style="width:120px;">是否提醒</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="添加日期" SortExpression="RecordDate" HtmlEncode="false" /><asp:BoundField DataField="Title" HeaderText="标 题" SortExpression="Title" /><asp:BoundField DataField="Content" HeaderText="详细内容" SortExpression="Content" /><asp:BoundField DataField="IsRemind" HeaderText="是否提醒" SortExpression="IsRemind" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr style="display:none">
            <td >
                    &nbsp;<asp:SqlDataSource ID="sqlBulletin" SelectCommand="SELECT [RecordID], [UserCode], [RecordDate], [Title], [InfoGuid], [IsRemind], [Content] FROM [OA_Calendar_Info] WHERE (([InfoGuid] = @InfoGuid) AND ([UserCode] = @UserCode))" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="InfoGuid" QueryStringField="ig" Type="String"></asp:QueryStringParameter><asp:SessionParameter Name="UserCode" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                </td>  
                </tr>   
        </table><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    
    </div>
    </form>
</body>
</html>
