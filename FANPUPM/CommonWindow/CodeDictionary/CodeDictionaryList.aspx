<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeDictionaryList.aspx.cs" Inherits="CommonWindow_CodeDictionary_CodeDictionaryList" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>编码字典</title>
<script language="javascript" type="text/javascript">
    <!--
     function ClickRow(recordid)
    {
        document.getElementById('HdnRecoreId').value = recordid;         
        document.getElementById('btnEdit').disabled = false;  
        document.getElementById('btnDel').disabled = false;
        
        
    }
    //编码字典申请添加\编辑
    function openEdit(t)
    {   
       
        var rid =     document.getElementById('HdnRecoreId').value ;   
        var classcode= document.getElementById('HdnClassCode').value;    
        if(classcode != "")
        {                
    	    if(t=='Add')
    	    {
    	        var url = 'CodeDictionaryEdit.aspx?t='+t+'&rid=0&cc='+classcode;
    	    }
    	    else
    	    {
    	        var url = 'CodeDictionaryEdit.aspx?t='+t+'&rid='+rid+'&cc='+classcode;
    	    }     	
		    return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:400px;center:1;help:0;status:0;");
		}
		else
		{
		    alert('请选择类别!');
		    return false;
		}
    } 
    
    -->    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div>
      <table style="height:100%;width:100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">            
            <tr>
                <td valign="top" style="width:25%" rowspan="2" >  
                    <table border="0" style="height:100%;width:100%">
                        <tr><td valign="top">                      
                            <asp:TreeView ID="TVDictionaryClass" ShowLines="true" OnSelectedNodeChanged="TVDictionaryClass_SelectedNodeChanged" runat="server"></asp:TreeView>
                            
                     </td></tr>
                    </table>
                </td>
                </tr>
                <tr>
                <td valign="top">
                 <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">            
                        <tr>               
                            <td class="td-title">编码字典
                                </td>
                        </tr>
                            <tr>
                            <td class="td-toolsbar" style="height: 24px">             
                                <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />&nbsp;
                                <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;
                                <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;                                      
                                <asp:HiddenField ID="HdnRecoreId" runat="server" />                    
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
                                <asp:HiddenField ID="HdnClassCode" runat="server" />
                                    </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="TVDictionaryClass" EventName="SelectedNodeChanged" runat="server" /></Triggers></asp:UpdatePanel>
                               
                                </td>
                        </tr>  
                        <tr>
                            <td>
                                 <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                                     <asp:GridView ID="GVPTDictionaryItem" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlDictionaryItem" PageSize="25" Width="100%" OnRowDataBound="GVPTDictionaryItem_RowDataBound" DataKeyNames="KeyValue" runat="server">
<EmptyDataTemplate>
                                             <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                                 border-collapse: collapse">
                                                 <tr class="grid_head">
                                                     <th scope="col" style="width: 40px">
                                                         序号</th>
                                                     <th scope="col">
                                                         名 称</th>
                                                     <th scope="col" style="width: 60px">
                                                         是否有效</th>
                                                 </tr>
                                             </table>
                                         </EmptyDataTemplate>
<Columns><asp:BoundField DataField="KeyValue" HeaderText="序号" InsertVisible="false" ReadOnly="true" SortExpression="KeyValue" /><asp:BoundField DataField="DisplayValue" HeaderText="名 称" SortExpression="DisplayValue" /><asp:TemplateField HeaderText="是否有效" SortExpression="IsValid">
<ItemTemplate>
                                                     &nbsp;<asp:CheckBox ID="CBIsValid" Checked="true" Enabled="false" runat="server" />
                                                 </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                                         </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="TVDictionaryClass" EventName="SelectedNodeChanged" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                                     &nbsp;
                                     <asp:SqlDataSource ID="SqlDictionaryItem" SelectCommand="SELECT [ClassID], [KeyValue], [DisplayValue], [IsValid] FROM [PT_DictionaryItem] WHERE [ClassID] = ''" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                                 </div>
                                 </td>
                        </tr>
                  </table>
      </td></tr>
      </table>
    </div>
    </form>
</body>
</html>
