<%@ Page Language="C#" AutoEventWireup="true" CodeFile="distributelist.aspx.cs" Inherits="DistributeList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>DistributeList</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

    
    <script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>

    <meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />


    <script language="javascript">
			function clickRow(obj,obj2,obj3)
			{
				document.getElementById('hdnID').value = obj;
				document.getElementById('hdnID2').value = obj2;
				document.getElementById('hdnID3').value = obj3;
				document.getElementById('btnEdit').disabled = false;
				document.getElementById('btnSee').disabled = false;
				document.getElementById('ItemList').src = "ItemList.aspx?PrjGuid="+obj+"&PriCode="+obj2+"&jw="+'<%=this.jw %>';

			}
			function WinLoad(obj)
			{
			
				var Optype = obj;
				var PrjGuid	= document.getElementById('hdnID').value;
				var PrjCode =  document.getElementById('hdnID2').value;
				var Url = "";
				var jw = '<%=this.jw %>';
				if(obj !="SEE")
				{
					Url = "DistributeEdit.aspx?PrjGuid="+PrjGuid+"&opType=Add&PrjCode="+PrjCode+"&jw="+jw;
					location.href = Url;
				}
				else
				{
					Url = "../ProjectInfo/InfoEdit.aspx?OpType=SEE&Code="+PrjGuid+"&PrjCode="+PrjCode+"&jw="+jw;
					return window.showModalDialog(Url,window,"dialogHeight:580px;dialogWidth:840px;center:1;help:0;status:0;");

				}
			}
			function WinLoad1(ID)
			{
				var ID = document.getElementById('hdnID').value;

				url = "../ProjectInfo/InfoView.aspx?OpType=SEE&Code="+ID;
				return window.showModalDialog(url,window,"dialogHeight:580px;dialogWidth:840px;center:1;help:0;status:0;");
			}
			function WinLoad2(ID)
			{
				url = "../ProjectInfo/InfoView.aspx?OpType=SEE&Code="+ID;
				return window.showModalDialog(url,window,"dialogHeight:580px;dialogWidth:840px;center:1;help:0;status:0;");
			}
			
    </script>

</head>
<body class="body_frame" scroll="no"  onload="selrow1('DataGrid1')">
    <form id="Form1" method="post" runat="server">
        <table class="table-none" id="Table1" height="100%" cellspacing="0" cellpadding="0"
            width="100%" border="1">
            <tr class="td-toolsbar">
                <td width="100" align="left">
                    项目信息</td>
                <td align="right">
                    <input id="hdnID3" type="hidden" name="HiddendTime" runat="server" />

                    &nbsp;<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                    <input id="hdnID2" type="hidden" name="hdnID" runat="server" />
<input id="hdnID" type="hidden" name="hdnID" runat="server" />
&nbsp;&nbsp;<input class="button-normal" id="btnEdit" type="button" onclick="WinLoad('EDIT');" value="项目分配" style="display: none" disabled="true" runat="server" />
&nbsp;<input class="button-normal" id="btnSee" onclick="WinLoad1();" type="button" value="查 看" disabled="true" runat="server" />
&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" valign="top" style="height: 50%">
                    <div class="gridBox">
                        <asp:DataGrid ID="DataGrid1" DataKeyField="PrjGuid" CssClass="grid" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="12" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn Visible="false"><ItemTemplate>
                                        <input id="SelectRow" type="radio" name="SelectRow">
                                    </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="prjCode" HeaderText="项目编号"></asp:BoundColumn><asp:HyperLinkColumn DataTextField="prjName" HeaderText="项目名称"></asp:HyperLinkColumn><asp:BoundColumn DataField="Owner" HeaderText="项目单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjTypeName" HeaderText="项目类型"></asp:BoundColumn><asp:BoundColumn DataField="PrjCost" HeaderText="造价(万元)"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn HeaderText="项目地区"></asp:BoundColumn><asp:BoundColumn DataField="State" HeaderText="状态"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
                        
                </td>
            </tr>
            <tr>
                <td height="100%" valign="top" colspan="2">
                    <iframe width="100%" height="100%" frameborder="0" name="ItemList" id="ItemList"
                        scrolling="no" src=""></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
