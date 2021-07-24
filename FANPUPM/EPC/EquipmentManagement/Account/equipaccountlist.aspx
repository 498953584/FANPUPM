<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipaccountlist.aspx.cs" Inherits="EquipAccountList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>EquipAccountList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />


    <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
<script src="../../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script language="javascript">
			function clickEquipmentRow(equipmentUniqueCode)
			{
				document.getElementById("hdnEquipmentUniqueCode").value = equipmentUniqueCode;
				document.getElementById('btnUpd').disabled = false;
				document.getElementById('btnDel').disabled = false;
			}
			function openEquipmentEdit(opType) {
			    var equipmentUniqueCode = document.getElementById("hdnEquipmentUniqueCode").value;
			    //window.alert(equipmentUniqueCode);
			    if (equipmentUniqueCode == "") {
			        equipmentUniqueCode = "1234";
			    }
			    
			    var url;
			    switch (opType) {
			        case 'add':
			            url = "/EPC/EquipmentManagement/Account/EquipAccountEdit.aspx?type=1&ec=" + equipmentUniqueCode;
			            break;
			        case 'upd':
			            url = "/EPC/EquipmentManagement/Account/EquipAccountEdit.aspx?type=0&ec=" + equipmentUniqueCode;
			            break;
			    }
			    //if (opType == "add") {
			    parent.desktop.flowclass = window; //不可少
			    toolbox_oncommand(url, "机械器具台账"); //引用js
			    //} else {
			    // return window.showModalDialog(url, window, 'dialogHeight:350px;dialogWidth:560px;center:1;help:0;status:0;');
			    //}
			}
			function pickEquipmentType()
			{
				var res = new Array();
				res[0] = "";
				res[1] = "";
				var url= "/EPC/Basic/Resource/ResourceTypeFrame.aspx";
				window.showModalDialog(url,res,"dialogHeight:320px;dialogWidth:500px;center:1;help:0;status:0;");
				//window.open(url);
				if (res[0]!="")
				{
					document.getElementById('hdnEquipmentType').value = res[0];
					document.getElementById('txtEquipType').value = res[1];
				}
			}
    </script>

</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" class="table-none" cellspacing="0" cellpadding="0" width="100%"
            height="100%">
            <tr>
                <td height="22" class="td-title">
                    机械器具台账管理</td>
            </tr>
            <tr>
                <td height="22" class="td-search" align="right">
                状态:
                <asp:DropDownList ID="drpState" runat="server"></asp:DropDownList>&nbsp;
                原值：
                <asp:TextBox ID="TextBox1" Width="88px" runat="server"></asp:TextBox>&nbsp;—
                    <asp:TextBox ID="TextBox2" Width="88px" runat="server"></asp:TextBox>&nbsp;
                    编号：
                    <asp:TextBox ID="txtEquipCode" Width="104px" runat="server"></asp:TextBox>名称：
                    <asp:TextBox ID="txtEquipName" Width="88px" runat="server"></asp:TextBox>&nbsp;类型：
                    <asp:TextBox ID="txtEquipType" Width="107px" runat="server"></asp:TextBox>
                    <span>
                        <img src="../../../images/corp.gif" border="0" onclick="pickEquipmentType();" style="cursor: hand;"
                            align=middle></span>
                    <input id="hdnEquipmentType" style="width: 10px" type="hidden" name="hdnEquipmentType" size="1" runat="server" />

                    <asp:Button ID="btnSearch" CssClass="button_search" OnClick="btnSearch_Click" runat="server" />&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td height="22" class="td-toolsbar" align="right">
                    <input id="hdnEquipmentUniqueCode" style="width: 10px" type="hidden" size="12" name="hdnEquipmentUniqueCode" runat="server" />

                    <asp:Button ID="btnAdd" Text="新 增" CssClass="button-normal" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnUpd" Text="修 改" Enabled="false" CssClass="button-normal" OnClick="btnUpd_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" style=" width:100%；">
                    <div id="dvEquipment" style="overflow: auto; width: 100%; height: 100%">
                        <asp:DataGrid ID="dgEquipment" Width="100%" CssClass="grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><SelectedItemStyle CssClass="grid_row"></SelectedItemStyle><EditItemStyle CssClass="grid_row"></EditItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        <asp:HiddenField ID="HiddenField1" Value='<%# Convert.ToString(Eval("EquipmentUniqueCode")) %>' runat="server" />
                                        <asp:Label ID="Label1" runat="server"><%# Convert.ToString(Container.ItemIndex + 1) %></asp:Label>
                                    </ItemTemplate>

<EditItemTemplate>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="EquipmentManualCode" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="EquipmentName" HeaderText="名称"></asp:BoundColumn><asp:BoundColumn DataField="Spec" HeaderText="规格"></asp:BoundColumn><asp:BoundColumn DataField="EquipmentType" HeaderText="机械器具类型"></asp:BoundColumn><asp:BoundColumn DataField="ThePrecision" HeaderText="精度"></asp:BoundColumn><asp:BoundColumn DataField="Manufacturer" HeaderText="制造厂家"></asp:BoundColumn><asp:BoundColumn DataField="FactoryCode" HeaderText="出厂编号"></asp:BoundColumn><asp:BoundColumn DataField="PurchaseDate" HeaderText="购置日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="OriginalPrice" HeaderText="原值" ReadOnly="true"></asp:BoundColumn><asp:BoundColumn DataField="State" HeaderText="机械器具状态"></asp:BoundColumn><asp:BoundColumn DataField="ContactDept" HeaderText="所在单位"></asp:BoundColumn><asp:BoundColumn DataField="EquipmentUniqueCode" HeaderText="使用项目"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
                        
                        </div>
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
