<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dbrInfo.aspx.cs" Inherits="dbrInfo" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>代办人设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        function setHeight() {
            var height = document.getElementById("td-person").clientHeight;
            //document.getElementById("divperson").style.height = height;
        }
        addEvent(window, 'load', function () {
            var jwTable = new JustWinTable('dgFlow');
            setHeight();
        });

        function doClickRow(nodeId, onuse) {
            document.getElementById('btnEdit').disabled = false;
            if (onuse == "False") {
                document.getElementById('btnDel').disabled = false;
                document.getElementById('btnonUse').disabled = false;
            }
            else {
                document.getElementById('btnDel').disabled = false;
                document.getElementById('btnonUse').disabled = true;
            }

            document.getElementById('hdnNodeId').value = nodeId;
        }

        function openDbrEdit(op, templateId, userCode, nodeid) {
            var nodeId = 0;
            var url = "";
            if (op == 2) {
                nodeId = document.getElementById('hdnNodeId').value;
            }
            var url = "/EPC/WorkFlow/dbrEdit.aspx?t=" + op + "&tid=" + templateId + "&code=" + userCode + "&nid=" + nodeId + "&Tempnid=" + nodeid;
            top.ui._dbrinfo = window;
            //            window.parent.parent.desktop.flowclass = window;
            toolbox_oncommand(url, "代办人维护");
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <table width="100%" style="height: 99%">
        <tr>
            <td class="divHeader" align="center" height="20px">
                代办人设置
            </td>
        </tr>
        <tr id="btntr" height="24" runat="server"><td id="Td1" runat="server">
                <input type="hidden" id="hdnNodeId" name="hdnNodeId" runat="server" />

                <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                <asp:Button ID="btnonUse" Text="启动" Enabled="false" OnClick="btonUse_Click" runat="server" />
            </td></tr>
        <tr>
            <td style="vertical-align: top" id="td-person">
                <div style="overflow: auto; width: 100%; height: 100%" id="divperson">
                    <asp:GridView ID="dgFlow" CssClass="gvdata" DataKeyNames="" AutoGenerateColumns="false" OnRowDataBound="dgFlow_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="emptyDgFlow" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" />
                                    </th>
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        审核人
                                    </th>
                                    <th scope="col">
                                        代办人
                                    </th>
                                    <th scope="col">
                                        状态
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="审核人"><ItemTemplate>
                                    <%# Eval("auditor") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="代办人"><ItemTemplate>
                                    <%# Eval("agent") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="状态"><ItemTemplate>
                                    <%# GetState(Eval("bt_Use").ToString()) ? "<font color='green'>启动</font>" : "<font color='red'>停止</font>" %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
