namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;

    public class ConstructOrganizeBBl
    {
        private ConstructOrganizeService server = new ConstructOrganizeService();

        public DataTable getDataTable(string DataTableName, string strwhere, string orderby)
        {
            return this.server.getDataTable(DataTableName, strwhere, orderby);
        }

        public DataTable getModelByGuid(string DataTableName, Guid flowGuid)
        {
            return this.server.getModelByFlow(DataTableName, flowGuid);
        }

        public int UpdGuidang(string tablename, int mark, int filesType, string strwhere)
        {
            return this.server.UpdGuidang(tablename, mark, filesType, strwhere);
        }
    }
}

