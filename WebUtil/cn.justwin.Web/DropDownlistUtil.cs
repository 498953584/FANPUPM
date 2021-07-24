namespace cn.justwin.Web
{
    using System;
    using System.Web.UI.WebControls;

    /// <summary>
    /// DropDownlist辅助类
    /// </summary>
    public class DropDownlistUtil
    {
        /// <summary>
        /// 添加空项
        /// </summary>
        /// <param name="list"></param>
        public static void AddEmptyItem(DropDownList list)
        {
            ListItem item = new ListItem();
            list.Items.Add(item);
        }

        /// <summary>
        /// 添加提示项
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        public static void AddHintItem(DropDownList list, string name)
        {
            ListItem item = new ListItem {
                Text = "",
                Value = ""
            };
            list.Items.Insert(0, item);
        }
    }
}

