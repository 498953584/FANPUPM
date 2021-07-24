namespace cn.justwin.stockBLL
{
    using cn.justwin.BLL;
    using cn.justwin.DAL;
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class receiveSelf : NBasePage, ISelfEvent
    {
        private string acode;
        private AllocationBllAction bllAction1 = new AllocationBllAction();
        private string currentUserName;
        private string depotType;
        private OutReserveBll outReserveBll = new OutReserveBll();
        private OutStockBll outStockBll = new OutStockBll();
        private PtYhmcBll ptYhmcBll = new PtYhmcBll();
        private receiveGoodsBLL receiveGood = new receiveGoodsBLL();
        private receiveNoteBLL receiveNote = new receiveNoteBLL();
        private sm_receiveNote receiveNoteModel;
        private string scode;
        private sendNoteBll sendnote = new sendNoteBll();
        private SendNodteModel sendNoteMdoleInfo;
        private cn.justwin.stockBLL.Storage storage = new cn.justwin.stockBLL.Storage();
        private cn.justwin.stockBLL.StorageStock storageStock = new cn.justwin.stockBLL.StorageStock();
        private cn.justwin.stockBLL.Treasury treasuryBll = new cn.justwin.stockBLL.Treasury();
        private string treasuryCode;

        private void AddStorage(SqlTransaction trans)
        {
            StorageModel model = new StorageModel {
                sid = Guid.NewGuid().ToString()
            };
            this.ReceiveNoteModel.stId = model.sid;
            model.scode = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.Scode = model.scode;
            model.flowstate = 1;
            model.person = this.UserName;
            model.intime = DateTime.Now;
            model.inflag = true;
            model.annx = string.Empty;
            model.explain = string.Empty;
            model.project = this.SendNoteMdoleInfo.prjCode.ToString();
            model.Trustee = string.Empty;
            model.Supervisor = string.Empty;
            model.IsInTime = DateTime.Now;
            if (this.DepotType == "TotalMode")
            {
                model.tcode = new cn.justwin.stockBLL.Treasury().GetTotalCode();
            }
            else
            {
                model.tcode = this.TreasuryCode;
            }
            model.isfirst = false;
            this.storage.Add(trans, model);
        }

        private void AddStorageStock(SqlTransaction trans)
        {
            if (this.ViewState["DataTable"] is DataTable)
            {
                DataTable table = this.ViewState["DataTable"] as DataTable;
                List<StorageStockModel> lstModel = new List<StorageStockModel>();
                foreach (DataRow row in table.Rows)
                {
                    StorageStockModel item = new StorageStockModel {
                        ssid = Guid.NewGuid().ToString(),
                        scode = row["ResourceCode"].ToString(),
                        stcode = this.Scode,
                        number = Convert.ToDecimal(row["number"].ToString()),
                        sprice = Convert.ToDecimal(row["price"].ToString()),
                        corp = row["suppyCode"].ToString(),
                        intype = string.Empty,
                        linkcode = this.ReceiveNoteModel.rnId
                    };
                    lstModel.Add(item);
                }
                this.storageStock.Add(trans, lstModel);
            }
        }

        public void CommitEvent(object key)
        {
            string rid = key.ToString();
            this.ReceiveResource(rid);
            this.ReceiveNoteModel = this.receiveNote.GetModelByrnId(rid);
            this.SendNoteMdoleInfo = this.sendnote.GetSendNoteModel(this.receiveNoteModel.snId);
            this.currentUserName = this.ptYhmcBll.GetModelById(this.SendNoteMdoleInfo.snAddUser).v_xm;
            this.treasuryCode = this.treasuryBll.GetTcode(this.SendNoteMdoleInfo.prjCode.ToString());
            if (!string.IsNullOrEmpty(this.treasuryCode))
            {
                this.depotType = StockParameter.getSm_setValue("DepotType");
                using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    this.AddStorage(trans);
                    this.AddStorageStock(trans);
                    if (this.depotType != "ParallelMode")
                    {
                        this.bllAction1.Insert(trans, this.GetAllocationData());
                        this.GetStockModel(trans);
                    }
                    this.smOutReserve(trans);
                    this.receiveNote.Update(trans, this.ReceiveNoteModel);
                    trans.Commit();
                }
            }
        }

        private AllocationModel GetAllocationData()
        {
            AllocationModel model = new AllocationModel {
                Aid = Guid.NewGuid().ToString()
            };
            this.ReceiveNoteModel.SAllocationId = model.Aid;
            model.Acode = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.Acode = model.Acode;
            model.Annex = "";
            model.Explain = "";
            model.FlowState = 1;
            model.InAllocationPerson = this.sendNoteMdoleInfo.snAddUser;
            model.OutAllocationPerson = this.ReceiveNoteModel.rnUser;
            model.Person = this.sendNoteMdoleInfo.snAddUser;
            model.TCodea = new cn.justwin.stockBLL.Treasury().GetTotalCode();
            model.TCodeb = this.TreasuryCode;
            model.InTime = DateTime.Now.ToShortDateString();
            model.IsOutA = true;
            model.IsInB = true;
            model.IsOutTime = DateTime.Now.ToString();
            model.IsInTime = DateTime.Now.ToString();
            return model;
        }

        private void GetStockModel(SqlTransaction trans)
        {
            if (this.ViewState["DataTable"] is DataTable)
            {
                DataTable table = this.ViewState["DataTable"] as DataTable;
                new List<StorageStockModel>();
                foreach (DataRow row in table.Rows)
                {
                    AllocationStockModel stockModel = new AllocationStockModel {
                        Number = Convert.ToDecimal(row["number"].ToString()),
                        Sprice = Convert.ToDecimal(row["price"].ToString()),
                        Corp = row["suppyCode"].ToString(),
                        ACode = this.Acode,
                        SCode = row["ResourceCode"].ToString(),
                        Asid = Guid.NewGuid().ToString()
                    };
                    this.bllAction1.Insert(trans, stockModel);
                }
            }
        }

        private string getUserDepartmentName(string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select v_bmbm from pt_d_bm where i_bmdm= ");
            builder.Append(" (select top 1 i_bmdm from pt_yhmc where v_yhdm= @userCode) ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.NVarChar, 8) };
            commandParameters[0].Value = userCode;
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString();
        }

        private void ReceiveResource(string rid)
        {
            string[] rnid = new string[] { rid };
            this.ViewState["DataTable"] = this.receiveGood.getResourceByRnid(rnid);
        }

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        private void smOutReserve(SqlTransaction trans)
        {
            OutReserveModel model = new OutReserveModel {
                annx = "",
                explain = "",
                flowstate = 1,
                intime = DateTime.Now,
                isout = true,
                orcode = DateTime.Now.ToString("yyyyMMddHHmmss"),
                orid = Guid.NewGuid().ToString()
            };
            this.ReceiveNoteModel.soId = model.orid;
            model.person = this.UserName;
            model.procode = this.SendNoteMdoleInfo.prjCode.ToString();
            model.tcode = this.TreasuryCode;
            model.PickingSector = this.getUserDepartmentName(this.sendNoteMdoleInfo.snAddUser);
            model.PickingPeople = this.UserName;
            model.IsOutTime = DateTime.Now;
            if (this.outReserveBll.Add(trans, model) != 0)
            {
                this.outStockBll.DeleteByWhere(trans, " where orcode='" + model.orcode + "'");
                DataTable table = (DataTable) this.ViewState["DataTable"];
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        OutStockModel model2 = new OutStockModel {
                            corp = row["suppyCode"].ToString(),
                            number = Convert.ToDecimal(row["number"].ToString()),
                            orcode = model.orcode,
                            orsid = Guid.NewGuid().ToString(),
                            scode = row["scode"].ToString(),
                            sprice = Convert.ToDecimal(row["price"]),
                            TaskId = string.Empty
                        };
                        this.outStockBll.Add(trans, model2);
                    }
                }
            }
        }

        public void SuperDelete(object key)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                List<string> lstStorageCode = new List<string>();
                try
                {
                    sm_receiveNote modelByrnId = this.receiveNote.GetModelByrnId(key.ToString());
                    OutReserveModel modelByIc = this.outReserveBll.GetModelByIc(modelByrnId.soId);
                    if (modelByIc != null)
                    {
                        this.outStockBll.DeleteByWhere(trans, " where orcode ='" + modelByIc.orcode + "'");
                        this.outReserveBll.Delete(trans, modelByIc.orcode);
                    }
                    StorageModel modelBySid = this.storage.GetModelBySid(modelByrnId.stId);
                    if (modelBySid != null)
                    {
                        lstStorageCode.Add(modelBySid.scode);
                    }
                    if (!string.IsNullOrEmpty(modelByrnId.SAllocationId))
                    {
                        AllocationBllAction action = new AllocationBllAction();
                        AllocationModel model3 = new AllocationModel();
                        model3 = action.ReturnAllocatonModel(" aid='" + modelByrnId.SAllocationId + "'");
                        if (model3 != null)
                        {
                            action.DelAllocationStockByAcode(trans, model3.Acode);
                            action.Delete(trans, model3.Acode);
                        }
                    }
                    this.receiveGood.Delete(trans, modelByrnId.rnId);
                    this.receiveNote.Delete(trans, modelByrnId.rnId);
                    this.sendnote.UpdateStateNo(trans, modelByrnId.snId);
                    if (lstStorageCode.Count != 0)
                    {
                        this.storage.DelByTrans(trans, lstStorageCode);
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    base.RegisterScript(@"alert('系统提示：\n\n对不起撤销失败！');");
                }
            }
        }

        public string Acode
        {
            get
            {
                return this.acode;
            }
            set
            {
                this.acode = value;
            }
        }

        public string DepotType
        {
            get
            {
                return this.depotType;
            }
            set
            {
                this.depotType = value;
            }
        }

        public sm_receiveNote ReceiveNoteModel
        {
            get
            {
                return this.receiveNoteModel;
            }
            set
            {
                this.receiveNoteModel = value;
            }
        }

        public string Scode
        {
            get
            {
                return this.scode;
            }
            set
            {
                this.scode = value;
            }
        }

        public SendNodteModel SendNoteMdoleInfo
        {
            get
            {
                return this.sendNoteMdoleInfo;
            }
            set
            {
                this.sendNoteMdoleInfo = value;
            }
        }

        public string TreasuryCode
        {
            get
            {
                return this.treasuryCode;
            }
            set
            {
                this.treasuryCode = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.currentUserName;
            }
            set
            {
                this.currentUserName = value;
            }
        }
    }
}

