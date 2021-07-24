namespace com.jwsoft.pm.entpm.action
{
    using System;
    using System.Web.UI;

    public class ProjectInfoCommon
    {
        private static void Common(bool bRet, Page page, string strSuccess, string strFail, int iType)
        {
            switch (iType)
            {
                case 1:
                    if (!bRet)
                    {
                        page.RegisterClientScriptBlock("info", "<script language='javascript'>alert('" + strFail + "');window.close();</script>");
                        return;
                    }
                    page.RegisterClientScriptBlock("info", "<script language='javascript'>alert('" + strSuccess + "');window.opener.location.href += '';window.close();</script>");
                    return;

                case 2:
                    if (!bRet)
                    {
                        page.RegisterClientScriptBlock("info", "<script language='javascript'>alert('" + strFail + "');window.close();</script>");
                        return;
                    }
                    page.RegisterClientScriptBlock("info", "<script language='javascript'>alert('" + strSuccess + "');window.close();</script>");
                    return;

                case 3:
                    if (!bRet)
                    {
                        page.RegisterClientScriptBlock("info", "<script language='javascript'>alert('" + strFail + "');</script>");
                        return;
                    }
                    page.RegisterClientScriptBlock("info", "<script language='javascript'>alert('" + strSuccess + "');location.href += '';</script>");
                    return;
            }
        }

        public static void OperateDialog(bool bRet, Page page, string strSuccess, string strFail)
        {
            Common(bRet, page, strSuccess, strFail, 2);
        }

        public static void OperateDialogLess(bool bRet, Page page, string strSuccess, string strFail)
        {
            Common(bRet, page, strSuccess, strFail, 1);
        }

        public static void OperateSelf(bool bRet, Page page, string strSuccess, string strFail)
        {
            Common(bRet, page, strSuccess, strFail, 3);
        }
    }
}

