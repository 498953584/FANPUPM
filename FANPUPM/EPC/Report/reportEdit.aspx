<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reportEdit.aspx.cs" Inherits="EPC_ConstructSchedule_project_reportEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title></head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;选择：\r
        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" DataSourceID="Sql_list" DataTextField="Title" DataValueField="ReportID" Width="112px" runat="server"></asp:DropDownList>
        <asp:FormView ID="FormView1" DataSourceID="Sql_repmain" Height="512px" Width="608px" runat="server"><EditItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 111px; height: 21px">
                            报表ID:</td>
                                    <td style="width: 139px; height: 21px">
                            <asp:TextBox ID="TexReportID" Text='<%# Convert.ToString(Eval("[ReportID]")) %>' runat="server"></asp:TextBox></td>
                                    <td style="width: 110px; height: 21px">
                            设计者:
                                    </td>
                                    <td style="width: 141px; height: 21px">
                            <asp:TextBox ID="Tex设计者" Text='<%# Convert.ToString(Eval("[设计者]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            原sql语句:
                                    </td>
                                    <td style="width: 139px">
                                    </td>
                                    <td style="width: 110px">
                                    </td>
                                    <td style="width: 141px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                            <asp:TextBox ID="Texoldsql" Height="125px" Width="574px" TextMode="MultiLine" Text='<%# Convert.ToString(Eval("[原Sql语句]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            sql语句:
                                    </td>
                                    <td style="width: 139px">
                                    </td>
                                    <td style="width: 110px">
                                    </td>
                                    <td style="width: 141px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                            <asp:TextBox ID="Texsql" Height="159px" Width="577px" TextMode="MultiLine" Text='<%# Convert.ToString(Eval("[SelectSql]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            标题:</td>
                                    <td colspan="3">
                            <asp:TextBox ID="TexTitle" Width="473px" Text='<%# Convert.ToString(Eval("[Title]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            页脚:
                                    </td>
                                    <td colspan="3">
                            <asp:TextBox ID="TexFooter" Width="469px" Text='<%# Convert.ToString(Eval("[Footer]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px; height: 21px">
                            页头:</td>
                                    <td colspan="3" style="height: 21px">
                            <asp:TextBox ID="TexHeader" Width="468px" Text='<%# Convert.ToString(Eval("[Header]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            排序语句:
                                    </td>
                                    <td style="width: 139px">
                            <asp:TextBox ID="TexSortBy" Text='<%# Convert.ToString(Eval("[SortBy]")) %>' runat="server"></asp:TextBox></td>
                                    <td style="width: 110px">
                            页宽度:</td>
                                    <td style="width: 141px">
                            <asp:TextBox ID="TexHeadWidth" Text='<%# Convert.ToString(Eval("[HeadWidth]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px; height: 21px">
                                    </td>
                                    <td style="width: 139px; height: 21px">
                                    </td>
                                    <td style="width: 110px; height: 21px">
                                    </td>
                                    <td style="width: 141px; height: 21px">
                                    </td>
                                </tr>
                            </table>
                          
                            <br />
                            <asp:LinkButton ID="UpdateButton" CausesValidation="true" CommandName="Update" Text="更新" runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="UpdateCancelButton" CausesValidation="false" CommandName="Cancel" Text="取消" runat="server"></asp:LinkButton>
                        </EditItemTemplate><ItemTemplate>
                            <table>
                                <tr>
                                    <td style="width: 111px; height: 21px">
                            报表ID:</td>
                                    <td style="width: 139px; height: 21px">
                            <asp:TextBox ID="TexReportID" Text='<%# Convert.ToString(Eval("[ReportID]")) %>' runat="server"></asp:TextBox></td>
                                    <td style="width: 110px; height: 21px">
                            设计者:
                                    </td>
                                    <td style="width: 141px; height: 21px">
                            <asp:TextBox ID="Tex设计者" Text='<%# Convert.ToString(Eval("[设计者]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            原sql语句:
                                    </td>
                                    <td style="width: 139px">
                                    </td>
                                    <td style="width: 110px">
                                    </td>
                                    <td style="width: 141px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                            <asp:TextBox ID="Texoldsql" Height="125px" Width="566px" TextMode="MultiLine" Text='<%# Convert.ToString(Eval("[原Sql语句]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            sql语句:
                                    </td>
                                    <td style="width: 139px">
                                    </td>
                                    <td style="width: 110px">
                                    </td>
                                    <td style="width: 141px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                            <asp:TextBox ID="Texsql" Height="159px" Width="571px" TextMode="MultiLine" Text='<%# Convert.ToString(Eval("[SelectSql]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            标题:</td>
                                    <td colspan="3">
                            <asp:TextBox ID="TexTitle" Width="473px" Text='<%# Convert.ToString(Eval("[Title]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            页脚:
                                    </td>
                                    <td colspan="3">
                            <asp:TextBox ID="TexFooter" Width="469px" Text='<%# Convert.ToString(Eval("[Footer]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px; height: 21px">
                            页头:</td>
                                    <td colspan="3" style="height: 21px">
                            <asp:TextBox ID="TexHeader" Width="468px" Text='<%# Convert.ToString(Eval("[Header]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px">
                            排序语句:
                                    </td>
                                    <td style="width: 139px">
                            <asp:TextBox ID="TexSortBy" Text='<%# Convert.ToString(Eval("[SortBy]")) %>' runat="server"></asp:TextBox></td>
                                    <td style="width: 110px">
                            页宽度:</td>
                                    <td style="width: 141px">
                            <asp:TextBox ID="TexHeadWidth" Width="178px" Text='<%# Convert.ToString(Eval("[HeadWidth]")) %>' runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 111px; height: 21px">
                                    </td>
                                    <td style="width: 139px; height: 21px">
                                    </td>
                                    <td style="width: 110px; height: 21px">
                                    </td>
                                    <td style="width: 141px; height: 21px">
                                    </td>
                                </tr>
                            </table>
                            <asp:LinkButton ID="EditButton" CausesValidation="false" CommandName="Edit" Text="编辑" runat="server"></asp:LinkButton>
                        </ItemTemplate></asp:FormView>
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
        <asp:SqlDataSource ID="Sql_repmain" SelectCommand="SELECT [ReportID],[设计者],[原Sql语句] ,[SelectSql] ,[Title] ,[Footer] ,[Header] ,[SortBy] ,[HeadWidth] ,[Remark]   FROM [Rep_Main]  where ReportID = @ReportID " CacheExpirationPolicy="Sliding" UpdateCommand="update  set [原Sql语句]=@Texoldsql   where ReportID = @TexReportID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="DropDownList1" Name="ReportID" PropertyName="SelectedValue" runat="server" /></SelectParameters></asp:SqlDataSource>
        <br />
        <asp:SqlDataSource ID="Sql_list" CacheExpirationPolicy="Sliding" SelectCommand="SELECT [ReportID],[设计者],[原Sql语句] ,[SelectSql] ,[Title] ,[Footer] ,[Header] ,[SortBy] ,[HeadWidth] ,[Remark]   FROM [Rep_Main]  " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
        &nbsp;&nbsp;
    </div>
    </form>
</body>
</html>
