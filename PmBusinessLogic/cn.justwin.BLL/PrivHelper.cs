namespace cn.justwin.BLL
{
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PrivHelper
    {
        public static IList<string> GetBusiDataId(string type, string userCode)
        {
            List<string> list = new List<string>();
            new PrivUserRoleService();
            PrivBusiDataRoleService service = new PrivBusiDataRoleService();
            if (type == "project")
            {
                string tableName = "PT_PrjInfo_ZTB_Detail";
                IList<string> busiData = service.GetBusiData(userCode, tableName);
                IList<string> prjByUserCode = new PTPrjInfoZTBUserService().GetPrjByUserCode(userCode);
                string str2 = "XPM_Basic_CodeList";
                IList<string> guidByPrjProperty = new PTPrjInfoZTBDetailService().GetGuidByPrjProperty(userCode, str2);
                busiData = busiData.Union<string>(guidByPrjProperty).Distinct<string>().ToList<string>();
                busiData.Union<string>(prjByUserCode).Distinct<string>().ToList<string>();
                list = busiData.Union<string>(prjByUserCode).Distinct<string>().ToList<string>();
            }
            bool flag1 = type == "contractType";
            return list;
        }
    }
}

