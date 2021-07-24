namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using cn.justwin.stockBLL.AccountManage.acc_Manage;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PayoutContract
    {
        private string IsIncomeContractApprove = ConfigHelper.Get("IsIncomeContractApprove");
        private cn.justwin.contractDAL.PayoutContract payoutContract = new cn.justwin.contractDAL.PayoutContract();

        public void Add(PayoutContractModel model)
        {
            this.payoutContract.Add(model, null);
        }

        public void Archived(List<string> contractIds)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                Action<string> action = null;
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    if (action == null)
                    {
                        action = delegate (string id) {
                            PayoutContractModel model = this.payoutContract.GetModel(id);
                            model.IsArchived = true;
                            model.ArchiveDate = new DateTime?(DateTime.Now);
                            this.payoutContract.Update(model, trans);
                        };
                    }
                    contractIds.ForEach(action);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("归档失败");
                }
            }
        }

        public void Delete(string ContractID)
        {
            this.payoutContract.Delete(ContractID, null);
        }

        public void Delete(List<string> contractIds)
        {
            this.payoutContract.Delete(contractIds);
        }

        public bool Exists(string contractCode)
        {
            return this.payoutContract.Exists(contractCode);
        }

        public List<PayoutContractModel> GetAllList(string userCode)
        {
            return this.GetList(string.Empty, userCode);
        }

        public DataTable GetBName(string strCorpId)
        {
            string cmdText = "SELECT CorpName FROM XPM_Basic_ContactCorp WHERE CorpId='" + strCorpId + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText);
        }

        public DataTable GetBNameReport(string strBName, string userCode, int pageIndex, int pageSize)
        {
            string cmdText = "\r\n\t\t\t\t--DECLARE @CorpName nvarchar(200);\t--供应商名称\r\n\t\t\t\t--DECLARE @PageSize int;\t\t\t\t--页大小\r\n\t\t\t\t--DECLARE @PageNo int;\t\t\t\t--页码\r\n\t\t\t\t--SET @CorpName = '';\t\r\n\t\t\t\t--SET @PageSize = 15;\r\n\t\t\t\t--SET @PageNo = 1;\r\n\t\t\t\tWITH cte_PayoutContract AS (\r\n\t\t\t\t\tSELECT C.ContractID, C.BName, C.ModifiedMoney,\r\n\t\t\t\t\t\t(SELECT SUM(BalanceMoney)FROM Con_Payout_Balance B WHERE  B.ContractID = C.ContractID AND B.FlowState = 1) AS BalanceTotal,\r\n\t\t\t\t\t\t(SELECT SUM(PaymentMoney) FROM Con_Payout_Payment P WHERE P.ContractID = C.ContractID AND P.FlowState = 1) AS PaymentTotal,\r\n\t\t\t\t\t\t(SELECT SUM(Amount) FROM Con_Payout_Invoice I WHERE I.ContractID = C.ContractID) AS InvoiceTotal\r\n\t\t\t\t\tFROM Con_Payout_Contract C\r\n\t\t\t\t\tWHERE C.FlowState = '1' \r\n\t\t\t\t\tGROUP BY C.ContractID, C.BName, C.ModifiedMoney\r\n\t\t\t\t) \r\n\t\t\t\tSELECT TOP(@PageSize) * FROM (\r\n\t\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY CorpID DESC) AS Num, CP.CorpID, CP.CorpName, \r\n\t\t\t\t\t\tISNULL(SUM(ModifiedMoney), 0.000) AS ModifiedMoney,\t\t\t--合同金额\r\n\t\t\t\t\t\tISNULL(SUM(BalanceTotal), 0.000) AS BalanceMoney,\t\t\t--累计结算金额\r\n\t\t\t\t\t\tISNULL(SUM(PaymentTotal), 0.000) AS PaymentMoney,\t\t\t--累计已付款金额\r\n\t\t\t\t\t\t(ISNULL(SUM(BalanceTotal), 0.000) - ISNULL(SUM(PaymentTotal), 0.000)) AS NoPaymentMoney,\t--应付未付金额\r\n\t\t\t\t\t\tISNULL(SUM(InvoiceTotal), 0.000) AS Amount\t\t\t\t\t--已提供发票金额\r\n\t\t\t\t\tFROM XPM_Basic_ContactCorp CP\r\n\t\t\t\t\tJOIN cte_PayoutContract C ON CP.CorpID = C.BName\r\n\t\t\t\t\tWHERE CorpName like '%' + @CorpName + '%'\r\n\t\t\t\t\tGROUP BY CP.CorpID, CP.CorpName\r\n\t\t\t\t) AS T\r\n\t\t\t\tWHERE Num > (@PageNo - 1) * @PageSize\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CorpName", strBName), new SqlParameter("@PageNo", pageIndex), new SqlParameter("@pageSize", pageSize) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetBNameReportCount(string strBName, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (\n", new object[0]);
            builder.AppendFormat("SELECT Row_Number() OVER (ORDER BY Data.CorpName DESC) as Num,Data.CorpName,BCC.CorpId,ModifiedMoney,BalanceMoney,PaymentMoney,NoPaymentMoney,Amount \n", new object[0]);
            builder.AppendFormat(" FROM( \n", new object[0]);
            builder.AppendFormat(" SELECT ISNULL(SUM(A.ModifiedMoney),0.00) AS ModifiedMoney,ISNULL(SUM(A.BalanceMoney),0.00) AS BalanceMoney,\n", new object[0]);
            builder.AppendFormat(" ISNULL(SUM(B.PaymentMoney),0.00) AS PaymentMoney,ISNULL(ISNULL(SUM(A.BalanceMoney),0.00)-ISNULL(SUM(B.PaymentMoney),0.00),0.00) AS NoPaymentMoney, \n", new object[0]);
            builder.AppendFormat(" ISNULL(SUM(C.Amount),0.00) AS Amount,A.CorpName \n", new object[0]);
            builder.AppendFormat(" from( \n", new object[0]);
            builder.AppendFormat(" SELECT C.ContractID,CorpId,ModifiedMoney,SUM(BalanceMoney) AS BalanceMoney,ISNULL(CorpName,BName) AS CorpName,\n", new object[0]);
            builder.AppendFormat(" C.UserCodes AS UserCodes \n", new object[0]);
            builder.AppendFormat(" FROM Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN Con_Payout_Balance AS CPB ON C.ContractID=CPB.ContractID AND CPB.FlowState=1\n", new object[0]);
            builder.AppendFormat(" LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))\n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" GROUP BY CorpName,CorpId,UserCodes,BName,C.ContractID,ModifiedMoney)\n", new object[0]);
            builder.AppendFormat(" AS A,\n", new object[0]);
            builder.AppendFormat(" (SELECT C.ContractID,CorpId,ModifiedMoney,SUM(PaymentMoney) AS PaymentMoney,ISNULL(CorpName,BName) AS CorpName,\n", new object[0]);
            builder.AppendFormat(" C.UserCodes AS UserCodes \n", new object[0]);
            builder.AppendFormat(" FROM Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Payment AS CPP ON C.ContractID=CPP.ContractID AND CPP.FlowState=1\n", new object[0]);
            builder.AppendFormat(" LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))\n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" GROUP BY CorpName,CorpId,UserCodes,BName,C.ContractID,ModifiedMoney)\n", new object[0]);
            builder.AppendFormat(" AS B,\n", new object[0]);
            builder.AppendFormat(" (SELECT C.ContractID,CorpId,ModifiedMoney,SUM(Amount) AS Amount,ISNULL(CorpName,BName) AS CorpName,C.UserCodes AS UserCodes \n", new object[0]);
            builder.AppendFormat(" FROM Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Invoice AS CPI ON C.ContractID=CPI.ContractID \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))\n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" GROUP BY CorpName,CorpId,UserCodes,BName,C.ContractID,ModifiedMoney)\n", new object[0]);
            builder.AppendFormat(" AS C \n", new object[0]);
            builder.AppendFormat(" WHERE A.ContractID=B.ContractID AND A.ContractID=C.ContractID \n", new object[0]);
            builder.AppendFormat(" GROUP BY A.CorpName) AS Data join XPM_Basic_ContactCorp AS BCC ON BCC.CorpName=Data.CorpName", new object[0]);
            if (!string.IsNullOrEmpty(strBName))
            {
                builder.AppendFormat(" WHERE Data.CorpName like '%{0}%'", strBName);
            }
            builder.AppendFormat(") AS T", new object[0]);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetContractReport(string strCorpName, string userCode, string strContractCode, string strContractName, string prjName, string contractType, string signStartDate, string signEndDate, int pageIndex, int pageSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP (" + pageSize + ") * FROM \n", new object[0]);
            builder.AppendFormat("( \n", new object[0]);
            builder.AppendFormat("SELECT Row_Number() OVER (ORDER BY A.ContractCode DESC) as Num,A.ContractID,A.ContractCode,A.ContractName,Prj.PrjName,A.SignDate,D.TypeName,\n", new object[0]);
            builder.AppendFormat(" ISNULL(A.ModifiedMoney,0.00) AS ModifiedMoney,ISNULL(BalanceMoney,0.00) AS BalanceMoney, \n", new object[0]);
            builder.AppendFormat(" ISNULL(B.PaymentMoney,0.00) AS PaymentMoney,ISNULL((ISnull(A.BalanceMoney,0.00)- ISNULL(B.PaymentMoney,0.00)),0.00) AS NoPaymentMoney,\n", new object[0]);
            builder.AppendFormat(" ISNULL(C.Amount,0.00) AS Amount,A.IsMainContract,A.UserCodes,A.CorpName \n", new object[0]);
            builder.AppendFormat(" FROM ( \n", new object[0]);
            builder.AppendFormat(" SELECT C.ContractID,C.ContractCode,C.ContractName,C.ModifiedMoney,sum(BalanceMoney) AS BalanceMoney,\n", new object[0]);
            builder.AppendFormat(" IsMainContract,C.UserCodes,ISNULL(CorpName,BName) AS CorpName,C.TypeId,C.SignDate,C.PrjGuid \n", new object[0]);
            builder.AppendFormat(" FROM dbo.Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Balance AS CPB ON C.ContractID=CPB.ContractID AND CPB.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))\n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1\n", new object[0]);
            builder.AppendFormat(" GROUP BY C.ContractID,C.ContractCode,C.ContractName,C.ModifiedMoney,IsMainContract,UserCodes,BName,CorpName,TypeId,SignDate,PrjGuid)\n", new object[0]);
            builder.AppendFormat(" AS A,\n", new object[0]);
            builder.AppendFormat(" (SELECT C.ContractCode,C.ContractName,C.ModifiedMoney,sum(PaymentMoney) AS PaymentMoney,\n", new object[0]);
            builder.AppendFormat(" IsMainContract,C.UserCodes,ISNULL(CorpName,BName) AS CorpName \n", new object[0]);
            builder.AppendFormat(" FROM dbo.Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Payment AS CPP ON C.ContractID=CPP.ContractID AND CPP.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024)) \n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" GROUP BY C.ContractCode,C.ContractName,C.ModifiedMoney,IsMainContract,UserCodes,BName,CorpName)\n", new object[0]);
            builder.AppendFormat(" AS B,\n", new object[0]);
            builder.AppendFormat(" (SELECT C.ContractCode,C.ContractName,C.ModifiedMoney,sum(Amount) AS Amount,\n", new object[0]);
            builder.AppendFormat(" IsMainContract,C.UserCodes,ISNULL(CorpName,BName) AS CorpNam \n", new object[0]);
            builder.AppendFormat(" FROM dbo.Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Invoice AS CPI ON C.ContractID=CPI.ContractID \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))\n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" GROUP BY C.ContractCode,C.ContractName,C.ModifiedMoney,IsMainContract,UserCodes,BName,CorpName)\n", new object[0]);
            builder.AppendFormat(" AS C, \n", new object[0]);
            builder.AppendFormat(" dbo.Con_ContractType AS D,PT_PrjInfo AS Prj \n", new object[0]);
            builder.AppendFormat(" WHERE A.ContractCode=B.ContractCode AND A.ContractCode=C.ContractCode AND A.TypeId=D.TypeID AND A.PrjGuid=Prj.PrjGuid AND A.CorpName='{0}'", strCorpName);
            if (!string.IsNullOrEmpty(strContractCode))
            {
                builder.AppendFormat(" AND A.ContractCode like '%{0}%'", strContractCode);
            }
            if (!string.IsNullOrEmpty(strContractName))
            {
                builder.AppendFormat(" AND A.ContractName like '%{0}%'", strContractName);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat("AND A.UserCodes like '%{0}%'", userCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat("AND Prj.PrjName like '%{0}%'", prjName);
            }
            if (!string.IsNullOrEmpty(contractType))
            {
                builder.AppendFormat("AND D.TypeName like '%{0}%'", contractType);
            }
            if (!string.IsNullOrEmpty(signStartDate) && !string.IsNullOrEmpty(signEndDate))
            {
                builder.AppendFormat("AND A.SignDate between '{0}' And '{1}'", signStartDate, signEndDate);
            }
            else if (!string.IsNullOrEmpty(signStartDate))
            {
                builder.AppendFormat("AND A.SignDate >= '{0}'", signStartDate);
            }
            else if (!string.IsNullOrEmpty(signEndDate))
            {
                builder.AppendFormat("AND A.SignDate <= '{0}'", signEndDate);
            }
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetContractReportCount(string strCorpName, string userCode, string strContractCode, string strContractName, string prjName, string contractType, string signStartDate, string signEndDate)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (\n", new object[0]);
            builder.AppendFormat("SELECT Row_Number() OVER (ORDER BY A.ContractCode DESC) as Num,A.ContractCode,A.ContractName,Prj.PrjName,A.SignDate,D.TypeName,\n", new object[0]);
            builder.AppendFormat(" ISNULL(A.ModifiedMoney,0.00) AS ModifiedMoney,ISNULL(BalanceMoney,0.00) AS BalanceMoney,\n", new object[0]);
            builder.AppendFormat(" ISNULL(B.PaymentMoney,0.00) AS PaymentMoney,ISNULL((ISnull(A.BalanceMoney,0.00)- ISNULL(B.PaymentMoney,0.00)),0.00) AS NoPaymentMoney,\n", new object[0]);
            builder.AppendFormat(" ISNULL(C.Amount,0.00) AS Amount,A.IsMainContract,A.UserCodes,A.CorpName \n", new object[0]);
            builder.AppendFormat(" FROM ( \n", new object[0]);
            builder.AppendFormat(" SELECT C.ContractCode,C.ContractName,C.ModifiedMoney,sum(BalanceMoney) AS BalanceMoney,\n", new object[0]);
            builder.AppendFormat(" IsMainContract,C.UserCodes,ISNULL(CorpName,BName) AS CorpName,C.TypeId,C.SignDate,C.PrjGuid \n", new object[0]);
            builder.AppendFormat(" FROM dbo.Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Balance AS CPB ON C.ContractID=CPB.ContractID AND CPB.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))\n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1\n", new object[0]);
            builder.AppendFormat(" GROUP BY C.ContractCode,C.ContractName,C.ModifiedMoney,IsMainContract,UserCodes,BName,CorpName,TypeId,SignDate,PrjGuid)\n", new object[0]);
            builder.AppendFormat(" AS A,\n", new object[0]);
            builder.AppendFormat(" (SELECT C.ContractCode,C.ContractName,C.ModifiedMoney,sum(PaymentMoney) AS PaymentMoney,\n", new object[0]);
            builder.AppendFormat(" IsMainContract,C.UserCodes,ISNULL(CorpName,BName) AS CorpName \n", new object[0]);
            builder.AppendFormat(" FROM dbo.Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Payment AS CPP ON C.ContractID=CPP.ContractID AND CPP.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024)) \n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" GROUP BY C.ContractCode,C.ContractName,C.ModifiedMoney,IsMainContract,UserCodes,BName,CorpName)\n", new object[0]);
            builder.AppendFormat(" AS B,\n", new object[0]);
            builder.AppendFormat(" (SELECT C.ContractCode,C.ContractName,C.ModifiedMoney,sum(Amount) AS Amount,\n", new object[0]);
            builder.AppendFormat(" IsMainContract,C.UserCodes,ISNULL(CorpName,BName) AS CorpNam \n", new object[0]);
            builder.AppendFormat(" FROM dbo.Con_Payout_Contract AS C \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.Con_Payout_Invoice AS CPI ON C.ContractID=CPI.ContractID \n", new object[0]);
            builder.AppendFormat(" LEFT JOIN dbo.XPM_Basic_ContactCorp ON BName=CAST(CorpId AS NVARCHAR(1024))\n", new object[0]);
            builder.AppendFormat(" WHERE C.FlowState=1 \n", new object[0]);
            builder.AppendFormat(" GROUP BY C.ContractCode,C.ContractName,C.ModifiedMoney,IsMainContract,UserCodes,BName,CorpName)\n", new object[0]);
            builder.AppendFormat(" AS C, \n", new object[0]);
            builder.AppendFormat(" dbo.Con_ContractType AS D,PT_PrjInfo AS Prj \n", new object[0]);
            builder.AppendFormat(" WHERE A.ContractCode=B.ContractCode AND A.ContractCode=C.ContractCode AND A.TypeId=D.TypeID AND A.PrjGuid=Prj.PrjGuid AND A.CorpName='{0}'", strCorpName);
            if (!string.IsNullOrEmpty(strContractCode))
            {
                builder.AppendFormat(" AND A.ContractCode like '%{0}%'", strContractCode);
            }
            if (!string.IsNullOrEmpty(strContractName))
            {
                builder.AppendFormat(" AND A.ContractName like '%{0}%'", strContractName);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat("AND A.UserCodes like '%{0}%'", userCode);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat("AND Prj.PrjName like '%{0}%'", prjName);
            }
            if (!string.IsNullOrEmpty(contractType))
            {
                builder.AppendFormat("AND D.TypeName like '%{0}%'", contractType);
            }
            if (!string.IsNullOrEmpty(signStartDate) && !string.IsNullOrEmpty(signEndDate))
            {
                builder.AppendFormat("AND A.SignDate between '{0}' And '{1}'", signStartDate, signEndDate);
            }
            else if (!string.IsNullOrEmpty(signStartDate))
            {
                builder.AppendFormat("AND A.SignDate >= '{0}'", signStartDate);
            }
            else if (!string.IsNullOrEmpty(signEndDate))
            {
                builder.AppendFormat("AND A.SignDate <= '{0}'", signEndDate);
            }
            builder.AppendFormat(") AS T", new object[0]);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetIncometContractReport(string userCode, string strContractCode, string strContractName, int pageIndex, int pageSize, string strTypeName, string strPrjName, string strParty, string strSignName, string strSignedStartTime, string strSignedEndTime)
        {
            StringBuilder builder = new StringBuilder();
            if (pageSize > 0)
            {
                builder.AppendFormat("SELECT TOP (" + pageSize + ") * FROM \n", new object[0]);
            }
            else
            {
                builder.AppendFormat("SELECT  * FROM \n", new object[0]);
            }
            builder.AppendFormat("( \n", new object[0]);
            builder.AppendFormat("SELECT Row_Number() OVER (ORDER BY Con.ContractID DESC) as Num,Con.ContractID,Con.ContractCode,Con.ContractName,Con.TypeName,Con.PrjName,Con.TypeID,Con.PrjGuid,Con.EndPrice,\n", new object[0]);
            builder.AppendFormat("IncB.ClearingPrice,IncP.CllectionPrice,ISNULL((IncB.ClearingPrice-IncP.CllectionPrice),0) AS NOCllectionPrice,\n", new object[0]);
            builder.AppendFormat("Con.isFContract,Con.UserCodes,Con.Party,Con.SignName,Con.SignedTime \n", new object[0]);
            builder.AppendFormat("FROM( \n", new object[0]);
            builder.AppendFormat("SELECT IncContract.ContractID,IncContract.ContractCode,IncContract.ContractName,ConT.TypeName,Prj.PrjName,\n", new object[0]);
            builder.AppendFormat("ConT.TypeID,Prj.PrjGuid,\n", new object[0]);
            builder.AppendFormat("ISNULL(IncContract.ContractPrice+isnull((select sum(ChangePrice) from  dbo.Con_Incomet_Modify where ContractId=IncContract.ContractId GROUP BY ContractID),0),0) as EndPrice, \n", new object[0]);
            builder.AppendFormat("IncContract.isFContract,IncContract.UserCodes,IncContract.Party,IncContract.SignPeople,Staff.v_xm AS SignName,IncContract.SignedTime \n", new object[0]);
            builder.AppendFormat("FROM Con_Incomet_Contract AS IncContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN Con_ContractType AS ConT ON IncContract.TypeID=ConT.TypeID \n", new object[0]);
            builder.AppendFormat("LEFT JOIN PT_PrjInfo AS Prj ON IncContract.Project=Prj.PrjGuid \n", new object[0]);
            builder.AppendFormat("LEFT JOIN PT_yhmc AS Staff ON IncContract.SignPeople=Staff.v_yhdm \n", new object[0]);
            if (this.IsIncomeContractApprove == "1")
            {
                builder.AppendFormat("WHERE IncContract.FlowState=1 \n", new object[0]);
            }
            builder.AppendFormat(") AS Con,\n", new object[0]);
            builder.AppendFormat("(SELECT IncContract.ContractID,IncContract.ContractCode,ISNULL(sum(ClearingPrice),0) AS ClearingPrice \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Incomet_Contract AS IncContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Incomet_Balance AS IncBmoney ON IncContract.ContractID=IncBmoney.ContractID \n", new object[0]);
            if (this.IsIncomeContractApprove == "1")
            {
                builder.AppendFormat("WHERE IncContract.FlowState=1 \n", new object[0]);
            }
            builder.AppendFormat("GROUP BY IncContract.ContractCode,IncContract.ContractID) AS IncB,\n", new object[0]);
            builder.AppendFormat("(SELECT IncContract.ContractID,IncContract.ContractCode,ISNULL(sum(CllectionPrice),0) AS CllectionPrice \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Incomet_Contract AS IncContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Incomet_Payment AS IncPmoney ON IncContract.ContractID=IncPmoney.ContractID \n", new object[0]);
            if (this.IsIncomeContractApprove == "1")
            {
                builder.AppendFormat("WHERE IncContract.FlowState=1 \n", new object[0]);
            }
            builder.AppendFormat("GROUP BY IncContract.ContractCode,IncContract.ContractID)IncP \n", new object[0]);
            builder.AppendFormat("WHERE Con.ContractID=IncB.ContractID AND Con.ContractID=IncP.ContractID \n", new object[0]);
            if (!string.IsNullOrEmpty(strContractCode))
            {
                builder.AppendFormat(" AND Con.ContractCode like '%{0}%'", strContractCode);
            }
            if (!string.IsNullOrEmpty(strContractName))
            {
                builder.AppendFormat(" AND Con.ContractName like '%{0}%'", strContractName);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat("AND Con.UserCodes like '%{0}%'", userCode);
            }
            if (!string.IsNullOrEmpty(strTypeName))
            {
                builder.AppendFormat("AND Con.TypeName like '%{0}%'", strTypeName);
            }
            if (!string.IsNullOrEmpty(strPrjName))
            {
                builder.AppendFormat("AND Con.PrjName like '%{0}%'", strPrjName);
            }
            if (!string.IsNullOrEmpty(strParty))
            {
                builder.AppendFormat("AND Con.Party like '%{0}%'", strParty);
            }
            if (!string.IsNullOrEmpty(strSignName))
            {
                builder.AppendFormat("AND Con.SignName like '%{0}%'", strSignName);
            }
            if (!string.IsNullOrEmpty(strSignedStartTime) && !string.IsNullOrEmpty(strSignedEndTime))
            {
                builder.AppendFormat("AND Con.SignedTime BETWEEN '{0}' AND '{1}'", strSignedStartTime, strSignedEndTime);
            }
            else if (!string.IsNullOrEmpty(strSignedStartTime))
            {
                builder.AppendFormat("AND Con.SignedTime >= '{0}'", strSignedStartTime);
            }
            else if (!string.IsNullOrEmpty(strSignedEndTime))
            {
                builder.AppendFormat("AND Con.SignedTime <= '{0}'", strSignedEndTime);
            }
            builder.Append(") T \n");
            if ((pageSize > 0) && (pageIndex >= 0))
            {
                builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetIncometContractReportCount(string userCode, string strContractCode, string strContractName, string strTypeName, string strPrjName, string strParty, string strSignName, string strSignedStartTime, string strSignedEndTime)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (\n", new object[0]);
            builder.AppendFormat("SELECT Con.ContractID,Con.ContractCode,Con.ContractName,Con.TypeName,Con.PrjName,Con.TypeID,Con.PrjGuid,Con.EndPrice,\n", new object[0]);
            builder.AppendFormat("IncB.ClearingPrice,IncP.CllectionPrice,ISNULL((IncB.ClearingPrice-IncP.CllectionPrice),0) AS NOCllectionPrice,\n", new object[0]);
            builder.AppendFormat("Con.isFContract,Con.UserCodes,Con.Party,Con.SignName,Con.SignedTime \n", new object[0]);
            builder.AppendFormat("FROM( \n", new object[0]);
            builder.AppendFormat("SELECT IncContract.ContractID,IncContract.ContractCode,IncContract.ContractName,ConT.TypeName,Prj.PrjName,\n", new object[0]);
            builder.AppendFormat("ConT.TypeID,Prj.PrjGuid,\n", new object[0]);
            builder.AppendFormat("ISNULL(IncContract.ContractPrice+isnull((select sum(ChangePrice) from  dbo.Con_Incomet_Modify where ContractId=IncContract.ContractId GROUP BY ContractID),0),0) as EndPrice, \n", new object[0]);
            builder.AppendFormat("IncContract.isFContract,IncContract.UserCodes,IncContract.Party,IncContract.SignPeople,Staff.v_xm AS SignName,IncContract.SignedTime \n", new object[0]);
            builder.AppendFormat("FROM Con_Incomet_Contract AS IncContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN Con_ContractType AS ConT ON IncContract.TypeID=ConT.TypeID \n", new object[0]);
            builder.AppendFormat("LEFT JOIN PT_PrjInfo AS Prj ON IncContract.Project=Prj.PrjGuid \n", new object[0]);
            builder.AppendFormat("LEFT JOIN PT_yhmc AS Staff ON IncContract.SignPeople=Staff.v_yhdm \n", new object[0]);
            if (this.IsIncomeContractApprove == "1")
            {
                builder.AppendFormat("WHERE IncContract.FlowState=1 \n", new object[0]);
            }
            builder.AppendFormat(") AS Con,\n", new object[0]);
            builder.AppendFormat("(SELECT IncContract.ContractID,IncContract.ContractCode,ISNULL(sum(ClearingPrice),0) AS ClearingPrice \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Incomet_Contract AS IncContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Incomet_Balance AS IncBmoney ON IncContract.ContractID=IncBmoney.ContractID \n", new object[0]);
            if (this.IsIncomeContractApprove == "1")
            {
                builder.AppendFormat("WHERE IncContract.FlowState=1 \n", new object[0]);
            }
            builder.AppendFormat("GROUP BY IncContract.ContractCode,IncContract.ContractID) AS IncB,\n", new object[0]);
            builder.AppendFormat("(SELECT IncContract.ContractID,IncContract.ContractCode,ISNULL(sum(CllectionPrice),0) AS CllectionPrice \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Incomet_Contract AS IncContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Incomet_Payment AS IncPmoney ON IncContract.ContractID=IncPmoney.ContractID \n", new object[0]);
            if (this.IsIncomeContractApprove == "1")
            {
                builder.AppendFormat("WHERE IncContract.FlowState=1 \n", new object[0]);
            }
            builder.AppendFormat("GROUP BY IncContract.ContractCode,IncContract.ContractID)IncP \n", new object[0]);
            builder.AppendFormat("WHERE Con.ContractID=IncB.ContractID AND Con.ContractID=IncP.ContractID \n", new object[0]);
            if (!string.IsNullOrEmpty(strContractCode))
            {
                builder.AppendFormat(" AND Con.ContractCode like '%{0}%'", strContractCode);
            }
            if (!string.IsNullOrEmpty(strContractName))
            {
                builder.AppendFormat(" AND Con.ContractName like '%{0}%'", strContractName);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat("AND Con.UserCodes like '%{0}%'", userCode);
            }
            if (!string.IsNullOrEmpty(strTypeName))
            {
                builder.AppendFormat("AND Con.TypeName like '%{0}%'", strTypeName);
            }
            if (!string.IsNullOrEmpty(strPrjName))
            {
                builder.AppendFormat("AND Con.PrjName like '%{0}%'", strPrjName);
            }
            if (!string.IsNullOrEmpty(strParty))
            {
                builder.AppendFormat("AND Con.Party like '%{0}%'", strParty);
            }
            if (!string.IsNullOrEmpty(strSignName))
            {
                builder.AppendFormat("AND Con.SignName like '%{0}%'", strSignName);
            }
            if (!string.IsNullOrEmpty(strSignedStartTime) && !string.IsNullOrEmpty(strSignedEndTime))
            {
                builder.AppendFormat("AND Con.SignedTime BETWEEN '{0}' AND '{1}'", strSignedStartTime, strSignedEndTime);
            }
            else if (!string.IsNullOrEmpty(strSignedStartTime))
            {
                builder.AppendFormat("AND Con.SignedTime >= '{0}'", strSignedStartTime);
            }
            else if (!string.IsNullOrEmpty(strSignedEndTime))
            {
                builder.AppendFormat("AND Con.SignedTime <= '{0}'", strSignedEndTime);
            }
            builder.AppendFormat(") AS T", new object[0]);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public List<PayoutContractModel> GetLedger(string userCode)
        {
            string strWhere = " Con_Payout_Contract.IsArchived = 0 AND FlowState = 1 ";
            return this.GetList(strWhere, userCode);
        }

        public DataTable GetLedger(string userCode, int pageIndex, int pageSize)
        {
            string strWhere = " Con_Payout_Contract.IsArchived = 0 AND FlowState = 1 ";
            return this.GetList(strWhere, userCode, pageIndex, pageSize);
        }

        public List<PayoutContractModel> GetList(string strWhere)
        {
            return this.payoutContract.GetList(strWhere);
        }

        public List<PayoutContractModel> GetList(string strWhere, string userCode)
        {
            return this.payoutContract.GetList(strWhere, userCode);
        }

        public DataTable GetList(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            return this.payoutContract.GetList(strWhere, userCode, pageIndex, pageSize);
        }

        public DataTable GetList_New(string strWhere, string userCode)
        {
            return this.payoutContract.GetList_New(strWhere);
        }

        public PayoutContractModel GetModel(string ContractID)
        {
            return this.payoutContract.GetModel(ContractID);
        }

        public DataTable GetPayoutContractReport(string userCode, string strContractCode, string strContractName, int pageIndex, int pageSize, string strTypeName, string strPrjName)
        {
            StringBuilder builder = new StringBuilder();
            if (pageSize > 0)
            {
                builder.AppendFormat("SELECT TOP (" + pageSize + ") * FROM \n", new object[0]);
            }
            else
            {
                builder.AppendFormat("SELECT  * FROM \n", new object[0]);
            }
            builder.AppendFormat("( \n", new object[0]);
            builder.AppendFormat("SELECT Row_Number() OVER (ORDER BY Con.ContractID DESC) as Num,Con.ContractID,Con.ContractCode,Con.ContractName,Con.TypeName,Con.PrjName,Con.TypeID,Con.PrjGuid,Con.ModifiedMoney,\n", new object[0]);
            builder.AppendFormat("Con.IsMainContract,Con.UserCodes,PayB.BalanceMoney,PayP.PaymentMoney,ISNULL((PayB.BalanceMoney-PayP.PaymentMoney),0) AS NoPaymentMoney \n", new object[0]);
            builder.AppendFormat("FROM ( \n", new object[0]);
            builder.AppendFormat("SELECT PayContract.ContractID,PayContract.ContractCode,PayContract.ContractName,ConT.TypeName,Prj.PrjName,\n", new object[0]);
            builder.AppendFormat("ConT.TypeID,Prj.PrjGuid,\n", new object[0]);
            builder.AppendFormat("ISNULL(PayContract.ModifiedMoney,0) AS ModifiedMoney,PayContract.IsMainContract,PayContract.UserCodes \n", new object[0]);
            builder.AppendFormat("FROM Con_Payout_Contract AS PayContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN Con_ContractType AS ConT ON PayContract.TypeID=ConT.TypeID \n", new object[0]);
            builder.AppendFormat("LEFT JOIN PT_PrjInfo AS Prj ON PayContract.PrjGuid=Prj.PrjGuid \n", new object[0]);
            builder.AppendFormat("WHERE PayContract.FlowState=1) AS Con,\n", new object[0]);
            builder.AppendFormat("(SELECT PayContract.ContractID,PayContract.ContractCode,ISNULL(sum(BalanceMoney),0) AS BalanceMoney \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Payout_Contract AS PayContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Payout_Balance AS PayBmoney ON PayContract.ContractID=PayBmoney.ContractID AND PayBmoney.FlowState=1 \n", new object[0]);
            builder.AppendFormat("WHERE PayContract.FlowState=1 \n", new object[0]);
            builder.AppendFormat("GROUP BY PayContract.ContractCode,PayContract.ContractID) AS PayB,\n", new object[0]);
            builder.AppendFormat("(SELECT PayContract.ContractID,PayContract.ContractCode,ISNULL(sum(PaymentMoney),0) AS PaymentMoney \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Payout_Contract AS PayContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Payout_Payment AS PayPmoney ON PayContract.ContractID=PayPmoney.ContractID AND PayPmoney.FlowState=1 \n", new object[0]);
            builder.AppendFormat("WHERE PayContract.FlowState=1 \n", new object[0]);
            builder.AppendFormat("GROUP BY PayContract.ContractCode,PayContract.ContractID) AS PayP \n", new object[0]);
            builder.AppendFormat("WHERE Con.ContractID=PayB.ContractID AND Con.ContractID=PayP.ContractID \n", new object[0]);
            if (!string.IsNullOrEmpty(strContractCode))
            {
                builder.AppendFormat(" AND Con.ContractCode like '%{0}%'", strContractCode);
            }
            if (!string.IsNullOrEmpty(strContractName))
            {
                builder.AppendFormat(" AND Con.ContractName like '%{0}%'", strContractName);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat("AND Con.UserCodes like '%{0}%'", userCode);
            }
            if (!string.IsNullOrEmpty(strTypeName))
            {
                builder.AppendFormat("AND Con.TypeName like '%{0}%'", strTypeName);
            }
            if (!string.IsNullOrEmpty(strPrjName))
            {
                builder.AppendFormat("AND Con.PrjName like '%{0}%'", strPrjName);
            }
            builder.Append(") T \n");
            if ((pageSize > 0) && (pageIndex > 0))
            {
                builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetPayoutContractReportCount(string userCode, string strContractCode, string strContractName, string strtypeName, string strPrjName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (\n", new object[0]);
            builder.AppendFormat("SELECT Con.ContractID,Con.ContractCode,Con.ContractName,Con.TypeName,Con.PrjName,Con.TypeID,Con.PrjGuid,Con.ModifiedMoney,\n", new object[0]);
            builder.AppendFormat("Con.IsMainContract,Con.UserCodes,PayB.BalanceMoney,PayP.PaymentMoney,ISNULL((PayB.BalanceMoney-PayP.PaymentMoney),0) AS NoPaymentMoney \n", new object[0]);
            builder.AppendFormat("FROM ( \n", new object[0]);
            builder.AppendFormat("SELECT PayContract.ContractID,PayContract.ContractCode,PayContract.ContractName,ConT.TypeName,Prj.PrjName,\n", new object[0]);
            builder.AppendFormat("ConT.TypeID,Prj.PrjGuid,\n", new object[0]);
            builder.AppendFormat("ISNULL(PayContract.ModifiedMoney,0) AS ModifiedMoney,PayContract.IsMainContract,PayContract.UserCodes \n", new object[0]);
            builder.AppendFormat("FROM Con_Payout_Contract AS PayContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN Con_ContractType AS ConT ON PayContract.TypeID=ConT.TypeID \n", new object[0]);
            builder.AppendFormat("LEFT JOIN PT_PrjInfo AS Prj ON PayContract.PrjGuid=Prj.PrjGuid \n", new object[0]);
            builder.AppendFormat("WHERE PayContract.FlowState=1) AS Con,\n", new object[0]);
            builder.AppendFormat("(SELECT PayContract.ContractID,PayContract.ContractCode,ISNULL(sum(BalanceMoney),0) AS BalanceMoney \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Payout_Contract AS PayContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Payout_Balance AS PayBmoney ON PayContract.ContractID=PayBmoney.ContractID AND PayBmoney.FlowState=1 \n", new object[0]);
            builder.AppendFormat("WHERE PayContract.FlowState=1 \n", new object[0]);
            builder.AppendFormat("GROUP BY PayContract.ContractCode,PayContract.ContractID) AS PayB,\n", new object[0]);
            builder.AppendFormat("(SELECT PayContract.ContractID,PayContract.ContractCode,ISNULL(sum(PaymentMoney),0) AS PaymentMoney \n", new object[0]);
            builder.AppendFormat("FROM dbo.Con_Payout_Contract AS PayContract \n", new object[0]);
            builder.AppendFormat("LEFT JOIN dbo.Con_Payout_Payment AS PayPmoney ON PayContract.ContractID=PayPmoney.ContractID AND PayPmoney.FlowState=1 \n", new object[0]);
            builder.AppendFormat("WHERE PayContract.FlowState=1 \n", new object[0]);
            builder.AppendFormat("GROUP BY PayContract.ContractCode,PayContract.ContractID) AS PayP \n", new object[0]);
            builder.AppendFormat("WHERE Con.ContractID=PayB.ContractID AND Con.ContractID=PayP.ContractID \n", new object[0]);
            if (!string.IsNullOrEmpty(strContractCode))
            {
                builder.AppendFormat(" AND Con.ContractCode like '%{0}%'", strContractCode);
            }
            if (!string.IsNullOrEmpty(strContractName))
            {
                builder.AppendFormat(" AND Con.ContractName like '%{0}%'", strContractName);
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat("AND Con.UserCodes like '%{0}%'", userCode);
            }
            if (!string.IsNullOrEmpty(strtypeName))
            {
                builder.AppendFormat("AND Con.TypeName like '%{0}%'", strtypeName);
            }
            if (!string.IsNullOrEmpty(strPrjName))
            {
                builder.AppendFormat("AND Con.PrjName like '%{0}%'", strPrjName);
            }
            builder.AppendFormat(") AS T", new object[0]);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public string GetReportCondition(DateTime startDate, DateTime endDate, string typeName, string prjName, string name, string code, string b, string prjTypeCode, string signPersonName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" Con_Payout_Contract.SignDate between '{0}' and '{1}' \n", startDate.ToShortDateString(), endDate.ToShortDateString());
            if (!string.IsNullOrEmpty(typeName))
            {
                builder.AppendFormat(" AND Con_ContractType.TypeName LIKE '%{0}%' \n", typeName);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat(" AND ISNULL(PT_PrjInfo.PrjName,PT_PrjInfo_ZTB.PrjName) LIKE '%{0}%' \n", prjName);
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.AppendFormat(" AND Con_Payout_Contract.ContractName like '%{0}%' \n", name);
            }
            if (!string.IsNullOrEmpty(code))
            {
                builder.AppendFormat(" AND Con_Payout_Contract.ContractCode like '%{0}%' \n", code);
            }
            if (!string.IsNullOrEmpty(b))
            {
                builder.AppendFormat(" AND (Con_Payout_Contract.BName like '%{0}%' OR CorpName like '%{0}%') \n", b);
            }
            if (!string.IsNullOrEmpty(prjTypeCode))
            {
                builder.AppendFormat(" AND PrjType.CodeName like '%{0}%' \n", prjTypeCode);
            }
            if (!string.IsNullOrEmpty(signPersonName))
            {
                builder.AppendFormat(" AND YH.v_xm lIKE '%{0}%' \n", signPersonName);
            }
            return builder.ToString();
        }

        public DataTable GetReportData(string strWhere, string userCode)
        {
            return this.payoutContract.GetReportData(strWhere, userCode);
        }

        public DataTable GetReportData(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            return this.payoutContract.GetReportDataSource(strWhere, userCode, pageIndex, pageSize);
        }

        private StringBuilder GetSelectArchivesCondition(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project)
        {
            StringBuilder builder = this.GetSelectListCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            builder.Append(" AND Con_Payout_Contract.IsArchived = 1 ");
            return builder;
        }

        private StringBuilder GetSelectLedgerCondition(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project)
        {
            StringBuilder builder = this.GetSelectListCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            builder.Append(" AND Con_Payout_Contract.IsArchived = 0 ");
            return builder;
        }

        private StringBuilder GetSelectLedgerWhere(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string where)
        {
            StringBuilder builder = this.GetSelectListCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            builder.Append("  " + where);
            return builder;
        }

        private StringBuilder GetSelectListCondition(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Con_Payout_Contract.SignDate BETWEEN '").Append(startDate.ToShortDateString()).Append("' ");
            builder.Append(" AND '").Append(endDate.ToShortDateString()).Append("' ");
            builder.Append(" AND ModifiedMoney BETWEEN '").Append(startMoney.ToString()).Append("' ");
            builder.Append(" AND '").Append(endMoney.ToString()).Append("' ");
            if (!string.IsNullOrEmpty(contractCode))
            {
                builder.Append(" AND ContractCode LIKE '%").Append(contractCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(contractName))
            {
                builder.Append(" AND ContractName LIKE '%").Append(contractName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(contractType))
            {
                builder.Append(" AND Con_ContractType.TypeName LIKE '%").Append(contractType).Append("%' ");
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append(" AND PT_PrjInfo.PrjName like '%").Append(project).Append("%' ");
            }
            return builder;
        }

        public bool IsReferenced(string contractID)
        {
            return this.payoutContract.IsReferenced(contractID);
        }

        public List<PayoutContractModel> SelCon(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode)
        {
            accountMange mange = new accountMange();
            StringBuilder builder = this.GetSelectLedgerCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            return mange.GetConNoList(builder.ToString(), userCode);
        }

        public List<PayoutContractModel> SelConState(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode)
        {
            accountMange mange = new accountMange();
            StringBuilder builder = this.GetSelectLedgerCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            builder.Append(" and flowstate=1");
            return mange.GetConNoList(builder.ToString(), userCode);
        }

        public DataTable Select(string condition, string userCode)
        {
            return this.GetReportData(condition, userCode);
        }

        public List<PayoutContractModel> Select(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Con_Payout_Contract.SignDate BETWEEN '").Append(startDate.ToShortDateString()).Append("' ");
            builder.Append(" AND '").Append(endDate.ToShortDateString()).Append("' ");
            builder.Append(" AND ContractMoney BETWEEN '").Append(startMoney.ToString()).Append("' ");
            builder.Append(" AND '").Append(endMoney.ToString()).Append("' ");
            if (!string.IsNullOrEmpty(contractCode))
            {
                builder.Append(" AND ContractCode LIKE '%").Append(contractCode).Append("%' ");
            }
            if (!string.IsNullOrEmpty(contractName))
            {
                builder.Append(" AND ContractName LIKE '%").Append(contractName).Append("%' ");
            }
            if (!string.IsNullOrEmpty(contractType))
            {
                builder.Append(" AND Con_Payout_Contract.TypeID = '").Append(contractType).Append("' ");
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append(" AND Con_Payout_Contract.PrjGuid = '").Append(project).Append("' ");
            }
            return this.GetList(builder.ToString(), userCode);
        }

        public List<PayoutContractModel> SelectAndLedgerWhere(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode, string where)
        {
            StringBuilder builder = this.GetSelectLedgerWhere(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project, where);
            return this.GetList(builder.ToString(), userCode);
        }

        public List<PayoutContractModel> SelectArchives(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode)
        {
            StringBuilder builder = this.GetSelectArchivesCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            return this.GetList(builder.ToString(), userCode);
        }

        public DataTable SelectArchives(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode, int pageIndex, int pageSize)
        {
            StringBuilder builder = this.GetSelectArchivesCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            return this.GetList(builder.ToString(), userCode, pageIndex, pageSize);
        }

        public DataTable SelectData(string condition, string userCode, int pageIndex, int pageSize)
        {
            return this.GetReportData(condition, userCode, pageIndex, pageSize);
        }

        public List<PayoutContractModel> SelectLedger(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode)
        {
            StringBuilder builder = this.GetSelectLedgerCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            return this.GetList(builder.ToString(), userCode);
        }

        public List<PayoutContractModel> SelectLedger(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode, string condition)
        {
            StringBuilder builder = this.GetSelectLedgerCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            builder.Append(condition);
            return this.GetList(builder.ToString(), userCode);
        }

        public DataTable SelectLedger(DateTime startDate, DateTime endDate, decimal startMoney, decimal endMoney, string contractCode, string contractName, string contractType, string project, string userCode, string condition, int pageIndex, int pageSize)
        {
            StringBuilder builder = this.GetSelectLedgerCondition(startDate, endDate, startMoney, endMoney, contractCode, contractName, contractType, project);
            builder.Append(condition);
            return this.GetList(builder.ToString(), userCode, pageIndex, pageSize);
        }

        private List<PayoutContractModel> Sort_(List<PayoutContractModel> contracts)
        {
            List<PayoutContractModel> list = contracts.FindAll(contract => contract.IsMainContract);
            List<PayoutContractModel> lst = new List<PayoutContractModel>();
            list.ForEach(delegate (PayoutContractModel contract) {
                lst.Add(contract);
                List<PayoutContractModel> collection = contracts.FindAll(con => con.MainContractID == contract.ContractID);
                if (collection.Count > 0)
                {
                    lst.AddRange(collection);
                }
            });
            return lst;
        }

        public void Update(PayoutContractModel model, SqlTransaction trans)
        {
            this.payoutContract.Update(model, trans);
        }

        public void updateConState(List<string> contractIds, int state)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (string str in contractIds)
                    {
                        this.payoutContract.UpdateState(str, state);
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("状态修改失败");
                }
            }
        }
    }
}

