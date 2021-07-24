<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BidSet.aspx.cs" Inherits="TenderManage_SetInfoBid" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>投标管理-投标</title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
        });

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
        //取消按钮事件
        //url 父页面
        //refresh 是否刷新父页面
        function CancelClick(url, refresh) {
            winclose('BidSet', url, refresh);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divbtnBid" runat="server">
        <table id="table2" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    开标日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtTenderBeginDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'开标日期必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    最高限价
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderCeilingPrice" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    单位
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderUnit" MaxLength="200" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    报价
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderQuote" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    评标方法
                </td>
                <td class="txt">
                    <asp:DropDownList ID="dropTenderAppraiseMethod" Width="100%" runat="server"></asp:DropDownList>
                </td>
                <td class="word">
                    参考价
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderAverage" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    投标答疑日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderAnswerDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    投标保证金
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderEarnestMoney" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    缴费方式
                </td>
                <td class="txt">
                    <asp:DropDownList ID="dropTenderPayWay" Width="100%" runat="server"></asp:DropDownList>
                </td>
                <td class="word">
                    项目经理
                </td>
                <td class="txt ">
                    <span class="spanSelect" style="width: 99%;">
                        <asp:TextBox ID="txtTenderPrjManager" MaxLength="50" Style="width: 61%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
                            onclick="selectUser('hfldPrjManage','txtTenderPrjManager');" />
                    </span>
                    
                    <asp:HiddenField ID="hfldPrjManage" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    现场勘察日期
                </td>
                <td>
                    <asp:TextBox ID="txtTenderProspect" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    阅知人
                </td>
                <td>
                    <span class="spanSelect" style="width: 99%;">
                        <asp:TextBox ID="txtTenderReadOne" Style="width: 61%; height: 15px;
                            border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img1"
                            onclick="selectUser('hfldTenderReadOne','txtTenderReadOne');" />
                    </span>
                    <asp:HiddenField ID="hfldTenderReadOne" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    现场费内容
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTenderCostContent" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    标书内容
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTenderContent" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTenderRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    相关附件
                </td>
                <td colspan="3">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="Div1" class="divFooter2" runat="server">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="CancelClick('BidManage.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
