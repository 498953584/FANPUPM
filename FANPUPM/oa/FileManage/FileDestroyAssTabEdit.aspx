<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDestroyAssTabEdit.aspx.cs" Inherits="oa_FileManage_FileDestroyAssTabEdit" %>

<html>
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(DestroyFileID,index)
	{
	    document.getElementById('btnDel').disabled = false;
	    document.getElementById('HdnIndexID').value = index;
	}
	function SelectFileName(obj,LibraryCode)
	{
	    var file = Array();
	    file[0] = "";
	    file[1] = "";
	    file[2] = "";
	    file[3] = "";
	    file[4] = "";
		var url= "SelectFile.aspx?lc=" + LibraryCode;
		window.showModalDialog(url,file,"dialogHeight:500px;dialogWidth:320px;center:1;help:0;status:0;");
		if(file[0] != "")
		{
		    while(obj=obj.parentElement)
		    {
		        if(obj.tagName.toLowerCase() == 'tr')
		        {
		            obj.cells(1).children(0).value = file[1];
		            obj.cells(1).children(1).value = file[0];
		            obj.cells(2).children(0).value = file[2];
		            obj.cells(3).children(0).value = file[3];
		            if(file[4] == '1')
		            {
		                obj.cells(4).children(0).value = '密秘';
		            }
		            if(file[4] == '2')
		            {
		                obj.cells(4).children(0).value = '机密';
		            }
		            if(file[4] == '3')
		            {
		                obj.cells(4).children(0).value = '绝密';
		            }
		            break;
		        }
		    }
		}
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                    申请明细记录</td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新  增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删  除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />
                    <input id="HdnIndexID" style="width: 20px" type="hidden" runat="server" />

                 </td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        档案标题</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 120px">
                                        档案编号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        所属项目</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        密级</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:TemplateField HeaderText="档案标题">
<ItemTemplate>
                                    <input id="txtFileName" title="双击选择档案!" type="text" style="width:100%;" runat="server" />

                                    <input id="HdnRecordID" type="hidden" runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="档案编号">
<ItemTemplate>
                                    <input id="txtFileCode" type="text" style="border-width:0px;width:100%" runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="所属项目">
<ItemTemplate>
                                    <input id="txtPrjName" type="text" style="border-width:0px;width:100%" runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="密级">
<ItemTemplate>
                                    <input id="txtSecretLevel" type="text" style="border-width:0px;width:100%" runat="server" />

                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
