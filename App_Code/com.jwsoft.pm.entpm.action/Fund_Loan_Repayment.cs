namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class Fund_Loan_Repayment
    {
        public DataTable GetRepaymentInfoByLLoanID(string loanid)
        {
            return publicDbOpClass.DataTableQuary("SELECT *, (SELECT top 1 v_xm FROM PT_yhmc WHERE v_yhdm=a.LoanMan) AS LoanManName, (SELECT  PrjName FROM PT_PrjInfo  WHERE PrjGuid=a.PrjGuid) AS PrjName  ,datediff(d,getdate(),PlanReturnDate) as syday  ,(SELECT top 1 v_xm FROM PT_yhmc WHERE v_yhdm=a.Returnteller) AS ReturntellerName  FROM Fund_Prj_Loan a  WHERE loanid='" + loanid + "' and flowstate=1   ORDER BY PlanReturnDate DESC");
        }

        public DataTable GetRepaymentInfoByPrjGuid(string _PrjGuid)
        {
            return publicDbOpClass.DataTableQuary("SELECT *, (SELECT top 1 v_xm FROM PT_yhmc WHERE v_yhdm=a.LoanMan) AS LoanManName, (SELECT  PrjName FROM PT_PrjInfo  WHERE PrjGuid=a.PrjGuid) AS PrjName  ,datediff(d,getdate(),PlanReturnDate) as syday  ,(SELECT top 1 v_xm FROM PT_yhmc WHERE v_yhdm=a.Returnteller) AS ReturntellerName  FROM Fund_Prj_Loan a  WHERE PrjGuid='" + _PrjGuid + "' and flowstate=1   ORDER BY PlanReturnDate DESC");
        }

        public int SetLoanRepayment(string loanid, string usercode)
        {
            return publicDbOpClass.ExecSqlString("UPDATE [Fund_Prj_Loan]   SET  [ReturnDate] = getdate()   ,[ReturnMan] =''   ,[Returnteller] = '" + usercode + "'    ,[ReturnFlowState] = 1  WHERE LoanID='" + loanid + "' ");
        }

        public string SetStateColor(string State)
        {
            switch (State)
            {
                case "已还款":
                    return "#008B45";

                case "逾期未还":
                    return "#ff0000";

                case "已到还款期限":
                    return "#030310";

                case "快还款期限，请注意提醒":
                    return "#914229";
            }
            return "#050505";
        }

        public string SetStatets(int syday, string ReturnState)
        {
            if (ReturnState == "1")
            {
                return "已还款";
            }
            if (syday < 0)
            {
                return "逾期未还";
            }
            if (syday == 0)
            {
                return "已到还款期限";
            }
            if ((syday > 0) && (syday < 10))
            {
                return "快还款期限，请注意提醒";
            }
            return "";
        }
    }
}

