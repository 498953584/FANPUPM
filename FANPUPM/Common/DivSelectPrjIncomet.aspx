<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectPrjIncomet.aspx.cs" Inherits="Common_DivSelectPrjIncomet" %>
<%@ Import Namespace="cn.justwin.BLL"%>
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
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script src="/Web_Client/Tree.js" type="text/javascript"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            showTooltip('tooltip', 15);
            var aa = new JustWinTable('grdModules');
            formateTable('grdModules', 1);
        });
        function clickRow(obj, moduleCode, isLeaf, theCode, theName, thePrjCode, prjFundWorkable, forecastProfitRate, qualityClass, PrjFundInfo, CodeName, isValid) {
            $("#hdName").val(theName);
            $("#hdCode").val(theCode);
            $("#hdPrjCode").val(thePrjCode);
            $("#hdprjFundWorkable").val(prjFundWorkable);
            $("#hdforecastProfitRate").val(forecastProfitRate);
            $("#hdqualityClass").val(qualityClass);
            $("#hdPrjFundInfo").val(PrjFundInfo);
            $("#hdCodeName").val(CodeName);
            if (isValid == '0') {
                $("#btnSave").attr('disabled', 'disabled');
            } else {
                $("#btnSave").removeAttr('disabled');
            }
        }

        function dbClickRow(obj, theCode, theName, thePrjCode, prjFundWorkable, forecastProfitRate, qualityClass, PrjFundInfo, CodeName, isLeaf) {
            btnOk();
        }
        //点确定
        function btnOk() {
            if (typeof top.ui.callback == 'function') {
                top.ui.callback($("#hdCode").val(), $("#hdName").val(), $("#hdPrjCode").val(),
                    $("#hdprjFundWorkable").val(), $("#hdforecastProfitRate").val(), $("#hdqualityClass").val(),
                    $("#hdPrjFundInfo").val(), $("#hdCodeName").val());
            }
            top.ui.callback = null;
            top.ui.closeWin();
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
                项目编号：<asp:TextBox ID="prjcode" Width="120px" runat="server"></asp:TextBox>&nbsp;
                项目名称：<asp:TextBox ID="prjname" Width="120px" runat="server"></asp:TextBox>&nbsp;
                <asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" style="border: solid 0px red; height: 100%; padding-top: 10px;">
                <div id="pagediv" style="width: 100%; border: solid 0px red; text-align: left;" align="center">
                    <asp:DataGrid ID="grdModules" DataKeyField="PrjCode" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="gvdata" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><PagerStyle Mode="NumericPages"></PagerStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="项目编号">
<ItemTemplate>
                                    <asp:Label ID="Label1" name="Label1" Text='<%# System.Convert.ToString(Eval("DataItem.HeadImg"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    <span style="vertical-align: middle;">
                                        <%# Eval("DataItem.PrjCode") %>
                                        <asp:HiddenField ID="hfldIsMainContract" Value='<%# System.Convert.ToString(Eval("DataItem.isMainContract"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </span>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TypeCode" HeaderText="序号" Visible="false"></asp:BoundColumn><asp:TemplateColumn HeaderText="项目名称">
<ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("prjName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("prjName").ToString(), 15) %>
                                    </span>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="Owner" HeaderText="建设单位"></asp:BoundColumn><asp:BoundColumn DataField="PrjCost" HeaderText="工程造价" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="startdate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}" Visible="false"></asp:BoundColumn><asp:BoundColumn HeaderText="项目地点" DataField="PrjPlace"></asp:BoundColumn><asp:BoundColumn DataField="PrjState" HeaderText="状态"></asp:BoundColumn></Columns></asp:DataGrid>
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
        <input id="btnCancel" type="button" value="取 消" onclick="top.ui.closeWin();" />
    </div>
    <input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hdCode" runat="server" />
    <asp:HiddenField ID="hdPrjCode" runat="server" />
    <asp:HiddenField ID="hdprjFundWorkable" runat="server" />
    <asp:HiddenField ID="hdforecastProfitRate" runat="server" />
    <asp:HiddenField ID="hdqualityClass" runat="server" />
    <asp:HiddenField ID="hdPrjFundInfo" runat="server" />
    <asp:HiddenField ID="hdCodeName" runat="server" />
    </form>
</body>
</html>
