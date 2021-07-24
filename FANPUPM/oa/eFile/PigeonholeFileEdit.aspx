<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PigeonholeFileEdit.aspx.cs" Inherits="oa_eFile_PigeonholeFileEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>项目档案管理</title>
 
    <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
    function SelectClass()
    {
       var url = '/CommonWindow/MultiClasses/SelectClassList.aspx?ct=004';       
       var al = new Array();
       al[0] = "";
       al[1] = "";
       window.showModalDialog(url,al,"dialogHeight:450px;dialogWidth:360px;center:1;help:0;status:0;");
       if(al[0]!="")
       {
            document.getElementById("TxtClassName").value = al[1];
            document.getElementById("HdnClassID").value = al[0];
       }
       return false;
    }
       -->
    </script>
	
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        &nbsp;&nbsp;
    <div>
    <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title" colspan="4">档案归档</td>
      </tr>	   
      <tr>
        <td class="td-label">
            归档类型</td>
            <td>
            <asp:TextBox ID="TxtClassName" Enabled="false" runat="server"></asp:TextBox><asp:Button ID="BtnSelect" Text="选择" runat="server" />
                <asp:HiddenField ID="HdnClassID" runat="server" />
            </td>
       </tr>
	    <tr>	    
	     <td class="td-label">
		        档案编号
	        </td>
	        <td >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
		        <asp:TextBox ID="txtFileCode" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="DDLFileCopy" EventName="SelectedIndexChanged" runat="server" /><asp:AsyncPostBackTrigger ControlID="DDLFileType" EventName="SelectedIndexChanged" runat="server" /></Triggers></asp:UpdatePanel>
	        </td>	     
	        <td class="td-label">
		        文档名称
	        </td>
	        <td >
		        <asp:TextBox ID="txtFileTitle" Width="200px" Enabled="false" runat="server"></asp:TextBox>
	        </td>
	       
	      </tr>
	      <tr>
	       <td class="td-label">
		        提交人
	        </td>
	        <td >
		        <asp:TextBox ID="txtSubmitMan" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                <asp:HiddenField ID="HdnSubmitMan" runat="server" />
	        </td>
	         <td class="td-label">
                文件类型</td>
	        <td >
               <asp:DropDownList ID="DDLFileType" AutoPostBack="true" OnSelectedIndexChanged="DDLFileType_SelectedIndexChanged" runat="server"><asp:ListItem Text="文本文件" Value="T" /><asp:ListItem Text="图像文件" Value="I" /><asp:ListItem Text="图形文件" Value="G" /><asp:ListItem Text="影像文件" Value="V" /><asp:ListItem Text="声音文件" Value="A" /><asp:ListItem Text="多媒体文件" Value="M" /><asp:ListItem Text="计算机程序" Value="P" /><asp:ListItem Text="数据文件" Value="D" /></asp:DropDownList></td>
	      </tr>
	      <tr>
	       
	        <td class="td-label">
                文件稿本</td>
	        <td >
                <asp:DropDownList ID="DDLFileCopy" AutoPostBack="true" OnSelectedIndexChanged="DDLFileCopy_SelectedIndexChanged" runat="server"><asp:ListItem Text="草稿性电子文件" Value="M" /><asp:ListItem Text="非正式电子文件" Value="U" /><asp:ListItem Text="正式电子文件" Value="F" /></asp:DropDownList></td>
                 <td class="td-label">
		       密级
	        </td>
	        <td >
               <asp:DropDownList ID="DDLSecretLevel" runat="server"><asp:ListItem Value="1" Text="秘密" /><asp:ListItem Value="2" Text="机密" /><asp:ListItem Value="3" Text="绝密" /></asp:DropDownList></td>
	       </tr>
	    <tr>
	        <td class="td-label">
		        提交时间
	        </td>
	        <td >
                <JWControl:DateBox ID="DBSubmitDate" Enabled="false" runat="server"></JWControl:DateBox></td>
                <td class="td-label">
		        保存期限
	        </td>
	        <td>
		        <asp:TextBox ID="txtSaveLimit" Width="200px" runat="server"></asp:TextBox>
	        </td>
	     
	    </tr>
	    <tr>
	         
	        <td class="td-label">
		        归档时间
	        </td>
	        <td colspan="3">
                <JWControl:DateBox ID="DBRecordDate" runat="server"></JWControl:DateBox></td>
	     </tr>
	    <tr>
	       <td class="td-label">
		        备注
	        </td>
	        <td colspan="3">
		        <asp:TextBox ID="txtRemark" Width="80%" MaxLength="500" Rows="5" TextMode="MultiLine" runat="server"></asp:TextBox>
	        </td>
	    </tr>
	    		<tr>
					<td  align="center" colspan="4" class="td-submit"><asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtClassName" Display="None" ErrorMessage="必须选择文档类型！" runat="server"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="DBRecordDate" Display="None" ErrorMessage="归档时间不能为空！" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
			</table><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
   
    </div>
    </form>
</body>
</html>
