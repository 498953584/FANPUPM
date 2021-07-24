<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewBoardroomState.aspx.cs" Inherits="ViewBoardroomState" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>查看会议室状态</title>
    <script language="javascript">
    <!--
        window.name = "win";
    -->
    </script>
</head>
<body class="body_popup" scroll="no">
    <form id="form1" target="win" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <table id="table1" border="1" cellpadding="0" cellspacing="0" width = "720px" height="100%" class="table-form">
            <tr>
                <TD class="td-head"  height="20px">会议室状态查看</TD>
            </tr>
            <tr>
                <td style="height: 24px;" class = "td-search">&nbsp;&nbsp;会议室
                            <asp:DropDownList ID="ddltMeetingRoom" DataSourceID="SqlMeetingRoom" DataTextField="MeetingRoom" DataValueField="RecordID" runat="server"></asp:DropDownList><asp:SqlDataSource ID="SqlMeetingRoom" SelectCommand="SELECT [RecordID], [MeetingRoom] FROM [OA_MeetingRoom_Info] WHERE ([IsValid] = @IsValid)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="y" Name="IsValid" Type="String" /></SelectParameters></asp:SqlDataSource>&nbsp;按
                            <asp:DropDownList ID="ddltYear" DataSourceID="SqlYear" DataTextField="Year" DataValueField="Year" runat="server"></asp:DropDownList><asp:SqlDataSource ID="SqlYear" SelectCommand="select distinct year(UserDate) as Year from OA_MeetingRoom_Apply" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                            年&nbsp;
							<asp:DropDownList ID="ddltMonth" AutoPostBack="false" runat="server"><asp:ListItem Value="1" Selected="true" Text="1" /><asp:ListItem Value="2" Text="2" /><asp:ListItem Value="3" Text="3" /><asp:ListItem Value="4" Text="4" /><asp:ListItem Value="5" Text="5" /><asp:ListItem Value="6" Text="6" /><asp:ListItem Value="7" Text="7" /><asp:ListItem Value="8" Text="8" /><asp:ListItem Value="9" Text="9" /><asp:ListItem Value="10" Text="10" /><asp:ListItem Value="11" Text="11" /><asp:ListItem Value="12" Text="12" /></asp:DropDownList>月&nbsp;
							<asp:Button ID="btnSearch" CssClass="button-normal" OnClick="btnSearch_Click" runat="server" />
							<INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose" class="button-normal">
                    </td>
            </tr>
            <tr>
                <td>
                
                    <table id="table2" border="1" cellpadding="0" cellspacing="0" width="720px" height="40px" class="table-form">
                        <tr height="20px" class="grid_head">
                            <td rowspan="2" align="center" valign="middle" width="56px" class="grid_head">日期</td>
                            <td colspan="11" align="center" class="grid_head" style="height: 21px">
                                时间</td>
                         </tr>
                        <tr height="20px" class="grid_head">
                            <td width="60px" align="center">8</td>
                            <td width="60px" align="center">9</td>
                            <td width="60px" align="center">10</td>
                            <td width="60px" align="center">11</td>
                            <td width="60px" align="center">12</td>
                            <td width="60px" align="center">13</td>
                            <td width="60px" align="center">14</td>
                            <td width="60px" align="center">15</td>
                            <td width="60px" align="center">16</td>
                            <td width="60px" align="center">17</td>
                            <td align="center" style="width: 60px">18</td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr>
                <td>
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                        <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                            <table id="tbState" border="1" cellpadding="0" cellspacing="0" width="100%" class="table-form" runat="server"></table>
                        </div>
                        </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                </td>
            </tr>
        </table>
        &nbsp;
    </form>
</body>
</html>
