using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using cn.justwin.Tender;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
namespace cn.justwin.PrjManager
{
    public class ProjectInfo
    {
        private static string IsIncomeContractApprove = ConfigHelper.Get("IsIncomeContractApprove");
        private System.Collections.Generic.IList<EngineeringType> engineeringTypes;
        private System.Collections.Generic.IList<ProjectKind> prjTypes;
        private System.Collections.Generic.IList<ProjectRank> prjRanks;
        public string TypeCode
        {
            get;
            set;
        }
        public int I_xh
        {
            get;
            set;
        }
        public string IsValid
        {
            get;
            set;
        }
        public string UserCode
        {
            get;
            set;
        }
        public string RecordDate
        {
            get;
            set;
        }
        public int I_ChildNum
        {
            get;
            set;
        }
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
        public int? PrjState
        {
            get;
            set;
        }
        public string MarketInfoGuid
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
        public string LinkMan
        {
            get;
            set;
        }
        public string LinkManUserName
        {
            get
            {
                return this.GetUserName(this.LinkMan);
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
        public string UndergroundArea
        {
            get;
            set;
        }
        public string PrjFundInfo
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
        public string OtherStatement
        {
            get;
            set;
        }
        public string Podepom
        {
            get;
            set;
        }
        public bool IsConfirm
        {
            get;
            set;
        }
        public string PrjStateRemark
        {
            get;
            set;
        }
        public string Xmgroup
        {
            get;
            set;
        }
        public string PrjGrade
        {
            get;
            set;
        }
        public string Businessman
        {
            get;
            set;
        }
        public string BusinessmanName
        {
            get
            {
                return this.GetUserName(this.Businessman);
            }
        }
        public string Telephone
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
        public string Grade
        {
            get;
            set;
        }
        public string EngineeringSubType
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
        public bool IsTender
        {
            get;
            set;
        }
        public System.DateTime? ActualRunDate
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
        public string OwnerLinkMan
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
        public string InputUser
        {
            get;
            set;
        }
        public System.DateTime? InputDate
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
        public System.Collections.Generic.IList<ProjectKind> PrjTypes
        {
            get
            {
                if (this.prjTypes == null)
                {
                    this.prjTypes = ProjectKind.GetProjectKinds(this.PrjGuid);
                }
                return this.prjTypes;
            }
            set
            {
                this.prjTypes = value;
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
        public void Add(ProjectInfo model)
        {
            using (pm2Entities context = new pm2Entities())
            {
                if (model != null)
                {
                    int? query = (
                        from m in context.PT_PrjInfo
                        select m.i_xh).Max<int?>();
                    if (!query.HasValue)
                    {
                        query = new int?(0);
                    }
                    PT_PrjInfo prjInfo = new PT_PrjInfo
                    {
                        PrjGuid = new System.Guid?(model.PrjGuid),
                        TypeCode = model.TypeCode,
                        i_xh = query + 1,
                        IsValid = "1",
                        i_ChildNum = new int?(0),
                        PrjCode = model.PrjCode,
                        PrjName = model.PrjName,
                        RecordDate = new System.DateTime?(System.DateTime.Now),
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        ComputeClass = "1",
                        RationClass = "",
                        PrjCost = model.PrjCost,
                        ContractSum = "",
                        Duration = model.Duration,
                        QualityClass = model.QualityClass,
                        Area = model.Area,
                        PrjPlace = model.PrjPlace,
                        Remark1 = model.Remark,
                        Counsellor = model.Counsellor,
                        Designer = model.Designer,
                        Inspector = model.Inspector,
                        PrjInfo = model.PrjInfo,
                        PrjState = model.PrjState,
                        OwnerCode = model.OwnerCode,
                        MarketInfoGuid = new System.Guid?(System.Guid.Empty),
                        BudgetWay = model.BudgetWay,
                        ContractWay = model.ContractWay,
                        PayCondition = model.PayCondition,
                        TenderWay = model.TenderWay,
                        PayWay = model.PayWay,
                        KeyPart = model.KeyPart,
                        WorkUnit = model.WorkUnit,
                        LinkMan = model.LinkMan,
                        PrjManager = model.PrjManager,
                        TotalHouseNum = model.TotalHouseNum,
                        BuildingArea = model.BuildingArea,
                        UsegrounArea = model.UsegrounArea,
                        UndergroundArea = model.UndergroundArea,
                        PrjFundInfo = model.PrjFundInfo,
                        OtherStatement = model.OtherStatement,
                        xmgroup = model.Xmgroup,
                        grade = model.PrjGrade,
                        businessman = model.Businessman,
                        telephone = model.Telephone,
                        UserCode = model.UserCode
                    };
                    context.AddToPT_PrjInfo(prjInfo);
                    PT_PrjInfo_ZTB_Detail ztb = new PT_PrjInfo_ZTB_Detail
                    {
                        PrjGuid = model.PrjGuid,
                        ProgAgent = model.ProgAgent,
                        ProjFlowSate = new int?(-1),
                        EngineeringType = model.EngineeringType,
                        EngineeringSubType = model.EngineeringSubType,
                        Grade = model.Grade,
                        IsTender = false,
                        ProjPeopleName = model.ProjPeopleUserName,
                        ProjPeopleDep = model.ProjPeopleCorp,
                        ProjPeopleDuty = model.ProjPeopleDuty,
                        ProjPeopleTel = model.ProjPeopleTel,
                        OwnerLinkMan = model.OwnerLinkMan,
                        OwnerLinkPhone = model.OwnerLinkPhone,
                        OwnerAddress = model.OwnerAddress,
                        ForecastProfitRate = model.ForecastProfitRate,
                        EngineeringEstimates = model.EngineeringEstimates,
                        PrjDutyPerson = model.PrjDutyPerson,
                        PrjApprovalOf = model.PrjApprovalOf,
                        PrjFundWorkable = model.PrjFundWorkable,
                        ManagementMargin = model.ManagementMargin,
                        MigrantQualityMarginRate = model.MigrantQualityMarginRate,
                        WithholdingTaxRate = model.WithholdingTaxRate,
                        PerformanceBond = model.PerformanceBond,
                        ElseMargin = model.ElseMargin,
                        InputUser = model.InputUser,
                        InputDate = model.InputDate,
                        ActualRunDate = model.ActualRunDate,
                        PrjReadOne = model.PrjReadOne,
                        ProjInfoOrigin = model.ProjInfoOrigin,
                        ProjElseRequest = model.ProjElseRequest,
                        Telephone = model.Telephone,
                        PrjProperty = model.PrjProperty,
                        MemberFlowState = new int?(-1),
                        CompletedFlowState = new int?(-1),
                        PrjManagerRequire = model.PrjManagerRequire,
                        TechnicalLeaderRequire = model.TechnicalLeaderRequire,
                        Province = model.Province,
                        City = model.City,
                        SetUpFlowState = new int?(-1),
                        AfforestArea = model.AfforestArea,
                        ParkArea = model.ParkArea
                    };
                    context.AddToPT_PrjInfo_ZTB_Detail(ztb);
                    context.SaveChanges();
                    this.AddDefaultLimit(model);
                    cn.justwin.Tender.EngineeringType.UpdateEngineerType(model.EngineeringTypes, model.PrjGuid.ToString(), context);
                    ProjectKind.Update(model.PrjGuid.ToString(), model.PrjTypes, context);
                    ProjectRank.Update(model.PrjGuid.ToString(), model.prjRanks, context);
                    string typecode = model.TypeCode;
                    if (typecode.Length > 5)
                    {
                        int num = (
                            from n in context.PT_PrjInfo
                            where n.TypeCode.StartsWith(typecode.Substring(0, 5)) && n.TypeCode.Length == typecode.Length
                            select n).Count<PT_PrjInfo>();
                        PT_PrjInfo prj = (
                            from a in context.PT_PrjInfo
                            where a.TypeCode == typecode.Substring(0, 5)
                            select a).FirstOrDefault<PT_PrjInfo>();
                        prj.i_ChildNum = new int?(num);
                    }
                    context.SaveChanges();
                }
            }
        }
        public int AddByTender(string PrjId, string UpPrjGuid, string xmgroup, string date)
        {
            string TypeCode = ProjectInfo.GetTypeCode(UpPrjGuid);
            string i_xh = "";
            using (pm2Entities context = new pm2Entities())
            {
                i_xh = (
                    from m in context.PT_PrjInfo
                    select m.i_xh).Max<int?>().ToString();
                context.SaveChanges();
            }
            if (string.IsNullOrEmpty(i_xh))
            {
                i_xh = "1";
            }
            else
            {
                i_xh = (int.Parse(i_xh) + 1).ToString();
            }
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append("\r\n\t\t\t\tDECLARE @user AS nvarchar(4000)\r\t\t\t\tSELECT @user= ISNULL(@user + ',' ,',') + UserCode \r\t\t\t\tFROM (\r\t\t\t\t\tSELECT DISTINCT UserCode FROM PT_PrjInfo_ZTB_User \r\t\t\t\t\tWHERE PrjGuid = '" + PrjId + "'\r\t\t\t\t)AS T\r\t\t\t\tSELECT @user AS users");
            string users = string.Empty;
            try
            {
                users = SqlHelper.ExecuteScalar(CommandType.Text, sql.ToString(), new SqlParameter[0]).ToString();
            }
            catch
            {
            }
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.AppendFormat(" INSERT INTO PT_PrjInfo (TypeCode,i_xh,PrjState,xmgroup,UserCode,PrjCode,PrjGuid,PrjName,StartDate,EndDate,ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,\r\nArea,PrjKindClass,PrjPlace,Remark1,Owner,Counsellor,Designer,Inspector,PrjInfo,OwnerCode,MarketInfoGuid,Rank,BudgetWay,\r\nContractWay,PayCondition,TenderWay,PayWay,KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,\r\nUsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,Podepom\r\n)\r\nSELECT '{0}','{1}',5,'{2}',InputUser, PrjCode,PT_PrjInfo_ZTB.PrjGuid,PrjName,StartDate,EndDate,ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,\r\nArea,PrjKindClass,PrjPlace,Remark,Owner,Counsellor,Designer,Inspector,PrjInfo,OwnerCode,MarketInfoGuid,Rank,BudgetWay,\r\nContractWay,PayCondition,TenderWay,PayWay,KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,\r\nUsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,'{4}' FROM PT_PrjInfo_ZTB \r\nLEFT JOIN  PT_PrjInfo_ZTB_Detail ON PT_PrjInfo_ZTB.PrjGuid=PT_PrjInfo_ZTB_Detail.PrjGuid  WHERE PT_PrjInfo_ZTB.PrjGuid='{3}'", new object[]
			{
				TypeCode,
				i_xh,
				xmgroup,
				PrjId,
				users
			});
            if (!string.IsNullOrEmpty(date))
            {
                strSql.AppendFormat("UPDATE PT_PrjInfo_ZTB_Detail SET ActualRunDate= '{0}' WHERE PrjGuid='{1}'", date, PrjId);
            }
            strSql.AppendFormat("UPDATE PT_PrjInfo_ZTB_Detail SET SetUpFlowState=1 WHERE PrjGuid='{0}'", PrjId);
            if (!string.IsNullOrEmpty(UpPrjGuid))
            {
                strSql.AppendFormat("UPDATE Pt_PrjInfo SET i_ChildNum=i_ChildNum+1 WHERE PrjGuid='{0}'", UpPrjGuid);
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), new SqlParameter[0]);
        }
        public void UpdatTenderAndProjectStartDate(string prjGuid, System.DateTime? startDate)
        {
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid guid = new System.Guid(prjGuid);
                PT_PrjInfo_ZTB tender = (
                    from m in context.PT_PrjInfo_ZTB
                    where m.PrjGuid == guid
                    select m).FirstOrDefault<PT_PrjInfo_ZTB>();
                if (tender != null)
                {
                    tender.StartDate = startDate;
                }
                PT_PrjInfo project = (
                    from m in context.PT_PrjInfo
                    where m.PrjGuid == (System.Guid?)guid
                    select m).FirstOrDefault<PT_PrjInfo>();
                if (project != null)
                {
                    project.StartDate = startDate;
                }
                context.SaveChanges();
            }
        }
        public void Update(ProjectInfo model, string prjGuid)
        {
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid guid = new System.Guid(prjGuid);
                PT_PrjInfo project = (
                    from m in context.PT_PrjInfo
                    where m.PrjGuid == (System.Guid?)guid
                    select m).FirstOrDefault<PT_PrjInfo>();
                if (project != null)
                {
                    project.PrjCode = model.PrjCode;
                    project.PrjName = model.PrjName;
                    project.StartDate = model.StartDate;
                    project.EndDate = model.EndDate;
                    project.PrjCost = model.PrjCost;
                    project.Duration = model.Duration;
                    project.QualityClass = model.QualityClass;
                    project.Area = model.Area;
                    project.PrjPlace = model.PrjPlace;
                    project.Remark1 = model.Remark;
                    project.Counsellor = model.Counsellor;
                    project.Designer = model.Designer;
                    project.Inspector = model.Inspector;
                    project.PrjInfo = model.PrjInfo;
                    project.PrjState = model.PrjState;
                    project.OwnerCode = model.OwnerCode;
                    project.BudgetWay = model.BudgetWay;
                    project.ContractWay = model.ContractWay;
                    project.PayCondition = model.PayCondition;
                    project.TenderWay = model.TenderWay;
                    project.PayWay = model.PayWay;
                    project.KeyPart = model.KeyPart;
                    project.WorkUnit = model.WorkUnit;
                    project.LinkMan = model.LinkMan;
                    project.PrjManager = model.PrjManager;
                    project.BuildingType = model.BuildingType;
                    project.TotalHouseNum = model.TotalHouseNum;
                    project.BuildingArea = model.BuildingArea;
                    project.UsegrounArea = model.UsegrounArea;
                    project.UndergroundArea = model.UndergroundArea;
                    project.PrjFundInfo = model.PrjFundInfo;
                    project.OtherStatement = model.OtherStatement;
                    project.xmgroup = model.Xmgroup;
                    project.grade = model.PrjGrade;
                    this.UpdateUserPrimit(project.businessman, model.Businessman, guid);
                    project.businessman = model.Businessman;
                    PT_PrjInfo_ZTB_Detail ztb = (
                        from n in context.PT_PrjInfo_ZTB_Detail
                        where n.PrjGuid == guid
                        select n).FirstOrDefault<PT_PrjInfo_ZTB_Detail>();
                    this.UpdateUserPrimit(ztb.ProgAgent, model.ProgAgent, guid);
                    ztb.ProgAgent = model.ProgAgent;
                    ztb.EngineeringType = model.EngineeringType;
                    ztb.EngineeringSubType = model.EngineeringSubType;
                    ztb.Grade = model.Grade;
                    ztb.ProjPeopleName = model.ProjPeopleUserName;
                    ztb.ProjPeopleDep = model.ProjPeopleCorp;
                    ztb.ProjPeopleDuty = model.ProjPeopleDuty;
                    ztb.ProjPeopleTel = model.ProjPeopleTel;
                    ztb.OwnerLinkMan = model.OwnerLinkMan;
                    ztb.OwnerLinkPhone = model.OwnerLinkPhone;
                    ztb.OwnerAddress = model.OwnerAddress;
                    ztb.ForecastProfitRate = model.ForecastProfitRate;
                    ztb.EngineeringEstimates = model.EngineeringEstimates;
                    ztb.AfforestArea = model.AfforestArea;
                    ztb.ParkArea = model.ParkArea;
                    this.UpdateUserPrimit(ztb.PrjDutyPerson, model.PrjDutyPerson, guid);
                    ztb.PrjDutyPerson = model.PrjDutyPerson;
                    ztb.PrjApprovalOf = model.PrjApprovalOf;
                    ztb.PrjFundWorkable = model.PrjFundWorkable;
                    ztb.ManagementMargin = model.ManagementMargin;
                    ztb.MigrantQualityMarginRate = model.MigrantQualityMarginRate;
                    ztb.WithholdingTaxRate = model.WithholdingTaxRate;
                    ztb.PerformanceBond = model.PerformanceBond;
                    ztb.ElseMargin = model.ElseMargin;
                    ztb.ActualRunDate = model.ActualRunDate;
                    ztb.PrjManagerRequire = model.PrjManagerRequire;
                    ztb.TechnicalLeaderRequire = model.TechnicalLeaderRequire;
                    ztb.Province = model.Province;
                    ztb.City = model.City;
                    this.UpdateUserPrimit(ztb.PrjReadOne, model.PrjReadOne, guid);
                    ztb.PrjReadOne = model.PrjReadOne;
                    ztb.ProjInfoOrigin = model.ProjInfoOrigin;
                    ztb.ProjElseRequest = model.ProjElseRequest;
                    ztb.Telephone = model.Telephone;
                    ztb.PrjProperty = model.PrjProperty;
                    ProjectKind.Update(prjGuid.ToString(), model.PrjTypes, context);
                    ProjectRank.Update(prjGuid.ToString(), model.prjRanks, context);
                    cn.justwin.Tender.EngineeringType.UpdateEngineerType(model.EngineeringTypes, model.PrjGuid.ToString(), context);
                    context.SaveChanges();
                }
            }
        }
        public static void UpdatePrjState(string prjGuid, string prjState)
        {
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid guid = new System.Guid(prjGuid);
                PT_PrjInfo prj = (
                    from m in context.PT_PrjInfo
                    where m.PrjGuid == (System.Guid?)guid
                    select m).FirstOrDefault<PT_PrjInfo>();
                prj.PrjState = new int?(int.Parse(prjState));
                context.SaveChanges();
            }
        }
        public static void UpdatePrjGroup(string prjId, string prjGroup)
        {
            using (pm2Entities context = new pm2Entities())
            {
                PT_PrjInfo prj = (
                    from m in context.PT_PrjInfo
                    where m.PrjGuid == (System.Guid?)new System.Guid(prjId)
                    select m).FirstOrDefault<PT_PrjInfo>();
                prj.xmgroup = prjGroup;
                context.SaveChanges();
            }
        }
        public static void BindPrj(DropDownList drop)
        {
            using (pm2Entities context = new pm2Entities())
            {
                System.Collections.Generic.List<PT_PrjInfo> query = (
                    from m in context.PT_PrjInfo
                    join n in context.PT_PrjInfo_ZTB_Detail on m.PrjGuid equals (System.Guid?)n.PrjGuid
                    where m.TypeCode.Length == 5 && n.IsTender == true
                    select m).ToList<PT_PrjInfo>();
                drop.DataSource = query;
                drop.DataTextField = "PrjName";
                drop.DataValueField = "PrjGuid";
                drop.DataBind();
                drop.Items.Insert(0, new ListItem("", ""));
            }
        }
        public static bool IsSameCode(string prjCode)
        {
            int count = 0;
            using (pm2Entities context = new pm2Entities())
            {
                count = (
                    from m in context.PT_PrjInfo
                    where m.PrjCode == prjCode
                    select m).Count<PT_PrjInfo>();
            }
            return count > 0;
        }
        public static bool UpdateIsSameCode(string oldCode, string newCode)
        {
            int count = 0;
            using (pm2Entities context = new pm2Entities())
            {
                count = (
                    from m in context.PT_PrjInfo
                    where m.PrjCode != oldCode && m.PrjCode == newCode
                    select m).Count<PT_PrjInfo>();
            }
            return count > 0;
        }
        public static string GetTypeCode(string prjGuid)
        {
            string code = "00001";
            using (pm2Entities context = new pm2Entities())
            {
                if (string.IsNullOrEmpty(prjGuid))
                {
                    string maxCode = (
                        from m in context.PT_PrjInfo
                        where m.TypeCode.Length == 5
                        orderby m.TypeCode descending
                        select m.TypeCode).FirstOrDefault<string>();
                    if (!string.IsNullOrEmpty(maxCode))
                    {
                        code = (System.Convert.ToInt32(maxCode) + 1).ToString().PadLeft(5, '0');
                    }
                }
                else
                {
                    System.Guid guid = new System.Guid(prjGuid);
                    string parentCode = (
                        from n in context.PT_PrjInfo
                        where n.PrjGuid == (System.Guid?)guid
                        select n.TypeCode).FirstOrDefault<string>();
                    string lastChildCode = (
                        from m in context.PT_PrjInfo
                        where m.TypeCode.StartsWith(parentCode) && m.TypeCode.Length - 5 == parentCode.Length
                        orderby m.TypeCode descending
                        select m.TypeCode).FirstOrDefault<string>();
                    if (string.IsNullOrEmpty(lastChildCode))
                    {
                        code = parentCode + code;
                    }
                    else
                    {
                        string lastThree = lastChildCode.Substring(lastChildCode.Length - 5);
                        int lastThreeInt = System.Convert.ToInt32(lastThree) + 1;
                        code = parentCode + lastThreeInt.ToString().PadLeft(5, '0');
                    }
                }
            }
            return code;
        }
        public static ProjectInfo GetById(string id)
        {
            ProjectInfo project = null;
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid prjGuid = new System.Guid(id);
                project = (
                    from m in context.PT_PrjInfo
                    join d in context.PT_PrjInfo_ZTB_Detail on m.PrjGuid equals (System.Guid?)d.PrjGuid
                    where m.PrjGuid == (System.Guid?)prjGuid && d.PrjGuid == prjGuid
                    select new ProjectInfo
                    {
                        TypeCode = m.TypeCode,
                        PrjCode = m.PrjCode,
                        PrjName = m.PrjName,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        PrjCost = m.PrjCost,
                        Duration = m.Duration,
                        QualityClass = m.QualityClass,
                        Area = m.Area,
                        PrjKindClass = m.PrjKindClass,
                        PrjPlace = m.PrjPlace,
                        Remark = m.Remark1,
                        Rank = m.Rank,
                        BudgetWay = m.BudgetWay,
                        ContractWay = m.ContractWay,
                        PayCondition = m.PayCondition,
                        TenderWay = m.TenderWay,
                        PayWay = m.PayWay,
                        KeyPart = m.KeyPart,
                        WorkUnit = m.WorkUnit,
                        LinkMan = m.LinkMan,
                        PrjManager = m.PrjManager,
                        BuildingType = m.BuildingType,
                        TotalHouseNum = m.TotalHouseNum,
                        BuildingArea = m.BuildingArea,
                        UsegrounArea = m.UsegrounArea,
                        UndergroundArea = m.UndergroundArea,
                        PrjFundInfo = m.PrjFundInfo,
                        OtherStatement = m.OtherStatement,
                        Xmgroup = m.xmgroup,
                        PrjGrade = m.grade,
                        Businessman = m.businessman,
                        Telephone = d.Telephone,
                        ProjElseRequest = d.ProjElseRequest,
                        ProjInfoOrigin = d.ProjInfoOrigin,
                        PrjReadOne = d.PrjReadOne,
                        ProgAgent = d.ProgAgent,
                        EngineeringType = d.EngineeringType,
                        EngineeringSubType = d.EngineeringSubType,
                        Grade = d.Grade,
                        PrjInfo = m.PrjInfo,
                        PrjState = m.PrjState,
                        PrjGuid = m.PrjGuid ?? System.Guid.Empty,
                        Designer = m.Designer,
                        Counsellor = m.Counsellor,
                        Inspector = m.Inspector,
                        OwnerCode = m.OwnerCode,
                        OwnerLinkMan = d.OwnerLinkMan,
                        OwnerLinkPhone = d.OwnerLinkPhone,
                        OwnerAddress = d.OwnerAddress,
                        ProjPeopleUserName = d.ProjPeopleName,
                        ProjPeopleCorp = d.ProjPeopleDep,
                        ProjPeopleDuty = d.ProjPeopleDuty,
                        ProjPeopleTel = d.ProjPeopleTel,
                        ForecastProfitRate = d.ForecastProfitRate,
                        EngineeringEstimates = d.EngineeringEstimates,
                        PrjDutyPerson = d.PrjDutyPerson,
                        PrjApprovalOf = d.PrjApprovalOf,
                        PrjFundWorkable = d.PrjFundWorkable,
                        ManagementMargin = d.ManagementMargin,
                        MigrantQualityMarginRate = d.MigrantQualityMarginRate,
                        WithholdingTaxRate = d.WithholdingTaxRate,
                        PerformanceBond = d.PerformanceBond,
                        ElseMargin = d.ElseMargin,
                        IsTender = d.IsTender,
                        ActualRunDate = d.ActualRunDate,
                        PrjManagerRequire = d.PrjManagerRequire,
                        TechnicalLeaderRequire = d.TechnicalLeaderRequire,
                        PrjProperty = d.PrjProperty,
                        Province = d.Province,
                        City = d.City,
                        AfforestArea = d.AfforestArea,
                        ParkArea = d.ParkArea
                    }).FirstOrDefault<ProjectInfo>();
            }
            return project;
        }
        public static System.Collections.Generic.IList<string> GetChildPrjByTypeCode(string typeCode)
        {
            System.Collections.Generic.IList<string> typeCodeList = new System.Collections.Generic.List<string>();
            using (pm2Entities context = new pm2Entities())
            {
                typeCodeList = (
                    from m in context.PT_PrjInfo
                    where m.TypeCode.Length == 10 && m.TypeCode.Substring(0, 5) == typeCode
                    select m.TypeCode).ToList<string>();
            }
            return typeCodeList;
        }
        private void AddDefaultLimit(ProjectInfo mode)
        {
            System.Collections.Generic.List<string> codes = new System.Collections.Generic.List<string>();
            codes.Add("00000000");
            if (!string.IsNullOrEmpty(mode.UserCode))
            {
                codes.Add(mode.UserCode);
            }
            if (!string.IsNullOrEmpty(mode.PrjDutyPerson))
            {
                codes.Add(mode.PrjDutyPerson);
            }
            if (!string.IsNullOrEmpty(mode.ProgAgent))
            {
                codes.Add(mode.ProgAgent);
            }
            if (!string.IsNullOrEmpty(mode.Businessman))
            {
                codes.Add(mode.Businessman);
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
        private void UpdateUserPrimit(string oldCode, string newCode, System.Guid prjGuid)
        {
            if (!string.IsNullOrEmpty(newCode) && newCode != oldCode && newCode != "00000000" && !TenderUser.isExist(prjGuid, newCode))
            {
                TenderUser.Add(prjGuid.ToString(), newCode);
            }
        }
        private static void Del(string id, pm2Entities context)
        {
            System.Guid guid = new System.Guid(id);
            PT_PrjInfo prjInfo = (
                from m in context.PT_PrjInfo
                where m.PrjGuid == (System.Guid?)guid
                select m).FirstOrDefault<PT_PrjInfo>();
            if (prjInfo != null)
            {
                System.Collections.Generic.IList<string> listTypeCode = ProjectInfo.GetChildPrjByTypeCode(prjInfo.TypeCode);
                if (listTypeCode.Count > 0)
                {
                    throw new System.Exception("请先删除子项目！");
                }
                cn.justwin.Tender.EngineeringType.Delete(id, context);
                context.DeleteObject(prjInfo);
                string typeCode = prjInfo.TypeCode;
                if (typeCode.Length == 10)
                {
                    string parCode = typeCode.Substring(0, 5);
                    PT_PrjInfo parPrjInfo = (
                        from m in context.PT_PrjInfo
                        where m.TypeCode == parCode
                        select m).FirstOrDefault<PT_PrjInfo>();
                    if (parPrjInfo != null)
                    {
                        parPrjInfo.i_ChildNum--;
                    }
                }
                PT_PrjInfo_ZTB_Detail prjZTB_Detail = (
                    from m in context.PT_PrjInfo_ZTB_Detail
                    where m.PrjGuid == guid
                    select m).FirstOrDefault<PT_PrjInfo_ZTB_Detail>();
                if (prjZTB_Detail != null)
                {
                    context.DeleteObject(prjZTB_Detail);
                }
                PTPrjInfoZTBUserService PrjInfoUserSev = new PTPrjInfoZTBUserService();
                PrjInfoUserSev.Delete(guid.ToString());
            }
        }
        public static void Del(string id)
        {
            using (pm2Entities context = new pm2Entities())
            {
                ProjectInfo.Del(id, context);
            }
        }
        public static void Del(System.Collections.Generic.IList<string> idList)
        {
            using (pm2Entities context = new pm2Entities())
            {
                if (idList.Count == 1)
                {
                    ProjectInfo.Del(idList[0], context);
                }
                else
                {
                    ProjectInfo.Del(idList, context);
                }
                context.SaveChanges();
            }
        }
        private static void Del(System.Collections.Generic.IList<string> idlist, pm2Entities context)
        {
            foreach (string id in idlist)
            {
                ProjectInfo.Del(id, context);
            }
        }
        public static DataTable GetTenderPrjReport(string prjCode, string prjName, string prjManager, string prjKindClass, string startDate, string endDate, string owner, string prjState, string userCode, int pageNo, int pageSize, string IsTender, ref int refRowCount)
        {
            DataTable dt = new DataTable();
            string columns = "TypeCode,Primit,i_ChildNum,PrjGuid,PrjCode,PrjName,InputDate,StartDate,EndDate,PrjCost,Duration,Owner,PrjMangerName,PrjStateName,PrjKindName,IsTender\r\n,CASE IsTender \r\n\tWHEN 1 THEN 1\r\n\tELSE SetUpFlowState\r\nEND AS SetUpFlowState";
            SqlParameter rowCount = new SqlParameter("@rowCount", SqlDbType.Int);
            rowCount.Direction = ParameterDirection.Output;
            System.Collections.Generic.List<SqlParameter> paras = new System.Collections.Generic.List<SqlParameter>();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                strSql.AppendFormat(" AND PrjCode like  '%{0}%' ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                strSql.AppendFormat(" AND PrjName like  '%{0}%' ", prjName);
            }
            if (!string.IsNullOrEmpty(prjManager))
            {
                strSql.AppendFormat(" AND PrjMangerName like  '%{0}%' ", prjManager);
            }
            if (!string.IsNullOrEmpty(prjKindClass))
            {
                strSql.AppendFormat(" AND  PrjKindClass='{0}'", prjKindClass);
            }
            if (!string.IsNullOrEmpty(owner))
            {
                strSql.AppendFormat(" AND Owner like  '%{0}%' ", owner);
            }
            if (!string.IsNullOrEmpty(prjState))
            {
                strSql.AppendFormat(" AND PrjState={0}", prjState);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                strSql.AppendFormat(" AND InputDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                strSql.AppendFormat(" AND InputDate <= '{0}' ", endDate + " 23:59:59");
            }
            paras.Add(new SqlParameter("@userCode", userCode));
            paras.Add(new SqlParameter("@isTender", IsTender));
            paras.Add(new SqlParameter("@columns", columns));
            paras.Add(new SqlParameter("@condition", strSql.ToString()));
            paras.Add(new SqlParameter("@pageIndex", pageNo));
            paras.Add(new SqlParameter("@pageSize", pageSize));
            paras.Add(rowCount);
            dt = SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "uspGetProject", paras.ToArray());
            refRowCount = System.Convert.ToInt32(rowCount.Value);
            return dt;
        }
        public static DataTable GetAllTenderPrjReport(string prjCode, string prjName, string prjManager, string prjKindClass, string startDate, string endDate, string owner, string prjState, string userCode, int pageNo, int pageSize, string IsTender, ref int refRowCount)
        {
            DataTable dt = new DataTable();
            string columns = "TypeCode,Primit,i_ChildNum,PrjGuid,PrjCode,InputDate,PrjName,StartDate,EndDate,PrjCost,Duration,Owner,PrjMangerName,PrjStateName,PrjKindName,IsTender\r\n,CASE IsTender \r\n\tWHEN 1 THEN 1\r\n\tELSE SetUpFlowState\r\nEND AS SetUpFlowState";
            System.Collections.Generic.List<SqlParameter> paras = new System.Collections.Generic.List<SqlParameter>();
            SqlParameter rowCount = new SqlParameter("@rowCount", SqlDbType.Int);
            rowCount.Direction = ParameterDirection.Output;
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                strSql.AppendFormat(" AND PrjCode like  '%{0}%' ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                strSql.AppendFormat(" AND PrjName like  '%{0}%' ", prjName);
            }
            if (!string.IsNullOrEmpty(prjManager))
            {
                strSql.AppendFormat(" AND PrjMangerName like  '%{0}%' ", prjManager);
            }
            if (!string.IsNullOrEmpty(prjKindClass))
            {
                strSql.AppendFormat(" AND  PrjKindClass='{0}'", prjKindClass);
            }
            if (!string.IsNullOrEmpty(owner))
            {
                strSql.AppendFormat(" AND Owner like  '%{0}%' ", owner);
            }
            if (!string.IsNullOrEmpty(prjState))
            {
                strSql.AppendFormat(" AND PrjState={0}", prjState);
            }
            else
            {
                strSql.AppendFormat(" AND PrjState in(5,7,8,9,10,11,12,13,17) ", new object[0]);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                strSql.AppendFormat(" AND InputDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                strSql.AppendFormat(" AND InputDate <= '{0}' ", endDate + " 23:59:59");
            }
            paras.Add(new SqlParameter("@userCode", userCode));
            paras.Add(new SqlParameter("@isTender", IsTender));
            paras.Add(new SqlParameter("@columns", columns));
            paras.Add(new SqlParameter("@condition", strSql.ToString()));
            paras.Add(new SqlParameter("@pageIndex", pageNo));
            paras.Add(new SqlParameter("@pageSize", pageSize));
            paras.Add(rowCount);
            dt = SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "uspGetAllProject", paras.ToArray());
            refRowCount = System.Convert.ToInt32(rowCount.Value);
            return dt;
        }
        public static string GetProjectName(string prjId)
        {
            string prjName = string.Empty;
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid guid = new System.Guid(prjId);
                prjName = (
                    from m in context.PT_PrjInfo
                    where m.PrjGuid == (System.Guid?)guid
                    select m.PrjName).FirstOrDefault<string>();
            }
            return prjName;
        }
        public static DataTable GetProjectInfo(string userCode)
        {
            string sql = "\r\n\t\t\t\t--DECLARE @UserCode nvarchar(8)\r\n\t\t\t\t--SET @UserCode = '00000000';\r\n\t\t\t\tWITH cte_PrjInfo AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT prjInfo.PrjGuid,PrjCode,PrjName,ISNULL(City,'') City,PrjPlace,ISNULL(itemName,'') PrjState \r\n\t\t\t\t\tFROM PT_PrjInfo AS prjInfo\r\n\t\t\t\t\tINNER JOIN PT_PrjInfo_ZTB_User AS prjUser ON prjInfo.PrjGuid=prjUser.PrjGuid\r\n\t\t\t\t\tLEFT JOIN (\r\n\t\t\t\t\t\tSELECT ItemCode,ItemName FROM Basic_CodeList WHERE TypeCode='ProjectState') stateList\r\n\t\t\t\t\t\tON prjInfo.PrjState=stateList.ItemCode\r\n\t\t\t\t\tLEFT JOIN PT_PrjInfo_ZTB_Detail AS detail ON prjInfo.PrjGuid=detail.PrjGuid\r\n\t\t\t\t\tWHERE IsValid=1 AND prjUser.UserCode=@UserCode\r\n\t\t\t\t)\r\n\t\t\t\tSELECT * FROM cte_PrjInfo\r\n\t\t\t\tORDER BY PrjCode\r\n\t\t\t";
            SqlParameter[] paras = new SqlParameter[]
			{
				new SqlParameter("@UserCode", userCode)
			};
            return SqlHelper.ExecuteQuery(CommandType.Text, sql, paras);
        }
        public static int GetPrjCountByCondition(string prjName, string prjCode, string prjManager, string prjPlace, string prjDutyPerson, string startDate, string endDate, string userCode)
        {
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            System.Text.StringBuilder strWhere = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjName))
            {
                strWhere.Append("\r\n\t\t\tAND PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName));
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                strWhere.Append("\r\n\t\t\tAND PrjCode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode));
            }
            if (!string.IsNullOrEmpty(prjManager))
            {
                strWhere.AppendFormat(" \r\n\t\t\tAND PrjMangerName LIKE '%'+@prjManager+'%' ", new object[0]);
                parameters.Add(new SqlParameter("@prjManager", prjManager));
            }
            if (!string.IsNullOrEmpty(prjDutyPerson))
            {
                strWhere.AppendFormat(" \r\n\t\t\tAND prjDutyName LIKE '%'+@prjDutyPerson+'%' ", new object[0]);
                parameters.Add(new SqlParameter("@prjDutyPerson", prjDutyPerson));
            }
            if (!string.IsNullOrEmpty(prjPlace))
            {
                strWhere.AppendFormat(" \r\n\t\t\tAND prjPlace LIKE '%'+@prjPlace+'%' ", prjPlace);
                parameters.Add(new SqlParameter("@prjPlace", prjPlace));
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                strWhere.Append("\r\n\t\t\tAND FirstContract.SignedTime >= @startDate ");
                parameters.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                strWhere.Append("\r\n\t\t\tAND FirstContract.SignedTime <= @endDate ");
                parameters.Add(new SqlParameter("@endDate", endDate + " 23:59:59"));
            }
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.AppendLine("WITH IncometCon AS      --收入合同的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(ContractPrice) ContractPrice,Project FROM Con_Incomet_Contract ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine(" GROUP BY Project ), IncometModify AS   --收入合同变更的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(ChangePrice) ChangePrice,Project FROM Con_Incomet_Modify AS Modify ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Modify.ContractId=Incomet.ContractId ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("), BudRes AS          --目标成本的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(ResourceQuantity*ResourcePrice) ResTotal,PrjId FROM Bud_TaskResource AS TaskRes INNER JOIN  ");
            strSql.AppendLine("Bud_Task Task ON TaskRes.TaskId=Task.TaskId  GROUP BY PrjId ");
            strSql.AppendLine("),BudIndirect AS      --预算间接成本的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT ProjectId,SUM(AccountAmount) BudIndirectAmount FROM Bud_IndirectBudget WHERE State=2 GROUP BY ProjectId ");
            strSql.AppendLine("), ConsRes AS         --施工报量的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(UnitPrice*ISNULL(Bud_ConsTaskRes.AccountingQuantity,0)) ConsResTotal,PrjId FROM Bud_ConsTaskRes  ");
            strSql.AppendLine("INNER JOIN Bud_ConsTask ON Bud_ConsTaskRes.ConsTaskId=Bud_ConsTask.ConsTaskId ");
            strSql.AppendLine("INNER JOIN Bud_ConsReport ON Bud_ConsTask.ConsReportId=Bud_ConsReport.ConsReportId  ");
            strSql.AppendLine("WHERE IsValid=1 AND FlowState=1 GROUP BY PrjId ");
            strSql.AppendLine("),IndirectDiary AS    --间接成本日记账的金融之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(Amount) IndirectTotal,ProjectId FROM Bud_IndirectDiaryDetails DiaryDetails  ");
            strSql.AppendLine("INNER JOIN Bud_IndirectDiaryCost DiaryCost ON DiaryDetails.InDiaryId=DiaryCost.InDiaryId ");
            strSql.AppendLine("WHERE FlowState=1 GROUP BY ProjectId ");
            strSql.AppendLine("),FirstContractId AS  -- 第一个合同Id ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT MIN(RegisterTime) MinRegisterTime,Project,MAX(ContractId) ContractId FROM Con_Incomet_Contract  ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("),FirstContract AS    --第一个合同 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT IncometContract.* FROM Con_Incomet_Contract IncometContract INNER JOIN FirstContractId  ");
            strSql.AppendLine("ON FirstContractId.ContractId=IncometContract.ContractId ");
            strSql.AppendLine("),PayMode AS          --付款方式列表 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT NoteId,CodeName FROM XPM_Basic_CodeList CodeList INNER JOIN XPM_Basic_CodeType CodeType ");
            strSql.AppendLine("ON CodeList.TypeId=CodeType.TypeId WHERE SignCode='payment' AND CodeList.IsValid=1 ");
            strSql.AppendLine("),IncometBalance AS   --所有结算的汇总 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(ClearingPrice) ClearingPrice,Project FROM Con_Incomet_Balance AS Balance ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Balance.ContractId=Incomet.ContractId ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("), IncometPaymentCurrent AS --本月收入合同收款的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(CllectionPrice) CllectionPrice,Project FROM Con_Incomet_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Payment.ContractId=Incomet.ContractId ");
            strSql.AppendLine("WHERE DATEDIFF(MONTH,CllectionTime,GETDATE())=0 ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" AND FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("),IncometPayment AS        --收入合同收款的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(CllectionPrice) CllectionPrice,Project FROM Con_Incomet_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Payment.ContractId=Incomet.ContractId ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project),PayoutPaymentCurrent AS  --本月支出合同支出的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(PaymentMoney) PaymentMoney,PrjGuid FROM Con_Payout_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Payout_Contract AS Payout ON Payment.ContractId=Payout.ContractId ");
            strSql.AppendLine("WHERE  Payment.FlowState=1 AND Payout.FlowState=1 ");
            strSql.AppendLine("GROUP BY PrjGuid ");
            strSql.AppendLine("),PayoutPayment AS         --支出合同支出的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(PaymentMoney) PaymentMoney,PrjGuid FROM Con_Payout_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Payout_Contract AS Payout ON Payment.ContractId=Payout.ContractId ");
            strSql.AppendLine("WHERE  Payment.FlowState=1 AND Payout.FlowState=1 AND DATEDIFF(DAY,PaymentDate,GETDATE())>=0 ");
            strSql.AppendLine("GROUP BY PrjGuid ");
            strSql.AppendLine("),ctePrj AS          --全部项目的信息 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT vProject.*,FirstContract.SignedTime,FirstContract.StartDate ContractStartDate,FirstContract.EndDate Contractenddate, ");
            strSql.AppendLine("FirstContract.QualityPeriod,PayMode.CodeName FROM vProject LEFT JOIN FirstContract ON vProject.PrjGuid=FirstContract.Project ");
            strSql.AppendLine("LEFT JOIN PayMode ON FirstContract.PayMode=PayMode.NoteId  ");
            strSql.AppendLine("),FilterPrj AS      --通过过滤条件的项目 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT vProject.TypeCode, vProject.PrjGuid, vProject.PrjName FROM vProject LEFT JOIN FirstContract ON vProject.PrjGuid=FirstContract.Project ");
            strSql.AppendLine("LEFT JOIN PayMode ON FirstContract.PayMode=PayMode.NoteId  ");
            strSql.AppendLine("INNER JOIN PT_PrjInfo_ZTB_User ZTBUser ON vProject.PrjGuid=ZTBUser.PrjGuid ");
            strSql.AppendFormat("WHERE ZTBUser.Usercode='{0}' ", userCode).AppendLine();
            strSql.AppendLine(strWhere.ToString());
            strSql.AppendLine("),mergeParent AS --查询出符合条件的项目及其主项目 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT vProject.TypeCode,vProject.PrjGuid,vProject.PrjName FROM FilterPrj INNER JOIN vProject ON SUBSTRING(FilterPrj.TypeCode, 1, 5)=  ");
            strSql.AppendLine("SUBSTRING(vProject.TypeCode, 1, 5) WHERE len(vProject.TypeCode) = 5 ");
            strSql.AppendLine("UNION ");
            strSql.AppendLine("SELECT * FROM FilterPrj ");
            strSql.AppendLine(") ");
            strSql.AppendLine("SELECT COUNT(*) FROM ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT *,CASE Tab.i_ChildNum ");
            strSql.AppendLine("WHEN '0' THEN ( ");
            strSql.AppendLine("CASE LEN(Tab.TypeCode) ");
            strSql.AppendLine("WHEN '5' THEN Tab.InputDate  ");
            strSql.AppendLine("ELSE (SELECT InputDate FROM PT_PrjInfo_ZTB_Detail WHERE PT_PrjInfo_ZTB_Detail.PrjGuid =(SELECT PT1.PrjGuid FROM PT_PrjInfo AS PT1 WHERE PT1.TypeCode = LEFT(Tab.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')) ");
            strSql.AppendLine("END ");
            strSql.AppendLine(") ");
            strSql.AppendLine("ELSE Tab.InputDate ");
            strSql.AppendLine("END AS UserDefined_Date  ");
            strSql.AppendLine("FROM  ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT ctePrj.TypeCode,i_ChildNum,ctePrj.PrjGuid,ctePrj.PrjCode,ctePrj.PrjName,PrjPlace,OwnerCode, ");
            strSql.AppendLine("ISNULL(CorpName,'') CorpName,ISNULL(PrjMangerName,'') PrjMangerName,ctePrj.InputDate,ISNULL(ctePrj.telephone,'') Telephone, ");
            strSql.AppendLine("ISNULL(ProjPeopleName,'') ProjPeopleName,ISNULL(Detail.ProjPeopleDep,'') ProjPeopleDep,ISNULL(PrjDutyName,'') PrjDutyName, ");
            strSql.AppendLine("PrjFundInfo,ISNULL(IncometCon.ContractPrice,0) ContractPrice,ISNULL(ChangePrice,0) ChangePrice, ");
            strSql.AppendLine("ISNULL(BudRes.ResTotal,0) BudResTotal,ISNULL(BudIndirect.BudIndirectAmount,0) BudIndirectAmount, ");
            strSql.AppendLine("ISNULL(ConsRes.ConsResTotal,0) ConsResTotal,ISNULL(IndirectDiary.IndirectTotal,0) IndirectTotal,ctePrj.ActualRunDate, ");
            strSql.AppendLine("ctePrj.CompletedDate,SignedTime,StartDate,EndDate,ISNULL(QualityPeriod,'') QualityPeriod,ISNULL(CodeName,'') PayMode, ");
            strSql.AppendLine("ISNULL(IncometBalance.ClearingPrice,0) ClearingPrice,ISNULL(IncometPaymentCurrent.CllectionPrice,0) CurrentCllectionPrice, ");
            strSql.AppendLine("ISNULL(IncometPayment.CllectionPrice,0) CllectionPrice,ISNULL(PayoutPaymentCurrent.PaymentMoney,0) CurrentPaymentMoney, ");
            strSql.AppendLine("ISNULL(PayoutPayment.PaymentMoney,0) PaymentMoney,ISNULL(ManagementMargin,0) ManagementMargin, ");
            strSql.AppendLine("ISNULL(MigrantQualityMarginRate,0) MigrantQualityMarginRate,ISNULL(WithholdingTaxRate,0) WithholdingTaxRate, ");
            strSql.AppendLine("ISNULL(PerformanceBond,0) PerformanceBond,ISNULL(ElseMargin,0) ElseMargin FROM mergeParent INNER JOIN ctePrj  ");
            strSql.AppendLine("ON mergeParent.PrjGuid=ctePrj.PrjGuid  ");
            strSql.AppendLine("LEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON ctePrj.PrjGuid=Detail.PrjGuid ");
            strSql.AppendLine("LEFT JOIN XPM_Basic_ContactCorp ON ctePrj.OwnerCode=CorpId  ");
            strSql.AppendLine("LEFT JOIN IncometCon ON ctePrj.PrjGuid=IncometCon.Project  ");
            strSql.AppendLine("LEFT JOIN IncometModify ON ctePrj.PrjGuid=IncometModify.Project  ");
            strSql.AppendLine("LEFT JOIN BudRes ON ctePrj.PrjGuid=BudRes.PrjId  ");
            strSql.AppendLine("LEFT JOIN BudIndirect ON Convert(NVARCHAR(50),ctePrj.PrjGuid)=BudIndirect.ProjectId  ");
            strSql.AppendLine("LEFT JOIN ConsRes ON ctePrj.PrjGuid=ConsRes.PrjId  ");
            strSql.AppendLine("LEFT JOIN IndirectDiary ON ctePrj.PrjGuid=IndirectDiary.ProjectId  ");
            strSql.AppendLine("LEFT JOIN IncometBalance ON ctePrj.PrjGuid=IncometBalance.Project  ");
            strSql.AppendLine("LEFT JOIN IncometPaymentCurrent ON ctePrj.PrjGuid=IncometPaymentCurrent.Project  ");
            strSql.AppendLine("LEFT JOIN IncometPayment ON ctePrj.PrjGuid=IncometPayment.Project  ");
            strSql.AppendLine("LEFT JOIN PayoutPaymentCurrent ON ctePrj.PrjGuid=PayoutPaymentCurrent.PrjGuid  ");
            strSql.AppendLine("LEFT JOIN PayoutPayment ON ctePrj.PrjGuid=PayoutPayment.PrjGuid  WHERE Detail.SetUpFlowState=1");
            strSql.AppendLine(") Tab ) Tab1 ");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters.ToArray()));
        }
        public static DataTable GetPrjByCondition(string prjName, string prjCode, string prjManager, string prjPlace, string prjDutyPerson, string startDate, string endDate, string userCode, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = ProjectInfo.GetPrjCountByCondition(prjName, prjCode, prjManager, prjPlace, prjDutyPerson, startDate, endDate, userCode);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
            System.Text.StringBuilder strWhere = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjName))
            {
                strWhere.Append("\r\n\t\t\tAND PrjName LIKE '%'+@prjName+'%' ");
                parameters.Add(new SqlParameter("@prjName", prjName));
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                strWhere.Append("\r\n\t\t\tAND PrjCode LIKE '%'+@prjCode+'%' ");
                parameters.Add(new SqlParameter("@prjCode", prjCode));
            }
            if (!string.IsNullOrEmpty(prjManager))
            {
                strWhere.AppendFormat(" \r\n\t\t\tAND PrjMangerName LIKE '%'+@prjManager+'%' ", new object[0]);
                parameters.Add(new SqlParameter("@prjManager", prjManager));
            }
            if (!string.IsNullOrEmpty(prjDutyPerson))
            {
                strWhere.AppendFormat(" \r\n\t\t\tAND prjDutyName LIKE '%'+@prjDutyPerson+'%' ", new object[0]);
                parameters.Add(new SqlParameter("@prjDutyPerson", prjDutyPerson));
            }
            if (!string.IsNullOrEmpty(prjPlace))
            {
                strWhere.AppendFormat(" \r\n\t\t\tAND prjPlace LIKE '%'+@prjPlace+'%' ", prjPlace);
                parameters.Add(new SqlParameter("@prjPlace", prjPlace));
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                strWhere.Append("\r\n\t\t\tAND FirstContract.SignedTime >= @startDate ");
                parameters.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                strWhere.Append("\r\n\t\t\tAND FirstContract.SignedTime <= @endDate ");
                parameters.Add(new SqlParameter("@endDate", endDate + " 23:59:59"));
            }
            DataTable dtProject = new DataTable();
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.AppendLine("WITH IncometCon AS      --收入合同的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(ContractPrice) ContractPrice,Project FROM Con_Incomet_Contract ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ), IncometModify AS   --收入合同变更的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(ChangePrice) ChangePrice,Project FROM Con_Incomet_Modify AS Modify ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Modify.ContractId=Incomet.ContractId ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("), BudRes AS          --目标成本的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT ResTotal,BudVersion.PrjId FROM ( ");
            strSql.AppendLine("SELECT SUM(ResourceQuantity*ResourcePrice) ResTotal,PrjId,Version FROM Bud_TaskResource AS TaskRes INNER JOIN   ");
            strSql.AppendLine("Bud_Task Task ON TaskRes.TaskId=Task.TaskId  GROUP BY PrjId,Version) BudTotalVersion INNER JOIN vGetCurBudVersion BudVersion ");
            strSql.AppendLine("ON BudTotalVersion.PrjId=BudVersion.PrjId AND Version=CurVersion ");
            strSql.AppendLine("),BudIndirect AS      --预算间接成本的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT ProjectId,SUM(AccountAmount) BudIndirectAmount FROM Bud_IndirectBudget WHERE State=2 GROUP BY ProjectId ");
            strSql.AppendLine("), ConsRes AS         --施工报量的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(UnitPrice*ISNULL(Bud_ConsTaskRes.AccountingQuantity,0)) ConsResTotal,PrjId FROM Bud_ConsTaskRes  ");
            strSql.AppendLine("INNER JOIN Bud_ConsTask ON Bud_ConsTaskRes.ConsTaskId=Bud_ConsTask.ConsTaskId ");
            strSql.AppendLine("INNER JOIN Bud_ConsReport ON Bud_ConsTask.ConsReportId=Bud_ConsReport.ConsReportId  ");
            strSql.AppendLine("WHERE IsValid=1 AND FlowState=1 GROUP BY PrjId ");
            strSql.AppendLine("),IndirectDiary AS    --间接成本日记账的金融之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(Amount) IndirectTotal,ProjectId FROM Bud_IndirectDiaryDetails DiaryDetails  ");
            strSql.AppendLine("INNER JOIN Bud_IndirectDiaryCost DiaryCost ON DiaryDetails.InDiaryId=DiaryCost.InDiaryId ");
            strSql.AppendLine("WHERE FlowState=1 GROUP BY ProjectId ");
            strSql.AppendLine("),FirstContractId AS  -- 第一个合同Id ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT MIN(RegisterTime) MinRegisterTime,Project,MAX(ContractId) ContractId FROM Con_Incomet_Contract  ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("),FirstContract AS    --第一个合同 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT IncometContract.* FROM Con_Incomet_Contract IncometContract INNER JOIN FirstContractId  ");
            strSql.AppendLine("ON FirstContractId.ContractId=IncometContract.ContractId ");
            strSql.AppendLine("),PayMode AS          --付款方式列表 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT NoteId,CodeName FROM XPM_Basic_CodeList CodeList INNER JOIN XPM_Basic_CodeType CodeType ");
            strSql.AppendLine("ON CodeList.TypeId=CodeType.TypeId WHERE SignCode='payment' AND CodeList.IsValid=1 ");
            strSql.AppendLine("),IncometBalance AS   --所有结算的汇总 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(ClearingPrice) ClearingPrice,Project FROM Con_Incomet_Balance AS Balance ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Balance.ContractId=Incomet.ContractId ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("), IncometPaymentCurrent AS --本月收入合同收款的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(CllectionPrice) CllectionPrice,Project FROM Con_Incomet_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Payment.ContractId=Incomet.ContractId ");
            strSql.AppendLine("WHERE DATEDIFF(MONTH,CllectionTime,GETDATE())=0 ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" AND FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ");
            strSql.AppendLine("),IncometPayment AS        --收入合同收款的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(CllectionPrice) CllectionPrice,Project FROM Con_Incomet_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Incomet_Contract AS Incomet ON Payment.ContractId=Incomet.ContractId ");
            if (ProjectInfo.IsIncomeContractApprove == "1")
            {
                strSql.AppendLine(" WHERE FlowState=1 ");
            }
            strSql.AppendLine("GROUP BY Project ),PayoutPaymentCurrent AS  --本月支出合同支出的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(PaymentMoney) PaymentMoney,PrjGuid FROM Con_Payout_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Payout_Contract AS Payout ON Payment.ContractId=Payout.ContractId ");
            strSql.AppendLine("WHERE  Payment.FlowState=1 AND Payout.FlowState=1 AND DATEDIFF(MONTH,PaymentDate,GETDATE())=0 ");
            strSql.AppendLine("GROUP BY PrjGuid ");
            strSql.AppendLine("),PayoutPayment AS         --支出合同支出的金额之和 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT SUM(PaymentMoney) PaymentMoney,PrjGuid FROM Con_Payout_Payment AS Payment ");
            strSql.AppendLine("INNER JOIN Con_Payout_Contract AS Payout ON Payment.ContractId=Payout.ContractId ");
            strSql.AppendLine("WHERE  Payment.FlowState=1 AND Payout.FlowState=1 ");
            strSql.AppendLine("GROUP BY PrjGuid ");
            strSql.AppendLine("),ctePrj AS          --全部项目的信息 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT vProject.*,FirstContract.SignedTime,FirstContract.StartDate ContractStartDate,FirstContract.EndDate Contractenddate, ");
            strSql.AppendLine("FirstContract.QualityPeriod,PayMode.CodeName FROM vProject LEFT JOIN FirstContract ON vProject.PrjGuid=FirstContract.Project ");
            strSql.AppendLine("LEFT JOIN PayMode ON FirstContract.PayMode=PayMode.NoteId  ");
            strSql.AppendLine("),FilterPrj AS      --通过过滤条件的项目 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT vProject.TypeCode, vProject.PrjGuid, vProject.PrjName FROM vProject LEFT JOIN FirstContract ON vProject.PrjGuid=FirstContract.Project ");
            strSql.AppendLine("LEFT JOIN PayMode ON FirstContract.PayMode=PayMode.NoteId  ");
            strSql.AppendLine("INNER JOIN PT_PrjInfo_ZTB_User ZTBUser ON vProject.PrjGuid=ZTBUser.PrjGuid ");
            strSql.AppendFormat("WHERE ZTBUser.Usercode='{0}' ", userCode).AppendLine();
            strSql.AppendLine(strWhere.ToString());
            strSql.AppendLine("),mergeParent AS --查询出符合条件的项目及其主项目 ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT vProject.TypeCode,vProject.PrjGuid,vProject.PrjName FROM FilterPrj INNER JOIN vProject ON SUBSTRING(FilterPrj.TypeCode, 1, 5)=  ");
            strSql.AppendLine("SUBSTRING(vProject.TypeCode, 1, 5) WHERE len(vProject.TypeCode) = 5 ");
            strSql.AppendLine("UNION ");
            strSql.AppendLine("SELECT * FROM FilterPrj ");
            strSql.AppendLine(") ");
            strSql.AppendFormat("SELECT TOP({0}) Convert(Nvarchar(30),Num) as Num,TypeCode,PrjGuid,PrjName,PrjCode, PrjType,ProjPeopleDep, ", pageSize).AppendLine();
            strSql.AppendLine("PrjDutyName,PrjMangerName,Telephone,CorpName,PrjPlace,PrjFundInfo, ContractPrice,BudTotal, ConsTotal, ");
            strSql.AppendLine("SignedTime,StartDate,EndDate,ActualRunDate,CompletedDate,QualityPeriod, PayMode,Convert(decimal(18,3),ConsResTotal) ConsResTotal,ClearingPrice, ");
            strSql.AppendLine("SurplusClearingPrice,CurrentCllectionPrice,CllectionPrice,CurrentPaymentMoney,PaymentMoney,ManagementMargin, ");
            strSql.AppendLine("MigrantQualityMarginRate,WithholdingTaxRate,PerformanceBond,ElseMargin,UserDefined_Date  FROM ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT Row_Number()OVER(ORDER BY UserDefined_Date DESC,TypeCode) AS Num,TypeCode,PrjGuid,PrjName,PrjCode, ");
            strSql.AppendLine("'' PrjType,ProjPeopleDep,PrjDutyName,PrjMangerName,Telephone,CorpName,PrjPlace,PrjFundInfo, ");
            strSql.AppendLine("(ContractPrice+ChangePrice) ContractPrice,(BudResTotal+BudIndirectAmount) BudTotal, ");
            strSql.AppendLine("(ConsResTotal+IndirectTotal) ConsTotal,SignedTime,StartDate,EndDate,ActualRunDate,CompletedDate,QualityPeriod, ");
            strSql.AppendLine("PayMode,ConsResTotal,ClearingPrice,(ClearingPrice-CllectionPrice) SurplusClearingPrice,CurrentCllectionPrice, ");
            strSql.AppendLine("CllectionPrice,CurrentPaymentMoney,PaymentMoney,ManagementMargin,MigrantQualityMarginRate,WithholdingTaxRate, ");
            strSql.AppendLine("PerformanceBond,ElseMargin,UserDefined_Date  FROM ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT *,CASE Tab.i_ChildNum ");
            strSql.AppendLine("WHEN '0' THEN ( ");
            strSql.AppendLine("CASE LEN(Tab.TypeCode) ");
            strSql.AppendLine("WHEN '5' THEN Tab.InputDate  ");
            strSql.AppendLine("ELSE (SELECT InputDate FROM PT_PrjInfo_ZTB_Detail WHERE PT_PrjInfo_ZTB_Detail.PrjGuid =(SELECT PT1.PrjGuid FROM PT_PrjInfo AS PT1 WHERE PT1.TypeCode = LEFT(Tab.TypeCode,5) AND i_ChildNum > 0 AND IsValid = '1')) ");
            strSql.AppendLine("END ");
            strSql.AppendLine(") ");
            strSql.AppendLine("ELSE Tab.InputDate ");
            strSql.AppendLine("END AS UserDefined_Date  ");
            strSql.AppendLine("FROM  ");
            strSql.AppendLine("( ");
            strSql.AppendLine("SELECT ctePrj.TypeCode,i_ChildNum,ctePrj.PrjGuid,ctePrj.PrjCode,ctePrj.PrjName,PrjPlace,OwnerCode, ");
            strSql.AppendLine("ISNULL(CorpName,'') CorpName,ISNULL(PrjMangerName,'') PrjMangerName,ctePrj.InputDate,ISNULL(ctePrj.telephone,'') Telephone, ");
            strSql.AppendLine("ISNULL(ProjPeopleName,'') ProjPeopleName,ISNULL(Detail.ProjPeopleDep,'') ProjPeopleDep,ISNULL(PrjDutyName,'') PrjDutyName, ");
            strSql.AppendLine("PrjFundInfo,ISNULL(IncometCon.ContractPrice,0) ContractPrice,ISNULL(ChangePrice,0) ChangePrice, ");
            strSql.AppendLine("ISNULL(BudRes.ResTotal,0) BudResTotal,ISNULL(BudIndirect.BudIndirectAmount,0) BudIndirectAmount, ");
            strSql.AppendLine("ISNULL(ConsRes.ConsResTotal,0) ConsResTotal,ISNULL(IndirectDiary.IndirectTotal,0) IndirectTotal,ctePrj.ActualRunDate, ");
            strSql.AppendLine("ctePrj.CompletedDate,SignedTime,StartDate,EndDate,ISNULL(QualityPeriod,'') QualityPeriod,ISNULL(CodeName,'') PayMode, ");
            strSql.AppendLine("ISNULL(IncometBalance.ClearingPrice,0) ClearingPrice,ISNULL(IncometPaymentCurrent.CllectionPrice,0) CurrentCllectionPrice, ");
            strSql.AppendLine("ISNULL(IncometPayment.CllectionPrice,0) CllectionPrice,ISNULL(PayoutPaymentCurrent.PaymentMoney,0) CurrentPaymentMoney, ");
            strSql.AppendLine("ISNULL(PayoutPayment.PaymentMoney,0) PaymentMoney,ISNULL(ManagementMargin,0) ManagementMargin, ");
            strSql.AppendLine("ISNULL(MigrantQualityMarginRate,0) MigrantQualityMarginRate,ISNULL(WithholdingTaxRate,0) WithholdingTaxRate, ");
            strSql.AppendLine("ISNULL(PerformanceBond,0) PerformanceBond,ISNULL(ElseMargin,0) ElseMargin FROM mergeParent INNER JOIN ctePrj  ");
            strSql.AppendLine("ON mergeParent.PrjGuid=ctePrj.PrjGuid  ");
            strSql.AppendLine("LEFT JOIN PT_PrjInfo_ZTB_Detail AS Detail ON ctePrj.PrjGuid=Detail.PrjGuid ");
            strSql.AppendLine("LEFT JOIN XPM_Basic_ContactCorp ON ctePrj.OwnerCode=CorpId  ");
            strSql.AppendLine("LEFT JOIN IncometCon ON ctePrj.PrjGuid=IncometCon.Project  ");
            strSql.AppendLine("LEFT JOIN IncometModify ON ctePrj.PrjGuid=IncometModify.Project  ");
            strSql.AppendLine("LEFT JOIN BudRes ON ctePrj.PrjGuid=BudRes.PrjId  ");
            strSql.AppendLine("LEFT JOIN BudIndirect ON Convert(NVARCHAR(50),ctePrj.PrjGuid)=BudIndirect.ProjectId  ");
            strSql.AppendLine("LEFT JOIN ConsRes ON ctePrj.PrjGuid=ConsRes.PrjId  ");
            strSql.AppendLine("LEFT JOIN IndirectDiary ON ctePrj.PrjGuid=IndirectDiary.ProjectId  ");
            strSql.AppendLine("LEFT JOIN IncometBalance ON ctePrj.PrjGuid=IncometBalance.Project  ");
            strSql.AppendLine("LEFT JOIN IncometPaymentCurrent ON ctePrj.PrjGuid=IncometPaymentCurrent.Project  ");
            strSql.AppendLine("LEFT JOIN IncometPayment ON ctePrj.PrjGuid=IncometPayment.Project  ");
            strSql.AppendLine("LEFT JOIN PayoutPaymentCurrent ON ctePrj.PrjGuid=PayoutPaymentCurrent.PrjGuid  ");
            strSql.AppendLine("LEFT JOIN PayoutPayment ON ctePrj.PrjGuid=PayoutPayment.PrjGuid  WHERE Detail.SetUpFlowState=1");
            strSql.AppendFormat(") Tab ) Tab1 ) Tab2 WHERE Num> {0}*({1}-1) ", pageSize, pageIndex).AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), parameters.ToArray());
        }
        public static DataTable GetProjectDate(string prjId)
        {
            DataTable dtDate = new DataTable();
            string sql = "SELECT StartDate,EndDate FROM PT_PrjInfo WHERE PrjGuid=@PrjId";
            SqlParameter param = new SqlParameter("@PrjId", prjId);
            return SqlHelper.ExecuteQuery(CommandType.Text, sql, new SqlParameter[]
			{
				param
			});
        }
        public static void UpdateProjectDate(string prjId, System.DateTime? startDate, System.DateTime? endDate)
        {
            using (pm2Entities context = new pm2Entities())
            {
                System.Guid guid = new System.Guid(prjId);
                PT_PrjInfo project = (
                    from m in context.PT_PrjInfo
                    where m.PrjGuid == (System.Guid?)guid
                    select m).FirstOrDefault<PT_PrjInfo>();
                if (project != null)
                {
                    project.StartDate = startDate;
                    project.EndDate = endDate;
                    context.SaveChanges();
                }
            }
        }
        public static DataTable GetPrjPrivilege(string prjCode, string prjName, string startDate, string endDate, string prjManager, string prjState, string userCode, int pageNo, int pageSize, string IsTender, string prjProperty, string setUpDep, string kind, ref int refRowCount)
        {
            DataTable dt = new DataTable();
            string columns = "TypeCode,Primit,i_ChildNum,PrjGuid,PrjCode,PrjName,InputDate,StartDate,EndDate,PrjMangerName,PrjStateName,IsTender,PrjPropertyName,ProjPeopleDep";
            System.Collections.Generic.List<SqlParameter> paras = new System.Collections.Generic.List<SqlParameter>();
            SqlParameter rowCount = new SqlParameter("@rowCount", SqlDbType.Int);
            rowCount.Direction = ParameterDirection.Output;
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                strSql.AppendFormat(" AND PrjCode like  '%{0}%' ", prjCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                strSql.AppendFormat(" AND PrjName like  '%{0}%' ", prjName);
            }
            if (!string.IsNullOrEmpty(prjManager))
            {
                strSql.AppendFormat(" AND PrjMangerName like  '%{0}%' ", prjManager);
            }
            if (!string.IsNullOrEmpty(prjState))
            {
                strSql.AppendFormat(" AND PrjState={0}", prjState);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                strSql.AppendFormat(" AND InputDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                strSql.AppendFormat(" AND InputDate <= '{0}' ", endDate + " 23:59:59");
            }
            if (!string.IsNullOrEmpty(prjProperty))
            {
                strSql.AppendFormat(" AND PrjProperty = '{0}' ", prjProperty);
            }
            if (!string.IsNullOrWhiteSpace(setUpDep))
            {
                strSql.AppendFormat(" AND ProjPeopleDep LIKE '%{0}%' \n ", setUpDep);
            }
            if (!string.IsNullOrWhiteSpace(kind))
            {
                strSql.AppendFormat("  AND P.PrjGuid IN (SELECT PrjGuid FROM PT_PrjInfo_Kind WHERE PrjGuid = P.PrjGuid AND PrjKind = '{0}') \n", kind);
            }
            paras.Add(new SqlParameter("@userCode", userCode));
            paras.Add(new SqlParameter("@isTender", IsTender));
            paras.Add(new SqlParameter("@columns", columns));
            paras.Add(new SqlParameter("@condition", strSql.ToString()));
            paras.Add(new SqlParameter("@pageIndex", pageNo));
            paras.Add(new SqlParameter("@pageSize", pageSize));
            paras.Add(rowCount);
            dt = SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "uspGetProject", paras.ToArray());
            refRowCount = System.Convert.ToInt32(rowCount.Value);
            return dt;
        }
        public int GetStartWorkPrjCount(string prjName, string prjCode, System.DateTime? start, System.DateTime? end, string flowState, int? prjState, string userCode, string prjManagerName)
        {
            System.Collections.Generic.List<SqlParameter> paras = new System.Collections.Generic.List<SqlParameter>();
            System.Text.StringBuilder sqlDirectWhere = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                sqlDirectWhere.AppendFormat(" AND PrjCode LIKE '%'+@prjCode+'%' ", new object[0]);
                paras.Add(new SqlParameter("@prjCode", prjCode));
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                sqlDirectWhere.AppendFormat(" AND PrjName LIKE '%' + @prjName + '%' ", new object[0]);
                paras.Add(new SqlParameter("@prjName", prjName));
            }
            if (!string.IsNullOrEmpty(prjManagerName))
            {
                sqlDirectWhere.AppendFormat(" AND PrjManager LIKE '%' + @prjManagerName + '%' ", new object[0]);
                paras.Add(new SqlParameter("@prjManagerName", prjManagerName));
            }
            if (prjState.HasValue)
            {
                sqlDirectWhere.AppendFormat(" AND PrjState= @prjState ", new object[0]);
                paras.Add(new SqlParameter("@prjState", prjState));
            }
            if (start.HasValue)
            {
                sqlDirectWhere.AppendFormat(" AND P.InputDate >= @start ", new object[0]);
                paras.Add(new SqlParameter("@start", Common2.GetTime(start)));
            }
            if (end.HasValue)
            {
                sqlDirectWhere.AppendFormat(" AND P.InputDate < @end ", new object[0]);
                paras.Add(new SqlParameter("@end", Common2.GetTime(end)));
            }
            if (flowState != null)
            {
                if (!string.IsNullOrEmpty(flowState))
                {
                    sqlDirectWhere.AppendFormat(" AND FlowState= @flowState  ", new object[0]);
                    paras.Add(new SqlParameter("@flowState", flowState));
                }
                else
                {
                    sqlDirectWhere.AppendFormat(" AND FlowState IS NULL ", new object[0]);
                }
            }
            paras.Add(new SqlParameter("@userCode", userCode));
            string sqlGetPrjCount = " \r\n                --declare @UserCode nvarchar(500);\r\n                --declare @prjCode nvarchar(500),@prjName nvarchar(500), @start Datetime,@end Datetime;\r\n                --declare @flowState nvarchar(60),@prjState int,@prjManagerName;\r\n                --set @UserCode='00000000';\r\n                --set @prjCode='';\r\n                --set @prjName='';\r\n                --set @start='';\r\n                --set @end='';\r\n                --set @flowState='';\r\n                --set @prjState=;\r\n                --set @prjManagerName='';\r\n                WITH Project AS\r\n                (\r\n\t                SELECT P.PrjGuid, P.TypeCode\r\n\t                FROM vProject AS P\r\n                    LEFT JOIN PT_StartReport StartReport ON P.PrjGuid=StartReport.PrjGuid\r\n\t                WHERE P.PrjGuid IN (SELECT DISTINCT PrjGuid FROM PT_PrjInfo_ZTB_User WHERE UserCode = @UserCode) \r\n\t                AND SetUpFlowState=1 AND (PrjState=7 OR PrjState=17 OR prjState=5) ";
            sqlGetPrjCount += sqlDirectWhere.ToString();
            sqlGetPrjCount += "\r\n                )\r\n                ,DirectProject AS\r\n                (\r\n\t                SELECT Primit,TypeCode,PrjGuid,PrjCode,PrjName,PrjManager AS Person,PrjState,\r\n\t                CASE LEN(TypeCode) \r\n\t                WHEN 10 THEN (\r\n\t                SELECT InputDate FROM vProject AS V1 WHERE V1.TypeCode = SUBSTRING(D.TypeCode, 1, 5)\r\n\t                )\r\n\t                WHEN 5 THEN InputDate\r\n\t                END AS InputDate\r\n\t                FROM (\r\n\t                SELECT C.Primit, vProject.* \r\n\t                FROM (\r\n\t                SELECT pro.*, 1 AS Primit \r\n\t                FROM (SELECT * FROM Project) AS pro\r\n\t                UNION\r\n\t                SELECT P.PrjGuid, P.TypeCode, \r\n\t                (SELECT COUNT(*) FROM \r\n\t                --不符合条件的PrjGuid\r\n\t                (SELECT prjGuid FROM vProject WHERE prjGuid IN\r\n\t                (SELECT DISTINCT PrjGuid FROM PT_PrjInfo_ZTB_User WHERE UserCode = @UserCode)\r\n\t                AND SetUpFlowState=1 ) \r\n\t                 AS PP WHERE PP.PrjGuid = P.PrjGuid) AS Primit\r\n\t                FROM PT_PrjInfo AS P\r\n\t                WHERE P.TypeCode IN (\r\n\t                SELECT DISTINCT LEFT(TypeCode, 5) ParentTypeCode\r\n\t                FROM (SELECT * FROM Project) AS pro\r\n\t                WHERE LEN(pro.TypeCode) = 10\r\n\t                )\r\n\t                ) AS C\r\n\t                INNER JOIN vProject ON vProject.PrjGuid = C.PrjGuid ) AS D \r\n                )\r\n\r\n                SELECT COUNT(*)\r\n                FROM(\r\n                SELECT * FROM DirectProject\r\n                ) AS NewProjectList ";
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, sqlGetPrjCount, paras.ToArray()));
        }
        public DataTable GetStartWorkPrjInfos(string prjName, string prjCode, System.DateTime? start, System.DateTime? end, string flowState, int? prjState, string userCode, string prjManagerName, int pageSize, int pageIndex)
        {
            System.Collections.Generic.List<SqlParameter> paras = new System.Collections.Generic.List<SqlParameter>();
            System.Text.StringBuilder sqlDirectWhere = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(prjCode))
            {
                sqlDirectWhere.AppendFormat(" AND PrjCode LIKE '%'+@prjCode+'%' ", new object[0]);
                paras.Add(new SqlParameter("@prjCode", prjCode));
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                sqlDirectWhere.AppendFormat(" AND PrjName LIKE '%' + @prjName + '%' ", new object[0]);
                paras.Add(new SqlParameter("@prjName", prjName));
            }
            if (!string.IsNullOrEmpty(prjManagerName))
            {
                sqlDirectWhere.AppendFormat(" AND PrjManager LIKE '%' + @prjManagerName + '%' ", new object[0]);
                paras.Add(new SqlParameter("@prjManagerName", prjManagerName));
            }
            if (prjState.HasValue)
            {
                sqlDirectWhere.AppendFormat(" AND PrjState= @prjState ", new object[0]);
                paras.Add(new SqlParameter("@prjState", prjState));
            }
            if (start.HasValue)
            {
                sqlDirectWhere.AppendFormat(" AND P.InputDate >= @start ", new object[0]);
                paras.Add(new SqlParameter("@start", Common2.GetTime(start)));
            }
            if (end.HasValue)
            {
                sqlDirectWhere.AppendFormat(" AND P.InputDate < @end ", new object[0]);
                paras.Add(new SqlParameter("@end", Common2.GetTime(end)));
            }
            if (flowState != null)
            {
                if (!string.IsNullOrEmpty(flowState))
                {
                    sqlDirectWhere.AppendFormat(" AND FlowState= @flowState  ", new object[0]);
                    paras.Add(new SqlParameter("@flowState", flowState));
                }
                else
                {
                    sqlDirectWhere.AppendFormat(" AND FlowState IS NULL ", new object[0]);
                }
            }
            paras.Add(new SqlParameter("@userCode", userCode));
            paras.Add(new SqlParameter("@pageIndex", pageIndex));
            paras.Add(new SqlParameter("@pageSize", pageSize));
            string sqlGetPrjInfos = " \r\n                --declare @UserCode nvarchar(500),@pageSize int, @pageIndex int;\r\n                --declare @prjCode nvarchar(500),@prjName nvarchar(500), @start Datetime,@end Datetime;\r\n                --declare @flowState nvarchar(60),@prjState int,@prjManagerName;\r\n                --set @UserCode='00000000';\r\n                --set @pageSize=1000;\r\n                --set @pageIndex=1;\r\n                --set @prjCode='';\r\n                --set @prjName='';\r\n                --set @start='';\r\n                --set @end='';\r\n                --set @flowState='';\r\n                --set @prjState='';\r\n                --set @prjManagerName='';\r\n                WITH Project AS\r\n                (\r\n\t                SELECT P.PrjGuid, P.TypeCode\r\n\t                FROM vProject AS P\r\n                    LEFT JOIN PT_StartReport StartReport ON P.PrjGuid=StartReport.PrjGuid\r\n\t                WHERE P.PrjGuid IN (SELECT DISTINCT PrjGuid FROM PT_PrjInfo_ZTB_User WHERE UserCode = @UserCode) \r\n\t                AND SetUpFlowState=1 AND (PrjState=7 OR PrjState=17 OR prjState=5) ";
            sqlGetPrjInfos += sqlDirectWhere.ToString();
            sqlGetPrjInfos += "\r\n                )\r\n                ,DirectProject AS\r\n                (\r\n\t                SELECT Primit,TypeCode,PrjGuid,PrjCode,PrjName,PrjManager AS Person,PrjState,\r\n\t                CASE LEN(TypeCode) \r\n\t                WHEN 10 THEN (\r\n\t                SELECT InputDate FROM vProject AS V1 WHERE V1.TypeCode = SUBSTRING(D.TypeCode, 1, 5)\r\n\t                )\r\n\t                WHEN 5 THEN InputDate\r\n\t                END AS InputDate\r\n\t                FROM (\r\n\t                SELECT C.Primit, vProject.* \r\n\t                FROM (\r\n\t                SELECT pro.*, 1 AS Primit \r\n\t                FROM (SELECT * FROM Project) AS pro\r\n\t                UNION\r\n\t                SELECT P.PrjGuid, P.TypeCode, \r\n\t                (SELECT COUNT(*) FROM \r\n\t                --不符合条件的PrjGuid\r\n\t                (SELECT prjGuid FROM vProject WHERE prjGuid IN\r\n\t                (SELECT DISTINCT PrjGuid FROM PT_PrjInfo_ZTB_User WHERE UserCode = @UserCode)\r\n\t                AND SetUpFlowState=1 ) \r\n\t                 AS PP WHERE PP.PrjGuid = P.PrjGuid) AS Primit\r\n\t                FROM PT_PrjInfo AS P\r\n\t                WHERE P.TypeCode IN (\r\n\t                SELECT DISTINCT LEFT(TypeCode, 5) ParentTypeCode\r\n\t                FROM (SELECT * FROM Project) AS pro\r\n\t                WHERE LEN(pro.TypeCode) = 10\r\n\t                )\r\n\t                ) AS C\r\n\t                INNER JOIN vProject ON vProject.PrjGuid = C.PrjGuid ) AS D \r\n                )\r\n\r\n                SELECT TOP (@pageSize) * FROM \r\n                (\r\n                SELECT ROW_NUMBER() OVER(ORDER BY NewProjectList.InputDate DESC, NewProjectList.TypeCode ASC) AS No,NewProjectList.*,\r\n                ProjectState.ItemName,ISNULL(ReportId,'') ReportId,SP.FlowState\r\n                FROM(\r\n                SELECT * FROM DirectProject\r\n                ) AS NewProjectList \r\n                LEFT JOIN (SELECT * FROM Basic_CodeList WHERE TypeCode='ProjectState') AS ProjectState \r\n                ON NewProjectList.PrjState=ProjectState.ItemCode \r\n                LEFT JOIN PT_StartReport SP ON NewProjectList.PrjGuid=SP.PrjGuid\r\n                ) AS AllProjectInfo WHERE [No]>@pageSize*(@pageIndex-1) ";
            return SqlHelper.ExecuteQuery(CommandType.Text, sqlGetPrjInfos, paras.ToArray());
        }
    }
}
