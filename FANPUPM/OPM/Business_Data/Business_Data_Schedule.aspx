<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business_Data_Schedule.aspx.cs" Inherits="Business_Data_Schedule" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>图纸总览</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript">
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtPrjName', idinput: 'hdnPrjID' });
        }
    </script>
</head>
<body class="body_clear">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" height="100%" width="100%">
        <tr>
            <td valign="top" height="30">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            项目
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px">
                                <asp:TextBox ID="txtPrjName" Style="width: 97px; height: 15px; border: none;
                                    line-height: 16px; margin: 1px 0px 1px 2px;" AutoPostBack="true" runat="server"></asp:TextBox>
                                <img src="../../../images/icon.bmp" style="float: right;" alt="选择项目" id="imgPrj"
                                    onclick="openProjPicker();" />
                            </span>
                            <asp:HiddenField ID="hdnPrjID" runat="server" />
                        </td>
                        <td class="descTd" style="display: none;">
                            图纸总览
                        </td>
                        <td style="display: none;">
                            <asp:DropDownList ID="ddlStage" CssClass="txtCss" BackColor="#FEFEF4" Height="20px" Width="120px" DataTextField="PrjStageName" DataValueField="PrjStageID" AutoPostBack="true" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            项目编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            &nbsp;
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" height="95%">
                <div style="height: 100%; overflow: auto;">
                    <asp:GridView CssClass="gvdata" ID="gvStageSchedule" AutoGenerateColumns="true" OnRowDataBound="gvStageSchedule_RowDataBound" OnRowCreated="gvStageSchedule_RowCreated" runat="server">
<EmptyDataTemplate>
                            <table id="emptyContractType" class="gvdata" width="100%" border="0">
                                <tr class="header">
                                    <th scope="col">
                                        项目名称
                                    </th>
                                    <th scope="col">
                                        阶段一
                                    </th>
                                    <th scope="col">
                                        阶段二
                                    </th>
                                    <th scope="col">
                                        阶段三
                                    </th>
                                    <th scope="col">
                                        阶段四
                                    </th>
                                    <th scope="col">
                                        责任人
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="rowa" VerticalAlign="Middle" HorizontalAlign="Center" ForeColor="#008B45"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
    </table>
    <input id="hfldtrorder" type="hidden" runat="server" />

    </form>
</body>
</html>
