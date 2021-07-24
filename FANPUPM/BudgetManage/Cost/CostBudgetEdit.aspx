<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostBudgetEdit.aspx.cs" Inherits="BudgetManage_Cost_CostBudgetEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>新增/编辑间接成本费用预算</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript">
        $(document).ready(function () {
            setWidthHeight();
            var gvTab = new JustWinTable("gvDiaryDetails");
            setButton(gvTab, 'btnDel', 'btnEdit', 'hfldCheckedIds');
            Val.validate('form1', 'btnSave');
            $('#txtInputDate').attr('readonly','readonly');
            //readonly=true的文本框禁止后退事件
            $('input[readonly]').keydown(function(e){
                e.preventDefault(); 
            });
            $('#txtConType').attr('readonly','true');
        });

        function setWidthHeight() {
            $('#divBudget').height($(this).height() - 173);
        }
        //取消
        function wClose() {
            var flag = false;
            var url = 'CostBudgetApply.aspx';
            if (arguments.length > 0) {
                flag = true;
                url = arguments[0];
            }
            winclose('CostBudgetEdit', url, flag);
        }
        function setButton(jwTable, btnDel, btnEdit, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }

            jwTable.registClickTrListener(function () {
                var index = $(this).find('td:eq(1)').html();
                $('#hfldSingleId').val(index);
                if (hdChkId != '') {
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById(btnEdit).removeAttribute('disabled');
                    document.getElementById(hdChkId).value = this.id;
                }
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
               
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
                }
                else {
                    var index = $(this).parents('tr').find('td:eq(1)').html();
                    $('#hfldSingleId').val(index);
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    }
                    document.getElementById(btnDel).removeAttribute('disabled');
                    if (checkedChk.length > 1) {
                        document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
                    } else {
                        document.getElementById(btnEdit).removeAttribute('disabled');
                        document.getElementById('hfldSingleId').value = document.getElementById      (document.getElementById(hdChkId).value).cells[1].innerText;
                    }
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    }
                    document.getElementById(btnDel).removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = '';
                    }
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }
                document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
            });
        };
        //合计
        function fillTotalAmount(total) {
            var trText = '<tr><td colspan="4" align="center">合计</td><td colspan="4">' + total + '</td></tr>';
            if ($('#gvDiaryDetails')) {
                var tab = $('#gvDiaryDetails').get(0);
                var lastRowId = tab.rows[tab.rows.length - 1].id;
                $('#' + lastRowId).after(trText);
            }
        }
       

        //限制字符
        function limitText() {
            taskCode = judgeTest($('#txtName')[0]);
            taskName = judgeTest($('#txtDeparment')[0]);
            if (taskCode == true && taskName == true) {
                $('#btnSave').removeAttr('disabled');
            } else {
                alert("系统提示：\n输入项的内容中存在非有效字符或不能超过200个字！");
                $('#btnSave').attr('disabled', 'disabled');
            }
        }

        //判断任务节点信息是否符合条件
        function judgeTest(txt) {
            var isOK = true;
            var txtValue = txt.value;
            if(!!/[<>?/'~`%\$\^《》？‘·￥]/.test(txtValue)){
                isOK =  false;
            }
            if (txtValue.length > 200) {
                isOK =  false;
            }
           return isOK;
        }

	</script>
	<style type="text/css">
		.tdLeft
		{
			text-align: left;
		}
	</style>
</head>
<body style="height: 519px;">
	<form id="form1" runat="server">
	<div style="text-align: center;">
		<table border="1" cellpadding="5" cellspacing="0" style="width: 99%; margin: auto;">
			<tr>
				<td align="right">
					费用名称
				</td>
				<td>
					<asp:TextBox ID="txtName" Width="200px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
				</td>
				<td align="right">
					申请日期
				</td>
				<td class="elemTd mustInput">
					<asp:TextBox ID="txtInputDate" Width="200px" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'发生时间必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="right">
					填报人
				</td>
				<td class="elemTd mustInput">
					<asp:TextBox Width="200px" ID="txtInputUser" ReadOnly="true" CssClass="{required:true, messages:{required:'填报人必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td align="right">
					费用编号
				</td>
				<td class="elemTd mustInput">
					<asp:TextBox ID="txtIndireCode" Width="200px" CssClass="{required:true, messages:{required:'费用编号必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<div class="divHeader" style="text-align: left; padding-top: 4px; margin-top: 4px;">
		<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
		<asp:Button ID="btnEdit" Text="编 辑" Style="height: 21px" OnClick="btnEdit_Click" runat="server" />
		<asp:Button ID="btnDel" Text="删 除" disabled="disabled" OnClick="btnDel_Click" runat="server" />
	</div>
	<div id="divBudget" class="pagediv">
		<asp:GridView CssClass="gvdata" ID="gvDiaryDetails" AutoGenerateColumns="false" OnRowDataBound="gvDiaryDetails_RowDataBound" DataKeyNames="Id,CBSCode" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
						<asp:CheckBox ID="cbBoxAll" runat="server" />
					</HeaderTemplate><ItemTemplate>
						<asp:CheckBox ID="cbBox" runat="server" />
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
						<%# Container.DataItemIndex + 1 %>
					</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="名称">
<ItemTemplate>
						<asp:Label ID="lblgvName" Text='<%# System.Convert.ToString(Eval("Name"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
					</ItemTemplate>

<EditItemTemplate>
						<asp:TextBox ID="txtgvName" Text='<%# System.Convert.ToString(Eval("Name"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
					</EditItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="成本科目"><ItemTemplate>
						<asp:Label ID="lblgvCBSCode" Text='<%# System.Convert.ToString(base.CBSName(Eval("CBSCode").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
					</ItemTemplate><EditItemTemplate>
						<asp:DropDownList ID="ddlCBSCode" runat="server"></asp:DropDownList>
					</EditItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预算金额" HeaderStyle-Width="80px"><ItemTemplate>
						<asp:Label ID="lblgvAmount" Text='<%# System.Convert.ToString(Eval("Cost"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
					</ItemTemplate><EditItemTemplate>
						<asp:TextBox ID="txtgvAmount" CssClass="decimal_input" Text='<%# System.Convert.ToString(Eval("Cost"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
					</EditItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="说明" ItemStyle-Width="200px"><ItemTemplate>
						<asp:Label ID="lblgvNote" Text='<%# System.Convert.ToString(Eval("Note"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
					</ItemTemplate><EditItemTemplate>
						<asp:TextBox ID="txtgvNote" Text='<%# System.Convert.ToString(Eval("Note"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
					</EditItemTemplate></asp:TemplateField></Columns><HeaderStyle CssClass="header"></HeaderStyle></asp:GridView>
	</div>
	<div class="divFooter" style="text-align: right;">
		<asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />
		<input type="button" value="取 消" onclick="wClose()" />
	</div>
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<!-- 存储要编辑的索引-->
	<asp:HiddenField ID="hfldSingleId" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	<asp:HiddenField ID="hfldEditRow" runat="server" />
	</form>
</body>
</html>
