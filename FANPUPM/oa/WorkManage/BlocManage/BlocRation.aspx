<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlocRation.aspx.cs" Inherits="oa_WorkManage_BlocManage_BlocRation" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_workmanage_blocmanage_ucdepartmentration_ascx" Src="~/oa/WorkManage/BlocManage/UCDepartmentRation.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_workmanage_blocmanage_ucpersonration_ascx" Src="~/oa/WorkManage/BlocManage/UCPersonRation.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_workmanage_blocmanage_ucsubcompanyration_ascx" Src="~/oa/WorkManage/BlocManage/UCSubCompanyRation.ascx" %>

<html>
<head runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(t,RecordID)
	{
	    
	}

	function checkDecimal(sourObj)
	{
		if (sourObj.value=="")
		{
		   sourObj.value = 0;
		}
		if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
		{
		    alert('数据类型不正确！');
		    sourObj.focus();
		    return;
		}
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
                    <iewc:TabStrip ID="TabStrip" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" TabHoverStyle="background-color:#777777;" TabDefaultStyle="background-image:url(../../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TargetID="MPage" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="个人定额" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="部门定额" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="分子机构定额" runat="server"></iewc:Tab><iewc:TabSeparator DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
				</td>
			</tr>
            <tr>
                <td valign="top" height="100%">
					<iewc:MultiPage ID="MPage" runat="server"><iewc:PageView runat="server">
								<MyUserControl:oa_workmanage_blocmanage_ucpersonration_ascx ID="UCPersonRation" runat="server" />
							</iewc:PageView><iewc:PageView runat="server">
								<MyUserControl:oa_workmanage_blocmanage_ucdepartmentration_ascx ID="UCDepartmentRation" runat="server" />
							</iewc:PageView><iewc:PageView runat="server">
								<MyUserControl:oa_workmanage_blocmanage_ucsubcompanyration_ascx ID="UCSubCompanyRation" runat="server" />
							</iewc:PageView></iewc:MultiPage>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
