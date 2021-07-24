namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.stockBLL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class IndirectBudget
    {
        private IndirectBudget()
        {
        }

        public void Add(IndirectBudget indirectBudget)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_IndirectBudget budget = new Bud_IndirectBudget {
                    Id = indirectBudget.Id,
                    BudgetAmount = indirectBudget.BudgetAmount,
                    AccountAmount = indirectBudget.AccountAmount,
                    CBSCode = indirectBudget.CBSCode,
                    ProjectId = indirectBudget.ProjectId,
                    State = indirectBudget.State,
                    Note = indirectBudget.Note,
                    InputUser = indirectBudget.InputUser,
                    InputDate = this.InputDate
                };
                entities.AddToBud_IndirectBudget(budget);
                entities.SaveChanges();
            }
        }

        public static void AddOrDelDesktopNotifications(string prjId, bool isEReport)
        {
            PTPrjInfoBll bll = new PTPrjInfoBll();
            PrjInfoModel modelByPrjGuid = new PrjInfoModel();
            modelByPrjGuid = bll.GetModelByPrjGuid(prjId);
            string podepom = modelByPrjGuid.Podepom;
            string[] strArray = new string[0];
            if ((podepom != null) && podepom.Contains<char>(','))
            {
                strArray = podepom.Split(new char[] { ',' });
            }
            PTDbsjBll bll2 = new PTDbsjBll();
            bll2.DelPastDueData(prjId);
            if (isEReport)
            {
                foreach (string str2 in strArray)
                {
                    if (str2 != "")
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append("项目:" + modelByPrjGuid.PrjName);
                        builder.Append("的间接成本已经上报,请审核!");
                        PTDbsjModel model = new PTDbsjModel {
                            C_OpenFlag = "0",
                            DTM_DBSJ = DateTime.Now,
                            I_XGID = prjId,
                            V_Content = builder.ToString(),
                            V_DBLJ = "BudgetManage/Cost/IndirectBudgetQuery.aspx?id=" + prjId,
                            V_LXBM = "021",
                            V_TPLJ = "new_Mail.gif",
                            V_YHDM = str2
                        };
                        try
                        {
                            bll2.Add(model);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        protected static void AddTotalIndirect(List<IndirectBudget> indirectBudgets)
        {
            IndirectBudget item = new IndirectBudget {
                Id = Guid.NewGuid().ToString(),
                CBSCode = "001002",
                AccountAmount = 0M,
                BudgetAmount = 0M,
                State = "2"
            };
            indirectBudgets.Insert(0, item);
        }

        public static IndirectBudget Create(string id, string projectId, string code, decimal budgetAmount, decimal accountAmount, string inputUser, DateTime inputDate, string Note)
        {
            string paramName = "间接成本预算";
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(paramName, "id不能为空!");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException(paramName, "项目Id不能为空!");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(paramName, "间接成本Id不能为空!");
            }
            return new IndirectBudget { Id = id, ProjectId = projectId, CBSCode = code, AccountAmount = accountAmount, BudgetAmount = budgetAmount, InputDate = inputDate, InputUser = inputUser, State = "0", Note = Note };
        }

        public static List<IndirectBudget> GetAll(string prjId)
        {
            List<IndirectBudget> list = new List<IndirectBudget>();
            using (pm2Entities entities = new pm2Entities())
            {
                list = (from m in entities.Bud_IndirectBudget
                    where m.ProjectId == prjId
                    orderby m.CBSCode
                    select new IndirectBudget { Id = m.Id, AccountAmount = m.AccountAmount, BudgetAmount = m.BudgetAmount, CBSCode = m.CBSCode, State = m.State }).ToList<IndirectBudget>();
                List<IndirectBudget> list2 = (from m in entities.Bud_CostAccounting
                    where m.Type == "I"
                    orderby m.CBSCode
                    select new IndirectBudget { Id = m.CBSCode, BudgetAmount = 0M, AccountAmount = 0M, State = "0", CBSCode = m.CBSCode }).ToList<IndirectBudget>();
                if (list.Count != 0)
                {
                    foreach (IndirectBudget budget in list2)
                    {
                        foreach (IndirectBudget budget2 in list)
                        {
                            if (budget.CBSCode == budget2.CBSCode)
                            {
                                budget.BudgetAmount = budget2.BudgetAmount;
                                budget.State = budget2.State;
                                budget.AccountAmount = budget2.AccountAmount;
                                list.Remove(budget2);
                                break;
                            }
                        }
                    }
                }
                return list2;
            }
        }

        public static List<IndirectBudget> GetAllNotEReport(string prjId)
        {
            List<IndirectBudget> indirectBudgets = new List<IndirectBudget>();
            using (pm2Entities entities = new pm2Entities())
            {
                indirectBudgets = (from m in entities.Bud_IndirectBudget
                    where (m.ProjectId == prjId) && (((m.State == "1") || (m.State == "2")) || (m.State == "4"))
                    orderby m.CBSCode
                    select new IndirectBudget { Id = m.Id, AccountAmount = m.AccountAmount, BudgetAmount = m.BudgetAmount, CBSCode = m.CBSCode, State = m.State }).ToList<IndirectBudget>();
                if (indirectBudgets.Count > 0)
                {
                    AddTotalIndirect(indirectBudgets);
                }
            }
            return indirectBudgets;
        }

        public static List<IndirectBudget> GetAllPass(string prjId)
        {
            List<IndirectBudget> indirectBudgets = new List<IndirectBudget>();
            using (pm2Entities entities = new pm2Entities())
            {
                indirectBudgets = (from m in entities.Bud_IndirectBudget
                    where (m.ProjectId == prjId) && (m.State == "2")
                    orderby m.CBSCode
                    select new IndirectBudget { Id = m.Id, AccountAmount = m.AccountAmount, BudgetAmount = m.BudgetAmount, CBSCode = m.CBSCode }).ToList<IndirectBudget>();
                if (indirectBudgets.Count > 0)
                {
                    AddTotalIndirect(indirectBudgets);
                }
            }
            return indirectBudgets;
        }

        public static List<IndirectBudget> GetAllReport(string prjId)
        {
            List<IndirectBudget> list = new List<IndirectBudget>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_IndirectBudget
                    where (m.ProjectId == prjId) && ((m.State == "1") || (m.State == "4"))
                    orderby m.CBSCode
                    select new IndirectBudget { Id = m.Id, BudgetAmount = m.BudgetAmount, CBSCode = m.CBSCode, State = m.State }).ToList<IndirectBudget>();
            }
        }

        public static IndirectBudget GetById(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_IndirectBudget
                    where m.Id == id
                    select new IndirectBudget { Id = m.Id, State = m.State, InputDate = m.InputDate, AccountAmount = m.AccountAmount, CBSCode = m.CBSCode, ProjectId = m.ProjectId, BudgetAmount = m.BudgetAmount }).FirstOrDefault<IndirectBudget>();
            }
        }

        public static IndirectBudget GetByPrjIdAndCBSCode(string prjId, string cbsCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_IndirectBudget
                    where (m.ProjectId == prjId) && (m.CBSCode == cbsCode)
                    select new IndirectBudget { State = m.State, AccountAmount = m.AccountAmount, CBSCode = m.CBSCode, ProjectId = m.ProjectId, BudgetAmount = m.BudgetAmount }).FirstOrDefault<IndirectBudget>();
            }
        }

        public static int GetFlowState(string relatedId)
        {
            int num = -1;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            SELECT FlowState FROM Bud_IndirectBudgetWF\r\n            WHERE RelatedId='{0}'", relatedId);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString());
            if (obj2 != null)
            {
                num = Convert.ToInt32(obj2.ToString());
            }
            return num;
        }

        public static string GetGuid(string relatedId)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            SELECT Id FROM Bud_IndirectBudgetWF \r\n            WHERE RelatedId='{0}'", relatedId);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString());
            if (obj2 != null)
            {
                str = obj2.ToString();
            }
            return str;
        }

        public static string GetRelatedId(string id)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            SELECT RelatedId FROM Bud_IndirectBudgetWF \r\n            WHERE Id='{0}'", id);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString());
            if (obj2 != null)
            {
                str = obj2.ToString();
            }
            return str;
        }

        public static DataTable GetWFInfo(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            SELECT Isnull(T2.V_BMMC, '') + Isnull(T4.PrjName, '') AS [Name], \r\n                   T3.v_xm                                        AS userName, \r\n                   T1.InputDate \r\n            FROM   Bud_IndirectBudgetWF AS T1 \r\n                   LEFT JOIN PT_d_bm AS T2 \r\n                     ON T1.RelatedId = Cast(T2.i_bmdm AS NVARCHAR(100)) \r\n                   LEFT JOIN pt_yhmc AS T3 \r\n                     ON T1.InputUser = T3.v_yhdm \r\n                   LEFT JOIN PT_Prjinfo AS T4 \r\n                     ON T1.RelatedId = Cast(T4.PrjGuid AS NVARCHAR(100)) \r\n            WHERE  T1.Id = '{0}' ", id);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static void InsertWF(string relatedId, string inputUser, string guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n            IF EXISTS(SELECT * FROM Bud_IndirectBudgetWF WHERE RelatedId='{1}')\r\n                DELETE FROM Bud_IndirectBudgetWF WHERE RelatedId='{1}'\r\n            INSERT INTO Bud_IndirectBudgetWF(Id,RelatedId,FlowState,inputDate,inputUser) VALUES('{0}','{1}',-1,GETDATE(),'{2}')", guid, relatedId, inputUser);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString());
        }

        public static bool IsExistCBSCode(string cbsCode)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_IndirectBudget
                    where (m.CBSCode == cbsCode) && (((m.State == "1") || (m.State == "2")) || (m.State == "4"))
                    select m).FirstOrDefault<Bud_IndirectBudget>() != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool IsExistPrjId(string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_IndirectBudget
                    where m.ProjectId == prjId
                    select m).FirstOrDefault<Bud_IndirectBudget>() != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool isHaveEReport(string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_IndirectBudget
                    where (m.ProjectId == prjId) && ((m.State == "1") || (m.State == "4"))
                    select m).Count<Bud_IndirectBudget>() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static void SwapData(string id, string type)
        {
            if (!(type == "1") && !(type == "2"))
            {
                throw new Exception("指定的type类型不存在");
            }
            string relatedId = GetRelatedId(id);
            StringBuilder builder = new StringBuilder();
            int num = 2;
            if (type == "2")
            {
                num = 0;
            }
            builder.AppendFormat("UPDATE Bud_IndirectBudget SET State={0} WHERE ProjectId='{1}' AND LEN(CBSCode)>6 ", num, relatedId);
            builder.AppendFormat("UPDATE Bud_OrganizationBudget SET State={0} WHERE OrganizationBudgetId='{1}' AND LEN(CBSCode)>6", num, relatedId);
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString());
        }

        public void Update(IndirectBudget indirectBudget)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_IndirectBudget budget = (from m in entities.Bud_IndirectBudget
                    where (m.ProjectId == indirectBudget.ProjectId) && (m.CBSCode == indirectBudget.CBSCode)
                    select m).FirstOrDefault<Bud_IndirectBudget>();
                if (budget == null)
                {
                    throw new Exception("此项目的间接成本预算不存在!");
                }
                budget.BudgetAmount = indirectBudget.BudgetAmount;
                budget.AccountAmount = indirectBudget.AccountAmount;
                budget.State = indirectBudget.State;
                entities.SaveChanges();
            }
        }

        public static void UpdateState(string CBSCode, string prjId, string state)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_IndirectBudget budget = (from m in entities.Bud_IndirectBudget
                    where (m.CBSCode == CBSCode) && (m.ProjectId == prjId)
                    select m).FirstOrDefault<Bud_IndirectBudget>();
                if (budget != null)
                {
                    budget.State = state;
                    entities.SaveChanges();
                }
            }
        }

        public decimal AccountAmount { get; set; }

        public decimal BudgetAmount { get; set; }

        public string CBSCode { get; set; }

        public CostAccounting CostAcc
        {
            get
            {
                return CostAccounting.GetByCode(this.CBSCode);
            }
        }

        public string Id { get; set; }

        public DateTime InputDate { get; set; }

        public string InputUser { get; set; }

        public string Note { get; set; }

        public string ProjectId { get; set; }

        public string State { get; set; }
    }
}

