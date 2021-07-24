<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettleQuestionEdit.aspx.cs" Inherits="EPC_ProjectCost_PhotosCheckIn_SettleQuestionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server"><title>拍照监督</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self" />
    <link href="/StyleCss/PM4.css" rel="stylesheet" type="text/css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../../Web_Client/TreeNew.js"></script>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript">
        window.name = "win";
        //选择人员
        function selectUser() {
            var url = "/Common/DivSelectAllUser.aspx?Method=returnUser";
            document.getElementById("divFram").title = "选择人员";
            document.getElementById("ifFram").src = url;
            selectnEvent('divFram');
        }
        //选择人员返回值
        function returnUser(id, name) {
            document.getElementById('txtToPerson').value = name;
            document.getElementById('hdfusercode').value = id;

        }
        function validate() {
            myTdHeight(1, 1, 1, 'dvModulesBox', 0);
        }
        function validate2() {
            myTdHeight(1, 1, 1, 'Div2', 270);
        }
        //上传图片
        function upLoadFile(InfoGuid) {

            var url = "UpLoadPhoto.aspx?p=" + InfoGuid + "&ty=1";
            var re = window.showModalDialog(url, window, "dialogHeight:280px;dialogWidth:400px;center:1;help:0;status:0;");
            if (re) {
                ajaxPhotostable();

            }
        }
    </script>
    <script type="text/javascript">
        var InfoGuid = "<%=InfoGuid %>";
        var flag = "<%=flag %>"; //是否是验证
        $(document).ready(function () {
            validate();
            //validate2();

            ajaxPhotostable();
            ajaxSettleTable();
        });
       
    </script>
    <script type="text/javascript" language="javascript">
        var IntendanceGuid = "<%=IntendanceGuid %>";

        function ajaxSettleTable() {
            PageMethods.GetSettleItem(IntendanceGuid, onSucceed, function (result) { alert("生成Json数据时错误!") });
            function onSucceed(result) {
                json = eval(result);
                var count = 1;
                for (var index in json) {
                    var xm = json[index].SettleYhdm;
                    var SettleToPersonxm = json[index].SettleToPerson;
                    var SettleDate = json[index].Sdate;
                    var QuestionExplain = json[index].QuestionExplain;
                    var SettleExplain = json[index].SettleExplain;
                    var NoteId = json[index].NoteId;
                    var ToCause = json[index].ToCause;
                    //                    var strTable = "<br/> <span id='lblSettleTitle'" + NoteId + ">" + xm + "解决问题的回复时间 " + SettleDate + "</span>";
                    var strTable = "<br/> <div style='text-align: center;'>";
                    strTable += "<table style='width: 80%;' class='table-form'><tr>" +
                             "<td style='width:10%;' class='td-label'>问题提问人</td><td style=\"width:40%;\"> <span id='lblQuestionperson'" + count + ">" + xm + "</span></td></tr><tr>" +
                             "<td style='width: 10%;' class='td-label'>问题描述</td><td style=\"width: 40%;\"> <span id='lblQuestion'" + count + ">" + QuestionExplain + "</span></td></tr><tr>" +
                             "<td class='td-label'>现<br/>场<br/>照<br/>片</td><td><div id='dvModulesBox'" + count + " class='gridBox' style='height: 100px;'>" +
                             "<table id='tb" + NoteId + "'border='1'  class='grid' style='width: 100%; border-collapse: collapse;'>" +
                             "<tr class=\"td-label\"><td nowrap=\"nowrap\" width='40'>序号 </td><td nowrap=\"nowrap\" width='80'>照片编号</td>" +
                             "<td nowrap=\"nowrap\">照片说明</td><td nowrap=\"nowrap\" width='80'> 照片文件 </td></tr></table></div></td></tr>" +
                              "<td style='width:10%;' class='td-label'>问题转派人</td><td style=\"width:40%;\"> <span id='lblQuestionSettleToPersonxm'" + count + ">" + SettleToPersonxm + "</span></td></tr>" +
                              "<tr><td style='width: 10%;' class='td-label'>转派原因</td><td style=\"width: 40%;\"> <span id='lblQuestionToCause'" + count + ">" + ToCause + "</span></td></tr>" +
                              "<tr><td style='width: 10%;' class='td-label'>转派时间</td><td style=\"width: 40%;\"> <span id='lblQuestionSettleDate'" + count + ">" + SettleDate + "</span></td></tr>" +
                             "<tr><td style='width: 10%;' class='td-label'>解决情况</td><td style=\"width: 40%;\"> <span id='lblSettle'" + count + ">" + SettleExplain + "</span></td></tr>" +

                    //                             "<td class='td-label'>现<br/>场<br/>照<br/>片</td><td><div id='dvSettle'" + count + " class='gridBox' style='height: 100px;'>" +
                    //                             "<table id='tbSettle" + NoteId + "' class='grid'  border='1' style='width: 100%; border-collapse: collapse;'>" +
                    //                             "<tr class=\"td-label\"><td nowrap=\"nowrap\" width='40'>序号 </td><td nowrap=\"nowrap\" width='80'>照片编号</td>" +
                    //                             "<td nowrap=\"nowrap\">照片说明</td><td nowrap=\"nowrap\" width='80'> 照片文件 </td></tr></table></div></td></tr>" +
                             "</table></div>";

                    $("#DivSettle").append(strTable);
                    //动态生成回复列表中的照片列表
                    PageMethods.GetPhotoItem(NoteId, onSucceed1, function (result) { alert("生成Json数据时错误!") });
                    function onSucceed1(result) {

                        json = eval(result);
                        var AskCount = 1;
                        var settleCount = 1;
                        for (var index in json) {
                            var imgPath = json[index].ThumbnaImgPath;
                            var imgName = json[index].ThumbnaName;
                            var photoNumber = json[index].PhotoNumber;
                            var photoExplain = json[index].PhotoExplain;
                            var noteId = json[index].NoteId;
                            var PhotoType = json[index].PhotoType;
                            var infoGuid = json[index].InfoGuid;
                            if (PhotoType == 0) {
                                var img = "<img style=\"cursor: hand;\" class='thumbnai' title='查看照片' onclick=\"openPhotos(" + (AskCount - 1) + "," + PhotoType + ",'" + infoGuid + "');\"  src='" + imgPath + imgName + "'/>";
                            } else {
                                var img = "<img style=\"cursor: hand;\" class='thumbnai' title='查看照片' onclick=\"openPhotos(" + (settleCount - 1) + "," + PhotoType + ",'" + infoGuid + "');\"  src='" + imgPath + imgName + "'/>";
                            }
                            var strTr = "";
                            if (PhotoType == 0) {
                                strTr = "<tr><td  align='center' >" + AskCount + "</td>";
                            } else {
                                strTr = "<tr><td  align='center' >" + settleCount + "</td>";
                            }

                            strTr += "<td   align='center' >" + photoNumber + "</td>" +
                                  "<td   align='center' >" + photoExplain + "</td>" +
                                  "<td   align='center' >" + img + "</td>";
                            strTr += "</tr>";

                            if (PhotoType == 0) {
                                $("#DivSettle table[id=tb" + infoGuid + "]").append(strTr);
                                AskCount++;
                            }
                            else {
                                $("#DivSettle table[id=tbSettle" + infoGuid + "]").append(strTr);
                                settleCount++;
                            }
                        }

                    }
                    count++;
                }

            }
        } 
    </script>
    <script type="text/javascript" language="javascript">
        var flag = "<%=flag %>";
        function ajaxPhotostable() {

            PageMethods.GetPhotoItem(InfoGuid, onSucceed, function (result) { alert("生成Json数据时错误!") });
            function onSucceed(result) {

                json = eval(result);
                $("#TbModules tr:gt(0)").remove();
                //$("#TbSettle tr:gt(0)").remove();
                var jsonCount = 1;
                var settleCount = 1;
                //                if (flag == 0) {
                //                    document.getElementById("tdUpload").style.display = "block";
                //                }
                for (var index in json) {
                    var imgPath = json[index].ThumbnaImgPath;
                    var imgName = json[index].ThumbnaName;
                    var photoNumber = json[index].PhotoNumber;
                    var photoExplain = json[index].PhotoExplain;
                    var noteId = json[index].NoteId;
                    var PhotoType = json[index].PhotoType;
                    var infoGuid = json[index].InfoGuid;
                    if (PhotoType == 0) {
                        var img = "<img style=\"cursor: hand;\" class='thumbnai' title='查看照片' onclick=\"openPhotos(" + (jsonCount - 1) + "," + PhotoType + ",'" + InfoGuid + "');\"  src='" + imgPath + imgName + "'/>";
                    } else {
                        var img = "<img style=\"cursor: hand;\" class='thumbnai' title='查看照片' onclick=\"openPhotos(" + (settleCount - 1) + "," + PhotoType + ",'" + InfoGuid + "');\"  src='" + imgPath + imgName + "'/>";
                    }
                    var link = "<a  onclick=\"return confirm('确定要删除吗？');\" id=\"" + noteId + "\"" +
                       " href=\"javascript:delData(this,'" + noteId + "')\">删除</a>";

                    var strTr = "";
                    if (PhotoType == 0) {
                        strTr = "<tr><td  align='center' >" + jsonCount + "</td>";
                    } else {
                        strTr = "<tr><td  align='center' >" + settleCount + "</td>";
                    }

                    strTr += "<td   align='center' >" + photoNumber + "</td>" +
                                  "<td   align='center' >" + photoExplain + "</td>" +
                                  "<td   align='center' >" + img + "</td>";
                    //                    if (flag == 0) {
                    //                        document.getElementById("tdUpload").style.display = "block";
                    //                    }
                    if (PhotoType == 0) {
                        strTr += "</tr>";
                        $("#TbModules").append(strTr);
                        jsonCount++;
                    }
                    else {
                        if (flag == 0) {
                            strTr += "<td  align='center'>" + link + "</td></tr>";
                        } else {
                            strTr += "<td   style='display:none;width:0'></td></tr>";
                        }
                        //$("#TbSettle").append(strTr);
                        $("#TbModules").append(strTr);
                        settleCount++;
                    }



                }
                if (flag == 0) {
                    //上传行
                    var link = " <a id=\"link" + settleCount + "\" href=\"javascript:upLoadFile('" + InfoGuid + "');\">上传</a>";
                    var str = "<tr style=\" height:30px\"><td style=\" width:50px;\"  nowrap='nowrap' align='center'>" + settleCount + "</td>" +
                                  "<td  align='center'> </td>" +
                                  "<td   align='center'></td>" +
                                  "<td   align='center'></td>" +
                                  "<td  align='center'>" + link + "</td></tr>";

                    //$("#TbSettle").append(str);                    
                }

            }


        }
        var w = screen.availWidth - 10;
        var h = screen.availHeight - 50;
        var p = "top=0,left=0,width=" + w + "px,height=" + h + "px,toolbar=no,status=yes,scrollbars=yes,resizable=yes";

        function openPhotos(index, type, info) {

            url = "PhotosList.aspx?IntendanceGuid=" + info + "&index=" + index + "&type=" + type;
            window.open(url, '', p);
        }
        function delData(obj, noteId) {
            PageMethods.DelPhotosItem(noteId, onSucceed, function (result) { alert("生成Json数据时错误!") });

            function onSucceed(result) {
                ajaxPhotostable();

            }
        }

        //关闭
        function btnCancel_onclick() {
            top.ui.closeTab();
        }
    </script>
</head>
<body>
    <form id="form1" method="post" target="win" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdnUserCode" runat="server" />
    <div id="divContent2" class="divContent2">
        <table id="Table1" cellspacing="0" style="width: 100%; height: 10%;" cellpadding="0"
            align="center" border="0" class="table-none">
            <tr>
                <td class="td-title">
                    <asp:Label ID="lblTitle" Text="现场问题登记" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 100px;">
                    &nbsp;&nbsp; 项目名称:<asp:Label ID="lblPrjName" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="height: 89%; text-align: center;">
            <table class="table-form" style="width: 80%; height: 10%;">
                <tr>
                    <td style="width: 10%;" class=" td-label">
                        问题标题
                    </td>
                    <td style="width: 40%;">
                        <asp:Label ID="txtQuestionTitle" Text="" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%;" class=" td-label">
                        问题类别
                    </td>
                    <td style="width: 40%;">
                        <asp:Label ID="lbltype" Text="" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;" class=" td-label">
                        登记时间
                    </td>
                    <td style="width: 40%;">
                        <asp:Label ID="txtDate" Text="" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%;" class=" td-label">
                        指派给
                    </td>
                    <td style="width: 40%;">
                        <asp:Label ID="txtPerson" Text="" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="padding-left: 130px; padding-top: 10px; text-align: left; height: 14px">
                <asp:Label ID="lbl1" Text="" Visible="false" Style="font-size: 14px" runat="server"></asp:Label>
            </div>
            <table style="width: 80%;" class="table-form">
                <tr>
                    <td style="width: 10%;" class=" td-label">
                        问题描述
                    </td>
                    <td style="width: 40%;">
                        <asp:Label ID="txtQuestion" Text="" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;" class=" td-label">
                        转派
                    </td>
                    <td style="width: 40%;">
                        <input type="text" contenteditable="false" id="txtToPerson" style="background-color: #ffffc0;
                            width: 95%; height: 20px" runat="server" />

                        <img id="Img3" style="cursor: hand" onclick="selectUser();" src="/images/contact.gif" align="absmiddle" runat="server" />

                        <input id="ManagerCode" type="hidden" name="ManagerCode" runat="server" />

                        <asp:Label ID="lblToPerson" Text="" Visible="false" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdfusercode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;" class=" td-label">
                        转派原因
                    </td>
                    <td style="width: 40%;">
                        <asp:TextBox ID="txtToCause" CssClass="textarea-input" BackColor="#FFFFCC" TextMode="MultiLine" Width="99%" Height="20px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblToCause" Text="" Visible="false" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;" class=" td-label">
                        解决情况
                    </td>
                    <td style="width: 40%;">
                        <asp:TextBox ID="txtSettleExplain" CssClass="textarea-input" BackColor="#FFFFCC" TextMode="MultiLine" Width="99%" Height="20px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblSettleExplain" Text="" Visible="false" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        质<br />
                        量<br />
                        照<br />
                        片
                    </td>
                    <td valign="top">
                        <div id="dvModulesBox" class="gridBox" style="height: 100px;">
                            <table id="TbModules" class="grid" border="1" style="width: 100%;
                                border-collapse: collapse;" runat="server"><tr class="td-label" runat="server"><td nowrap="true" align="center" width="40" runat="server">
                                        序号
                                    </td><td nowrap="true" align="center" width="80" runat="server">
                                        照片编号
                                    </td><td nowrap="true" align="center" runat="server">
                                        照片说明
                                    </td><td nowrap="true" align="center" width="80" runat="server">
                                        照片文件
                                    </td></tr></table>
                        </div>
                    </td>
                </tr>
                
            </table>
        </div>
        <div id="divFram" title="选择人员" style="display: none">
            <iframe id="ifFram" frameborder="0" width="100%" height="100%"></iframe>
        </div>
        <div id="DivSettle" style="height: 100%;" runat="server">
        </div>
    </div>
    <div id="divFooter" class="divFooter2" style="margin-left: 0px; margin-bottom: 0;
        padding-bottom: 0; vertical-align: bottom;">
        <table class="tableFooter2" style="width: 100%;">
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" CssClass="button-normal" Text="保存" Width="63px" OnClick="btnSave_Click" runat="server" />
                    <asp:Button ID="btnSettled" CssClass="button-normal" Text="已解决" Width="63px" OnClick="btnSettled_Click" runat="server" />
                    <asp:Button ID="btnSettling" CssClass="button-normal" Text="解决中" Width="63px" OnClick="btnSettling_Click" runat="server" />
                    <input type="button" class="button-normal" value="取 消" onclick="return btnCancel_onclick()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
