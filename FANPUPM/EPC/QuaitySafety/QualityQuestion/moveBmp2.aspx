<%@ Page Language="C#" AutoEventWireup="true" CodeFile="moveBmp2.aspx.cs" Inherits="PlatForm_ShowInfo_moveBmp_moveBmp" %>
<%@ Import Namespace="cn.justwin.opm.Public"%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目信息展示图</title>
    <link href="Scripts/PrjInfomoveBmp/css/css.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/PrjInfomoveBmp/js/ScrollPic.js" type="text/javascript"></script>
    <script src="../../../Web_Client/TreeNew.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        function imgclick(type, prjid, pn, iid, pc) {
            switch (type) {
                case "CheckIn":
                    toolbox_oncommand("/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/SettleQuestionEdit.aspx?op=add&prj=" + prjid + "&pn=" + pn + "&IntendanceGuid=" + iid + "&t=2", "查看问题");
                    break;
            }
        }
       
    </script>
</head>
<body style="padding: 0px; margin: 0px" scoll="no">
    <table>
        <tr>
            <td>
                <div class="rollphotos">
                    <div class="blk_29" style="width: <%=Request["Cwidth"] %>">
                        <div class="LeftBotton" id="LeftArr">
                        </div>
                        <div class="Cont" id="ISL_Cont_1">
                            <!-- 图片列表 begin -->
                            <%for (int i = 0; i < this.PhotoCount; i++)
{
if (i + 1 > this.PhotoDat.Rows.Count || this.PhotoDat.Rows[i]["tpath"] == "")
{%>
                            <div class="box">
                                <a class="imgBorder">
                                    <img alt="" height="84" src="Scripts/PrjInfomoveBmp/images/nopic.png" style="cursor: pointer;" width="124"
                                        border="0" /></a>
                                <p>
                                    <a>暂无图片</a></p>
                            </div>
                            <%}
else
{%>
                            <div class="box">
                                <a class="imgBorder">
                                    <img style="cursor: pointer;" id='<%=this.PhotoDat.Rows[i]["id1"] %>' title='<%=this.PhotoDat.Rows[i]["title"] %>'
                                        alt="" height="84" src='<%=this.PhotoDat.Rows[i]["tpath"] %>' width="124" border="0"
                                        onclick="<%=getOnclickstr(i) %>" /></a>
                                <p>
                                    <%=GridViewStringSub.StrSub(this.PhotoDat.Rows[i]["PrjName"].ToString(), 6) %></p>
                            </div>
                            <%}
}%>
                            <!-- 图片列表 end -->
                        </div>
                        <div class="RightBotton" id="RightArr">
                        </div>
                    </div>
                    <script language="javascript" type="text/javascript">
		<!--                        //--><![CDATA[//><!--
                        var scrollPic_02 = new ScrollPic();
                        scrollPic_02.scrollContId = "ISL_Cont_1"; //内容容器ID
                        scrollPic_02.arrLeftId = "LeftArr"; //左箭头ID
                        scrollPic_02.arrRightId = "RightArr"; //右箭头ID

                        scrollPic_02.frameWidth = 950; //显示框宽度
                        scrollPic_02.pageWidth = 135; //翻页宽度

                        scrollPic_02.speed = 10; //移动速度(单位毫秒，越小越快)
                        scrollPic_02.space = 10; //每次移动像素(单位px，越大越快)
                        scrollPic_02.autoPlay = true; //自动播放
                        scrollPic_02.autoPlayTime = 1; //自动播放间隔时间(秒)

                        scrollPic_02.initialize(); //初始化
                        //--><!]]>
                    </script>
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
