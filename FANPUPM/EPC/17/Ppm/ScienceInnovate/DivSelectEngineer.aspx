<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectEngineer.aspx.cs" Inherits="EPC_17_Ppm_ScienceInnovate_DivSelectEngineer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script src="/Web_Client/Tree.js" type="text/javascript"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
 <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>   
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function() {            
            var aa = new JustWinTable('grdModules');
        });
		    function clickRow(obj, theCode, theName) {		        
		        $("#hdName").val(theName);
		        $("#hdCode").val(theCode);
		        $("#btnSave").removeAttr('disabled');
		    }

		    function dbClickRow(obj, theCode, theName) {
		        var method = getRequestParam('Method'); //方法
		        parent[method](theCode, theName);
		        divClose(parent);
		    }
		    //点确定
		    function btnOk() {
		        if ($("#hdCode").val() != null && $("#hdName").val()!=null) {
		            var method = getRequestParam('Method'); //方法
		            parent[method]($("#hdCode").val(), $("#hdName").val());
		        }
		        divClose(parent);
		    }
		    function queryEngineer(path,title) {
		       // parent.parent.desktop.DivSelectEngineer = window;
		        toolbox_oncommand(path, title);
		   }
    </script>
<style type="text/css">
    
</style>
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" class="table-nomal" height="100%" cellspacing="0" cellpadding="0"
        width="100%">
        <tr class="td-title">
            <td width="20" style="border:solid 0px red;" >
                <input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

            </td>
        </tr>
        <tr>
            <td valign="top" align="center"  style="border:solid 0px red;height:100%; padding-top:10px;">
                <div id="pagediv" style="width:100%; overflow:auto; border:solid 0px red; text-align:left;" align="center">
                    <asp:GridView ID="gvVehicle" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnRowDataBound="gvVehicle_RowDataBound" OnPageIndexChanging="gvVehicle_PageIndexChanging" DataKeyNames="ID,SerialNumber" runat="server">
<EmptyDataTemplate>
                            <table class ="rowa"  style =" width :100%; text-align :center ;" >
                                <tr class="header">
                                    <td style =" width :25px;">序号</td>
                                    <td>资料编号</td>
                                    <td>资料名称</td>
                                    <td>备注</td>
                                    <td>附件</td>
                                    <td>类型</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="ID" DataField="ID" Visible="false" /><asp:BoundField HeaderText="资料编号" DataField="SerialNumber" /><asp:HyperLinkField DataTextField="ItemName" HeaderText="资料名称" /><asp:BoundField HeaderText="备注" DataField="Remark" /><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                  
                                     <%# GetAnnx(Convert.ToString(Eval("ID"))) %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                                        <%# switchTypeName(int.Parse(Eval("BigSort").ToString())) %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
        <tr class="td-submit">
            <td align="right" style="border:solid 0px red;">               
                <input id="btnSave" type="button" value="确 定" onclick="btnOk();" disabled="disabled" />
                <input id="btnCancel" type="button" value="取 消" onclick="divClose(parent);" />
            </td>
        </tr>
    </table>
    <input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hdCode" runat="server" />    
    </form>
</body>
</html>
