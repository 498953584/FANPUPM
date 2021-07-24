<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Audit.ascx.cs" Inherits="UserControl_Audit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<asp:Repeater ID="rptAudit" runat="server">
<HeaderTemplate>
		<table id="tabAudit" class="tab2" style="border-top: none;">
			<tr>
				<td style="width: 16%">
					申请人签字
				</td>
				<td style="width: 34%">
					<%=(base.GetOrginator() == null) ? "" : base.GetOrginator().v_xm %>
				</td>
				<td style="width: 16%">
					签名及日期
				</td>
				<td>
					<img id="imgOrginator" width="80px" height="80px" src='<%=GetNamePath((base.GetOrginator() == null) ? "" : base.GetOrginator().v_xm) %>' />
					<span>
						<%=GetOriginatorDate() %></span>
				</td>
			</tr>
	</HeaderTemplate>

<ItemTemplate>
		<tr>
			<td>
				审核意见
			</td>
			<td>
				<%# Common2.GetStateNoColor(Eval("AuditResult").ToString()) %>。
				<%# Eval("AuditInfo") %>
				<br />
				<%# GetAnnex(Eval("ID").ToString(), Eval("NoteID").ToString()) %>
			</td>
			<td>
				签名及日期
			</td>
			<td>
				<span id="span_operator">
					<%# GetName(Eval("Operator")) %>&nbsp;&nbsp;</span>
				<img id="imgOrginator" width="80px" height="80px" src='<%# GetNamePath(Eval("Operator")) %>' />
				<span>
					<%# (Eval("AuditDate") == null) ? "" : Convert.ToDateTime(Eval("AuditDate")).ToString("yyyy-MM-dd") %>
			</td>
		</tr>
	</ItemTemplate>

<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:Repeater>
<script type="text/javascript">
	$('img').each(function () {
		// 如果没有签名图片，就显示用户名称
		if (this.src.indexOf('UploadFiles') == -1) {
			$(this).remove();
		} else {
			$(this).prev().remove();
		}
	});
</script>
