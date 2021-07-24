<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjCompletedQuery.aspx.cs" Inherits="PrjManager_Completed_PrjCompletedQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目竣工信息查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; border-collapse: collapse;">
        <tr>
            <td class="divHeader">
                <asp:Label ID="lblPrjName" CssClass="divHeader" runat="server"></asp:Label>
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px">
            <td style="vertical-align: top;">
                <asp:GridView ID="gvComplete" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
                        <table id="emptyTable">
                            <tr class="header">
                                <th scope="col" style="width: 20px;">
                                </th>
                                <th scope="col">
                                    序号
                                </th>
                                <th scope="col">
                                    准备项目
                                </th>
                                <th scope="col">
                                    准备情况
                                </th>
                                <th scope="col">
                                    未完成事项
                                </th>
                                <th scope="col">
                                    整改及完善措施
                                </th>
                                <th scope="col">
                                    附件
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Name" HeaderText="准备项目" ItemStyle-Width="110px" ItemStyle-Wrap="false" /><asp:TemplateField HeaderText="准备情况" ItemStyle-Width="200px" ItemStyle-Wrap="false"><ItemTemplate>
                                <div style="width: 95%; word-break: break-all;">
                                    <%# StringUtility.ReplaceTxt(Eval("PrepareStatus").ToString()) %>
                                </div>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="未完成情况" ItemStyle-Width="200px" ItemStyle-Wrap="false"><ItemTemplate>
                                <div style="width: 95%; word-break: break-all;">
                                    <%# StringUtility.ReplaceTxt(Eval("UncompletedTrans").ToString()) %>
                                </div>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="整改及完善措施" ItemStyle-Width="200px" ItemStyle-Wrap="false"><ItemTemplate>
                                <div style="width: 95%; word-break: break-all;">
                                    <%# StringUtility.ReplaceTxt(Eval("Rectification").ToString()) %>
                                </div>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-Width="50px" ItemStyle-Wrap="false"><ItemTemplate>
                                <%# FilesBind(Eval("ID").ToString()) %>
                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            </td>
        </tr>
        <tr id="trCompleted" visible="false" style="vertical-align: top; height: 1px;" runat="server"><td style="vertical-align: top;" runat="server">
                <div class="groupInfo" style="text-align: center; margin-bottom: 5px; margin-top: 5px;">
                    项目竣工信息
                </div>
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            竣工日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCompletedDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            竣工说明
                        </td>
                        <td class="elemTd" id="td_completedNote" runat="server">
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="106" BusiClass="001" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
