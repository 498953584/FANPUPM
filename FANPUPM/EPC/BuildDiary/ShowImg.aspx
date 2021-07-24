<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowImg.aspx.cs" Inherits="EPC_BuildDiary_ShowImg" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>现场照片</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            getImgPath();
        });
        var imgArr = new Array();
        var imgLength = 0;
        function getImgPath() {
            var str = $('#hfldImgPath').val();
            JSON.parse(str, function (k, v) {
                if (v != ',') {
                    imgArr.push(v);
                }
            });
            imgLength = imgArr.length - 1;
        }
        function next() {
            var nextIndex = parseInt($('#img').attr('alt'));
            if (nextIndex >= imgLength) {
                $('#img').attr('src', imgArr[0]);
                $('#img').attr('alt', 1);
            }
            else {
                $('#img').attr('src', imgArr[nextIndex]);
                $('#img').attr('alt', ++nextIndex);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <img alt="" id="img" src="" onclick="next();" runat="server" />

    </div>
    <asp:HiddenField ID="hfldImgPath" Value="" runat="server" />
    </form>
</body>
</html>
