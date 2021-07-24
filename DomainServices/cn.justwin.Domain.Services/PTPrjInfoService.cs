namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [ServiceContract, AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed), NHibernateContext, ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class PTPrjInfoService : Repository<PTPrjInfo>
    {
        public void AddPrjInfo(PTPrjInfoZTB prjInfoZTB)
        {
        }

        public void ChangePrjInfo(PTPrjInfoZTB prjInfoZTB, int prjState, int flowState = 1)
        {
            PTPrjInfoZTBDetailService service = new PTPrjInfoZTBDetailService();
            PTPrjInfoZTBDetail byId = service.GetById(prjInfoZTB.PrjGuid.ToString());
            PTPrjInfo item = this.GetById(prjInfoZTB.PrjGuid.ToString());
            PTPrjInfo prjInfo = this.GetPrjInfo(prjInfoZTB, byId);
            if (item != null)
            {
                item.PrjState = new int?(prjState);
                base.Update(item);
            }
            else
            {
                base.Add(prjInfo);
            }
            byId.SetUpFlowState = new int?(flowState);
            service.Update(byId);
            if (!string.IsNullOrEmpty(prjInfoZTB.ParentTypeCode))
            {
                int? parentCodeCount = this.GetParentCodeCount(prjInfoZTB.ParentTypeCode);
                PTPrjInfo info3 = (from p in this
                    where p.TypeCode == prjInfoZTB.ParentTypeCode
                    select p).FirstOrDefault<PTPrjInfo>();
                info3.i_ChildNum = parentCodeCount;
                base.Update(info3);
            }
        }

        public void Delete(string id)
        {
            string sql = string.Format("DELETE FROM PT_PrjInfo WHERE PrjGuid = '{0}'", id);
            base.ExcuteSql(sql);
        }

        public string GenerateTypeCode(string prjId)
        {
            string str = "00001";
            if (string.IsNullOrEmpty(prjId))
            {
                string str2 = (from p in this
                    where p.TypeCode.Length == 5
                    orderby p.TypeCode descending
                    select p.TypeCode).FirstOrDefault<string>();
                if (!string.IsNullOrEmpty(str2))
                {
                    int num = Convert.ToInt32(str2) + 1;
                    str = num.ToString().PadLeft(5, '0');
                }
                return str;
            }
            Guid guid = new Guid(prjId);
            string parentCode = (from p in this
                where p.PrjGuid == guid
                select p.TypeCode).FirstOrDefault<string>();
            string str3 = (from m in this
                where m.TypeCode.StartsWith(parentCode) && ((m.TypeCode.Length - 5) == parentCode.Length)
                orderby m.TypeCode descending
                select m.TypeCode).FirstOrDefault<string>();
            if (string.IsNullOrEmpty(str3))
            {
                return (parentCode + str);
            }
            int num2 = Convert.ToInt32(str3.Substring(str3.Length - 5)) + 1;
            return (parentCode + num2.ToString().PadLeft(5, '0'));
        }

        public int GenerateXh()
        {
            int num = 0;
            int? nullable = (from p in this select p.i_xh).Max<int?>();
            if (nullable.HasValue)
            {
                num = nullable.Value + 1;
            }
            return num;
        }

        [OperationContract, WebGet(UriTemplate="/Project/{id}", ResponseFormat=WebMessageFormat.Json)]
        public PTPrjInfo GetById(string id)
        {
            try
            {
                Guid prjGuid = new Guid(id);
                return (from p in this
                    where p.PrjGuid == prjGuid
                    select p).FirstOrDefault<PTPrjInfo>();
            }
            catch
            {
                return null;
            }
        }

        [WebGet(UriTemplate="/UserProject/{userCode}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<PTPrjInfo> GetByUserCode(string userCode)
        {
            try
            {
                PTPrjMemberService service = new PTPrjMemberService();
                PTPrjInfoService service2 = new PTPrjInfoService();
                PTYhmcService service3 = new PTYhmcService();
                return (from m in service
                    join y in service3 on m.MemberCode equals y.v_yhdm 
                    join p in service2 on m.PrjGuid.Value equals p.PrjGuid.Value 
                    where y.userCode == userCode
                    select p).ToList<PTPrjInfo>();
            }
            catch
            {
                return null;
            }
        }

        public PTPrjInfo GetEntityByTypeCode(string typeCode)
        {
            return (from m in this
                where m.TypeCode == typeCode
                select m).FirstOrDefault<PTPrjInfo>();
        }

        private int GetI_xh()
        {
            string str = (from p in this select p.i_xh).Max<int?>().ToString();
            if (string.IsNullOrEmpty(str))
            {
                return 1;
            }
            return (int.Parse(str) + 1);
        }

        public PTPrjInfo GetParent(string id)
        {
            Guid prjGuid = new Guid(id);
            return (from p1 in this
                join p2 in this on p1.TypeCode.Substring(0, 5) equals p2.TypeCode into p2
                where p1.PrjGuid == prjGuid
                    select p1).FirstOrDefault<PTPrjInfo>();
        }

        public IList<PTPrjInfo> GetParent(IList<PTPrjInfo> subList)
        {
            List<string> subIdList = (from s in subList select s.TypeCode.Substring(0, 5)).ToList<string>();
            return (from p in this
                where subIdList.Contains(p.TypeCode) && (p.IsValid == "1")
                select p).ToList<PTPrjInfo>();
        }

        public int? GetParentCodeCount(string ParentTypeCode)
        {
            string typeCode = string.Empty;
            int num = 0;
            if (!string.IsNullOrEmpty(ParentTypeCode))
            {
                if (ParentTypeCode.Length > 5)
                {
                    typeCode = ParentTypeCode.Substring(0, 5);
                }
                else
                {
                    typeCode = ParentTypeCode;
                }
                num = (from p in this
                    where p.TypeCode.StartsWith(typeCode) && (p.TypeCode.Length >= 5)
                    select p).ToList<PTPrjInfo>().Count<PTPrjInfo>();
            }
            return new int?(num);
        }

        private PTPrjInfo GetPrjInfo(PTPrjInfoZTB prjInfoZTB, PTPrjInfoZTBDetail prjInfoZTBDetail)
        {
            bool flag = this.IsExist(prjInfoZTB.PrjGuid);
            PTPrjInfoZTBUserService service = new PTPrjInfoZTBUserService();
            string typeCode = string.Empty;
            int? nullable = null;
            if (flag)
            {
                if (!string.IsNullOrEmpty(prjInfoZTB.ParentTypeCode))
                {
                    PTPrjInfo info = (from p in this
                        where p.PrjGuid == prjInfoZTB.PrjGuid
                        select p).FirstOrDefault<PTPrjInfo>();
                    typeCode = string.Equals(info.TypeCode.Substring(0, 5), prjInfoZTB.ParentTypeCode) ? info.TypeCode : this.GetTypeCode(prjInfoZTB.ParentTypeCode);
                    nullable = info.i_xh;
                }
                else
                {
                    typeCode = this.GetTypeCode(prjInfoZTB.ParentTypeCode);
                    nullable = new int?(this.GetI_xh());
                }
            }
            else
            {
                typeCode = this.GetTypeCode(prjInfoZTB.ParentTypeCode);
                nullable = new int?(this.GetI_xh());
            }
            PTPrjInfo info2 = new PTPrjInfo {
                TypeCode = typeCode,
                i_xh = nullable,
                IsValid = "1",
                PrjCode = prjInfoZTB.PrjCode,
                PrjState = prjInfoZTB.PrjState,
                xmgroup = "",
                UserCode = prjInfoZTBDetail.InputUser,
                PrjGuid = new Guid?(prjInfoZTB.PrjGuid),
                PrjName = prjInfoZTB.PrjName,
                StartDate = prjInfoZTB.StartDate,
                EndDate = prjInfoZTB.EndDate,
                ComputeClass = prjInfoZTB.ComputeClass,
                RationClass = prjInfoZTB.RationClass,
                PrjCost = prjInfoZTB.PrjCost,
                ContractSum = prjInfoZTB.ContractSum,
                Duration = prjInfoZTB.Duration,
                QualityClass = prjInfoZTB.QualityClass,
                Area = prjInfoZTB.Area,
                PrjKindClass = prjInfoZTB.PrjKindClass,
                PrjPlace = prjInfoZTB.PrjPlace,
                Remark1 = prjInfoZTB.Remark,
                Owner = prjInfoZTB.Owner,
                Counsellor = prjInfoZTB.Counsellor,
                Designer = prjInfoZTB.Designer,
                Inspector = prjInfoZTB.Inspector,
                PrjInfo = prjInfoZTB.PrjInfo,
                OwnerCode = prjInfoZTB.OwnerCode,
                MarketInfoGuid = prjInfoZTB.MarketInfoGuid,
                Rank = prjInfoZTB.Rank,
                BudgetWay = prjInfoZTB.BudgetWay,
                ContractWay = prjInfoZTB.ContractWay,
                PayCondition = prjInfoZTB.PayCondition,
                TenderWay = prjInfoZTB.TenderWay,
                PayWay = prjInfoZTB.PayWay,
                KeyPart = prjInfoZTB.KeyPart,
                WorkUnit = prjInfoZTB.WorkUnit,
                LinkMan = prjInfoZTB.LinkMan,
                PrjManager = prjInfoZTB.PrjManager,
                BuildingType = prjInfoZTB.BuildingType,
                TotalHouseNum = prjInfoZTB.TotalHouseNum,
                BuildingArea = prjInfoZTB.BuildingArea,
                UsegrounArea = prjInfoZTB.UsegrounArea,
                UndergroundArea = prjInfoZTB.UndergroundArea,
                PrjFundInfo = prjInfoZTB.PrjFundInfo,
                OtherStatement = prjInfoZTB.OtherStatement,
                i_ChildNum = 0
            };
            string str2 = service.GetUser(prjInfoZTB.PrjGuid.ToString()).ToCsv();
            info2.Podepom = "," + str2;
            return info2;
        }

        public IList<PTPrjInfo> GetSubproject(string id)
        {
            Guid prjGuid = new Guid(id);
            return (from p1 in this
                join p2 in this on p1.TypeCode equals p2.TypeCode.Substring(0, 5) into p2
                    where (p1.PrjGuid == prjGuid) && (p1.TypeCode.Length == 10)
                    select p1).ToList<PTPrjInfo>();
        }

        private string GetTypeCode(string ParentTypeCode)
        {
            string str = "00001";
            if (string.IsNullOrEmpty(ParentTypeCode))
            {
                string str2 = (from m in this
                    where m.TypeCode.Length == 5
                    orderby m.TypeCode descending
                    select m.TypeCode).FirstOrDefault<string>();
                int num = Convert.ToInt32(str2) + 1;
                if (!string.IsNullOrEmpty(str2))
                {
                    str = num.ToString().PadLeft(5, '0');
                }
                return str;
            }
            string str3 = (from p in this
                where p.TypeCode.StartsWith(ParentTypeCode) && ((p.TypeCode.Length - 5) == ParentTypeCode.Length)
                orderby p.TypeCode descending
                select p.TypeCode).FirstOrDefault<string>();
            if (string.IsNullOrEmpty(str3))
            {
                return (ParentTypeCode + str);
            }
            int num2 = Convert.ToInt32(str3.Substring(str3.Length - 5)) + 1;
            return (ParentTypeCode + num2.ToString().PadLeft(5, '0'));
        }

        public bool IsExist(Guid prjId)
        {
            return ((from p in this
                where p.PrjGuid == prjId
                select p).Count<PTPrjInfo>() > 0);
        }
    }
}

