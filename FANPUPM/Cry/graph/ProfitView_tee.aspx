<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfitView_tee.aspx.cs" Inherits="Cry_graph_ProfitView" %>
<%@ Register Assembly="TeeChart" Namespace="Steema.TeeChart.Web" TagPrefix="tchart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title></head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" align="center">
                <tr>
                    <td style="width: 962px">
                        工程名称：<asp:DropDownList ID="ddlPrjname" AutoPostBack="true" OnSelectedIndexChanged="ddlPrjname_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 962px" >
                        <tchart:WebChart ID="webchart" AutoPostback="false" GetChartFile="aspx" Height="300px" TempChart="Session" Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFJUZWVDaGFydCwgVmVyc2lvbj0zLjIuMjc2My4yNjA4NCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1mYjk5Yjg0NzRmMDU3M2NlDAMAAABRU3lzdGVtLkRyYXdpbmcsIFZlcnNpb249Mi4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1iMDNmNWY3ZjExZDUwYTNhBQEAAAAVU3RlZW1hLlRlZUNoYXJ0LkNoYXJ0BQAAABUuQXNwZWN0LlNtb290aGluZ01vZGUZLkFzcGVjdC5Db2xvclBhbGV0dGVJbmRleBkuQXNwZWN0LlRleHRSZW5kZXJpbmdIaW50Fi5Bc3BlY3QuQ2hhcnQzRFBlcmNlbnQZLkxlZ2VuZC5UaXRsZS5QZW4uVmlzaWJsZQQABAAAJlN5c3RlbS5EcmF3aW5nLkRyYXdpbmcyRC5TbW9vdGhpbmdNb2RlAwAAAAglU3lzdGVtLkRyYXdpbmcuVGV4dC5UZXh0UmVuZGVyaW5nSGludAMAAAAIAQIAAAAF/P///yZTeXN0ZW0uRHJhd2luZy5EcmF3aW5nMkQuU21vb3RoaW5nTW9kZQEAAAAHdmFsdWVfXwAIAwAAAAAAAAAAAAAABfv///8lU3lzdGVtLkRyYXdpbmcuVGV4dC5UZXh0UmVuZGVyaW5nSGludAEAAAAHdmFsdWVfXwAIAwAAAAAAAAAjAAAAAAs=" Width="800px" BorderStyle="None" EnableViewState="false" runat="server"></tchart:WebChart>
                    </td>
                </tr>
                <tr>
                    <td style="width: 962px">
                        <asp:GridView ID="gv" Width="800px" HeaderStyle-CssClass="grid_head" CssClass="grid" runat="server"></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
