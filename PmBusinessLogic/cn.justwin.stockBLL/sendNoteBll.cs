namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class sendNoteBll
    {
        private SendNote sendnote = new SendNote();

        public int Add(SqlTransaction trans, SendNodteModel model)
        {
            return this.sendnote.Add(trans, model);
        }

        public int DeleteBysnId(SqlTransaction trans, string snid)
        {
            return this.sendnote.Delete(trans, snid);
        }

        public List<SendNodteModel> GetListArray(string strWhere)
        {
            return this.sendnote.GetListArray(strWhere);
        }

        public List<SendNodteModel> GetListByTimeAndNum(string startTime, string endTime, string snCode, string person, string project, string userCode)
        {
            return this.sendnote.GetListByTimeAndNum(startTime, endTime, snCode, person, project, userCode);
        }

        public List<SendNodteModel> GetListByTimeAndNum1(string startTime, string endTime, string snCode, string person, string project)
        {
            return this.sendnote.GetListByTimeAndNum1(startTime, endTime, snCode, person, project);
        }

        public SendNodteModel GetSendNoteModel(string snId)
        {
            return this.sendnote.GetModel(snId);
        }

        public int Update(SqlTransaction trans, SendNodteModel model)
        {
            return this.sendnote.Update(trans, model);
        }

        public int updateState(SqlTransaction trans, string snId)
        {
            return this.sendnote.UpdateState(trans, snId);
        }

        public int UpdateStateNo(SqlTransaction trans, string snid)
        {
            return this.sendnote.UpdateStateNo(trans, snid);
        }
    }
}

