namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class ProjectAction
    {
        public DataTable GetBuildingList(string projectId)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_buildings where projectId = " + projectId.ToString());
        }

        public DataTable GetProjectList(string UserCode)
        {
            return publicDbOpClass.DataTableQuary("select * from V_CPM_UserProjectList where UserCode = '" + UserCode + "'");
        }

        public DataTable GetProjectName(Guid ProjectGuid)
        {
            return publicDbOpClass.DataTableQuary("select * from PM_projects where Guid = '" + ProjectGuid + "'");
        }
    }
}

