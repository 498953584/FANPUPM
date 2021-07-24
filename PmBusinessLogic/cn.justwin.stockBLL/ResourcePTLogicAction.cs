namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;

    public class ResourcePTLogicAction
    {
        public int Add(ResourcePTModel model)
        {
            int num = 0;
            if (model.rptId != "")
            {
                num = ResourcePTAction.Add(model);
            }
            return num;
        }

        public int Delete(string rptId)
        {
            string[] strArray = rptId.Split(new char[] { ',' });
            int foreignKeyAmount = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                rptId = strArray[i].ToString();
                DataTable list = ResourcePTAction.GetList(rptId);
                if (list.Rows.Count > 0)
                {
                    foreignKeyAmount = ResourcePTAction.GetForeignKeyAmount(list.Rows[0]["rptCode"].ToString());
                    if (foreignKeyAmount == 0)
                    {
                        if (ResourcePTAction.Delete(rptId) > 0)
                        {
                            foreignKeyAmount = -1;
                        }
                        else
                        {
                            foreignKeyAmount = 0;
                        }
                    }
                }
            }
            return foreignKeyAmount;
        }

        public DataTable GetList()
        {
            return ResourcePTAction.GetList();
        }

        public ResourcePTModel GetPriceTypeSolidModel(string rptId)
        {
            ResourcePTModel model = new ResourcePTModel();
            DataTable list = ResourcePTAction.GetList(rptId);
            if (list.Rows.Count > 0)
            {
                if (list.Rows[0]["rptid"].ToString() != "")
                {
                    model.rptId = list.Rows[0]["rptid"].ToString();
                }
                if (list.Rows[0]["rptcode"].ToString() != "")
                {
                    model.rptCode = list.Rows[0]["rptcode"].ToString();
                }
                if (list.Rows[0]["rptname"].ToString() != "")
                {
                    model.rptName = list.Rows[0]["rptname"].ToString();
                }
                if (list.Rows[0]["rptexplain"].ToString() != "")
                {
                    model.rptExplain = list.Rows[0]["rptexplain"].ToString();
                }
                if (list.Rows[0]["rptisshow"].ToString() != "")
                {
                    model.rptIsShow = list.Rows[0]["rptisshow"].ToString();
                }
            }
            return model;
        }

        public int IsPreSenceOneData(string rptCode)
        {
            int num = 0;
            if (rptCode != "")
            {
                num = ResourcePTAction.IsPreSenceOneData(rptCode);
            }
            return num;
        }

        public int Update(ResourcePTModel model)
        {
            int num = 0;
            if (model.rptId != "")
            {
                num = ResourcePTAction.Update(model);
            }
            return num;
        }

        public int UpdateState(string rptid)
        {
            int num = 0;
            if (rptid != "")
            {
                num = ResourcePTAction.UpdateState(rptid);
            }
            return num;
        }
    }
}

