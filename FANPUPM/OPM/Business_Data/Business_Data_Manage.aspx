<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business_Data_Manage.aspx.cs" Inherits="Business_Data_Manage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>图纸管理</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvBusiness_Data');
            replaceEmptyTable('emptyBusiness_Data', 'gvBusiness_Data');
            setButton(jwTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldIDArr')
            setWfButtonState2(jwTable, 'WF1');
        });


        //点击行事件
        function clickRows(id) {

            if (id != "") {
                $("#btnUpdate").removeAttr("disabled");
                $("#btnDelete").removeAttr("disabled");
                $("#btnQuery").removeAttr("disabled");
                $("#btnUserCodes").removeAttr("disabled");
                $("#hfldUID").val(id);

            } else {
                $("#btnUpdate").attr("disabled", "disabled");
                $("#btnDelete").attr("disabled", "disabled");
                $("#btnQuery").attr("disabled", "disabled");
                $("#btnUserCodes").attr("disabled", "disabled");
            }
        }
        //新增或编辑
        function edit(type) {
            top.ui._Business_Data_Manage = window;
            var codeid = document.getElementById('hdnCodeId').value;
            var prjCode = document.getElementById('hdnPrjCode').value;
            var url = "";
            if (type == 'edit') {

                url = "/OPM/Business_Data/Business_Data_Edit.aspx?type=edit&uid=" + document.getElementById('hfldUID').value + "&codeid=" + codeid + "&" + location.href.split('?')[1];
                title = '编辑图纸计划';
            }
            else if (type == 'add') {
                url = "/OPM/Business_Data/Business_Data_Edit.aspx?type=add&" + location.href.split('?')[1];
                title = "新增图纸计划";
            }
            else {
                url = "/OPM/Business_Data/Business_Data_View.aspx?ic=" + document.getElementById('hfldUID').value;
                title = '查看图纸计划信息';
            }
            if (codeid == '0' && type == 'add') {
                top.ui.alert('请选择类型！');
                return;
            }
            else {
                toolbox_oncommand(url, title);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%" cellpadding="0" cellspacing="0">
        <tr style="height: 20px; display: none;">
            <td class="divHeader">
                <asp:Label ID="lblTitle" Text="Label" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 100%;">
                <table style="width: 100%;">
                    <tr id="query" runat="server"><td id="Td1" style="width: 100%; vertical-align: top;" runat="server">
                            <table cellpadding="3px" cellspacing="0px">
                                <tr>
                                    <td class="descTd" style="display: none;">
                                        项目
                                    </td>
                                    <td style="display: none;">
                                        <asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="descTd">
                                        责任人
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDutyUser" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="descTd">
                                        图纸计划编号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBCode" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="descTd">
                                        计划完成时间
                                    </td>
                                    <td>
										<asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td></tr>
                    <tr style="vertical-align: top;" id="btn" runat="server"><td id="Td2" class="divFooter" style="text-align: left;" runat="server">
                            <input type="button" id="btnAdd" value="新增" onclick="edit('add')" runat="server" />

                            <input type="button" id="btnUpdate" value="编辑" disabled="true" onclick="edit('edit')" runat="server" />

                            <asp:Button ID="btnDelete" Text="删除" Enabled="false" OnClick="btnDelete_Click" runat="server" />
                            <input type="button" id="btnQuery" value="查看" onclick="edit('view')" disabled="true" runat="server" />

                            <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="821" BusiClass="001" runat="server" />
                            
                        </td></tr>
                    <tr id="trName" class="td-toolstbar" style="display: none" runat="server"><td id="Td3" runat="server">
                            <table>
                                <tr>
                                    <td style="font-weight: bold;">
                                        项目名称：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName1" Text="..." Style="font-weight: bold;" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 30px">
                                    </td>
                                    <td style="font-weight: bold;">
                                        阶段名称：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName2" Text="..." Style="font-weight: bold;" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td></tr>
                    <tr>
                        <td style="height: 100%;">
                            <div style="height: 100%; width: 100%;">
                                <asp:GridView CssClass="gvdata" ID="gvBusiness_Data" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvBusiness_Data_RowDataBound" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyBusiness_Data" class="gvdata" cellspacing="0" rules="all" border="1"
                                            style="width: 100%; border-collapse: collapse;">
                                            <tr class="header">
                                                <th scope="col" style="width: 20px;">
                                                    <input id="chk1" type="checkbox" />
                                                </th>
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    图纸计划编号
                                                </th>
                                                <th scope="col">
                                                    图纸计划名称
                                                </th>
                                                <th scope="col">
                                                    责任人
                                                </th>
                                                <th scope="col">
                                                    设计时间
                                                </th>
                                                <th scope="col">
                                                    计划完成时间
                                                </th>
                                                <th scope="col">
                                                    流程状态
                                                </th>
                                                
                                                
                                                <th scope="col">
                                                    备注
                                                </th>
                                                <th scope="col">
                                                    附件
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate>

<ItemTemplate>
                                                <asp:CheckBox ID="cbBox" runat="server" />
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="150px" HeaderText="图纸计划编号">
<ItemTemplate>
                                                <span class="link" onclick="toolbox_oncommand('/OPM/Business_Data/Business_Data_View.aspx?ic=<%# Eval("UID") %>', '图纸信息查看')">
                                                    <%# Eval("BCode") %></span>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="图纸计划名称">
<ItemTemplate>
                                                <%# Eval("BName") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px" HeaderText="责任人">
<ItemTemplate>
                                                <%# Eval("DutyUser") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="设计时间">
<ItemTemplate>
                                                <%# Common2.GetTime(Eval("BeginDate").ToString()) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="计划完成时间">
<ItemTemplate>
                                                <%# Common2.GetTime(Eval("EndDate").ToString()) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
<ItemTemplate>
                                                <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="图审人" Visible="false" HeaderStyle-Width="70px">
<ItemTemplate>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="150px">
<ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Remark").ToString(), 15) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px" HeaderText="附件"><ItemTemplate>
                                                <%# GetAnnx(Eval("UID").ToString()) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <asp:Label ID="lblMsgAleave" Text="" runat="server"></asp:Label>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldUID" runat="server" />
    <asp:HiddenField ID="hdUserCodes" runat="server" />
    <asp:HiddenField ID="hfldIDArr" runat="server" />
    <asp:HiddenField ID="hdUser" runat="server" />
    <asp:HiddenField ID="hdnCodeId" runat="server" />
    <asp:HiddenField ID="hfldAdjunctPath" runat="server" />
    <asp:HiddenField ID="hdnPrjCode" runat="server" />
    <asp:HiddenField ID="hdnChk" runat="server" />
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
