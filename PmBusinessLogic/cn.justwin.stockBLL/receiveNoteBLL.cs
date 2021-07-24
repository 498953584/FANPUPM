namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class receiveNoteBLL
    {
        private receiveNoteDal receiveNote = new receiveNoteDal();

        public int Add(SqlTransaction trans, sm_receiveNote receiveNoteModel)
        {
            return this.receiveNote.Add(trans, receiveNoteModel);
        }

        public int Delete(SqlTransaction trans, string rnid)
        {
            return this.receiveNote.Delete(trans, rnid);
        }

        public List<sm_receiveNote> GetListArray(string strWhere)
        {
            return this.receiveNote.GetListArray(strWhere);
        }

        public List<sm_receiveNote> GetListByTimeAndNum(string startTime, string endTime, string rnCode, string rnUser)
        {
            return this.receiveNote.GetListByTimeAndNum(startTime, endTime, rnCode, rnUser);
        }

        public List<sm_receiveNote> GetListByTimeAndNum(string startTime, string endTime, string rnCode, string rnUser, string prjId)
        {
            return this.receiveNote.GetListByTimeAndNum(startTime, endTime, rnCode, rnUser, prjId);
        }

        public sm_receiveNote GetModelByrnId(string rnId)
        {
            return this.receiveNote.GetModel(rnId);
        }

        public DataTable GetNodeInfo(string sqlWhe)
        {
            return this.receiveNote.GetNode(sqlWhe);
        }

        public sm_receiveNote GetReceiveNoteBySnid(string snid)
        {
            return this.receiveNote.GetRNBySnid(snid);
        }

        public int Update(SqlTransaction trans, sm_receiveNote receiveNoteModel)
        {
            return this.receiveNote.Update(trans, receiveNoteModel);
        }
    }
}

