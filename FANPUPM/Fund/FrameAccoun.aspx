<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrameAccoun.aspx.cs" Inherits="PlatForm_ShowInfoFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }
       
    </style>
</head>

      
            <frameset  rows='280,*'>
                <frame id="top" src="ShowInfo/OwnerCanApproval.aspx" runat="server">
                </frame>
                
                 <frame id="bottom" runat="server">
                 </frame>
            </frameset>
           

  

</html>
