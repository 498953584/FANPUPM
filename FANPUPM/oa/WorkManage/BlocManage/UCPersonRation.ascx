<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCPersonRation.ascx.cs" Inherits="oa_WorkManage_BlocManage_UCPersonRation" %>

<table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablea" class="table-none" style="table-layout:auto">
								<tr>
                                    <td height="20px" class="td-title">
                                        个人定额设置
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20px" class="td-toolsbar">
                                        <asp:Button ID="btnSave" Text="保存设置" OnClick="btnSave_Click" runat="server" />
                                        <input id="HdnRecordID" style="width: 20px" type="hidden" runat="server" />

                                    </td>
                                </tr>
                                <tr>
                <td valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVManager" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="1px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVManager_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        级别</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        职衔</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        定额</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="PostLevel" HeaderText="级别" HtmlEncode="false" /><asp:BoundField DataField="PostAndRank" HeaderText="职衔" HtmlEncode="false" /><asp:TemplateField HeaderText="定额">
<ItemTemplate>
                                    <input type="text" id="txtRation" class="text-input" style="width:100%;" value='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Ration")) %>' runat="server" />

                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Remark" HeaderText="备注" HtmlEncode="false" /><asp:BoundField DataField="RecordID" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [v_OA_OfficeRes_RationSetAndPositionLevel]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
								</table>
