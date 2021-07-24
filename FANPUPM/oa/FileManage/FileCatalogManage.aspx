<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileCatalogManage.aspx.cs" Inherits="oa_FileManage_FileCatalogManage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(RecordID,ClassCode)
	{
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('HdnRecordID').value = RecordID;
	}
	function OpenWin(op,ClassTypeCode,CatalogName)
	{
	    var RecordID = document.getElementById('HdnRecordID').value;
	    var ClassID = document.getElementById('HdnClassID').value;
	    var LibraryCode = document.getElementById('HdnLibraryCode').value;
	    var FileAge = document.getElementById('HdnFileAge').value;
	    var TimeLimit = document.getElementById('HdnTimeLimit').value;
		var url= "FileCatalogManageEdit.aspx?t="+op+"&rc="+RecordID+"&lc="+LibraryCode+"&cid="+ ClassID +"&y="+FileAge+"&l="+TimeLimit;
		var ref = window.showModalDialog(url,window,"dialogHeight:360px;dialogWidth:450px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;
	}
	function IsInteger(sourObj)
	{
	    var ddl = document.getElementById('DDLSearch');
	    var value = ddl.options[ddl.selectedIndex].value;
	    if(value == "2" || value == "3")
	    {
		    if (!(new RegExp(/^\d+$/).test(sourObj.value)))
		    {
		        alert('请输入整数数据类型!');
			    sourObj.focus();
			    return;
		    }
		}
	}
	function CheckChanged(obj)
	{
	    var ddl = document.getElementById('txtotherSearch');
	    var value = obj.options[obj.selectedIndex].value;
	    if(value == "2" || value == "3")
	    {
		    ddl.value = "0";
		}
		else
		{
		    ddl.value = "";
		}
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                     档案目录信息列表
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-search" align="right">
                    按<asp:DropDownList ID="DDLSearch" runat="server"><asp:ListItem Selected="true" Value="0" Text="档案编号" /><asp:ListItem Value="1" Text="密级" /><asp:ListItem Value="2" Text="件号" /><asp:ListItem Value="3" Text="档案盒号" /><asp:ListItem Value="4" Text="责任者" /></asp:DropDownList>
                    <asp:TextBox ID="txtotherSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" Text="" OnClick="btnSearch_Click" runat="server" />
                    <input id="HdnRecordID" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnLibraryCode" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnFileAge" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnTimeLimit" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnClassID" type="hidden" style="width:20px" runat="server" />

                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新  增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="编  辑" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删  除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnView" Text="查 看" OnClick="btnView_Click" runat="server" />
                    </td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" PageSize="20" ToolTip="请双击查看详细信息" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        档案编号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        档案标题</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        卷号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        件号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        盒号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        存放位置</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        所属项目</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        责任人</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        密级</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="FileCode" HeaderText="档案编号" /><asp:BoundField DataField="FileName" HeaderText="档案标题" /><asp:BoundField DataField="Volume" HeaderText="卷号" HtmlEncode="false" /><asp:BoundField DataField="FileNumber" HeaderText="件号" HtmlEncode="false" /><asp:BoundField DataField="BoxNumber" HeaderText="盒号" HtmlEncode="false" /><asp:BoundField DataField="SavePlace" HeaderText="存放位置" HtmlEncode="false" /><asp:BoundField DataField="PrjName" HeaderText="所属项目" HtmlEncode="false" /><asp:BoundField DataField="Principal" HeaderText="责任人" HtmlEncode="false" /><asp:BoundField HeaderText="密级" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        &nbsp;
                    </div>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
