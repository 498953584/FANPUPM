<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditStatus.aspx.cs" Inherits="AuditStatus" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看流程状态</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script language="javascript" type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
	function setHeight()
	{
	   height=document.getElementById("tdflowstate").clientHeight;
	   document.getElementById("divstate").style.height=height;
	 // alert(height);
	  
	}
        addEvent(window,'load',function(){
            var jwTable = new JustWinTable('dgFlow');      
            setHeight();   
        });
    </script>
    <style type="text/css">
        .gvdata1
        {
            width: 100%; /*table-layout: fixed;*/
            border-collapse: separate;
            border-spacing: 0px 0px;
            border-width: 0px;
            border-bottom: solid 1px #CADEED;
            border-top: solid 1px #CADEED;
            border-left: solid 1px #CBDEED;
            border-right: solid 1px #CBDEED;
        }
        table.gvdata1 th
        {
            overflow: hidden;
            font-weight: normal;
            /*text-align: center;*/
            border-color: #CADEED;
            color: #727FAA;
            padding-left: 6px;
            padding-right: 6px;
        }
        table.gvdata1 td
        {
            /*overflow: hidden;*/
            vertical-align:top;
            padding-left: 6px;
            padding-right: 6px;
            font-weight: normal;
            border-color: #CADEED;
        }
        table.gvdata1 tr
        {
            height: 22px;
        }
        .header
        {
            background-color: #EEF2F5;
        }
        .rowa
        {
            background-color: #fbfbfb;
        }
        .rowb
        {
            background-color: #ffffff;
        }
        .footer
        {
        }
       
    </style>
</head>
<body>
    <form id="Form1" scroll="no" runat="server">
    <table width="100%" style="height: 95%;">
        <tr>
            <td class="divHeader" align="center" height="22px">
                流程状态查看
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" id="tdflowstate">
                <div style="overflow: auto; width: 100%;" id="divstate">
                    <asp:DataGrid ID="dgFlow" CssClass="gvdata1" AutoGenerateColumns="false" Width="100%" OnItemDataBound="dgFlow_ItemDataBound" OnPageIndexChanged="dgFlow_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="header"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
<ItemTemplate>
                                    
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="节点名称" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
<ItemTemplate>
                                    <asp:Label ID="TxtNodeName" Text='<%# System.Convert.ToString(Eval("NodeName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="审核人" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
<ItemTemplate>
                                    <asp:Label ID="TxtOperator" Text='<%# System.Convert.ToString(Eval("auditor"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="审核时间" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
<ItemTemplate>
                                    <asp:Label ID="TxtAuditDate" Text='<%# System.Convert.ToString(Eval("AuditDate"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="审核结果" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
<ItemTemplate>
                                    <asp:Label ID="TxtAuditResult" Text='<%# System.Convert.ToString(Eval("Result"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="审核，审核意见" ItemStyle-Width="200px" ItemStyle-Wrap="true"><ItemTemplate>
                                <div style="width: 95%; word-break: break-all;">
                                 
                                 </div>   
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="状态" ItemStyle-Wrap="false"><ItemTemplate>
                                    <asp:Label ID="TxtSing" Text='<%# System.Convert.ToString(Eval("Status"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn></Columns><FooterStyle CssClass="footer"></FooterStyle><PagerStyle Mode="NumericPages"></PagerStyle></asp:DataGrid>
                </div>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
