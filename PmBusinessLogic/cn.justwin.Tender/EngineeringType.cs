namespace cn.justwin.Tender
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Linq;
    public class EngineeringType
    {
        public static void Add(EngineeringType engType, pm2Entities pm2)
        {
            PT_PrjInfo_EngineeringType type = new PT_PrjInfo_EngineeringType {
                ID = engType.ID,
                PrjGuid = new Guid(engType.PrjGuid),
                EngineeringType = engType.EngineeringTypeID,
                EngineeringSubType = new int?(Convert.ToInt32(engType.EngineeringSubTypeID)),
                Grade = engType.Grade
            };
            pm2.AddToPT_PrjInfo_EngineeringType(type);
        }

        public static void Delete(string prjGuid, pm2Entities pm2)
        {
            Guid guid = new Guid(prjGuid);
            foreach (PT_PrjInfo_EngineeringType type in from et in pm2.PT_PrjInfo_EngineeringType
                where et.PrjGuid == guid
                select et)
            {
                pm2.DeleteObject(type);
            }
        }

        public static IList<EngineeringType> GetByPrjGuid(string prjGuid)
        {
            IList<EngineeringType> list = new List<EngineeringType>();
            using (pm2Entities entities = new pm2Entities())
            {
                Guid guid = new Guid(prjGuid);
                foreach (var type in from et in entities.PT_PrjInfo_EngineeringType
                    join ct in entities.Basic_CodeType on et.EngineeringType equals ct.TypeCode 
                    join cl in entities.Basic_CodeList on et.EngineeringSubType equals (int?) cl.ItemCode 
                    where (et.PrjGuid == guid) && (et.EngineeringType == cl.TypeCode)
                    select new { ID = et.ID, PrjGuid = et.PrjGuid, EngineeringType = et.EngineeringType, TypeName = ct.TypeName, EngineeringSubType = et.EngineeringSubType, ItemName = cl.ItemName, Grade = et.Grade })
                {
                    EngineeringType item = new EngineeringType {
                        ID = type.ID,
                        PrjGuid = type.PrjGuid.Value.ToString(),
                        EngineeringTypeID = type.EngineeringType,
                        EngineeringTypeName = type.TypeName,
                        EngineeringSubTypeID = type.EngineeringSubType.Value.ToString(),
                        EngineeringSubTypeName = type.ItemName,
                        Grade = type.Grade
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.EngineeringTypeName).Append("--").Append(this.EngineeringSubTypeName);
            if (!string.IsNullOrEmpty(this.Grade))
            {
                builder.Append("--").Append(this.Grade).Append("级");
            }
            return builder.ToString();
        }

        public static void UpdateEngineerType(IList<EngineeringType> engTypeList, string prjGuid)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Delete(prjGuid, entities);
                foreach (EngineeringType type in engTypeList)
                {
                    Add(type, entities);
                }
            }
        }

        public static void UpdateEngineerType(IList<EngineeringType> engTypeList, string prjGuid, pm2Entities pm2)
        {
            Delete(prjGuid, pm2);
            foreach (EngineeringType type in engTypeList)
            {
                Add(type, pm2);
            }
        }

        public string EngineeringSubTypeID { get; set; }

        public string EngineeringSubTypeName { get; set; }

        public string EngineeringTypeID { get; set; }

        public string EngineeringTypeName { get; set; }

        public string Grade { get; set; }

        public string ID { get; set; }

        public string PrjGuid { get; set; }
    }
}

