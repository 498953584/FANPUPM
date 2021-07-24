<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialBackFrame.aspx.cs" Inherits="StockManage_MaterialBack_MaterialBackFrame" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>材料退库</title></head>
<body>
    <form id="form1" runat="server">
    			<table class="tab" border="1" style="border-collapse:collapse;" cellpadding="0" cellspacing="0">
							<tr style="height:100%;">
								<td style="width:200px;" valign="top">
									<div style="border:solid 0px red; width:200px; vertical-align:top; overflow:auto; padding:0px;margin:0px;">
									<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="uproject" runat="server" />
									</div >
								</td>
								<td valign="top" class="pagediv" style="height:100%;  padding-top:0; padding-left:0; margin-top:0; margin-left:0; margin:0">
		<div class="pagediv">						
								<iframe id="InfoList" name="InfoList" frameborder="0" src="#" style="width:100%;height:100%; margin-top:0; margin-left:0; padding-top:0; padding-left:0" runat="server"></iframe></div>
								</td>
							</tr>
						</table>
					
    </form>
</body>
</html>
