namespace com.jwsoft.common.baseclass
{
    using System;
    using System.Web.UI;

    public class Security
    {
        public static void CheckSession(Page myPage)
        {
            if (myPage.Session["yhdm"] == null)
            {
                myPage.Response.Write("您没有登录，或身份已经过期！");
                myPage.Response.End();
            }
            string script = "";
            script = ((((((((script + "<script language=javascript>") + "try{" + "var obj = dialogArguments;") + "}" + "catch(e){") + "try{" + " var obj = top.NavigationMenu.document.body.getAttribute(\"yhdm\");") + "}" + "catch(anError)") + "{" + "\tif(anError.description = '\\'top.NavigationMenu.document\\'为空或不是对象')") + "\t{" + "     window.location='/';") + "\t}" + "}") + "}" + "</script>";
            myPage.RegisterStartupScript("IsLogin", script);
        }
    }
}

