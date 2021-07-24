<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectElectronFilesSearch.aspx.cs" Inherits="ProjectElectronFilesSearch" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>电子归档</title>
    <script src="/Script/jquery.js" type="text/javascript"></script>
     <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2S.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function() {
            $("#GridView1 tr").each(function() {
                $(this).find('td').eq(0).find("img").attr("src", "/images/over.gif");
                var thisTR = $(this);
                $(this).click(function() {
                    var _id = $(thisTR).attr("id");
                    $("#hidenID").val(_id);
                });
            });
            replaceEmptyTable();
            var GridView1Table = new JustWinTable('GridView1');
            setButton(GridView1Table, 'Button1', 'Button1', 'btnQuery', 'hidenID')
        });
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
        function replaceEmptyTable() {
            if (!document.getElementById('GridView1')) return;
            if (!document.getElementById('emptyContractType')) return;
            var gvwContractType = document.getElementById('GridView1');
            var emptyContractType = document.getElementById('emptyContractType');
            if (gvwContractType.firstChild.childNodes.length == 1) {
                gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
            }
        }

        function viewopen_n(url, titleStr) {
            if (url != "") {
                parent.parent.desktop.flowclass = window;
                toolbox_oncommand(url, titleStr);
            }
        }
    </script>
</head>
	<body>
	    <form id="from1" runat="server">
	        <div id="header" style="height:28px; line-height:28px;">
                    电子归档
	        </div>
	        <div>
	           <table class="queryTable" cellpadding="3px" cellspacing="0px">
	            	<tr>
	            		<td class="descTd">名称</td>
	            		<td class="">
                            <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox></td>
	            		<td class="descTd">备注</td>
	            		<td class="">
                            <asp:TextBox ID="txtInfo" Width="120px" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnQueryList" Text="查询" OnClick="btnQueryList_Click" runat="server" />
                                </td>
	            	</tr>
	            </table>
	        </div>
	        <div class="divFooter" style="text-align: left;">
                <input id="Button1" type="button" value="button" style="display:none" />
                <input id="Button2" type="button" value="button" style="display:none" />              
                <asp:Button ID="btnQuery" Text="查看" OnClick="btnQuery_Click" runat="server" />
	        </div>
	        <div>
                <asp:GridView ID="GridView1" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" Width="100%" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
                 <table id="emptyContractType" class="gvdata">
                       <tr class="header">                      
                         <th scope="col" style="width: 20px;">
                               </th>                                  
                            
                    		<th scope="col">
                    		    序号
                    		</th>
                    		<th scope="col">
                    		    名称
                    		</th>
                    		<th scope="col">
                    		    信息描述
                    		</th>
                    		<th scope="col">
                    		    附件
                    		</th>
                    	</tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
<HeaderTemplate>
                              
                        </HeaderTemplate>

<ItemTemplate>
                            <asp:Image ID="Image1" Width="12px" Height="12px" runat="server" />
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="file_name" HeaderText="名称" ItemStyle-Width="100px" HeaderStyle-Width="100px" /><asp:BoundField DataField="file_info" HeaderText="信息描述" /><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px" HeaderStyle-Width="30px">
<ItemTemplate>
                            <%# GetAnnx(Convert.ToString(Eval("file_sid"))) %>
                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
	        </div>
        <asp:HiddenField ID="hidenID" runat="server" />
	    </form>
	</body>
</html>
