<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ftb.imagegallery.aspx.cs" Inherits="ftb_imagegallery" %>

<!doctype html public "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head>
		<TITLE>插入图片</TITLE>
		
		<asp:Panel ID="MainPage" Visible="false" runat="server">
			<META http-equiv="Expires" content="0">
			<STYLE>BODY { BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; BACKGROUND: #ffffff; PADDING-BOTTOM: 0px; MARGIN: 0px; OVERFLOW: hidden; BORDER-LEFT: 0px; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: 0px }
	BODY { FONT-SIZE: 10pt; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }
	TR { FONT-SIZE: 10pt; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }
	TD { FONT-SIZE: 10pt; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }
	DIV.imagespacer { FLOAT: left; MARGIN: 5px; FONT: 10pt verdana; OVERFLOW: hidden; WIDTH: 120px; HEIGHT: 126px; TEXT-ALIGN: center }
	DIV.imageholder { BORDER-RIGHT: #cccccc 1px solid; PADDING-RIGHT: 0px; BORDER-TOP: #cccccc 1px solid; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: #cccccc 1px solid; WIDTH: 100px; PADDING-TOP: 0px; BORDER-BOTTOM: #cccccc 1px solid; HEIGHT: 100px }
	DIV.titleholder { FONT-SIZE: 8pt; OVERFLOW: hidden; WIDTH: 100px; FONT-FAMILY: ms sans serif, arial; WHITE-SPACE: nowrap; TEXT-OVERFLOW: ellipsis }
	</STYLE>
			<SCRIPT language="javascript">
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
			</SCRIPT>
	</HEAD>
	<BODY>
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<form enctype="multipart/form-data" runat="server">
				<TBODY>
					<TR>
						<TD height="320">
							<DIV id="galleryarea" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
								<asp:Label ID="gallerymessage" runat="server"></asp:Label>
								<asp:Panel ID="GalleryPanel" runat="server"></asp:Panel></DIV>
						</TD>
					</TR>
					<asp:Panel ID="UploadPanel" runat="server">
						<TR>
							<TD height="18">
								<TABLE>
									<TR>
										<TD vAlign="top"><input id="UploadFile" style="WIDTH: 300px" type="file" name="UploadFile" runat="server" />
</TD>
										<TD vAlign="top">
											<asp:Button ID="UploadImage" CssClass="button-normal" Text="上传" OnClick="UploadImage_OnClick" runat="server" /></TD>
										<TD vAlign="top">
											<asp:Button ID="DeleteImage" CssClass="button-normal" Text="删除" OnClick="DeleteImage_OnClick" runat="server" /></TD>
										<TD vAlign="middle"></TD>
									<TR>
										<TD colSpan="3">
											<asp:RegularExpressionValidator ID="FileValidator" Display="Dynamic" ControlToValidate="UploadFile" runat="server"></asp:RegularExpressionValidator>
											<asp:Literal ID="ResultsMessage" runat="server"></asp:Literal></TD>
									</TR>
								</TABLE>
								<input id="FileToDelete" type="hidden" runat="server" />
 <input id="RootImagesFolder" type="hidden" value="images" runat="server" />

								<input id="CurrentImagesFolder" type="hidden" value="images" runat="server" />

							</TD>
						</TR>
					</asp:Panel></form>
			</TBODY></TABLE>
		</asp:Panel>
		<asp:Panel ID="iframePanel" runat="server"> <!--<html><head><title>插入图片</title></head>-->
			<STYLE> body { margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; background: #ffffff; overflow:hidden; }
</STYLE>
			<IFRAME 
style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; WIDTH: 100%; BORDER-BOTTOM: 0px; HEIGHT: 100%" 
border=0 src="ftb.imagegallery.aspx?frame=1&amp;<%=Request.QueryString %>" 
frameBorder=0>
			</IFRAME>
		</asp:Panel>
	</BODY>
</HTML>
