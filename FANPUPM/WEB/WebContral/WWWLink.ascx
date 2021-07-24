<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WWWLink.ascx.cs" Inherits="WWWLink" %>

<li style="margin-right:21px;">
<asp:DropDownList ID="DDLWay" CssClass="jump3" Width="150px" runat="server"><asp:ListItem Value="-1" Text="招商信息" /></asp:DropDownList></li>
			<li>
            <asp:DropDownList ID="DDLBrer" CssClass="jump3" Width="150px" runat="server"><asp:ListItem Value="-1" Text="友情链接" /></asp:DropDownList>
			</li>
			
<script language="javascript">
	///网站连接
		function OpenWeb(Addess)
		{
			var URL = Addess.value;
			if(URL != "-1")
			{
			    window.open(URL,"web");
			}
		}
</script>
