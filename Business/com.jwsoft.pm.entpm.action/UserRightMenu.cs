namespace com.jwsoft.pm.entpm.action
{
    using System;
    using System.Data;

    public class UserRightMenu
    {
        private string _strUserCode = "";

        public string RightMenu()
        {
            string str = "";
            if (this._strUserCode == "")
            {
                return str;
            }
            DataTable userDisplayMenu = new userManageDb().GetUserDisplayMenu(this._strUserCode);
            str = str + "<SCRIPT language=\"javascript\"  type=\"text/javascript\" src=\"/oa/common/DrawMouseMenu.js\"></SCRIPT>\n" + "<SCRIPT language=\"javascript\">DrawMouseMenu(";
            for (int i = 0; i < userDisplayMenu.Rows.Count; i++)
            {
                string str2 = str;
                str = str2 + "\"top.NavigationMenu.location.href='/SysFrame/NavigationMenu.aspx?id=" + userDisplayMenu.Rows[i][0].ToString() + "&url=/" + userDisplayMenu.Rows[i][2].ToString() + "';\\\">" + userDisplayMenu.Rows[i][1].ToString() + "\"";
                if (i != (userDisplayMenu.Rows.Count - 1))
                {
                    str = str + ",";
                }
            }
            return (str + ");</SCRIPT>");
        }

        public string UserCode
        {
            get
            {
                return this._strUserCode;
            }
            set
            {
                this._strUserCode = value;
            }
        }
    }
}

