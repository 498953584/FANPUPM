<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckView.aspx.cs" Inherits="EPC_EquipmentManagement_Check_CheckView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备检定提醒</title></head>
<body class="body_clear" scroll="auto">
    <form id="form1" runat="server">
        <table class="table-form" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="1">
	        <tr>
		        <td class="td-head" colSpan="4">机械器具查看</td>
	        </tr>
	        <tr>
		        <td class="td-label" width="20%">机械器具编号：</td>
		        <td width="30%">
                    <asp:Label ID="lblcode" Text="Label" runat="server"></asp:Label>
                </td>
		        <td class="td-label" width="20%">机械器具：</td>
		        <td width="30%">
                    <asp:Label ID="lblName" Text="Label" runat="server"></asp:Label>
                </td>
	        </tr>
	        <tr>
		        <td class="td-label">机械器具类型：</td>
		        <td><asp:Label ID="lblType" Text="Label" runat="server"></asp:Label>
                </td>
		        <td class="td-label">规格型号：</td>
		        <td>
                    <asp:Label ID="lblSp" Text="Label" runat="server"></asp:Label>
                </td>
	        </tr>
	        <tr>
		        <td class="td-label">精&nbsp;&nbsp; &nbsp;度：</td>
		        <td>
                    <asp:Label ID="lbljingdu" Text="Label" runat="server"></asp:Label>
                </td>
		        <td class="td-label">出厂编号：</td>
		        <td>
                    <asp:Label ID="lblchuchang" Text="Label" runat="server"></asp:Label>
                </td>
	        </tr>
	        <tr>
		        <td class="td-label">制造厂家：</td>
		        <td>
                    <asp:Label ID="lblmader" Text="Label" runat="server"></asp:Label>
                </td>
		        <td class="td-label">折旧率：
		        </td>
		        <td>
                    <asp:Label ID="lblzeju" Text="Label" runat="server"></asp:Label>
                </td>
	        </tr>
	        <tr>
		        <td class="td-label">出厂日期：</td>
		        <td>
                    <asp:Label ID="lblDatechu" Text="Label" runat="server"></asp:Label>
                </td>
		        <td class="td-label">购置日期：</td>
		        <td>
                    <asp:Label ID="lblBugDate" Text="Label" runat="server"></asp:Label>
                </td>
	        </tr>
	        <tr>
		        <td class="td-label">检定周期：</td>
		        <td>
                    <asp:Label ID="lblzhouqi" Text="Label" runat="server"></asp:Label>(月)
			        </td>
		        <td class="td-label">耐用年限：</td>
		        <td>
                    <asp:Label ID="lblyear" Text="Label" runat="server"></asp:Label>(年)
			        </td>
	        </tr>
	        <tr>
		        <td class="td-label">原&nbsp;&nbsp; &nbsp;值：</td>
		        <td>
                    <asp:Label ID="lblMoney" Text="Label" runat="server"></asp:Label>(元)
			        </td>
		        <td class="td-label">设置状态：</td>
		        <td>
		            <asp:Label ID="lblState" Text="Label" runat="server"></asp:Label>
			        </td>
	        <tr>
		        <td class="td-label">所在单位：</td>
		        <td colSpan=><asp:Label ID="lbldanwei" Text="Label" runat="server"></asp:Label>
                </td>
				        <td class="td-label">
				            使用项目
				        </td>
				        <td><asp:Label ID="lblProject" Text="Label" runat="server"></asp:Label>
				        </td>
	        </tr>
	        </tr>
	        <tr>
		        <td class="td-label">备&nbsp;&nbsp; &nbsp;注：</td>
		        <td colSpan="3">
                    <asp:Label ID="lblRemark" Text="Label" runat="server"></asp:Label>
                </td>
	        </tr>
	        <tr>
		        <td class="td-submit" colSpan="4">
		        <input onclick="window.close();" type="button" value="取 消">
		        </td>
	        </tr>
        </table>
    </form>
</body>
</html>
