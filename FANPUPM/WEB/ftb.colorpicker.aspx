<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ftb.colorpicker.aspx.cs" Inherits="ftb_colorpicker" %>

<html>
<head runat="server"><title>拾色器</title>
<style>
.cc {
	width:10;height:8;
}
</style>
<script launguage="JavaScript">
function colorOver(theTD) {
	previewColor(theTD.style.backgroundColor);
	setTextField(theTD.style.backgroundColor);
}
function colorClick(theTD) {
	setTextField(theTD.style.backgroundColor);
	returnColor(theTD.style.backgroundColor);
}
function setTextField(ColorString) {
	document.getElementById("ColorText").value = ColorString.toUpperCase();
}
function returnColor(ColorString) {
	window.returnValue = ColorString;
	window.close();	
}		
function userInput(theinput) {
	previewColor(theinput.value);
}
function previewColor(theColor) {
	try {
		PreviewDiv.style.backgroundColor = theColor;
	} catch (e) {
	}
}
</script>		
</head>

<body style="background-color:d4d0c8; margin: 2 2 2 2;">
<form runat="server">

<table cellpadding=0 cellspacing=0 border=0>
<tr><td colspan=3>
	<asp:Literal ID="Colors" EnableViewState="false" runat="server"></asp:Literal>
</td></tr>
<tr>	
	<td><input type="text" name="ColorText" id="ColorText" style="width:60;height:22;" onkeyup="userInput(this);"></td>
	<td align=center><div id="PreviewDiv" style="width:50;height:20;border: 1 solid black; background-color: #ffffff;">&nbsp;</div></td>
	<td align=right><input type="button" value="确定" onclick="returnColor(ColorText.value);" id="ColorButton"  style="width:80;"></td>
</tr>
</table>

</form>
</body>
</html>
