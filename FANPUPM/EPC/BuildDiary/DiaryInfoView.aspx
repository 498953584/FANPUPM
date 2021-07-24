<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiaryInfoView.aspx.cs" Inherits="OPM_EPCM_BuildDiary_DiaryInfoView" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>施工日志信息</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyTable', 'gvDiary_MX');
            getImgPath();
        });
        var imgArr = new Array();
        var imgLength = 0;
        function showImg(imgUrl, imgIndex) {
            var img3 = parseInt($('#img3').attr('alt'));
            if (imgIndex == 1) {
                imgIndex = img3 - 1;
            }
            else {

                if (imgIndex == 2) {
                    imgIndex = img3;
                }
                else {
                    if (imgIndex == 3) {
                        imgIndex = 4;
                    }
                }
            }
            var url = '/EPC/BuildDiary/ShowImg.aspx?imgUrl=' + imgUrl + '&imgIndex=' + imgIndex + '&imgPath=' + $('#hfldImgPath').val();
            top.ui.openWin({ title: '现场照片', url: url, width: 450, height: 450 });
        }
        function getImgPath() {
            var str = $('#hfldImgPath').val();
            if (str.length <= 2) {
                $('#photo').hide();
            }
            JSON.parse(str, function (k, v) {
                if (v != ',') {
                    imgArr.push(v);
                }
            });
            if (imgArr.length >= 3) {
                imgLength = imgArr.length - 1;
            }
            else {
                imgLength = imgArr.length;
            }
            if (imgLength > 0) {
                if (imgLength == 1) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').hide();
                    $('#img3').hide();
                    $('#img3').attr('alt', 1);
                }
                if (imgLength == 2) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').attr('src', imgArr[1]);
                    $('#img3').hide();
                    $('#img3').attr('alt', 2);
                }
                if (imgLength >= 3) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').attr('src', imgArr[1]);
                    $('#img3').attr('src', imgArr[2]);
                    $('#img3').attr('alt', 3);
                }
            }
        }
        //向后翻
        function next() {
            var imgIndex = parseInt($('#img3').attr('alt'));
            if (imgLength <= 3 || imgIndex < 3) {
                return;
            }
            else {
                if (imgLength == imgIndex) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').attr('src', imgArr[1]);
                    $('#img3').attr('src', imgArr[2]);
                    $('#img3').attr('alt', 3);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
                if (imgLength - imgIndex == 1) {
                    $('#img1').attr('src', imgArr[imgIndex]);
                    $('#img2').hide();
                    $('#img3').hide();
                    $('#img3').attr('alt', ++imgIndex);
                    return false;
                }
                if (imgLength - imgIndex == 2) {
                    $('#img1').attr('src', imgArr[imgIndex]);
                    $('#img2').attr('src', imgArr[++imgIndex]);
                    $('#img3').hide();
                    $('#img3').attr('alt', ++imgIndex);
                    $('#img2').show();
                    return false;
                }
                if (imgLength - imgIndex >= 3) {
                    $('#img1').attr('src', imgArr[imgIndex]);
                    $('#img2').attr('src', imgArr[++imgIndex]);
                    $('#img3').attr('src', imgArr[++imgIndex]);
                    $('#img3').attr('alt', ++imgIndex);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
            }
        }
        //向前翻
        function pre() {
            var img1Index;
            if (imgLength <= 3) {
                return;
            }
            else {
                var imgIndex = parseInt($('#img3').attr('alt'));
                if (imgIndex == 3) {
                    img1Index = imgLength - 1;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').hide();
                    $('#img3').hide();
                    return;
                }
                if (imgLength == imgIndex) {
                    img1Index = imgIndex - 5;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img2').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
                if (imgLength - imgIndex == 1) {
                    img1Index = imgIndex;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').hide();
                    $('#img3').hide();
                    return;
                }
                if (imgLength - imgIndex == 2) {
                    img1Index = imgIndex;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img2').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').show();
                    $('#img3').hide();
                    return false;
                }
                if (imgLength - imgIndex >= 3) {
                    img1Index = imgLength - 4;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img2').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
            }
        }    
    </script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; border-collapse: collapse; width: 700px;">
        <tr>
            <td class="divHeader">
                日志信息
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
        <tr id="tr_tb" runat="server"><td style="vertical-align: top" runat="server">
                <table cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            日志编号
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblSN" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblPrjName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="descTd">
                            是否关联工程量清单
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblSfgl" runat="server"></asp:Label>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            施工人员
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblJbr" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            记录人员
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblRecord" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="workNum" runat="server"><td class="descTd" runat="server">
                            水电人数
                        </td><td class="elemTd" colspan="3" runat="server">
                            <asp:Label ID="lblWaterElec" runat="server"></asp:Label>
                        </td><td class="descTd" runat="server">
                            泥工人数
                        </td><td class="elemTd" colspan="3" runat="server">
                            <asp:Label ID="lblMason" runat="server"></asp:Label>
                        </td></tr>
                    <tr id="workNum1" runat="server"><td class="descTd" runat="server">
                            油漆人数
                        </td><td class="elemTd" colspan="3" runat="server">
                            <asp:Label ID="lblPainter" runat="server"></asp:Label>
                        </td><td class="descTd" runat="server">
                            木工人数
                        </td><td class="elemTd" colspan="3" runat="server">
                            <asp:Label ID="lblCarpentry" runat="server"></asp:Label>
                        </td></tr>
                    <tr>
                        <td class="descTd">
                            上午天气
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblAmWeather" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            下午天气
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblPmWeather" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            发生日期
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblFsrq" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            编制日期
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblBzrq" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            2时温度
                        </td>
                        <td style="width: 12%">
                            <asp:Label ID="lbl2Cemperature" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            8时温度
                        </td>
                        <td style="width: 12%">
                            <asp:Label ID="lbl8Cemperature" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            14时温度
                        </td>
                        <td style="width: 12%">
                            <asp:Label ID="lbl14Cemperature" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            20时温度
                        </td>
                        <td style="width: 12%">
                            <asp:Label ID="lbl20Cemperature" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="photo" style="height: 150px;">
                        <td class="descTd" valign="middle" align="center">
                            现场照片
                        </td>
                        <td colspan="7" style="vertical-align: middle">
                            <div style="width: 35px; float: left; padding-top: 60px;">
                                <input class="noprint" type="button" value="pre" onclick="pre();" style="width: 35px" /></div>
                            <div style="width: 510px; height: 140px; float: left; padding-bottom: 5px; overflow: hidden;">
                                <table style="border: none; border-width: 0px; text-align: right;">
                                    <tr>
                                        <td style="border: none; border-width: 0px; width: 170px;">
                                            <img alt="" id="img1" style="height: 140px; width: 170px;" src="" onclick="showImg(this.src,1)" />
                                        </td>
                                        <td style="border: none; border-width: 0px; width: 170px;">
                                            <img alt="" id="img2" style="height: 140px; width: 170px;" src="" onclick="showImg(this.src,2)" />
                                        </td>
                                        <td style="border: none; border-width: 0px; width: 170px;">
                                            <img alt="" id="img3" style="height: 140px; width: 170px;" src="" onclick="showImg(this.src,3)" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            &nbsp
                            <div style="width: 35px; float: right; padding-top: 60px;">
                                <input class="noprint" type="button" value="next" onclick="next();" style="width: 35px" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预检情况
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblPreview" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            验收情况
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblCheck" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            设计变更
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblDesignChange" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            材料进场
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblMaterials" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            技术交底
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblTechnology" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            材料送检
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblMaterialSubmission" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            资料交接
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblDataTransfer" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            会议情况
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblExternalMeet" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            上级检查
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblSuperiorCheck" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            安全处理
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblSafeHand" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            质量机械及现场情况
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblShyj" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            其它情况
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblOtherSituation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td class="elemTd" colspan="7">
                            <asp:Label ID="lblRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            创建人员
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblAddUser" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            创建时间
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblAddTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td></tr>
        <tr>
            <td>
                <div style="margin-top: 5px">
                    <asp:GridView ID="gvDiary_MX" AutoGenerateColumns="false" CssClass="gvdata" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
                            <table id="emptyTable" style="background-color: Red;" class="tab" width="100%">
                                <tr class="header">
                                    <td style="width: 25px" align="center">
                                        序号
                                    </td>
                                    <td style="width: 160px" align="center">
                                        任务代码
                                    </td>
                                    <td align="center">
                                        任务名称
                                    </td>
                                    <td align="center">
                                        工作班组
                                    </td>
                                    <td align="center">
                                        工作人数
                                    </td>
                                    <td align="center">
                                        施工部位
                                    </td>
                                    <td align="center">
                                        进度情况
                                    </td>
                                    <td align="center">
                                        备注
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                    
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务代码" HeaderStyle-Width="100px"><ItemTemplate>
                                    <asp:Label ID="lblCode" Text='<%# System.Convert.ToString(Eval("TaskCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务名称" HeaderStyle-Width="30px"><ItemTemplate>
                                    
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工作班组" HeaderStyle-Width="30px"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工作人数" HeaderStyle-Width="60px"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工部位" HeaderStyle-Width="100px"><ItemTemplate>
                                    
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="进度情况" HeaderStyle-Width="60px"><ItemTemplate>
                                    
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="30px"><ItemTemplate>
                                    
                                    
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
        <tr id="trAudit" runat="server"><td runat="server">
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="110" BusiClass="001" runat="server" />
            </td></tr>
    </table>
    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="图片" FileType="*.jpg;*.gif;*.png" Class="BuildDiaryPhoto" Visible="false" runat="server" />
    
    <asp:HiddenField ID="hfldImgPath" Value="" runat="server" />
    </form>
</body>
</html>
