<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjMemberView.aspx.cs" Inherits="PrjManager_PrjMemberView" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目小组成员查看</title>

    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            replaceEmptyTable('emptyPrjMembers', 'gvwPrjMembers');
        });

        
    </script>
    <style type="text/css" media="print">
    .noprint{display : none }
    </style>
<style type="text/css">
#gvwPrjMembers div
{
	word-break: break-all;
	
	}
</style>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; border-collapse: collapse;">
        <tr>
            <td class="divHeader">
                项目成员信息
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td  class="groupInfo" style="vertical-align: top;  text-align: center;
                border-style: none; height:20px;">
               <asp:Label ID="lblMemberTitle" class="groupInfo" runat="server">小组成员</asp:Label>
            </td>
        </tr>
        <tr>
            <td id="gvwTd"  style="vertical-align: top; height:1px; ">
                <asp:GridView ID="gvwPrjMembers" AutoGenerateColumns="false" ShowHeader="true" CssClass="viewTable" OnRowDataBound="gvwPrjMembers_RowDataBound" DataKeyNames="PrjMemberId" runat="server">
<EmptyDataTemplate>
                        <table class="gvdata" cellspacing="0" id="emptyPrjMembers" rules="all" border="1"
                            style="width: 100%; border-collapse: collapse;">
                            <tr>
                                <th class="header">
                                    序号
                                </th>
                                <th class="header">
                                    姓名
                                </th>
                                <th class="header">
                                    岗位
                                </th>
                                <th class="header">
                                    学历及专业
                                </th>
                                <th class="header">
                                    项目编号
                                </th>
                                <th class="header">
                                    职称
                                </th>
                                <th class="header">
                                    资格证书
                                </th>
                                <th class="header">
                                    上岗培训情况
                                </th>
                                <th class="header">
                                    以往工作表现
                                </th>
                                <th>
                                    附件
                                </th>
                            </tr>
                            <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                             <td></td>
                            <td></td>
                            <td></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序号" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" DataField="No" /><asp:BoundField HeaderText="姓名" DataField="MemberName" HeaderStyle-Width="100px" /><asp:BoundField HeaderText="岗位" DataField="Post" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="学历及专业" HeaderStyle-Width="200px"><ItemTemplate>
                                <span>
                                    <%# Eval("EducationalBackground") %></span> <span>
                                       </span>
                            </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="职称" DataField="Technical" HeaderStyle-Width="100px" /><asp:BoundField HeaderText="资格证书" DataField="PostAndCompetency" HeaderStyle-Width="150px" /><asp:TemplateField HeaderText="上岗培训情况" HeaderStyle-Width="220px"><ItemTemplate>
                            <div>
                                
                                        <%# Eval("TrainingInformation") %></p>
                                        </div>
                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="以往工作表现" HeaderStyle-Width="220px"><ItemTemplate>
                         <div>
                                
                                <%# StringUtility.ReplaceTxt(Eval("PastPerformance").ToString()) %>
                            </div>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" HeaderStyle-Width="70px"><ItemTemplate>
                            <div>
                              <span  ID="spAdjunct" runat="server"></span>
                              </div>
                            </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td style="vertical-align: top;">
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="100" BusiClass="001" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
