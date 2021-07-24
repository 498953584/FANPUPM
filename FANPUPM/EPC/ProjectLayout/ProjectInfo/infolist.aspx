<%@ Page Language="C#" AutoEventWireup="true" CodeFile="infolist.aspx.cs" Inherits="InfoList" EnableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>InfoList</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />


    <script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>

    <meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />


    <script language="javascript">
		
		function clickRow(obj,type,PrjState,delname)
			{
				//alert(PrjState);
				document.getElementById('hdnID').value = obj;
				document.getElementById('hdndelname').value = delname;
				if(type!="SEE")
				{
				document.getElementById('btnEdit').disabled = false;
				document.getElementById('BtnDel').disabled = true;
				}
				document.getElementById('btnSee').disabled = false;

				//市场、投标、中标管理

				if(type=="-3")
				{
					if(PrjState!="-3")
					{
					document.getElementById('btnEdit').disabled = true;
					document.getElementById('BtnDel').disabled = true;
					}else
					{
					document.getElementById('BtnDel').disabled = false;
					}
				}
				if(type=="-2")
				{
					if(PrjState=="-3"||PrjState=="-2")
					{
					    document.getElementById('btnEdit').disabled = false;
					}else
					{
					    document.getElementById('btnEdit').disabled = true;
					}
					document.getElementById('BtnDel').disabled = false;
				}
				if(type=="-1")
				{
					if(PrjState=="-2"||PrjState=="-1")
					{
					document.getElementById('btnEdit').disabled = false;

					}else
					{
					document.getElementById('btnEdit').disabled = true;
					}
						document.getElementById('BtnDel').disabled = true;
				
				}
//				if(PrjState>"0")
//				{
//				document.getElementById('BtnDel').disabled = false;
//				}
			}
			function WinLoad(obj,Type)
			{
				var Optype = obj;
				var ID = document.getElementById('hdnID').value;
				var url = ""
				if(obj != "SEE")
				{
					url = "InfoEdit.aspx?OpType="+Optype+"&Code="+ID+"&Type="+Type;
				}
				else
				{
					url = "InfoView.aspx?OpType="+Optype+"&Code="+ID;
				}
				return window.showModalDialog(url,window,"dialogHeight:580px;dialogWidth:840px;center:1;help:0;location=0;");
			}
			function WinLoad1(ID) {
			    url = "InfoView.aspx?OpType=SEE&Code=" + ID;
			    return window.showModalDialog(url, window, "dialogHeight:580px;dialogWidth:840px;center:1;help:0;status:0;");
			}
			function pagesetup_null()
			{
                try{
                    var RegWsh = new ActiveXObject("wb.Shell");
                    hkey_key="\header";
                    RegWsh.RegWrite(hkey_root+hkey_path+hkey_key,"");
                    hkey_key="\footer";
                    RegWsh.RegWrite(hkey_root+hkey_path+hkey_key,"");
                    }
                    catch(e)
                    {}
            }

			function printpreview()
			{ 
			 wb.execwb(7,1); //调用ie打印
            } 
            function adminDel()
            {
                url = "../../../EPC/UserControl1/TwoPassSet.aspx?tt=1";
				return window.showModalDialog(url,window,"dialogHeight:135px;dialogWidth:260px;center:1;status:no;scroll:yes;help:no");
            }    
    </script>

    <object id="wb" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"
        name="wb">
    </object>
</head>
<body class="body_frame" scroll="no" onload="pagesetup_null();">
    <form id="Form1" method="post" runat="server">
    <table class="table-none" id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="1" runat="server"><tr class="td-toolsbar" runat="server"><td style="font-size: 9pt; line-height: 100%;" align="left" runat="server">
                <img src="../../../images/toolahead.jpg" align="absmiddle" />项目信息
            </td><td runat="server">
            项目编号：<asp:TextBox ID="txtprjCodeQ" Width="150px" runat="server"></asp:TextBox>
            项目名称：<asp:TextBox ID="txtprjNameQ" Width="150px" runat="server"></asp:TextBox>
                <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
            </td><td height="23px" style="font-size: 9pt; line-height: 100%;" runat="server">
                <input id="HdnType" style="width: 56px; height: 22px" type="hidden" size="4" name="Hidden1" runat="server" />

                <input id="hdnID" type="hidden" runat="server" />

                <input id="hdndelname" type="hidden" runat="server" />

                <asp:Button ID="Button1" Text="新 增" OnClick="Button1_Click" runat="server" />
                <asp:Button ID="btnEdit" Text="编 辑" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                <asp:Button ID="BtnDel" Text="删 除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
                <asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
                <asp:Button ID="btnSee" Text="查 看" Enabled="false" runat="server" />
                <asp:Button ID="btndelAdmin" Text="超级删除" Width="0px" OnClick="btndelAdmin_Click" runat="server" />
            </td></tr><tr runat="server"><td valign="top" colspan="3" height="100%" runat="server">
                <div class="gridBox">
                    <asp:DataGrid ID="DataGrid1" CssClass="grid" Width="100%" AutoGenerateColumns="false" DataKeyField="PrjGuid" AllowPaging="true" PageSize="22" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn Visible="false"><ItemTemplate>
                                    <input id="SelectRow" type="radio" name="SelectRow">
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                    <%# Container.ItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="prjCode" HeaderText="项目编号"></asp:BoundColumn><asp:HyperLinkColumn DataTextField="prjName" HeaderText="项目名称"></asp:HyperLinkColumn><asp:BoundColumn DataField="Owner" HeaderText="建设单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjTypeName" HeaderText="项目类型"></asp:BoundColumn><asp:BoundColumn DataField="PrjCost" HeaderText="造价(万元)"></asp:BoundColumn><asp:BoundColumn HeaderText="项目地点" DataField="PrjPlace"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="State" HeaderText="状态" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
                </div>
                <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
            </td></tr></table>
    </form>
</body>
</html>
