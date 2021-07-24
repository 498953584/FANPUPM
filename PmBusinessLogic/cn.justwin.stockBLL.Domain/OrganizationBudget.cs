using cn.justwin.DAL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace cn.justwin.stockBLL.Domain
{
	public class OrganizationBudget
	{
		public string Id
		{
			get;
			set;
		}
		public string OrganizationBudgetId
		{
			get;
			set;
		}
		public string CBSCode
		{
			get;
			set;
		}
		public decimal BudgetAmount
		{
			get;
			set;
		}
		public decimal AccountAmount
		{
			get;
			set;
		}
		public string State
		{
			get;
			set;
		}
		public string InputUser
		{
			get;
			set;
		}
		public System.DateTime InputDate
		{
			get;
			set;
		}
		public string Note
		{
			get;
			set;
		}
		public CostAccounting CostAcc
		{
			get
			{
				return CostAccounting.GetByCode(this.CBSCode);
			}
		}
		private OrganizationBudget()
		{
		}
		public static OrganizationBudget Create(string id, string organizationBudgetId, string code, decimal budgetAmount, decimal accountAmount, string inputUser, System.DateTime inputDate, string Note)
		{
			string paramName = "间接成本预算";
			if (string.IsNullOrEmpty(id))
			{
				throw new System.ArgumentNullException(paramName, "id不能为空!");
			}
			if (string.IsNullOrEmpty(organizationBudgetId))
			{
				throw new System.ArgumentNullException(paramName, "组织机构编码不能为空!");
			}
			if (string.IsNullOrEmpty(code))
			{
				throw new System.ArgumentNullException(paramName, "间接成本Id不能为空!");
			}
			return new OrganizationBudget
			{
				Id = id,
				OrganizationBudgetId = organizationBudgetId,
				CBSCode = code,
				AccountAmount = accountAmount,
				BudgetAmount = budgetAmount,
				InputDate = inputDate,
				InputUser = inputUser,
				State = "0",
				Note = Note
			};
		}
		public static OrganizationBudget GetById(string id)
		{
			OrganizationBudget organBudget = null;
			using (pm2Entities context = new pm2Entities())
			{
				organBudget = (
					from m in context.Bud_OrganizationBudget
					where m.Id == id
					select new OrganizationBudget
					{
						Id = m.Id,
						State = m.State,
						InputDate = m.InputDate,
						AccountAmount = m.AccountingAmount ?? 0m,
						CBSCode = m.CBSCode,
						OrganizationBudgetId = m.OrganizationBudgetId,
						BudgetAmount = m.BudgetAmount ?? 0m
					}).FirstOrDefault<OrganizationBudget>();
			}
			return organBudget;
		}
		public static OrganizationBudget GetByOrganAndCBSCode(string organizationBudgetId, string cbsCode)
		{
			OrganizationBudget organBudget = null;
			using (pm2Entities context = new pm2Entities())
			{
				organBudget = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == organizationBudgetId && m.CBSCode == cbsCode
					select new OrganizationBudget
					{
						State = m.State,
						AccountAmount = m.AccountingAmount ?? 0m,
						CBSCode = m.CBSCode,
						OrganizationBudgetId = m.OrganizationBudgetId,
						BudgetAmount = m.BudgetAmount ?? 0m
					}).FirstOrDefault<OrganizationBudget>();
			}
			return organBudget;
		}
		public static System.Collections.Generic.List<OrganizationBudget> GetAll(string OrganizationBudgetId)
		{
			System.Collections.Generic.List<OrganizationBudget> organBudgets = new System.Collections.Generic.List<OrganizationBudget>();
			using (pm2Entities context = new pm2Entities())
			{
				organBudgets = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == OrganizationBudgetId
					orderby m.CBSCode
					select new OrganizationBudget
					{
						Id = m.Id,
						AccountAmount = m.AccountingAmount ?? 0m,
						BudgetAmount = m.BudgetAmount ?? 0m,
						CBSCode = m.CBSCode,
						State = m.State
					}).ToList<OrganizationBudget>();
				System.Collections.Generic.List<OrganizationBudget> organBudgetsAll = (
					from m in context.Bud_CostAccounting
					where m.Type == "I"
					orderby m.CBSCode
					select new OrganizationBudget
					{
						Id = m.CBSCode,
						BudgetAmount = 0m,
						AccountAmount = 0m,
						State = "0",
						CBSCode = m.CBSCode
					}).ToList<OrganizationBudget>();
				if (organBudgets.Count == 0)
				{
					organBudgets = organBudgetsAll;
				}
				else
				{
					foreach (OrganizationBudget item in organBudgetsAll)
					{
						foreach (OrganizationBudget existItem in organBudgets)
						{
							if (item.CBSCode == existItem.CBSCode)
							{
								item.BudgetAmount = existItem.BudgetAmount;
								item.AccountAmount = existItem.AccountAmount;
								item.State = existItem.State;
								organBudgets.Remove(existItem);
								break;
							}
						}
					}
					organBudgets = organBudgetsAll;
				}
			}
			return organBudgets;
		}
		public static System.Collections.Generic.List<OrganizationBudget> GetAllReport(string organizationBudgetId)
		{
			System.Collections.Generic.List<OrganizationBudget> organBudgets = new System.Collections.Generic.List<OrganizationBudget>();
			using (pm2Entities context = new pm2Entities())
			{
				organBudgets = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == organizationBudgetId && (m.State == "1" || m.State == "4")
					orderby m.CBSCode
					select new OrganizationBudget
					{
						Id = m.Id,
						BudgetAmount = m.BudgetAmount ?? 0m,
						CBSCode = m.CBSCode,
						State = m.State
					}).ToList<OrganizationBudget>();
			}
			return organBudgets;
		}
		public static System.Collections.Generic.List<OrganizationBudget> GetAllPass(string organizationBudgetId)
		{
			System.Collections.Generic.List<OrganizationBudget> organBudgets = new System.Collections.Generic.List<OrganizationBudget>();
			using (pm2Entities context = new pm2Entities())
			{
				organBudgets = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == organizationBudgetId && m.State == "2"
					orderby m.CBSCode
					select new OrganizationBudget
					{
						Id = m.Id,
						AccountAmount = (decimal)m.AccountingAmount,
						BudgetAmount = (decimal)m.BudgetAmount,
						CBSCode = m.CBSCode
					}).ToList<OrganizationBudget>();
				if (organBudgets.Count > 0)
				{
					OrganizationBudget.AddTotalIndirect(organBudgets);
				}
			}
			return organBudgets;
		}
		public static System.Collections.Generic.List<OrganizationBudget> GetAllNotEReport(string organizationBudgetId)
		{
			System.Collections.Generic.List<OrganizationBudget> organBudgets = new System.Collections.Generic.List<OrganizationBudget>();
			using (pm2Entities context = new pm2Entities())
			{
				organBudgets = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == organizationBudgetId && (m.State == "1" || m.State == "2" || m.State == "4")
					orderby m.CBSCode
					select new OrganizationBudget
					{
						Id = m.Id,
						AccountAmount = m.AccountingAmount ?? 0m,
						BudgetAmount = m.BudgetAmount ?? 0m,
						CBSCode = m.CBSCode,
						State = m.State
					}).ToList<OrganizationBudget>();
				if (organBudgets.Count > 0)
				{
					OrganizationBudget.AddTotalIndirect(organBudgets);
				}
			}
			return organBudgets;
		}
		protected static void AddTotalIndirect(System.Collections.Generic.List<OrganizationBudget> indirectBudgets)
		{
			indirectBudgets.Insert(0, new OrganizationBudget
			{
				Id = System.Guid.NewGuid().ToString(),
				CBSCode = "001002",
				AccountAmount = 0m,
				BudgetAmount = 0m,
				State = "2"
			});
		}
		public static bool IsExistOrgan(string organizationBudgetId)
		{
			bool exist = false;
			using (pm2Entities context = new pm2Entities())
			{
				Bud_OrganizationBudget query = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == organizationBudgetId
					select m).FirstOrDefault<Bud_OrganizationBudget>();
				if (query != null)
				{
					exist = true;
				}
			}
			return exist;
		}
		public static bool IsExistCBSCode(string cbsCode)
		{
			bool exist = false;
			using (pm2Entities context = new pm2Entities())
			{
				Bud_OrganizationBudget query = (
					from m in context.Bud_OrganizationBudget
					where m.CBSCode == cbsCode && (m.State == "1" || m.State == "2" || m.State == "4")
					select m).FirstOrDefault<Bud_OrganizationBudget>();
				if (query != null)
				{
					exist = true;
				}
			}
			return exist;
		}
		public static bool isHaveEReport(string organizationBudgetId)
		{
			bool flag = false;
			using (pm2Entities context = new pm2Entities())
			{
				int count = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == organizationBudgetId && (m.State == "1" || m.State == "4")
					select m).Count<Bud_OrganizationBudget>();
				if (count > 0)
				{
					flag = true;
				}
			}
			return flag;
		}
		public static void UpdateState(string CBSCode, string organizationBudgetId, string state)
		{
			using (pm2Entities context = new pm2Entities())
			{
				Bud_OrganizationBudget organBudget = (
					from m in context.Bud_OrganizationBudget
					where m.CBSCode == CBSCode && m.OrganizationBudgetId == organizationBudgetId
					select m).FirstOrDefault<Bud_OrganizationBudget>();
				if (organBudget != null)
				{
					organBudget.State = state;
					context.SaveChanges();
				}
			}
		}
		public static void AddOrDelDesktopNotifications(string id, bool isEReport)
		{
			string zzjgName = OrganizationBudget.GetZZJGName(id);
			System.Text.StringBuilder sqlYHMC = new System.Text.StringBuilder();
			sqlYHMC.Append("SELECT v_yhdm FROM PT_yhmc");
			sqlYHMC.AppendLine();
			sqlYHMC.AppendFormat("WHERE i_bmdm='{0}' AND State='1' AND c_sfyx='y'", id);
			SqlDataReader sdrYHBM = SqlHelper.ExecuteReader(CommandType.Text, sqlYHMC.ToString(), new SqlParameter[0]);
			PTDbsjBll pTDbsjBll = new PTDbsjBll();
			pTDbsjBll.DelPastDueData(id);
			while (sdrYHBM.Read())
			{
				if (isEReport)
				{
					string people = sdrYHBM["v_yhdm"].ToString();
					if (people != "")
					{
						System.Text.StringBuilder content = new System.Text.StringBuilder();
						content.Append("组织机构:" + zzjgName);
						content.Append("的间接成本预算已经上报,请审核!");
						pTDbsjBll.Add(new PTDbsjModel
						{
							C_OpenFlag = "0",
							DTM_DBSJ = System.DateTime.Now,
							I_XGID = id,
							V_Content = content.ToString(),
							V_DBLJ = "BudgetManage/Cost/IndirectBudgetQuery.aspx?id=" + id,
							V_LXBM = "021",
							V_TPLJ = "new_Mail.gif",
							V_YHDM = people
						});
					}
				}
			}
		}
		public static string GetZZJGName(string id)
		{
			System.Text.StringBuilder sqlName = new System.Text.StringBuilder();
			sqlName.AppendFormat("SELECT V_BMMC FROM PT_d_bm WHERE i_bmdm='{0}'", id);
			return SqlHelper.ExecuteScalar(CommandType.Text, sqlName.ToString(), new SqlParameter[0]).ToString();
		}
		public void Add(OrganizationBudget organBudget)
		{
			using (pm2Entities context = new pm2Entities())
			{
				Bud_OrganizationBudget organ_Budget = new Bud_OrganizationBudget
				{
					Id = organBudget.Id,
					BudgetAmount = new decimal?(organBudget.BudgetAmount),
					AccountingAmount = new decimal?(organBudget.AccountAmount),
					CBSCode = organBudget.CBSCode,
					OrganizationBudgetId = organBudget.OrganizationBudgetId,
					State = organBudget.State,
					Note = organBudget.Note,
					InputUser = organBudget.InputUser,
					InputDate = this.InputDate
				};
				context.AddToBud_OrganizationBudget(organ_Budget);
				context.SaveChanges();
			}
		}
		public void Update(OrganizationBudget organBudget)
		{
			using (pm2Entities context = new pm2Entities())
			{
				Bud_OrganizationBudget bud_organBudget = (
					from m in context.Bud_OrganizationBudget
					where m.OrganizationBudgetId == organBudget.OrganizationBudgetId && m.CBSCode == organBudget.CBSCode
					select m).FirstOrDefault<Bud_OrganizationBudget>();
				if (bud_organBudget == null)
				{
					throw new System.Exception("此项目的间接成本预算不存在!");
				}
				bud_organBudget.BudgetAmount = new decimal?(organBudget.BudgetAmount);
				bud_organBudget.AccountingAmount = new decimal?(organBudget.AccountAmount);
				bud_organBudget.State = organBudget.State;
				context.SaveChanges();
			}
		}
	}
}
