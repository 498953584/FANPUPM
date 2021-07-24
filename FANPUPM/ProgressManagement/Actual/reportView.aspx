<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reportView.aspx.cs" Inherits="ProgressManagement_Actual_reportView" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>实际进度上报信息查看 </title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var gvTask = new JustWinTable('gvwReportDetails');
            replaceEmptyTable('tblEmpty', 'gvwReportDetails');
        });
    </script>
    <style type="text/css">
        .tbl
        {
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="print" style="width: 100%; margin: 0 auto;">
        <table class="tab" style="vertical-align: top; border-collapse: collapse;">
            <tr>
                
                <td class="divHeader">
                    实际进度上报查看
                    <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                        <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                    </div>
                </td>
            </tr>
            <tr style="height: 1px;">
                <td>
                    <table id="Table1" cellpadding="0" cellspacing="0" style="border-style: none;" class="viewTable">
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
            <tr>
                <td>
                    <table id="reportInfo" class="viewTable" style="margin-bottom: 4px;">
                        <tr>
                            <td width="80">
                                上报日期
                            </td>
                            <td>
                                <asp:Label ID="lblInputDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                记录人
                            </td>
                            <td>
                                <asp:Label ID="lblInputUser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                附件
                            </td>
                            <td>
                                <asp:Label ID="lblAdjunct" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                备注
                            </td>
                            <td>
                                <asp:Label ID="lblNote" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwReportDetails" AutoGenerateColumns="false" CssClass="viewTable" runat="server">
<EmptyDataTemplate>
                            <table id="tblEmpty">
                                <tr class="header">
                                    <th rowspan="2" scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th rowspan="2" scope="col">
                                        任务名称
                                    </th>
                                    <th rowspan="2" scope="col">
                                        工期
                                    </th>
                                    <th scope="col">
                                        计划开始日期
                                    </th>
                                    <th scope="col">
                                        计划结束日期
                                    </th>
                                    <th scope="col">
                                        实际开始日期
                                    </th>
                                    <th scope="col">
                                        实际结束日期
                                    </th>
                                    <th rowspan="2" scope="col" colspan="2">
                                        完成百分比
                                    </th>
                                    <th rowspan="2" scope="col">
                                        说明
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务名称">
<ItemTemplate>
                                    <%# StringUtility.ReplaceTxt(Eval("TaskName").ToString()) %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工期">
<ItemTemplate>
                                    <%# Eval("DURATION_") %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="计划开始日期" HeaderStyle-Width="80px" DataField="START_" DataFormatString="{0:yyy-M-d}" /><asp:BoundField HeaderText="计划结束日期" HeaderStyle-Width="80px" DataField="FINISH_" DataFormatString="{0:yyy-M-d}" /><asp:BoundField HeaderText="实际开始日期" HeaderStyle-Width="80px" DataField="Start" DataFormatString="{0:yyy-M-d}" /><asp:BoundField HeaderText="实际结束日期" HeaderStyle-Width="80px" DataField="Finish" DataFormatString="{0:yyy-M-d}" /><asp:TemplateField HeaderText="完成百分比">
<ItemTemplate>
                                    <%# Eval("Completed").ToString() + "%" %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="说明">
<ItemTemplate>
                                    <%# StringUtility.ReplaceTxt(Eval("Note").ToString()) %>
                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
            <tr id="trAudit" style="vertical-align: top;">
                <td>
                    <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="111" BusiClass="001" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
