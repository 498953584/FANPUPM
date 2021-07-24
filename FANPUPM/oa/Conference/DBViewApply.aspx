<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBViewApply.aspx.cs" Inherits="oa_Conference_DBViewApply" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>会议室申请</title></head>
<body>
    <form id="form1" runat="server">
    <div>
     <table class="table-form" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="1">
		            <tr>
			            <td class="td-head" colspan="4" height="20">
                            会议室申请被退回</td>
		            </tr>
		            <tr>
			            <td class="td-label" width="15%">
                            会议室名称</td>
			            <td width="35%"><asp:Label ID="LbMeetingRoom" CssClass="text-input" TabIndex="1" MaxLength="100" runat="server"></asp:Label>
                            <font color="#ff0000">&nbsp;</font> 
			            </td>
			            <td class="td-label" width="15%">参与人数</td>
			            <td><asp:Label ID="LbHumanNumber" CssClass="text-input" TabIndex="2" MaxLength="4" runat="server"></asp:Label>&nbsp;
                        </td>
		            </tr>
		            <tr>
		                <td class="td-label" width="15%">主题</td>
		                <td width="35%"><asp:Label ID="LbTitle" runat="server"></asp:Label></td>
			            <td class="td-label" width="15%">召开日期</td>
			            <td>
                            <asp:Label ID="LbUserDate" runat="server"></asp:Label></td>
		            </tr> 
		            <tr>
			            <td class="td-label" width="15%" style="height: 25px">开始时间</td>
			            <td width="35%" style="height: 25px">
                            <asp:Label ID="LBBeginDate" runat="server"></asp:Label></td>
			            <td class="td-label" width="15%" style="height: 25px">结束时间</td>
			            <td style="height: 25px">
                            <asp:Label ID="LbEndDate" runat="server"></asp:Label></td>
		            </tr>
		              <tr>
			            <td class="td-label" width="15%" style="height: 25px">申请人</td>
			            <td width="35%" style="height: 25px" colspan="3"><asp:Label ID="LbUserName" runat="server"></asp:Label>
                            <font color="#ff0000">&nbsp;</font> 
			            </td>
			           
		            </tr>
		            <tr>
			            <td class="td-label" width="15%" >备注</td>
			            <td colspan="3"><asp:Label ID="LbContent" Width="80%" TextMode="MultiLine" runat="server"></asp:Label><font color="#ff0000">&nbsp;</font> </td>
		            </tr>
		       <tr>
					<td  align="center" colspan="4" class="td-submit"><input id="BtnClose" onclick="JavaScript:history.go(-1);" type="button" value="返  回"/></td>
				</tr>
	            </table>
    </div>
    </form>
</body>
</html>
