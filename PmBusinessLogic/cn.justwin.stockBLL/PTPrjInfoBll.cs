namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PTPrjInfoBll
    {
        private PrjInfo prjInfo = new PrjInfo();

        public int Add(PrjInfoModel model)
        {
            return this.prjInfo.Add(model);
        }

        public void Add(string _PrjGuid, string _grade, string _businessman, string _telephone)
        {
            this.prjInfo.Add(_PrjGuid, _grade, _businessman, _telephone);
        }

        public int Delete(string TypeCode)
        {
            return this.prjInfo.Delete(TypeCode);
        }

        public string getBusinessman(string TypeCode)
        {
            string str = string.Empty;
            if ((TypeCode != null) && (TypeCode.ToString() != ""))
            {
                DataTable table = this.prjInfo.getbusinessman(TypeCode, "PT_PrjInfo");
                if ((table != null) && (table.Rows.Count > 0))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        str = row["businessman"].ToString();
                    }
                }
            }
            return str.Substring(str.LastIndexOf("-") + 1);
        }

        public DataTable getDataTable(string PrjGuid)
        {
            return this.prjInfo.getDataTable(PrjGuid, "PT_PrjInfo");
        }

        public List<PrjInfoModel> GetListArray(string strWhere)
        {
            return this.prjInfo.GetListArray(strWhere);
        }

        public PrjInfoModel GetModel(string TypeCode)
        {
            return this.prjInfo.GetModel(TypeCode);
        }

        public PrjInfoModel GetModelByPrjGuid(string prjGuid)
        {
            return this.prjInfo.GetModelByPrjGuid(prjGuid);
        }

        public List<string> GetPrjInfoIncoment(string prjId)
        {
            return this.prjInfo.GetPrjInfoIncoment(prjId);
        }

        public List<string> GetPrjInfoZTBIncoment(string conId)
        {
            return this.prjInfo.GetPrjInfoZTBIncoment(conId);
        }

        public DataTable GetProject(string usercode, string prjcode, string prjname, int pageIndex, int pageSize)
        {
            return this.prjInfo.GetProject(usercode, prjcode, prjname, pageIndex, pageSize);
        }

        public int GetProjectCount(string usercode, string prjcode, string prjname)
        {
            return this.prjInfo.GetProjectCount(usercode, prjcode, prjname);
        }

        public DataTable GetProjectIncoment(string usercode, string prjcode, string prjname, int pageIndex, int pageSize)
        {
            return this.prjInfo.GetProjectIncoment(usercode, prjcode, prjname, pageIndex, pageSize);
        }

        public int GetProjectIncomentCount(string usercode, string prjcode, string prjname)
        {
            return this.prjInfo.GetProjectIncomentCount(usercode, prjcode, prjname);
        }

        public DataTable GetTableByPrjGuid(string prjGuid)
        {
            return this.prjInfo.GetTableByPrjGuid(prjGuid);
        }

        public int update(string PrjGuid, string grade, string businessman, string telephone)
        {
            return this.prjInfo.update(PrjGuid, grade, businessman, telephone, "PT_PrjInfo");
        }

        public int Update(PrjInfoModel model)
        {
            return this.prjInfo.Update(model);
        }
    }
}

