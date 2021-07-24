<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contactcorplistview.aspx.cs" Inherits="ContactCorpListView" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server"><title>单位信息</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#Txt_CorpSearch').css('display', 'none');
            //如果只有一页的数据,隐藏页码
            hideFirstPageNo();
        });
    </script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table class="table-none" height="100%" cellspacing="0" cellpadding="0" width="100%"
        border="0">
        <tr>
            <td class="td-title" height="20">
                单 位 信 息
            </td>
        </tr>
        <tr>
            <td align="left" height="24">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td>
                            单位名称<asp:TextBox ID="txtCorpName" Width="120px" runat="server"></asp:TextBox>
                            单位性质<asp:DropDownList ID="DDL_CorpKind" Width="120px" runat="server"></asp:DropDownList>
                            单位联系人<asp:TextBox ID="txtLinkMan" Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="td-toolsbar" style="text-align: left" height="20">
                <input id="HdnCorpID" style="width: 10px" type="hidden" name="HdnCorpID" runat="server" />

                <input id="HdnTypeID" style="width: 10px" type="hidden" name="HdnTypeID" runat="server" />

                <input id="HdnCorpName" style="width: 10px" type="hidden" name="HdnCorpName" runat="server" />

                <input id="hdnRecordID" style="width: 10px" type="hidden" name="hdnRecordID" runat="server" />

                <asp:TextBox ID="Txt_CorpSearch" Width="0px" runat="server"></asp:TextBox>
                <asp:Button ID="BtnAdd" Text="新  增" Width="0px" Visible="false" OnClick="BtnAdd_Click" runat="server" />
                <asp:Button ID="BtnMod" Text="编  辑" Enabled="false" Width="0px" Visible="false" OnClick="BtnMod_Click" runat="server" />
                <asp:Button ID="BtnDel" Text="删  除" Enabled="false" Width="0px" Visible="false" OnClick="BtnDel_Click" runat="server" />
                <asp:Button ID="btnpzdel" Text="彻底删除" Width="0px" Visible="false" OnClick="btnpzdel_Click" runat="server" />
                <input id="BtnSearch" type="button" name="BtnSearch" value="查  询" visible="false" OnServerClick="BtnSearch_ServerClick" runat="server" />

                <asp:Button ID="btnExp" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
                <asp:Button ID="btnActiv" Text="激  活" Width="0px" Enabled="false" Visible="false" OnClick="btnActiv_Click" runat="server" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" Visible="false" runat="server" />
                <asp:Button ID="BtnAudit" Text="审  核" Width="0px" Visible="false" OnClick="BtnAudit_Click" runat="server" />
                <asp:Button ID="BtnClose" Text="关  闭" Visible="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div>
                    <asp:DataGrid ID="DgrdList" Width="100%" AutoGenerateColumns="false" PageSize="15" AllowPaging="true" CssClass="grid" DataKeyField="CorpID" OnPageIndexChanged="DgrdList_PageIndexChanged" runat="server"><PagerStyle Mode="NumericPages" CssClass="GD-Pager"></PagerStyle><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                    <%# Container.ItemIndex + 1 + this.DgrdList.PageSize * this.DgrdList.CurrentPageIndex %>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CorpName" HeaderText="单位名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="推荐供应商">
<ItemTemplate>
                                    <%# (Eval("IsHot").ToString().Trim() == "Y") ? "是" : "否" %>
                                </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CorpKind" HeaderText="单位性质"></asp:BoundColumn><asp:BoundColumn DataField="WorkType" HeaderText="经营类别"></asp:BoundColumn><asp:BoundColumn DataField="Speciality" HeaderText="产品概况"></asp:BoundColumn><asp:BoundColumn DataField="LinkMan" HeaderText="联系人"></asp:BoundColumn><asp:BoundColumn DataField="Telephone" HeaderText="联系电话"></asp:BoundColumn><asp:BoundColumn HeaderText="流程状态"></asp:BoundColumn></Columns></asp:DataGrid></div>
            </td>
        </tr>
        <tr id="TrFraFlow" height="30%">
            <td valign="top" colspan="2">
                <iframe id="FraFlow" name="FraFlow" src="" frameborder="no" width="100%" height="100%">
                </iframe>
            </td>
        </tr>
    </table>
    <script>
        if ('<%=this.isAudit %>' != "Audit") {
            document.getElementById("TrFraFlow").style.display = "none";
        }
        else {
            if ('<%=CorpTypeID %>' == "7") {
                document.getElementById("TrFraFlow").style.display = "";
            }
            else {
                document.getElementById("TrFraFlow").style.display = "none";
            }
        }
    </script>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
