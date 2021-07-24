<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costinputedit.aspx.cs" Inherits="CostInputEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns:cc3>
<head runat="server"><title>其它成本录入</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />

    <!-- 清除页面缓存-->
    <base target="_self">

        <script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>

        <script language="javascript">
		var objTr;
		function GetOneCBSCode(txtchecker)
		{
			var url = "/EPC/Basic/common/selectcbssengle.aspx";
			var ArrReturn = window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:580px;center:1;help:0;status:0;");
			//var ArrReturn = window.open(url);
			if(ArrReturn != null)
			{
				txtchecker.parentElement.parentElement.children[2].children[0].value=ArrReturn[1];
				txtchecker.parentElement.parentElement.children[3].children[0].value=ArrReturn[0];
			}
		}
		
		function clickRow(ItemIndex,obj)
		{
			document.getElementById('btnEdit').disabled = false;
			document.getElementById('btnDel').disabled = false;
			document.getElementById('hdnItemIndex').value = ItemIndex;
			objTr = obj;
		}
		
		function doWith()
		{
			//仅仅是为了点击“删除”时不弹出对话框
			objTr.children[0].children[0].value='1';
			objTr.children[2].children[0].value='1';
		}
		
		//人员选择
		function pickOnePerson()
		{
			var human = new Array();
			human[0] = "";
			human[1] = "";
			var url= "/CommonWindow/PickSinglePerson.aspx";
			window.showModalDialog(url,human,"dialogHeight:434px;dialogWidth:400px;center:1;help:0;status:0;");
			if (human[0]!="")
			{
				document.getElementById('HdnPerson').value = human[0];
				document.getElementById('TxtPerson').value = human[1];
			}
		}
        </script>
</head>
<body class="body_popup" scroll="no">
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table class="table-normal" id="Table1" height="100%" width="100%" border="1">
                <tr>
                    <td style="height: 80px">
                        <table class="table-form" id="Table2" cellspacing="1" cellpadding="1" width="100%"
                            border="1">
                            <tr height="22">
                                <td class="td-head" colspan="4" id="TD1">
                                    成本录入</td>
                            </tr>
                            <tr>
                                <td class="td-toolsbar" colspan="4">
                                    <asp:Button ID="btn_save" Text="保 存" OnClick="btn_save_Click" runat="server" /><font
                                        face="宋体">&nbsp;</font><input onclick="window.returnValue=false;window.close();"
                                            type="button" value="关 闭"></td>
                            </tr>
                            <tr>
                                <td class="td-label" style="width: 17%">
                                    费用项目</td>
                                <td class="text-input" style="width: 36%">
                                    <asp:TextBox ID="txtItemName" MaxLength="50" Width="100%" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="费用项目名称必填！" ControlToValidate="txtItemName" Display="None" runat="server"></asp:RequiredFieldValidator></td>
                                <td class="td-label" style="width: 17%">
                                    发生时间</td>
                                <td class="text-input" style="width: 30%">
                                    <JWControl:DateBox ID="dbDate" runat="server"></JWControl:DateBox></td>
                            </tr>
                            <tr>
                                <td class="td-label" style="width: 17%">
                                    发生单位</td>
                                <td class="text-input" style="width: 36%">
                                    <asp:TextBox ID="txtDept" MaxLength="50" Width="100%" runat="server"></asp:TextBox></td>
                                <td class="td-label" style="width: 17%">
                                    填报人</td>
                                <td class="text-input" style="width: 30%">
                                    <asp:TextBox ID="txtUser" MaxLength="10" Width="100%" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="td-label" style="width: 17%">
                                    经手人</td>
                                <td colspan="3" class="text-input" style="width: 36%">
                                      <input type="text" readonly="true" class="txtreadonly" style="background-color: #ffffc0" id="TxtPerson" maxlength="100" columns="60" runat="server" />

                                    <img
                                        style="cursor: hand; valign: bottom" onclick="pickOnePerson();"  src="/Images/corp.gif" align=absmiddle
                                        width="16" border="0"><input id="HdnPerson" type="hidden" name="HdnPerson" style="width: 20px" runat="server" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" ControlToValidate="TxtPerson" ErrorMessage="经手人必须选择!" runat="server"></asp:RequiredFieldValidator></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td-toolsbar">
                        <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />&nbsp;
                        <asp:Button ID="btnEdit" Text="编 辑" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;
                        <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" /><input id="hdnItemIndex" type="hidden" name="Hidden1" runat="server" />
<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                    </td>
                </tr>
                <tr>
                    <td valign="top" height="100%">
                        <div style="overflow: auto; width: 100%; height: 100%">
                            <asp:DataGrid ID="dgCostInputSlave" Width="100%" PageSize="8" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid" DataKeyField="RecordID" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn Visible="false" DataField="ChildID" ReadOnly="true" HeaderText="成本明细ID"></asp:BoundColumn><asp:TemplateColumn HeaderText="名称">
<ItemTemplate>
                                            <asp:Label ID="lblFeeName" Text='<%# Convert.ToString(Eval("ItemName")) %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<EditItemTemplate>
                                            <asp:TextBox ID="txtFeeName" Width="100%" Text='<%# Convert.ToString(Eval("ItemName")) %>' runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="费用名称必填！" ControlToValidate="txtFeeName" Display="None" runat="server"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="金额(元)">
<ItemTemplate>
                                            <asp:Label ID="lblPrice" Text='<%# Convert.ToString(Eval("Price")) %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<EditItemTemplate>
                                            <asp:TextBox ID="txtPrice" Width="100%" Text='<%# Convert.ToString(Eval("Price")) %>' runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="金额格式不正确" ControlToValidate="txtPrice" ValidationExpression="^\d+(\.\d+)?$" Display="None" runat="server"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="成本科目">
<ItemTemplate>
                                            <asp:Label ID="lblCostItem" Text='<%# Convert.ToString(Eval("CBSName")) %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<EditItemTemplate>
                                            <input type="text" class="txtreadonly" id="txtCostItem" ondblclick="GetOneCBSCode(this);" style="background-color: #ffffc0;width:100%;" title="请双击此处选择!" value='<%# Convert.ToString(Eval("CBSName")) %>' runat="server" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="成本科目必选！" ControlToValidate="txtCostItem" Display="None" runat="server"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="科目编码">
<ItemTemplate>
                                            <asp:Label ID="lblCostItemCode" Text='<%# Convert.ToString(Eval("CostCode")) %>' runat="server"></asp:Label>
                                        </ItemTemplate>

<EditItemTemplate>
                                            <asp:TextBox ID="txtCostItemCode" Width="100%" Text='<%# Convert.ToString(Eval("CostCode")) %>' runat="server"></asp:TextBox>
                                        </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
                                            <asp:Label ID="lblContent" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
                                        </ItemTemplate><EditItemTemplate>
                                            <asp:TextBox ID="txtContent" Width="100%" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:TextBox>
                                        </EditItemTemplate></asp:TemplateColumn><asp:BoundColumn Visible="false" DataField="RecordID" ReadOnly="true" HeaderText="成本主表Guid"></asp:BoundColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid></div>
                    </td>
                </tr>
            </table>
        </font>
    </form>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
