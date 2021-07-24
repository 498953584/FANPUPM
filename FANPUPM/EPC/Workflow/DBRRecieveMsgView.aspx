<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBRRecieveMsgView.aspx.cs" Inherits="ModuleSet_Workflow_DBRRecieveMsgView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>被告知信息查看</title></head>
<body>
    <form id="form1" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
				<tr>
					<td class="td-title" align="center" colspan="2">被告知信息查看
					</td>
				</tr>
				<tr>
				    <td class="td-label" width="10%">告知人：</td>
				    <td><asp:Label ID="lbRecieveUserCode" runat="server"></asp:Label></td>
				 </tr>
				<tr>
					<td class="td-label" width="10%">
                        被告知人:</td>
					<td width="23%">
						<asp:Label ID="lblv_yhdm" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td class="td-label">业务名称:
					</td>
					<td><asp:Label ID="lblInstanceCode" runat="server"></asp:Label></td>
				</tr>			
				<tr>
				    <td class="td-label">序号:</td>
				    <td>
                        	<asp:Label ID="lblTheOrder" runat="server"></asp:Label>
                    </td>
				 </tr>
				<tr style="height:150px">
					<td class="td-label">被告知内容:</td>
					<td>
					<div id="divcontent" style="width:100%;height:100%; vertical-align:top;" runat="server"></div>					
					</td>
				</tr>
				<tr>
					<td  align="center" colspan="2" class="td-submit">
					<input id="BtnClose" onclick="window.close();" type="button" value="关 闭" style="display:none"  / ></td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
