<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsDrawTransact.aspx.cs" Inherits="oa_WorkManage_BlocManage_GoodsDrawTransact" %>

<html>
<head runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(t,RecordID)
	{
	    
	}
	-->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" style="table-layout:fixed">
            <tr>
				<td height="5px"></td>
		    </tr>
            <tr>
				<td height="20px">
                    <iewc:TabStrip ID="TabStrip" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:120;height:20;font-size:12px;text-align:center;" TabHoverStyle="background-color:#777777;" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:120;height:20;text-align:center;" TargetID="MPage" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="集团个人领用" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="集团部门领用" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="分子机构个人领用" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="分子机构部门领用" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
				</td>
			</tr>
            <tr>
                <td height="100%"><!--class="mp_frame_top" height="100%" style="padding:5px;"-->
                    <iewc:MultiPage ID="MPage" runat="server"><iewc:PageView runat="server">
								<iframe id="frmBlocPerson" name="frmBlocPerson" frameborder="0" width="100%" height="100%" src="BlocPersonFrame.aspx" runat="server"></iframe>
							</iewc:PageView><iewc:PageView runat="server">
								<iframe id="frmBlocDept" name="frmBlocDept" frameborder="0" width="100%" height="100%" src="BlocDepartmentFrame.aspx" runat="server"></iframe>
							</iewc:PageView><iewc:PageView runat="server">
								<iframe id="frmSubPerson" name="frmSubPerson" frameborder="0" width="100%" height="100%" src="SubCompanyPersonFrame.aspx" runat="server"></iframe>
							</iewc:PageView><iewc:PageView runat="server">
								<iframe id="frmSubDept" name="frmSubDept" frameborder="0" width="100%" height="100%" src="SubCompanyDepartmentFrame.aspx" runat="server"></iframe>
							</iewc:PageView></iewc:MultiPage>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
