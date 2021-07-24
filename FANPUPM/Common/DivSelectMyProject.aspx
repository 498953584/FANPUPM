<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectMyProject.aspx.cs" Inherits="Common_DivSelectMyProject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目信息</title>
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />


  

    <script src="/Web_Client/Tree.js" type="text/javascript"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
 <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>

   
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function() {            
            var aa = new JustWinTable('grdModules');
        });
        function clickRow(obj, moduleCode, isLeaf, theCode, theName, accouid, accidname, monny) {		        
		        $("#hdName").val(theName);
		        $("#hdCode").val(theCode);
		        $("#btnSave").removeAttr('disabled');
		    }

		    function dbClickRow(obj, theCode, theName,accouid,accidname,monny, isLeaf) {
		        var method = getRequestParam('Method'); //方法
		        parent[method](theCode, theName, accouid, accidname, monny);
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
            <td  style="text-align: left;border:solid 0px red;" >
                项目编号：<asp:TextBox ID="prjcode" Width="78px" runat="server"></asp:TextBox>&nbsp;
                项目名称：<asp:TextBox ID="prjname" Width="120px" runat="server"></asp:TextBox>&nbsp;
                <asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center"  style="border:solid 0px red;height:100%; padding-top:10px;">
                <div id="pagediv" style="width:100%; border:solid 0px red; text-align:left;" align="center">
                    <asp:DataGrid ID="grdModules" DataKeyField="PrjCode" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="gvdata" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:BoundColumn DataField="TypeCode" HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="prjName" HeaderText="项目名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="账号名称">
<ItemTemplate>
                                <asp:HiddenField ID="hdfacid" Value='<%# System.Convert.ToString(base.GetBankNum(Eval("prjGuid").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            <asp:Label ID="lblname" Text='<%# System.Convert.ToString(base.GetBankName(Eval("prjGuid").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="Owner" HeaderText="建设单位"></asp:BoundColumn><asp:TemplateColumn HeaderText="预算金额">
<ItemTemplate>
                                    <asp:Label ID="LabePrjCost" Text='<%# System.Convert.ToString(Eval("DataItem.PrjCost"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="PrjState" HeaderText="状态"></asp:BoundColumn></Columns></asp:DataGrid></div>
            </td>
        </tr>
        <tr class="td-submit">
            <td align="right" style="border:solid 0px red;">
                <input type="hidden" id="hdn1" name="hdn1" style="width: 10px" runat="server" />

                <input type="hidden" id="hdn2" name="hdn2" style="width: 10px" runat="server" />

                <input type="hidden" id="hdn3" name="hdn3" style="width: 10px" runat="server" />

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
