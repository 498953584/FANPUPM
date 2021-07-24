<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExplainPlan.aspx.cs" Inherits="oa_WorkPlanAndSummary_ExplainPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
        <style type="text/css">
        .divFooter2
        {
	        height: 24px;
	        text-align: right;
	        background: url(/images1/divBottomBack.jpg) repeat-x;
	        vertical-align: middle;
	        position: absolute;
	        bottom: 0px;
	        margin-bottom:0px;
	        width: 100%;
        }
        .tableFooter2
        {
	        width: 100%;
	        text-align: right;
	        bottom: 0px;
        }
        .txt
        {
	        width: 40%;
	        text-align: left;
	        border:1px solid black
        }
        .word
        {
        	width:10%;
        	text-align:right;
        	border:1px solid black
        }
        .sum
        {
        	border-bottom:1px solid black;
        	border-right:1px solid black;
        	}
        .sumright
        {
        	border-right:1px solid black;
        	}
        .toptable
        {
        	border-top:0px solid black;
        	border-left:1px solid black;
        	border-right:1px solid black;
        	border-bottom:1px solid black;
        }
        .plantoplbl
        {
        	width: 40%;
	        text-align: left;
	        border-top:0px solid black;
        	border-left:1px solid black;
        	border-right:1px solid black;
        	border-bottom:1px solid black;
        }
        .plantoptd
        {
        	width: 10%;
	        text-align: right;
	        border-top:0px solid black;
        	border-left:1px solid black;
        	border-right:1px solid black;
        	border-bottom:1px solid black;
        }
    </style>
    <script type="text/javascript" src="../../StockManage/script/Config.js"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div style="height:95%; overflow:auto;width:100%">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="divHeader">
                        工作计划说明与总结
                    </td>
                </tr>
            </table>
        </div>
        <div class="divContent2" align="center">
            <table cellpadding="5px" cellspacing="0" border="1px solid black" width="80%" >
            <tr>
                <td class="word">
                计划编号
                </td>
                <td class="txt">
                    <asp:Label ID="lblCode" ReadOnly="true" Height="15px" runat="server"></asp:Label>
                </td>
                <td class="word">
                填报日期
                </td>
                <td class="txt">
                   <asp:Label ID="lblDate" Height="15px" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="word">
                填报人
                </td>
                <td class="txt">
                    <asp:Label ID="lblReportName" Height="15px" Enabled="true" runat="server"></asp:Label>
                </td>
                <td class="word">
                    部门名称
                </td>
                <td class="txt">
                    <asp:Label ID="lblPart" Height="15px" TextMode="MultiLine" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="word">
                    查看人
                </td>
                <td class="txt" colspan="3">
                    <asp:Label ID="lblChecker" Height="15px" TextMode="MultiLine" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="word">
                    计划说明
                </td>
                <td class="txt" colspan="3">
                    <asp:Label ID="lblPlanSumm" Height="15px" TextMode="MultiLine" runat="server"></asp:Label>
                </td>
            </tr>
         </table>
         <asp:DataList ID="dlData" Width="80%" CellPadding="0" CellSpacing="0" BorderStyle="None" OnItemDataBound="dlData_ItemDataBound" runat="server">
<ItemTemplate>
                  <table border="0px solid #CADEED" width="100%" cellpadding="5" cellspacing="0" style="border-style:none;">
                    <tr>
                        <td class="plantoptd">开始时间</td>
                        <td class="plantoplbl">
                            <asp:Label runat="server"><%# Convert.ToString(DateTime.Parse(Eval("wkpstarttime").ToString()).ToShortDateString()) %></asp:Label>
                        </td>
                        <td class="plantoptd">结束时间</td>
                        <td class="plantoplbl">
                            <asp:Label runat="server"><%# Convert.ToString(DateTime.Parse(Eval("wkpendtime").ToString()).ToShortDateString()) %></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="word">负 责 人</td>
                        <td class="txt"><asp:Label runat="server"><%# Convert.ToString(Eval("wkpchief")) %></asp:Label>
                        </td>
                        <td class="word">责 任 人</td>
                        <td class="txt"><asp:Label runat="server"><%# Convert.ToString(Eval("wkppersons")) %></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="word">计划内容</td>
                        <td colspan="3" class="txt" > <asp:Label runat="server"><%# Convert.ToString(Eval("wkpcontents")) %></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border:1px solid black;padding:0px;">
                            <asp:DataList ID="dlSumm" Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0px" runat="server">
<ItemTemplate>
                                <table border="0px solid black" width="100%" cellpadding="5" cellspacing="0">
                                    <tr>
                                        <td style="width:10%;" class="sum" align="right">
                                        总结时间
                                        </td>
                                        <td style="width:40%;" class="sum" align="left">
                                        <asp:Label ID="Label3" runat="server"><%# Convert.ToString(DateTime.Parse(Eval("wkprecorddate").ToString()).ToShortDateString()) %></asp:Label>
                                        </td>
                                        <td style="width:10%;" class="sum" align="right">
                                        完成比例
                                        </td>
                                        <td style="width:40%;border-right:0px solid black" class="sum" align="left">
                                        <asp:Label ID="Label1" runat="server"><%# Convert.ToString(Eval("wkppercent")) %></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%;" class="sumright"  align="right">
                                        计划总结
                                        </td >
                                        <td style="width:90%;"  align="left" colspan="3">
                                        <asp:Label ID="Label2" runat="server"><%# Convert.ToString(Eval("wkpsmcontents")) %></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
</asp:DataList>
                        </td>
                    </tr>
                  </table>
                </ItemTemplate>
</asp:DataList>
        </div>
        <div align="center">
        <table width="80%" cellpadding="5" cellspacing="0" style=" border-style:none;">
            <tr>
                <td style="width:10%;text-align:right;" class="toptable">
                自 评 分
                </td>
                <td style="width:90%;text-align:left;" class="toptable">
                    <asp:Label ID="lblScore" runat="server"></asp:Label>
                </td>                
            </tr>
            <tr>
                <td style="width:10%;text-align:right;border:1px solid black">
                总结说明
                </td>
                <td style="width:90%;text-align:left;border:1px solid black">
                    <asp:Label ID="lblSumm" runat="server"></asp:Label>
                </td>    
            </tr>
        </table>
        </div>
     </div>
        <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input id="btnSave" type="button" onclick="this.style.display='none';document.getElementById('btnCancel').style.display='none';winPrint()" value="打印" style=" display:none;" />
                    <input type="button" id="btnCancel" value="取消" onclick="parent.desktop['ExplainPlan'] = null;top.frameWorkArea.window.desktop.getActive().close();"/>
                </td>
            </tr>
        </table>
    </div>
</form>
</body>
</html>
