<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projectlist.aspx.cs" Inherits="ProjectList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>��Ŀ�б�</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />


    <script language="javascript">
            window.onload = function() {
            var purchasePlanTable = new JustWinTable('DataGrid1');
            }
			function clickRow(obj,obj2,state)
			{
			    //alert(state);
				document.getElementById('hdnID').value = obj;//guid
				document.getElementById('hdnID2').value = obj2;//code
				document.getElementById('HdnState').value = state;
				switch (state)
				{	
				case 7:
						document.getElementById('BtnOver').disabled = true;
						document.getElementById('BtnStop').disabled = true;
						document.getElementById("rdate").style.display="none";
						break;
				case 4:
						document.getElementById('BtnOver').disabled = false;
						document.getElementById("BtnOver").value = '����';
						document.getElementById('BtnStop').disabled = false;
						document.getElementById('BtnStop').value = 'ͣ��';
						
						document.getElementById("rdate").style.display="block";
						document.getElementById("HidFlag").value="0";
						break;	
						case -1:
				default:
						document.getElementById('BtnOver').disabled = false;
						document.getElementById('BtnOver').value = '�ڽ�';
						document.getElementById('BtnStop').disabled = true;
						
						document.getElementById("rdate").style.display="none";
						document.getElementById("HidFlag").value="1";
						break;				
				
				}
				//document.getElementById('btnEdit').disabled = false;
				//document.getElementById('BtnDel').disabled = false;
				//document.getElementById('btnSee').disabled = false;
				//window.ItemList.location.href = "ItemList.aspx?PrjGuid="+obj+"&PriCode="+obj2;
			}
			function test()
			{
			    if(document.getElementById("txtDate").value==""&&document.getElementById("HidFlag").value=="0")
			    {
			        alert("��ѡ�����ڣ�");
			        return false;
			    }else
			    {
			    return true;
			}}
			
    </script>
    <style type="text/css">
        .dgheader
        {
	        background-color: #EEF2F5;
	        white-space: nowrap;
	        overflow: hidden;
	        font-weight: normal;
	        text-align: center;
	        border-color: #CADEED;
	        color: #727FAA;
	        padding-left: 6px;
	        padding-right: 6px;

	        }
        .style1
        {
            width:60%;
        }
    </style>
</head>
<body >
   
   <form id="Form1" method="post" runat="server">
   <div style="overflow:auto; ">
        <table id="Table1" height="100%" cellspacing="0" cellpadding="0"
            width="100%" border="0" >  
            <tr class="divFooter">
                <td width="70px" style="text-align:left; padding-left:15px">
                    ��Ŀ��Ϣ</td>  
                <td  align="right" style="border-width:0px;" class="style1">
                    <input id="HdnState" style="width: 10px" type="hidden" name="HdnState" runat="server" />
<input id="hdnID2" type="hidden" name="hdnID" style="width: 10px" runat="server" />
<input id="hdnID" type="hidden" name="hdnID" style="width: 10px" runat="server" />
&nbsp;</td>
                <td style="text-align:right; "><span id="rdate" style="display:none;">ѡ�����ڣ�<JWControl:DateBox ID="txtDate" Columns="10" ReadOnly="false" Width="70px" runat="server"></JWControl:DateBox></span></td>
                <td style="border-width:0px; text-align:right; width:120px;">
                    <asp:Button ID="BtnOver" Text="�� ��" OnClick="BtnOver_Click" runat="server" />&nbsp;
                    <asp:Button ID="BtnStop" Text="ͣ ��" OnClick="BtnStop_Click" runat="server" />&nbsp;</td>
                    <asp:HiddenField ID="HidFlag" Value="0" runat="server" />
            </tr>
            <tr>
                <td valign="top" colspan="4" height="100%"  >
                    <div class="pagediv" style="overflow:auto;">
                        <asp:DataGrid ID="DataGrid1" AutoGenerateColumns="false" Width="100%" CssClass="gvdata" DataKeyField="PrjGuid" AllowPaging="true" PageSize="12" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn Visible="false"><ItemTemplate>
                                        <input id="SelectRow" type="radio" name="SelectRow">
                                    </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="���">
<ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="prjCode" HeaderText="��Ŀ���"></asp:BoundColumn><asp:BoundColumn DataField="prjName" HeaderText="��Ŀ����"></asp:BoundColumn><asp:BoundColumn DataField="Owner" HeaderText="��Ŀ��λ"></asp:BoundColumn><asp:BoundColumn DataField="PrjTypeName" HeaderText="��Ŀ����"></asp:BoundColumn><asp:BoundColumn DataField="prjCost" HeaderText="�������(��)"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="��ʼʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="PrjPlace" HeaderText="��Ŀ����"></asp:BoundColumn><asp:BoundColumn DataField="PrjStateRemark" HeaderText="��/ͣ��ʱ��"></asp:BoundColumn><asp:BoundColumn DataField="State" HeaderText="״̬"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
                        </div>
                </td>
            </tr>
        </table>
        </div>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
