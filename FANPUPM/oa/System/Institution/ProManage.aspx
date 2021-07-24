<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProManage.aspx.cs" Inherits="ProjectManage_Provider_ProManage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>Ȩ������</title><meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
	<script language="javascript" type="text/javascript">
		window.name = "win";

		//��ʼ��rFrame��checkbox
		function initCheckBox() {
			var DUTYIDs = document.getElementById('HidDutyCodeS').value + "'";
			if (DUTYIDs != "") {
				var arrDUTYIDs = DUTYIDs.split(',');

				var table = top.rFrame.document.getElementById('GridView1');
				for (var i = 1; i < table.rows.length; i++) {
					for (var j = 0; j < arrDUTYIDs.length; j++) {
						if (arrDUTYIDs[j] == table.rows[i].title) {
							var cellObj = table.rows[i].cells[1].all.tags("input")[0];
							cellObj.checked = true;

						}
					}

				}
			}



		}

		//ͨ��rFrame��checkbox���ø�λID��
		function SelectCBox(objChecked, DutyID, TBName) {
			var DUTYIDs = document.getElementById('HidDutyCodeS').value;
			var table = top.rFrame.document.getElementById(TBName);
			//alert(table.rows[1].cells[2].innerText);

			if (objChecked)//ѡ��ʱ�Ĵ���
			{
				//����ѡ�еĸ�λID������Ƿ���ڣ���������������׷�Ӹãɣģ�����������

				if (DUTYIDs == "") {
					DUTYIDs = +DutyID + ",";
				}
				else {
					DUTYIDs += DutyID + ",";
				}

			}
			else//ȡ��ѡ�еĴ���
			{
				if (DUTYIDs != "") {
					DUTYIDs = DUTYIDs.replace(DutyID + ",", "");
				}
			}

			document.getElementById('HidDutyCodeS').value = DUTYIDs;

			DUTYIDs = DUTYIDs + "0"//Ϊ������Ĵ������
			document.getElementById('rFrame2').src = "DutyList2.aspx?DUTYID=" + DUTYIDs;


		}

		//ͨ��rFrame2��checkboxȡ��ѡ���λID
		function SelectCBox2(objChecked, DutyID, TBName) {
			var DUTYIDs = document.getElementById('HidDutyCodeS').value;
			var table = top.rFrame.document.getElementById(TBName);
			if (!objChecked) {
				if (DUTYIDs != "") {
					DUTYIDs = DUTYIDs.replace(DutyID + ",", "");
					document.getElementById('HidDutyCodeS').value = DUTYIDs;
				}


				for (var i = 1; i < table.rows.length; i++) {
					var cellObj = table.rows[i].cells[1].all.tags("input")[0];
					if (table.rows[i].title == DutyID) {
						cellObj.checked = false;

					}
				}
				DUTYIDs = DUTYIDs + "0"//Ϊ������Ĵ������
				document.getElementById('rFrame2').src = "DutyList2.aspx?DUTYID=" + DUTYIDs;


			}
		}
		
        
	</script>
</head>
<body class="body_clear" scroll="no">
	<form id="formx" target="win" runat="server">
	<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="1" id="tablex"
		class="table-normal">
		<tr>
			<td width="25%" valign="top" rowspan="2">
				<div id="div1" style="width: 100%; height: 100%; overflow: auto;">
					
					<asp:TreeView ID="tv" ShowLines="true" ExpandDepth="1" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
				</div>
			</td>
			<td width="75%">
				<iframe id="rFrame" name="rFrame" src="DutyList.aspx?code=0" frameborder="0" width="100%" height="100%" runat="server"></iframe>
			</td>
		</tr>
		<tr height="200">
			<td valign="top">
				<iframe id="rFrame2" name="rFrame2" src="DutyList2.aspx?DUTYID=0" frameborder="0" width="100%" height="100%" runat="server"></iframe>
			</td>
		</tr>
		<tr height="50">
			<td colspan="2" class="td-submit" style="text-align: center;">
				<asp:Button ID="ButOk" Text="ȷ ��" OnClick="ButOk_Click" runat="server" />
				<input id="ButClose" type="button" onclick="window.close();" value="�� ��" />
			</td>
		</tr>
	</table>
	<input id="HidDutyCodeS" type="hidden" runat="server" />

	<input id="HidClassId" type="hidden" runat="server" />

	</form>
</body>
</html>
