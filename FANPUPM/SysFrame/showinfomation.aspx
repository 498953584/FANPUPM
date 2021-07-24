<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showinfomation.aspx.cs" Inherits="ShowInfomation" %>
<%@ Register TagPrefix="MyUserControl" TagName="sysframe_webswf_ascx" Src="~/SysFrame/WebSwf.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>首页面</title>
    <base target="_self" />
    <style type="text/css">
    body {
            margin:0px;
            font-size:9pt;
           }
    .txt {
	    font-size: 9pt;
	    line-height: 220%;
	    color: #666666;
	    text-decoration: none;
	    text-indent: 18pt;
    }
    .pt9 {
	    font-size: 9pt;
	    line-height: 200%;
	    color: #999999;
	    text-decoration: none;
    }
   
	.right_table_style { BORDER-COLLAPSE: collapse }
	.right_table_sub_td_style { BORDER-RIGHT: #d7dadd 1px solid; PADDING-RIGHT: 0px; BORDER-TOP: #d7dadd 1px solid; PADDING-LEFT: 0px; FONT-SIZE: 9pt; PADDING-BOTTOM: 0px; BORDER-LEFT: #d7dadd 1px solid; COLOR: #000000; PADDING-TOP: 0px; BORDER-BOTTOM: #d7dadd 1px solid; FONT-FAMILY: "Courier New", Courier, mono; BORDER-COLLAPSE: collapse; HEIGHT: 27px }
	.page_text { FONT-SIZE: 9pt; COLOR: #666666; LINE-HEIGHT: 180%; TEXT-DECORATION: none }

	</style>
    <script type="text/javascript" src="../Script/jquery.js"></script>
	<script type='text/javascript' language='javascript' src='jscript/JsControlMenuTool.js'></script>
	<link rel="stylesheet" href="../StyleCss/PM1.css" type="text/css" />


    <script language="javascript" src="../../../Web_Client/Tree.js" type="text/javascript"></script>

 

</head>
<body style="font-size: 9pt" >
    <form id="Form1" method="post" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="10">&nbsp;</td>
  </tr>
</table>

        <table cellspacing="0" cellpadding="0" width="770px" align="center" border="0">
            <tr>
                <td width="366px" background="<%=this.strSkinPath %>/bg.gif">
                    <table cellspacing="0" cellpadding="0" width="366px" height="217px" align="center"
                        border="0">
                        <tr height="10px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td width="15px">
                            </td>
                            <td height="23px">
                                <img src="<%=this.strSkinPath %>/title-1.gif" width="293px" height="23px" border="0" usemap="#Map" />
                            </td>
                            <td width="15px">
                            </td>
                        </tr><tr><td>&nbsp;</td><td></td><td></td></tr>
                        <tr>
                            <td width="15px">
                            </td>
                            <td height="170px" valign="top">
                                <!--电子公告
				                <marquee onMouseOver="this.stop()" onMouseOut="this.start()" scrollAmount="1" scrollDelay="60"
																			direction="up" behavior="scroll" width="330" height="160"><asp:Label ID="LbBulletin" runat="server"></asp:Label></marquee>-->
                                <asp:GridView ID="GVbulletin" AutoGenerateColumns="false" GridLines="None" ShowHeader="false" Width="320px" OnRowDataBound="GVbulletin_RowDataBound" runat="server"><Columns><asp:TemplateField><ItemTemplate>
                                              <img src="<%=this.strSkinPath %>/main_dot.gif" alt="pic" / >
                                            </ItemTemplate></asp:TemplateField><asp:ButtonField DataTextField="V_TITLE" Text="按钮" /><asp:BoundField DataField="rq" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle ForeColor="#333333" Font-Size="12px" CssClass="sys_default_liststyle"></RowStyle></asp:GridView>
                            </td> 
                            <td width="15px">            
                            </td>    
                        </tr>     
                    </table>  
                </td>
              
                <td width="28px">
                </td>
                <td width="366px" background="<%=this.strSkinPath %>/bg.gif">
                    <table cellspacing="0" cellpadding="0" width="366px" height="217px" align="center"
                        border="0">
                        <tr height="10px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td width="15px">
                            </td>
                            <td height="23px">
                                
                                 <img src="<%=this.strSkinPath %>/title-2.gif" width="293px" height="23px" usemap="#Map2" border="0"/>
                            </td>
                            <td width="15px">
                            </td>
                        </tr><tr><td>&nbsp; </td><td></td><td></td></tr>
                        <tr>
                            <td width="15px">
                            </td>
                            <td height="170px" valign="top">
                                <asp:GridView ID="GVNews" AutoGenerateColumns="false" DataSourceID="SqlNews" Width="320px" GridLines="None" ShowHeader="false" OnRowDataBound="GVNews_RowDataBound" runat="server"><Columns><asp:TemplateField><ItemTemplate>
                                                <img src="<%=this.strSkinPath %>/main_dot.gif" />
                                            </ItemTemplate></asp:TemplateField><asp:HyperLinkField DataTextField="InsName" /><asp:BoundField DataField="writedate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle Font-Size="12px" ForeColor="#333333" CssClass="sys_default_liststyle"></RowStyle></asp:GridView>
                                <asp:SqlDataSource ID="SqlNews" SelectCommand="SELECT top(6) * from Institutions where status=1 and isvalid=1 order by writedate desc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                                
                               
                            </td>
                            <td width="15px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="30px">
                <td>
                   </td>
            </tr>
            <tr>
                <td width="366px" background="<%=this.strSkinPath %>/bg.gif">
                    <table cellspacing="0" cellpadding="0" width="366px" height="217px" align="center"
                        border="0">
                        <tr height="10px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td width="15px">
                            </td>
                            <td height="23px">
                                <img src="<%=this.strSkinPath %>/title-4.gif" width="293px" height="23px" usemap="#Map3" />
                            </td>
                            <td width="15px">
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    <tr><td>&nbsp;</td><td></td><td></td></tr>
                            <td width="15px">
                            </td>
                            <td height="170px" valign="top">
                                <!--流程审核-->
                                <asp:GridView ID="gvAuditingList" AutoGenerateColumns="false" DataSourceID="SqlAudit" GridLines="None" ShowHeader="false" Width="293px" OnRowDataBound="gvAuditingList_RowDataBound" DataKeyNames="NoteID" runat="server">
<EmptyDataTemplate>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField><ItemTemplate>
                                            <img src="<%=this.strSkinPath %>/main_dot.gif" />
                                        </ItemTemplate></asp:TemplateField><asp:ButtonField CommandName="Select" DataTextField="BusinessClassName" /><asp:BoundField DataField="rq" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle><RowStyle Font-Size="12px" ForeColor="#333333" CssClass="sys_default_liststyle"></RowStyle></asp:GridView>

                                <asp:SqlDataSource ID="SqlAudit" SelectCommand="SELECT top 6 a.BusinessCode, DATEDIFF(hh, b.OutOfTime, GETDATE()) AS cs,a.BusinessClass,CONVERT (varchar(10), a.StartTime, 20) as rq, (SELECT BusinessClassName FROM WF_Business_Class AS d WHERE (BusinessCode = a.BusinessCode) AND (BusinessClass = a.BusinessClass)) AS BusinessClassName, b.NoteID, b.IsAllPass, a.TemplateID, (SELECT TemplateName FROM WF_Template AS c WHERE (TemplateID = a.TemplateID)) AS TemplateName, b.NodeID, b.NodeName, (SELECT v_xm FROM PT_yhmc WHERE (v_yhdm = a.Organiger)) AS organigerName, a.StartTime, a.InstanceCode, dbo.GetBusinessName(a.BusinessCode) AS BusinessName ,b.ArriveTime ,b.During FROM WF_Instance_Main AS a INNER JOIN WF_Instance AS b ON a.ID = b.ID WHERE ((b.Sing = 0) AND (b.Operator = @operator)) OR ((b.Sing = 0) AND (dbo.GetCommissionMan(a.TemplateID, b.Operator) = @operator)) order by a.StartTime desc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="operator" SessionField="yhdm"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                            </td>
                            <td width="15px">
                            </td>
                    </table>
                </td>
                <td width="38px">
                </td>
                <td width="366px"  background="<%=this.strSkinPath %>/bg.gif">
                    <table cellspacing="0" cellpadding="0" width="366px" height="217px" align="center"
                        border="0">
                        <tr height="10px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td width="15px">
                            </td>
                            <td height="23px">
                                <img src="<%=this.strSkinPath %>/title-3.gif" width="293px" height="23px" usemap="#Map4" />
                            </td>
                            <td width="15px">
                            </td>
                        </tr><tr><td>&nbsp;</td><td></td><td></td></tr>
                        <tr>
                            <td width="15px">
                            </td>
                            <td height="170px" valign="top" id="TD1" runat="server">
                                <!--待办-->
                                <asp:GridView ID="GVDBSJ" AutoGenerateColumns="false" DataSourceID="SqlDBSJ" ShowHeader="false" Width="320px" GridLines="None" OnRowDataBound="GVDBSJ_RowDataBound" DataKeyNames="I_DBSJ_ID" runat="server"><Columns><asp:TemplateField><ItemTemplate>
                                         <img src="<%=this.strSkinPath %>/main_dot.gif" />
                                        </ItemTemplate></asp:TemplateField><asp:HyperLinkField DataTextField="V_Content" /><asp:BoundField DataField="DTM_DBSJ" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle Font-Size="12px" ForeColor="#333333" CssClass="sys_default_liststyle"></RowStyle></asp:GridView>
                                <asp:SqlDataSource ID="SqlDBSJ" SelectCommand="SELECT top 6 [I_DBSJ_ID], [I_XGID], [V_LXBM], [V_YHDM], convert(varchar(10),DTM_DBSJ,20) as DTM_DBSJ, [C_OpenFlag], [V_DBLJ], [V_TPLJ], [V_Content] FROM [PT_DBSJ] where [V_YHDM] = @V_YHDM and datediff(ss,DTM_DBSJ,getdate())>0 and v_dblj != '' ORDER BY [DTM_DBSJ] desc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="V_YHDM" SessionField="yhdm"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                            </td>
                            <td width="15px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="15px">
                <td>
                </td>
            </tr>
            <tr style="display: none">
                <td colspan="3">
                    &nbsp;<MyUserControl:sysframe_webswf_ascx ID="WebSwf1" DESIGNTIMEDRAGDROP="425" runat="server" />
                </td>
            </tr>
        </table>
    </form>
    <map name="Map2" id="Map2">
 
 <area shape="rect" coords="252,2,290,20" href="javascript:toolbox_oncommand('/oa/System/Institution/InstitutionListSearch.aspx','公司制度');">
   </map>
    <map name="Map4" id="Map4">
        <area shape="rect" coords="253,1,289,19" href="javascript:toolbox_oncommand('PTDBSJList.aspx','待办工作');">
    </map>
    <map name="Map3" id="Map3">
        <area shape="rect" coords="252,3,292,21" href="javascript:toolbox_oncommand('/EPC/WorkFlow/PTAuditList.aspx','流程审核');">
    </map>
    <map name="Map" id="Map">
        <area shape="rect" coords="244,1,292,23" href="javascript:toolbox_oncommand('/oa/Bulletin/BulletinManage.aspx?type=see','公告查看');">
    </map>
 
</body>
</html>
