<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnexList.aspx.cs" Inherits="AnnexList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>附件列表</title>
    <base target="_self" />
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            var GridView1Table = new JustWinTable('dgdAnnex');
            if (request("Type") == "0") {
                if (document.getElementById("btnAdd") != null) {
                    document.getElementById("btnAdd").disabled = false;
                }

                if (document.getElementById("btnDel") != null) {
                    document.getElementById("btnDel").disabled = false;
                }
            }
        })

        function clickAnnexRow(btnObj, hdnObj, annexCode) {
            try {
                if (document.getElementById(btnObj) != null) {
                    document.getElementById(btnObj).disabled = false;
                }
                document.getElementById(hdnObj).value = annexCode;
                if (request("Type") == "0") {
                    if (document.getElementById("btnAdd") != null) {
                        document.getElementById("btnAdd").disabled = false;
                    }
                    if (document.getElementById("btnDel") != null) {
                        document.getElementById("btnDel").disabled = false;
                    }
                }
            }
            catch (e) { }
        }

        function openAnnexEdit(moduleID, annexType) {
            var url = "/CommonWindow/Annex/AnnexAdd.aspx?mid=" + moduleID + "&at=" + annexType;
            return window.showModalDialog(url, window, "dialogHeight:170px;dialogWidth:400px;center:1;help:0;status:0;");
        }

        function upLoadFile(fid, mid) {
            var url = "/CommonWindow/Annex/upLoadFile.aspx?mid=" + mid + "&fid=" + fid;
            window.showModalDialog(url, window, "dialogHeight:180px;dialogWidth:400px;center:1;help:0;status:0;");
            window.location.reload();
            return true;
        }

        // 用来接收处理 url参数
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
		
    </script>
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <asp:HiddenField ID="hdfCode" runat="server" />
    <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="1">
        <tr>
            <td valign="top" height="90%" colspan="3">
                <div class="pagediv">
                    <asp:DataGrid ID="dgdAnnex" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" DataKeyField="annexCode" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="header"></HeaderStyle><Columns><asp:TemplateColumn Visible="false">
<HeaderTemplate>
                                    <input id="chkAll" type="checkbox" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                    <%# Container.ItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="AddDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="FileCode" HeaderText="文件编号"></asp:BoundColumn><asp:TemplateColumn HeaderText="文件名称" HeaderStyle-HorizontalAlign="Center">
<ItemTemplate>
                                    <asp:HyperLink NavigateUrl="" Text='<%# Convert.ToString(Eval("OriginalName")) %>' runat="server"></asp:HyperLink>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="Remark" HeaderText="备注" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn></Columns></asp:DataGrid></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divFooter2">
                    <table class="tableFooter2">
                        <tr>
                            <td>
                                <asp:Label ID="lblAnnexState" runat="server"></asp:Label>
                                <input id="hdnAnnexCode" style="width: 10px" type="hidden" name="hdnAnnexCode" runat="server" />

                                <input id="hdnContractCode" style="width: 10px" type="hidden" name="hdnContractCode" runat="server" />

                                <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" Text="新增" runat="server" />
                                <asp:Button ID="btnDel" Enabled="false" Text="删除" runat="server" />
                                <input onclick="javascript:returnValue=false;window.close();" type="button" value="确定" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
