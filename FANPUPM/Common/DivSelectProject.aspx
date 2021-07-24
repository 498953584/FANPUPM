<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectProject.aspx.cs" Inherits="Common_DivSelectProject" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目信息</title>
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script src="/Web_Client/Tree.js" type="text/javascript"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Script/json2.js"></script>
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            showTooltip('tooltip', 23);
            var prjTab = new JustWinTable('grdModules');
            formateTable('grdModules', 1);
        });
        function clickRow(obj, moduleCode, isLeaf, theCode, theName, thePrjCode, CodeName, isValid) {
            $("#hdName").val(theName);
            $("#hdCode").val(theCode);
            $("#hdPrjCode").val(thePrjCode);
            $("#hdCodeName").val(CodeName);

            // Json序列化
            var prjObj = { prjId: theCode, prjName: theName, prjCode: thePrjCode, typeCode: moduleCode };
            $('#hdPrjInfo').val(JSON.stringify(prjObj));

            if (isValid == 0) {
                $("#btnSave").attr('disabled', 'disabled');
            } else {
                $("#btnSave").removeAttr('disabled');
            }
        }

        function dbClickRow(obj, theCode, theName, thePrjCode, CodeName, isLeaf) {
            var method = getRequestParam('Method'); 	// 方法
            var callBack = getRequestParam('CallBack'); // 返回详细信息的方法, Json字符串
            var isPhoto = getRequestParam('isPhoto'); // 付页面是否是拍照监控的页面  

            // 执行回调方法
            try {
                if (!!method)
                    parent[method](theCode, theName, thePrjCode, CodeName);

                if (!!callBack) {
                    parent[callBack]($('#hdPrjInfo').val());
                }
            } catch (ex) { }

            divClose(parent);

            if (isPhoto == "true") {
                parent.document.getElementById('btnDisplayBud').click();
            }
        }
        //点确定
        function btnOk() {
            var isPhoto = getRequestParam('isPhoto'); //付页面是否是拍照监控的页面
            var callBack = getRequestParam('CallBack'); // 返回详细信息的方法, Json字符串

            try {
                if ($("#hdCode").val() != null && $("#hdName").val() != null) {
                    var method = getRequestParam('Method'); //方法
                    if (!!method)
                        parent[method]($("#hdCode").val(), $("#hdName").val(), $("#hdPrjCode").val(), $("#hdCodeName").val());
                }

                if (!!callBack) {
                    parent[callBack]($('#hdPrjInfo').val());
                }

                divClose(parent);
                if (isPhoto == "true") {
                    parent.document.getElementById('btnDisplayBud').click();
                }
            }
            catch (ex) { }
        }

        // 取消
        function cancle() {
            divClose(parent)

            var callBack = getRequestParam('CallBack');
            if (!!callBack)
                parent[callBack]();
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
                    <asp:DataGrid ID="grdModules" DataKeyField="PrjCode" AutoGenerateColumns="false" CellPadding="0" Width="100%" CssClass="gvdata" AllowPaging="false" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><PagerStyle Mode="NumericPages"></PagerStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="项目编号">
<ItemTemplate>
                                    <asp:Label ID="Label1" name="Label1" Text='<%# System.Convert.ToString(Eval("HeadImg"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    <span style="vertical-align: middle;">
                                        
                                        <%# Eval("PrjCode") %>
                                        <asp:HiddenField ID="hfldIsMainContract" Value='<%# System.Convert.ToString(Eval("isMainContract"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
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
        <input id="btnCancel" type="button" value="取 消" onclick="cancle();" />
    </div>
    <input id="hdnModuleCodeList" style="width: 10px" type="hidden" name="hdnModuleCodeList" runat="server" />

    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hdCode" runat="server" />
    <asp:HiddenField ID="hdPrjCode" runat="server" />
    <asp:HiddenField ID="hdPrjInfo" runat="server" />
    <asp:HiddenField ID="hdCodeName" runat="server" />
    </form>
</body>
</html>
