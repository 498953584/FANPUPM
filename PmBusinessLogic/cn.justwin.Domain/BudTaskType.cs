namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class BudTaskType
    {
        private BudTaskType()
        {
        }

        public static BudTaskType Create(string id, string code, string name, string user, DateTime date)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("任务类型ID", "任务类型ID不能为空");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("任务类型编码", "任务类型编码不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("任务类型名称", "任务类型名称不能为空");
            }
            return new BudTaskType { Id = id, Code = code, Name = name, Layer = GetCount() + 1 };
        }

        public static IList<BudTaskType> GetAll()
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<BudTaskType> list = (from cl in entities.XPM_Basic_CodeList
                    join ct in entities.XPM_Basic_CodeType on cl.TypeID equals ct.TypeID 
                    where (ct.SignCode == "TaskType") && cl.IsValid
                    select new BudTaskType { Layer = cl.CodeID, Name = cl.CodeName }).ToList<BudTaskType>();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Layer = i + 1;
                }
                return list;
            }
        }

        public static BudTaskType GetById(string id)
        {
            return GetByLayer(Convert.ToInt32(id));
        }

        public static BudTaskType GetByLayer(int layer)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP(1) CodeName FROM XPM_Basic_CodeList AS clist,XPM_Basic_CodeType AS ctype WHERE clist.TypeId=ctype.TypeId AND SignCode='taskType' AND clist.IsValid='1'");
            builder.AppendLine();
            builder.AppendFormat("AND NoteID NOT IN(SELECT TOP({0}-1)NoteId FROM XPM_Basic_CodeList AS clist,XPM_Basic_CodeType AS ctype ", layer);
            builder.AppendLine();
            builder.Append(" WHERE clist.TypeId=ctype.TypeId AND SignCode='taskType' AND clist.IsValid='1')");
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                BudTaskType type = null;
                while (reader.Read())
                {
                    type = new BudTaskType {
                        Id = layer.ToString(),
                        Name = reader["CodeName"].ToString(),
                        Layer = layer
                    };
                }
                return type;
            }
        }

        public static int GetCount()
        {
            int num = 0;
            pm2Entities entities = new pm2Entities();
            try
            {
                num = (from cl in entities.XPM_Basic_CodeList
                    join ct in entities.XPM_Basic_CodeType on cl.TypeID equals ct.TypeID 
                    where ct.SignCode == "TaskType"
                    select cl).Count<XPM_Basic_CodeList>();
            }
            catch
            {
            }
            finally
            {
                if (entities != null)
                {
                    entities.Dispose();
                }
            }
            return num;
        }

        public static string GetTypeIdByLayer(int layer)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP(1) CodeName FROM XPM_Basic_CodeList AS clist,XPM_Basic_CodeType AS ctype WHERE clist.TypeId=ctype.TypeId AND SignCode='taskType' AND clist.IsValid='1'");
            builder.AppendLine();
            builder.AppendFormat("AND NoteID NOT IN(SELECT TOP({0}-1)NoteId FROM XPM_Basic_CodeList AS clist,XPM_Basic_CodeType AS ctype ", layer + 1);
            builder.AppendLine();
            builder.Append(" WHERE clist.TypeId=ctype.TypeId AND SignCode='taskType' AND clist.IsValid='1')");
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                string str = "0";
                while (reader.Read())
                {
                    str = (layer + 1).ToString();
                }
                return str;
            }
        }

        public string Code { get; set; }

        public string Id { get; set; }

        public int Layer { get; set; }

        public string Name { get; set; }
    }
}

