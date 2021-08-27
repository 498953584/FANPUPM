using cn.justwin.Serialize;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace DomainServices.cn.justwin.Domain.EasyUI
{
    /// <summary>
    /// 管辖区域EasyUI Tree
    /// </summary>
    [DataContract]
    public  class Jurisdiction
    {
        /// <summary>
        /// 获取Json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cityDt"></param>
        /// <param name="countryDt"></param>
        /// <param name="jurisdictionDt"></param>
        /// <returns></returns>
        public string GetJson(DataTable dt, DataTable cityDt, DataTable countryDt,DataTable jurisdictionDt)
        {
            var userList = jurisdictionDt.Rows.Cast<DataRow>().Select(_ => _["AreaId"].ToString()).ToList();
            var serializer = new JsonSerializer();
            return serializer.Serialize(new[]
            {
                new Jurisdiction
                {
                    Id = "cn",
                    Text = "中国",
                    Children = GetProvince(dt, cityDt, countryDt,userList)
                }
            });
        }

        /// <summary>
        /// 获取省节点
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cityDt"></param>
        /// <param name="countryDt"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        private static List<Jurisdiction> GetProvince(DataTable dt, DataTable cityDt, DataTable countryDt, ICollection<string> userList)
        {
            return (from DataRow row in dt.Rows
                select new Jurisdiction
                {
                    Id = row["province_id"].ToString(),
                    Text = row["name"].ToString(),
                    Children = GetCity(cityDt, row["province_id"].ToString(), countryDt, userList),
                    State = "closed",
                    Attributes = new AttributesInfo { ProvinceId = row["province_id"].ToString() }
                }).ToList();
        }

        /// <summary>
        /// 获取城市节点
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="id"></param>
        /// <param name="countryDt"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        private static List<Jurisdiction> GetCity(DataTable dt, string id, DataTable countryDt, ICollection<string> userList)
        {
            return dt.Select($"province_id='{id}'")
                .Select(row =>
                {
                    var cityId = row["city_id"].ToString();
                    var city = new Jurisdiction
                    {
                        Id = cityId,
                        Text = row["name"].ToString(),
                        Children = GetCountry(countryDt, cityId, id, userList),
                        State = "closed",
                    };
                    if (city.Children.Any()) return city;
                    city.Checked = userList.Contains(cityId);
                    city.Attributes = new AttributesInfo { CityId = cityId, ProvinceId = id };
                    return city;
                }).ToList();
        }

        /// <summary>
        /// 获取区域节点
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        private static List<Jurisdiction> GetCountry(DataTable dt, string id, string pid, ICollection<string> userList)
        {
            return dt.Select($"city_id='{id}'").Select(row =>
            {
                var cid = row["country_id"].ToString();
                var c = new Jurisdiction
                {
                    Id = cid,
                    Text = row["name"].ToString(),
                    Attributes = new AttributesInfo { CityId = id, ProvinceId = pid },
                    Checked = userList.Contains(cid)
                };
                return c;
            }).ToList();
        }

        /// <summary>
        /// 定义了一些子节点的节点数组
        /// </summary>
        [DataMember(Name = "children", Order = 5, EmitDefaultValue = false, IsRequired = false)]
        public List<Jurisdiction> Children { get; set; }

        /// <summary>
        /// 用来显示图标的 css class
        /// </summary>
        [DataMember(Name = "iconCls", Order = 3, EmitDefaultValue = false, IsRequired = false)]
        public string IconCls { get; set; }

        /// <summary>
        /// 绑定到节点的标识值
        /// </summary>
        [DataMember(Name = "id", Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// 节点状态，'open' 或 'closed'
        /// </summary>
        [DataMember(Name = "state", Order = 4, EmitDefaultValue = false, IsRequired = false)]
        public string State { get; set; }

        /// <summary>
        /// 要显示的文本
        /// </summary>
        [DataMember(Name = "text", Order = 2)]
        public string Text { get; set; }

        /// <summary>
        /// 节点是否被选中
        /// </summary>
        [DataMember(Name = "checked", Order = 6, EmitDefaultValue = false)]
        public bool Checked { get; set; }

        /// <summary>
        /// 绑定到节点的自定义属性
        /// </summary>
        [DataMember(Name = "attributes",Order = 7,EmitDefaultValue = false)]
        public AttributesInfo Attributes { get; set; }
    }

    /// <summary>
    /// 扩展属性信息
    /// </summary>
    [DataContract]
    public class AttributesInfo
    {
        /// <summary>
        /// 省ID
        /// </summary>
        [DataMember]
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市ID
        /// </summary>
        [DataMember]
        public string CityId { get; set; }

    }
}
