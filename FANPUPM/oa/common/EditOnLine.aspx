<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditOnLine.aspx.cs" Inherits="EditOnLine" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>华厦幸福基业OFFICE文档在线编辑控件</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	    <SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="ntkoocx.js"></SCRIPT>
	</head>
	<body onLoad='javascript:TANGER_OCX_OpenDoc("<%=URL %>")' class="body_popup">
		<center>
			<form id="myForm" method="post" ENCTYPE="multipart/form-data" action="upload.aspx">
				<TABLE  class="table-form" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
				    <tr height="15"><td colspan="3" class="td-head" align="center">华厦幸福基业OFFICE文档在线编辑控件</td></tr>
					<tr height="10">
						<td class="td-label">文件名称：</td>
						<td><input id="filename" name="filename" MAXLENGTH=50 size=50 value="<%=filename %>"></td>
						<td align="right" class="td-submit"><input type="button" id="btnSave" value="保存到Web服务器" style="width : 100px" onclick="TANGER_OCX_SaveEditToServerDisk();" runat="server" />

						<INPUT type="button" id="btnClose" VALUE="关 闭" onclick="javascript:window.close();"></td>
					</tr>
					<tr>
					    <td colspan="3">
				            <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" style="BORDER-RIGHT:#9dc2db 1px solid; BORDER-TOP:#9dc2db 1px solid; BORDER-LEFT:#9dc2db 1px solid; BORDER-BOTTOM:#9dc2db 1px solid">
					            <tr width="100%">
						            <td width="100%">
							            <OBJECT id="TANGER_OCX" codeBase="OfficeControl.cab#version=4,0,0,7" height="100%" width="100%" classid="clsid:C9BC4DFF-4248-4a3c-8A49-63A7D317F404" VIEWASTEXT>
								            <PARAM NAME="_ExtentX" VALUE="18071">
								            <PARAM NAME="_ExtentY" VALUE="20981">
								            <PARAM NAME="BorderColor" VALUE="14402205">
								            <PARAM NAME="BackColor" VALUE="-2147483643">
								            <PARAM NAME="ForeColor" VALUE="-2147483640">
								            <PARAM NAME="TitlebarColor" VALUE="14402205">
								            <PARAM NAME="TitlebarTextColor" VALUE="0">
								            <PARAM NAME="BorderStyle" VALUE="1">
								            <PARAM NAME="Titlebar" VALUE="0">
								            <PARAM NAME="Toolbars" VALUE="1">
								            <PARAM NAME="Caption" VALUE="OFFICE文档在线编辑控件">
								            <PARAM NAME="IsShowToolMenu" VALUE="1">
								            <PARAM NAME="IsNoCopy" VALUE="1">
								            <PARAM NAME="IsHiddenOpenURL" VALUE="0">
								            <PARAM NAME="MaxUploadSize" VALUE="0">
								            <PARAM NAME="Menubar" VALUE="0">
								            <PARAM NAME="Statusbar" VALUE="1">
								            <PARAM NAME="FileNew" VALUE="-1">
								            <PARAM NAME="FileOpen" VALUE="-1">
								            <PARAM NAME="FileClose" VALUE="-1">
								            <PARAM NAME="FileSave" VALUE="-1">
								            <PARAM NAME="FileSaveAs" VALUE="-1">
								            <PARAM NAME="FilePrint" VALUE="-1">
								            <PARAM NAME="FilePrintPreview" VALUE="-1">
								            <PARAM NAME="FilePageSetup" VALUE="-1">
								            <PARAM NAME="FileProperties" VALUE="-1">
								            <PARAM NAME="IsStrictNoCopy" VALUE="0">
								            <PARAM NAME="IsUseUTF8URL" VALUE="0">
								            <PARAM NAME="MenubarColor" VALUE="-2147483643">
								            <PARAM NAME="IsUseControlAgent" VALUE="0">
								            <PARAM NAME="IsUseUTF8Data" VALUE="0">
								            <PARAM NAME="IsSaveDocExtention" VALUE="0">
								            <PARAM NAME="IsDirectConnect" VALUE="0">
								            <PARAM NAME="SignCursorType" VALUE="0">
								            <PARAM NAME="IsResetToolbarsOnOpen" VALUE="0">
								            <PARAM NAME="IsSaveDataIfHasVDS" VALUE="0">
								            <SPAN STYLE="color:red">不能装载文档控件。请在检查浏览器的选项中检查浏览器的安全设置。</SPAN>
							            </OBJECT>
							            <!-- OnDocumentClosed and OnDocumentOpened -->
							            <script language="JScript" for="TANGER_OCX" event="OnDocumentClosed()">
		                                        TANGER_OCX_OnDocumentClosed();
							            </script>
							            <script language="JScript" for="TANGER_OCX" event="OnDocumentOpened(TANGER_OCX_str,TANGER_OCX_obj)">
		                                        TANGER_OCX_OnDocumentOpened(TANGER_OCX_str,TANGER_OCX_obj);
							            </script>
							            <script language="JScript" for="TANGER_OCX" event="OnFileCommand(cmd,canceled)">
	                                            if (cmd == 3) //user has clicked on file save menu or button
	                                            {
		                                            //save to server
		                                            //cancel default process
		                                            //TANGER_OCX_SaveEditToServerDisk();
		                                            //document.all("TANGER_OCX").CancelLastCommand = true;
	                                            }
							            </script>
							            <script language="JScript" for="TANGER_OCX" event="OnSignSelect(issign,signinfo)">
								            if(issign)
								            {
										            TANGER_OCX_OBJ.SetReadOnly(true);
										            TANGER_OCX_OBJ.SetReadOnly(false);
								            }
								            else
								            {
								            }
							            </script>
						            </td>
					            </tr>
				            </table>
				        </td>
				     </tr>
				</TABLE>
			</form>
		</center>
	</body>
</HTML>
