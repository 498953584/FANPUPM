<%@ Page Language="C#" AutoEventWireup="true" CodeFile="easyui.aspx.cs" Inherits="easyui" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnDialog').click(function () {
				$('#dd').css('display', 'block');
				$('#dd').dialog({
					buttons: [{
						text: '确定',
						iconCls: 'icon-ok',
						handler: function () {
							alert('ok');
						}
					}, {
						text: '取消',
						handler: function () {
							$('#dd').dialog('close');
						}
					}],
					resizable: true,
					collapsible: true
				});
			});

			$('#btnConfirm1').get(0).onclick = function () { return confirm('msg'); }

			$('#btnConfirm2').get(0).onclick = function () {
				$.messager.confirm('title', 'msg', function (r) { return r });
			}

			$('#btnConfirm3').get(0).onclick = function () {
				$.messager.confirm('title', 'msg', function (r) {
					if (r) {
						__doPostBack('btnConfirm3', '');
					}
				});
				return false;
			}

			//			$('#btnConfirm3').get(0).onclick = function () {return jw.confirm('title', 'msg', 'btnConfirm3'); }

			$('#Button1').get(0).onclick = function () {

				if (!$('#form1').form('validate')) {
					return false;
				}
			}
		});


		function valid() {
			alert($('#dfe').validatebox('isValid'));
		}
		$(function () {
			$.extend($.fn.validatebox.defaults.rules, {
				minLength: {
					validator: function (value, param) {
						return value.length >= param[0];
					},
					message: 'Please enter at least {0} characters.'
				}
			});
		});

		function TT() {

		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="aa" class="easyui-accordion" style="width: 700px; height: 300px;">
		<div title="Title1" data-options="iconCls:'icon-ok'" style="overflow: auto; padding: 10px;">
			<h3 style="color: #0099FF;">
				Accordion for jQuery</h3>
			<p>
				Accordion is a part of easyui framework for jQuery. It lets you define your accordion
				component on web page more easily.</p>
		</div>
		<div title="Title2" data-options="iconCls:'icon-search',selected:true,
				tools:[{
					iconCls:'icon-reload',
					handler:function(){
						$('#tt').datagrid('reload');
					}
				}]">
			<table id="tt" class="easyui-datagrid" data-options="url:'datagrid_data2.json',border:false,fit:true,fitColumns:true,singleSelect:true">
				<thead>
					<tr>
						<th data-options="field:'itemid',width:80">
							Item ID
						</th>
						<th data-options="field:'productid',width:100">
							Product ID
						</th>
						<th data-options="field:'listprice',width:80,align:'right'">
							List Price
						</th>
						<th data-options="field:'unitcost',width:80,align:'right'">
							Unit Cost
						</th>
						<th data-options="field:'attr1',width:150">
							Attribute
						</th>
						<th data-options="field:'status',width:50,align:'center'">
							Status
						</th>
					</tr>
				</thead>
			</table>
		</div>
		<div title="Title3" style="padding: 10px;">
			content3
		</div>
	</div>
	<hr />
	dialog
	<br />
	<input type="button" id="btnDialog" value="dialog" onclick="" />
	<input type="button" id="btnContains" value="IsContains" onclick="" />
	<asp:Button ID="btnConfirm" Text="btnConfirm" OnClientClick="return jw.confirm('sys', '??');" OnClick="btnConfirm3_Click" runat="server" />
	<input type="button" id="btnAlert" value="btnAlert" onclick="" />
	<input type="button" id="btnShow" value="btnShow" />
	<asp:Button ID="Button1" Text="Buttontttt" OnClick="Button1_Click" runat="server" />
	<br />
	<asp:Button ID="btnConfirm1" Text="btnConfirm1" runat="server" />
	<asp:Button ID="btnConfirm2" Text="btnConfirm2" runat="server" />
	<asp:Button ID="btnConfirm3" Text="btnConfirm3" OnClientClick="return jw.confirm('title', 'msg', 'btnConfirm3');" OnClick="btnConfirm3_Click" runat="server" />
	<div id="dd" style="padding: 5px; display: none; width: 800px; height: 200px;">
		<p>
			dialog content.</p>
		<p>
			dialog content.</p>
		<p>
			dialog content.</p>
		<p>
			dialog content.</p>
		<p>
			dialog content.</p>
		<p>
			dialog content.</p>
		<p>
			dialog content.</p>
		<p>
			dialog content.</p>
	</div>
	<br />
	<asp:TextBox ID="TextBox1" Width="1400px" class="easyui-validatebox" data-options="required:true,validType:'email'" runat="server"></asp:TextBox>
	</form>
</body>
</html>
