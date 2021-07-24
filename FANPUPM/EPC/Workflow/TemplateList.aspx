<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateList.aspx.cs" Inherits="TemplateList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>TemplateList</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/TemplateTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function doClickRow(businessCode, businessClass, templateId, isGeneral) {
            if (businessClass.length > 0) {
                document.getElementById('btnEdit').disabled = false;
                document.getElementById('btnLimit').disabled = false;
            }
            else {
                document.getElementById('btnEdit').disabled = true;
                document.getElementById('btnLimit').disabled = true;
            }
            document.getElementById('btnDel').disabled = false;
            var url;
            document.getElementById('hdnTemplateID').value = templateId;
            url = "FlowChart.aspx?tid=" + templateId + "&bncode=" + businessCode + "&class=" + businessClass + "&flag=" + isGeneral;
            parent.frames[1].location = url;
        }

        function EditorAdd(op, businessCode, businessClass) {
            var templateID = 0;
            var isGeneral = document.getElementById('hdnIsGeneral').value;
            if (op == 2) {
                templateID = document.getElementById('hdnTemplateID').value;
            }
            var url = "/EPC/Workflow/TemplateEdit.aspx?t=" + op + "&ti=" + templateID + "&code=" + businessCode + "&class=" + businessClass + "&flag=" + isGeneral;

			top.ui._wfTemplateList = window;
            toolbox_oncommand(url, "流程模板维护");
        }

        function LimitAdd(businessCode, businessClass) {
            var isGeneral = document.getElementById('hdnIsGeneral').value;
            var templateId = document.getElementById('hdnTemplateID').value;
            var url = "/EPC/Workflow/TemplatePrivilege.aspx?tid=" + templateId + "&code=" + businessCode + "&class=" + businessClass + "&flag=" + isGeneral;
            viewopen(url);
        }

        function initAjax() {
            var ajax = false;
            try {
                ajax = new ActiveXObject("Msxml2.XMLHTTP");
            }
            catch (e) {
                try {
                    ajax = new ActiveXObject("Microsoft.XMLHTTP");
                }
                catch (e) {
                    ajax = false;
                }
            }
            if (!ajax && typeof XMLHttpRequest != 'undefined') {
                ajax = new XMLHttpRequest();
            }
            return ajax;
        }

        function returnConfirm() {
            var ajax = initAjax();
            var temp = "";
            var url = "TemplateConfirm.ashx?tid=" + document.getElementById("hdnTemplateID").value; //处理程序的路径一定要正确
            ajax.open("GET", url, false);
            ajax.setRequestHeader("If-Modified-Since", "0");
            ajax.onreadystatechange = function () {
                if (ajax.readyState == 4 && ajax.status == 200) {
                    if (ajax.responseText == "1") {
                        temp = "你确定删除该项吗";
                    }
                    else {
                        temp = "该流程正在使用，但删除不会对已经提交的流程造成影响，您确认删除吗！";
                    }
                }
            }
            return temp;
        }

        addEvent(window, 'load', function () {
            var jwTable = new JustWinTable('dgFlow');
            setHeight();
            hideFirstPageNo();
            var arrPager = document.querySelectorAll('.GD-Pager');
            for (var i = 0; i < arrPager.length; i++) {
                var currPager = arrPager[i];
                currPager.setAttribute('onclick', 'clickFooter();');
            }
        });

        function clickFooter() {
            document.getElementById('btnEdit').disabled = true;
            document.getElementById('btnDel').disabled = true;
            document.getElementById('btnLimit').disabled = true;         
        }

        function setHeight() {
            var height = document.getElementById("td-flow").clientHeight;
            document.getElementById("templatediv").style.height = height;
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <input id="hfldPurchaseChecked" type="hidden" />
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    <div style=" height:200px;  overflow:auto;">
    <table style="height: 400; width: 100%;">
        
        <tr id="btnTr" height="22px" runat="server"><td style="text-align: left" runat="server">
                <input type="hidden" id="hdnIsGeneral" name="hdnIsGeneral" runat="server" />

                <input type="hidden" id="hdnTemplateID" name="hdnTemplateID" runat="server" />

                <input type="button" value="新增" id="btnAdd" runat="server" />

                <input type="button" value="修改" id="btnEdit" disabled="true" runat="server" />

                <asp:Button ID="btnDel" Text="删 除" Enabled="false" runat="server" />
                <asp:Button ID="btnLimit" Text="权限" Enabled="false" runat="server" />
            </td></tr>
        <tr>
            <td style="vertical-align: top;" id="td-flow">
                <div style="overflow: auto;" id="templatediv">
                    <asp:DataGrid ID="dgFlow" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" Width="100%" PageSize="8" OnPageIndexChanged="dgFlow_PageIndexChanged" runat="server"><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                               <%# Container.ItemIndex +1 %>     
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="名称">
<ItemTemplate>
                                    <asp:Label ID="TxtFlowName" Width="100%" Text='<%# System.Convert.ToString(Eval("TemplateName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="废止">
<ItemTemplate>
                                    <asp:CheckBox ID="CkbIsAbnormality" Enabled="false" Checked='<%# (Eval( "IsAbnormality").ToString() == "1") %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="通用流程" Visible="false">
<ItemTemplate>
                                    <asp:CheckBox ID="CkbIsGeneral" Enabled="false" Checked='<%# (Eval( "IsGeneral").ToString() == "1") %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
                                    <asp:Label ID="TxtRemark" Width="100%" Text='<%# System.Convert.ToString(Eval("Remark"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="完成">
<ItemTemplate>
                                    <asp:CheckBox ID="CkbIsComplete" Enabled="false" Checked='<%# (Eval( "IsComplete").ToString() == "1") %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn></Columns><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
                </div>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
