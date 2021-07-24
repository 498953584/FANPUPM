namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResUnit
    {
        private ResUnit()
        {
        }

        public static void Add(ResUnit resUnit)
        {
            Res_Unit unit = new Res_Unit();
            using (pm2Entities entities = new pm2Entities())
            {
                unit.UnitId = resUnit.Id;
                unit.Code = resUnit.Code;
                unit.Name = resUnit.Name;
                unit.InputUser = resUnit.InputUser;
                unit.InputDate = resUnit.InputDate;
                entities.AddToRes_Unit(unit);
                entities.SaveChanges();
            }
        }

        public static ResUnit Create(string id, string code, string name, string user, DateTime? date)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("单位Id", "单位Id不能为空");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("单位编码", "单位编码不能为空");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("单位名称", "单位名称不能为空");
            }
            return new ResUnit { Id = id, Code = code, Name = name, InputUser = user, InputDate = date };
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                entities.DeleteObject(entities.Res_Unit.FirstOrDefault<Res_Unit>(c => c.UnitId == id));
                entities.SaveChanges();
            }
        }

        public static IList<ResUnit> GetAll()
        {
            List<ResUnit> list = new List<ResUnit>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_Unit unit in (from u in entities.Res_Unit select u).ToList<Res_Unit>())
                {
                    ResUnit item = new ResUnit {
                        Id = unit.UnitId,
                        Code = unit.Code,
                        Name = unit.Name,
                        InputUser = unit.InputUser,
                        InputDate = unit.InputDate
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static ResUnit GetById(string id)
        {
            ResUnit unit = new ResUnit();
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Unit unit2 = (from u in entities.Res_Unit
                    where u.UnitId == id
                    select u).FirstOrDefault<Res_Unit>();
                if (unit2 != null)
                {
                    unit.Id = id;
                    unit.Code = unit2.Code;
                    unit.Name = unit2.Name;
                    unit.InputUser = unit2.InputUser;
                    unit.InputDate = unit2.InputDate;
                    return unit;
                }
                return null;
            }
        }

        public static ResUnit GetByUnitName(string name)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_Unit
                    where m.Name == name
                    select new ResUnit { Id = m.UnitId, Code = m.Code, Name = m.Name, InputDate = m.InputDate, InputUser = m.InputUser }).FirstOrDefault<ResUnit>();
            }
        }

        public static void Update(ResUnit resUnit)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Unit unit = entities.Res_Unit.FirstOrDefault<Res_Unit>(c => c.UnitId == resUnit.Id);
                unit.Code = resUnit.Code;
                unit.Name = resUnit.Name;
                unit.InputUser = resUnit.InputUser;
                unit.InputDate = resUnit.InputDate;
                entities.SaveChanges();
            }
        }

        public string Code { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string Name { get; set; }
    }
}

