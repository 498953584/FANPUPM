namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class IncometModifyBll
    {
        private IncometModify incometModify = new IncometModify();

        public int Add(IncometModifyModel model)
        {
            return this.incometModify.Add(model);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            return this.incometModify.Delete(trans, ID);
        }

        public void DeleteSheet(string modifyId)
        {
            this.incometModify.DeleteSheet(modifyId);
        }

        public List<IncometModifyModel> GetListArray(string strWhere)
        {
            return this.incometModify.GetListArray(strWhere);
        }

        public IncometModifyModel GetModel(string ID)
        {
            return this.incometModify.GetModel(ID);
        }

        public DataTable GetSheet(string modifyId)
        {
            return this.incometModify.GetSheet(modifyId);
        }

        public string GetSheetNameById(int id)
        {
            return this.incometModify.GetSheetNameById(id);
        }

        public void InsertSheet(string modifyId, string sheetId)
        {
            this.incometModify.InsertSheet(modifyId, sheetId);
        }

        public bool IsExists(string modifyId, string sheetId)
        {
            return this.incometModify.IsExistsSheet(modifyId, sheetId);
        }

        public DataTable PickSheetByModifyId(string modifyId)
        {
            return this.incometModify.PickSheetByModifyId(modifyId);
        }

        public DataTable PickSheetByPrjGuid(string prjGuid)
        {
            return this.incometModify.PickSheetByPrjGuid(prjGuid);
        }

        public int Update(IncometModifyModel model)
        {
            return this.incometModify.Update(model);
        }
    }
}

