<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonalInfo.aspx.cs" Inherits="oa_Buffet_PersonalInfo" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>个人信息查看</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        .style1
        {
            height: 12px;
        }
        .style2
        {
            width: 128px;
            height: 12px;
        }
    </style>
</head>
<body  class="body_popup" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table class="table-form" id="Table1" cellspacing="0" cellpadding="0" width="100%" border="1">
                <tr>
	                <td class="td-title" colspan="5" height="40">
                        个人信息查看</td>
                </tr>
                <tr>
                  <td class="td-label" width="11%" style="height:47px;">姓&nbsp;&nbsp;&nbsp;&nbsp;名:</td>
	                <td width="30%" ><asp:Label ID="LbName" runat="server"></asp:Label></td>	                
	                <td class="td-label" width="11%">所属部门:</td>
	                <td  width="30%">
                        <asp:Label ID="LbDeptName" runat="server"></asp:Label>                       
                    </td>
	                <td rowspan="3" align="center" valign="middle" style=" text-align:center; margin:0px auto; padding:0px;">
                        <asp:Image ID="Image1" Width="112px" Height="140px" ImageAlign="Middle" GenerateEmptyAlternateText="true" AlternateText="照片" ImageUrl="~/images/PhotoDefault.gif" runat="server" />
                     </td>
                </tr>
                <tr>
	                <td class="td-label"width="10%" style="height:47px;" >民&nbsp;&nbsp;&nbsp;&nbsp;族:</td>
	                <td class="style1">
                        <asp:Label ID="LbNation" runat="server"></asp:Label>
                    </td>
	                <td class="td-label" width="10%" >性&nbsp;&nbsp;&nbsp;&nbsp;别:</td>
	                <td class="style1">
                        <asp:Label ID="LbSex" runat="server"></asp:Label>
                        </td>
	              
                </tr>
                <tr>
	               
                    <td class="td-label" width="10%" style="height:47px;">出生日期:</td>
	                <td width="10%">
	                <asp:Label ID="LbBirthday" runat="server"></asp:Label>	            
                        </td>
                         <td class="td-label" width="10%">
                         身份证号:</td>
	            <td >
	                <asp:Label ID="LbIDCard" TabIndex="21" runat="server"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
	            <td class="td-label" width="10%" style="height:47px;">
                    手机号:</td>
	            <td  width="10%" style=" height:32px;">
                    <asp:Label ID="LbMobilePhoneCode" MaxLength="13" TabIndex="20" Width="62%" runat="server"></asp:Label>
                     </td>
	           
                     <td class="td-label" width="10%">
                    职称与资格:</td>
	            <td  colspan="2">
                    <asp:Label ID="LbPostAndCompetency" MaxLength="20" TabIndex="20" Width="90%" runat="server"></asp:Label>
                    </td>
        
            </tr>
          
            <tr>
                <td colspan="5"><b>使用须知：</b><br /><br />
                    注：1、员工个人信息正在完善整理中，如发现数据项错误请联系总部人力资源中心；&nbsp;<br /><br />
                  &nbsp; &nbsp; 2、手机作为系统信息的接收渠道，必须保证号码正确。如有误或更改请联系总部人力资源中心。</td>
            </tr>
         <tr>
             <td colspan="5">
             </td>
         </tr>
               
            </table>
    </div>
    </form>
</body>
</html>
