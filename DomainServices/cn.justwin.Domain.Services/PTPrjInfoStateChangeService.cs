namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PTPrjInfoStateChangeService : Repository<PTPrjInfoStateChange>
    {
        public void ChangeFlowStateInfo(string prjId, int flowSate, int changeFlowState)
        {
            foreach (PTPrjInfoStateChange change in (from pc in this
                where (pc.PrjId == prjId) && (pc.FlowState == flowSate)
                select pc).ToList<PTPrjInfoStateChange>())
            {
                change.FlowState = new int?(changeFlowState);
                base.Update(change);
            }
        }

        public PTPrjInfoStateChange GetById(string id)
        {
            return (from p in this
                where p.Id == id
                select p).FirstOrDefault<PTPrjInfoStateChange>();
        }

        public PTPrjInfoStateChange GetByPrjIdByOrder(string prjId)
        {
            return (from p in this
                where p.PrjId == prjId
                select p).FirstOrDefault<PTPrjInfoStateChange>();
        }

        public PTPrjInfoStateChange GetByPrjIdByOrder(string prjId, int flowSate)
        {
            return (from p in this
                where (p.PrjId == prjId) && (p.FlowState == flowSate)
                orderby p.ChangeTime descending
                orderby p.InputDate descending
                select p).FirstOrDefault<PTPrjInfoStateChange>();
        }

        public int GetCountByPrjGuid(string prjGuid)
        {
            return (from p in this
                where p.PrjId == prjGuid
                select p).Count<PTPrjInfoStateChange>();
        }

        public int GetCountByPrjGuidAndCondition(string prjGuid, string startDate, string endDate)
        {
            IQueryable<PTPrjInfoStateChange> source = from p in this.AsQueryable<PTPrjInfoStateChange>()
                where p.PrjId == prjGuid
                where p.FlowState == 1
                select p;
            if (!string.IsNullOrEmpty(startDate))
            {
                source = from p in source
                    where p.ChangeTime > DateTime.Parse(startDate)
                    select p;
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                source = from p in source
                    where p.ChangeTime <= DateTime.Parse(endDate + " 23:59:59")
                    select p;
            }
            return source.Count<PTPrjInfoStateChange>();
        }

        public IList<PTPrjInfoStateChange> GetDTByPrjGuid(string prjGuid, int currentPage, int pageSize)
        {
            return (from p in this
                where p.PrjId == prjGuid
                orderby p.ChangeTime descending, p.InputDate descending
                select p).Skip<PTPrjInfoStateChange>(((currentPage - 1) * pageSize)).Take<PTPrjInfoStateChange>(pageSize).ToList<PTPrjInfoStateChange>();
        }

        public List<PTPrjInfoStateChange> GetLstByPrjGuid(string prjGuid, string startDate, string endDate, int currentPage, int pageSize)
        {
            IQueryable<PTPrjInfoStateChange> queryable = from p in this.AsQueryable<PTPrjInfoStateChange>()
                where p.PrjId == prjGuid
                where p.FlowState == 1
                select p;
            if (!string.IsNullOrEmpty(startDate))
            {
                queryable = from p in queryable
                    where p.ChangeTime > DateTime.Parse(startDate)
                    select p;
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                queryable = from p in queryable
                    where p.ChangeTime <= DateTime.Parse(endDate + " 23:59:59")
                    select p;
            }
            return (from p in queryable
                orderby p.ChangeTime descending
                orderby p.InputDate descending
                select p).Skip<PTPrjInfoStateChange>(((currentPage - 1) * pageSize)).Take<PTPrjInfoStateChange>(pageSize).ToList<PTPrjInfoStateChange>();
        }
    }
}

