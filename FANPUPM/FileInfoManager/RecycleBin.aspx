<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecycleBin.aspx.cs" Inherits="FileInfoManager_RecycleBin" %>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>回收站</title>

    <script type="text/javascript" src="../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>

    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />


    <script type="text/javascript">
        $(document).ready(function() {
        var jwTable = new JustWinTable('gvFile');
        replaceEmptyTable('emptyData', 'gvFile');
            setBtn(jwTable, 'hfldPurchaseChecked');
            showTooltip('tooltip', 25);
        });
        function setBtn(jwTable, hdChkId) {
            if (jwTable.table == undefined)
                return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function() {
                document.getElementById(hdChkId).value = this.id;
                setDisabledButton('');
            });
            jwTable.registClickSingleCHKListener(function() {
                var checkedChk = jwTable.getCheckedChk();
                document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                if (checkedChk.length == 0) {
                    setDisabledButton('disabled');
                }
                else {
                    setDisabledButton('');
                }
            });
            jwTable.registClickAllCHKLitener(function() {
                document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                if (this.checked) {
                    var checkedChks = jwTable.getAllChk();
                    if (checkedChks.length > 0)
                        setDisabledButton('');
                }
                else {
                    setDisabledButton('disabled');
                }
            });
        }
        //设置按钮权限
        function setDisabledButton(disabled) {
            if (disabled != undefined && disabled != '') {
                $('#btnDel').attr('disabled', 'disabled');
                $('#btnRecover').attr('disabled', 'disabled');
            }
            else {
                $('#btnDel').removeAttr('disabled');
                $('#btnRecover').removeAttr('disabled');
            }
        }
    </script>

    <style type="text/css">
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" style="height: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 94%; width: 100%; vertical-align: top;">
                <table style="height: 100%;" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="vertical-align: top; height: 100%;">
                            <table style="width: 100%; height: 100%;">
                                <tr>
                                    <td class="divFooter">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnDel" disabled="disabled" Width="80px" Text="彻底删除" Style="cursor: pointer;" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
                                                    <asp:Button ID="btnRecover" Text="恢复" Style="cursor: pointer;" disabled="disabled" OnClick="btnRecover_Click" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 95%;">
                                        <div style="height: 100%; overflow: auto;">
                                            <asp:GridView CssClass="gvdata" ID="gvFile" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="gvFile_RowDataBound" OnPageIndexChanging="gvFile_PageIndexChanging" DataKeyNames="id,fileName" runat="server">
<EmptyDataTemplate>
                                                    <table id="emptyData" class="gvdata" >
                                                        <tr class="header">
                                                            <th style=" width:20px;">
                                                            <input type="checkbox" />
                                                            </th>
                                                            <th style=" width:25px;">
                                                                序号
                                                            </th>
                                                            <th style=" width:250px;">
                                                                名称
                                                            </th>
                                                            <th >
                                                                资料权限
                                                            </th>
                                                            <th style=" width:80px;">
                                                                创建者
                                                            </th>
                                                            <th style=" width:70px;">
                                                                创建时间
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
                                                            <asp:CheckBox ID="cbAllBox" runat="server" />
                                                        </HeaderTemplate>

<ItemTemplate>
                                                            <asp:CheckBox ID="cbBox" runat="server" />
                                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                            <%# Eval("No") %>
                                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                                            名称
                                                        </HeaderTemplate>

<ItemTemplate>
                                                            <%# GetFileName(Eval("FileName").ToString(), Eval("FileType").ToString(), Eval("FileNewName").ToString()) %>
                                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField><HeaderTemplate>
                                                            资料权限
                                                        </HeaderTemplate><ItemTemplate>
                                                            <asp:TextBox ID="TextBox1" ReadOnly="true" Style="height: 15px; border: solid 0px red;" TextMode="MultiLine" Text='<%# System.Convert.ToString(base.GetUserName(Eval("userCodes").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px"><HeaderTemplate>
                                                            创建者
                                                        </HeaderTemplate><ItemTemplate>
                                                            <%# PageHelper.QueryUser(this, Eval("FileOwner").ToString()) %>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px"><HeaderTemplate>
                                                            创建时间
                                                        </HeaderTemplate><ItemTemplate>
                                                            <%# Eval("CreateTime") %>
                                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                             <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdId" runat="server" />
    <asp:HiddenField ID="hdUserCodes" runat="server" />
    <asp:HiddenField ID="hdType" runat="server" />
    <asp:Button ID="btnRe" Style="display: none;" runat="server" />
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
