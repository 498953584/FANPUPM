namespace cn.justwin.Tender
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class Area
    {
        public static List<Area> citys = new List<Area> { 
            new Area("110100", "北京"), new Area("120100", "天津"), new Area("130101", "石家庄"), new Area("130201", "唐山"), new Area("130301", "秦皇岛"), new Area("130701", "张家口"), new Area("130801", "承德"), new Area("131001", "廊坊"), new Area("130401", "邯郸"), new Area("130501", "邢台"), new Area("130601", "保定"), new Area("130901", "沧州"), new Area("133001", "衡水"), new Area("140101", "太原"), new Area("140201", "大同"), new Area("140301", "阳泉"), 
            new Area("140501", "晋城"), new Area("140601", "朔州"), new Area("142201", "忻州"), new Area("142331", "离石"), new Area("142401", "榆次"), new Area("142601", "临汾"), new Area("142701", "运城"), new Area("140401", "长治"), new Area("150101", "呼和浩特"), new Area("150201", "包头"), new Area("150301", "乌海"), new Area("152601", "集宁"), new Area("152701", "东胜"), new Area("152801", "临河"), new Area("152921", "阿拉善左旗"), new Area("150401", "赤峰"), 
            new Area("150501", "鄂尔多斯"), new Area("152301", "通辽"), new Area("152502", "锡林浩特"), new Area("152101", "海拉尔"), new Area("152201", "乌兰浩特"), new Area("210101", "沈阳"), new Area("210201", "大连"), new Area("210301", "鞍山"), new Area("210401", "抚顺"), new Area("210501", "本溪"), new Area("210601", "丹东"), new Area("210701", "锦州"), new Area("210801", "营口"), new Area("210901", "阜新"), new Area("211101", "盘锦"), new Area("211201", "铁岭"), 
            new Area("211301", "朝阳"), new Area("211401", "锦西"), new Area("211501", "兴城"), new Area("220101", "长春"), new Area("220201", "吉林"), new Area("220301", "四平"), new Area("220401", "辽源"), new Area("220601", "浑江"), new Area("222301", "白城"), new Area("222401", "延吉"), new Area("220501", "通化"), new Area("230101", "哈尔滨"), new Area("230301", "鸡西"), new Area("230401", "鹤岗"), new Area("230501", "双鸭山"), new Area("230701", "伊春"), 
            new Area("230801", "佳木斯"), new Area("230901", "七台河"), new Area("231001", "牡丹江"), new Area("232301", "绥化"), new Area("230201", "齐齐哈尔"), new Area("230601", "大庆"), new Area("232601", "黑河"), new Area("232700", "加格达奇"), new Area("310100", "上海"), new Area("320101", "南京"), new Area("320201", "无锡"), new Area("320301", "徐州"), new Area("320401", "常州"), new Area("320501", "苏州"), new Area("320600", "南通"), new Area("320701", "连云港"), 
            new Area("320801", "淮阴"), new Area("320901", "盐城"), new Area("321001", "扬州"), new Area("321101", "镇江"), new Area("330101", "杭州"), new Area("330201", "宁波"), new Area("330301", "温州"), new Area("330401", "嘉兴"), new Area("330501", "湖州"), new Area("330601", "绍兴"), new Area("330701", "金华"), new Area("330801", "衢州"), new Area("330901", "舟山"), new Area("332501", "丽水"), new Area("332602", "临海"), new Area("340101", "合肥"), 
            new Area("340201", "芜湖"), new Area("340301", "蚌埠"), new Area("340401", "淮南"), new Area("340501", "马鞍山"), new Area("340601", "淮北"), new Area("340701", "铜陵"), new Area("340801", "安庆"), new Area("341001", "黄山"), new Area("342101", "阜阳"), new Area("342201", "宿州"), new Area("342301", "滁州"), new Area("342401", "六安"), new Area("342501", "宣州"), new Area("342601", "巢湖"), new Area("342901", "贵池"), new Area("350101", "福州"), 
            new Area("350201", "厦门"), new Area("350301", "莆田"), new Area("350401", "三明"), new Area("350501", "泉州"), new Area("350601", "漳州"), new Area("352101", "南平"), new Area("352201", "宁德"), new Area("352601", "龙岩"), new Area("360101", "南昌"), new Area("360201", "景德镇"), new Area("362101", "赣州"), new Area("360301", "萍乡"), new Area("360401", "九江"), new Area("360501", "新余"), new Area("360601", "鹰潭"), new Area("362201", "宜春"), 
            new Area("362301", "上饶"), new Area("362401", "吉安"), new Area("362502", "临川"), new Area("370101", "济南"), new Area("370201", "青岛"), new Area("370301", "淄博"), new Area("370401", "枣庄"), new Area("370501", "东营"), new Area("370601", "烟台"), new Area("370701", "潍坊"), new Area("370801", "济宁"), new Area("370901", "泰安"), new Area("371001", "威海"), new Area("371100", "日照"), new Area("372301", "滨州"), new Area("372401", "德州"), 
            new Area("372501", "聊城"), new Area("372801", "临沂"), new Area("372901", "菏泽"), new Area("410101", "郑州"), new Area("410201", "开封"), new Area("410301", "洛阳"), new Area("410401", "平顶山"), new Area("410501", "安阳"), new Area("410601", "鹤壁"), new Area("410701", "新乡"), new Area("410801", "焦作"), new Area("410901", "濮阳"), new Area("411001", "许昌"), new Area("411101", "漯河"), new Area("411201", "三门峡"), new Area("412301", "商丘"), 
            new Area("412701", "周口"), new Area("412801", "驻马店"), new Area("412901", "南阳"), new Area("413001", "信阳"), new Area("420101", "武汉"), new Area("420201", "黄石"), new Area("420301", "十堰"), new Area("420400", "沙市"), new Area("420501", "宜昌"), new Area("420601", "襄樊"), new Area("420701", "鄂州"), new Area("420801", "荆门"), new Area("422103", "黄州"), new Area("422201", "孝感"), new Area("422301", "咸宁"), new Area("422421", "江陵"), 
            new Area("422801", "恩施"), new Area("430101", "长沙"), new Area("430401", "衡阳"), new Area("430501", "邵阳"), new Area("432801", "郴州"), new Area("432901", "永州"), new Area("430801", "大庸"), new Area("433001", "怀化"), new Area("433101", "吉首"), new Area("430201", "株洲"), new Area("430301", "湘潭"), new Area("430601", "岳阳"), new Area("430701", "常德"), new Area("432301", "益阳"), new Area("432501", "娄底"), new Area("440101", "广州"), 
            new Area("440301", "深圳"), new Area("441501", "汕尾"), new Area("441301", "惠州"), new Area("441601", "河源"), new Area("440601", "佛山"), new Area("441801", "清远"), new Area("441901", "东莞"), new Area("440401", "珠海"), new Area("440701", "江门"), new Area("441201", "肇庆"), new Area("442001", "中山"), new Area("442101", "云浮"), new Area("440801", "湛江"), new Area("440901", "茂名"), new Area("440201", "韶关"), new Area("440501", "汕头"), 
            new Area("441401", "梅州"), new Area("441701", "阳江"), new Area("450101", "南宁"), new Area("450401", "梧州"), new Area("452501", "玉林"), new Area("450301", "桂林"), new Area("452601", "百色"), new Area("452701", "河池"), new Area("452802", "钦州"), new Area("450201", "柳州"), new Area("450501", "北海"), new Area("450601", "防城港"), new Area("460100", "海口"), new Area("460200", "三亚"), new Area("460300", "文昌"), new Area("510101", "成都"), 
            new Area("513321", "康定"), new Area("513101", "雅安"), new Area("513229", "马尔康"), new Area("510301", "自贡"), new Area("500100", "重庆"), new Area("512901", "南充"), new Area("510501", "泸州"), new Area("510601", "德阳"), new Area("510701", "绵阳"), new Area("510901", "遂宁"), new Area("511001", "内江"), new Area("511101", "乐山"), new Area("512501", "宜宾"), new Area("510801", "广元"), new Area("513021", "达县"), new Area("513401", "西昌"), 
            new Area("510401", "攀枝花"), new Area("500239", "黔江土家族苗族自治县"), new Area("520101", "贵阳"), new Area("520200", "六盘水"), new Area("522201", "铜仁"), new Area("522501", "安顺"), new Area("522601", "凯里"), new Area("522701", "都匀"), new Area("522301", "兴义"), new Area("522421", "毕节"), new Area("522101", "遵义"), new Area("530101", "昆明"), new Area("530201", "东川"), new Area("532201", "曲靖"), new Area("532301", "楚雄"), new Area("532401", "玉溪"), 
            new Area("532501", "个旧"), new Area("532621", "文山"), new Area("532721", "思茅"), new Area("532101", "昭通"), new Area("532821", "景洪"), new Area("532901", "大理"), new Area("533001", "保山"), new Area("533121", "潞西"), new Area("533221", "丽江纳西族自治县"), new Area("533321", "泸水"), new Area("533421", "中甸"), new Area("533521", "临沧"), new Area("540101", "拉萨"), new Area("542121", "昌都"), new Area("542221", "乃东"), new Area("542301", "日喀则"), 
            new Area("542421", "那曲"), new Area("542523", "噶尔"), new Area("542621", "林芝"), new Area("610101", "西安"), new Area("610201", "铜川"), new Area("610301", "宝鸡"), new Area("610401", "咸阳"), new Area("612101", "渭南"), new Area("612301", "汉中"), new Area("612401", "安康"), new Area("612501", "商州"), new Area("612601", "延安"), new Area("612701", "榆林"), new Area("620101", "兰州"), new Area("620401", "白银"), new Area("620301", "金昌"), 
            new Area("620501", "天水"), new Area("622201", "张掖"), new Area("622301", "武威"), new Area("622421", "定西"), new Area("622624", "成县"), new Area("622701", "平凉"), new Area("622801", "西峰"), new Area("622901", "临夏"), new Area("623027", "夏河"), new Area("620201", "嘉峪关"), new Area("622102", "酒泉"), new Area("630100", "西宁"), new Area("632121", "平安"), new Area("632221", "门源回族自治县"), new Area("632321", "同仁"), new Area("632521", "共和"), 
            new Area("632621", "玛沁"), new Area("632721", "玉树"), new Area("632802", "德令哈"), new Area("640101", "银川"), new Area("640201", "石嘴山"), new Area("642101", "吴忠"), new Area("642221", "固原"), new Area("650101", "乌鲁木齐"), new Area("650201", "克拉玛依"), new Area("652101", "吐鲁番"), new Area("652201", "哈密"), new Area("652301", "昌吉"), new Area("652701", "博乐"), new Area("652801", "库尔勒"), new Area("652901", "阿克苏"), new Area("653001", "阿图什"), 
            new Area("653101", "喀什"), new Area("654101", "伊宁"), new Area("710001", "台北"), new Area("710002", "基隆"), new Area("710020", "台南"), new Area("710019", "高雄"), new Area("710008", "台中"), new Area("211001", "辽阳"), new Area("653201", "和田"), new Area("542200", "泽当镇"), new Area("542600", "八一镇"), new Area("820000", "澳门"), new Area("810000", "香港")
         };
        private string dropvalue;
        public static List<Area> provices = new List<Area> { 
            new Area("", "请选择"), new Area("110000", "北京市"), new Area("120000", "天津市"), new Area("130000", "河北省"), new Area("140000", "山西省"), new Area("150000", "内蒙古自治区"), new Area("210000", "辽宁省"), new Area("220000", "吉林省"), new Area("230000", "黑龙江省"), new Area("310000", "上海市"), new Area("320000", "江苏省"), new Area("330000", "浙江省"), new Area("340000", "安徽省"), new Area("350000", "福建省"), new Area("360000", "江西省"), new Area("370000", "山东省"), 
            new Area("410000", "河南省"), new Area("420000", "湖北省"), new Area("430000", "湖南省"), new Area("440000", "广东省"), new Area("450000", "广西壮族自治区"), new Area("460000", "海南省"), new Area("500000", "重庆市"), new Area("510000", "四川省"), new Area("520000", "贵州省"), new Area("530000", "云南省"), new Area("540000", "西藏自治区"), new Area("610000", "陕西省"), new Area("620000", "甘肃省"), new Area("630000", "青海省"), new Area("640000", "宁夏回族自治区"), new Area("650000", "新疆维吾尔自治区"), 
            new Area("710000", "台湾省"), new Area("810000", "香港特别行政区"), new Area("820000", "澳门特别行政区")
         };
        private string text;

        public Area(string dropvalue, string text)
        {
            this.dropvalue = dropvalue;
            this.text = text;
        }

        public static void BindProvince(DropDownList drop)
        {
            drop.DataSource = provices;
            drop.DataTextField = "Text";
            drop.DataValueField = "Dropvalue";
            drop.DataBind();
        }

        public static string GetCity(string ProvinceCode)
        {
            string str = string.Empty;
            if (ProvinceCode == "110000")
            {
                return "请选择|北京";
            }
            if (ProvinceCode == "120000")
            {
                return "请选择|天津";
            }
            if (ProvinceCode == "310000")
            {
                return "请选择|上海";
            }
            if (ProvinceCode == "810000")
            {
                return "请选择|香港";
            }
            if (ProvinceCode == "820000")
            {
                return "请选择|澳门";
            }
            if (ProvinceCode == "500000")
            {
                return "请选择|重庆|黔江土家族苗族自治县";
            }
            str = "请选择|";
            for (int i = 0; i < citys.Count; i++)
            {
                if (citys[i].dropvalue.Substring(0, 2) == ProvinceCode.Substring(0, 2))
                {
                    str = str + citys[i].text + "|";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public string Dropvalue
        {
            get
            {
                return this.dropvalue;
            }
            set
            {
                this.dropvalue = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

