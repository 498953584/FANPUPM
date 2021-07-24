<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectFileManageEdit.aspx.cs" Inherits="oa_eFile_ProjectFileManageEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>档案管理</title> 
	<script type="text/javascript" language="javascript">
	<!--
	 window.name = "win";
	function selChange()
		{
		    var opts = document.all("RBSelectPath") ;  

		    if(opts[1].checked) 
		    {
		        document.getElementById('txtFilePath').style.display = "none";
		        document.getElementById('FUFilePath').style.display = "";
		    }
		    else if(opts[2].checked)
		    {
		        document.getElementById('FUFilePath').style.display = "none";
		        document.getElementById('txtFilePath').style.display = "";
		    }
		}
		///判断有没有添加电子档案
	function IsNullValue()
	{
	     var opts = document.all("RBSelectPath") ;
	     try
	     {
	        if(opts[1].checked) 
	        {	        
	            if(document.getElementById('FUFilePath').value == "")
	            {
	                alert('请添加电子档案内容1!');
	                return false;
	            }
	            else
	            {
	                return true;
	            }
	        }
	        else if(opts[2].checked)
	        {	        
	            if(document.getElementById('txtFilePath').value == "")
	            {
	                alert('请添加电子档案内容2!');
	                return false;
	            }	        
	            else
	            {
	                return true;
	            }
	        }  
	     }
	     catch(e)
	     {}
	}
	 function download(url)
	{       
        window.location.href = url;
	}
	-->
	</script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div>
    <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
                <td class="td-title" colspan="4">档案管理</td>
            </tr>	   
            
	    <tr>
	     <td class="td-label">
		        档案编号
	        </td>
	        <td >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
		        <asp:TextBox ID="txtFileCode" Width="200px" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="DDLFileCopy" EventName="SelectedIndexChanged" runat="server" /><asp:AsyncPostBackTrigger ControlID="DDLFileType" EventName="SelectedIndexChanged" runat="server" /></Triggers></asp:UpdatePanel>               
	        </td>	     
	        <td class="td-label">
		        文档名称
	        </td>
	        <td >
		        <asp:TextBox ID="txtFileTitle" Width="200px" MaxLength="100" runat="server"></asp:TextBox>
	        </td>
	       
	      </tr>
	      <tr>
	       <td class="td-label">
		        提交人
	        </td>
	        <td >
		        <asp:TextBox ID="txtSubmitMan" Width="200px" runat="server"></asp:TextBox>
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
               <asp:DropDownList ID="DDLSecretLevel" runat="server"><asp:ListItem Value="1" Text="秘密" /><asp:ListItem Value="2" Text="机密" /></asp:DropDownList></td>
	       </tr>
	    <tr>
	        <td class="td-label">
		        提交时间
	        </td>
	        <td >
                <JWControl:DateBox ID="DBSubmitDate" runat="server"></JWControl:DateBox></td>
                <td class="td-label">
		        保存期限
	        </td>
	        <td>
		        <asp:TextBox ID="txtSaveLimit" Width="200px" MaxLength="25" runat="server"></asp:TextBox>
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
                </td>
	        <td >
                &nbsp;
                <asp:RadioButtonList ID="RBSelectPath" RepeatDirection="Horizontal" onclick="javascript:selChange();" runat="server"><asp:ListItem Selected="true" Value="1" Text="上传附件" /><asp:ListItem Value="2" Text="填写附件地址" /></asp:RadioButtonList></td>
            <td colspan="2" id="Td_Up" runat="server">
                <asp:FileUpload ID="FUFilePath" Width="80%" runat="server" />
                <asp:TextBox ID="txtFilePath" Width="90%" style="display:none" MaxLength="75" runat="server"></asp:TextBox>
              </td>
              <td colspan="2" id="Td_Look" runat="server">
                  &nbsp;<asp:HyperLink ID="HL_Annex" Target="_self" runat="server">附件名</asp:HyperLink>
			<asp:ImageButton ID="IBtn_DelAnnex" ImageUrl="/images/del.gif" OnClick="IBtn_DelAnnex_Click" runat="server" /></td>
	     </tr>
	    <tr>
	       <td class="td-label">
		        请况说明
	        </td>
	        <td colspan="3">
		        <asp:TextBox ID="txtRemark" Width="80%" MaxLength="250" Rows="5" TextMode="MultiLine" runat="server"></asp:TextBox>
	        </td>
	    </tr>
	    		<tr>
					<td  align="center" colspan="4" class="td-submit"><asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
                    </td>
				</tr>
			</table><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
   
    </div>
    </form>
</body>
</html>
