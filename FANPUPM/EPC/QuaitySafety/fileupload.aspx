<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fileupload.aspx.cs" Inherits="FileUpload" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>FileUpload</title>
        <script src="../../Script/jquery.js" type="text/javascript"></script>
		 <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
        <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
        <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
		<script language="javascript" type="text/javascript">
		    window.onload = function() {
		        replaceEmptyTable();
		        var GridView1Table = new JustWinTable('GridView1');
		        var ve = document.getElementById("EDIT").value;
		        if (ve == "Edit") {
		            //setButton(GridView1Table, 'BtnDelete', 'BtnModify', 'BtnSee', 'Hidden1')
		        }
		    }
		    function clickRow(obj, type) {
		        document.getElementById('Hidden1').value = obj;
		        if (type != "List") {
		        }
		    }
		    function OpType(obj, PrjCode,CA) {
		        var _type = document.getElementById("QS").value;
		        var Id = document.getElementById('Hidden1').value;
		        var Class = document.getElementById('hdnClass').value;
		        var url;
		        var refresh = false;
		        var type = obj;
		        var titleStr = "";
		        if (_type == "S") {
		            titleStr = "安全监督";
		            Class="10";
		        } else {
		            titleStr = "工程质量监督";
		            Class="9";
		        }
		        switch (type) {
		            case "EDIT":
		                url = "/EPC/QuaitySafety/ProjectSuperviseEdit.aspx?Flag=" + _type + "&CA="+CA+"&Type=EDIT&i_id=" + Id + "&DatumClass=" + Class + "&PrjCode=" + PrjCode;
		                break;
		            case "ADD":
		                url = "/EPC/QuaitySafety/ProjectSuperviseEdit.aspx?Flag=" + _type + "&CA=" + CA + "&Type=Add&DatumClass=" + Class + "&PrjCode=" + PrjCode;
		                break;
		            case "SEE":
		                url = "/EPC/QuaitySafety/ProjectSuperviseEdit.aspx?Flag=" + _type + "&CA=" + CA + "&Type=SEE&i_id=" + Id + "&DatumClass=" + Class + "&PrjCode=" + PrjCode;
		                break;
		        }		      
		        viewopen_n(url, titleStr);
		    }		   
		    function viewopen_n(url, titleStr) {
		        if (url != "") {
		            parent.parent.desktop.flowclass = window;
		            toolbox_oncommand(url, titleStr);
		        }
		    }

		    function getCheckBox() {
		        //chk
		        $("#chkAll").each(function() {
		            var flag = $(this).attr("checked");
		            $("[name=chk]:checkbox").each(function() {
		                $(this).attr("checked", flag);
		            })
		        });
		    }
		    function replaceEmptyTable() {
		        //当数据量为空时，修改样式
		        if (!document.getElementById('GridView1')) return;
		        if (!document.getElementById('emptyContractType')) return;
		        var gvwContractType = document.getElementById('GridView1');
		        var emptyContractType = document.getElementById('emptyContractType');
		        if (gvwContractType.firstChild.childNodes.length == 1) {
		            gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
		        }
		    }
        </script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="EDIT" runat="server" />
        <asp:HiddenField ID="hiden_CA" runat="server" />
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<tr>
					<td class="divHeader" colSpan="2" style="height:28px;">						
						<%=this.strTitle %>
					</td>
				</tr>
				<tr>
					<td class="" style=" text-align: left; width:100%; padding-left:5px; padding-top:5px; padding-bottom:5px;" colSpan="2">
					<asp:DropDownList ID="DDLLookup" Width="120px" AutoPostBack="true" runat="server"><asp:ListItem Value="1" Text="名称" /><asp:ListItem Value="2" Text="内容" /><asp:ListItem Value="3" Text="备注" /></asp:DropDownList>
						<asp:TextBox ID="TxtLookup" Width="120px" CssClass="" runat="server"></asp:TextBox>
						<asp:Button ID="btn_Search" Text="查询" OnClick="btn_Search_Click" runat="server" />
						&nbsp;&nbsp;
						<input id="Hidden1" type="hidden" size="1" name="Hidden1" runat="server" />
</td>
				</tr>				
				<tr>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox">
						<asp:GridView ID="GridView1" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" DataKeyField="i_id" AllowPaging="true" PageSize="20" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="i_id" runat="server">
<EmptyDataTemplate>                                    
                                    <table id="emptyContractType" class="gvdata">
                                        <tr class="header">     
                                        <th scope="col"  width="20px" >
                                       
                                        </th>                                     
                                            <th scope="col" align="center" style="width:40px;">
                                            序号
                                            </th>     
                                            <th scope="col">
                                                名称
                                            </th>  
                                            <th scope="col">发生日期</th>         
                                            <th scope="col">内容</th>   
                                            <th scope="col">类型</th>              
                                            <th scope="col">备注</th>                                     
                                    </tr>
                                </TABLE>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Height="20px"><HeaderTemplate>
                                                 
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px"><ItemTemplate>
										     <%# Container.DataItemIndex + 1 %>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="名称" ItemStyle-Width="140px"><ItemTemplate>  
                                        <asp:Label ID="Label1" Text="" Width="140px" runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:BoundField ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" DataField="AddDate" HeaderText="发生日期" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField DataField="Context" ItemStyle-Width="30%" HeaderText="内容" /><asp:BoundField DataField="CA" ItemStyle-Width="30%" HeaderText="类型" /><asp:BoundField DataField="Remark" ItemStyle-Width="20%" HeaderText="备注" /></Columns><RowStyle CssClass="rowa"></RowStyle><HeaderStyle CssClass="header"></HeaderStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><FooterStyle CssClass="footer"></FooterStyle><PagerStyle CssClass="GD-Pager"></PagerStyle></asp:GridView>
					    </div>
					<input id="hdnClass" type="hidden" runat="server" />
</td>
				</tr>
			</TABLE>
        <asp:HiddenField ID="QS" runat="server" />
		</form>
	</body>
</HTML>
<script language="javascript" type="text/javascript">
    $(function() {
        $("#GridView1 tr").each(function() {
            var _markTem = $(this).attr("mark");
            if (_markTem != "undefined" && _markTem != "" && _markTem != null) {
                if (_markTem == "1") {
                    $(this).find('td').eq(0).find("img").attr("src", "/images/over.gif");
                } else
                    if (_markTem == "2") {
                    $(this).find('td').eq(0).find("img").attr("src", "/images/Edit.gif");
                } else
                    if (_markTem == "3") {
                    $(this).find('td').eq(0).find("img").attr("src", "/images/Process.gif");
                }
            }
        });
    });   
</script>
