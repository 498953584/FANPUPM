namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;
    using System.Text;

    public class EquConnectionItemService : Repository<EquConnectionItem>
    {
        public DataTable GetByConnectionId(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("WITH children AS (");
            builder.AppendLine("SELECT ItemCode,ParentCode,ItemName,UnitPrice");
            builder.AppendLine("FROM Equ_RepairCompanyInfo");
            builder.AppendLine("WHERE ItemCode in (SELECT ItemCode FROM  Equ_ConnectionItem WHERE ConnectionId = '{0}')");
            builder.AppendLine("),father AS (");
            builder.AppendLine("SELECT DISTINCT company.ItemCode,company.ItemName,Address,Note");
            builder.AppendLine("FROM Equ_RepairCompanyInfo AS company,children");
            builder.AppendLine("WHERE company.ItemCode = children.ParentCode)");
            builder.AppendLine("SELECT father.ItemCode,father.ItemName,children.ItemCode,children.ItemName,");
            builder.AppendLine("       children.UnitPrice,father.Address,father.Note");
            builder.AppendLine("FROM children LEFT JOIN father ON children.ParentCode = father.ItemCode");
            return base.ExecuteQuery(string.Format(builder.ToString(), id));
        }
    }
}

