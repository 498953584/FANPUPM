<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileLendSearch.aspx.cs" Inherits="oa_FileManage_FileLendSearch" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    <!--
	function CheckFileCode(obj)
	{
	    var ParentCode = parent.window.document.getElementById('HdnParentFileCode').value;
	    var arrParent = ParentCode.split(,);
	    var fileValue = obj.value;
	    if(obj.checked)
	    {
	        if(ParentCode.indexOf(fileValue) == -1)
	        {
	            arrParent.push(obj.value);
	        }
	    }
	    else
	    {
	        if(ParentCode.indexOf(fileValue) != -1)
	        {
	            DeleteFileCode(arrParent,fileValue);
	        }
	    }
	    parent.window.document.getElementById('HdnParentFileCode').value = arrParent.join();
	    document.getElementById('HdnFileCode').value = arrParent.join();
	}
	function DeleteFileCode(arrParent,value)
	{
	    var i = Index(arrParent,value);
	    arrParent.splice(i,1);
	}
	function Index(arr,value)
	{
	    var index = -2;
	    for(var i=0;i<arr.length;i++)
	    {
	        if(arr[i] == value)
	        {
	            index = i;
	            break;
	        }
	    }
	    return index;
	}
	function SelectFileCode()
	{
	    var parentValue = parent.window.document.getElementById('HdnParentFileCode').value;
	    var chk = document.getElementsByTagName('input');
	    for(var i=0;i<chk.length;i++)
	    {
	        if(chk[i].type.toLowerCase() == "checkbox")
	        {
	            var value = chk[i].value;
	            var clientID = chk[i].id;
	            if(parentValue.indexOf(value) != -1 && clientID.indexOf('GVBook') != -1)
	            {
	                chk[i].checked = true;
	            }
	        }
	    }
	}
	
	function FileLendClickRow(RecordID)
	{
	    document.getElementById('HdnLendOneFileCode').value = RecordID;
	}
	function CheckLendFileCode(obj)
	{
	    var vlend = document.getElementById('HdnLendFileCode').value;
	    var arrLend = vlend.split(,);
	    var ovalue = obj.value;
	    if(obj.checked)
	    {
	        if(vlend.indexOf(ovalue) == -1)
	        {
	            arrLend.push(ovalue);
	        }
	    }
	    else
	    {
	        if(vlend.indexOf(ovalue) != -1)
	        {
	            var i = Index(arrLend,ovalue);
	            arrLend.splice(i,1);
	        }
	    }
	    document.getElementById('HdnLendFileCode').value = arrLend.join();
	    if(arrLend.join() != "")
	    {
	        document.getElementById('btnDel').disabled = false;
	    }
	    else
	    {
	        document.getElementById('btnDel').disabled = true;
	    }
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
	function isDate(sourObj)
    {
        var cdate = new Date();
        var nowDate = cdate.getFullYear()+'-'+parseInt(cdate.getMonth()+1)+'-'+cdate.getDate();
        var re = /^[1-9]{1}[0-9]{3}-[0-9]{1,2}-[0-9]{1,2}$/;
        if (!(new RegExp(re).test(sourObj.value)))
		{
		    alert('日期格式不正确!\r\n正确日期格式为:年-月-日\r\n如:'+nowDate);
		    sourObj.value = nowDate;
			sourObj.focus();
			return false;
		}
		else
		{
		    var l = sourObj.value.length;
		    var f = sourObj.value.indexOf('-');
		    var s = sourObj.value.lastIndexOf('-');
		    var y = sourObj.value.substring(0,4);
		    var m = sourObj.value.substring(f+1,s);
		    var d = sourObj.value.substring(s+1,l);
		    if(parseFloat(m) < 1 || parseFloat(m) > 12)
		    {
		        alert('月份类型不正确!');
		        sourObj.value = nowDate;
			    sourObj.focus();
			    return false;
		    }
		    if(parseFloat(m) == 2)
		    {
		        if(y%4 == 0)
		        {
		            if(parseFloat(d) < 1 || parseFloat(d) > 29)
		            {
		                alert('日类型不正确!');
		                sourObj.value = nowDate;
			            sourObj.focus();
			            return false;
		            }
		            return true;
		        }
		        else
		        {
		            if(parseFloat(d) < 1 || parseFloat(d) > 28)
		            {
		                alert('日类型不正确!');
		                sourObj.value = nowDate;
			            sourObj.focus();
			            return false;
		            }
		            return true;
		        }
		    }
		    else
		    {
		        var arrm = "01,03,05,07,08,10,12";
		        if(arrm.indexOf(m) != -1)
		        {
		            if(parseFloat(d) < 1 || parseFloat(d) > 31)
		            {
		                alert('日类型不正确!');
		                sourObj.value = nowDate;
		                sourObj.focus();
		                return false;
		            }
		            return true;
		        }
		        else
		        {
		            if(parseFloat(d) < 1 || parseFloat(d) > 30)
		            {
		                alert('日类型不正确!');
		                sourObj.value = nowDate;
		                sourObj.focus();
		                return false;
		            }
		            return true;
		        }
		    }
		}
		return true;
    }
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <asp:ScriptManager ID="ScriptManager" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                     档案列表
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-search" align="right">
                    <asp:UpdatePanel ID="UpdatePanelFileSearch" runat="server">
<ContentTemplate>
                        按<asp:DropDownList ID="DDLSearch" runat="server"><asp:ListItem Selected="true" Value="0" Text="档案编号" /><asp:ListItem Value="1" Text="密级" /><asp:ListItem Value="2" Text="件号" /><asp:ListItem Value="3" Text="档案盒号" /><asp:ListItem Value="4" Text="责任者" /><asp:ListItem Value="5" Text="档案标题" /></asp:DropDownList>
                    <asp:TextBox ID="txtotherSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" Text="" OnClick="btnSearch_Click" runat="server" />
                    <input id="HdnFileCode" type="hidden" style="width:20px" runat="server" />

                    </ContentTemplate>
</asp:UpdatePanel>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:UpdatePanel ID="UpdatePanelFile" runat="server">
<ContentTemplate>
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" PageSize="20" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 20px">
                                        &nbsp;</th>
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
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField>
<ItemTemplate>
                                    <input type="checkbox" id="FileRecordID" runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="FileCode" HeaderText="档案编号" /><asp:BoundField DataField="FileName" HeaderText="档案标题" /><asp:BoundField DataField="Volume" HeaderText="卷号" HtmlEncode="false" /><asp:BoundField DataField="FileNumber" HeaderText="件号" HtmlEncode="false" /><asp:BoundField DataField="BoxNumber" HeaderText="盒号" HtmlEncode="false" /><asp:BoundField DataField="SavePlace" HeaderText="存放位置" HtmlEncode="false" /><asp:BoundField DataField="PrjName" HeaderText="所属项目" HtmlEncode="false" /><asp:BoundField DataField="Principal" HeaderText="责任人" HtmlEncode="false" /><asp:BoundField HeaderText="密级" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="20" class="td-toolsbar"></td>
            </tr>
            <tr>
                <td align="Right" style="height: 20px">
                    <asp:UpdatePanel ID="UpdatePanelButton" runat="server">
<ContentTemplate>
                     <asp:Button ID="btnAdd" CssClass="button-normal" Text="添加到我的收藏夹" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnApplyLend" Text="申请借阅" CssClass="button-normal" OnClick="btnApplyLend_Click" runat="server" />
                    <asp:Button ID="btnDel" CssClass="button-normal" Enabled="false" OnClientClick="DeleteLendFileCode();" Text="删  除" OnClick="btnDel_Click" runat="server" />
                    <input style="width: 20px" id="HdnLendFileCode" type="hidden" runat="server" />

                    <input id="HdnLendOneFileCode" type="hidden" style="width:20px" runat="server" />

                        </ContentTemplate>
</asp:UpdatePanel>
                 </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                     <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
        <asp:UpdatePanel ID="UpdatePanelFileLend" runat="server">
<ContentTemplate>
                    <asp:GridView CssClass="grid" ID="GVLend" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVLend_RowDataBound" OnPageIndexChanging="GVLend_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 20px">
                                        &nbsp;</th>
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
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 80px">
                                        计划归还时间</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField>
<ItemTemplate>
                                    <input type="checkbox" id="LendRecordID" runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="档案编号" /><asp:BoundField HeaderText="档案标题" /><asp:BoundField HeaderText="卷号" HtmlEncode="false" /><asp:BoundField HeaderText="件号" HtmlEncode="false" /><asp:BoundField HeaderText="盒号" HtmlEncode="false" /><asp:BoundField HeaderText="存放位置" HtmlEncode="false" /><asp:BoundField HeaderText="所属项目" HtmlEncode="false" /><asp:BoundField HeaderText="责任人" HtmlEncode="false" /><asp:BoundField HeaderText="密级" HtmlEncode="false" /><asp:TemplateField HeaderText="计划归还时间">
<ItemTemplate>
                                    <asp:TextBox ID="TxtPlanReturnDate" CssClass="text-normal" Width="80px" AutoPostBack="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "PlanReturnDate", "{0:yyyy-MM-dd}")) %>' OnTextChanged="TxtPlanReturnDate_TextChanged" runat="server"></asp:TextBox>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="RecordID" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
            </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
               </div>          
                </td>
            </tr>
        </table>
        <script language="javascript" type="text/javascript">
	    <!--
            SelectFileCode();
	        document.getElementById('HdnFileCode').value = parent.window.document.getElementById('HdnParentFileCode').value;
	    -->
        </script>
    </form>
</body>
</html>
