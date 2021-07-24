<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotosCheckInList2.aspx.cs" Inherits="EPC_ProjectCost_PhotosCheckInList" %>

<html>
<head runat="server"><title>拍照监督解决问题</title>
    <script type="text/javascript" src="../../../../Web_Client/TreeNew.js"></script>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

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
    <script language="JavaScript">
        function clickRow(obj, IntendanceGuid, state, QuestionTag, prjName, prjGuid) {
            document.getElementById("hdnPrjName").value = prjName;
            document.getElementById("hdnPrjGuid").value = prjGuid;
            document.getElementById("hdnIntendanceGuid").value = IntendanceGuid;
            if (document.getElementById("hdnflag").value == 1) {//查询

                if (state != 2) {
                    if (state == 0) {
                        document.getElementById('btnUpdate').disabled = false;
                        if (QuestionTag == 0) { //是否有回复

                            document.getElementById('btnDel').disabled = false; //没有回复可以删除
                        } else {
                            document.getElementById('btnDel').disabled = true;
                        }
                        document.getElementById("btnValidate").disabled = true;
                    } else {
                        document.getElementById('btnUpdate').disabled = true;
                        document.getElementById('btnDel').disabled = true;
                        document.getElementById("btnValidate").disabled = true;
                    }
                } else {
                    document.getElementById("btnValidate").disabled = false;
                    document.getElementById('btnUpdate').disabled = true;
                    document.getElementById('btnDel').disabled = true;
                }
            } else if (document.getElementById("hdnflag").value == 2) { //验证

                if (state != 2) {
                    if (state == 0) {
                        document.getElementById('btnUpdate').disabled = false;
                        if (QuestionTag == 0) { //是否有回复

                            document.getElementById('btnDel').disabled = false; //没有回复可以删除
                        } else {
                            document.getElementById('btnDel').disabled = true;
                        }
                        document.getElementById("btnValidate").disabled = true;
                    } else {
                        document.getElementById('btnUpdate').disabled = true;
                        document.getElementById('btnDel').disabled = true;
                        document.getElementById("btnValidate").disabled = true;
                    }
                } else {
                    document.getElementById("btnValidate").disabled = false;
                    document.getElementById('btnUpdate').disabled = true;
                    document.getElementById('btnDel').disabled = true;
                }
            }
            else if (document.getElementById("hdnflag").value == 3) {

                if (state == 1 || state == 0) {
                    document.getElementById('btnSettle').disabled = false;
                } else {
                    document.getElementById('btnSettle').disabled = true;

                }
                document.getElementById('btnView').disabled = false;
            }
            document.getElementById('btnView').disabled = false;
            /*在这之前添加你的处理代码*/
            doClick(obj, 'grdModules'); //调用样式设置
        }
        function outRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOut(obj); //调用样式设置
        }
        function overRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOver(obj); //调用样式设置
        }

        function switchDisplay(obj, t1, t2) {
            /*在这之前添加你的处理代码*/
            doSwitchDisplay(obj, 'grdModules', 'hdnModuleCodeList', t1, t2, '../../../'); //调用样式设置
        }

        function validate() {
            divTdHeight(1, 0, 0, 'dvModulesBox')
        }

        //   var pn = "<%=ProjectName %>";
        function ClickBtn(op) {
            var IntendanceGuid = document.getElementById('hdnIntendanceGuid').value;
            var pn = document.getElementById("hdnPrjName").value;
            var projectCode = document.getElementById("hdnPrjGuid").value;
            var url = ""
            var re = true;
            var w = screen.availWidth;
            var h = screen.availHeight;
            var p = "top=0,left=0,width=" + w + "px,height=" + h + "px,toolbar=no,status=yes,scrollbars=yes,resizable=yes";
            switch (op.toLowerCase()) {
                case "add":
                    var pc = "<%=ProjectCode %>";
                    var pname = "<%=ProjectName %>";
                    //                    var h= h / 2;E:\OPM_0325\MainWeb\OPM\EPCM\PhotosCheckIn\PhotosCheckInList2.aspx
                    url = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/PhotosCheckInEdit.aspx?op=add&prj=" + pc + "&pn=" + escape(pname) + "&IntendanceGuid=" + IntendanceGuid;
                    //re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + 600 + 'px;center:1;help:0;status:0;');
                    // window.open(url, '', p);
                    //                    if (state == "n") {
                    //                        parent.desktop.flowclass = window; //不可少
                    //                    } else {
                    top.ui._PhotosCheckInList2 = window; //不可少
                    //}
                    re = toolbox_oncommand(url, "登记问题");

                    break;
                case "upd":
                    url = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/PhotosCheckInEdit.aspx?op=upd&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid;
                    // window.open(url, '', p);

                    //re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    //                    if (state == "n") {
                    //                        parent.desktop.flowclass = window; //不可少
                    //                    } else {
                    top.ui._PhotosCheckInList2 = window; //不可少
                    //                    }
                    re = toolbox_oncommand(url, "登记问题修改");
                    break;
                case "view":
                    // url = "PhotosCheckInEdit.aspx?op=view&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid;
                    url = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/SettleQuestionEdit.aspx?op=view&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid + "&t=2";
                    //re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    //window.open(url, '', p);
                    re = toolbox_oncommand(url, "查看问题");
                    break;

                case "settle": //解决问题
                    url = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/SettleQuestionEdit.aspx?prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid + "&t=0";
                    //re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    // window.open(url, '', p);
                    top.ui._PhotosCheckInList2 = window; //不可少
                    re = toolbox_oncommand(url, "解决问题");
                    break;
                case "v": //验证
                    url = "/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/SettleQuestionEdit.aspx?op=add&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid + "&t=1";
                    //re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    top.ui._PhotosCheckInList2 = window; //不可少
                    re = toolbox_oncommand(url, "验证");
                    // window.open(url, '', p);
                    break;

            }
            return re;
        }

        //选择项目
        function openProjPicker() {

            document.getElementById("divFram").title = "选择项目";
            document.getElementById("ifFram").src = "/Common/DivSelectProject.aspx?Method=returnPrj";
            selectnEvent('divFram');
            // alert('测试');
        }
        //选择项目返回值
        function returnPrj(id, name, PrjManager) {
            document.getElementById('hdnPrjID').value = id;
            document.getElementById('txtPrjName').value = name;
            document.getElementById('txtPrjName').accessKey = '1';
        }
    </script>
    
    <script language="javascript" type="text/javascript">
        function blur_func(chcid, txtId) {

            if (document.getElementById(txtId).value != '') {
                document.getElementById(chcid).checked = true;
            } else {
                document.getElementById(chcid).checked = false;
            }
        }
        function blur_checkBox(chcid, txtId) {
            if (document.getElementById(chcid).checked == false) {
                document.getElementById(txtId).value = "";
            }
        }

        //设置时间复选框
        function blur_Date(beginId, endId, chcId) {
            if (document.getElementById(beginId).value != '' || document.getElementById(endId).value != '') {
                document.getElementById(chcId).checked = true;
            } else if (document.getElementById(beginId).value == '' && document.getElementById(endId).value == '') {
                document.getElementById(chcId).checked = false;
            }
        }
        function blur_checkDate(beginId, endId, chcId) {
            if (document.getElementById(chcId).checked == false) {
                document.getElementById(beginId).value = "";
                document.getElementById(endId).value = "";
            }
        }

        function aa() {
            alert();
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 122px;
        }
    </style>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <table id="Table2" cellspacing="0" cellpadding="0" height="100%" width="100%" border="1"
        align="center" class="table-normal">
        <tr>
            <td class="td-search">
                
                <input id="chcDate" type="checkbox" style="display: none" onclick="blur_checkDate('txtBeginDate','txtEndDate','chcDate')" runat="server" />

                登记时间：<JWControl:DateBox ID="txtBeginDate" onblur="return blur_Date('txtBeginDate','txtEndDate','chcDate')" Width="60px" CssClass="text-input" runat="server"></JWControl:DateBox>-<JWControl:DateBox ID="txtEndDate" Width="60px" onblur="return blur_Date('txtBeginDate','txtEndDate','chcDate')" CssClass="text-input" runat="server"></JWControl:DateBox>
                <input id="chcTitle" type="checkbox" onclick="blur_checkBox('chcTitle','txtTitle')" style="display: none" runat="server" />

                问题标题：
                <input id="txtTitle" type="text" class="text-input" style="width: 80px;" onkeyup="return blur_func('chcTitle','txtTitle');" onchange="return blur_func('chcTitle','txtTitle');" runat="server" />

                <asp:CheckBox ID="chcState" Style="display: none" runat="server" />处理状态：<asp:DropDownList ID="ddlState" runat="server"><asp:ListItem Value="4" Text="全部" /><asp:ListItem Value="0" Text="未解决" /><asp:ListItem Value="1" Text="解决中" /><asp:ListItem Value="2" Text="已解决" /><asp:ListItem Value="3" Text="已解决并验证" /></asp:DropDownList>
                <asp:CheckBox ID="chcType" Style="display: none" runat="server" />
                问题类别：<asp:DropDownList ID="ddlType" DataTextField="CodeName" DataValueField="CodeID" runat="server"></asp:DropDownList>
                
                <asp:Label ID="chcPrj" Text="项目名称：" runat="server"></asp:Label>
                <span id="Panel1" class="spanSelect" style="width: 124px; height: 20px;
                    border: 1px solid #B5CCDE; text-align: left; vertical-align: bottom" runat="server">
                    <asp:TextBox ID="txtPrjName" ContentEditable="false" Style="width: 97px;
                        float: left; height: 19px; border: none; line-height: 16px;" runat="server"></asp:TextBox>
                    <img src="../../../../images/icon.bmp" style="float: right; margin-right: 2px" alt="选择项目"
                        id="imgPrj" onclick="openProjPicker();" />
                </span>
                <asp:HiddenField ID="hdnPrjID" runat="server" />
                <asp:Button ID="BtnSearch" CssClass="td-search-button" Text="" OnClick="BtnSearch_Click" runat="server" />
            </td>
        </tr>
        <tr class="td-toolsbar">
            <td>
                &nbsp;
                <asp:Button ID="btnAdd" Text="添加" CssClass="button-normal" Enabled="false" OnClick="btnAdd_Click" runat="server" />
                <asp:Button ID="btnUpdate" Enabled="false" Text="编辑" CssClass="button-normal" OnClick="btnUpdate_Click" runat="server" />
                <asp:Button ID="btnDel" Enabled="false" Text="删除" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
                <asp:Button ID="btnValidate" Text="验证" CssClass="button-normal" Enabled="false" OnClick="btnValidate_Click" runat="server" />
                <asp:Button ID="btnSettle" Text="解决问题" Width="80px" CssClass="button-normal" Enabled="false" OnClick="btnSettle_Click" runat="server" />
                <asp:Button ID="btnView" Text="查看" CssClass="button-normal" Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td-title">
                <asp:Label ID="lblTitle" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <div id="dvModulesBox" class="gridBox" style="height: 400px">
                    <asp:GridView ID="grdModules" AllowPaging="true" AutoGenerateColumns="false" Width="1280px" PageSize="15" CssClass="grid" BorderColor="#4F92E7" GridLines="Both" OnRowDataBound="grdModules_RowDataBound" OnPageIndexChanging="grdModules_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" bordercolor="#4F92E7" border="1"
                                id="GV" style="border-color: #4F92E7; border-collapse: collapse;">
                                <tr class="grid_head">
                                    <th scope="col">
                                        序号
                                    </th>
                                    <th scope="col">
                                        问题标题
                                    </th>
                                    <th scope="col">
                                        问题类别
                                    </th>
                                    <th scope="col">
                                        项目名称
                                    </th>
                                    <th scope="col">
                                        登记时间
                                    </th>
                                    <th scope="col">
                                        登记人
                                    </th>
                                    <th scope="col">
                                        处理状态
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>

<PagerTemplate>
                            <table width="100%">
                                <tr>
                                    <td style="text-align: right">
                                        第<asp:Label ID="lblPageIndex" Text='<%# Convert.ToString(((GridView)Container.Parent.Parent).PageIndex + 1) %>' runat="server"></asp:Label>页
                                        共<asp:Label ID="lblPageCount" Text='<%# Convert.ToString(((GridView)Container.Parent.Parent).PageCount) %>' runat="server"></asp:Label>页
                                        <asp:LinkButton ID="btnFirst" CausesValidation="false" CommandArgument="First" CommandName="Page" Text="首页" runat="server"></asp:LinkButton>
                                        <asp:LinkButton ID="btnPrev" CausesValidation="false" CommandArgument="Prev" CommandName="Page" Text="上一页" runat="server"></asp:LinkButton>
                                        <asp:LinkButton ID="btnNext" CausesValidation="false" CommandArgument="Next" CommandName="Page" Text="下一页" runat="server"></asp:LinkButton>
                                        <asp:LinkButton ID="btnLast" CausesValidation="false" CommandArgument="Last" CommandName="Page" Text="尾页" runat="server"></asp:LinkButton>
                                        <asp:TextBox ID="txtNewPageIndex" Width="20px" Text='<%# Convert.ToString(((GridView)Container.Parent.Parent).PageIndex + 1) %>' runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="btnGo" CausesValidation="false" CommandArgument="-1" CommandName="Page" Text="GO" runat="server"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="问题标题"><ItemTemplate>
                                    <span style="color: Blue; text-decoration: underline; cursor: pointer;" onclick="toolbox_oncommand('/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/SettleQuestionEdit.aspx?op=add&IntendanceGuid= <%# Eval("IntendanceGuid") %>&t=2','查看问题');">
                                        <%# Eval("QuestionTitle") %>
                                    </span>
                                    
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="CodeName" HeaderText="问题类别" /><asp:BoundField DataField="PrjName" HeaderText="项目名称" /><asp:BoundField DataField="BookInDate" HeaderText="登记时间" HeaderStyle-Width="70px" DataFormatString="{0:yyyy-MM-dd}" /><asp:BoundField DataField="v_xm" HeaderText="登记人" /><asp:BoundField DataField="SettleState" HeaderText="处理状态" /></Columns></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdnIntendanceGuid" runat="server" />
    <asp:HiddenField ID="hdnflag" runat="server" />
    <asp:HiddenField ID="hdnPrjGuid" runat="server" />
    <asp:HiddenField ID="hdnPrjName" runat="server" />
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
