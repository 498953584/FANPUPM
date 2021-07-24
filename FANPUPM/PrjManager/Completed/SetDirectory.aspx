<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetDirectory.aspx.cs" Inherits="PrjManager_Completed_SetDirectory" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设置目录</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			disabledCheckBox('disabled');
			setCheckClick();
		});

		// 更新到附件
		function setParentAdunct(prjCompleteId, prjId, adjunctCount, existAnnexAddress) {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback();
			}
			top.ui.closeWin();
		}

		function disabledCheckBox(disabled) {
			$(':checkbox[title="无权限"]').each(function (i) {
				if (disabled == 'disabled')
					$(this).attr('disabled', 'disabled');
				else
					$(this).removeAttr('disabled');
			});
		}

		// 选中目录加载其文件
		function setCheckClick() {
			$('input[type=checkbox]').bind('click', function () {
				var selId = $('#hfldSelDriectoryId').val();
				var id = $(this).next().attr('href');
				id = id.substring(1, id.length - 1);
				if (this.checked) {
					selId += id + ',';
				}
				else {
					if (selId.indexOf(id) != -1)
						selId = selId.replace(id + ',', '');
				}
				$('#hfldSelDriectoryId').val(selId)
				selId = selId.substring(0, selId.length - 1);
				var src = '/PrjManager/Completed/IFDirectoryFilesInfo.aspx?' + new Date().getTime() + '&id=' + selId;
				$('#ifFilesInfo').attr("src", src);
			});
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab" style="border: solid 0px red;">
		<tr>
			<td valign="top" style="height: 400px; width: 150px; border: solid 0px orange">
				<div class="pagediv" style="overflow: scroll; vertical-align: top; position: relative;">
					<asp:TreeView ID="tvFile" ExpandDepth="1" ShowLines="true" ShowCheckBoxes="All" Style="top: 0px; position: absolute;" runat="server"><NodeStyle HorizontalPadding="5px" /><SelectedNodeStyle HorizontalPadding="5px" BackColor="#3399FF" ForeColor="White" /></asp:TreeView>
				</div>
				<asp:Label ID="lblText" Text="" runat="server"></asp:Label>
			</td>
			<td>
				<div id="divFilesInfo" style="overflow: hidden; vertical-align: top; border: solid 0px red;
					height: 400px;">
					<iframe id="ifFilesInfo" style="width: 100%; height: 100%; overflow: hidden;" frameborder="0">
					</iframe>
				</div>
			</td>
		</tr>
		<tr>
			<td align="right" colspan="2">
				<asp:Button ID="btnSave" Text="保存" OnClientClick="disabledCheckBox('')" OnClick="btnSave_Click" runat="server" />
				<input type="button" id="btnCancel" value="取消" onclick="closDg()" />
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldSelDriectoryId" runat="server" />
	</form>
</body>
</html>
