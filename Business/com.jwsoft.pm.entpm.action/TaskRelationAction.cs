namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class TaskRelationAction
    {
        public DataTable TaskRelationBind(Guid prjCode, string TaskCode)
        {
            string str = "";
            object obj2 = str + "select *,(select top 1 TaskName from EPM_Task_TaskList ";
            object obj3 = string.Concat(new object[] { obj2, " where ProjectCode='", prjCode, "'and TaskCode = BeginTaskCode) as TaskName " });
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj3, " from EPM_Task_TaskRelation where ProjectCode='", prjCode, "'and EndTaskCode = '", TaskCode, "'" }));
        }

        public int TaskRelationDel(int NoteID)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + "delete EPM_Task_TaskRelation where NoteID=" + NoteID);
        }

        public int TaskRelationIns(TaskRelation task)
        {
            string str = "";
            object obj2 = str + "insert into EPM_Task_TaskRelation (ProjectCode,BeginTaskCode,EndTaskCode,Relationship,WaitDay)values";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "('", task.ProjectCode, "','", task.BeginTaskCode, "','", task.EndTaskCode, "',", task.Relationship, ",", task.WaitDay, ")" }));
        }

        public int TaskRelationUp(ArrayList task)
        {
            string str = " begin ";
            for (int i = 0; i < task.Count; i++)
            {
                object obj2 = str + " update EPM_Task_TaskRelation set  BeginTaskCode = '" + ((TaskRelation) task[i]).BeginTaskCode + "',";
                object obj3 = string.Concat(new object[] { obj2, " Relationship = ", ((TaskRelation) task[i]).Relationship, " , WaitDay=", ((TaskRelation) task[i]).WaitDay, " where " });
                str = string.Concat(new object[] { obj3, " ProjectCode= '", ((TaskRelation) task[i]).ProjectCode, "' and EndTaskCode = '", ((TaskRelation) task[i]).EndTaskCode, "'" });
            }
            return publicDbOpClass.ExecSqlString(str + " end");
        }
    }
}

