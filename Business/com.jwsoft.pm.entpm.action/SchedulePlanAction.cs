namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class SchedulePlanAction
    {
        public string GetTaskName(Guid ProjectCode, string TaskCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select TaskName from EPM_Task_TaskList where ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' " }));
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["TaskName"].ToString();
            }
            return string.Empty;
        }

        public TaskRelationCollection GetTaskRelation(Guid ProjectCode, string TaskCode)
        {
            TaskRelationCollection relations = new TaskRelationCollection();
            string str = "";
            object obj2 = str;
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "select * from EPM_Task_TaskRelation where ProjectCode='", ProjectCode, "' and EndTaskCode='", TaskCode, "' " })))
            {
                if (table.Rows.Count <= 0)
                {
                    return relations;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    relations.Add(this.GetTaskRelationFromDataRow(table.Rows[i]));
                }
            }
            return relations;
        }

        private TaskRelation GetTaskRelationFromDataRow(DataRow dr)
        {
            return new TaskRelation { BeginTaskCode = dr["BeginTaskCode"].ToString(), EndTaskCode = dr["EndTaskCode"].ToString(), NoteID = (int) dr["NoteID"], ProjectCode = (Guid) dr["ProjectCode"], Relationship = (int) dr["Relationship"], WaitDay = (int) dr["WaitDay"] };
        }

        public int UpdateBeforeTask(TaskRelationCollection tc, Guid ProjectCode, string TaskCode)
        {
            string sqlString = " ";
            object obj2 = sqlString;
            sqlString = string.Concat(new object[] { obj2, " delete from EPM_Task_TaskRelation where ProjectCode='", ProjectCode, "' and EndTaskCode='", TaskCode, "' " });
            if (tc.Count > 0)
            {
                for (int i = 0; i < tc.Count; i++)
                {
                    object obj3 = sqlString + " insert into EPM_Task_TaskRelation(ProjectCode,BeginTaskCode,EndTaskCode,Relationship,WaitDay) " + " values( ";
                    object obj4 = (string.Concat(new object[] { obj3, " '", tc[i].ProjectCode, "', " }) + " '" + tc[i].BeginTaskCode + "', ") + " '" + tc[i].EndTaskCode + "', ";
                    object obj5 = string.Concat(new object[] { obj4, " '", tc[i].Relationship, "', " });
                    sqlString = string.Concat(new object[] { obj5, " '", tc[i].WaitDay, "' " }) + " ) ";
                }
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}

