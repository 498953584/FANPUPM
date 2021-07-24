namespace cn.justwin.stockBLL
{
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;

    internal class AnnexSettingsBll
    {
        private AnnexSettingsBll annexSettingsBll = new AnnexSettingsBll();

        public int Add(AnnexSettingsModel model)
        {
            return this.annexSettingsBll.Add(model);
        }

        public int Delete(int ModuleID)
        {
            return this.annexSettingsBll.Delete(ModuleID);
        }

        public List<AnnexSettingsModel> GetListArray(string strWhere)
        {
            return this.annexSettingsBll.GetListArray(strWhere);
        }

        public AnnexSettingsModel GetModel(int ModuleID)
        {
            return this.annexSettingsBll.GetModel(ModuleID);
        }

        public int Update(AnnexSettingsModel model)
        {
            return this.annexSettingsBll.Update(model);
        }
    }
}

