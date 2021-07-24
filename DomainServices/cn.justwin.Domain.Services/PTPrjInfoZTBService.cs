namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PTPrjInfoZTBService : Repository<PTPrjInfoZTB>
    {
        public void ChangePrjState(string prjId, int? prjState)
        {
            Guid id = new Guid(prjId);
            PTPrjInfoZTB byId = this.GetById(id);
            byId.PrjState = prjState;
            base.Update(byId);
        }

        private void ChangeStateAndPrjState(PTPrjInfoZTB prjInfoZTB, int prjState, int initFlowState, int ptfFlowState, int bitFlowstate, int giveUpFlowstate)
        {
            prjInfoZTB.PrjState = new int?(prjState);
            prjInfoZTB.InitiateFlowState = initFlowState;
            prjInfoZTB.PftFlowState = ptfFlowState;
            prjInfoZTB.BidFlowState = bitFlowstate;
            prjInfoZTB.GiveUpFlowState = giveUpFlowstate;
            base.Update(prjInfoZTB);
        }

        public void ClearGiveUp(PTPrjInfoZTB prjInfoZTb, int FlowState)
        {
            prjInfoZTb.OldState = 1;
            if (FlowState == 4)
            {
                prjInfoZTb.InitiateFlowState = -1;
            }
            else if (FlowState == 14)
            {
                prjInfoZTb.PftFlowState = -1;
            }
            else if (FlowState == 5)
            {
                prjInfoZTb.BidFlowState = -1;
            }
            prjInfoZTb.GiveUpFlowState = -1;
            prjInfoZTb.GiveUpTime = null;
            prjInfoZTb.GiveUpReason = string.Empty;
            prjInfoZTb.GiveUpNote = string.Empty;
            prjInfoZTb.Operator = string.Empty;
            base.Update(prjInfoZTb);
        }

        public void Delete(string id)
        {
            string sql = string.Format("DELETE FROM PT_PrjInfo_ZTB WHERE PrjGuid = '{0}'", id);
            base.ExcuteSql(sql);
        }

        public PTPrjInfoZTB GetById(Guid id)
        {
            return (from p in this
                where p.PrjGuid == id
                select p).FirstOrDefault<PTPrjInfoZTB>();
        }

        public PTPrjInfoZTB GetById(string id)
        {
            try
            {
                Guid guid = new Guid(id);
                return (from p in this
                    where p.PrjGuid == guid
                    select p).FirstOrDefault<PTPrjInfoZTB>();
            }
            catch
            {
                return null;
            }
        }

        public string GetParentTypeCode(string prjId)
        {
            string typeCode = string.Empty;
            if (!string.IsNullOrEmpty(prjId))
            {
                Guid prjguid = new Guid(prjId);
                PTPrjInfoService service = new PTPrjInfoService();
                if (!string.IsNullOrEmpty(prjId))
                {
                    typeCode = (from p in service
                        where p.PrjGuid == prjguid
                        select p).FirstOrDefault<PTPrjInfo>().TypeCode;
                }
            }
            return typeCode;
        }

        public void ModifyPrjInfoChildNum(PTPrjInfoZTB prjZTB)
        {
            PTPrjInfoService service = new PTPrjInfoService();
            new PTPrjInfo();
            if (!string.IsNullOrEmpty(prjZTB.ParentTypeCode))
            {
                PTPrjInfo item = (from p in service
                    where p.TypeCode.StartsWith(prjZTB.ParentTypeCode) && (p.TypeCode.Length == 5)
                    select p).FirstOrDefault<PTPrjInfo>();
                if (item != null)
                {
                    int? nullable2;
                    int? nullable = item.i_ChildNum;
                    item.i_ChildNum = nullable.HasValue ? ((nullable2 = nullable).HasValue ? new int?(nullable2.GetValueOrDefault() - 1) : null) : 0;
                }
                service.Update(item);
            }
        }

        public void UpdatePrjState(PTPrjInfoZTB prjInfoZTB, int? state)
        {
            PTPrjInfoService service = new PTPrjInfoService();
            bool flag = service.IsExist(prjInfoZTB.PrjGuid);
            prjInfoZTB.PrjState = state;
            prjInfoZTB.PrjStateChangeTime = new DateTime?(DateTime.Now);
            base.Update(prjInfoZTB);
            if (flag)
            {
                PTPrjInfo byId = service.GetById(prjInfoZTB.PrjGuid.ToString());
                byId.PrjState = state;
                service.Update(byId);
            }
        }

        public void UpdatePrjStateAtStateChange(string prjId, int prjOldState, int prjState)
        {
            Guid id = new Guid(prjId);
            PTPrjInfoZTB byId = this.GetById(id);
            if (byId != null)
            {
                byId.PrjStateChangeTime = new DateTime?(DateTime.Now);
                base.Update(byId);
                PTPrjInfoZTBDetailService service = new PTPrjInfoZTBDetailService();
                if (prjOldState == 1)
                {
                    service.ChangeFlowSateByPrjId(prjId, 1);
                }
                PTPrjInfoService service2 = new PTPrjInfoService();
                PTPrjInfo item = service2.GetById(prjId);
                if (item != null)
                {
                    item.PrjState = new int?(prjState);
                    service2.Update(item);
                }
                if (prjState == 1)
                {
                    this.ChangeStateAndPrjState(byId, prjState, -1, -1, -1, -1);
                    service.ChangeFlowSateByPrjId(prjId, -1);
                }
                else if (prjState == 2)
                {
                    this.ChangeStateAndPrjState(byId, prjState, -1, -1, -1, -1);
                }
                else if (((prjState == 3) || (prjState == 0x13)) || (prjState == 14))
                {
                    this.ChangeStateAndPrjState(byId, prjState, 1, -1, -1, -1);
                }
                else if (((prjState == 15) || (prjState == 0x10)) || (prjState == 4))
                {
                    this.ChangeStateAndPrjState(byId, prjState, 1, 1, -1, -1);
                }
                else if (prjState == 6)
                {
                    this.ChangeStateAndPrjState(byId, prjState, 1, 1, 1, -1);
                }
                else if (prjState == 5)
                {
                    this.ChangeStateAndPrjState(byId, prjState, 1, 1, 1, -1);
                    service2.ChangePrjInfo(byId, prjState, 1);
                }
                else if (prjState == 0x12)
                {
                    this.ChangeStateAndPrjState(byId, prjState, 1, -1, -1, 1);
                }
            }
        }
    }
}

