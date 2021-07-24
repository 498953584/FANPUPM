<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocDispense.aspx.cs" Inherits="DocDispense" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>公文分发</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"></base>
	<script language="javascript">		
    function checkAll(obj)  //全选
    {   
        var num = document.all.tags("input").length;
        var str_temp;
        //设置复选框
        for(var i=0; i<num; i++)
        {
            str_temp = document.all.tags("input")[i].id;
            if(str_temp.substr(str_temp.length-9,9) == 'CheckBox1')
            {
                document.all.tags("input")[i].checked = obj.checked;
            }
        }
    }   
    </script>
</head>
<body scroll="no" class="body_popup">
    <form id="Form1" method="post" runat="server">
    <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">分子机构列表</TD>
    </tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
				<asp:DataGrid ID="dgCorpCode" CssClass="grid" DataKeyField="corpCode" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgCorpCode_ItemDataBound" OnSelectedIndexChanged="dgCorpCode_SelectedIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CorpCode" HeaderText="分子机构编码"></asp:BoundColumn><asp:BoundColumn DataField="CorpName" HeaderText="分子机构名称"></asp:BoundColumn><asp:TemplateColumn>
<HeaderTemplate>
							<asp:CheckBox ID="chkAll" onclick="javascript:checkAll(this);" AutoPostBack="false" ToolTip="Select/Deselect All" runat="server" />
							</HeaderTemplate>

<ItemTemplate>
								<asp:CheckBox ID="CheckBox1" AutoPostBack="false" runat="server" />
							</ItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
			</div>
		</TD>
    </tr>
    <tr>
		<td class="td-submit"><asp:Button ID="btnSave" Text="保 存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" /><input class="button-normal" id="btnClose" onclick="window.close();" type="button" value="关 闭">
		</td>
	</tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
