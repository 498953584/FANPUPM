<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressimplementevaluateall.aspx.cs" Inherits="ProgressImplementEvaluateAll" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>公司评价一览表</title>
		
		<script src="../../../../Web_Client/Tree.js"></script>
		<base target="_self">
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
    </style>
	</head>
	<body class="body_popup">
		<form id="Form1" method="post" runat="server">
			<div>
		        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="height: 20px;">
                        <td class="divHeader">
                            公司评价一览表</td>
                    </tr>
                 </table>
		    </div>
				<TABLE id="Table1" height="90%" cellspacing="0" cellpadding="0" width="100%">
					<TR>
						<TD vAlign="top">
						 <div style="overflow: auto; width: 100%; height: 100%">
						<asp:DataGrid ID="dglist" AutoGenerateColumns="false" CssClass="gvdata" DataKeyField="MainId" runat="server"><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:BoundColumn DataField="AppraisePeople" ReadOnly="true" HeaderText="评论人"></asp:BoundColumn><asp:TemplateColumn HeaderText="评论时间"><ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("AppraiseTime", "{0:yyyy-MM-dd}")) %>' runat="server"></asp:Label>
										</ItemTemplate><EditItemTemplate>
											<JWControl:DateBox ReadOnly="true" ID="AppriaseTime" Text='<%# Convert.ToString(Eval("AppraiseTime", "{0:yyyy-MM-dd}")) %>' runat="server"></JWControl:DateBox>
										</EditItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="评论内容"><ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("Appraise")) %>' runat="server"></asp:Label>
										</ItemTemplate><EditItemTemplate>
											<asp:TextBox ID="Appraise" Text='<%# Convert.ToString(Eval("Appraise")) %>' runat="server"></asp:TextBox>
										</EditItemTemplate></asp:TemplateColumn><asp:EditCommandColumn ButtonType="LinkButton" UpdateText="保存" CancelText="取消" EditText="编辑"></asp:EditCommandColumn><asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid>
							</div>
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></TD>
					</TR>
					<TR style="text-align:center; width:100%; height:10px;">
						<TD><JWControl:PaginationControl ID="pc" OnPageIndexChange="pc_PageIndexChange" runat="server"></JWControl:PaginationControl></TD>
					</TR>
				</TABLE>
		</form>
	</body>
</HTML>
