<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="StockManage_ErrorPage" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>错误页面</title>
    <style type="text/css">
        html, body, form
        {
            width: 100%;
            height: 100%;
            margin: 0px;
            padding: 0px;
            border-width: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="background-image: url('/StockManage/skins/img/top_02.jpg'); width: 100%;
                    height: 50px;">
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 100px; vertical-align:top;">
                                <div style="margin-left:100px; margin-top:20px;">
                                    <img src="skins/img/err_05.jpg" alt="" />
                                </div>
                            </td>
                            <td style="width:100%; vertical-align:top; padding-left:50px;">
                                <table style="border:solid 0px red; width:100%; height:300px; vertical-align:top;">
                                    <tr>
                                        <td style="vertical-align:top; color:#44769b; font-weight:bold; height:20px;">
                                            抱歉，无法打开页面
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom:dashed 1px #44769b;  height:5px;">
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border:solid 0px red; vertical-align:top; ">
                                            <div style="padding-top:20px; font-size:12px; line-height:30px;">
                                              <img src="skins/img/con_10.jpg" /> 
                                              <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                                            </div>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
