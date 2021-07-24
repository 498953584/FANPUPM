namespace cn.justwin.DAL
{
    using System;
    using System.Data.EntityClient;
    using System.Data.Objects;

    public class pm2Entities : ObjectContext
    {
        private ObjectSet<cn.justwin.DAL.Basic_CodeList> _Basic_CodeList;
        private ObjectSet<cn.justwin.DAL.Basic_CodeType> _Basic_CodeType;
        private ObjectSet<cn.justwin.DAL.Basic_ContactInfo> _Basic_ContactInfo;
        private ObjectSet<cn.justwin.DAL.Basic_Privilege> _Basic_Privilege;
        private ObjectSet<cn.justwin.DAL.Bud_ConsReport> _Bud_ConsReport;
        private ObjectSet<cn.justwin.DAL.Bud_ConsTask> _Bud_ConsTask;
        private ObjectSet<cn.justwin.DAL.Bud_ConsTaskRes> _Bud_ConsTaskRes;
        private ObjectSet<cn.justwin.DAL.Bud_ContractResource> _Bud_ContractResource;
        private ObjectSet<cn.justwin.DAL.Bud_ContractTask> _Bud_ContractTask;
        private ObjectSet<cn.justwin.DAL.Bud_ContractTaskAudit> _Bud_ContractTaskAudit;
        private ObjectSet<cn.justwin.DAL.Bud_CostAccounting> _Bud_CostAccounting;
        private ObjectSet<cn.justwin.DAL.Bud_IndirectBudget> _Bud_IndirectBudget;
        private ObjectSet<cn.justwin.DAL.Bud_IndirectDiaryCost> _Bud_IndirectDiaryCost;
        private ObjectSet<cn.justwin.DAL.Bud_IndirectDiaryDetails> _Bud_IndirectDiaryDetails;
        private ObjectSet<cn.justwin.DAL.Bud_IndirectMonthBudget> _Bud_IndirectMonthBudget;
        private ObjectSet<cn.justwin.DAL.Bud_OrganizationBudget> _Bud_OrganizationBudget;
        private ObjectSet<cn.justwin.DAL.Bud_OrganizationMonthBudget> _Bud_OrganizationMonthBudget;
        private ObjectSet<cn.justwin.DAL.Bud_OrgDiaryCost> _Bud_OrgDiaryCost;
        private ObjectSet<cn.justwin.DAL.Bud_OrgDiaryDetails> _Bud_OrgDiaryDetails;
        private ObjectSet<cn.justwin.DAL.Bud_PrjTaskInfo> _Bud_PrjTaskInfo;
        private ObjectSet<cn.justwin.DAL.Bud_Task> _Bud_Task;
        private ObjectSet<cn.justwin.DAL.Bud_TaskChange> _Bud_TaskChange;
        private ObjectSet<cn.justwin.DAL.Bud_TaskResource> _Bud_TaskResource;
        private ObjectSet<cn.justwin.DAL.Bud_Template> _Bud_Template;
        private ObjectSet<cn.justwin.DAL.Bud_TemplateItem> _Bud_TemplateItem;
        private ObjectSet<cn.justwin.DAL.Bud_TemplateResource> _Bud_TemplateResource;
        private ObjectSet<cn.justwin.DAL.Bud_TemplateType> _Bud_TemplateType;
        private ObjectSet<cn.justwin.DAL.plus_progress> _plus_progress;
        private ObjectSet<cn.justwin.DAL.plus_progress_privilege> _plus_progress_privilege;
        private ObjectSet<cn.justwin.DAL.plus_progress_version> _plus_progress_version;
        private ObjectSet<cn.justwin.DAL.plus_report> _plus_report;
        private ObjectSet<cn.justwin.DAL.plus_reportDetail> _plus_reportDetail;
        private ObjectSet<cn.justwin.DAL.PT_d_bm> _PT_d_bm;
        private ObjectSet<cn.justwin.DAL.PT_Prj_Completed> _PT_Prj_Completed;
        private ObjectSet<cn.justwin.DAL.PT_Prj_Completed_Annex> _PT_Prj_Completed_Annex;
        private ObjectSet<cn.justwin.DAL.PT_Prj_Completed_Detail> _PT_Prj_Completed_Detail;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo> _PT_PrjInfo;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo_EngineeringType> _PT_PrjInfo_EngineeringType;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo_Kind> _PT_PrjInfo_Kind;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo_Rank> _PT_PrjInfo_Rank;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB> _PT_PrjInfo_ZTB;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail> _PT_PrjInfo_ZTB_Detail;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_Stock> _PT_PrjInfo_ZTB_Stock;
        private ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_User> _PT_PrjInfo_ZTB_User;
        private ObjectSet<cn.justwin.DAL.PT_PrjMember> _PT_PrjMember;
        private ObjectSet<cn.justwin.DAL.PT_Warning> _PT_Warning;
        private ObjectSet<cn.justwin.DAL.PT_yhmc> _PT_yhmc;
        private ObjectSet<cn.justwin.DAL.Res_Attribute> _Res_Attribute;
        private ObjectSet<cn.justwin.DAL.Res_Price> _Res_Price;
        private ObjectSet<cn.justwin.DAL.Res_PriceType> _Res_PriceType;
        private ObjectSet<cn.justwin.DAL.Res_Resource> _Res_Resource;
        private ObjectSet<cn.justwin.DAL.Res_ResourceTemp> _Res_ResourceTemp;
        private ObjectSet<cn.justwin.DAL.Res_ResourceType> _Res_ResourceType;
        private ObjectSet<cn.justwin.DAL.Res_Template> _Res_Template;
        private ObjectSet<cn.justwin.DAL.Res_TemplateItem> _Res_TemplateItem;
        private ObjectSet<cn.justwin.DAL.Res_Unit> _Res_Unit;
        private ObjectSet<cn.justwin.DAL.V_PCSupplier> _V_PCSupplier;
        private ObjectSet<cn.justwin.DAL.v_pt_d_bm> _v_pt_d_bm;
        private ObjectSet<cn.justwin.DAL.vGetCurBudVersion> _vGetCurBudVersion;
        private ObjectSet<cn.justwin.DAL.vSupplierProject> _vSupplierProject;
        private ObjectSet<cn.justwin.DAL.XPM_Basic_CodeList> _XPM_Basic_CodeList;
        private ObjectSet<cn.justwin.DAL.XPM_Basic_CodeType> _XPM_Basic_CodeType;
        private ObjectSet<cn.justwin.DAL.XPM_Basic_ContactCorp> _XPM_Basic_ContactCorp;

        public pm2Entities() : base("name=pm2Entities", "pm2Entities")
        {
        }

        public pm2Entities(EntityConnection connection) : base(connection, "pm2Entities")
        {
        }

        public pm2Entities(string connectionString) : base(connectionString, "pm2Entities")
        {
        }

        public void AddToBasic_CodeList(cn.justwin.DAL.Basic_CodeList basic_CodeList)
        {
            base.AddObject("Basic_CodeList", basic_CodeList);
        }

        public void AddToBasic_CodeType(cn.justwin.DAL.Basic_CodeType basic_CodeType)
        {
            base.AddObject("Basic_CodeType", basic_CodeType);
        }

        public void AddToBasic_ContactInfo(cn.justwin.DAL.Basic_ContactInfo basic_ContactInfo)
        {
            base.AddObject("Basic_ContactInfo", basic_ContactInfo);
        }

        public void AddToBasic_Privilege(cn.justwin.DAL.Basic_Privilege basic_Privilege)
        {
            base.AddObject("Basic_Privilege", basic_Privilege);
        }

        public void AddToBud_ConsReport(cn.justwin.DAL.Bud_ConsReport bud_ConsReport)
        {
            base.AddObject("Bud_ConsReport", bud_ConsReport);
        }

        public void AddToBud_ConsTask(cn.justwin.DAL.Bud_ConsTask bud_ConsTask)
        {
            base.AddObject("Bud_ConsTask", bud_ConsTask);
        }

        public void AddToBud_ConsTaskRes(cn.justwin.DAL.Bud_ConsTaskRes bud_ConsTaskRes)
        {
            base.AddObject("Bud_ConsTaskRes", bud_ConsTaskRes);
        }

        public void AddToBud_ContractResource(cn.justwin.DAL.Bud_ContractResource bud_ContractResource)
        {
            base.AddObject("Bud_ContractResource", bud_ContractResource);
        }

        public void AddToBud_ContractTask(cn.justwin.DAL.Bud_ContractTask bud_ContractTask)
        {
            base.AddObject("Bud_ContractTask", bud_ContractTask);
        }

        public void AddToBud_ContractTaskAudit(cn.justwin.DAL.Bud_ContractTaskAudit bud_ContractTaskAudit)
        {
            base.AddObject("Bud_ContractTaskAudit", bud_ContractTaskAudit);
        }

        public void AddToBud_CostAccounting(cn.justwin.DAL.Bud_CostAccounting bud_CostAccounting)
        {
            base.AddObject("Bud_CostAccounting", bud_CostAccounting);
        }

        public void AddToBud_IndirectBudget(cn.justwin.DAL.Bud_IndirectBudget bud_IndirectBudget)
        {
            base.AddObject("Bud_IndirectBudget", bud_IndirectBudget);
        }

        public void AddToBud_IndirectDiaryCost(cn.justwin.DAL.Bud_IndirectDiaryCost bud_IndirectDiaryCost)
        {
            base.AddObject("Bud_IndirectDiaryCost", bud_IndirectDiaryCost);
        }

        public void AddToBud_IndirectDiaryDetails(cn.justwin.DAL.Bud_IndirectDiaryDetails bud_IndirectDiaryDetails)
        {
            base.AddObject("Bud_IndirectDiaryDetails", bud_IndirectDiaryDetails);
        }

        public void AddToBud_IndirectMonthBudget(cn.justwin.DAL.Bud_IndirectMonthBudget bud_IndirectMonthBudget)
        {
            base.AddObject("Bud_IndirectMonthBudget", bud_IndirectMonthBudget);
        }

        public void AddToBud_OrganizationBudget(cn.justwin.DAL.Bud_OrganizationBudget bud_OrganizationBudget)
        {
            base.AddObject("Bud_OrganizationBudget", bud_OrganizationBudget);
        }

        public void AddToBud_OrganizationMonthBudget(cn.justwin.DAL.Bud_OrganizationMonthBudget bud_OrganizationMonthBudget)
        {
            base.AddObject("Bud_OrganizationMonthBudget", bud_OrganizationMonthBudget);
        }

        public void AddToBud_OrgDiaryCost(cn.justwin.DAL.Bud_OrgDiaryCost bud_OrgDiaryCost)
        {
            base.AddObject("Bud_OrgDiaryCost", bud_OrgDiaryCost);
        }

        public void AddToBud_OrgDiaryDetails(cn.justwin.DAL.Bud_OrgDiaryDetails bud_OrgDiaryDetails)
        {
            base.AddObject("Bud_OrgDiaryDetails", bud_OrgDiaryDetails);
        }

        public void AddToBud_PrjTaskInfo(cn.justwin.DAL.Bud_PrjTaskInfo bud_PrjTaskInfo)
        {
            base.AddObject("Bud_PrjTaskInfo", bud_PrjTaskInfo);
        }

        public void AddToBud_Task(cn.justwin.DAL.Bud_Task bud_Task)
        {
            base.AddObject("Bud_Task", bud_Task);
        }

        public void AddToBud_TaskChange(cn.justwin.DAL.Bud_TaskChange bud_TaskChange)
        {
            base.AddObject("Bud_TaskChange", bud_TaskChange);
        }

        public void AddToBud_TaskResource(cn.justwin.DAL.Bud_TaskResource bud_TaskResource)
        {
            base.AddObject("Bud_TaskResource", bud_TaskResource);
        }

        public void AddToBud_Template(cn.justwin.DAL.Bud_Template bud_Template)
        {
            base.AddObject("Bud_Template", bud_Template);
        }

        public void AddToBud_TemplateItem(cn.justwin.DAL.Bud_TemplateItem bud_TemplateItem)
        {
            base.AddObject("Bud_TemplateItem", bud_TemplateItem);
        }

        public void AddToBud_TemplateResource(cn.justwin.DAL.Bud_TemplateResource bud_TemplateResource)
        {
            base.AddObject("Bud_TemplateResource", bud_TemplateResource);
        }

        public void AddToBud_TemplateType(cn.justwin.DAL.Bud_TemplateType bud_TemplateType)
        {
            base.AddObject("Bud_TemplateType", bud_TemplateType);
        }

        public void AddToplus_progress(cn.justwin.DAL.plus_progress plus_progress)
        {
            base.AddObject("plus_progress", plus_progress);
        }

        public void AddToplus_progress_privilege(cn.justwin.DAL.plus_progress_privilege plus_progress_privilege)
        {
            base.AddObject("plus_progress_privilege", plus_progress_privilege);
        }

        public void AddToplus_progress_version(cn.justwin.DAL.plus_progress_version plus_progress_version)
        {
            base.AddObject("plus_progress_version", plus_progress_version);
        }

        public void AddToplus_report(cn.justwin.DAL.plus_report plus_report)
        {
            base.AddObject("plus_report", plus_report);
        }

        public void AddToplus_reportDetail(cn.justwin.DAL.plus_reportDetail plus_reportDetail)
        {
            base.AddObject("plus_reportDetail", plus_reportDetail);
        }

        public void AddToPT_d_bm(cn.justwin.DAL.PT_d_bm pT_d_bm)
        {
            base.AddObject("PT_d_bm", pT_d_bm);
        }

        public void AddToPT_Prj_Completed(cn.justwin.DAL.PT_Prj_Completed pT_Prj_Completed)
        {
            base.AddObject("PT_Prj_Completed", pT_Prj_Completed);
        }

        public void AddToPT_Prj_Completed_Annex(cn.justwin.DAL.PT_Prj_Completed_Annex pT_Prj_Completed_Annex)
        {
            base.AddObject("PT_Prj_Completed_Annex", pT_Prj_Completed_Annex);
        }

        public void AddToPT_Prj_Completed_Detail(cn.justwin.DAL.PT_Prj_Completed_Detail pT_Prj_Completed_Detail)
        {
            base.AddObject("PT_Prj_Completed_Detail", pT_Prj_Completed_Detail);
        }

        public void AddToPT_PrjInfo(cn.justwin.DAL.PT_PrjInfo pT_PrjInfo)
        {
            base.AddObject("PT_PrjInfo", pT_PrjInfo);
        }

        public void AddToPT_PrjInfo_EngineeringType(cn.justwin.DAL.PT_PrjInfo_EngineeringType pT_PrjInfo_EngineeringType)
        {
            base.AddObject("PT_PrjInfo_EngineeringType", pT_PrjInfo_EngineeringType);
        }

        public void AddToPT_PrjInfo_Kind(cn.justwin.DAL.PT_PrjInfo_Kind pT_PrjInfo_Kind)
        {
            base.AddObject("PT_PrjInfo_Kind", pT_PrjInfo_Kind);
        }

        public void AddToPT_PrjInfo_Rank(cn.justwin.DAL.PT_PrjInfo_Rank pT_PrjInfo_Rank)
        {
            base.AddObject("PT_PrjInfo_Rank", pT_PrjInfo_Rank);
        }

        public void AddToPT_PrjInfo_ZTB(cn.justwin.DAL.PT_PrjInfo_ZTB pT_PrjInfo_ZTB)
        {
            base.AddObject("PT_PrjInfo_ZTB", pT_PrjInfo_ZTB);
        }

        public void AddToPT_PrjInfo_ZTB_Detail(cn.justwin.DAL.PT_PrjInfo_ZTB_Detail pT_PrjInfo_ZTB_Detail)
        {
            base.AddObject("PT_PrjInfo_ZTB_Detail", pT_PrjInfo_ZTB_Detail);
        }

        public void AddToPT_PrjInfo_ZTB_Stock(cn.justwin.DAL.PT_PrjInfo_ZTB_Stock pT_PrjInfo_ZTB_Stock)
        {
            base.AddObject("PT_PrjInfo_ZTB_Stock", pT_PrjInfo_ZTB_Stock);
        }

        public void AddToPT_PrjInfo_ZTB_User(cn.justwin.DAL.PT_PrjInfo_ZTB_User pT_PrjInfo_ZTB_User)
        {
            base.AddObject("PT_PrjInfo_ZTB_User", pT_PrjInfo_ZTB_User);
        }

        public void AddToPT_PrjMember(cn.justwin.DAL.PT_PrjMember pT_PrjMember)
        {
            base.AddObject("PT_PrjMember", pT_PrjMember);
        }

        public void AddToPT_Warning(cn.justwin.DAL.PT_Warning pT_Warning)
        {
            base.AddObject("PT_Warning", pT_Warning);
        }

        public void AddToPT_yhmc(cn.justwin.DAL.PT_yhmc pT_yhmc)
        {
            base.AddObject("PT_yhmc", pT_yhmc);
        }

        public void AddToRes_Attribute(cn.justwin.DAL.Res_Attribute res_Attribute)
        {
            base.AddObject("Res_Attribute", res_Attribute);
        }

        public void AddToRes_Price(cn.justwin.DAL.Res_Price res_Price)
        {
            base.AddObject("Res_Price", res_Price);
        }

        public void AddToRes_PriceType(cn.justwin.DAL.Res_PriceType res_PriceType)
        {
            base.AddObject("Res_PriceType", res_PriceType);
        }

        public void AddToRes_Resource(cn.justwin.DAL.Res_Resource res_Resource)
        {
            base.AddObject("Res_Resource", res_Resource);
        }

        public void AddToRes_ResourceTemp(cn.justwin.DAL.Res_ResourceTemp res_ResourceTemp)
        {
            base.AddObject("Res_ResourceTemp", res_ResourceTemp);
        }

        public void AddToRes_ResourceType(cn.justwin.DAL.Res_ResourceType res_ResourceType)
        {
            base.AddObject("Res_ResourceType", res_ResourceType);
        }

        public void AddToRes_Template(cn.justwin.DAL.Res_Template res_Template)
        {
            base.AddObject("Res_Template", res_Template);
        }

        public void AddToRes_TemplateItem(cn.justwin.DAL.Res_TemplateItem res_TemplateItem)
        {
            base.AddObject("Res_TemplateItem", res_TemplateItem);
        }

        public void AddToRes_Unit(cn.justwin.DAL.Res_Unit res_Unit)
        {
            base.AddObject("Res_Unit", res_Unit);
        }

        public void AddToV_PCSupplier(cn.justwin.DAL.V_PCSupplier v_PCSupplier)
        {
            base.AddObject("V_PCSupplier", v_PCSupplier);
        }

        public void AddTov_pt_d_bm(cn.justwin.DAL.v_pt_d_bm v_pt_d_bm)
        {
            base.AddObject("v_pt_d_bm", v_pt_d_bm);
        }

        public void AddTovGetCurBudVersion(cn.justwin.DAL.vGetCurBudVersion vGetCurBudVersion)
        {
            base.AddObject("vGetCurBudVersion", vGetCurBudVersion);
        }

        public void AddTovSupplierProject(cn.justwin.DAL.vSupplierProject vSupplierProject)
        {
            base.AddObject("vSupplierProject", vSupplierProject);
        }

        public void AddToXPM_Basic_CodeList(cn.justwin.DAL.XPM_Basic_CodeList xPM_Basic_CodeList)
        {
            base.AddObject("XPM_Basic_CodeList", xPM_Basic_CodeList);
        }

        public void AddToXPM_Basic_CodeType(cn.justwin.DAL.XPM_Basic_CodeType xPM_Basic_CodeType)
        {
            base.AddObject("XPM_Basic_CodeType", xPM_Basic_CodeType);
        }

        public void AddToXPM_Basic_ContactCorp(cn.justwin.DAL.XPM_Basic_ContactCorp xPM_Basic_ContactCorp)
        {
            base.AddObject("XPM_Basic_ContactCorp", xPM_Basic_ContactCorp);
        }

        public ObjectSet<cn.justwin.DAL.Basic_CodeList> Basic_CodeList
        {
            get
            {
                if (this._Basic_CodeList == null)
                {
                    this._Basic_CodeList = base.CreateObjectSet<cn.justwin.DAL.Basic_CodeList>("Basic_CodeList");
                }
                return this._Basic_CodeList;
            }
        }

        public ObjectSet<cn.justwin.DAL.Basic_CodeType> Basic_CodeType
        {
            get
            {
                if (this._Basic_CodeType == null)
                {
                    this._Basic_CodeType = base.CreateObjectSet<cn.justwin.DAL.Basic_CodeType>("Basic_CodeType");
                }
                return this._Basic_CodeType;
            }
        }

        public ObjectSet<cn.justwin.DAL.Basic_ContactInfo> Basic_ContactInfo
        {
            get
            {
                if (this._Basic_ContactInfo == null)
                {
                    this._Basic_ContactInfo = base.CreateObjectSet<cn.justwin.DAL.Basic_ContactInfo>("Basic_ContactInfo");
                }
                return this._Basic_ContactInfo;
            }
        }

        public ObjectSet<cn.justwin.DAL.Basic_Privilege> Basic_Privilege
        {
            get
            {
                if (this._Basic_Privilege == null)
                {
                    this._Basic_Privilege = base.CreateObjectSet<cn.justwin.DAL.Basic_Privilege>("Basic_Privilege");
                }
                return this._Basic_Privilege;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_ConsReport> Bud_ConsReport
        {
            get
            {
                if (this._Bud_ConsReport == null)
                {
                    this._Bud_ConsReport = base.CreateObjectSet<cn.justwin.DAL.Bud_ConsReport>("Bud_ConsReport");
                }
                return this._Bud_ConsReport;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_ConsTask> Bud_ConsTask
        {
            get
            {
                if (this._Bud_ConsTask == null)
                {
                    this._Bud_ConsTask = base.CreateObjectSet<cn.justwin.DAL.Bud_ConsTask>("Bud_ConsTask");
                }
                return this._Bud_ConsTask;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_ConsTaskRes> Bud_ConsTaskRes
        {
            get
            {
                if (this._Bud_ConsTaskRes == null)
                {
                    this._Bud_ConsTaskRes = base.CreateObjectSet<cn.justwin.DAL.Bud_ConsTaskRes>("Bud_ConsTaskRes");
                }
                return this._Bud_ConsTaskRes;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_ContractResource> Bud_ContractResource
        {
            get
            {
                if (this._Bud_ContractResource == null)
                {
                    this._Bud_ContractResource = base.CreateObjectSet<cn.justwin.DAL.Bud_ContractResource>("Bud_ContractResource");
                }
                return this._Bud_ContractResource;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_ContractTask> Bud_ContractTask
        {
            get
            {
                if (this._Bud_ContractTask == null)
                {
                    this._Bud_ContractTask = base.CreateObjectSet<cn.justwin.DAL.Bud_ContractTask>("Bud_ContractTask");
                }
                return this._Bud_ContractTask;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_ContractTaskAudit> Bud_ContractTaskAudit
        {
            get
            {
                if (this._Bud_ContractTaskAudit == null)
                {
                    this._Bud_ContractTaskAudit = base.CreateObjectSet<cn.justwin.DAL.Bud_ContractTaskAudit>("Bud_ContractTaskAudit");
                }
                return this._Bud_ContractTaskAudit;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_CostAccounting> Bud_CostAccounting
        {
            get
            {
                if (this._Bud_CostAccounting == null)
                {
                    this._Bud_CostAccounting = base.CreateObjectSet<cn.justwin.DAL.Bud_CostAccounting>("Bud_CostAccounting");
                }
                return this._Bud_CostAccounting;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_IndirectBudget> Bud_IndirectBudget
        {
            get
            {
                if (this._Bud_IndirectBudget == null)
                {
                    this._Bud_IndirectBudget = base.CreateObjectSet<cn.justwin.DAL.Bud_IndirectBudget>("Bud_IndirectBudget");
                }
                return this._Bud_IndirectBudget;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_IndirectDiaryCost> Bud_IndirectDiaryCost
        {
            get
            {
                if (this._Bud_IndirectDiaryCost == null)
                {
                    this._Bud_IndirectDiaryCost = base.CreateObjectSet<cn.justwin.DAL.Bud_IndirectDiaryCost>("Bud_IndirectDiaryCost");
                }
                return this._Bud_IndirectDiaryCost;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_IndirectDiaryDetails> Bud_IndirectDiaryDetails
        {
            get
            {
                if (this._Bud_IndirectDiaryDetails == null)
                {
                    this._Bud_IndirectDiaryDetails = base.CreateObjectSet<cn.justwin.DAL.Bud_IndirectDiaryDetails>("Bud_IndirectDiaryDetails");
                }
                return this._Bud_IndirectDiaryDetails;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_IndirectMonthBudget> Bud_IndirectMonthBudget
        {
            get
            {
                if (this._Bud_IndirectMonthBudget == null)
                {
                    this._Bud_IndirectMonthBudget = base.CreateObjectSet<cn.justwin.DAL.Bud_IndirectMonthBudget>("Bud_IndirectMonthBudget");
                }
                return this._Bud_IndirectMonthBudget;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_OrganizationBudget> Bud_OrganizationBudget
        {
            get
            {
                if (this._Bud_OrganizationBudget == null)
                {
                    this._Bud_OrganizationBudget = base.CreateObjectSet<cn.justwin.DAL.Bud_OrganizationBudget>("Bud_OrganizationBudget");
                }
                return this._Bud_OrganizationBudget;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_OrganizationMonthBudget> Bud_OrganizationMonthBudget
        {
            get
            {
                if (this._Bud_OrganizationMonthBudget == null)
                {
                    this._Bud_OrganizationMonthBudget = base.CreateObjectSet<cn.justwin.DAL.Bud_OrganizationMonthBudget>("Bud_OrganizationMonthBudget");
                }
                return this._Bud_OrganizationMonthBudget;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_OrgDiaryCost> Bud_OrgDiaryCost
        {
            get
            {
                if (this._Bud_OrgDiaryCost == null)
                {
                    this._Bud_OrgDiaryCost = base.CreateObjectSet<cn.justwin.DAL.Bud_OrgDiaryCost>("Bud_OrgDiaryCost");
                }
                return this._Bud_OrgDiaryCost;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_OrgDiaryDetails> Bud_OrgDiaryDetails
        {
            get
            {
                if (this._Bud_OrgDiaryDetails == null)
                {
                    this._Bud_OrgDiaryDetails = base.CreateObjectSet<cn.justwin.DAL.Bud_OrgDiaryDetails>("Bud_OrgDiaryDetails");
                }
                return this._Bud_OrgDiaryDetails;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_PrjTaskInfo> Bud_PrjTaskInfo
        {
            get
            {
                if (this._Bud_PrjTaskInfo == null)
                {
                    this._Bud_PrjTaskInfo = base.CreateObjectSet<cn.justwin.DAL.Bud_PrjTaskInfo>("Bud_PrjTaskInfo");
                }
                return this._Bud_PrjTaskInfo;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_Task> Bud_Task
        {
            get
            {
                if (this._Bud_Task == null)
                {
                    this._Bud_Task = base.CreateObjectSet<cn.justwin.DAL.Bud_Task>("Bud_Task");
                }
                return this._Bud_Task;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_TaskChange> Bud_TaskChange
        {
            get
            {
                if (this._Bud_TaskChange == null)
                {
                    this._Bud_TaskChange = base.CreateObjectSet<cn.justwin.DAL.Bud_TaskChange>("Bud_TaskChange");
                }
                return this._Bud_TaskChange;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_TaskResource> Bud_TaskResource
        {
            get
            {
                if (this._Bud_TaskResource == null)
                {
                    this._Bud_TaskResource = base.CreateObjectSet<cn.justwin.DAL.Bud_TaskResource>("Bud_TaskResource");
                }
                return this._Bud_TaskResource;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_Template> Bud_Template
        {
            get
            {
                if (this._Bud_Template == null)
                {
                    this._Bud_Template = base.CreateObjectSet<cn.justwin.DAL.Bud_Template>("Bud_Template");
                }
                return this._Bud_Template;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_TemplateItem> Bud_TemplateItem
        {
            get
            {
                if (this._Bud_TemplateItem == null)
                {
                    this._Bud_TemplateItem = base.CreateObjectSet<cn.justwin.DAL.Bud_TemplateItem>("Bud_TemplateItem");
                }
                return this._Bud_TemplateItem;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_TemplateResource> Bud_TemplateResource
        {
            get
            {
                if (this._Bud_TemplateResource == null)
                {
                    this._Bud_TemplateResource = base.CreateObjectSet<cn.justwin.DAL.Bud_TemplateResource>("Bud_TemplateResource");
                }
                return this._Bud_TemplateResource;
            }
        }

        public ObjectSet<cn.justwin.DAL.Bud_TemplateType> Bud_TemplateType
        {
            get
            {
                if (this._Bud_TemplateType == null)
                {
                    this._Bud_TemplateType = base.CreateObjectSet<cn.justwin.DAL.Bud_TemplateType>("Bud_TemplateType");
                }
                return this._Bud_TemplateType;
            }
        }

        public ObjectSet<cn.justwin.DAL.plus_progress> plus_progress
        {
            get
            {
                if (this._plus_progress == null)
                {
                    this._plus_progress = base.CreateObjectSet<cn.justwin.DAL.plus_progress>("plus_progress");
                }
                return this._plus_progress;
            }
        }

        public ObjectSet<cn.justwin.DAL.plus_progress_privilege> plus_progress_privilege
        {
            get
            {
                if (this._plus_progress_privilege == null)
                {
                    this._plus_progress_privilege = base.CreateObjectSet<cn.justwin.DAL.plus_progress_privilege>("plus_progress_privilege");
                }
                return this._plus_progress_privilege;
            }
        }

        public ObjectSet<cn.justwin.DAL.plus_progress_version> plus_progress_version
        {
            get
            {
                if (this._plus_progress_version == null)
                {
                    this._plus_progress_version = base.CreateObjectSet<cn.justwin.DAL.plus_progress_version>("plus_progress_version");
                }
                return this._plus_progress_version;
            }
        }

        public ObjectSet<cn.justwin.DAL.plus_report> plus_report
        {
            get
            {
                if (this._plus_report == null)
                {
                    this._plus_report = base.CreateObjectSet<cn.justwin.DAL.plus_report>("plus_report");
                }
                return this._plus_report;
            }
        }

        public ObjectSet<cn.justwin.DAL.plus_reportDetail> plus_reportDetail
        {
            get
            {
                if (this._plus_reportDetail == null)
                {
                    this._plus_reportDetail = base.CreateObjectSet<cn.justwin.DAL.plus_reportDetail>("plus_reportDetail");
                }
                return this._plus_reportDetail;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_d_bm> PT_d_bm
        {
            get
            {
                if (this._PT_d_bm == null)
                {
                    this._PT_d_bm = base.CreateObjectSet<cn.justwin.DAL.PT_d_bm>("PT_d_bm");
                }
                return this._PT_d_bm;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_Prj_Completed> PT_Prj_Completed
        {
            get
            {
                if (this._PT_Prj_Completed == null)
                {
                    this._PT_Prj_Completed = base.CreateObjectSet<cn.justwin.DAL.PT_Prj_Completed>("PT_Prj_Completed");
                }
                return this._PT_Prj_Completed;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_Prj_Completed_Annex> PT_Prj_Completed_Annex
        {
            get
            {
                if (this._PT_Prj_Completed_Annex == null)
                {
                    this._PT_Prj_Completed_Annex = base.CreateObjectSet<cn.justwin.DAL.PT_Prj_Completed_Annex>("PT_Prj_Completed_Annex");
                }
                return this._PT_Prj_Completed_Annex;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_Prj_Completed_Detail> PT_Prj_Completed_Detail
        {
            get
            {
                if (this._PT_Prj_Completed_Detail == null)
                {
                    this._PT_Prj_Completed_Detail = base.CreateObjectSet<cn.justwin.DAL.PT_Prj_Completed_Detail>("PT_Prj_Completed_Detail");
                }
                return this._PT_Prj_Completed_Detail;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo> PT_PrjInfo
        {
            get
            {
                if (this._PT_PrjInfo == null)
                {
                    this._PT_PrjInfo = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo>("PT_PrjInfo");
                }
                return this._PT_PrjInfo;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo_EngineeringType> PT_PrjInfo_EngineeringType
        {
            get
            {
                if (this._PT_PrjInfo_EngineeringType == null)
                {
                    this._PT_PrjInfo_EngineeringType = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo_EngineeringType>("PT_PrjInfo_EngineeringType");
                }
                return this._PT_PrjInfo_EngineeringType;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo_Kind> PT_PrjInfo_Kind
        {
            get
            {
                if (this._PT_PrjInfo_Kind == null)
                {
                    this._PT_PrjInfo_Kind = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo_Kind>("PT_PrjInfo_Kind");
                }
                return this._PT_PrjInfo_Kind;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo_Rank> PT_PrjInfo_Rank
        {
            get
            {
                if (this._PT_PrjInfo_Rank == null)
                {
                    this._PT_PrjInfo_Rank = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo_Rank>("PT_PrjInfo_Rank");
                }
                return this._PT_PrjInfo_Rank;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB> PT_PrjInfo_ZTB
        {
            get
            {
                if (this._PT_PrjInfo_ZTB == null)
                {
                    this._PT_PrjInfo_ZTB = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB>("PT_PrjInfo_ZTB");
                }
                return this._PT_PrjInfo_ZTB;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail> PT_PrjInfo_ZTB_Detail
        {
            get
            {
                if (this._PT_PrjInfo_ZTB_Detail == null)
                {
                    this._PT_PrjInfo_ZTB_Detail = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_Detail>("PT_PrjInfo_ZTB_Detail");
                }
                return this._PT_PrjInfo_ZTB_Detail;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_Stock> PT_PrjInfo_ZTB_Stock
        {
            get
            {
                if (this._PT_PrjInfo_ZTB_Stock == null)
                {
                    this._PT_PrjInfo_ZTB_Stock = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_Stock>("PT_PrjInfo_ZTB_Stock");
                }
                return this._PT_PrjInfo_ZTB_Stock;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_User> PT_PrjInfo_ZTB_User
        {
            get
            {
                if (this._PT_PrjInfo_ZTB_User == null)
                {
                    this._PT_PrjInfo_ZTB_User = base.CreateObjectSet<cn.justwin.DAL.PT_PrjInfo_ZTB_User>("PT_PrjInfo_ZTB_User");
                }
                return this._PT_PrjInfo_ZTB_User;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_PrjMember> PT_PrjMember
        {
            get
            {
                if (this._PT_PrjMember == null)
                {
                    this._PT_PrjMember = base.CreateObjectSet<cn.justwin.DAL.PT_PrjMember>("PT_PrjMember");
                }
                return this._PT_PrjMember;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_Warning> PT_Warning
        {
            get
            {
                if (this._PT_Warning == null)
                {
                    this._PT_Warning = base.CreateObjectSet<cn.justwin.DAL.PT_Warning>("PT_Warning");
                }
                return this._PT_Warning;
            }
        }

        public ObjectSet<cn.justwin.DAL.PT_yhmc> PT_yhmc
        {
            get
            {
                if (this._PT_yhmc == null)
                {
                    this._PT_yhmc = base.CreateObjectSet<cn.justwin.DAL.PT_yhmc>("PT_yhmc");
                }
                return this._PT_yhmc;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_Attribute> Res_Attribute
        {
            get
            {
                if (this._Res_Attribute == null)
                {
                    this._Res_Attribute = base.CreateObjectSet<cn.justwin.DAL.Res_Attribute>("Res_Attribute");
                }
                return this._Res_Attribute;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_Price> Res_Price
        {
            get
            {
                if (this._Res_Price == null)
                {
                    this._Res_Price = base.CreateObjectSet<cn.justwin.DAL.Res_Price>("Res_Price");
                }
                return this._Res_Price;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_PriceType> Res_PriceType
        {
            get
            {
                if (this._Res_PriceType == null)
                {
                    this._Res_PriceType = base.CreateObjectSet<cn.justwin.DAL.Res_PriceType>("Res_PriceType");
                }
                return this._Res_PriceType;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_Resource> Res_Resource
        {
            get
            {
                if (this._Res_Resource == null)
                {
                    this._Res_Resource = base.CreateObjectSet<cn.justwin.DAL.Res_Resource>("Res_Resource");
                }
                return this._Res_Resource;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_ResourceTemp> Res_ResourceTemp
        {
            get
            {
                if (this._Res_ResourceTemp == null)
                {
                    this._Res_ResourceTemp = base.CreateObjectSet<cn.justwin.DAL.Res_ResourceTemp>("Res_ResourceTemp");
                }
                return this._Res_ResourceTemp;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_ResourceType> Res_ResourceType
        {
            get
            {
                if (this._Res_ResourceType == null)
                {
                    this._Res_ResourceType = base.CreateObjectSet<cn.justwin.DAL.Res_ResourceType>("Res_ResourceType");
                }
                return this._Res_ResourceType;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_Template> Res_Template
        {
            get
            {
                if (this._Res_Template == null)
                {
                    this._Res_Template = base.CreateObjectSet<cn.justwin.DAL.Res_Template>("Res_Template");
                }
                return this._Res_Template;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_TemplateItem> Res_TemplateItem
        {
            get
            {
                if (this._Res_TemplateItem == null)
                {
                    this._Res_TemplateItem = base.CreateObjectSet<cn.justwin.DAL.Res_TemplateItem>("Res_TemplateItem");
                }
                return this._Res_TemplateItem;
            }
        }

        public ObjectSet<cn.justwin.DAL.Res_Unit> Res_Unit
        {
            get
            {
                if (this._Res_Unit == null)
                {
                    this._Res_Unit = base.CreateObjectSet<cn.justwin.DAL.Res_Unit>("Res_Unit");
                }
                return this._Res_Unit;
            }
        }

        public ObjectSet<cn.justwin.DAL.V_PCSupplier> V_PCSupplier
        {
            get
            {
                if (this._V_PCSupplier == null)
                {
                    this._V_PCSupplier = base.CreateObjectSet<cn.justwin.DAL.V_PCSupplier>("V_PCSupplier");
                }
                return this._V_PCSupplier;
            }
        }

        public ObjectSet<cn.justwin.DAL.v_pt_d_bm> v_pt_d_bm
        {
            get
            {
                if (this._v_pt_d_bm == null)
                {
                    this._v_pt_d_bm = base.CreateObjectSet<cn.justwin.DAL.v_pt_d_bm>("v_pt_d_bm");
                }
                return this._v_pt_d_bm;
            }
        }

        public ObjectSet<cn.justwin.DAL.vGetCurBudVersion> vGetCurBudVersion
        {
            get
            {
                if (this._vGetCurBudVersion == null)
                {
                    this._vGetCurBudVersion = base.CreateObjectSet<cn.justwin.DAL.vGetCurBudVersion>("vGetCurBudVersion");
                }
                return this._vGetCurBudVersion;
            }
        }

        public ObjectSet<cn.justwin.DAL.vSupplierProject> vSupplierProject
        {
            get
            {
                if (this._vSupplierProject == null)
                {
                    this._vSupplierProject = base.CreateObjectSet<cn.justwin.DAL.vSupplierProject>("vSupplierProject");
                }
                return this._vSupplierProject;
            }
        }

        public ObjectSet<cn.justwin.DAL.XPM_Basic_CodeList> XPM_Basic_CodeList
        {
            get
            {
                if (this._XPM_Basic_CodeList == null)
                {
                    this._XPM_Basic_CodeList = base.CreateObjectSet<cn.justwin.DAL.XPM_Basic_CodeList>("XPM_Basic_CodeList");
                }
                return this._XPM_Basic_CodeList;
            }
        }

        public ObjectSet<cn.justwin.DAL.XPM_Basic_CodeType> XPM_Basic_CodeType
        {
            get
            {
                if (this._XPM_Basic_CodeType == null)
                {
                    this._XPM_Basic_CodeType = base.CreateObjectSet<cn.justwin.DAL.XPM_Basic_CodeType>("XPM_Basic_CodeType");
                }
                return this._XPM_Basic_CodeType;
            }
        }

        public ObjectSet<cn.justwin.DAL.XPM_Basic_ContactCorp> XPM_Basic_ContactCorp
        {
            get
            {
                if (this._XPM_Basic_ContactCorp == null)
                {
                    this._XPM_Basic_ContactCorp = base.CreateObjectSet<cn.justwin.DAL.XPM_Basic_ContactCorp>("XPM_Basic_ContactCorp");
                }
                return this._XPM_Basic_ContactCorp;
            }
        }
    }
}

