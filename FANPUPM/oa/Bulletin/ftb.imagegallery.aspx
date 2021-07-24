<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ftb.imagegallery.aspx.cs" Inherits="ftb_imagegallery" %>
<asp:Panel ID="MainPage" Visible="false" runat="server">
<!doctype html public "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<HEAD>
<META HTTP-EQUIV="Expires" CONTENT="0">
<title>插入图片</title>
<style>

body {
	margin: 0px 0px 0px 0px;
	padding: 0px 0px 0px 0px;
	background: #ffffff; 
	width: 100%;
	overflow:hidden;
	border: 0;
}

body,tr,td {
	color: #000000;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 10pt;
}

div.imagespacer {
	width: 120;
	height: 126;
	text-align: center;			
	float: left;
	font: 10pt verdana;
	margin: 5px;
	overflow: hidden;
}
div.imageholder {
	margin: 0px;
	padding: 0px;
	border: 1 solid #CCCCCC;
	width: 100;
	height: 100;
}

div.titleholder {
	font-family: ms sans serif, arial;
	font-size: 8pt;
	width: 100;
	text-overflow: ellipsis;
	overflow: hidden;
	white-space: nowrap;			
}		

</style>


<script language="javascript">
lastDiv = null;
function divClick(theDiv,filename) {
	if (lastDiv) {
		lastDiv.style.border = "1 solid #CCCCCC";
	}
	lastDiv = theDiv;
	theDiv.style.border = "2 solid #316AC5";
	
	document.getElementById("FileToDelete").value = filename;

}
function gotoFolder(rootfolder,newfolder) {
	window.navigate("ftb.imagegallery.aspx?frame=1&rif=" + rootfolder + "&cif=" + newfolder);
}		
function returnImage(imagename,width,height) {
	var arr = new Array();
	arr["filename"] = imagename;  
	arr["width"] = width;  
	arr["height"] = height;			 
	window.parent.returnValue = arr;
	window.parent.close();	
}		
</script>		
</HEAD>
<body>
<table width=100% height=100% cellpadding=0 cellspacing=0 border=0>

<form enctype="multipart/form-data" runat="server">

<tr><td>
	<div id="galleryarea" style="width=100%; height:100%; overflow: auto;">
		<asp:Label ID="gallerymessage" runat="server"></asp:Label>
		<asp:Panel ID="GalleryPanel" runat="server"></asp:Panel>
	</div>
</td></tr>
<asp:Panel ID="UploadPanel" runat="server">
<tr><td height=16 style="padding-left:10px;border-top: 1 solid #999999; background-color:#99ccff;">
	
	<table>
	<tr>
		<td valign=top><input id="UploadFile" type="file" name="UploadFile" style="width:300;" runat="server" />
</td>
		<td valign=top><asp:Button ID="UploadImage" Text="上传" runat="server" /></td>
		<td valign=top><asp:Button ID="DeleteImage" Text="删除" runat="server" /></td>
		<td valign=middle>		
	</tr>
	<tr>
		<td colspan=3>
			<asp:RegularExpressionValidator ControlToValidate="UploadFile" ID="FileValidator" Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
			<asp:Literal ID="ResultsMessage" runat="server"></asp:Literal>		
		</td>		
	</tr></table>	
	<input type="hidden" id="FileToDelete" value="" runat="server" />

	<input type="hidden" id="RootImagesFolder" value="images" runat="server" />

	<input type="hidden" id="CurrentImagesFolder" value="images" runat="server" />

</td></tr>
</asp:Panel>
</form>
</table>
</body>
</HTML>
</asp:Panel>
<asp:Panel ID="iframePanel" runat="server">
<html> 
<head><title>插入图片</title></head>
<style>
body {
	margin: 0px 0px 0px 0px;
	padding: 0px 0px 0px 0px;
	background: #ffffff;
	overflow:hidden;
}
</style>
<body>
	<iframe style="width:100%;height:100%;border:0;" border=0 frameborder=0 src="ftb.imagegallery.aspx?frame=1&<%=Request.QueryString %>"></iframe>
</body>
</html>
</asp:Panel>
