<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowView.aspx.cs" Inherits="StockManage_basicset_ShowView" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>预警提醒</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript">
        function winClose() {
            winclose('ShowView', null, false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div>
    <table cellspacing="0" cellpadding="0" width="100%" border="1" >
				<tr>
					<td style="background-color:Silver; height:30px; font-weight:bold; font-size:14px;" align="center"  colspan="2">预 警 提 醒
					</td>
				</tr>
				<tr>
					<td style="width:100px; text-align:right; height:30px; background-color:#E4ECF1;">
                        添加日期:</td>
					<td >
						<asp:Label ID="lbRecordDate" runat="server"></asp:Label></td>
				</tr>
			
				<tr>
					<td style="width:100px; text-align:right; vertical-align:top; background-color:#E4ECF1;">详细内容:</td>
					<td style="height:200px; vertical-align:top;" ><asp:Label ID="lbContent" runat="server"></asp:Label></td>
				</tr>	
				<tr >
					<td  align="center" colspan="2" class="td-submit"><input id="BtnClose" onclick="winClose();" type="button" value="关 闭"/></td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
