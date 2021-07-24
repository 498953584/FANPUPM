<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuildDiaryList.aspx.cs" Inherits="OPM_BuildDiary_BuildDiaryList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>施工日志列表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            hideControls();

            var typeTable = new JustWinTable('gvBuildDiaryList');
            setButton(typeTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldGuid')
            //            typeTable.setBtnStateByJustwinTable('btnUserCodes');
            replaceEmptyTable();
            registerBtnDeleteEvent();
            registerBtnQueryEvent();

            if (document.getElementById('HdnState').value != "view") {
                setWfButtonState2(typeTable, 'WF1');
            }

            //trFrame 为存放Frame的TR
            //必需将整个Table的高度设置为100%，且第二个TR的高度为1px
            var trWidth = document.getElementById('trFrame').offsetHeight;
            document.getElementById('frmbottom').style.height = (trWidth - 34) + 'px';
            document.getElementById("");

            //默认选中第一行  根据胡经理的意思修改
            $('#gvBuildDiaryList tr[id]:eq(0)').trigger('click');
        });


        //点击行事件
        function clickRows(id, flowstate) {
            var url = "";
            var type = document.getElementById('hfldType').value;
            if (id != "") {
                $("#btnUpdate").removeAttr("disabled");
                $("#btnDelete").removeAttr("disabled");
                $("#btnQuery").removeAttr("disabled");
                $("#hfldBuildDiaryId").val(id);
                url = "BuildDiary_MX.aspx?UID=" + document.getElementById("hfldBuildDiaryId").value + "&type=" + type + "&flowState=" + flowstate;
                document.getElementById("frmbottom").src = url;

            } else {
                $("#btnUpdate").attr("disabled", "disabled");
                $("#btnDelete").attr("disabled", "disabled");
                $("#btnQuery").attr("disabled", "disabled");
                url = "BuildDiary_MX.aspx?UID=''&type=" + type;
                document.getElementById("frmbottom").src = url;
            }

        }
        function Change(SCheckBox) {
            var objs = document.getElementsByTagName('input');
            for (var i = 0; i < objs.length; i++) {
                if (objs[i].type.toLowerCase() == 'checkbox') {
                    if (objs[i].id == SCheckBox.id)
                        objs[i].checked = true;
                    else
                        objs[i].checked = false;
                }
            }
            document.getElementById("hfldGuid").value = SCheckBox.parentNode.parentNode.id;
            //            setbkImg(getRequestParam("spId"), 'hfldGuid');
        }

        //新增或编辑
        function addBuildDiaryList(type) {
            parent.parent.desktop.flowclass = window; //不可少
            var title = '';
            if (type == "add") {
                title = '添加施工日志';
            }
            else if (type == "edit") {
                title = '更新施工日志';
            }

            var uid = document.getElementById("hfldBuildDiaryId").value; //施工日志ID

            var url = '/EPC/BuildDiary/AddBuildDiary.aspx?uid=' + uid + '&type=' + type + "&pc=" + document.getElementById('hfldSelectedPrj').value;

            top.ui._BuildDiaryList = window;
            //top.ui.openTab(url, title);
            toolbox_oncommand(url, title);
        }

        function registerBtnDeleteEvent() {
            if (!document.getElementById('btnDelete')) return;
            var btnDelete = document.getElementById('btnDelete');
            btnDelete.onclick = function () {
                if (!confirm('系统提示：\n\n确定要删除吗？')) {
                    return false;
                }
            }
        }

        function registerBtnQueryEvent() {
            if (!document.getElementById('btnQuery')) return;
            var btnQuery = document.getElementById('btnQuery');
            addEvent(btnQuery, 'click', function () {
                //                parent.desktop.BuildDiaryView = window;
                var url = '/EPC/BuildDiary/DiaryInfoView.aspx?ic=' + document.getElementById('hfldBuildDiaryId').value;
                toolbox_oncommand(url, "查看施工日志");
            })
        }

        function replaceEmptyTable() {
            //当数据量为空时，修改样式
            if (!document.getElementById('gvBuildDiaryList')) return;
            if (!document.getElementById('emptyBuildDiaryList')) return;
            var gvwContractType = document.getElementById('gvBuildDiaryList');
            var emptyContractType = document.getElementById('emptyBuildDiaryList');
            if (gvwContractType.firstChild.childNodes.length == 1) {
                gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
            }
        }

        function hideControls() {
            if (!document.getElementById('btnDataBind')) return;
            document.getElementById('btnDataBind').style.display = 'none';
        }

       

    </script>
    <style type="text/css">
        .style1
        {
            height: 278px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <table class="queryTable" cellpadding="3px" cellspacing="0px">
        <tr>
            <td class="descTd">
                创建人
            </td>
            <td>
                <asp:TextBox ID="txtcreatorName" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td class="descTd">
                记录员
            </td>
            <td>
                <asp:TextBox ID="txtrecordName" Width="120px" runat="server"></asp:TextBox>
            </td>
            <td class="descTd">
                施工人员
            </td>
            <td>
                <asp:TextBox ID="txtjobName" Width="120px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="descTd">
                签约日期
            </td>
            <td>
                <JWControl:DateBox ID="txtstartTime" Width="120px" runat="server"></JWControl:DateBox>
            </td>
            <td class="descTd">
                至
            </td>
            <td>
                <JWControl:DateBox ID="txtendTime" Width="120px" runat="server"></JWControl:DateBox>
            </td>
            <td style="border: solid 0px red;" colspan="2">
                <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr id="frmTop">
            <td style="vertical-align: top;" class="style1">
                <table width="100%">
                    <tr style="vertical-align: top;">
                        <td class="divFooter" style="text-align: left;">
                            <input type="button" id="btnAdd" value="新增" onclick="addBuildDiaryList('add')" runat="server" />

                            <input type="button" id="btnUpdate" value="编辑" disabled="true" onclick="addBuildDiaryList('edit')" runat="server" />

                            <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                            <input type="button" id="btnQuery" value="查看" disabled="disabled" />&nbsp;
                            <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="110" BusiClass="001" runat="server" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvBuildDiaryList" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvBuildDiaryList_RowDataBound" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
                                    <table id="emptyBuildDiaryList" class="gvdata">
                                        <tr class="header">
                                            <th scope="col" style="width: 20px;">
                                                <input id="chk1" type="checkbox" />
                                            </th>
                                            <th scope="col" style="width: 25px;">
                                                序号
                                            </th>
                                            <th scope="col">
                                                日志编号
                                            </th>
                                            <th scope="col">
                                                项目
                                            </th>
                                            <th scope="col">
                                                施工人员
                                            </th>
                                            <th scope="col">
                                                记录员
                                            </th>
                                            <th scope="col">
                                                是否有效
                                            </th>
                                            <th scope="col">
                                                流程状态
                                            </th>
                                            <th scope="col">
                                                创建人
                                            </th>
                                            <th scope="col">
                                                创建时间
                                            </th>
                                            <th scope="col">
                                                发生日期
                                            </th>
                                            <th scope="col">
                                                备注
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" />
                                        </HeaderTemplate><ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <%# (this.BuildDiaryPage.CurrentPageIndex - 1) * this.BuildDiaryPage.PageSize + Container.DataItemIndex + 1 %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="日志编号" HeaderStyle-Width="200px"><ItemTemplate>
                                            <%# Eval("SN") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目" HeaderStyle-Width="200px"><ItemTemplate>
                                            <span class="link" onclick="viewopen('/EPC/BuildDiary/DiaryInfoView.aspx?ic=<%# Eval("UID") %>', '施工日志查看')">
                                                <%# Eval("PrjName") %>
                                            </span>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工人员" HeaderStyle-Width="200px"><ItemTemplate>
                                            <%# Eval("Jbr") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="记录员" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <%# Eval("Record") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否有效" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <img src='<%# WebUtil.GetImgUrl(Eval("IsValid").ToString()) %>' />
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                            <asp:HiddenField ID="hfldFlowState" Value='<%# System.Convert.ToString(Eval("FlowState"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="创建人" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <%# StringUtility.GetStr(WebUtil.GetUserNames(Eval("AddUser").ToString())) %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <%# Common2.GetTime(Eval("AddTime").ToString()) %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发生日期" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                            <%# Common2.GetTime(Eval("Fsrq").ToString()) %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                            <asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Remark").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Remark").ToString()) %></asp:HyperLink>
                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            <webdiyer:AspNetPager ID="BuildDiaryPage" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="BuildDiaryPage_PageChanged" runat="server">
                            </webdiyer:AspNetPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trFrame">
            <td style="width: 100%; vertical-align: top; padding-top: 10px;" colspan="4">
                <iframe id="frmbottom" frameborder="0" width="100%" runat="server"></iframe>
            </td>
        </tr>
    </table>
    <div id="divManage" title="管理施工日志" style="display: none;">
        <iframe id="ifManage" frameborder="0" width="100%" height="100%"></iframe>
    </div>
    <input type="button" id="btn" value="Button" style="display: none" onclick="Test()" />
    <asp:HiddenField ID="hfldSelectedPrj" runat="server" />
    <asp:HiddenField ID="hfldBuildDiaryId" runat="server" />
    <asp:HiddenField ID="hfldGuid" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <asp:HiddenField ID="hfldTypeID" runat="server" />
    <asp:HiddenField ID="HdnState" runat="server" />
    <asp:Button ID="btnDataBind" Text="Button" OnClick="btnDataBind_Click" runat="server" />
    </form>
</body>
</html>
