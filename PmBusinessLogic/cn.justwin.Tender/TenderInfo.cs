using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using cn.justwin.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace cn.justwin.Tender
{
    public class TenderInfo
    {
        private System.Collections.Generic.IList<EngineeringType> engineeringTypes;
        private System.Collections.Generic.IList<ProjectKind> prjKinds;
        private System.Collections.Generic.IList<ProjectRank> prjRanks;
        public string PrjCode
        {
            get;
            set;
        }
        public System.Guid PrjGuid
        {
            get;
            set;
        }
        public string PrjName
        {
            get;
            set;
        }
        public System.DateTime? StartDate
        {
            get;
            set;
        }
        public System.DateTime? EndDate
        {
            get;
            set;
        }
        public string ComputeClass
        {
            get;
            set;
        }
        public string RationClass
        {
            get;
            set;
        }
        public double? PrjCost
        {
            get;
            set;
        }
        public string ContractSum
        {
            get;
            set;
        }
        public string Duration
        {
            get;
            set;
        }
        public string QualityClass
        {
            get;
            set;
        }
        public string Area
        {
            get;
            set;
        }
        public string PrjKindClass
        {
            get;
            set;
        }
        public string PrjPlace
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public string FileName
        {
            get;
            set;
        }
        public string FileURL
        {
            get;
            set;
        }
        public string Counsellor
        {
            get;
            set;
        }
        public string Designer
        {
            get;
            set;
        }
        public string Inspector
        {
            get;
            set;
        }
        public string PrjInfo
        {
            get;
            set;
        }
        public int PrjState
        {
            get;
            set;
        }
        public int? OwnerCode
        {
            get;
            set;
        }
        public string OwnerName
        {
            get
            {
                string code = (!this.OwnerCode.HasValue) ? string.Empty : this.OwnerCode.ToString();
                if (!string.IsNullOrEmpty(code))
                {
                    return this.GetCorpName(code);
                }
                return string.Empty;
            }
        }
        public string OwnerLinkManName
        {
            get;
            set;
        }
        public string OwnerLinkPhone
        {
            get;
            set;
        }
        public string OwnerAddress
        {
            get;
            set;
        }
        public System.Guid? MarketInfoGuid
        {
            get;
            set;
        }
        public string Rank
        {
            get;
            set;
        }
        public string BudgetWay
        {
            get;
            set;
        }
        public string ContractWay
        {
            get;
            set;
        }
        public string PayCondition
        {
            get;
            set;
        }
        public string TenderWay
        {
            get;
            set;
        }
        public string PayWay
        {
            get;
            set;
        }
        public string KeyPart
        {
            get;
            set;
        }
        public string WorkUnit
        {
            get;
            set;
        }
        public string WorkUnitName
        {
            get
            {
                return this.WorkUnit;
            }
        }
        public string PrjManager
        {
            get;
            set;
        }
        public string PrjManagerName
        {
            get
            {
                return this.PrjManager;
            }
        }
        public string BuildingType
        {
            get;
            set;
        }
        public int BuildingTypeNo
        {
            get;
            set;
        }
        public string TotalHouseNum
        {
            get;
            set;
        }
        public string BuildingArea
        {
            get;
            set;
        }
        public string UsegrounArea
        {
            get;
            set;
        }
        public string UndergroundArea
        {
            get;
            set;
        }
        public string AfforestArea
        {
            get;
            set;
        }
        public string ParkArea
        {
            get;
            set;
        }
        public string PrjFundInfo
        {
            get;
            set;
        }
        public string OtherStatement
        {
            get;
            set;
        }
        public string InputUser
        {
            get;
            set;
        }
        public string ProjPeopleCode
        {
            get;
            set;
        }
        public string ProjPeopleUserName
        {
            get;
            set;
        }
        public string ProjPeopleCorp
        {
            get;
            set;
        }
        public string ProjPeopleDuty
        {
            get;
            set;
        }
        public string ProjPeopleTel
        {
            get;
            set;
        }
        public string ProjInfoOrigin
        {
            get;
            set;
        }
        public string ProjElseRequest
        {
            get;
            set;
        }
        public System.DateTime? ProjApplyDate
        {
            get;
            set;
        }
        public System.DateTime? ProjTenderDate
        {
            get;
            set;
        }
        public System.DateTime? ProjApprovalDate
        {
            get;
            set;
        }
        public int? ProjRegistDeadline
        {
            get;
            set;
        }
        public string ProgAgent
        {
            get;
            set;
        }
        public string ProgAgentName
        {
            get
            {
                return this.GetUserName(this.ProgAgent);
            }
        }
        public System.DateTime? ProjStartDate
        {
            get;
            set;
        }
        public string BusinessManager
        {
            get;
            set;
        }
        public string BusinessManagerName
        {
            get
            {
                return this.GetUserName(this.BusinessManager);
            }
        }
        public string ProjStartRemark
        {
            get;
            set;
        }
        public System.DateTime? ProjTenderBeginDate
        {
            get;
            set;
        }
        public decimal? TenderCeilingPrice
        {
            get;
            set;
        }
        public string TenderUnit
        {
            get;
            set;
        }
        public decimal? TenderQuote
        {
            get;
            set;
        }
        public string TenderAppraiseMethod
        {
            get;
            set;
        }
        public decimal? TenderAverage
        {
            get;
            set;
        }
        public string ProjTenderCostContent
        {
            get;
            set;
        }
        public System.DateTime? ProjTenderAnswerDate
        {
            get;
            set;
        }
        public decimal? ProjTenderEarnestMoney
        {
            get;
            set;
        }
        public string ProjTenderPayWay
        {
            get;
            set;
        }
        public string ProjTenderContent
        {
            get;
            set;
        }
        public string ProjTenderRemark
        {
            get;
            set;
        }
        public System.DateTime? SuccessBidDate
        {
            get;
            set;
        }
        public decimal? SuccessBidPrice
        {
            get;
            set;
        }
        public string SuccessBidRemark
        {
            get;
            set;
        }
        public System.DateTime? OutBidDate
        {
            get;
            set;
        }
        public bool? OutBidIsReturn
        {
            get;
            set;
        }
        public string OutBidRemark
        {
            get;
            set;
        }
        public int ProjFlowSate
        {
            get;
            set;
        }
        public string EngineeringType
        {
            get;
            set;
        }
        public string EngineeringSubType
        {
            get;
            set;
        }
        public string Grade
        {
            get;
            set;
        }
        public System.Collections.Generic.IList<EngineeringType> EngineeringTypes
        {
            get
            {
                if (this.engineeringTypes == null)
                {
                    this.engineeringTypes = cn.justwin.Tender.EngineeringType.GetByPrjGuid(this.PrjGuid.ToString());
                }
                return this.engineeringTypes;
            }
            set
            {
                this.engineeringTypes = value;
            }
        }
        public string Telephone
        {
            get;
            set;
        }
        public System.DateTime? InputDate
        {
            get;
            set;
        }
        public System.DateTime? ActualRunDate
        {
            get;
            set;
        }
        public string PrjDutyPerson
        {
            get;
            set;
        }
        public string PrjDutyPersonName
        {
            get
            {
                return this.GetUserName(this.PrjDutyPerson);
            }
        }
        public string PrjApprovalOf
        {
            get;
            set;
        }
        public string PrjFundWorkable
        {
            get;
            set;
        }
        public decimal? ForecastProfitRate
        {
            get;
            set;
        }
        public string EngineeringEstimates
        {
            get;
            set;
        }
        public decimal? ManagementMargin
        {
            get;
            set;
        }
        public decimal? MigrantQualityMarginRate
        {
            get;
            set;
        }
        public decimal? WithholdingTaxRate
        {
            get;
            set;
        }
        public decimal? PerformanceBond
        {
            get;
            set;
        }
        public decimal? ElseMargin
        {
            get;
            set;
        }
        public string PrjManagerRequire
        {
            get;
            set;
        }
        public string TechnicalLeaderRequire
        {
            get;
            set;
        }
        public string PrequalificationRequire
        {
            get;
            set;
        }
        public System.DateTime? QualificationPassDate
        {
            get;
            set;
        }
        public string QualificationPassReason
        {
            get;
            set;
        }
        public System.DateTime? QualificationFailData
        {
            get;
            set;
        }
        public string QualificationFailReason
        {
            get;
            set;
        }
        public string Province
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string PrjProperty
        {
            get;
            set;
        }
        public string PrjReadOne
        {
            get;
            set;
        }
        public decimal QualificationMargin
        {
            get;
            set;
        }
        public string QualificationReadOne
        {
            get;
            set;
        }
        public System.DateTime? TenderProspect
        {
            get;
            set;
        }
        public string TenderReadOne
        {
            get;
            set;
        }
        public System.Collections.Generic.IList<ProjectKind> PrjKinds
        {
            get
            {
                if (this.prjKinds == null)
                {
                    this.prjKinds = ProjectKind.GetProjectKinds(this.PrjGuid);
                }
                return this.prjKinds;
            }
            set
            {
                this.prjKinds = value;
            }
        }
        public System.Collections.Generic.IList<ProjectRank> PrjRanks
        {
            get
            {
                if (this.prjRanks == null)
                {
                    this.prjRanks = ProjectRank.GetRanks(this.PrjGuid);
                }
                return this.prjRanks;
            }
            set
            {
                this.prjRanks = value;
            }
        }
        public static void Del(string id)
        {
            System.Guid guid = new System.Guid(id);
            using (pm2Entities context = new pm2Entities())
            {
                PT_PrjInfo_ZTB prjZTB = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjGuid == guid
                    select m).FirstOrDefault<PT_PrjInfo_ZTB>();
                context.DeleteObject(prjZTB);
                if ((
                    from m in context.PT_PrjInfo
                    where m.PrjGuid == (System.Guid?)guid
                    select m).FirstOrDefault<PT_PrjInfo>() == null)
                {
                    PT_PrjInfo_ZTB_Detail prjZTB_Detail = (
                        from m in context.PT_PrjInfo_ZTB_Detail
                        where m.PrjGuid == guid
                        select m).FirstOrDefault<PT_PrjInfo_ZTB_Detail>();
                    context.DeleteObject(prjZTB_Detail);
                }
                cn.justwin.Tender.EngineeringType.Delete(id, context);
                PTPrjInfoZTBUserService userSer = new PTPrjInfoZTBUserService();
                userSer.Delete(id);
                context.SaveChanges();
            }
        }
        public static TenderInfo GetById(string id)
        {
            TenderInfo tender = null;
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid prjGuid = new System.Guid(id);
                tender = (
                    from m in context.PT_PrjInfo_ZTB
                    join d in context.PT_PrjInfo_ZTB_Detail on m.PrjGuid equals d.PrjGuid
                    where m.PrjGuid == prjGuid && d.PrjGuid == prjGuid
                    select new TenderInfo
                    {
                        Area = m.Area,
                        BudgetWay = m.BudgetWay,
                        BuildingArea = m.BuildingArea,
                        BuildingType = m.BuildingType,
                        BuildingTypeNo = d.BuildingTypeNo ?? 0,
                        ComputeClass = m.ComputeClass,
                        ContractSum = m.ContractSum,
                        ContractWay = m.ContractWay,
                        Counsellor = m.Counsellor,
                        Designer = m.Designer,
                        Duration = m.Duration,
                        EndDate = m.EndDate,
                        FileName = m.FileName,
                        FileURL = m.FileURL,
                        Inspector = m.Inspector,
                        KeyPart = m.KeyPart,
                        MarketInfoGuid = m.MarketInfoGuid,
                        OtherStatement = m.OtherStatement,
                        PrjCode = m.PrjCode,
                        PrjCost = m.PrjCost,
                        PrjFundInfo = m.PrjFundInfo,
                        OutBidIsReturn = d.OutBidIsReturn,
                        OutBidDate = d.OutBidDate,
                        OutBidRemark = d.OutBidRemark,
                        OwnerCode = m.OwnerCode,
                        OwnerLinkManName = d.OwnerLinkMan,
                        OwnerLinkPhone = d.OwnerLinkPhone,
                        OwnerAddress = d.OwnerAddress,
                        PayCondition = m.PayCondition,
                        PayWay = m.PayWay,
                        PrjGuid = m.PrjGuid,
                        PrjInfo = m.PrjInfo,
                        PrjKindClass = m.PrjKindClass,
                        PrjManager = m.PrjManager,
                        PrjName = m.PrjName,
                        PrjPlace = m.PrjPlace,
                        PrjState = m.PrjState ?? 0,
                        QualityClass = m.QualityClass,
                        Rank = m.Rank,
                        RationClass = m.RationClass,
                        Remark = m.Remark,
                        StartDate = m.StartDate,
                        TenderWay = m.TenderWay,
                        TotalHouseNum = m.TotalHouseNum,
                        UndergroundArea = m.UndergroundArea,
                        UsegrounArea = m.UsegrounArea,
                        WorkUnit = m.WorkUnit,
                        ProjFlowSate = d.ProjFlowSate ?? -1,
                        ProjPeopleUserName = d.ProjPeopleName,
                        ProjPeopleCorp = d.ProjPeopleDep,
                        ProjPeopleDuty = d.ProjPeopleDuty,
                        ProjPeopleTel = d.ProjPeopleTel,
                        ProjStartDate = d.ProjStartDate,
                        ProjStartRemark = d.ProjStartRemark,
                        ProjTenderAnswerDate = d.ProjTenderAnswerDate,
                        ProjTenderBeginDate = d.ProjTenderBeginDate,
                        ProjTenderContent = d.ProjTenderContent,
                        ProjTenderCostContent = d.ProjTenderCostContent,
                        ProjTenderEarnestMoney = d.ProjTenderEarnestMoney,
                        ProjTenderPayWay = d.ProjTenderPayWay,
                        ProjTenderRemark = d.ProjTenderRemark,
                        SuccessBidDate = d.SuccessBidDate,
                        SuccessBidPrice = d.SuccessBidPrice,
                        SuccessBidRemark = d.SuccessBidRemark,
                        BusinessManager = d.BusinessManager,
                        ProgAgent = d.ProgAgent,
                        ProjApplyDate = d.ProjApplyDate,
                        ProjApprovalDate = d.ProjApprovalDate,
                        ProjElseRequest = d.ProjElseRequest,
                        ProjInfoOrigin = d.ProjInfoOrigin,
                        ProjRegistDeadline = d.ProjRegistDeadline,
                        ProjTenderDate = d.ProjTenderDate,
                        TenderAppraiseMethod = d.TenderAppraiseMethod,
                        TenderAverage = d.TenderAverage,
                        TenderCeilingPrice = d.TenderCeilingPrice,
                        TenderQuote = d.TenderQuote,
                        TenderUnit = d.TenderUnit,
                        Telephone = d.Telephone,
                        Grade = d.Grade,
                        EngineeringType = d.EngineeringType,
                        EngineeringSubType = d.EngineeringSubType,
                        InputDate = d.InputDate,
                        InputUser = d.InputUser,
                        ActualRunDate = d.ActualRunDate,
                        PrjDutyPerson = d.PrjDutyPerson,
                        PrjApprovalOf = d.PrjApprovalOf,
                        PrjFundWorkable = d.PrjFundWorkable,
                        ForecastProfitRate = d.ForecastProfitRate,
                        EngineeringEstimates = d.EngineeringEstimates,
                        ManagementMargin = d.ManagementMargin,
                        MigrantQualityMarginRate = d.MigrantQualityMarginRate,
                        WithholdingTaxRate = d.WithholdingTaxRate,
                        PerformanceBond = d.PerformanceBond,
                        ElseMargin = d.ElseMargin,
                        PrjManagerRequire = d.PrjManagerRequire,
                        TechnicalLeaderRequire = d.TechnicalLeaderRequire,
                        PrequalificationRequire = d.PrequalificationRequire,
                        QualificationPassDate = d.QualificationPassDate,
                        QualificationPassReason = d.QualificationPassReason,
                        QualificationFailData = d.QualificationFailData,
                        QualificationFailReason = d.QualificationFailReason,
                        Province = d.Province,
                        City = d.City,
                        PrjProperty = d.PrjProperty,
                        PrjReadOne = d.PrjReadOne,
                        QualificationMargin = d.QualificationMargin,
                        QualificationReadOne = d.QualificationReadOne,
                        TenderProspect = d.TenderProspect,
                        TenderReadOne = d.TenderReadOne,
                        AfforestArea = d.AfforestArea,
                        ParkArea = d.ParkArea
                    }).FirstOrDefault<TenderInfo>();
            }
            return tender;
        }
        public static DataTable GetPartTender(string guid, string part, string userCode)
        {
            DataTable dtPart = new DataTable();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.Append("\r\n\r\n--DECLARE @prjGuid VARCHAR(100),@userCode VARCHAR(100)\r\n--SET @prjGuid='0B4A47C2-681F-480F-ACA5-0492FBF0A78A'\r\n--SET @userCode='00000000'\r\n\r\nSELECT \r\nTender.PrjManager --项目经理\r\n,Tender.PrjManager AS PrjManagerName --项目经理");
            if (part == ProjectParameter.Initiate || part == ProjectParameter.InitiateFail)
            {
                strSql.Append("\r\n--启动\r\n,TDetail.ProjStartDate  --报名通过时间\r\n,TDetail.ProjApplyDate --报名时间\r\n--,TDetail.BusinessManager --业务跟进人\r\n--,Yh2.v_xm AS BusinessManagerName --业务跟进人\r\n,TDetail.PrjDutyPerson --项目项目跟踪人 \r\n,Yh4.v_xm AS PrjDutyPersonName --项目跟踪人名称\r\n,TDetail.ProjStartRemark --启动备注");
            }
            else if (part == ProjectParameter.Bid)
            {
                strSql.Append("\r\n--投标\r\n,TDetail.ProjTenderBeginDate --开标时间\r\n,TDetail.TenderCeilingPrice --最高限价\r\n,TDetail.TenderUnit --单位\r\n,TDetail.TenderQuote --报价\r\n,TDetail.TenderAppraiseMethod --评比方法\r\n,TDetail.TenderAverage --平均价\r\n,TDetail.ProjTenderCostContent --投标现场费内容\r\n,TDetail.ProjTenderAnswerDate --投标答疑时间\r\n,TDetail.ProjTenderEarnestMoney --投标保证金\r\n,TDetail.ProjTenderPayWay --保证金缴纳方式\r\n,TDetail.ProjTenderContent --投标标书内容\r\n,TDetail.ProjTenderRemark   --投标备注\r\n,TDetail.TenderProspect     --现场勘察时间\r\n,TDetail.TenderReadOne      --投标阅知人\r\n,(SELECT v_xm FROM PT_yhmc WHERE v_yhdm = TDetail.TenderReadOne) AS TReadOneName --投标阅知人名称\r\n                ");
            }
            else if (part == ProjectParameter.WinBid)
            {
                strSql.Append("\r\n--中标\r\n,TDetail.SuccessBidDate --中标时间\r\n,TDetail.SuccessBidPrice --中标价格\r\n,TDetail.SuccessBidRemark --中标备注");
            }
            else if (part == ProjectParameter.OutBid)
            {
                strSql.Append("\r\n--落标\r\n,TDetail.OutBidDate --落标时间\r\n,TDetail.OutBidIsReturn  --落标保证金是否退取\r\n,TDetail.OutBidRemark  --落标备注");
            }
            else if (part == ProjectParameter.Prequalification)
            {
                strSql.Append("\r\n--资格预审\r\n,TDetail.ProjApplyDate --报名日期\r\n,TDetail.ProjApprovalDate --预审日期\r\n,TDetail.ProjTenderDate  --投标日期\r\n,TDetail.ProjRegistDeadline --登记期限\r\n,TDetail.PrequalificationRequire --资格预审要求\r\n,TDetail.ProgAgent --经办人\r\n,TDetail.QualificationMargin    --预审保证金\r\n,TDetail.QualificationReadOne   --预审阅知人\r\n,(SELECT v_xm FROM PT_yhmc WHERE v_yhdm = TDetail.QualificationReadOne) AS QReadOneName --预审阅知人名称\r\n,Yh3.v_xm AS ProgAgentName --经办人名称\r\n--,TDetail.PrjDutyPerson --项目责任人 \r\n--,Yh4.v_xm AS PrjDutyPersonName --项目责任人名称");
            }
            else if (part == ProjectParameter.QualificationPass)
            {
                strSql.Append("\r\n--预审通过\r\n,TDetail.ProjApplyDate --报名日期\r\n,TDetail.ProjApprovalDate --预审日期\r\n,TDetail.ProjTenderDate  --投标日期\r\n,TDetail.ProjRegistDeadline --登记期限\r\n,TDetail.PrequalificationRequire --资格预审要求\r\n,TDetail.ProgAgent --经办人\r\n,TDetail.QualificationMargin    --预审保证金\r\n,Yh3.v_xm AS ProgAgentName --经办人名称\r\n--,TDetail.PrjDutyPerson --项目责任人 \r\n--,Yh4.v_xm AS PrjDutyPersonName --项目责任人名称\r\n,TDetail.QualificationPassDate --预审通过日期\r\n,TDetail.QualificationReadOne  --预审阅知人\r\n,(SELECT v_xm FROM PT_yhmc WHERE v_yhdm = TDetail.QualificationReadOne) AS QReadOneName --预审阅知人名称\r\n,TDetail.QualificationPassReason --说明");
            }
            else if (part == ProjectParameter.QualificationFail)
            {
                strSql.Append("\r\n--预审失败\r\n,TDetail.ProjApplyDate --报名日期\r\n,TDetail.ProjApprovalDate --预审日期\r\n,TDetail.ProjTenderDate  --投标日期\r\n,TDetail.ProjRegistDeadline --登记期限\r\n,TDetail.PrequalificationRequire --资格预审要求\r\n,TDetail.ProgAgent --经办人\r\n,TDetail.QualificationMargin    --预审保证金\r\n,TDetail.TenderProspect         --现场勘查时间 \r\n,Yh3.v_xm AS ProgAgentName --经办人名称\r\n--,TDetail.PrjDutyPerson --项目责任人 \r\n--,Yh4.v_xm AS PrjDutyPersonName --项目责任人名称\r\n,TDetail.QualificationFailData --预审失败日期\r\n,TDetail.QualificationReadOne --预审阅知人\r\n,(SELECT v_xm FROM PT_yhmc WHERE v_yhdm = TDetail.QualificationReadOne) AS QReadOneName --预审阅知人名称\r\n,TDetail.QualificationFailReason --理由");
            }
            strSql.Append("\r\nFROM PT_PrjInfo_ZTB AS Tender\r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\n--JOIN PT_PrjInfo_ZTB_User AS Limit ON Tender.PrjGuid=Limit.PrjGuid\r\n--LEFT JOIN PT_yhmc AS Yh1 ON Tender.PrjManager=Yh1.v_yhdm --项目经理\r\nLEFT JOIN PT_yhmc AS Yh2 ON TDetail.BusinessManager=Yh2.v_yhdm --启动业务跟进人\r\nLEFT JOIN PT_yhmc AS Yh3 ON TDetail.ProgAgent=Yh3.v_yhdm --经办人\r\nLEFT JOIN PT_yhmc AS Yh4 ON TDetail.PrjDutyPerson=Yh4.v_yhdm --项目责任人\r\nWHERE Tender.PrjGuid=@prjGuid --AND Limit.UserCode=@userCode\r\n");
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@prjGuid", guid),
				new SqlParameter("@userCode", userCode)
			};
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        public static string GetPrjState(string prjGuid)
        {
            string StateName = string.Empty;
            System.Guid guid = new System.Guid(prjGuid);
            using (pm2Entities context = new pm2Entities())
            {
                StateName = (
                    from m in context.PT_PrjInfo
                    join n in context.Basic_CodeList on m.PrjState equals (int?)n.ItemCode
                    where n.TypeCode == "ProjectState" && m.PrjGuid == (System.Guid?)guid
                    select n.ItemName).FirstOrDefault<string>();
            }
            return StateName;
        }
        public static bool IsSameCode(string prjCode)
        {
            int count = 0;
            using (pm2Entities context = new pm2Entities())
            {
                count = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjCode == prjCode
                    select m).Count<PT_PrjInfo_ZTB>();
            }
            return count > 0;
        }
        public static bool IsSameName(string prjName)
        {
            int tenderCount = 0;
            int prjInfoCount = 0;
            using (pm2Entities context = new pm2Entities())
            {
                tenderCount = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjName == prjName
                    select m).Count<PT_PrjInfo_ZTB>();
                prjInfoCount = (
                    from m in context.PT_PrjInfo
                    where m.PrjName == prjName
                    select m).Count<PT_PrjInfo>();
            }
            return tenderCount > 0 || prjInfoCount > 0;
        }
        public static bool UpdateIsSameCode(string oldCode, string newCode)
        {
            int count = 0;
            using (pm2Entities context = new pm2Entities())
            {
                count = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjCode != oldCode && m.PrjCode == newCode
                    select m).Count<PT_PrjInfo_ZTB>();
            }
            return count > 0;
        }
        public static bool UpdateIsSameName(string oldName, string newName)
        {
            int tenderCount = 0;
            int prjInfoCount = 0;
            using (pm2Entities context = new pm2Entities())
            {
                tenderCount = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjName != oldName && m.PrjName == newName
                    select m).Count<PT_PrjInfo_ZTB>();
                prjInfoCount = (
                    from m in context.PT_PrjInfo
                    where m.PrjName != oldName && m.PrjName == newName
                    select m).Count<PT_PrjInfo>();
            }
            return tenderCount > 0 || prjInfoCount > 0;
        }
        public static string GetProjectName(string prjGuid)
        {
            string prjName = string.Empty;
            System.Guid guid = new System.Guid(prjGuid);
            using (pm2Entities context = new pm2Entities())
            {
                prjName = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjGuid == guid
                    select m.PrjName).FirstOrDefault<string>();
            }
            return prjName;
        }
        public static DataTable GetDtblProjectCode(string tableName, string prjCode)
        {
            DataTable prjDt = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, string.Concat(new string[]
			{
				"SELECT PrjCode FROM ",
				tableName,
				" WHERE PrjCode like '%",
				prjCode,
				"%'"
			}), null);
        }
        public static string GetProjectState(string prjId)
        {
            string result;
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid guid = new System.Guid(prjId);
                string prjState = string.Empty;
                int? state = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjGuid == guid
                    select m.PrjState).FirstOrDefault<int?>();
                if (state.HasValue)
                {
                    prjState = state.ToString();
                }
                result = prjState;
            }
            return result;
        }
        public static int GetCount(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType, string OldState, string flowStateName)
        {
            object result = TenderInfo.GetCountOrSumTotal(prjName, prjCode, buildUnit, prjKindClass, start, end, prjState, flowState, userCode, person, personType, "COUNT(*)", OldState, flowStateName);
            return (result == null) ? 0 : System.Convert.ToInt32(result);
        }
        public static int GetCountAtGiveUp(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType)
        {
            object result = TenderInfo.GetCountOrSumTotalAtGiveUp(prjName, prjCode, buildUnit, prjKindClass, start, end, prjState, flowState, userCode, person, personType, "COUNT(*)");
            return (result == null) ? 0 : System.Convert.ToInt32(result);
        }
        public static DataTable GetAllAtGiveUp(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, bool isDisplayFlowState, int personType, int pageNo, int pageSize)
        {
            DataTable dtSource = new DataTable();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            strSql.Append("\r\nSELECT * FROM(\r\nSELECT ROW_NUMBER() OVER (ORDER BY TDetail.InputDate DESC ) AS No\r\n,Tender.PrjGuid                  --项目Guid\r\n,Tender.PrjCode                  --项目编码\r\n,Tender.PrjName                  --项目名称\r\n,prjType.CodeName AS PrjTypeName --项目类型\r\n,Corp.CorpName AS WorkUnitName  --建设单位名称\r\n --立项申请时间\r\n,CONVERT(VARCHAR(100), TDetail.InputDate,23) AS InputDate");
            if (personType == 1)
            {
                strSql.Append(",TDetail.ProjPeopleName AS  Person");
            }
            else if (personType == 3)
            {
                strSql.Append(",Tender.PrjManager AS Person");
            }
            else
            {
                strSql.Append(",YHMC.v_xm AS Person ");
            }
            strSql.Append("\r\n,Tender.PrjState                --项目状态编码\r\n--,CASE ISNULL(CListPrjInfo.ItemName,'')\r\n\t--WHEN '' THEN  CListZTB.ItemName\r\n   -- ELSE\r\n        --CASE PrjInfo.PrjState\r\n            --WHEN '5' THEN CListZTB.ItemName\r\n        --ELSE\r\n           -- CASE Tender.PrjState\t\r\n\t\t\t   -- WHEN '5' THEN(CListZTB.ItemName+'('+CListPrjInfo.ItemName+')')\r\n               -- ELSE\r\n                  --  CListZTB.ItemName\r\n           -- END\r\n       -- END\t\r\n--END\r\n  ,CASE CListZTB.ItemCode\r\n\t\t            WHEN '5' THEN \r\n\t\t\t            CASE WHEN CListPrjInfo.ItemCode in(7,8,9,10,11,12,13,17) \r\n\t\t\t\t            THEN CListZTB.ItemName + '(' + CListPrjInfo.ItemName + ')'\r\n\t\t\t\t            ELSE CListZTB.ItemName\r\n\t\t\t            END                             \r\n\t\t            ELSE \r\n\t\t\t            CListZTB.ItemName\r\n\t            END\r\nAS StateText                    --项目状态Text\r\n,PrjInfo.PrjState AS PrjInfoState --项目开工状态\r\n,Tender.Duration                --工程工期");
            if (isDisplayFlowState)
            {
                strSql.Append("\r\n--流程状态\r\n,ISNULL(TDetail.ProjFlowSate,-1) AS   ProjFlowSate ");
            }
            strSql.Append("\r\n--,Tender.StartDate               --开始日期\r\n--,Tender.EndDate                 --结束日期\r\n,Tender.OldState                --放弃状态\r\n,Tender.InitiateFlowState       --报名状态\r\n,Tender.PftFlowState            --资格预审流程状态\r\n,Tender.BidFlowState            --中标流程状态   \r\n,Tender.GiveUpFlowState         --放弃流程状态\r\n,Tender.ChangeFlowSate          --状态变更流程状态\r\n,Tender.IsGiveUp                --是否放弃\r\n,CONVERT(DECIMAL(18,3),ISNULL(Tender.PrjCost,'0.000')) AS PrjCost  --造价 \r\nFROM PT_PrjInfo_ZTB AS Tender\r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\nLEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\nLEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n) AS prjType ON Tender.PrjKindClass=CONVERT(VARCHAR(100),prjType.CodeID)\r\nLEFT JOIN PT_PrjInfo AS PrjInfo ON  Tender.PrjGuid=PrjInfo.PrjGuid\r\nLEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS CListZTB ON Tender.PrjState=CListZTB.ItemCode\r\nLEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS CListPrjInfo ON PrjInfo.PrjState=CListPrjInfo.ItemCode");
            if (personType != 1)
            {
                if (personType == 2)
                {
                    strSql.Append("\r\nLEFT JOIN PT_yhmc AS YHMC ON TDetail.BusinessManager =YHMC.v_yhdm");
                }
                else if (personType != 3 && personType == 4)
                {
                    strSql.Append("\r\nLEFT JOIN PT_yhmc AS YHMC ON TDetail.PrjDutyPerson=YHMC.v_yhdm");
                }
            }
            strSql.Append("\r\nWHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                strSql.Append("\r\nAND Tender.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjCode))
            {
                strSql.Append("\r\nAND Tender.PrjCode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjKindClass))
            {
                strSql.Append(" AND Tender.PrjGuid IN ( \n").Append("\tSELECT PrjGuid FROM PT_PrjInfo_Kind \n").AppendFormat("\tWHERE PrjGuid = Tender.PrjGuid AND PrjKind = @prjKindClass \n", new object[0]).Append(" ) \n");
                parameters.Add(new SqlParameter("@prjKindClass", prjKindClass));
            }
            if (!string.IsNullOrWhiteSpace(buildUnit))
            {
                strSql.Append("\r\nAND Corp.CorpName LIKE '%'+ @buildUnit + '%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit.Trim()));
            }
            strSql.Append(" AND ((TDetail.ProjFlowSate in(-1,-2,-3) and Tender.InitiateFlowState=-1 and Tender.PftFlowState=-1) or (TDetail.ProjFlowSate=1 and Tender.InitiateFlowState in(-1,-2,-3) and Tender.PftFlowState=-1) or (TDetail.ProjFlowSate=1 and Tender.InitiateFlowState=1 and Tender.PftFlowState in(-1,-2,-3)) or (TDetail.ProjFlowSate=1 and Tender.InitiateFlowState=1 and Tender.PftFlowState=1)) ");
            if (prjState != null && prjState.Count > 0)
            {
                string search = TenderInfo.GetSearchAtGiveUp(prjState);
                strSql.AppendFormat("  AND Tender.PrjState IN ({0}) ", search);
            }
            if (!string.IsNullOrWhiteSpace(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrWhiteSpace(start))
            {
                strSql.Append("\r\nAND TDetail.InputDate >= @start ");
                parameters.Add(new SqlParameter("@start", start.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                strSql.Append("\r\nAND  TDetail.InputDate <= @end ");
                parameters.Add(new SqlParameter("@end", end.Trim() + " 23:59:59"));
            }
            if (personType == 1 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND  TDetail.ProjPeopleName LIKE'%'+@setUpPerson+'%'");
                parameters.Add(new SqlParameter("@setUpPerson", person.Trim()));
            }
            if (personType == 2 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND  YHMC.v_xm LIKE '%'+@businessManager+'%'");
                parameters.Add(new SqlParameter("@businessManager", person.Trim()));
            }
            if (personType == 3 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND Tender.PrjManager LIKE '%'+@prjManager+'%'");
                parameters.Add(new SqlParameter("@prjManager", person.Trim()));
            }
            if (personType == 4 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND YHMC.v_xm LIKE '%'+@prjDuty+'%'");
                parameters.Add(new SqlParameter("@prjDuty", person.Trim()));
            }
            strSql.Append("\r\n)AS T WHERE T.No BETWEEN (@pageNo-1)*@pageSize+1 AND @pageNo*@pageSize ");
            parameters.Add(new SqlParameter("@pageNo", pageNo));
            parameters.Add(new SqlParameter("@pageSize", pageSize));
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), parameters.ToArray());
        }
        public static decimal GetSumTotalAtGiveUp(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType)
        {
            object result = TenderInfo.GetCountOrSumTotalAtGiveUp(prjName, prjCode, buildUnit, prjKindClass, start, end, prjState, flowState, userCode, person, personType, "ISNULL(SUM(ISNULL(PrjCost,0)),0)");
            return (result == null) ? 0m : decimal.Parse(result.ToString());
        }
        private static object GetCountOrSumTotalAtGiveUp(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType, string expression)
        {
            prjName = prjName.Trim();
            prjCode = prjCode.Trim();
            buildUnit = buildUnit.Trim();
            start = start.Trim();
            end = end.Trim();
            person = person.Trim();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            strSql.Append("\r\n                    SELECT " + expression + "\r\n                FROM PT_PrjInfo_ZTB AS Tender\r\n                LEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\n                LEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\n                LEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n                ) AS prjType ON Tender.PrjKindClass=CONVERT(VARCHAR(100),prjType.CodeID)");
            if (personType != 1)
            {
                if (personType == 2)
                {
                    strSql.Append(" LEFT JOIN PT_yhmc AS YHMC ON TDetail.BusinessManager =YHMC.v_yhdm");
                }
                else if (personType != 3 && personType == 4)
                {
                    strSql.Append(" LEFT JOIN PT_yhmc AS YHMC ON TDetail.PrjDutyPerson =YHMC.v_yhdm ");
                }
            }
            strSql.Append(" WHERE 1=1");
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                strSql.Append(" AND Tender.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName));
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                strSql.Append(" AND Tender.PrjCode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode));
            }
            if (!string.IsNullOrEmpty(prjKindClass))
            {
                strSql.Append(" AND Tender.PrjGuid IN ( \n").Append("\tSELECT PrjGuid FROM PT_PrjInfo_Kind \n").AppendFormat("\tWHERE PrjGuid = Tender.PrjGuid AND PrjKind = @prjKindClass \n", new object[0]).Append(" ) \n");
                parameters.Add(new SqlParameter("@prjKindClass", prjKindClass));
            }
            if (!string.IsNullOrEmpty(buildUnit))
            {
                strSql.Append(" AND Corp.CorpName LIKE '%'+@buildUnit +'%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit));
            }
            strSql.Append(" AND ((TDetail.ProjFlowSate in(-1,-2,-3) and Tender.InitiateFlowState=-1 and Tender.PftFlowState=-1) or (TDetail.ProjFlowSate=1 and Tender.InitiateFlowState in(-1,-2,-3) and Tender.PftFlowState=-1) or (TDetail.ProjFlowSate=1 and Tender.InitiateFlowState=1 and Tender.PftFlowState in(-1,-2,-3)) or (TDetail.ProjFlowSate=1 and Tender.InitiateFlowState=1 and Tender.PftFlowState=1)) ");
            if (prjState != null && prjState.Count > 0)
            {
                string search = TenderInfo.GetSearchAtGiveUp(prjState);
                strSql.AppendFormat(" AND Tender.PrjState IN ({0}) ", search);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrEmpty(start))
            {
                strSql.Append(" AND  TDetail.InputDate >= @start ");
                parameters.Add(new SqlParameter("@start", start));
            }
            if (!string.IsNullOrEmpty(end))
            {
                strSql.Append(" AND TDetail.InputDate <= @end ");
                parameters.Add(new SqlParameter("@end", end + " 23:59:59"));
            }
            if (personType == 1 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND  TDetail.ProjPeopleName LIKE'%'+@setUpPerson+'%'", person);
                parameters.Add(new SqlParameter("@setUpPerson", person));
            }
            if (personType == 2 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND  YHMC.v_xm LIKE '%'+@businessManager+'%'", person);
                parameters.Add(new SqlParameter("@businessManager", person));
            }
            if (personType == 3 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND Tender.PrjManager LIKE '%'+@prjManager+'%'", person);
                parameters.Add(new SqlParameter("@prjManager", person));
            }
            if (personType == 4 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND YHMC.v_xm LIKE '%'+@prjDuty+'%'", person);
                parameters.Add(new SqlParameter("@prjDuty", person));
            }
            return SqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters.ToArray());
        }
        public static int GetCountAtChgState(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType, string flowStateField)
        {
            prjName = prjName.Trim();
            prjCode = prjCode.Trim();
            buildUnit = buildUnit.Trim();
            start = start.Trim();
            end = end.Trim();
            person = person.Trim();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.Append("\r\n                SELECT COUNT(*)\r\n                FROM PT_PrjInfo_ZTB AS Tender\r\n                LEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\n                LEFT JOIN PT_PrjInfo_StateChange AS PrjInfo_StateChange ON Tender.PrjGuid=PrjInfo_StateChange.PrjId AND PrjInfo_StateChange.flowState<>1\r\n                LEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\n                LEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n                ) AS prjType ON Tender.PrjKindClass=CONVERT(VARCHAR(100),prjType.CodeID)");
            string sqlConditon = TenderInfo.GetTenderSqlCondition(prjName, prjCode, buildUnit, prjKindClass, start, end, prjState, flowState, userCode, person, personType, flowStateField);
            strSql.Append(sqlConditon);
            object result = SqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), new SqlParameter[0]);
            return DBHelper.GetInt(result);
        }
        public static decimal GetSumTotalAtChgState(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType, string flowStateField)
        {
            prjName = prjName.Trim();
            prjCode = prjCode.Trim();
            buildUnit = buildUnit.Trim();
            start = start.Trim();
            end = end.Trim();
            person = person.Trim();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.Append("\r\n                SELECT ISNULL(SUM(ISNULL(PrjCost,0)),0)\r\n                FROM PT_PrjInfo_ZTB AS Tender\r\n                LEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\n                LEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\n                LEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n                ) AS prjType ON Tender.PrjKindClass=CONVERT(VARCHAR(100),prjType.CodeID)");
            string sqlConditon = TenderInfo.GetTenderSqlCondition(prjName, prjCode, buildUnit, prjKindClass, start, end, prjState, flowState, userCode, person, personType, flowStateField);
            strSql.Append(sqlConditon);
            object result = SqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), new SqlParameter[0]);
            return DBHelper.GetDecimal(result);
        }
        public static decimal GetSumTotal(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType, string OldState, string flowStateName)
        {
            object result = TenderInfo.GetCountOrSumTotal(prjName, prjCode, buildUnit, prjKindClass, start, end, prjState, flowState, userCode, person, personType, "ISNULL(SUM(ISNULL(PrjCost,0)),0)", OldState, flowStateName);
            return (result == null) ? 0m : decimal.Parse(result.ToString());
        }
        public static DataTable GetAllAtChgState(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, bool isDisplayFlowState, int personType, int pageNo, int pageSize, string flowStateField)
        {
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.Append("\r\n                    SELECT * FROM(\r\n                    SELECT ROW_NUMBER() OVER (ORDER BY TDetail.InputDate DESC ) AS No\r\n                    ,Tender.PrjGuid                  --项目Guid\r\n                    ,Tender.PrjCode                  --项目编码\r\n                    ,Tender.PrjName                  --项目名称\r\n                    ,prjType.CodeName AS PrjTypeName --项目类型\r\n                    ,Corp.CorpName AS WorkUnitName  --建设单位名称\r\n                     --立项申请时间\r\n                    ,CONVERT(VARCHAR(100), TDetail.InputDate,23) AS InputDate");
            if (personType == 1)
            {
                strSql.Append(",TDetail.ProjPeopleName AS  Person");
            }
            else if (personType == 3)
            {
                strSql.Append(",Tender.PrjManager AS Person");
            }
            else
            {
                strSql.Append(",YHMC.v_xm AS Person ");
            }
            strSql.Append("\r\n                    ,Tender.PrjState                --项目状态编码\r\n             ,CASE CListZTB.ItemCode\r\n\t\t            WHEN '5' THEN \r\n\t\t\t            CASE WHEN CListPrjInfo.ItemCode in(7,8,9,10,11,12,13,17) \r\n\t\t\t\t            THEN CListZTB.ItemName + '(' + CListPrjInfo.ItemName + ')'\r\n\t\t\t\t            ELSE CListZTB.ItemName\r\n\t\t\t            END                             \r\n\t\t            ELSE \r\n\t\t\t            CListZTB.ItemName\r\n\t            END AS StateText                    --项目状态Text\r\n                    ,PrjInfo.PrjState AS PrjInfoState --项目开工状态\r\n                    ,Tender.Duration                --工程工期");
            if (isDisplayFlowState)
            {
                strSql.Append("\r\n                    --流程状态\r\n                    ,ISNULL(TDetail.ProjFlowSate,-1) AS   ProjFlowSate ");
            }
            strSql.Append("\r\n                --,Tender.StartDate               --开始日期\r\n                --,Tender.EndDate                 --结束日期\r\n                ,Tender.OldState                --放弃状态\r\n                ,Tender.InitiateFlowState       --报名状态\r\n                ,Tender.PftFlowState            --资格预审流程状态\r\n                ,Tender.BidFlowState            --中标流程状态   \r\n                ,Tender.GiveUpFlowState         --放弃流程状态\r\n                ,Tender.ChangeFlowSate          --状态变更流程状态\r\n                ,PrjInfo_StateChange.changeState as changeState --变更的项目状态\r\n                ,CONVERT(DECIMAL(18,3),ISNULL(Tender.PrjCost,'0.000')) AS PrjCost  --造价 \r\n                FROM PT_PrjInfo_ZTB AS Tender\r\n                LEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\n                LEFT JOIN PT_PrjInfo_StateChange AS PrjInfo_StateChange ON Tender.PrjGuid=PrjInfo_StateChange.PrjId AND PrjInfo_StateChange.flowState<>1\r\n                LEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\n                LEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n                ) AS prjType ON Tender.PrjKindClass=CONVERT(VARCHAR(100),prjType.CodeID)\r\n                LEFT JOIN PT_PrjInfo AS PrjInfo ON  Tender.PrjGuid=PrjInfo.PrjGuid\r\n                LEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS CListZTB ON Tender.PrjState=CListZTB.ItemCode\r\n                LEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS CListPrjInfo ON PrjInfo.PrjState=CListPrjInfo.ItemCode");
            string sqlCondition = TenderInfo.GetTenderSqlCondition(prjName, prjCode, buildUnit, prjKindClass, start, end, prjState, flowState, userCode, person, personType, flowStateField);
            strSql.Append(sqlCondition);
            strSql.AppendFormat(")AS T WHERE T.No BETWEEN ({0}-1)*{1}+1 AND {0}*{1} ", pageNo, pageSize);
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), new SqlParameter[0]);
        }
        public static DataTable GetAll(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, bool isDisplayFlowState, int personType, int pageNo, int pageSize, string OldState, string flowStateName)
        {
            DataTable dtSource = new DataTable();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            strSql.Append("\r\nSELECT * FROM(\r\nSELECT ROW_NUMBER() OVER (ORDER BY TDetail.InputDate DESC ) AS No\r\n,Tender.PrjGuid                  --项目Guid\r\n,Tender.PrjCode                  --项目编码\r\n,Tender.PrjName                  --项目名称\r\n,prjType.CodeName AS PrjTypeName --项目类型\r\n,Corp.CorpName AS WorkUnitName  --建设单位名称\r\n --立项申请时间\r\n,CONVERT(VARCHAR(100), TDetail.InputDate,23) AS InputDate");
            if (personType == 1)
            {
                strSql.Append(",TDetail.ProjPeopleName AS  Person");
            }
            else if (personType == 3)
            {
                strSql.Append(",Tender.PrjManager AS Person");
            }
            else
            {
                strSql.Append(",YHMC.v_xm AS Person ");
            }
            strSql.Append("\r\n,Tender.PrjState                --项目状态编码\r\n--,CASE ISNULL(CListPrjInfo.ItemName,'')\r\n\t--WHEN '' THEN  CListZTB.ItemName\r\n   -- ELSE\r\n        --CASE PrjInfo.PrjState\r\n            --WHEN '5' THEN CListZTB.ItemName\r\n        --ELSE\r\n           -- CASE Tender.PrjState\t\r\n\t\t\t   -- WHEN '5' THEN(CListZTB.ItemName+'('+CListPrjInfo.ItemName+')')\r\n               -- ELSE\r\n                  --  CListZTB.ItemName\r\n           -- END\r\n       -- END\t\r\n--END\r\n  ,CASE CListZTB.ItemCode\r\n\t\t            WHEN '5' THEN \r\n\t\t\t            CASE WHEN CListPrjInfo.ItemCode in(7,8,9,10,11,12,13,17) \r\n\t\t\t\t            THEN CListZTB.ItemName + '(' + CListPrjInfo.ItemName + ')'\r\n\t\t\t\t            ELSE CListZTB.ItemName\r\n\t\t\t            END                             \r\n\t\t            ELSE \r\n\t\t\t            CListZTB.ItemName\r\n\t            END\r\nAS StateText                    --项目状态Text\r\n,PrjInfo.PrjState AS PrjInfoState --项目开工状态\r\n,Tender.Duration                --工程工期");
            if (isDisplayFlowState)
            {
                strSql.Append("\r\n--流程状态\r\n,ISNULL(TDetail.ProjFlowSate,-1) AS   ProjFlowSate ");
            }
            strSql.Append("\r\n--,Tender.StartDate               --开始日期\r\n--,Tender.EndDate                 --结束日期\r\n,Tender.OldState                --放弃状态\r\n,Tender.InitiateFlowState       --报名状态\r\n,Tender.PftFlowState            --资格预审流程状态\r\n,Tender.BidFlowState            --中标流程状态   \r\n,Tender.GiveUpFlowState         --放弃流程状态\r\n,Tender.ChangeFlowSate          --状态变更流程状态\r\n,Tender.IsGiveUp                --是否放弃\r\n,CONVERT(DECIMAL(18,3),ISNULL(Tender.PrjCost,'0.000')) AS PrjCost  --造价 \r\nFROM PT_PrjInfo_ZTB AS Tender\r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\nLEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\nLEFT JOIN (SELECT PrjGuid,CodeID,CodeName FROM XPM_Basic_CodeList\r\nLEFT JOIN PT_PrjInfo_Kind AS PrjKind ON PrjKind.PrjKind= CodeID\r\n WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n) AS prjType ON Tender.PrjGuid=prjType.PrjGuid\r\nLEFT JOIN PT_PrjInfo AS PrjInfo ON  Tender.PrjGuid=PrjInfo.PrjGuid\r\nLEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS CListZTB ON Tender.PrjState=CListZTB.ItemCode\r\nLEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS CListPrjInfo ON PrjInfo.PrjState=CListPrjInfo.ItemCode");
            if (personType != 1)
            {
                if (personType == 2)
                {
                    strSql.Append("\r\nLEFT JOIN PT_yhmc AS YHMC ON TDetail.BusinessManager =YHMC.v_yhdm");
                }
                else if (personType != 3 && personType == 4)
                {
                    strSql.Append("\r\nLEFT JOIN PT_yhmc AS YHMC ON TDetail.PrjDutyPerson=YHMC.v_yhdm");
                }
            }
            strSql.Append("\r\nWHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                strSql.Append("\r\nAND Tender.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjCode))
            {
                strSql.Append("\r\nAND Tender.PrjCode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjKindClass))
            {
                strSql.Append(" AND Tender.PrjGuid IN ( \n").Append("\tSELECT PrjGuid FROM PT_PrjInfo_Kind \n").AppendFormat("\tWHERE PrjGuid = Tender.PrjGuid AND PrjKind = @prjKindClass \n", new object[0]).Append(" ) \n");
                parameters.Add(new SqlParameter("@prjKindClass", prjKindClass));
            }
            if (!string.IsNullOrWhiteSpace(buildUnit))
            {
                strSql.Append("\r\nAND Corp.CorpName LIKE '%'+ @buildUnit + '%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit.Trim()));
            }
            if (!string.IsNullOrEmpty(flowStateName))
            {
                strSql.Append(" AND Tender." + flowStateName + "=1  ");
            }
            if (flowState != null && flowState.Count > 0)
            {
                string search = TenderInfo.GetSearch(flowState);
                if (flowState.Contains(-1))
                {
                    strSql.AppendFormat(" \r\nAND (TDetail.ProjFlowSate IN ({0}) OR TDetail.ProjFlowSate IS NULL) ", search);
                }
                else
                {
                    strSql.AppendFormat(" \r\nAND TDetail.ProjFlowSate IN ({0})", search);
                }
            }
            if (prjState != null && prjState.Count > 0)
            {
                string search2 = TenderInfo.GetSearch(prjState);
                if (string.IsNullOrEmpty(OldState))
                {
                    strSql.AppendFormat("  AND Tender.PrjState IN ({0}) ", search2);
                }
                else if (prjState.Count == 1)
                {
                    if (search2 == "'18'")
                    {
                        strSql.AppendFormat(" AND (Tender.PrjState=18 and Tender.OldState='" + OldState + "')", search2);
                    }
                    else
                    {
                        strSql.AppendFormat("  AND Tender.PrjState IN ({0}) ", search2);
                    }
                }
                else
                {
                    strSql.AppendFormat(" AND (Tender.PrjState IN ({0}) or (Tender.PrjState=18 and Tender.OldState='" + OldState + "'))", search2);
                }
            }
            if (!string.IsNullOrWhiteSpace(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrWhiteSpace(start))
            {
                strSql.Append("\r\nAND TDetail.InputDate >= @start ");
                parameters.Add(new SqlParameter("@start", start.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                strSql.Append("\r\nAND  TDetail.InputDate <= @end ");
                parameters.Add(new SqlParameter("@end", end.Trim() + " 23:59:59"));
            }
            if (personType == 1 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND  TDetail.ProjPeopleName LIKE'%'+@setUpPerson+'%'");
                parameters.Add(new SqlParameter("@setUpPerson", person.Trim()));
            }
            if (personType == 2 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND  YHMC.v_xm LIKE '%'+@businessManager+'%'");
                parameters.Add(new SqlParameter("@businessManager", person.Trim()));
            }
            if (personType == 3 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND Tender.PrjManager LIKE '%'+@prjManager+'%'");
                parameters.Add(new SqlParameter("@prjManager", person.Trim()));
            }
            if (personType == 4 && !string.IsNullOrWhiteSpace(person))
            {
                strSql.Append("\r\nAND YHMC.v_xm LIKE '%'+@prjDuty+'%'");
                parameters.Add(new SqlParameter("@prjDuty", person.Trim()));
            }
            strSql.Append("\r\n)AS T WHERE T.No BETWEEN (@pageNo-1)*@pageSize+1 AND @pageNo*@pageSize ");
            parameters.Add(new SqlParameter("@pageNo", pageNo));
            parameters.Add(new SqlParameter("@pageSize", pageSize));
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), parameters.ToArray());
        }
        public static void UpdatePrjState(System.Guid guid, int state)
        {
            using (pm2Entities context = new pm2Entities())
            {
                PT_PrjInfo_ZTB prjZTB = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjGuid == guid
                    select m).FirstOrDefault<PT_PrjInfo_ZTB>();
                if (prjZTB != null)
                {
                    prjZTB.PrjState = new int?(state);
                    context.SaveChanges();
                }
            }
        }
        public static DataTable GetFlowStateSummarizingInfo(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string setUpPerson, string businessManager, string prjManager)
        {
            DataTable dtSource = new DataTable();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            strSql.Append("\r\nSELECT * FROM (\r\n--100 代表未知状态,FlowState=null\r\nSELECT ISNULL(ProjFlowSate,100) AS ProjFlowSate,COUNT(ProjFlowSate) AS FCount \r\nFROM PT_PrjInfo_ZTB AS Tender\r\nLEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\nLEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\nLEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n) AS prjType ON Tender.PrjKindClass=CONVERT(VARCHAR(100),prjType.CodeID)\r\nLEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS CListZTB ON Tender.PrjState=CListZTB.ItemCode\r\n--项目经理\r\n--LEFT JOIN PT_yhmc AS YHMC1 ON Tender.PrjManager =YHMC1.v_yhdm\r\n--项目跟进人\r\nLEFT JOIN PT_yhmc AS YHMC2 ON TDetail.BusinessManager =YHMC2.v_yhdm");
            strSql.Append("\r\nWHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                strSql.Append("\r\nAND Tender.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjCode))
            {
                strSql.Append("\r\nAND Tender.PrjCode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjKindClass))
            {
                strSql.Append("\r\nAND Tender.PrjKindClass = @prjKindClass  ");
                parameters.Add(new SqlParameter("@prjKindClass", prjKindClass));
            }
            if (!string.IsNullOrWhiteSpace(buildUnit))
            {
                strSql.Append("\r\nAND Corp.CorpName LIKE '%'+ @buildUnit + '%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit.Trim()));
            }
            if (flowState != null && flowState.Count > 0)
            {
                string search = TenderInfo.GetSearch(flowState);
                if (flowState.Contains(-1))
                {
                    strSql.AppendFormat(" \r\nAND (TDetail.ProjFlowSate IN ({0}) OR TDetail.ProjFlowSate IS NULL) ", search);
                }
                else
                {
                    strSql.AppendFormat(" \r\nAND TDetail.ProjFlowSate IN ({0})", search);
                }
            }
            if (!string.IsNullOrWhiteSpace(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrWhiteSpace(start))
            {
                strSql.Append("\r\nAND TDetail.InputDate >= @start ");
                parameters.Add(new SqlParameter("@start", start.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                strSql.Append("\r\nAND  TDetail.InputDate <= @end ");
                parameters.Add(new SqlParameter("@end", end.Trim() + " 23:59:59"));
            }
            if (prjState != null && prjState.Count > 0)
            {
                string search2 = TenderInfo.GetSearch(prjState);
                strSql.AppendFormat(" \r\nAND Tender.PrjState IN ({0}) ", search2);
            }
            if (!string.IsNullOrWhiteSpace(setUpPerson))
            {
                strSql.Append("\r\nAND  TDetail.ProjPeopleName LIKE '%'+@setUpPerson+'%'");
                parameters.Add(new SqlParameter("@setUpPerson", setUpPerson.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(businessManager))
            {
                strSql.Append("\r\nAND  YHMC2.v_xm LIKE '%' +@businessManager+ '%'");
                parameters.Add(new SqlParameter("@businessManager", businessManager.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjManager))
            {
                strSql.Append("\r\nAND Tender.PrjManager LIKE '%' +@prjManager+ '%'");
                parameters.Add(new SqlParameter("@prjManager", prjManager.Trim()));
            }
            strSql.Append("\r\nGROUP BY TDetail.ProjFlowSate\r\n) AS FlowStateInfo \r\nPIVOT\r\n(\r\n MAX(FlowStateInfo.FCount) FOR FlowStateInfo.ProjFlowSate IN ([100],[-3],[-2],[-1],[0],[1])\r\n) AS FlowState");
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), parameters.ToArray());
        }
        public static string GetStartDate(string prjGuid)
        {
            string strDate = string.Empty;
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid guid = new System.Guid(prjGuid);
                System.DateTime? start = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjGuid == guid
                    select m.StartDate).FirstOrDefault<System.DateTime?>();
                if (start.HasValue)
                {
                    strDate = start.Value.ToString("yyyy-M-d");
                }
            }
            return strDate;
        }
        public static DataTable GetTenders(string code, string name, string setUpPerson, string setUpDep, string kind, string startDate, string endDate, string unitName, string flowState, string property, string userCode, int pageSize, int pageIndex)
        {
            string condition = TenderInfo.GenerateGetTendersSql(code, name, setUpPerson, setUpDep, kind, startDate, endDate, unitName, flowState, property, userCode);
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat("  SELECT TOP({0}) * FROM ( \n", pageSize).Append("      SELECT ROW_NUMBER() OVER(ORDER BY InputDate DESC)AS No,PrjGuid,PrjCode,PrjName,PrjState,StartDate,EndDate,Duration, \n").Append("      ISNULL(PrjCost,0) PrjCost,ProjFlowSate,ISNULL(SuccessBidPrice,0) SuccessBidPrice,InputDate,ProjPeopleName,ProjPeopleDep, \n").Append("      PrjProperty,PropertyName,WorkUnitName,StateText FROM vTender \n").Append("      WHERE 1 = 1 ").Append(condition).Append("      ) v \n").AppendFormat("  WHERE No > ({0} - 1) * {1} \n", pageIndex, pageSize);
            return SqlHelper.ExecuteQuery(CommandType.Text, sql.ToString(), new SqlParameter[0]);
        }
        public static int GetTendersCount(string code, string name, string setUpPerson, string setUpDep, string kind, string startDate, string endDate, string unitName, string flowState, string property, string userCode)
        {
            string condition = TenderInfo.GenerateGetTendersSql(code, name, setUpPerson, setUpDep, kind, startDate, endDate, unitName, flowState, property, userCode);
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append(" SELECT COUNT(*) FROM vTender \n");
            sql.Append(" WHERE 1 = 1 \n");
            sql.Append(condition);
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, sql.ToString(), new SqlParameter[0]);
            return DBHelper.GetInt(obj);
        }
        public static DataTable GetUserInfo(string usercode)
        {
            System.Text.StringBuilder sqlBuilder = new System.Text.StringBuilder();
            sqlBuilder.Append("SELECT  MobilePhoneCode AS Phone,\t\t\t\t\t\t--电话 \n");
            sqlBuilder.Append("\t    v_xm AS UserName,\t\t\t\t\t\t\t\t\t--用户名 \n");
            sqlBuilder.Append(" \tdbo.ufnRootDepName(PT_yhmc.i_bmdm) AS CorpName,\t\t--部门 \n");
            sqlBuilder.Append(" \tdutyName AS Duty\t\t\t\t\t\t--岗位 \n");
            sqlBuilder.Append("FROM PT_yhmc   \n");
            sqlBuilder.Append("INNER JOIN PT_DUTY ON PT_yhmc.I_DUTYID = PT_DUTY.I_DUTYID \n");
            sqlBuilder.Append("WHERE v_yhdm=@UserCode \n");
            SqlParameter[] paras = new SqlParameter[]
			{
				new SqlParameter("@UserCode", usercode)
			};
            return SqlHelper.ExecuteQuery(CommandType.Text, sqlBuilder.ToString(), paras);
        }
        public static DataTable GetOwnerInfo(string ownerName)
        {
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.AppendFormat("SELECT LinkMan,Telephone,Address FROM XPM_Basic_ContactCorp WHERE CorpName='{0}'", ownerName);
            DataTable dt = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), new SqlParameter[0]);
        }
        public static DataTable GetTenderReport(string prjCode, string prjName, string prjType, string prjManager, string dutyPersonName, string setUpPerson, string prjState, string startDate, string endDate, string buildUnit, string ProjPeopleDep, string dropPrjProperty, string principal, int pageNo, int pageSize, string userCode, string PrjStateChangeTimeStart, string PrjStateChangeTimeEnd)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            sql.Append("SELECT  * FROM(\r\n    SELECT Row_Number() OVER(ORDER BY InputDate DESC) AS No,ZTB.PrjGuid,ZTB.PrjCode,ZTB.PrjName,ZTB.PrjManager AS PrjManagerName,ZTB.WorkUnit\r\n--\t,ZTB.PrjKindClass\r\n\t,ISNULL(CodeName,'') CodeName\r\n\t,ISNULL(CorpName,'') AS CorpName\r\n\t,CONVERT(DECIMAL(18,3),ISNULL(ZTB.PrjCost,0)) AS PrjCost\r\n   \t,ISNULL(Detail.SuccessBidPrice, 0.000) AS SuccessBidPrice           --中标价格\r\n\t,CASE ISNULL(CostPrjList.ItemName,'')\r\n\t\tWHEN '' THEN  CostList.ItemName\r\n\t\tELSE\r\n\t\t\tCASE PrjInfo.PrjState\r\n\t\t\t\tWHEN '5' THEN CostList.ItemName\r\n\t\t\tELSE\r\n\t\t\t\tCASE ZTB.PrjState\t\r\n\t\t\t\t\tWHEN '5' THEN(CostList.ItemName+'('+CostPrjList.ItemName+')')\r\n\t\t\t\t\tELSE\r\n\t\t\t\t\t\tCostList.ItemName\r\n\t\t\t\tEND\r\n\t\t\tEND\t\r\n\tEND AS ItemName\r\n    ,ISNULL((SELECT TOP(1) ProfessionalCost FROM PT_PrjInfo_Kind WHERE PrjGuid = ZTB.PrjGuid AND PrjKind = @prjType ),0) AS ProfessionalCost      --专业造价\r\n\t,CONVERT(VARCHAR(20),Detail.InputDate,23) AS InputDate,ISNULL(YHMC3.v_xm,'') PrjDutyName\r\n\t,CASE ZTB.PrjState WHEN '2' THEN '1' END AS 'PrjSetUp'\r\n\t,CASE ZTB.PrjState WHEN '3' THEN '1' END AS 'PrjStart' \r\n    ,(CASE WHEN ZTB.PrjState='14' OR ZTB.PrjState='15' OR ZTB.PrjState='16' THEN '1'  END) as 'PrjYs'\r\n    --,(case when ZTB.PrjState='14' then '1' when ZTB.PrjState='15' then '2' when ZTB.PrjState='16' then '3'  end) as 'PrjYs'\r\n\t,CASE ZTB.PrjState WHEN '4' THEN '1' END AS 'PrjTender'\r\n\t,CASE ZTB.PrjState WHEN '5' THEN '1' END AS 'Winbid'\r\n\t,CASE ZTB.PrjState WHEN '6' THEN '1' END AS 'Outbid' \r\n    ,CASE ZTB.PrjState WHEN '19' THEN '1' END AS 'PrjStartNO'\r\n    ,CASE ZTB.PrjState WHEN '18' THEN '1' END AS 'PrjGiveUp'  \r\n\tFROM PT_PrjInfo_ZTB ZTB \r\n\tLEFT JOIN PT_PrjInfo_ZTB_Detail Detail ON ZTB.PrjGuid=Detail.PrjGuid  \r\n\tLEFT JOIN (SELECT CodeId,CodeName FROM XPM_Basic_CodeList WHERE TYPEID=\r\n\t\t(SELECT TypeId FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND ISVALID='1') CodeList  \r\n\t\tON ZTB.PrjKindClass=CAST(CodeList.CodeId AS NVARCHAR(30))  \r\n\tLEFT JOIN XPM_Basic_ContactCorp Corp ON ZTB.OwnerCode=Corp.CorpID \r\n\tLEFT JOIN (SELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode='ProjectState') CostList  \r\n\t\tON ZTB.PrjState=CostList.ItemCode \r\n\tLEFT JOIN Pt_PrjInfo PrjInfo ON ZTB.PrjGuid=PrjInfo.PrjGuid \r\n\tLEFT JOIN (SELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode='ProjectState') CostPrjList  \r\n\t\tON PrjInfo.PrjState=CostPrjList.ItemCode \r\n\t--项目跟进人\r\n\tLEFT JOIN PT_yhmc AS YHMC2 ON Detail.BusinessManager =YHMC2.v_yhdm\r\n    --项目跟踪人\r\n\tLEFT JOIN PT_yhmc AS YHMC3 ON Detail.PrjDutyPerson =YHMC3.v_yhdm\r\n\tWHERE  ZTB.PrjState!=1 ");
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    sql.AppendFormat(" AND ZTB.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    sql.AppendFormat(" AND ZTB.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrWhiteSpace(prjCode))
            {
                sql.Append(" AND ZTB.Prjcode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                sql.Append(" AND ZTB.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjType))
            {
                sql.Append(" AND ZTB.PrjGuid IN ( \n").Append("\tSELECT PrjGuid FROM PT_PrjInfo_Kind \n").AppendFormat("\tWHERE PrjGuid = ZTB.PrjGuid AND PrjKind = @prjType \n", new object[0]).Append(" ) \n");
            }
            if (!string.IsNullOrWhiteSpace(prjManager))
            {
                sql.Append(" AND ZTB.PrjManager LIKE '%'+@prjManagaer+'%' ");
                parameters.Add(new SqlParameter("@prjManagaer", prjManager.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjState))
            {
                sql.Append(" AND ZTB.prjState = @prjState ");
                parameters.Add(new SqlParameter("@prjState", prjState));
            }
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                sql.Append(" AND InputDate >= @startDate ");
                parameters.Add(new SqlParameter("@startDate", startDate.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                sql.Append(" AND InputDate <= @endDate ");
                parameters.Add(new SqlParameter("@endDate", endDate.Trim() + " 23:59:59"));
            }
            if (!string.IsNullOrWhiteSpace(buildUnit))
            {
                sql.Append(" AND CorpName LIKE  '%'+  @buildUnit +'%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(setUpPerson))
            {
                sql.Append(" AND ProjPeopleName LIKE  '%'+  @setUpPerson +'%' ");
                parameters.Add(new SqlParameter("@setUpPerson", setUpPerson.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(dutyPersonName))
            {
                sql.Append(" AND YHMC3.v_xm LIKE  '%'+  @dutyPersonName +'%' ");
                parameters.Add(new SqlParameter("@dutyPersonName", dutyPersonName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(ProjPeopleDep))
            {
                sql.Append(" AND Detail.ProjPeopleDep LIKE '%'+ @ProjPeopleDep +'%'");
                parameters.Add(new SqlParameter("@ProjPeopleDep", ProjPeopleDep.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(dropPrjProperty))
            {
                sql.Append(" AND Detail.PrjProperty LIKE '%'+ @dropPrjProperty +'%'");
                parameters.Add(new SqlParameter("@dropPrjProperty", dropPrjProperty.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(principal))
            {
                sql.Append(" AND ZTB.WorkUnit LIKE '%'+ @principal +'%'");
                parameters.Add(new SqlParameter("@principal", principal.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(PrjStateChangeTimeStart))
            {
                sql.AppendFormat(" AND PrjStateChangeTime >='{0}' ", PrjStateChangeTimeStart);
            }
            if (!string.IsNullOrWhiteSpace(PrjStateChangeTimeEnd))
            {
                sql.AppendFormat(" AND PrjStateChangeTime <='{0}' ", PrjStateChangeTimeEnd + " 23:59:59");
            }
            sql.Append(" ) AS PrjInfo WHERE  No BETWEEN (@pageNo-1)*@pageSize+1 AND @pageNo*@pageSize");
            parameters.Add(new SqlParameter("@prjType", prjType));
            parameters.Add(new SqlParameter("@userCode", userCode));
            parameters.Add(new SqlParameter("@pageNo", pageNo));
            parameters.Add(new SqlParameter("@pageSize", pageSize));
            return SqlHelper.ExecuteQuery(CommandType.Text, sql.ToString(), parameters.ToArray());
        }
        public static int GetTenderReportCount(string prjCode, string prjName, string prjType, string prjManager, string businessManager, string setUpPerson, string prjState, string startDate, string endDate, string buildUnit, string ProjPeopleDep, string dropPrjProperty, string principal, string userCode, string PrjStateChangeTimeStart, string PrjStateChangeTimeEnd)
        {
            object objRecordCount = TenderInfo.GetTenderReportSumTotalOrCount(prjCode, prjName, prjType, prjManager, businessManager, setUpPerson, prjState, startDate, endDate, buildUnit, ProjPeopleDep, dropPrjProperty, principal, userCode, false, PrjStateChangeTimeStart, PrjStateChangeTimeEnd);
            return (objRecordCount == null) ? 0 : System.Convert.ToInt32(objRecordCount);
        }
        public static DataTable GetSummarizingInfo(string prjCode, string prjName, string prjType, string prjManagaer, string businessManager, string setUpPerson, string prjState, string startDate, string endDate, string buildUnit, string ProjPeopleDep, string dropPrjProperty, string principal, string userCode, string PrjStateChangeTimeStart, string PrjStateChangeTimeEnd)
        {
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append("SELECT * FROM(\r\nSELECT ZTB.PrjState,COUNT(ZTB.PrjState) AS CountPrjState\r\n\tFROM PT_PrjInfo_ZTB ZTB \r\n\tLEFT JOIN PT_PrjInfo_ZTB_Detail Detail ON ZTB.PrjGuid=Detail.PrjGuid  \r\n\tLEFT JOIN (SELECT CodeId,CodeName FROM XPM_Basic_CodeList WHERE TYPEID=\r\n\t\t(SELECT TypeId FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND ISVALID='1') CodeList  \r\n\t\tON ZTB.PrjKindClass=CAST(CodeList.CodeId AS NVARCHAR(30))  \r\n\tLEFT JOIN XPM_Basic_ContactCorp Corp ON ZTB.OwnerCode=Corp.CorpID \r\n\tLEFT JOIN (SELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode='ProjectState') CostList  \r\n\t\tON ZTB.PrjState=CostList.ItemCode \r\n\tLEFT JOIN Pt_PrjInfo PrjInfo ON ZTB.PrjGuid=PrjInfo.PrjGuid \r\n\tLEFT JOIN (SELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode='ProjectState') CostPrjList  \r\n\t\tON PrjInfo.PrjState=CostPrjList.ItemCode \r\n    --项目经理\r\n\t--LEFT JOIN PT_yhmc AS YHMC1 ON ZTB.PrjManager =YHMC1.v_yhdm\r\n\t--项目跟进人\r\n\tLEFT JOIN PT_yhmc AS YHMC2 ON Detail.BusinessManager =YHMC2.v_yhdm\r\n\tWHERE  ZTB.PrjState!=1 ");
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    sql.AppendFormat(" AND ZTB.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    sql.AppendFormat(" AND ZTB.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrWhiteSpace(prjCode))
            {
                sql.Append(" AND ZTB.Prjcode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                sql.Append(" AND ZTB.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjType))
            {
                sql.Append(" AND ZTB.PrjKindClass = @prjType ");
                parameters.Add(new SqlParameter("@prjType", prjType));
            }
            if (!string.IsNullOrWhiteSpace(prjManagaer))
            {
                sql.Append(" AND ZTB.PrjManager LIKE '%'+@prjManagaer+'%' ");
                parameters.Add(new SqlParameter("@prjManagaer", prjManagaer.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjState))
            {
                sql.Append(" AND ZTB.prjState = @prjState ");
                parameters.Add(new SqlParameter("@prjState", prjState));
            }
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                sql.Append(" AND InputDate >= @startDate ");
                parameters.Add(new SqlParameter("@startDate", startDate.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                sql.Append(" AND InputDate <= @endDate ");
                parameters.Add(new SqlParameter("@endDate", endDate.Trim() + " 23:59:59"));
            }
            if (!string.IsNullOrWhiteSpace(buildUnit))
            {
                sql.Append(" AND CorpName LIKE  '%'+  @buildUnit +'%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(setUpPerson))
            {
                sql.Append(" AND ProjPeopleName LIKE  '%'+  @setUpPerson +'%' ");
                parameters.Add(new SqlParameter("@setUpPerson", setUpPerson.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(businessManager))
            {
                sql.Append(" AND YHMC2.v_xm LIKE  '%'+  @businessManager +'%' ");
                parameters.Add(new SqlParameter("@businessManager", businessManager.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(ProjPeopleDep))
            {
                sql.Append(" AND Detail.ProjPeopleDep LIKE '%'+ @ProjPeopleDep +'%'");
                parameters.Add(new SqlParameter("@ProjPeopleDep", ProjPeopleDep.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(dropPrjProperty))
            {
                sql.Append(" AND Detail.PrjProperty LIKE '%'+ @dropPrjProperty +'%'");
                parameters.Add(new SqlParameter("@dropPrjProperty", dropPrjProperty.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(principal))
            {
                sql.Append(" AND ZTB.WorkUnit LIKE '%'+ @principal +'%'");
                parameters.Add(new SqlParameter("@principal", principal.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(PrjStateChangeTimeStart))
            {
                sql.AppendFormat(" AND PrjStateChangeTime >='{0}' ", PrjStateChangeTimeStart);
            }
            if (!string.IsNullOrWhiteSpace(PrjStateChangeTimeEnd))
            {
                sql.AppendFormat(" AND PrjStateChangeTime <='{0}' ", PrjStateChangeTimeEnd + " 23:59:59");
            }
            parameters.Add(new SqlParameter("@userCode", userCode));
            sql.Append("\r\n\t\t\t\t\tGROUP BY ZTB.PrjState\r\n\t\t\t\t)AS PrjStateInfo\r\n\t\t\t\t PIVOT(\r\n\t\t\t\t\tMAX(PrjStateInfo.CountPrjState) FOR PrjStateInfo.PrjState IN ([2],[3],[4],[5],[6],[14],[15],[16],[18],[19])\r\n\t\t\t\t) AS PrjStatColumns\r\n\t\t\t");
            return SqlHelper.ExecuteQuery(CommandType.Text, sql.ToString(), parameters.ToArray());
        }
        public static decimal GetTenderReportSumTotal(string prjCode, string prjName, string prjType, string prjManagaer, string businessManager, string setUpPerson, string prjState, string startDate, string endDate, string buildUnit, string ProjPeopleDep, string dropPrjProperty, string principal, string userCode, string PrjStateChangeTimeStart, string PrjStateChangeTimeEnd)
        {
            object objRecordCount = TenderInfo.GetTenderReportSumTotalOrCount(prjCode, prjName, prjType, prjManagaer, businessManager, setUpPerson, prjState, startDate, endDate, buildUnit, ProjPeopleDep, dropPrjProperty, principal, userCode, true, PrjStateChangeTimeStart, PrjStateChangeTimeEnd);
            return (objRecordCount == null) ? 0m : decimal.Parse(objRecordCount.ToString());
        }
        public static DataTable GetTenderPrivilege(string prjCode, string prjName, string startDate, string endDate, string prjManageName, string prjState, string userCode, string kind, string setUpDep, string prjProperty, int pageNo, int pageSize)
        {
            new DataTable();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.AppendFormat("\r\n\t\t\t\tSELECT TOP({0}) * FROM (\r\n\t\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY V.InputDate DESC) AS Num, V.* FROM vTender AS V WHERE 1 = 1 \r\n\t\t\t", pageSize);
            string condition = TenderInfo.GenerateGetTenderPrivilegeSql(prjCode, prjName, startDate, endDate, prjManageName, prjState, userCode, kind, setUpDep, prjProperty);
            strSql.Append(condition);
            strSql.AppendFormat(") AS T WHERE T.Num > ({0} - 1) * {1}", pageNo, pageSize);
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), parameters.ToArray());
        }
        public static int GetTenderPrivilegeCount(string prjCode, string prjName, string startDate, string endDate, string prjManageName, string prjState, string userCode, string kind, string setUpDep, string prjProperty)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            string condition = TenderInfo.GenerateGetTenderPrivilegeSql(prjCode, prjName, startDate, endDate, prjManageName, prjState, userCode, kind, setUpDep, prjProperty);
            sql.Append("SELECT COUNT(*) FROM vTender AS V WHERE 1 = 1 ");
            sql.Append(condition);
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, sql.ToString(), null);
            return DBHelper.GetInt(obj);
        }
        public void Add(TenderInfo model)
        {
            using (pm2Entities context = new pm2Entities())
            {
                if (model != null)
                {
                    PT_PrjInfo_ZTB prjZTB = new PT_PrjInfo_ZTB
                    {
                        ComputeClass = model.ComputeClass,
                        ContractSum = model.ContractSum,
                        FileName = model.FileName,
                        FileURL = model.FileURL,
                        RationClass = model.RationClass,
                        MarketInfoGuid = model.MarketInfoGuid,
                        OwnerCode = model.OwnerCode,
                        PrjGuid = model.PrjGuid,
                        PrjCode = model.PrjCode,
                        PrjName = model.PrjName,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Duration = model.Duration,
                        PrjCost = model.PrjCost,
                        PrjKindClass = model.PrjKindClass,
                        PrjPlace = model.PrjPlace,
                        PrjState = new int?(model.PrjState),
                        Area = model.Area,
                        Designer = model.Designer,
                        Counsellor = model.Counsellor,
                        Inspector = model.Inspector,
                        PrjInfo = model.PrjInfo,
                        QualityClass = model.QualityClass,
                        Remark = model.Remark,
                        Rank = model.Rank,
                        BudgetWay = model.BudgetWay,
                        PayWay = model.PayWay,
                        KeyPart = model.KeyPart,
                        ContractWay = model.ContractWay,
                        PayCondition = model.PayCondition,
                        PrjManager = model.PrjManager,
                        TenderWay = model.TenderWay,
                        WorkUnit = model.WorkUnit,
                        BuildingType = model.BuildingType,
                        BuildingArea = model.BuildingArea,
                        OtherStatement = model.OtherStatement,
                        PrjFundInfo = model.PrjFundInfo,
                        TotalHouseNum = model.TotalHouseNum,
                        UndergroundArea = model.UndergroundArea,
                        UsegrounArea = model.UsegrounArea
                    };
                    context.AddToPT_PrjInfo_ZTB(prjZTB);
                    PT_PrjInfo_ZTB_Detail prjZTB_Detail = new PT_PrjInfo_ZTB_Detail
                    {
                        PrjGuid = model.PrjGuid,
                        InputUser = model.InputUser,
                        InputDate = model.InputDate,
                        ProjFlowSate = new int?(-1),
                        MemberFlowState = new int?(-1),
                        CompletedFlowState = new int?(-1),
                        ProjPeopleName = model.ProjPeopleUserName,
                        ProjPeopleDep = model.ProjPeopleCorp,
                        ProjPeopleDuty = model.ProjPeopleDuty,
                        ProjPeopleTel = model.ProjPeopleTel,
                        OwnerLinkMan = model.OwnerLinkManName,
                        OwnerLinkPhone = model.OwnerLinkPhone,
                        OwnerAddress = model.OwnerAddress,
                        PrjDutyPerson = model.PrjDutyPerson,
                        PrjApprovalOf = model.PrjApprovalOf,
                        EngineeringEstimates = model.EngineeringEstimates,
                        ForecastProfitRate = model.ForecastProfitRate,
                        ProjInfoOrigin = model.ProjInfoOrigin,
                        ProjElseRequest = model.ProjElseRequest,
                        Telephone = model.Telephone,
                        EngineeringType = model.EngineeringType,
                        EngineeringSubType = model.EngineeringSubType,
                        Grade = model.Grade,
                        BuildingTypeNo = new int?(model.BuildingTypeNo),
                        PrjFundWorkable = model.PrjFundWorkable,
                        AfforestArea = model.AfforestArea,
                        ParkArea = model.ParkArea,
                        ManagementMargin = model.ManagementMargin,
                        MigrantQualityMarginRate = model.MigrantQualityMarginRate,
                        WithholdingTaxRate = model.WithholdingTaxRate,
                        PerformanceBond = model.PerformanceBond,
                        ElseMargin = model.ElseMargin,
                        IsTender = true,
                        PrjManagerRequire = model.PrjManagerRequire,
                        TechnicalLeaderRequire = model.TechnicalLeaderRequire,
                        Province = model.Province,
                        City = model.City,
                        PrjProperty = model.PrjProperty,
                        PrjReadOne = model.PrjReadOne,
                        QualificationMargin = model.QualificationMargin,
                        QualificationReadOne = model.QualificationReadOne,
                        TenderProspect = model.TenderProspect,
                        TenderReadOne = model.TenderReadOne
                    };
                    context.AddToPT_PrjInfo_ZTB_Detail(prjZTB_Detail);
                    cn.justwin.Tender.EngineeringType.UpdateEngineerType(model.EngineeringTypes, model.PrjGuid.ToString(), context);
                    ProjectKind.Update(model.PrjGuid.ToString(), model.prjKinds, context);
                    ProjectRank.Update(model.PrjGuid.ToString(), model.prjRanks, context);
                    context.SaveChanges();
                    this.AddDefaultLimit(model);
                }
            }
        }
        public void UpdateMultiplePart(TenderInfo model, string type)
        {
            using (pm2Entities context = new pm2Entities())
            {
                PT_PrjInfo_ZTB prjZTB = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjGuid == model.PrjGuid
                    select m).FirstOrDefault<PT_PrjInfo_ZTB>();
                if (prjZTB != null)
                {
                    prjZTB.Area = model.Area;
                    prjZTB.BudgetWay = model.BudgetWay;
                    prjZTB.BuildingArea = model.BuildingArea;
                    prjZTB.BuildingType = model.BuildingType;
                    prjZTB.PrjCode = model.PrjCode;
                    prjZTB.PrjCost = model.PrjCost;
                    prjZTB.PrjFundInfo = model.PrjFundInfo;
                    prjZTB.PrjInfo = model.PrjInfo;
                    prjZTB.PrjKindClass = model.PrjKindClass;
                    prjZTB.PrjManager = model.PrjManager;
                    prjZTB.PrjName = model.PrjName;
                    prjZTB.PrjPlace = model.PrjPlace;
                    prjZTB.QualityClass = model.QualityClass;
                    prjZTB.Rank = model.Rank;
                    prjZTB.RationClass = model.RationClass;
                    prjZTB.Remark = model.Remark;
                    prjZTB.StartDate = model.StartDate;
                    prjZTB.EndDate = model.EndDate;
                    prjZTB.TenderWay = model.TenderWay;
                    prjZTB.TotalHouseNum = model.TotalHouseNum;
                    prjZTB.UndergroundArea = model.UndergroundArea;
                    prjZTB.UsegrounArea = model.UsegrounArea;
                    prjZTB.WorkUnit = model.WorkUnit;
                    prjZTB.Duration = model.Duration;
                    prjZTB.Designer = model.Designer;
                    prjZTB.Inspector = model.Inspector;
                    prjZTB.Counsellor = model.Counsellor;
                    prjZTB.OwnerCode = model.OwnerCode;
                    prjZTB.ContractWay = model.ContractWay;
                    prjZTB.PayWay = model.PayWay;
                    prjZTB.KeyPart = model.KeyPart;
                    prjZTB.PayCondition = model.PayCondition;
                    prjZTB.OtherStatement = model.OtherStatement;
                    PT_PrjInfo_ZTB_Detail prjZTB_Detail = (
                        from m in context.PT_PrjInfo_ZTB_Detail
                        where m.PrjGuid == model.PrjGuid
                        select m).FirstOrDefault<PT_PrjInfo_ZTB_Detail>();
                    if (prjZTB_Detail == null)
                    {
                        prjZTB_Detail = new PT_PrjInfo_ZTB_Detail
                        {
                            PrjGuid = model.PrjGuid,
                            ProjFlowSate = new int?(-1)
                        };
                        context.AddToPT_PrjInfo_ZTB_Detail(prjZTB_Detail);
                    }
                    prjZTB_Detail.ProjPeopleName = model.ProjPeopleUserName;
                    prjZTB_Detail.ProjPeopleDep = model.ProjPeopleCorp;
                    prjZTB_Detail.ProjPeopleDuty = model.ProjPeopleDuty;
                    prjZTB_Detail.ProjPeopleTel = model.ProjPeopleTel;
                    prjZTB_Detail.ProjInfoOrigin = model.ProjInfoOrigin;
                    prjZTB_Detail.ProjElseRequest = model.ProjElseRequest;
                    prjZTB_Detail.Grade = model.Grade;
                    prjZTB_Detail.Telephone = model.Telephone;
                    prjZTB_Detail.EngineeringType = model.EngineeringType;
                    prjZTB_Detail.EngineeringSubType = model.EngineeringSubType;
                    prjZTB_Detail.BuildingTypeNo = new int?(model.BuildingTypeNo);
                    prjZTB_Detail.OwnerLinkMan = model.OwnerLinkManName;
                    prjZTB_Detail.OwnerLinkPhone = model.OwnerLinkPhone;
                    prjZTB_Detail.OwnerAddress = model.OwnerAddress;
                    this.UpdateUser(prjZTB_Detail.PrjDutyPerson, model.PrjDutyPerson, model.PrjGuid);
                    prjZTB_Detail.PrjDutyPerson = model.PrjDutyPerson;
                    prjZTB_Detail.PrjApprovalOf = model.PrjApprovalOf;
                    prjZTB_Detail.PrjFundWorkable = model.PrjFundWorkable;
                    prjZTB_Detail.ForecastProfitRate = model.ForecastProfitRate;
                    prjZTB_Detail.EngineeringEstimates = model.EngineeringEstimates;
                    prjZTB_Detail.ManagementMargin = model.ManagementMargin;
                    prjZTB_Detail.MigrantQualityMarginRate = model.MigrantQualityMarginRate;
                    prjZTB_Detail.WithholdingTaxRate = model.WithholdingTaxRate;
                    prjZTB_Detail.PerformanceBond = model.PerformanceBond;
                    prjZTB_Detail.ElseMargin = model.ElseMargin;
                    cn.justwin.Tender.EngineeringType.UpdateEngineerType(model.EngineeringTypes, model.PrjGuid.ToString(), context);
                    prjZTB_Detail.PrjManagerRequire = model.PrjManagerRequire;
                    prjZTB_Detail.TechnicalLeaderRequire = model.TechnicalLeaderRequire;
                    prjZTB_Detail.Province = model.Province;
                    prjZTB_Detail.City = model.City;
                    prjZTB_Detail.PrjProperty = model.PrjProperty;
                    this.UpdateUser(prjZTB_Detail.PrjReadOne, model.PrjReadOne, model.PrjGuid);
                    prjZTB_Detail.PrjReadOne = model.PrjReadOne;
                    prjZTB_Detail.QualificationMargin = model.QualificationMargin;
                    this.UpdateUser(prjZTB_Detail.QualificationReadOne, model.QualificationReadOne, model.PrjGuid);
                    prjZTB_Detail.QualificationReadOne = model.QualificationReadOne;
                    prjZTB_Detail.TenderProspect = model.TenderProspect;
                    this.UpdateUser(prjZTB_Detail.TenderReadOne, model.TenderReadOne, model.PrjGuid);
                    prjZTB_Detail.TenderReadOne = model.TenderReadOne;
                    ProjectKind.Update(model.PrjGuid.ToString(), model.prjKinds, context);
                    ProjectRank.Update(model.PrjGuid.ToString(), model.prjRanks, context);
                    if (type == ProjectParameter.Initiate)
                    {
                        prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                        this.UpdateUser(prjZTB_Detail.BusinessManager, model.BusinessManager, model.PrjGuid);
                        prjZTB_Detail.BusinessManager = model.BusinessManager;
                        prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                    }
                    if (type == ProjectParameter.Prequalification)
                    {
                        prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                        this.UpdateUser(prjZTB_Detail.BusinessManager, model.BusinessManager, model.PrjGuid);
                        prjZTB_Detail.BusinessManager = model.BusinessManager;
                        prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                        prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                        prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                        prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                        prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                        this.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                        prjZTB_Detail.ProgAgent = model.ProgAgent;
                        prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                    }
                    if (type == ProjectParameter.QualificationPass)
                    {
                        prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                        this.UpdateUser(prjZTB_Detail.BusinessManager, model.BusinessManager, model.PrjGuid);
                        prjZTB_Detail.BusinessManager = model.BusinessManager;
                        prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                        prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                        prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                        prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                        prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                        this.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                        prjZTB_Detail.ProgAgent = model.ProgAgent;
                        prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                        prjZTB_Detail.QualificationPassDate = model.QualificationPassDate;
                        prjZTB_Detail.QualificationPassReason = model.QualificationPassReason;
                    }
                    if (type == ProjectParameter.QualificationFail)
                    {
                        prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                        this.UpdateUser(prjZTB_Detail.BusinessManager, model.BusinessManager, model.PrjGuid);
                        prjZTB_Detail.BusinessManager = model.BusinessManager;
                        prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                        prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                        prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                        prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                        prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                        this.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                        prjZTB_Detail.ProgAgent = model.ProgAgent;
                        prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                        prjZTB_Detail.QualificationFailData = model.QualificationFailData;
                        prjZTB_Detail.QualificationFailReason = model.QualificationFailReason;
                    }
                    if (type == ProjectParameter.Bid)
                    {
                        prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                        this.UpdateUser(prjZTB_Detail.BusinessManager, model.BusinessManager, model.PrjGuid);
                        prjZTB_Detail.BusinessManager = model.BusinessManager;
                        prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                        prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                        prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                        prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                        prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                        this.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                        prjZTB_Detail.ProgAgent = model.ProgAgent;
                        prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                        prjZTB_Detail.QualificationPassDate = model.QualificationPassDate;
                        prjZTB_Detail.QualificationPassReason = model.QualificationPassReason;
                        prjZTB_Detail.ProjTenderBeginDate = model.ProjTenderBeginDate;
                        prjZTB_Detail.TenderCeilingPrice = model.TenderCeilingPrice;
                        prjZTB_Detail.TenderQuote = model.TenderQuote;
                        prjZTB_Detail.TenderAppraiseMethod = model.TenderAppraiseMethod;
                        prjZTB_Detail.TenderAverage = model.TenderAverage;
                        prjZTB_Detail.ProjTenderCostContent = model.ProjTenderCostContent;
                        prjZTB_Detail.ProjTenderAnswerDate = model.ProjTenderAnswerDate;
                        prjZTB_Detail.ProjTenderEarnestMoney = model.ProjTenderEarnestMoney;
                        prjZTB_Detail.ProjTenderPayWay = model.ProjTenderPayWay;
                        prjZTB_Detail.ProjTenderContent = model.ProjTenderContent;
                        prjZTB_Detail.ProjTenderRemark = model.ProjTenderRemark;
                    }
                    if (type == ProjectParameter.WinBid)
                    {
                        prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                        this.UpdateUser(prjZTB_Detail.BusinessManager, model.BusinessManager, model.PrjGuid);
                        prjZTB_Detail.BusinessManager = model.BusinessManager;
                        prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                        prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                        prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                        prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                        prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                        this.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                        prjZTB_Detail.ProgAgent = model.ProgAgent;
                        prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                        prjZTB_Detail.QualificationPassDate = model.QualificationPassDate;
                        prjZTB_Detail.QualificationPassReason = model.QualificationPassReason;
                        prjZTB_Detail.ProjTenderBeginDate = model.ProjTenderBeginDate;
                        prjZTB_Detail.TenderCeilingPrice = model.TenderCeilingPrice;
                        prjZTB_Detail.TenderQuote = model.TenderQuote;
                        prjZTB_Detail.TenderAppraiseMethod = model.TenderAppraiseMethod;
                        prjZTB_Detail.TenderAverage = model.TenderAverage;
                        prjZTB_Detail.ProjTenderCostContent = model.ProjTenderCostContent;
                        prjZTB_Detail.ProjTenderAnswerDate = model.ProjTenderAnswerDate;
                        prjZTB_Detail.ProjTenderEarnestMoney = model.ProjTenderEarnestMoney;
                        prjZTB_Detail.ProjTenderPayWay = model.ProjTenderPayWay;
                        prjZTB_Detail.ProjTenderContent = model.ProjTenderContent;
                        prjZTB_Detail.ProjTenderRemark = model.ProjTenderRemark;
                        prjZTB_Detail.SuccessBidDate = model.SuccessBidDate;
                        prjZTB_Detail.SuccessBidPrice = model.SuccessBidPrice;
                        prjZTB_Detail.SuccessBidRemark = model.SuccessBidRemark;
                    }
                    if (type == ProjectParameter.OutBid)
                    {
                        prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                        this.UpdateUser(prjZTB_Detail.BusinessManager, model.BusinessManager, model.PrjGuid);
                        prjZTB_Detail.BusinessManager = model.BusinessManager;
                        prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                        prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                        prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                        prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                        prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                        this.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                        prjZTB_Detail.ProgAgent = model.ProgAgent;
                        prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                        prjZTB_Detail.QualificationPassDate = model.QualificationPassDate;
                        prjZTB_Detail.QualificationPassReason = model.QualificationPassReason;
                        prjZTB_Detail.ProjTenderBeginDate = model.ProjTenderBeginDate;
                        prjZTB_Detail.TenderCeilingPrice = model.TenderCeilingPrice;
                        prjZTB_Detail.TenderQuote = model.TenderQuote;
                        prjZTB_Detail.TenderAppraiseMethod = model.TenderAppraiseMethod;
                        prjZTB_Detail.TenderAverage = model.TenderAverage;
                        prjZTB_Detail.ProjTenderCostContent = model.ProjTenderCostContent;
                        prjZTB_Detail.ProjTenderAnswerDate = model.ProjTenderAnswerDate;
                        prjZTB_Detail.ProjTenderEarnestMoney = model.ProjTenderEarnestMoney;
                        prjZTB_Detail.ProjTenderPayWay = model.ProjTenderPayWay;
                        prjZTB_Detail.ProjTenderContent = model.ProjTenderContent;
                        prjZTB_Detail.ProjTenderRemark = model.ProjTenderRemark;
                        prjZTB_Detail.OutBidDate = model.OutBidDate;
                        prjZTB_Detail.OutBidIsReturn = model.OutBidIsReturn;
                        prjZTB_Detail.OutBidRemark = model.OutBidRemark;
                    }
                    context.SaveChanges();
                }
            }
        }
        public void UpdatePart(TenderInfo model, string type)
        {
            using (pm2Entities context = new pm2Entities())
            {
                PT_PrjInfo_ZTB_Detail prjZTB_Detail = (
                    from m in context.PT_PrjInfo_ZTB_Detail
                    where m.PrjGuid == model.PrjGuid
                    select m).FirstOrDefault<PT_PrjInfo_ZTB_Detail>();
                if (prjZTB_Detail == null)
                {
                    prjZTB_Detail = new PT_PrjInfo_ZTB_Detail
                    {
                        PrjGuid = model.PrjGuid,
                        ProjFlowSate = new int?(-1)
                    };
                    context.AddToPT_PrjInfo_ZTB_Detail(prjZTB_Detail);
                }
                int prjState = 2;
                TenderInfo tender = new TenderInfo();
                if (type == ProjectParameter.Initiate)
                {
                    prjZTB_Detail.ProjStartDate = model.ProjStartDate;
                    string oldCode = prjZTB_Detail.PrjDutyPerson;
                    string newCode = model.PrjDutyPerson;
                    tender.UpdateUser(oldCode, newCode, model.PrjGuid);
                    prjZTB_Detail.PrjDutyPerson = model.PrjDutyPerson;
                    prjZTB_Detail.ProjStartRemark = model.ProjStartRemark;
                    prjState = int.Parse(ProjectParameter.Initiate);
                }
                else if (type == ProjectParameter.Bid)
                {
                    prjZTB_Detail.ProjTenderBeginDate = model.ProjTenderBeginDate;
                    prjZTB_Detail.TenderCeilingPrice = model.TenderCeilingPrice;
                    prjZTB_Detail.TenderQuote = model.TenderQuote;
                    prjZTB_Detail.TenderAppraiseMethod = model.TenderAppraiseMethod;
                    prjZTB_Detail.TenderAverage = model.TenderAverage;
                    prjZTB_Detail.ProjTenderCostContent = model.ProjTenderCostContent;
                    prjZTB_Detail.ProjTenderAnswerDate = model.ProjTenderAnswerDate;
                    prjZTB_Detail.ProjTenderEarnestMoney = model.ProjTenderEarnestMoney;
                    prjZTB_Detail.ProjTenderPayWay = model.ProjTenderPayWay;
                    prjZTB_Detail.ProjTenderContent = model.ProjTenderContent;
                    prjZTB_Detail.ProjTenderRemark = model.ProjTenderRemark;
                    prjZTB_Detail.TenderUnit = model.TenderUnit;
                    prjZTB_Detail.TenderProspect = model.TenderProspect;
                    tender.UpdateUser(prjZTB_Detail.TenderReadOne, model.TenderReadOne, model.PrjGuid);
                    prjZTB_Detail.TenderReadOne = model.TenderReadOne;
                    prjState = int.Parse(ProjectParameter.Bid);
                }
                else if (type == ProjectParameter.WinBid)
                {
                    prjZTB_Detail.SuccessBidDate = model.SuccessBidDate;
                    prjZTB_Detail.SuccessBidPrice = model.SuccessBidPrice;
                    prjZTB_Detail.SuccessBidRemark = model.SuccessBidRemark;
                    prjState = int.Parse(ProjectParameter.WinBid);
                }
                else if (type == ProjectParameter.OutBid)
                {
                    prjZTB_Detail.OutBidDate = model.OutBidDate;
                    prjZTB_Detail.OutBidIsReturn = model.OutBidIsReturn;
                    prjZTB_Detail.OutBidRemark = model.OutBidRemark;
                    prjState = int.Parse(ProjectParameter.OutBid);
                }
                else if (type == ProjectParameter.Prequalification)
                {
                    prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                    prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                    prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                    prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                    prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                    tender.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                    prjZTB_Detail.ProgAgent = model.ProgAgent;
                    prjZTB_Detail.QualificationMargin = model.QualificationMargin;
                    tender.UpdateUser(prjZTB_Detail.QualificationReadOne, model.QualificationReadOne, model.PrjGuid);
                    prjZTB_Detail.QualificationReadOne = model.QualificationReadOne;
                    prjState = int.Parse(ProjectParameter.Prequalification);
                }
                else if (type == ProjectParameter.QualificationPass)
                {
                    prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                    prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                    prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                    prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                    prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                    tender.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                    prjZTB_Detail.ProgAgent = model.ProgAgent;
                    prjZTB_Detail.QualificationMargin = model.QualificationMargin;
                    tender.UpdateUser(prjZTB_Detail.QualificationReadOne, model.QualificationReadOne, model.PrjGuid);
                    prjZTB_Detail.QualificationReadOne = model.QualificationReadOne;
                    prjZTB_Detail.QualificationPassDate = model.QualificationPassDate;
                    prjZTB_Detail.QualificationPassReason = model.QualificationPassReason;
                    prjState = int.Parse(ProjectParameter.QualificationPass);
                }
                else if (type == ProjectParameter.QualificationFail)
                {
                    prjZTB_Detail.ProjApplyDate = model.ProjApplyDate;
                    prjZTB_Detail.ProjApprovalDate = model.ProjApprovalDate;
                    prjZTB_Detail.ProjTenderDate = model.ProjTenderDate;
                    prjZTB_Detail.ProjRegistDeadline = model.ProjRegistDeadline;
                    prjZTB_Detail.PrequalificationRequire = model.PrequalificationRequire;
                    tender.UpdateUser(prjZTB_Detail.ProgAgent, model.ProgAgent, model.PrjGuid);
                    prjZTB_Detail.ProgAgent = model.ProgAgent;
                    prjZTB_Detail.QualificationMargin = model.QualificationMargin;
                    tender.UpdateUser(prjZTB_Detail.QualificationReadOne, model.QualificationReadOne, model.PrjGuid);
                    prjZTB_Detail.QualificationReadOne = model.QualificationReadOne;
                    prjZTB_Detail.QualificationFailData = model.QualificationFailData;
                    prjZTB_Detail.QualificationFailReason = model.QualificationFailReason;
                    prjState = int.Parse(ProjectParameter.QualificationFail);
                }
                if (type == ProjectParameter.Initiate.ToString() || type == ProjectParameter.Bid.ToString())
                {
                    this.SetPrjManager(model);
                }
                context.SaveChanges();
                TenderInfo.UpdatePrjState(model.PrjGuid, prjState);
            }
        }
        public string GetUserName(string userCode)
        {
            string result;
            using (pm2Entities context = new pm2Entities())
            {
                string userName = (
                    from m in context.PT_yhmc
                    where m.v_yhdm == userCode
                    select m.v_xm).FirstOrDefault<string>();
                result = (userName ?? string.Empty);
            }
            return result;
        }
        private static object GetCountOrSumTotal(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType, string expression, string OldState, string flowStateName)
        {
            prjName = prjName.Trim();
            prjCode = prjCode.Trim();
            buildUnit = buildUnit.Trim();
            start = start.Trim();
            end = end.Trim();
            person = person.Trim();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            strSql.Append("\r\n                    SELECT " + expression + "\r\n                FROM PT_PrjInfo_ZTB AS Tender\r\n                LEFT JOIN PT_PrjInfo_ZTB_Detail AS TDetail ON Tender.PrjGuid=TDetail.PrjGuid\r\n                LEFT JOIN XPM_Basic_ContactCorp AS Corp ON Tender.OwnerCode=CONVERT(VARCHAR(20),Corp.CorpID)\r\n                LEFT JOIN (SELECT CodeID,CodeName FROM XPM_Basic_CodeList WHERE TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND IsValid='1'\r\n                ) AS prjType ON Tender.PrjKindClass=CONVERT(VARCHAR(100),prjType.CodeID)");
            if (personType != 1)
            {
                if (personType == 2)
                {
                    strSql.Append("\r\nLEFT JOIN PT_yhmc AS YHMC ON TDetail.BusinessManager =YHMC.v_yhdm");
                }
                else if (personType != 3 && personType == 4)
                {
                    strSql.Append("\r\nLEFT JOIN PT_yhmc AS YHMC ON TDetail.PrjDutyPerson =YHMC.v_yhdm ");
                }
            }
            strSql.Append("\r\nWHERE 1=1");
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                strSql.Append("\r\nAND Tender.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName));
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                strSql.Append("\r\nAND Tender.PrjCode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode));
            }
            if (!string.IsNullOrEmpty(prjKindClass))
            {
                strSql.Append(" AND Tender.PrjGuid IN ( \n").Append("\tSELECT PrjGuid FROM PT_PrjInfo_Kind \n").AppendFormat("\tWHERE PrjGuid = Tender.PrjGuid AND PrjKind = @prjKindClass \n", new object[0]).Append(" ) \n");
                parameters.Add(new SqlParameter("@prjKindClass", prjKindClass));
            }
            if (!string.IsNullOrEmpty(buildUnit))
            {
                strSql.Append("\r\nAND Corp.CorpName LIKE '%'+@buildUnit +'%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit));
            }
            if (!string.IsNullOrEmpty(flowStateName))
            {
                strSql.Append(" AND tender." + flowStateName + "=1  ");
            }
            if (flowState != null && flowState.Count > 0)
            {
                string search = TenderInfo.GetSearch(flowState);
                if (flowState.Contains(-1))
                {
                    strSql.AppendFormat(" \r\nAND (TDetail.ProjFlowSate IN ({0}) OR TDetail.ProjFlowSate IS NULL) ", search);
                }
                else
                {
                    strSql.AppendFormat(" AND TDetail.ProjFlowSate IN ({0})", search);
                }
            }
            if (prjState != null && prjState.Count > 0)
            {
                string search2 = TenderInfo.GetSearch(prjState);
                if (string.IsNullOrEmpty(OldState))
                {
                    strSql.AppendFormat("  AND Tender.PrjState IN ({0}) ", search2);
                }
                else if (prjState.Count == 1)
                {
                    if (search2 == "'18'")
                    {
                        strSql.AppendFormat(" AND (Tender.PrjState=18 and Tender.OldState='" + OldState + "')", search2);
                    }
                    else
                    {
                        strSql.AppendFormat("  AND Tender.PrjState IN ({0}) ", search2);
                    }
                }
                else
                {
                    strSql.AppendFormat(" AND (Tender.PrjState IN ({0}) or (Tender.PrjState=18 and Tender.OldState='" + OldState + "'))", search2);
                }
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    strSql.AppendFormat(" AND Tender.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrEmpty(start))
            {
                strSql.Append(" AND  TDetail.InputDate >= @start ");
                parameters.Add(new SqlParameter("@start", start));
            }
            if (!string.IsNullOrEmpty(end))
            {
                strSql.Append(" AND TDetail.InputDate <= @end ");
                parameters.Add(new SqlParameter("@end", end + " 23:59:59"));
            }
            if (personType == 1 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND  TDetail.ProjPeopleName LIKE'%'+@setUpPerson+'%'", person);
                parameters.Add(new SqlParameter("@setUpPerson", person));
            }
            if (personType == 2 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND  YHMC.v_xm LIKE '%'+@businessManager+'%'", person);
                parameters.Add(new SqlParameter("@businessManager", person));
            }
            if (personType == 3 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND Tender.PrjManager LIKE '%'+@prjManager+'%'", person);
                parameters.Add(new SqlParameter("@prjManager", person));
            }
            if (personType == 4 && !string.IsNullOrEmpty(person))
            {
                strSql.AppendFormat(" AND YHMC.v_xm LIKE '%'+@prjDuty+'%'", person);
                parameters.Add(new SqlParameter("@prjDuty", person));
            }
            return SqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters.ToArray());
        }
        private static string GetTenderSqlCondition(string prjName, string prjCode, string buildUnit, string prjKindClass, string start, string end, System.Collections.Generic.List<int> prjState, System.Collections.Generic.List<int> flowState, string userCode, string person, int personType, string flowStateField)
        {
            System.Text.StringBuilder sqlstr = new System.Text.StringBuilder();
            if (personType == 1)
            {
                sqlstr.Append(" LEFT JOIN PT_yhmc AS YHMC ON TDetail.ProjPeopleName = YHMC.v_yhdm \n");
            }
            else if (personType == 2)
            {
                sqlstr.Append(" LEFT JOIN PT_yhmc AS YHMC ON TDetail.BusinessManager = YHMC.v_yhdm \n");
            }
            else if (personType == 3)
            {
                sqlstr.Append(" LEFT JOIN PT_yhmc AS YHMC ON Tender.PrjManager = YHMC.v_yhdm \n");
            }
            else if (personType == 4)
            {
                sqlstr.Append(" LEFT JOIN PT_yhmc AS YHMC ON TDetail.PrjDutyPerson = YHMC.v_yhdm \n");
            }
            sqlstr.Append(" WHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                sqlstr.AppendFormat(" AND Tender.PrjName LIKE '%{0}%' \n", prjName.Trim());
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                sqlstr.AppendFormat(" AND Tender.PrjCode LIKE '%{0}%' \n", prjCode.Trim());
            }
            if (!string.IsNullOrEmpty(prjKindClass))
            {
                sqlstr.Append(" AND Tender.PrjGuid IN ( \n").Append("\tSELECT PrjGuid FROM PT_PrjInfo_Kind \n").AppendFormat("\tWHERE PrjGuid = Tender.PrjGuid AND PrjKind ={0} \n", prjKindClass).Append(" ) \n");
            }
            if (!string.IsNullOrEmpty(buildUnit))
            {
                sqlstr.AppendFormat(" AND Corp.CorpName LIKE '%{0}%' \n", buildUnit);
            }
            if (flowState != null && flowState.Count > 0)
            {
                string search = TenderInfo.GetSearch(flowState);
                if (flowState.Contains(-1))
                {
                    sqlstr.AppendFormat(" AND (Tender.{0} IN ({1}) OR Tender.{0} IS NULL) \n", flowStateField, search);
                }
                else
                {
                    sqlstr.AppendFormat(" AND Tender.{0} IN ({1}) \n", flowStateField, search);
                }
            }
            if (prjState != null && prjState.Count > 0)
            {
                string search2 = TenderInfo.GetSearch(prjState);
                sqlstr.AppendFormat(" AND Tender.PrjState IN ({0}) \n", search2);
            }
            else
            {
                sqlstr.AppendFormat(" AND Tender.PrjState not IN ('18','5') \n", new object[0]);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    sqlstr.AppendFormat(" AND Tender.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    sqlstr.AppendFormat(" AND Tender.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrEmpty(start))
            {
                sqlstr.AppendFormat(" AND  TDetail.InputDate >= '{0}' \n", start);
            }
            if (!string.IsNullOrEmpty(end))
            {
                sqlstr.AppendFormat(" AND TDetail.InputDate <= '{0}' \n", end + " 23:59:59");
            }
            if (personType == 1 && !string.IsNullOrEmpty(person))
            {
                sqlstr.AppendFormat(" AND  TDetail.ProjPeopleName LIKE '%{0}%' \n", person);
            }
            if (personType == 2 && !string.IsNullOrEmpty(person))
            {
                sqlstr.AppendFormat(" AND  YHMC.v_xm LIKE '%{0}%' \n", person);
            }
            if (personType == 3 && !string.IsNullOrEmpty(person))
            {
                sqlstr.AppendFormat(" AND Tender.PrjManager LIKE '%{0}%' \n", person);
            }
            if (personType == 4 && !string.IsNullOrEmpty(person))
            {
                sqlstr.AppendFormat(" AND YHMC.v_xm LIKE '%{0}%' \n", person);
            }
            return sqlstr.ToString();
        }
        private static string GetSearch(System.Collections.Generic.List<int> obj)
        {
            string search = "";
            System.Collections.Generic.List<int> instanceObj = new System.Collections.Generic.List<int>(obj.ToArray());
            if (instanceObj.Count > 1)
            {
                for (int i = 0; i < instanceObj.Count; i++)
                {
                    int item = instanceObj[i];
                    if (item == 18)
                    {
                        instanceObj.RemoveAt(i);
                        break;
                    }
                }
            }
            if (instanceObj != null && instanceObj.Count > 0)
            {
                using (System.Collections.Generic.List<int>.Enumerator enumerator = instanceObj.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        int item2 = enumerator.Current;
                        object obj2 = search;
                        search = string.Concat(new object[]
						{
							obj2,
							"'",
							item2,
							"',"
						});
                    }
                    goto IL_B6;
                }
            }
            search = "''";
        IL_B6:
            search = search.Substring(0, search.Length - 1);
            return search;
        }
        private static string GetSearchAtGiveUp(System.Collections.Generic.List<int> obj)
        {
            string search = "";
            if (obj != null && obj.Count > 0)
            {
                using (System.Collections.Generic.List<int>.Enumerator enumerator = obj.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        int item = enumerator.Current;
                        object obj2 = search;
                        search = string.Concat(new object[]
						{
							obj2,
							"'",
							item,
							"',"
						});
                    }
                    goto IL_75;
                }
            }
            search = "''";
        IL_75:
            search = search.Substring(0, search.Length - 1);
            return search;
        }
        private void AddDefaultLimit(TenderInfo mode)
        {
            System.Collections.Generic.List<string> codes = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(mode.InputUser))
            {
                codes.Add(mode.InputUser);
            }
            if (mode.InputUser != "00000000")
            {
                codes.Add("00000000");
            }
            if (!string.IsNullOrEmpty(mode.ProgAgent))
            {
                codes.Add(mode.ProgAgent);
            }
            if (!string.IsNullOrEmpty(mode.BusinessManager))
            {
                codes.Add(mode.BusinessManager);
            }
            if (!string.IsNullOrEmpty(mode.PrjDutyPerson))
            {
                codes.Add(mode.PrjDutyPerson);
            }
            if (!string.IsNullOrEmpty(mode.PrjReadOne))
            {
                codes.Add(mode.PrjReadOne);
            }
            string guid = mode.PrjGuid.ToString();
            codes = codes.Distinct<string>().ToList<string>();
            foreach (string code in codes)
            {
                TenderUser.Add(guid, code);
            }
        }
        private void SetPrjManager(TenderInfo model)
        {
            if (model != null)
            {
                using (pm2Entities context = new pm2Entities())
                {
                    PT_PrjInfo_ZTB prjZTB = (
                        from m in context.PT_PrjInfo_ZTB
                        where m.PrjGuid == model.PrjGuid
                        select m).FirstOrDefault<PT_PrjInfo_ZTB>();
                    if (prjZTB != null)
                    {
                        string arg_B7_0 = prjZTB.PrjManager;
                        string newCode = model.PrjManager;
                        prjZTB.PrjManager = newCode;
                        context.SaveChanges();
                    }
                }
            }
        }
        private void UpdateUser(string oldCode, string newCode, System.Guid guid)
        {
            if (!string.IsNullOrEmpty(newCode) && newCode != oldCode && newCode != "00000000" && !TenderUser.isExist(guid, newCode))
            {
                TenderUser.Add(guid.ToString(), newCode);
            }
        }
        private static object GetTenderReportSumTotalOrCount(string prjCode, string prjName, string prjType, string prjManager, string dutyPersonName, string setUpPerson, string prjState, string startDate, string endDate, string buildUnit, string ProjPeopleDep, string dropPrjProperty, string principal, string userCode, bool isSumTotal, string PrjStateChangeTimeStart, string PrjStateChangeTimeEnd)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            if (isSumTotal)
            {
                sql.Append("SELECT CONVERT(DECIMAL(18,3),SUM(ISNULL(ZTB.PrjCost,0))) ");
            }
            else
            {
                sql.Append("SELECT COUNT(*) ");
            }
            sql.Append("\r\n\tFROM PT_PrjInfo_ZTB ZTB  \r\n\tLEFT JOIN PT_PrjInfo_ZTB_Detail Detail ON ZTB.PrjGuid=Detail.PrjGuid  \r\n\tLEFT JOIN (SELECT CodeId,CodeName FROM XPM_Basic_CodeList WHERE TYPEID=\r\n\t\t(SELECT TypeId FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') AND ISVALID='1') CodeList  \r\n\t\tON ZTB.PrjKindClass=CAST(CodeList.CodeId AS NVARCHAR(30))  \r\n\tLEFT JOIN XPM_Basic_ContactCorp Corp ON ZTB.OwnerCode=Corp.CorpID \r\n\tLEFT JOIN (SELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode='ProjectState') CostList  \r\n\t\tON ZTB.PrjState=CostList.ItemCode \r\n\tLEFT JOIN Pt_PrjInfo PrjInfo ON ZTB.PrjGuid=PrjInfo.PrjGuid \r\n\tLEFT JOIN (SELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode='ProjectState') CostPrjList  \r\n\t\tON PrjInfo.PrjState=CostPrjList.ItemCode \r\n    --项目经理\r\n\t--LEFT JOIN PT_yhmc AS YHMC1 ON ZTB.PrjManager =YHMC1.v_yhdm\r\n\t--项目跟进人\r\n\tLEFT JOIN PT_yhmc AS YHMC2 ON Detail.BusinessManager =YHMC2.v_yhdm\r\n    --项目跟踪人\r\n\tLEFT JOIN PT_yhmc AS YHMC3 ON Detail.PrjDutyPerson =YHMC3.v_yhdm\r\n\r\n\tWHERE  ZTB.PrjState!=1 ");
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    sql.AppendFormat(" AND ZTB.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    sql.AppendFormat(" AND ZTB.PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                sql.Append(" AND ZTB.Prjcode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjName))
            {
                sql.Append(" AND ZTB.PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjType))
            {
                parameters.Add(new SqlParameter("@prjType", prjType));
                sql.Append(" AND ZTB.PrjGuid IN ( \n").Append("\tSELECT PrjGuid FROM PT_PrjInfo_Kind \n").AppendFormat("\tWHERE PrjGuid = ZTB.PrjGuid AND PrjKind = @prjType \n", new object[0]).Append(" ) \n");
            }
            if (!string.IsNullOrWhiteSpace(prjManager))
            {
                sql.Append(" AND ZTB.PrjManager LIKE '%'+@prjManagaer+'%' ");
                parameters.Add(new SqlParameter("@prjManagaer", prjManager.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(prjState))
            {
                sql.Append(" AND ZTB.prjState = @prjState ");
                parameters.Add(new SqlParameter("@prjState", prjState));
            }
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                sql.Append(" AND InputDate >= @startDate ");
                parameters.Add(new SqlParameter("@startDate", startDate.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                sql.Append(" AND InputDate <= @endDate ");
                parameters.Add(new SqlParameter("@endDate", endDate.Trim() + " 23:59:59"));
            }
            if (!string.IsNullOrWhiteSpace(buildUnit))
            {
                sql.Append(" AND CorpName LIKE  '%'+  @buildUnit +'%' ");
                parameters.Add(new SqlParameter("@buildUnit", buildUnit.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(setUpPerson))
            {
                sql.Append(" AND ProjPeopleName LIKE  '%'+  @setUpPerson +'%' ");
                parameters.Add(new SqlParameter("@setUpPerson", setUpPerson.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(dutyPersonName))
            {
                sql.Append(" AND YHMC3.v_xm LIKE  '%'+  @dutyPersonName +'%' ");
                parameters.Add(new SqlParameter("@dutyPersonName", dutyPersonName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(ProjPeopleDep))
            {
                sql.Append(" AND Detail.ProjPeopleDep LIKE '%'+ @ProjPeopleDep +'%'");
                parameters.Add(new SqlParameter("@ProjPeopleDep", ProjPeopleDep.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(dropPrjProperty))
            {
                sql.Append(" AND Detail.PrjProperty LIKE '%'+ @dropPrjProperty +'%'");
                parameters.Add(new SqlParameter("@dropPrjProperty", dropPrjProperty.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(principal))
            {
                sql.Append(" AND ZTB.WorkUnit LIKE '%'+ @principal +'%'");
                parameters.Add(new SqlParameter("@principal", principal.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(PrjStateChangeTimeStart))
            {
                sql.AppendFormat(" AND PrjStateChangeTime >='{0}' ", PrjStateChangeTimeStart);
            }
            if (!string.IsNullOrWhiteSpace(PrjStateChangeTimeEnd))
            {
                sql.AppendFormat(" AND PrjStateChangeTime <='{0}' ", PrjStateChangeTimeEnd + " 23:59:59");
            }
            return SqlHelper.ExecuteScalar(CommandType.Text, sql.ToString(), parameters.ToArray());
        }
        private string GetCorpName(string code)
        {
            string result;
            using (pm2Entities context = new pm2Entities())
            {
                try
                {
                    int id = System.Convert.ToInt32(code);
                    string corpName = (
                        from m in context.XPM_Basic_ContactCorp
                        where m.CorpID == id
                        select m.CorpName).FirstOrDefault<string>();
                    result = (corpName ?? string.Empty);
                }
                catch
                {
                    result = string.Empty;
                }
            }
            return result;
        }
        private static string GenerateGetTendersSql(string code, string name, string setUpPerson, string setUpDep, string kind, string startDate, string endDate, string unitName, string flowState, string property, string userCode)
        {
            System.Text.StringBuilder condition = new System.Text.StringBuilder();
            if (!string.IsNullOrWhiteSpace(code))
            {
                condition.AppendFormat(" AND PrjCode LIKE '%{0}%' \n", code);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                condition.AppendFormat(" AND Prjname LIKE '%{0}%' \n", name);
            }
            if (!string.IsNullOrWhiteSpace(setUpPerson))
            {
                condition.AppendFormat(" AND ProjPeopleName LIKE '%{0}%' \n", setUpPerson);
            }
            if (!string.IsNullOrWhiteSpace(setUpDep))
            {
                condition.AppendFormat(" AND ProjPeopleDep LIKE '%{0}%' \n", setUpDep);
            }
            if (!string.IsNullOrWhiteSpace(kind))
            {
                condition.AppendFormat(" AND PrjGuid IN (SELECT PrjGuid FROM PT_PrjInfo_Kind WHERE PrjGuid = vTender.PrjGuid AND PrjKind = '{0}') \n", kind);
            }
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                condition.AppendFormat(" AND InputDate >= '{0}' \n", startDate);
            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                condition.AppendFormat(" AND InputDate <= '{0}' \n", endDate + " 23:59:59");
            }
            if (!string.IsNullOrWhiteSpace(unitName))
            {
                condition.AppendFormat(" AND WorkUnitName LIKE '%{0}%' \n", unitName);
            }
            if (!string.IsNullOrWhiteSpace(flowState))
            {
                condition.AppendFormat(" AND ProjFlowSate = '{0}' \n", flowState);
            }
            if (!string.IsNullOrWhiteSpace(property))
            {
                condition.AppendFormat(" AND PrjProperty LIKE '%{0}%' \n", property);
            }
            if (!string.IsNullOrWhiteSpace(userCode))
            {
                string[] guidArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (guidArr.Length == 0)
                {
                    condition.AppendFormat(" AND PrjGuid is null  \n", new object[0]);
                }
                else
                {
                    condition.AppendFormat(" AND PrjGuid IN ({0}) \n", DBHelper.GetInParameterSql(guidArr));
                }
            }
            condition.AppendFormat(" AND PrjState IN ('1','2') ", new object[0]);
            return condition.ToString();
        }
        private static string GenerateGetTenderPrivilegeSql(string prjCode, string prjName, string startDate, string endDate, string prjManageName, string prjState, string userCode, string kind, string setUpDep, string prjProperty)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                sql.AppendFormat(" AND V.PrjCode LIKE '%{0}%'  \n ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                sql.AppendFormat(" AND V.PrjName LIKE '%{0}%'  \n ", prjName);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.AppendFormat(" AND V.InputDate >= '{0}'  \n ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.AppendFormat(" AND V.InputDate < '{0}' \n ", endDate);
            }
            if (!string.IsNullOrEmpty(prjManageName))
            {
                sql.AppendFormat(" AND V.PrjMangerName LIKE '%{0}%'  \n ", prjManageName);
            }
            if (!string.IsNullOrEmpty(prjState))
            {
                sql.AppendFormat(" AND V.PrjState = '{0}'  \n ", prjState);
            }
            if (!string.IsNullOrWhiteSpace(setUpDep))
            {
                sql.AppendFormat(" AND V.ProjPeopleDep LIKE '%{0}%' \n ", setUpDep);
            }
            if (!string.IsNullOrWhiteSpace(prjProperty))
            {
                sql.AppendFormat(" AND V.PrjProperty = '{0}' \n ", prjProperty);
            }
            if (!string.IsNullOrWhiteSpace(kind))
            {
                sql.AppendFormat(" AND V.PrjGuid IN (SELECT PrjGuid FROM PT_PrjInfo_Kind WHERE PrjGuid = V.PrjGuid AND PrjKind = '{0}') \n", kind);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] userArr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (userArr.Length == 0)
                {
                    sql.AppendFormat(" AND V.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    sql.AppendFormat(" AND V.PrjGuid IN({0}) \n", DBHelper.GetInParameterSql(userArr));
                }
            }
            return sql.ToString();
        }
    }
}
