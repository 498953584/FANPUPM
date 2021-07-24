namespace cn.justwin.Popup
{
    using cn.justwin.popupDAL;
    using System;
    using System.Data;

    public class PopupSetting
    {
        private cn.justwin.popupDAL.PopupSetting popset = new cn.justwin.popupDAL.PopupSetting();

        public void Cancel(string userCode, string module)
        {
            this.popset.Cancel(userCode, module);
        }

        public DataTable GetSetting(string userCode)
        {
            return this.popset.GetSetting(userCode);
        }

        public void Subscribe(string userCode, string module)
        {
            this.popset.Subscribe(userCode, module);
        }
    }
}

