<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthorizationList.aspx.cs" Inherits="oa_eFile_AuthorizationList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>领导查询授权</title>
    <script language="javascript" type="text/javascript">
    <!--
    function openConsigee()
    {
        var p = new Array();
		p[0] = ""; 
		p[1] = "";
		var url = "";	
		 url = "../../CommonWindow/consignee.aspx";
		window.showModalDialog(url,p,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");		
		if (p[0]!="")
		{
			document.getElementById('HdnYhdmList').value = p[0];
		//	document.getElementById('txtOperater').value = p[1];
		}
		else
		{
		    alert('请选择用户!');
		    return false;
		}
    }
    -->
    </script>
</head>
<body class="body_clear" >
    <form id="form1" runat="server">
         <asp:HiddenField ID="HdnYhdmList" runat="server" />
                    <asp:HiddenField ID="HdnPrjGuid" Value="00000000-0000-0000-0000-000000000000" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div><table id="Table1" cellspacing="0" cellpadding="0" width="100%"  align="center" border="0" class="table-normal" style="height:100%">
            <tr>               
                <td class="td-title">
                    档案查阅授权</td>
            </tr>
              
          
            <tr><td style="height:24px" align="right">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>
                <asp:RadioButtonList ID="RBSelect" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RBSelect_SelectedIndexChanged" runat="server"><asp:ListItem Selected="true" Value="0" Text="项目档案" /><asp:ListItem Value="1" Text="管理类档案" /></asp:RadioButtonList>
                    </ContentTemplate>
</asp:UpdatePanel>
            </td></tr>
             <tr class="td-search">
                <td> 
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
                    按<asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Value="1" Text="档案名称" /><asp:ListItem Value="2" Text="密级" /><asp:ListItem Value="3" Text="项目名称" /></asp:DropDownList>
                            <asp:TextBox ID="TxtSearchCount" runat="server"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" />
                        </ContentTemplate>
</asp:UpdatePanel></td>                    
            </tr>
                  <tr>
                <td class="td-toolsbar" >             
                    <asp:Button ID="btnAuthorization" Text="档案授权" OnClientClick="openConsigee()" OnClick="btnAuthorization_Click" runat="server" />                  
                    
                    </td>
            </tr>
                <tr><td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                            <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table id="Table2" align="center" border="0" cellpadding="0" cellspacing="0" class="table-normal"
                                        style="height: 100%" width="100%">
                                        <tr>
                                            <td>
                                                <div id="dvDeptBox" class="div-scroll" style="width: 100%; height: 100%">
                                                    <asp:GridView ID="GVProjectFile" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="sqlProjecteFile" PageSize="16" Width="100%" OnRowDataBound="GVProjectFile_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                                            <table id="GVProjectFile" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse;">
                                                                <tr class="grid_head">
                                                                    <th scope="col" style="width: 70px;">
                                                                        项目名称
                                                                    </th>
                                                                    <th scope="col" style="width: 70px;">
                                                                        档案编号
                                                                    </th>
                                                                    <th scope="col" style="width: 70px;">
                                                                        文档名称</th>
                                                                    <th scope="col" style="width: 70px">
                                                                        密级</th>
                                                                    <th scope="col" style="width: 120px">
                                                                        备注</th>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><Columns><asp:TemplateField HeaderText="选择"><ItemTemplate>
                                                                    <asp:CheckBox ID="CBRecordID" runat="server" />
                                                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PrjGuid" HeaderText="项目编号" SortExpression="PrjGuid" /><asp:BoundField DataField="FileTitle" HeaderText="档案名称" SortExpression="FileTitle" /><asp:BoundField DataField="FileCode" HeaderText="档案编号" SortExpression="FileCode" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" SortExpression="SecretLevel" /><asp:BoundField DataField="Remark" HeaderText="备 注" SortExpression="Remark" /></Columns></asp:GridView>
                                                    <asp:SqlDataSource ID="sqlProjecteFile" SelectCommand="SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info] " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table id="Table3" align="center" border="0" cellpadding="0" cellspacing="0" class="table-normal"
                                        style="height: 100%" width="100%">
                                        <tr>
                                            <td>
                                                <div id="Div1" class="div-scroll" style="width: 100%; height: 100%">
                                                    <asp:GridView ID="GVeFile" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="sqlProjecteFile" PageSize="16" Width="100%" OnRowDataBound="GVeFile_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                                            <table id="GVProjectFile" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse;">
                                                                <tr class="grid_head">
                                                                    <th scope="col" style="width: 70px;">
                                                                        项目名称
                                                                    </th>
                                                                    <th scope="col" style="width: 70px;">
                                                                        档案编号
                                                                    </th>
                                                                    <th scope="col" style="width: 70px;">
                                                                        文档名称</th>
                                                                    <th scope="col" style="width: 70px">
                                                                        密级</th>
                                                                    <th scope="col" style="width: 120px">
                                                                        备注</th>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="选择"><ItemTemplate>
                                                                    <asp:CheckBox ID="CBRecordID" runat="server" />
                                                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="FileTitle" HeaderText="档案名称" SortExpression="FileTitle" /><asp:BoundField DataField="FileCode" HeaderText="档案编号" SortExpression="FileCode" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" SortExpression="SecretLevel" /><asp:BoundField DataField="Remark" HeaderText="备 注" SortExpression="Remark" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </ContentTemplate>
</asp:UpdatePanel>
                </td></tr>
            </table>
            <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    
    </div>
    </form>
</body>
</html>
