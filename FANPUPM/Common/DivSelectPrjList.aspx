<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectPrjList.aspx.cs" Inherits="Common_DivSelectPrjList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

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
        $(document).ready(function () {
            if ($('#hfldApplyPage').val() == 'Treasury') {
                $('#btnSave').removeAttr('disabled');
            }
            var prjView = new JustWinTable('grdModules');
            if (prjView.getCheckedChk().length > 0) {
                $('#btnSave').removeAttr('disabled');
            }
            else {
                if ($('#hfldApplyPage').val() != 'Treasury') {
                    $('#btnSave').attr('disabled', 'disabled');
                }
            }
            prjView.registClickTrListener(function () {
                if (this.prjState != '17' && this.setUpFlowState == '1' && this.ISDISPLAY != "0") {
                    $('#btnSave').removeAttr('disabled');
                } else {
                    $('#btnSave').attr('disabled', 'disabled');
                }
            });

            prjView.registClickSingleCHKListener(function () {
                if (prjView.getCheckedChk().length > 0) {
                    $('#btnSave').removeAttr('disabled');
                }
                else {
                    if ($('#hfldApplyPage').val() != 'Treasury') {
                        $('#btnSave').attr('disabled', 'disabled');
                    }
                }
                var prjIdChecked = '';
                var prjNameChecked = '';
                $('#hdName').val(prjNameChecked);
                $('#hdCode').val(prjIdChecked);
                $('#grdModules [type=checkbox]').each(function () {
                    if (this.checked) {
                        var prjState = this.parentNode.parentNode.prjState;
                        var setUpFlowState = this.parentNode.parentNode.setUpFlowState;
                        var ISDISPLAY = this.parentNode.parentNode.ISDISPLAY;
                        if (prjState != '17' && setUpFlowState == '1' && ISDISPLAY != "0") {
                            var prjId = this.parentNode.parentNode.id;
                            var prjName = this.parentNode.parentNode.prjName;
                            prjNameChecked += prjName + ',';
                            prjIdChecked += ',' + prjId;
                        }
                    }
                });
                $('#hdName').val(prjNameChecked);
                $('#hdCode').val(prjIdChecked);
            });

            prjView.registClickAllCHKLitener(function () {
                if (this.checked) {
                    $('#btnSave').removeAttr('disabled');
                    var prjIdChecked = '';
                    var prjNameChecked = '';
                    $('#hdName').val(prjNameChecked);
                    $('#hdCode').val(prjIdChecked);
                    $('#grdModules [type=checkbox]').each(function () {
                        if (this.id != 'grdModules_ctl01_CheckBox2') {
                            var prjState = this.parentNode.parentNode.prjState;
                            var setUpFlowState = this.parentNode.parentNode.setUpFlowState;
                            var ISDISPLAY=this.parentNode.parentNode.ISDISPLAY;
                            if (prjState != '17' && setUpFlowState == '1' && ISDISPLAY !="0") {
                                var prjId = this.parentNode.parentNode.id;
                                var prjName = this.parentNode.parentNode.prjName;
                                prjNameChecked += prjName + ',';
                                prjIdChecked += ',' + prjId;
                            }
                        }
                    });
                    $('#hdName').val(prjNameChecked);
                    $('#hdCode').val(prjIdChecked);
                }
                else {
                    if ($('#hfldApplyPage').val() != 'Treasury') {
                        $('#btnSave').attr('disabled', 'disabled');
                    }
                    $('#hdName').val('');
                    $('#hdCode').val('');
                }
            });
            formateTable('grdModules', 3);
        });
        function clickRow(obj, moduleCode, isLeaf, theCode, theName,isValid) {
            $("#hdName").val(theName);
            $("#hdCode").val(theCode);
            if (isValid == '0') {
                $("#btnSave").attr('disabled', 'disabled');
            } else {
                $("#btnSave").removeAttr('disabled');
            }
        }

        function dbClickRow(obj, theCode, theName, isLeaf) {
            var method = getRequestParam('Method'); //方法
            parent[method](theCode, theName);
            divClose(parent);
        }
        //点确定
        function btnOk() {
            if ($("#hdCode").val() != null && $("#hdName").val() != null) {
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
    <table id="Table2" class="table-nomal" height="85%" cellspacing="0" cellpadding="0"
        width="100%">
        <tr class="td-title">
            <td width="20" style="border: solid 0px red;">
                <input class="input_hide" id="hdnModuleCode" type="hidden" name="hdnModuleCode" runat="server" />

            </td>
        </tr>
        <tr>
            <td style="text-align: left; border: solid 0px red;">
                项目编号：<asp:TextBox ID="prjcode" Width="78px" runat="server"></asp:TextBox>&nbsp;
                项目名称：<asp:TextBox ID="prjname" Width="120px" runat="server"></asp:TextBox>&nbsp;
                <asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
                <asp:HiddenField ID="hdfRQis" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" style="border: solid 0px red; height: 100%; padding-top: 10px;">
                <div id="pagediv" style="width: 100%; border: solid 0px red; text-align: left;" align="center">
                    <asp:DataGrid ID="grdModules" DataKeyField="PrjCode" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="gvdata" runat="server"><PagerStyle Mode="NumericPages"></PagerStyle><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="全选">
<HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="项目编号">
<ItemTemplate>
                                    <asp:Label ID="Label2" name="Label2" Text='<%# System.Convert.ToString(Eval("DataItem.PrjCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdfprjCode" Value='<%# System.Convert.ToString(Eval("prjGuid"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    <asp:HiddenField ID="hfldIsMainContract" Value='<%# System.Convert.ToString(Eval("DataItem.isMainContract"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    <asp:HiddenField ID="hdfPrjName" Value='<%# System.Convert.ToString(Eval("DataItem.prjName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TypeCode" HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="prjName" HeaderText="项目名称"></asp:BoundColumn><asp:BoundColumn DataField="Owner" HeaderText="建设单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjCost" HeaderText="工程造价" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn HeaderText="项目地点" DataField="PrjPlace"></asp:BoundColumn><asp:BoundColumn DataField="PrjState" HeaderText="状态"></asp:BoundColumn><asp:BoundColumn DataField="ISDISPLAY" HeaderText="!已用" Visible="false"></asp:BoundColumn></Columns></asp:DataGrid>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
        <tr class="td-submit">
            <td align="right" style="border: solid 0px red;">
                <input type="hidden" id="hdn1" name="hdn1" style="width: 10px" runat="server" />

                <input type="hidden" id="hdn2" name="hdn2" style="width: 10px" runat="server" />

                <input type="hidden" id="hdn3" name="hdn3" style="width: 10px" runat="server" />

            </td>
        </tr>
    </table>
    <div style="text-align: right">
        <input id="btnSave" type="button" value="确 定" onclick="btnOk();" disabled="disabled" />
        <input id="btnCancel" type="button" value="取 消" onclick="divClose(parent);" />
    </div>
    <input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hdCode" runat="server" />
    
    <asp:HiddenField ID="hfldApplyPage" runat="server" />
    </form>
</body>
</html>
