namespace cn.justwin.PrjManager
{
    using cn.justwin.BLL;
    using cn.justwin.DAL;
    using cn.justwin.Tender;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.UI;

    public class PrjMember
    {
        public static void Add(string id, List<string> codes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Guid guid = new Guid(id);
                PrjMember member = new PrjMember();
                List<string> list = GetCodes(id);
                using (List<string>.Enumerator enumerator = list.GetEnumerator())
                {
                    string code;
                    while (enumerator.MoveNext())
                    {
                        code = enumerator.Current;
                        if (!codes.Contains(code))
                        {
                            PT_PrjMember entity = (from m in entities.PT_PrjMember
                                where (m.PrjGuid == guid) && (m.PT_yhmc.v_yhdm == code)
                                select m).FirstOrDefault<PT_PrjMember>();
                            if (entity != null)
                            {
                                entities.DeleteObject(entity);
                                DelFile(entity.PrjMemberId);
                            }
                        }
                    }
                }
                foreach (string str in codes)
                {
                    if (!list.Contains(str))
                    {
                        member.AddSign(guid, str, entities);
                    }
                }
                entities.SaveChanges();
            }
        }

        public static void Add(List<string> ids, List<string> codes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if ((ids != null) && (codes != null))
                {
                    PrjMember member = new PrjMember();
                    foreach (string str in ids)
                    {
                        Guid guid = new Guid(str);
                        List<string> list = (from m in entities.PT_PrjMember
                            where m.PrjGuid == guid
                            select m.PT_yhmc.v_yhdm).ToList<string>();
                        foreach (string str2 in codes)
                        {
                            if (!list.Contains(str2))
                            {
                                member.AddSign(guid, str2, entities);
                            }
                        }
                    }
                    entities.SaveChanges();
                }
            }
        }

        public static void AddLimit(Guid guid)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> codes = GetCodes(guid.ToString());
                if (codes.Count > 0)
                {
                    foreach (string str in TenderUser.GetUserCodes(guid))
                    {
                        if (codes.Contains(str))
                        {
                            codes.Remove(str);
                        }
                    }
                    foreach (string str2 in codes)
                    {
                        PT_PrjInfo_ZTB_User user = new PT_PrjInfo_ZTB_User {
                            Id = Guid.NewGuid().ToString(),
                            PrjGuid = new Guid?(guid),
                            UserCode = str2
                        };
                        entities.AddToPT_PrjInfo_ZTB_User(user);
                    }
                    entities.SaveChanges();
                }
            }
        }

        private void AddMoreInformation(string userCode, PT_PrjMember member)
        {
            DataTable oldInfo = this.GetOldInfo(userCode);
            if ((oldInfo != null) && (oldInfo.Rows.Count > 0))
            {
                member.Post = oldInfo.Rows[0]["Post"].ToString();
                member.EducationalBackground = oldInfo.Rows[0]["EducationalBackground"].ToString() + oldInfo.Rows[0]["Specialty"].ToString();
                member.Technical = oldInfo.Rows[0]["Technical"].ToString();
                member.PastPerformance = oldInfo.Rows[0]["PastPerformance"].ToString();
                member.PostAndCompetency = oldInfo.Rows[0]["PostAndCompetency"].ToString();
                member.TrainingInformation = oldInfo.Rows[0]["Courses"].ToString();
            }
        }

        private void AddSign(Guid guid, string code, pm2Entities context)
        {
            PT_PrjMember member = new PT_PrjMember {
                PrjMemberId = Guid.NewGuid().ToString(),
                PrjGuid = new Guid?(guid),
                PT_yhmc = (from m in context.PT_yhmc
                    where m.v_yhdm == code
                    select m).FirstOrDefault<PT_yhmc>(),
                InputDate = new DateTime?(DateTime.Now)
            };
            this.AddMoreInformation(code, member);
            context.AddToPT_PrjMember(member);
        }

        private static void DelFile(string prjMemberId)
        {
            if (!string.IsNullOrEmpty(prjMemberId))
            {
                Page page = new Page();
                string str = ConfigHelper.Get("ProjectFile");
                string path = page.Server.MapPath(str + prjMemberId);
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (File.Exists(files[i]))
                        {
                            File.Delete(files[i]);
                        }
                    }
                    Directory.Delete(path);
                }
            }
        }

        public static void DelMember(string prjMemberId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                PT_PrjMember entity = (from m in entities.PT_PrjMember
                    where m.PrjMemberId == prjMemberId
                    select m).FirstOrDefault<PT_PrjMember>();
                if (entity != null)
                {
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                }
            }
        }

        private void DelMember(Guid guid, pm2Entities context)
        {
            foreach (PT_PrjMember member in (from m in context.PT_PrjMember
                where m.PrjGuid == guid
                select m).ToList<PT_PrjMember>())
            {
                context.DeleteObject(member);
            }
        }

        private static string GenerateConditon(string prjName, string prjCode, DateTime? start, DateTime? end, int? memberFowState, int? prjState, string userCode, string prjManagerName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("1=1");
            if (!string.IsNullOrEmpty(userCode))
            {
                string[] arr = PrivHelper.GetBusiDataId("project", userCode).ToArray<string>();
                if (arr.Length == 0)
                {
                    builder.AppendFormat(" AND vProject.PrjGuid is null \n", new object[0]);
                }
                else
                {
                    builder.AppendFormat(" AND vProject.PrjGuid IN({0}) \n", DBHelper.GetInParameterSql(arr));
                }
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat(" AND PrjName LIKE '%{0}%' ", prjName.Trim());
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                builder.AppendFormat(" AND PrjCode LIKE '%{0}%' ", prjCode.Trim());
            }
            if (!string.IsNullOrEmpty(prjManagerName))
            {
                builder.AppendFormat(" AND PrjMangerName LIKE '%{0}%' ", prjManagerName.Trim());
            }
            if (start.HasValue)
            {
                builder.AppendFormat(" AND StartDate >= '{0}' ", Common2.GetTime(start));
            }
            if (end.HasValue)
            {
                builder.AppendFormat(" AND StartDate <='{0}' ", Common2.GetTime(end));
            }
            if (memberFowState.HasValue)
            {
                builder.AppendFormat(" AND MemberFlowState='{0}' ", memberFowState);
            }
            if (prjState.HasValue)
            {
                builder.AppendFormat(" AND PrjState='{0}'", prjState);
            }
            return builder.ToString();
        }

        public static DataTable GetAll(string prjName, string prjCode, DateTime? start, DateTime? end, int? memberFowState, int? prjState, string userCode, string prjManagerName, int pageNo, int pageSize)
        {
            string str = GenerateConditon(prjName, prjCode, start, end, memberFowState, prjState, userCode, prjManagerName);
            string dateConditon = GetDateConditon(start, end);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\t\tSELECT TOP({1}) * FROM (\r\n\t\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY Date DESC, TypeCode ASC) AS No,* \r\n\t\t\t\t\tFROM (\r\n\t\t\t\t\t\tSELECT P.TypeCode, P.InputDate, P.i_ChildNum, P.PrjCode, P.PrjGuid, P.PrjName\r\n\t\t\t\t\t\t\t, ISNULL(P.MemberFlowState, -1) AS MemberFlowState, 1 AS Primit, P.PrjStateName, P.PrjMangerName, P.MemberNames\r\n\t\t\t\t\t\t\t, P.StartDate, P.EndDate, P.PrjState\r\n\t\t\t\t\t\t\t,CASE LEN(P.TypeCode) \r\n\t\t\t\t\t\t\t\tWHEN 10 THEN (\r\n\t\t\t\t\t\t\t\t\tSELECT InputDate FROM vProject AS V1 WHERE V1.TypeCode = SUBSTRING(P.TypeCode, 1, 5)\r\n\t\t\t\t\t\t\t\t)\r\n\t\t\t\t\t\t\t\tWHEN 5 THEN InputDate\r\n\t\t\t\t\t\t\tEND AS Date\r\n\t\t\t\t\t\tFROM vProject AS P\r\n\t\t\t\t\t\tWHERE P.SetUpFlowState = 1 and P.PrjState in('5','7','8','9','10','11','12','13','17') {3}\r\n\t\t\t\t\t) AS V \r\n\t\t\t\t\tWHERE ((len(TypeCode)=5 and left(TypeCode,5) in (select distinct left(TypeCode,5) from vProject\r\n                                                                        where  {0}))\r\n                          or TypeCode in (select TypeCode from vProject where len(TypeCode)=10 and  {0}))\t\t\t\t\t\t\r\n\t\t\t\t) AS V2 \r\n\t\t\t\tWHERE 1 = 1 AND No > {2}\r\n\t\t\t", new object[] { str, pageSize, (pageNo - 1) * pageSize, dateConditon });
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public static List<string> GetCodes(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Guid guid = new Guid(id);
                return (from m in entities.PT_PrjMember
                    where m.PrjGuid == guid
                    orderby m.InputDate
                    select m.PT_yhmc.v_yhdm).ToList<string>();
            }
        }

        public static int GetCount(string prjName, string prjCode, DateTime? start, DateTime? end, int? memberFowState, int? prjState, string userCode, string prjManagerName)
        {
            string str = GenerateConditon(prjName, prjCode, start, end, memberFowState, prjState, userCode, prjManagerName);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\t\tSELECT COUNT(*)\r\n\t\t\t\tFROM (\r\n\t\t\t\t\tSELECT P.TypeCode, P.InputDate, P.i_ChildNum, P.PrjCode, P.PrjGuid, P.PrjName\r\n\t\t\t\t\t\t, P.MemberFlowState, 1 AS Primit, P.PrjStateName, P.PrjMangerName, P.MemberNames\r\n\t\t\t\t\t\t, P.StartDate, P.EndDate, P.PrjState\r\n\t\t\t\t\t\t,CASE LEN(P.TypeCode) \r\n\t\t\t\t\t\t\tWHEN 10 THEN (\r\n\t\t\t\t\t\t\t\tSELECT InputDate FROM vProject AS V1 WHERE V1.TypeCode = SUBSTRING(P.TypeCode, 1, 5)\r\n\t\t\t\t\t\t\t)\r\n\t\t\t\t\t\t\tWHEN 5 THEN InputDate\r\n\t\t\t\t\t\tEND AS Date\r\n\t\t\t\t\tFROM vProject AS P\r\n\t\t\t\t\tWHERE P.SetUpFlowState = 1 and P.PrjState in('5','7','8','9','10','11','12','13','17')\r\n\t\t\t\t) AS V \r\n\t\t\t\tWHERE ((len(TypeCode)=5 and left(TypeCode,5) in (select distinct left(TypeCode,5) from vProject\r\n                                                                        where  {0}))\r\n                          or TypeCode in (select TypeCode from vProject where len(TypeCode)=10 and  {0}))\r\n\t\t\t", str);
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null));
        }

        private static string GetDateConditon(DateTime? start, DateTime? end)
        {
            StringBuilder builder = new StringBuilder();
            if (start.HasValue)
            {
                builder.AppendFormat(" AND StartDate >= '{0}' ", Common2.GetTime(start));
            }
            if (end.HasValue)
            {
                builder.AppendFormat(" AND StartDate <='{0}' ", Common2.GetTime(end));
            }
            return builder.ToString();
        }

        public static string GetFlowState(string prjGuid)
        {
            string str = string.Empty;
            string cmdText = string.Format("SELECT WF.FlowState FROM PT_PrjMemberWF WF WHERE WF.PrjGuid = '{0}'", prjGuid);
            try
            {
                str = DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText));
            }
            catch
            {
            }
            return str;
        }

        public static DataTable GetMemberInfo(string prjMemberId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT Member.PrjMemberId,v_xm\r\n\t,Member.Post        --岗位 \r\n\t,Member.EducationalBackground   --学历和专业\r\n\t,Member.Technical               -- 职称\r\n\t,Member.PostAndCompetency       --资格证书\r\n\t,Member.PastPerformance        --AS 以往工作表现\r\n\t,Member.TrainingInformation            --上岗培训情况(只是培训中的培训课程) \r\nFROM PT_PrjMember AS Member\r\nLEFT JOIN PT_yhmc AS Yh ON Member.MemberCode=Yh.v_yhdm\r\nWHERE Member.PrjMemberId='{0}'", prjMemberId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable GetMembers(string prjGuid, string memberName, string postName, string educationalBackground, string specialty, string technical, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (\r\n\t                            SELECT ROW_NUMBER() OVER( ORDER BY InputDate DESC) AS No,* FROM V_GetAllPrjMembers\r\n\t                            WHERE PrjGuid='{0}' ", prjGuid);
            if (!string.IsNullOrEmpty(memberName))
            {
                builder.AppendFormat(" AND MemberName LIKE '%{0}%'", memberName);
            }
            if (!string.IsNullOrEmpty(postName))
            {
                builder.AppendFormat(" AND Post LIKE '%{0}%' ", postName);
            }
            if (!string.IsNullOrEmpty(educationalBackground))
            {
                builder.AppendFormat(" AND EducationalBackground LIKE '%{0}%' ", educationalBackground);
            }
            if (!string.IsNullOrEmpty(technical))
            {
                builder.AppendFormat(" AND Technical LIKE '%{0}%' ", technical);
            }
            builder.AppendFormat(") AS TResult WHERE No BETWEEN '{0}' AND '{1}'", ((pageNo - 1) * pageSize) + 1, pageNo * pageSize);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static int GetMembersCount(string prjGuid, string memberName, string postName, string educationalBackground, string specialty, string technical)
        {
            int result = 0;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM V_GetAllPrjMembers\r\n\t                            WHERE PrjGuid='{0}' ", prjGuid);
            if (!string.IsNullOrEmpty(memberName))
            {
                builder.AppendFormat(" AND MemberName LIKE '%{0}%'", memberName);
            }
            if (!string.IsNullOrEmpty(postName))
            {
                builder.AppendFormat(" AND Post LIKE '%{0}%' ", postName);
            }
            if (!string.IsNullOrEmpty(educationalBackground))
            {
                builder.AppendFormat(" AND EducationalBackground LIKE '%{0}%' ", educationalBackground);
            }
            if (!string.IsNullOrEmpty(technical))
            {
                builder.AppendFormat(" AND Technical LIKE '%{0}%' ", technical);
            }
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString());
            string s = (obj2 == null) ? "0" : obj2.ToString();
            int.TryParse(s, out result);
            return result;
        }

        private DataTable GetOldInfo(string userCode)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM V_GetMembersOldInfo WHERE  v_yhdm='{0}'", userCode);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static bool IsPrimit(string prjGuid, string userCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return ((from u in entities.PT_PrjInfo_ZTB_User
                    where (u.PrjGuid == new Guid(prjGuid)) && (u.UserCode == userCode)
                    select u).Count<PT_PrjInfo_ZTB_User>() > 0);
            }
        }

        public static void Update(PrjMember model)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                PT_PrjMember member = (from m in entities.PT_PrjMember
                    where m.PrjMemberId == model.PrjMemberId
                    select m).FirstOrDefault<PT_PrjMember>();
                if (member != null)
                {
                    member.Post = model.Post;
                    member.PostAndCompetency = model.PostAndCompetency;
                    member.PastPerformance = model.PastPerformance;
                    member.Technical = model.Technical;
                    member.TrainingInformation = model.TrainingInformation;
                    member.EducationalBackground = model.EducationalBackground;
                    entities.SaveChanges();
                }
            }
        }

        public string EducationalBackground { get; set; }

        public string PastPerformance { get; set; }

        public string Post { get; set; }

        public string PostAndCompetency { get; set; }

        public string PrjMemberId { get; set; }

        public string Technical { get; set; }

        public string TrainingInformation { get; set; }
    }
}

