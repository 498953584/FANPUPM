<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PhotosCheckInEdit.aspx.cs" Inherits="EPC_ProjectCost_PhotosCheckIn_PhotosCheckInEdit" %>

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
            document.getElementById("divUserCode").title = "选择人员";
            document.getElementById("ifUserCode").src = url;
            selectnEvent('divUserCode');
        }
        //选择人员返回值
        function returnUser(id, name) {
            document.getElementById('hdfusercode').value = id;
            document.getElementById('txtPerson').value = name;

        }

        function validate() {
            myTdHeight(1, 1, 1, 'dvModulesBox', 150);
        }
        //上传图片
        function upLoadFile(InfoGuid) {

            var url = "UpLoadPhoto.aspx?p=" + InfoGuid + "&ty=0";
            var flag = window.showModalDialog(url, window, "dialogHeight:280px;dialogWidth:400px;center:1;help:0;status:0;");
            if (flag) {
                ajaxPhotostable();

            }
        }
    </script>
    <script type="text/javascript">
        var InfoGuid = "<%=InfoGuid %>";
        var op = "<%=opType %>";
        $(document).ready(function () {

            validate();
            if (op == "v" || op == "upd") {
                if (op == "v") {
                    InfoGuid = "00000000-0000-0000-0000-000000000000";
                }
                ajaxSettleTable();
            }
            ajaxPhotostable();
        });
       
    </script>
    <script type="text/javascript" language="javascript">
        var IntendanceGuid = "<%=IntendanceGuid %>";

        function ajaxSettleTable() {
            PageMethods.GetSettleItem(IntendanceGuid, function (result) { }); //alert("生成Json数据时错误1!")
            function onSucceed(result) {
                jsons = eval(result);
                var count = 1;
                for (var index in jsons) {
                    var xm = jsons[index].SettleYhdm;
                    var SettleDate = jsons[index].Sdate;
                    var QuestionExplain = jsons[index].QuestionExplain;
                    var SettleExplain = jsons[index].SettleExplain;
                    var NoteId = jsons[index].NoteId;

                    var strTable = "<br/> <span id='lblSettleTitle'" + index + ">" + xm + "解决问题的回复时间 " + SettleDate + "</span>";
                    strTable += "<table style='width: 100%;' class='table-form'><tr>" +
                             "<td style='width: 10%;' class='td-label'>问题描述</td><td style=\"width: 40%;\"> <span id='lblQuestion'" + count + ">" + QuestionExplain + "</span></td>" +
                             "<td class='td-label'>现<br/>场<br/>照<br/>片</td><td><div id='dvModulesBox'" + count + " class='gridBox' style='height: 100px;'>" +
                             "<table id='tb" + NoteId + "' class=\"grid\"  border='1' style='width: 100%; border-collapse: collapse;'>" +
                             "<tr class='td-label'><td nowrap=\"nowrap\" width='40'>序号 </td><td nowrap=\"nowrap\" width='80'>照片编号</td>" +
                             "<td nowrap=\"nowrap\">照片说明</td><td nowrap=\"nowrap\" width='80'> 照片文件 </td></tr></table></div></td></tr>" +
                             "<tr><td style='width: 10%;' class='td-label'>解决情况</td><td style=\"width: 40%;\"> <span id='lblSettle'" + count + ">" + SettleExplain + "</span></td>" +
                             "<td class='td-label'>现<br/>场<br/>照<br/>片</td><td><div id='dvSettle'" + count + " class='gridBox' style='height: 100px;'>" +
                             "<table id='tbSettle" + NoteId + "' class='grid'  border='1' style='width: 100%; border-collapse: collapse;'>" +
                             "<tr class='td-label'><td nowrap=\"nowrap\" width='40'>序号 </td><td nowrap=\"nowrap\" width='80'>照片编号</td>" +
                             "<td nowrap=\"nowrap\">照片说明</td><td nowrap=\"nowrap\" width='80'> 照片文件 </td></tr></table></div></td></tr>" +
                             "</table>";

                    $("#DivSettle").append(strTable);

                    //动态生成回复列表中的照片列表

                    PageMethods.GetPhotoItem(NoteId, function (result) { alert("生成Json数据时错误2!") });
                    function onSucceed(result) {

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



        function ajaxPhotostable() {
            //            debugger;
            PageMethods.GetPhotoItem(InfoGuid, onSucceed, function (result) { alert("生成Json数据时错误3!") });
            function onSucceed(result) {

                json = eval(result);
                $("#TbModules tr:gt(0)").remove();
                var jsc = 1;
                for (var index in json) {
                    var imgPath = json[index].ThumbnaImgPath;
                    var imgName = json[index].ThumbnaName;
                    var photoNumber = json[index].PhotoNumber;
                    var photoExplain = json[index].PhotoExplain;
                    var noteId = json[index].NoteId;
                    var infoGuid = json[index].InfoGuid;
                    if (op != "add") {
                        var img = "<img style=\"cursor: hand;\" class='thumbnai' title='查看照片' onclick=\"openPhotos(" + (jsc - 1) + ",0,'" + infoGuid + "');\"  src='" + imgPath + imgName + "'/>";
                    } else {
                        var img = "<img style=\"cursor: hand;\" class='thumbnai' title='查看照片' onclick=\"openPhotos(" + (jsc - 1) + ",0,'" + InfoGuid + "');\" src='" + imgPath + imgName + "' />";
                        //var img = "<img class='thumbnai' src='" + imgPath + imgName + "' />";
                    }
                    var link = "<a  onclick=\"return confirm('确定要删除吗？');\" id=\"" + noteId + "\"" +
                       " href=\"javascript:delData(this,'" + noteId + "')\">删除</a>";

                    if (op != "view") {
                        var strTr = "<tr><td align='center' >" + jsc + "</td>" +
                                  "<td  align='center'>" + photoNumber + "</td>" +
                                  "<td   align='center' >" + photoExplain + "</td>" +
                                  "<td   align='center'>" + img + "</td>" +
                                  "<td align='center' >" + link + "</td></tr>";
                    } else {
                        var strTr = "<tr><td  align='center' >" + jsc + "</td>" +
                                  "<td   align='center' >" + photoNumber + "</td>" +
                                  "<td   align='center' >" + photoExplain + "</td>" +
                                  "<td   align='center' >" + img + "</td>" +
                                  "<td   style='display:none' align='center'></td></tr>";
                    }
                    $("#TbModules").append(strTr);
                    var height = 80 * (jsc + 1);
                    $("#dvModulesBox").height(height);
                    jsc++;
                }

                if (op != "view") {

                    var link = " <a id=\"link" + jsc + "\" href=\"javascript:upLoadFile('" + InfoGuid + "');\">上传</a>";
                    var str = "<tr style=\" height:30px\"><td style=\" width:50px;\"  nowrap='nowrap' align='center'>" + jsc + "</td>" +
                                  "<td  align='center'> </td>" +
                                  "<td   align='center'></td>" +
                                  "<td   align='center'></td>" +
                                  "<td   style=\" width:50px;\" align='center'>" + link + "</td></tr>";
                }
                $("#TbModules").append(str);


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
            PageMethods.DelPhotosItem(noteId, onSucceed, function (result) { alert("生成Json数据时错误4!") });

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
    <table id="Table1" cellspacing="0" style="width: 100%;" cellpadding="0" border="0"
        class="table-none">
        <tr>
            <td class="td-title">
                <asp:Label ID="lblTitle" Text="现场问题登记" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="padding-left: 100px">
            <td>
                &nbsp;&nbsp; 项目名称:<asp:Label ID="lblPrjName" Text="" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="text-align: center">
        <table style="width: 80%;" class="table-form">
            <tr>
                <td style="width: 10%; height: 30px" class=" td-label">
                    问题标题
                </td>
                <td style="width: 18%;">
                    <asp:TextBox ID="txtQuestionTitle" CssClass="text-input" BackColor="#FFFFCC" Width="95%" runat="server"></asp:TextBox>
                    <asp:Label ID="lblQuestionTitle" Text="" Style="display: none" runat="server"></asp:Label>
                </td>
                <td style="width: 10%;" class=" td-label">
                    问题类别
                </td>
                <td style="width: 18%;">
                    <asp:DropDownList ID="ddlType" DataTextField="CodeName" DataValueField="CodeID" Width="95%" runat="server"></asp:DropDownList>
                    <asp:Label ID="lbltype" Text="" Visible="false" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class=" td-label" style="height: 30px">
                    登记时间
                </td>
                <td>
                    <JWControl:DateBox ID="txtDate" CssClass="text-input" Width="95%" runat="server"></JWControl:DateBox>
                    <asp:Label ID="lblDate" Text="" Style="display: none" runat="server"></asp:Label>
                </td>
                <td style="width: 10%;" class=" td-label">
                    指派给
                </td>
                <td class="txt ">
                    <input type="text" contenteditable="false" id="txtPerson" style="background-color: #ffffc0;
                        width: 87%; height: 20px" runat="server" />

                    <img id="Img3" style="cursor: hand" onclick="selectUser();" src="/images/contact.gif" align="absmiddle" runat="server" />

                    <input id="ManagerCode" type="hidden" name="ManagerCode" runat="server" />

                    <asp:Label ID="lblPerson" Text="" Visible="false" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdfusercode" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; height: 100px;" class=" td-label">
                    问<br />
                    题<br />
                    描<br />
                    述
                </td>
                <td style="width: 40%;" colspan="3">
                    <asp:TextBox ID="txtQuestion" CssClass="textarea-input" TextMode="MultiLine" Width="99%" Height="90px" runat="server"></asp:TextBox>
                    <asp:Label ID="lblQuestion" Text="" Visible="false" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class=" td-label">
                    现<br />
                    场<br />
                    照<br />
                    片
                </td>
                <td valign="top" style="width: 47%;" colspan="3">
                    <div id="dvModulesBox" class="gridBox" style="height: 100px;">
                        <table id="TbModules" class="grid" border="1" style="width: 100%;
                            border-collapse: collapse;" runat="server"><tr class="td-label" runat="server"><td nowrap="true" width="40" runat="server">
                                    序号
                                </td><td nowrap="true" width="80" runat="server">
                                    照片编号
                                </td><td nowrap="true" runat="server">
                                    照片说明
                                </td><td nowrap="true" width="80" runat="server">
                                    照片文件
                                </td><td style="width: 0px;" runat="server">
                                </td></tr></table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivSettle" runat="server">
    </div>
    <div class="divFooter2">
        <table style="width: 100%;">
            <tr>
                <td align="right" class="">
                    <asp:Button ID="btnSave" CssClass="button-normal" Text="保  存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                    <input type="button" class="button-normal" value="取 消" onclick="return btnCancel_onclick()" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divUserCode" title="选择人员" style="display: none">
        <iframe id="ifUserCode" frameborder="0" width="100%" height="100%"></iframe>
    </div>
    </form>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    <script type="text/javascript">
        function valForm() {
            if (document.getElementById("txtQuestionTitle").value == "") {
                alert("系统提示：\n请输入问题名称！");
                document.getElementById("txtQuestionTitle").focus();
                return false;
            }
            else if (document.getElementById("txtPerson").value == "") {
                alert("系统提示：\n请输入指派人！");
                document.getElementById("txtPerson").focus();
                return false;
            }
            return true;
        }
        
    </script>
</body>
</html>
