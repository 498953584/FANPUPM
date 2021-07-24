<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcTechnology.ascx.cs" Inherits="HR_Organization_UcTechnology" %>

<table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" id="table2"
    class="table-none" style="table-layout: auto">
    <tr>
        <td height="20px" class="td-title">
            岗位证书类职级列表
        </td>
    </tr>
    <tr>
        <td height="20px" class="td-toolsbar">
            <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
            <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
            <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
            <input id="HdnRecordID" style="width: 20px" type="hidden" runat="server" />

        </td>
    </tr>
    <tr>
        <td height="100%">
            <div style="overflow: auto; width: 100%; height: 100%">
                <asp:GridView CssClass="grid" ID="GVTechnology" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVTechnology_RowDataBound" runat="server">
<EmptyDataTemplate>
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                            style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                            width: 100%; border-collapse: collapse; border-right-width: 0px">
                            <tr align="center" class="grid_head">
                                <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                    序号
                                </th>
                                <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                    技术类岗级
                                </th>
                                <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                    技术类职级
                                </th>
                                <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                    职衔
                                </th>
                                <th align="center" nowrap="nowrap" scope="col">
                                    备注
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="PostLevel" HeaderText="技术类岗级" HtmlEncode="false" /><asp:BoundField DataField="PositionLevel" HeaderText="技术类职级" HtmlEncode="false" /><asp:BoundField DataField="PostAndRank" HeaderText="职衔" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT RecordID,Type,(Convert(varchar(20),PostLevel)+'级') as PostLevel,(Convert(varchar(20),PositionLevel)+'级') as PositionLevel,PostAndRank,Remark FROM [HR_Org_PostLevel] WHERE ([Type] = @Type)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="2" Name="Type" Type="Int32" /></SelectParameters></asp:SqlDataSource>
            </div>
</table>
