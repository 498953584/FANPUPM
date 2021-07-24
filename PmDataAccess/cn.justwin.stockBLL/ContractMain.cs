namespace cn.justwin.stockBLL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ContractMain
    {
        public ContractMainModel GetModel(string contractCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,ProjectCode,ContractType,ContractCode,ContractName,SignAddr,SignDate,PayMode,SumMoney,BeginDate,EndDate,WorkDay,MainWork,Remark,ContractState,ContractOther,ContractOtherAddr,WorkAddr,QualitySafety,ContractOwner,ContractOwnerAddr,ContractOwnerMan,ContractOwnerSpokesman,ContractOwnerTel,ContractOwnerFax,ContractOwnerZipcode,ContractOwnerBack,ContractOwnerAccount,ContractOtherMan,ContractOtherSpokesman,ContractOtherTel,ContractOtherFax,ContractOtherZipcode,ContractOtherBack,ContractOtherAccount,SumBalance,BalanceRemark,FlowGuid,AuditState,FromType from EPM_Con_ContractMain ");
            builder.Append(" where ContractCode=@ContractCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractCode", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = contractCode;
            ContractMainModel model = new ContractMainModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["ID"].ToString() != "")
            {
                model.ID = int.Parse(table.Rows[0]["ID"].ToString());
            }
            if (table.Rows[0]["ProjectCode"].ToString() != "")
            {
                model.ProjectCode = new Guid(table.Rows[0]["ProjectCode"].ToString());
            }
            model.ContractType = table.Rows[0]["ContractType"].ToString();
            model.ContractCode = table.Rows[0]["ContractCode"].ToString();
            model.ContractName = table.Rows[0]["ContractName"].ToString();
            model.SignAddr = table.Rows[0]["SignAddr"].ToString();
            if (table.Rows[0]["SignDate"].ToString() != "")
            {
                model.SignDate = new DateTime?(DateTime.Parse(table.Rows[0]["SignDate"].ToString()));
            }
            model.PayMode = table.Rows[0]["PayMode"].ToString();
            if (table.Rows[0]["SumMoney"].ToString() != "")
            {
                model.SumMoney = new decimal?(decimal.Parse(table.Rows[0]["SumMoney"].ToString()));
            }
            if (table.Rows[0]["BeginDate"].ToString() != "")
            {
                model.BeginDate = new DateTime?(DateTime.Parse(table.Rows[0]["BeginDate"].ToString()));
            }
            if (table.Rows[0]["EndDate"].ToString() != "")
            {
                model.EndDate = new DateTime?(DateTime.Parse(table.Rows[0]["EndDate"].ToString()));
            }
            if (table.Rows[0]["WorkDay"].ToString() != "")
            {
                model.WorkDay = new decimal?(decimal.Parse(table.Rows[0]["WorkDay"].ToString()));
            }
            model.MainWork = table.Rows[0]["MainWork"].ToString();
            model.Remark = table.Rows[0]["Remark"].ToString();
            if (table.Rows[0]["ContractState"].ToString() != "")
            {
                model.ContractState = new int?(int.Parse(table.Rows[0]["ContractState"].ToString()));
            }
            model.ContractOther = table.Rows[0]["ContractOther"].ToString();
            model.ContractOtherAddr = table.Rows[0]["ContractOtherAddr"].ToString();
            model.WorkAddr = table.Rows[0]["WorkAddr"].ToString();
            model.QualitySafety = table.Rows[0]["QualitySafety"].ToString();
            model.ContractOwner = table.Rows[0]["ContractOwner"].ToString();
            model.ContractOwnerAddr = table.Rows[0]["ContractOwnerAddr"].ToString();
            model.ContractOwnerMan = table.Rows[0]["ContractOwnerMan"].ToString();
            model.ContractOwnerSpokesman = table.Rows[0]["ContractOwnerSpokesman"].ToString();
            model.ContractOwnerTel = table.Rows[0]["ContractOwnerTel"].ToString();
            model.ContractOwnerFax = table.Rows[0]["ContractOwnerFax"].ToString();
            model.ContractOwnerZipcode = table.Rows[0]["ContractOwnerZipcode"].ToString();
            model.ContractOwnerBack = table.Rows[0]["ContractOwnerBack"].ToString();
            model.ContractOwnerAccount = table.Rows[0]["ContractOwnerAccount"].ToString();
            model.ContractOtherMan = table.Rows[0]["ContractOtherMan"].ToString();
            model.ContractOtherSpokesman = table.Rows[0]["ContractOtherSpokesman"].ToString();
            model.ContractOtherTel = table.Rows[0]["ContractOtherTel"].ToString();
            model.ContractOtherFax = table.Rows[0]["ContractOtherFax"].ToString();
            model.ContractOtherZipcode = table.Rows[0]["ContractOtherZipcode"].ToString();
            model.ContractOtherBack = table.Rows[0]["ContractOtherBack"].ToString();
            model.ContractOtherAccount = table.Rows[0]["ContractOtherAccount"].ToString();
            if (table.Rows[0]["SumBalance"].ToString() != "")
            {
                model.SumBalance = new decimal?(decimal.Parse(table.Rows[0]["SumBalance"].ToString()));
            }
            model.BalanceRemark = table.Rows[0]["BalanceRemark"].ToString();
            if (table.Rows[0]["FlowGuid"].ToString() != "")
            {
                model.FlowGuid = new Guid(table.Rows[0]["FlowGuid"].ToString());
            }
            if (table.Rows[0]["AuditState"].ToString() != "")
            {
                model.AuditState = new int?(int.Parse(table.Rows[0]["AuditState"].ToString()));
            }
            model.FromType = table.Rows[0]["FromType"].ToString();
            return model;
        }
    }
}

