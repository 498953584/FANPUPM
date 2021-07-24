<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBuildDiary.aspx.cs" Inherits="OPM_BuildDiary_AddBuildDiary" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>添加施工日志</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var isShow = $('#hfldShowWorkNum').val();
            if (isShow == 'false') {
                $('#workNum').hide();
                $('#workNum1').hide();
            }
            else {
                $('#workNum').show();
                $('#workNum1').show();
            }
        });
        //选择项目
        function openProjPicker() {
            document.getElementById("divFram").title = "选择项目";
            document.getElementById("ifFram").src = "/Common/DivSelectProject.aspx?Method=returnPrj";
            selectnEvent('divFram');
        }
        //选择项目返回值
        function returnPrj(id, name, PrjManager) {
            document.getElementById('hdnPrjID').value = id;
            document.getElementById('txtPrjName').value = name;
            document.getElementById('txtPrjName').accessKey = '1';
        }

        //取消
        function btnCancel_onclick() {
            top.ui.closeTab();
        }

        //表单验证
        function valForm() {
            if (document.getElementById("txtSN").value == "") {
                alert("系统提示：\n日志编号必须输入！");
                document.getElementById("txtSN").focus();
                return false;
            }
            else if (document.getElementById("txtPrjName").value == "") {
                alert("系统提示：\n项目名称必须输入！");
                document.getElementById("txtPrjName").focus();
                return false;
            }
            return true;
        }

        // 添加人数
        function addWorkNum() {
            if ($('#chkAddNumber').attr('checked')) {
                $('#workNum').show();
                $('#workNum1').show();
            }
            else {
                $('#workNum').hide();
                $('#workNum1').hide();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0"
            width="80%">
            <tr>
                <td class="word" align="right">
                    日志编号
                </td>
                <td class="txt mustInput">
                    <input id="txtSN" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
                <td class="word" align="right">
                    项目
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtPrjName" Style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img src="../../../images/icon.bmp" style="float: right;" alt="选择项目" id="imgPrj" onclick="openProjPicker();" runat="server" />

                    </span>
                    <asp:HiddenField ID="hdnPrjID" runat="server" />
                </td>
            </tr>
            <tr style="display: none;">
                <td class="word" align="right">
                    是否关联工程量清单
                </td>
                <td>
                    <asp:DropDownList ID="ddlSfgl" Width="100%" runat="server"><asp:ListItem Value="0" Text="否" /><asp:ListItem Value="1" Selected="true" Text="是" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    施工人员
                </td>
                <td class="word">
                    <input id="txtJbr" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
                
                <td class="word" align="right">
                    记录人员
                </td>
                <td class="word">
                    <input id="txtRecord" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    添加工作人数
                </td>
                <td>
                    <input type="checkbox" id="chkAddNumber" onclick="addWorkNum();" />
                </td>
            </tr>
            <tr id="workNum">
                <td class="word" align="right">
                    水电人数
                </td>
                <td class="word">
                    <input id="txtWaterElec" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
                <td class="word" align="right">
                    泥工人数
                </td>
                <td class="word">
                    <input id="txtMason" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
            </tr>
            <tr id="workNum1">
                <td class="word" align="right">
                    油漆人数
                </td>
                <td class="word">
                    <input id="txtPainter" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
                <td class="word" align="right">
                    木工人数
                </td>
                <td class="word">
                    <input id="txtCarpentry" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    上午天气
                </td>
                <td class="word">
                    <input id="txtAmWeather" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
                <td class="word">
                    下午天气
                </td>
                <td class="word">
                    <input id="txtPmWeather" type="text" style="height: 15px; width: 100%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    发生日期
                </td>
                <td class="txt">
                    <JWControl:DateBox ID="txtFsrq" runat="server"></JWControl:DateBox>
                </td>
                <td class="word">
                    编制日期
                </td>
                <td>
                    <JWControl:DateBox ID="txtBzrq" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    2时温度
                </td>
                <td>
                    <input id="txt2Cemperature" type="text" style="height: 15px; width: 34%;" runat="server" />

                    8时温度
                    <input id="txt8Cemperature" type="text" style="height: 15px; width: 34%;" runat="server" />

                </td>
                <td class="word">
                    14时温度
                </td>
                <td>
                    <input id="txt14Cemperature" type="text" style="height: 15px; width: 34%;" runat="server" />

                    20时温度
                    <input id="txt20Cemperature" type="text" style="height: 15px; width: 34%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    现场图片
                </td>
                <td class="txt" colspan="3">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="图片" FileType="*.jpg;*.gif;*.png" Class="BuildDiaryPhoto" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    预检情况
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtPreview" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    验收情况
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtCheck" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    设计变更
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtDesignChange" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    材料进场
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtMaterials" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    技术交底
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtTechnology" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    材料送检
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtMaterialSubmission" name="S1" style="width: 100%;
                        height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    资料交接
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtDataTransfer" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    会议情况
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtExternalMeet" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    上级检查
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtSuperiorCheck" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    安全处理
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtSafeHand" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    质量机械及现场情况
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtShyj" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    其它情况
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtOtherSituation" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    备注
                </td>
                <td class="txt" colspan="3">
                    <textarea id="txtRemark" name="S1" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word" align="right">
                    创建人员
                </td>
                <td class="txt mustInput">
                    <input id="txtAddUser" type="text" readonly="readonly" style="height: 15px;
                        width: 100%;" runat="server" />

                </td>
                <td class="word" align="right">
                    创建时间
                </td>
                <td class="txt mustInput">
                    <JWControl:DateBox ID="txtAddTime" runat="server"></JWControl:DateBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="return btnCancel_onclick()" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldShowWorkNum" Value="false" runat="server" />
    </form>
</body>
</html>
