<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="TenderStateReport.aspx.cs" Inherits="TenderManage_TenderStateReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>项目状态一览表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var jwTable = new JustWinTable('gvTender');
            replaceEmptyTable('emptyTable', 'gvTender');
            showTooltip('tooltip', 25);
            jwTable.registClickTrListener(function () {
                $('#hfldPrjId').val($(this).attr('id'));
                if ($(this).attr('changed') == '0') {
                    $('#showPrjInfoZTBChgState').attr('disabled', 'disabled');
                } else {
                    $('#showPrjInfoZTBChgState').removeAttr('disabled');
                }
            });
        });

        // 查看状态变成信息
        function changeStateShow() {
            if ($('#hfldPrjId').val() != '') {
                var url = '/TenderManage/PrjinfoChangeFlowQuery.aspx?isShowQ=1&prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '状态变更查看', url: url });
            } else {
                top.ui.alert('请先选择项目！');
            }
        }
        // 选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }

        // 选择部门
        function SelDept(id, name) {
            jw.selectOneDep({ nameinput: 'txtProjPeopleDep', idinput: 'hfldProjPeopleDep' });
        }

        function viewopen_n(id) {
            viewopen('/TenderManage/InfoView.aspx?ic=' + id, '项目立项查看');
        }

        //甲方名称
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtOwnerName', idinput: 'hfldOwner' });
        }
        function setSummarizingInfo() {
            var SummarizingInfo = $('#spSummarizingInfo');
            var summarText = SummarizingInfo[0].innerText;
            $('#hfldSummarizingInfo').val(summarText);
        }
        
    </script>
    <style type="text/css">
        .padding
        {
            margin-left: 3px;
            margin-right: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            项目编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" Width="124px" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            项目经理
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <input type="text" id="txtManager" style="height: 15px; width: 97px;
                                    margin: 1px 0px 1px 2px; border: none;" runat="server" />

                                <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img3" onclick="selectUser('hfldManager','txtManager');" />
                            </span>
                            <asp:HiddenField ID="hfldManager" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            立项申请日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            至
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            建设单位
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <asp:TextBox ID="txtOwnerName" Style="width: 97px; height: 15px; border: none; line-height: 16px;
                                    margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                <img alt="选择甲方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
                            </span>
                            <asp:HiddenField ID="hfldOwner" runat="server" />
                        </td>
                        <td class="descTd">
                            主要负责人
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <input type="text" id="txtPrincipal" style="height: 15px; width: 97px;
                                    margin: 1px 0px 1px 2px; border: none;" runat="server" />

                                <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img5" onclick="selectUser('hfldPrincipal','txtPrincipal');" />
                            </span>
                            <asp:HiddenField ID="hfldPrincipal" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            立项申请人
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <input type="text" id="txtPrjPeople" style="height: 15px; width: 97px;
                                    margin: 1px 0px 1px 2px; border: none;" runat="server" />

                                <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="selectUser('hlfdPrjPeople','txtPrjPeople');" />
                            </span>
                            <asp:HiddenField ID="hlfdPrjPeople" runat="server" />
                        </td>
                        <td class="descTd">
                            项目跟踪人
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <input type="text" id="txtPrjFollowPeople" style="height: 15px; width: 97px;
                                    margin: 1px 0px 1px 2px; border: none;" runat="server" />

                                <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img2" onclick="selectUser('hlfdPrjFollowPeople','txtPrjFollowPeople');" />
                            </span>
                            <asp:HiddenField ID="hlfdPrjFollowPeople" runat="server" />
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            申请部门
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <asp:TextBox ID="txtProjPeopleDep" Style="width: 97px; height: 15px;
                                    border: none; line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img4" onclick="SelDept('hfldProjPeopleDep','txtProjPeopleDep');" runat="server" />

                            </span>
                            <input id="hfldProjPeopleDep" type="hidden" style="width: 1px" runat="server" />

                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            项目属性
                        </td>
                        <td>
                            <asp:DropDownList ID="dropPrjProperty" Width="100%" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                         <td class="descTd">
                            项目状态
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPrjState" Width="124px" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            状态变化时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjStateChangeTimeStart" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            至
                        </td>
                        <td class="descTd">
                            <asp:TextBox ID="txtPrjStateChangeTimeEnd" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="7">
                            <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="divFooter" style="text-align: left">
                <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClientClick="setSummarizingInfo()" OnClick="btnexecl_Click" runat="server" />
               <input id="showPrjInfoZTBChgState" value="查看变更记录" type="button" onclick='changeStateShow();' class="buttion-normal" style="width:100px;" disabled="disabled"/>
            </td>
        </tr>
        <tr>
            <td id="td-flow" style="vertical-align: top; width: 100%">
                <div class="pagediv">
                    <asp:GridView ID="gvTender" AutoGenerateColumns="false" CssClass="gvdata" ShowFooter="true" OnRowDataBound="gvTender_RowDataBound" DataKeyNames="PrjGuid" runat="server">
<EmptyDataTemplate>
                            <table id="emptyTable">
                                <tr class="header">
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        项目状态
                                    </th>
                                    <th scope="col">
                                        项目经理
                                    </th>
                                    <th scope="col">
                                        项目跟踪人
                                    </th>
                                    <th scope="col">
                                        主要负责人
                                    </th>
                                    <th scope="col">
                                        项目编号
                                    </th>
                                    <th scope="col">
                                        项目名称
                                    </th>
                                    <th scope="col">
                                        建设单位
                                    </th>
                                    <th scope="col">
                                        工程造价
                                    </th>
                                    <th scope="col">
                                        专业造价
                                    </th>
                                    <th scope="col">
                                        中标价
                                    </th>
                                    <th scope="col">
                                        信息立项
                                    </th>
                                    <th scope="col">
                                        报名通过
                                    </th>
                                    <th scope="col">
                                        报名不通过
                                    </th>
                                    <th scope="col">
                                        预审
                                    </th>
                                    <th scope="col">
                                        投标
                                    </th>
                                    <th scope="col">
                                        中标
                                    </th>
                                    <th scope="col">
                                        落标
                                    </th>
                                    <th scope="col">
                                        立项申请日期
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px"><ItemTemplate>
                                    <%# Eval("No") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目状态" HeaderStyle-Width="50px">
<ItemTemplate>
                                    <%# Eval("ItemName") %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目经理" HeaderStyle-Width="50px"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("PrjManagerName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("PrjManagerName").ToString(), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目跟踪人" HeaderStyle-Width="50px"><ItemTemplate>
                                    <%# Eval("PrjDutyName") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="主要负责人" HeaderStyle-Width="50px"><ItemTemplate>
                                    <%# Eval("WorkUnit") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目编号" HeaderStyle-Width="80px"><ItemTemplate>
                                    <%# Eval("PrjCode") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                                                    <span class="link" onclick="viewopen_n('<%# Eval("PrjGuid") %>');">
                                                  <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                                                </span>
                                    </asp:HyperLink>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位" HeaderStyle-Width="80px"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("CorpName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("CorpName").ToString(), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
                                    <%# Eval("PrjCost") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="专业造价" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
                                    <%# Eval("ProfessionalCost") %>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="中标价" ItemStyle-CssClass="decimal_input" DataField="SuccessBidPrice" ItemStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderText="信息立项" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("PrjSetUp").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="报名通过" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("PrjStart").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="报名不通过" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("PrjStartNO").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="预审" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("PrjYs").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="投标" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("PrjTender").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="中标" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("WinBid").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="落标" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("OutBid").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="放弃" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# GetState(Eval("PrjGiveUp").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px"><ItemTemplate>
                                    <%# Eval("InputDate") %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px">
                标示：<span style='color: green; font-size: 20px;'>●</span>—当前项目状态 <span style='color: green;
                    font-size: 20px; font-weight: bold;'>√</span>—预审通过 <span style='color: green; font-size: 20px;
                        font-weight: bold;'>×</span>—预审失败
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px">
                <span id="spSummarizingInfo" runat="server">
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </span>
            </td>
        </tr>
        <tr>
            <td>
                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldSummarizingInfo" runat="server" />
    <!-- 保存选中的项目Id-->
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    </form>
</body>
</html>
