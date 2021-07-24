<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemInfoAdd.aspx.cs" Inherits="oa_System_SystemInfoAdd" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <script type="text/javascript" language="javascript">
    <!--
   window.name = "win";
    function UpFile()
	{
	    //t=3 制度管理
	    var RecordCode = document.getElementById('hdnRecordId').value;//编号	 
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid=3&rc="+RecordCode+"&at=0&type=2";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
       -->
    </script>
    <title>制度管理</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" border="1" width="100%">
      <tr>
			<td class="td-title" colSpan="2" height="20">
                <asp:Label ID="LbTitle" Text="Label" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnRecordId" runat="server" />
                制度管理</td>
		</tr>  
		<tr>
			<td class="td-label" style="width: 76px">
                制度名称</td>
            <td>
                <asp:TextBox ID="txtSystemName" CssClass="text-input" Width="418px" runat="server"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="td-label" style="width: 76px">
                制定日期</td>
            <td>
                <JWControl:DateBox ID="DBSignDate" runat="server"></JWControl:DateBox>
                </td>
		</tr>
         <tr>
             <td class="td-label" style="width: 76px">
                 制定人</td>
             <td>
                 <asp:TextBox ID="txtSignMan" runat="server"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="td-label" style="width: 76px">
                 现行制度</td>
             <td>
                 <input id="chkNowSystem" type="checkbox" runat="server" />
</td>
         </tr>
		<tr>
			<td align="right" class="td-label" style="width: 76px">附件：</td>
			<td >
                <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
			</td>
		</tr>
		
		<tr>
			<td class="td-label" style="width: 76px">
                情况说明</td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" Rows="3" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox></td>
		</tr> 
	<tr>
		<td  align="center" colspan="2" class="td-submit">
				<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="取 消" runat="server" />
&nbsp;
            &nbsp;&nbsp;&nbsp;
            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSystemName" Display="None" ErrorMessage="制度名称不能为空!" runat="server"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="DBSignDate" Display="None" ErrorMessage="制定日期名称不能为空!" runat="server"></asp:RequiredFieldValidator></td>
		</tr>
      </table>
    
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
