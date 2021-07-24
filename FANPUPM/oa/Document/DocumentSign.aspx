<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentSign.aspx.cs" Inherits="DocumentSign" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>公文签收</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<script language="javascript">
	function doClickRow(DocumentType,fileID)
	{
		document.getElementById('btnView').disabled = false;
		document.getElementById('hdnFileID').value=fileID;
		document.getElementById('hdnDocumentType').value=DocumentType;
	}	
    function checkAll(obj)  //全选
    {   
        var num = document.all.tags("input").length;
        var str_temp;
        //设置复选框
        for(var i=0; i<num; i++)
        {
            str_temp = document.all.tags("input")[i].id;
            if(str_temp.substr(str_temp.length-9,9) == 'CheckBox1')
            {
                document.all.tags("input")[i].checked = obj.checked;
            }
        }
    }
    function openClassEdit(corpCode,userCode)
	{	
	    var url= "";		
		var fileID = document.getElementById('hdnFileID').value;
		var DocumentType = document.getElementById('hdnDocumentType').value;
		if (DocumentType==1)
		{
			url= "SendFileEdit.aspx?t=3&code=" + corpCode + "&fid="+ fileID + "&ucd="+userCode;
		}	
		if (DocumentType==2)
		{
			url= "ReceiveFileEdit.aspx?t=3&code=" + corpCode + "&fid="+ fileID + "&ucd="+userCode;
		}
		return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:550px;center:1;help:0;status:0;");
	}
    </script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">文件签收记录</TD>
    </tr>
    <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" runat="server">
			<input type="hidden" id="hdnTemplateID" name="hdnTemplateID" style="WIDTH: 10px" runat="server" />

			<input type="hidden" id="hdnDocumentType" style="WIDTH: 10px" runat="server" />

			<input type="hidden" id="hdnFileID" style="WIDTH: 10px" runat="server" />

            <asp:Button ID="btnView" Enabled="false" Text="查  看" runat="server" />
			<asp:Button ID="btnSign" Text="签  收" OnClick="btnSign_Click" runat="server" /></td></tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
				<asp:DataGrid ID="dgSign" CssClass="grid" DataKeyField="RecordID" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgSign_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
							<asp:CheckBox ID="chkAll" onclick="javascript:checkAll(this);" AutoPostBack="false" ToolTip="Select/Deselect All" runat="server" />
							</HeaderTemplate>

<ItemTemplate>
								<asp:CheckBox ID="CheckBox1" AutoPostBack="false" runat="server" />
							</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="RecordID" HeaderText="记录ID" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="ReceiveDate" HeaderText="接收时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:TemplateColumn HeaderText="发文单位"><ItemTemplate>
								<asp:Label ID="TxtCorpCode" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="主题"><ItemTemplate>
								<asp:Label ID="TxtTitle" Text='<%# Convert.ToString(Eval("Title")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="签收人"><ItemTemplate>
								<asp:Label ID="TxtSignUserName" Text='<%# Convert.ToString(Eval("SignUserName")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="SignDate" HeaderText="签收时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
								<asp:Label ID="TxtRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
			</div>
		</TD>
    </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
