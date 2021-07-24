<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryAdjustFrame.aspx.cs" Inherits="HR_Salary_SalaryAdjustFrame" %>
<%@ Import Namespace="Microsoft.Web.UI.WebControls"%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>工资调整申请</title></head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">                    
                        工资调整申请
                </td>
            </tr>
            <tr><td style="height: 30px">
        <iewc:TabStrip ID="TabStrip1" TargetID="MultiPage1" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:160;height:20;text-align:center;" TabHoverStyle="background-color:#777777;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:160;height:20;font-size:12px;text-align:center;" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabIndex="-1" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="单个工资调整申请" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="成批工资调整申请" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
        </td>
    </tr>
     <tr>
        <td valign="top">
            <iewc:MultiPage ID="MultiPage1" runat="server"><iewc:PageView runat="server">
   
     <table id="table4" border="0" cellpadding="0" cellspacing="0" class="table-form" width="100%" height="100%">
                     
              <tr>
                <td>
                 <iframe id="frmPage" name="frmPage" src="SalaryAdjustMain.aspx" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
               </tr>
       </table>
       
                </iewc:PageView><iewc:PageView runat="server">
                   <table id="table2" border="0" cellpadding="0" cellspacing="0" class="table-form" width="100%"  height="100%">
               
                    <tr>
                        <td>
                           <iframe id="Iframe1" name="frmPage" src="SalaryGroupAdjustList.aspx" frameborder="0" width="100%" height="100%" runat="server"></iframe>                       
                        </td>
                    </tr>
                    </table>  
                </iewc:PageView></iewc:MultiPage>
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
