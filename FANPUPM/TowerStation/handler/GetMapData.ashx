<%@ WebHandler Language="C#" Class="GetMapData" %>

using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
using Business;
using com.jwsoft.pm.data;
using Newtonsoft.Json;
public class GetMapData : IHttpHandler
{
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public void ProcessRequest(HttpContext context)
    {

        string Action = context.Request["Action"];
        context.Response.ContentType = "text/plain";
        if (Action.Trim().Equals("GetGYS"))//获取供应商列表
        {
            string gysinfo = JsonConvert.SerializeObject(GetGYSInfos());
            context.Response.Write(gysinfo);
        }
        else if (Action.Trim().Equals("GetTowerTypeInfos"))
        {
            string gys = context.Request["gys"];
            string networktype = context.Request["networktype"];
            DataSet ds = new DataSet();
            DataTable dt = GetTowerAllInfo_New_Old(gys, networktype);
            dt.TableName = "左上";
            ds.Tables.Add(dt);

            DataTable dtowner_new = GetTowerOwner_New(gys, networktype);
            dtowner_new.TableName = "新塔国有情况";
            ds.Tables.Add(dtowner_new);

            DataTable dtowner_old = GetTowerOwner_Old(gys, networktype);
            dtowner_old.TableName = "旧塔国有情况";
            ds.Tables.Add(dtowner_old);

            DataTable dtIsIntelligence_New = GetTowerIsIntelligence_New(gys, networktype);
            dtIsIntelligence_New.TableName = "新塔是否智能";
            ds.Tables.Add(dtIsIntelligence_New);

            DataTable dtIsIntelligence_old = GetTowerIsIntelligence_Old(gys, networktype);
            dtIsIntelligence_old.TableName = "旧塔是否智能";
            ds.Tables.Add(dtIsIntelligence_old);

            DataTable dtBuildState_New = GetTowerBuildState_New(gys, networktype);
            dtBuildState_New.TableName = "新塔建设状态";
            ds.Tables.Add(dtBuildState_New);
            context.Response.Write(JsonConvert.SerializeObject(ds));
        }
        else if (Action.Trim().Equals("GetTowerMapInfos"))
        {
            string gys = context.Request["gys"];
            string networktype = context.Request["networktype"];
            context.Response.Write(JsonConvert.SerializeObject(GetMapData_Tower(gys, networktype)));
        }
        else if (Action.Trim().Equals("CTSearch"))
        {
            string para_jjq = context.Request["para_jjq"];
            string para_fjjq = context.Request["para_fjjq"];
            context.Response.Write(CreateHTML(GetCTInfo(para_jjq, para_fjjq)));
        }

    }
    private string CreateHTML(DataTable dt)
    {
        StringBuilder sthtml = new StringBuilder();
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            sthtml.AppendFormat("<div id='' style='display: flex;width: 100%;justify-content: space-between;' class='hang'><div id='' class='col1 center tabGe col table-first'>序号</div><div id='' class='col2 center tabGe col table-first'>塔站名称</div><div id='' class='col1 center tabGe col table-first'>网络服务供应商</div><div id='' class='col1 center tabGe col table-first'>网络服务子系统</div><div id='' class='col3 center tabGe col table-first'>地图坐标</div><div id='' class='col4 center tabGe col table-first'>操作</div></div><div id='' style='display: flex;width: 100%;justify-content: space-between;' class='hang'><div id='' class='col1 center tabGe col'>{0}</div><div id='' class='col2 center tabGe col' >{1}</div><div id='' class='col1 center tabGe col'>{8}</div><div id='' class='col1 center tabGe col'>{9}</div><div id='' class='col3 center tabGe col'>{2}</div><div id='' class='col4 center tabGe col'><p onclick=\"showInfo('{6}',0)\">查看</p><p  onclick=\"showInfo('{6}',1)\">编辑</p></div></div><div id='' style='display: flex;width: 100%;justify-content: space-between;' class='hang'><div id='' class='col1 center tabGe col'>{3}</div><div id='' class='col2 center tabGe col' >{4}</div><div id='' class='col1 center tabGe col'>{10}</div><div id='' class='col1 center tabGe col'>{11}</div><div id='' class='col3 center tabGe col'>{5}</div><div id='' class='col4 center tabGe col'><p  onclick=\"showInfo('{7}',0)\">查看</p><p  onclick=\"showInfo('{7}',1)\">编辑</p></div></div></div>",
                i*2+1,dr["Name"],dr["GPSLongitude"]+"   "+dr["GPSDimension"],i*2+2,dr["b_Name"],dr["b_GPSLongitude"]+"   "+dr["b_GPSDimension"],dr["TowerStationGUID"],dr["b_TowerStationGUID"], dr["NetworkManufacturer"],dr["NetworkSubsystemName"],dr["b_NetworkManufacturer"],dr["b_NetworkSubsystemName"]);
                i++;
        }
        return sthtml.ToString(); ;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parajjq"></param>
    /// <param name="parafjjq"></param>
    /// <returns></returns>
    private DataTable GetCTInfo(string parajjq, string parafjjq)
    {
        string sql = "select distinct NetworkManufacturer from TowerStationInfo";
        string jjqSQL = "";
        string jjqgys = "";
        string jjqnetsystem = "";
        if (!string.IsNullOrEmpty(parajjq))
        {
            string[] jjqsearchs = parajjq.Split(',');
            foreach (string item in jjqsearchs)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] infos = item.Split('_');
                    jjqgys += "'" + infos[1] + "',";
                    jjqnetsystem += infos[2] + ",";
                }
            }
            jjqgys = jjqgys.Substring(0, jjqgys.Length - 1);
            jjqnetsystem = jjqnetsystem.Substring(0, jjqnetsystem.Length - 1);
            if (!string.IsNullOrEmpty(jjqgys) && !string.IsNullOrEmpty(jjqnetsystem))
            {
                jjqSQL = string.Format("select * from V_TowerStation where  TowerStationGUID<>B_TowerStationGUID AND IsGatherArea=1 and NetworkManufacturer in ({0}) and B_NetworkManufacturer in({0}) and NetworkSubsystem in({1}) and B_NetworkSubsystem in({1}) and jl<500 ", jjqgys, jjqnetsystem);
            }
        }
        string fjjqSQL = "";
        string fjjqgys = "";
        string fjjqnetsystem = "";
        if (!string.IsNullOrEmpty(parafjjq))
        {
            string[] fjjqsearchs = parafjjq.Split(',');
            foreach (string item in fjjqsearchs)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] infos = item.Split('_');
                    fjjqgys += "'" + infos[1] + "',";
                    fjjqnetsystem += infos[2] + ",";
                }
            }
            fjjqgys = fjjqgys.Substring(0, fjjqgys.Length - 1);
            fjjqnetsystem = fjjqnetsystem.Substring(0, fjjqnetsystem.Length - 1);
            if (!string.IsNullOrEmpty(fjjqgys) && !string.IsNullOrEmpty(fjjqnetsystem))
            {
                fjjqSQL = string.Format("select * from V_TowerStation where TowerStationGUID<>B_TowerStationGUID AND  IsGatherArea=0 and NetworkManufacturer in ({0}) and B_NetworkManufacturer in({0}) and NetworkSubsystem in({1}) and B_NetworkSubsystem in({1}) and jl<500 ", fjjqgys, fjjqnetsystem);
            }
        }
        sql = jjqSQL;
        if (!string.IsNullOrEmpty(sql) && !string.IsNullOrEmpty(fjjqSQL))
        {
            sql += " union " + fjjqSQL;
        }

        if (!string.IsNullOrEmpty(sql))
        {
            return publicDbOpClass.DataTableQuary(sql);
        }
        else
            return new DataTable();
    }
    #region 塔站分布接口
    /// <summary>
    /// 获取网络供应商名单
    /// </summary>
    /// <returns></returns>
    private DataTable GetGYSInfos()
    {
        string sql = "select distinct NetworkManufacturer from TowerStationInfo";
        return publicDbOpClass.DataTableQuary(sql);
    }
    /// <summary>
    /// 获取铁塔新旧比较信息。左上角  左上区域数据源
    /// 2020年6月1日前建的是旧的   以后建的为新的。
    /// </summary>
    private DataTable GetTowerAllInfo_New_Old(string gys, string networktype)
    {
        string where = "";
        if (!string.IsNullOrEmpty(gys))
            where += " And NetworkManufacturer='" + gys + "' ";
        if (!string.IsNullOrEmpty(networktype))
            where += " And NetworkSubsystem='" + networktype + "' ";
        string sql = "select  (select COUNT(0) from dbo.TowerStationInfo where BuildTime>'2020-06-01' " + where + " )new ,(select COUNT(0) from dbo.TowerStationInfo where BuildTime<='2020-06-01' " + where + " )old";
        return publicDbOpClass.DataTableQuary(sql);
    }
    /// <summary>
    /// 获取新铁塔的国有信息。左边  左中区域数据源
    /// 2020年6月1日前建的是旧的   以后建的为新的。
    /// IsStateOwned字段 为判断 是否国有   0为非国有，1为国有
    /// </summary>
    private DataTable GetTowerOwner_New(string gys, string networktype)
    {
        string where = "";
        if (!string.IsNullOrEmpty(gys))
            where += " And NetworkManufacturer='" + gys + "' ";
        if (!string.IsNullOrEmpty(networktype))
            where += " And NetworkSubsystem='" + networktype + "' ";
        string sql = "select  (select COUNT(0) from dbo.TowerStationInfo where  BuildTime>'2020-06-01' And IsStateOwned=0  " + where + " )非国有 ,(select COUNT(0) from dbo.TowerStationInfo where  BuildTime>'2020-06-01' And  IsStateOwned=1  " + where + " )国有";
        return publicDbOpClass.DataTableQuary(sql);
    }
    /// <summary>
    /// 获取旧铁塔的国有信息。左边  左下区域数据源
    /// 2020年6月1日前建的是旧的   以后建的为新的。
    ///  IsStateOwned字段 为判断 是否国有   0为非国有，1为国有
    /// </summary>
    private DataTable GetTowerOwner_Old(string gys, string networktype)
    {
        string where = "";
        if (!string.IsNullOrEmpty(gys))
            where += " And NetworkManufacturer='" + gys + "' ";
        if (!string.IsNullOrEmpty(networktype))
            where += " And NetworkSubsystem='" + networktype + "' ";
        string sql = "select  (select COUNT(0) from dbo.TowerStationInfo where BuildTime<='2020-06-01' AND  IsStateOwned=0  " + where + " )非国有 ,(select COUNT(0) from dbo.TowerStationInfo where BuildTime<='2020-06-01' AND IsStateOwned=1  " + where + " )国有";
        return publicDbOpClass.DataTableQuary(sql);
    }
    /// <summary>
    /// 获取新铁塔的智能情况信息。右边  右上区域数据源
    /// 2020年6月1日前建的是旧的   以后建的为新的。
    /// IsIntelligence 为判断 是否智能   0为非智能，1为智能
    /// </summary>
    private DataTable GetTowerIsIntelligence_New(string gys, string networktype)
    {
        string where = "";
        if (!string.IsNullOrEmpty(gys))
            where += " And NetworkManufacturer='" + gys + "' ";
        if (!string.IsNullOrEmpty(networktype))
            where += " And NetworkSubsystem='" + networktype + "' ";
        string sql = "select  (select COUNT(0) from dbo.TowerStationInfo where  BuildTime>'2020-06-01' And IsIntelligence=0  " + where + " )非智能 ,(select COUNT(0) from dbo.TowerStationInfo where  BuildTime>'2020-06-01' And  IsIntelligence=1  " + where + " )智能";
        return publicDbOpClass.DataTableQuary(sql);
    }
    /// <summary>
    /// 获取旧铁塔的智能情况信息。右边  右中区域数据源
    /// 2020年6月1日前建的是旧的   以后建的为新的。
    ///  IsIntelligence 为判断 是否智能   0为非智能，1为智能
    /// </summary>
    private DataTable GetTowerIsIntelligence_Old(string gys, string networktype)
    {
        string where = "";
        if (!string.IsNullOrEmpty(gys))
            where += " And NetworkManufacturer='" + gys + "' ";
        if (!string.IsNullOrEmpty(networktype))
            where += " And NetworkSubsystem='" + networktype + "' ";
        string sql = "select  (select COUNT(0) from dbo.TowerStationInfo where BuildTime<='2020-06-01' AND  IsIntelligence=0  " + where + " )非智能 ,(select COUNT(0) from dbo.TowerStationInfo where BuildTime<='2020-06-01' AND IsIntelligence=1  " + where + " )智能";
        return publicDbOpClass.DataTableQuary(sql);
    }
    /// <summary>
    /// 获取新铁塔的建造状态。右边  右下区域数据源
    /// 2020年6月1日前建的是旧的   以后建的为新的。
    /// BuildState 为判断 建造状态  已建成   建设中    规划中 
    /// </summary>
    private DataTable GetTowerBuildState_New(string gys, string networktype)
    {
        string where = "";
        if (!string.IsNullOrEmpty(gys))
            where += " And NetworkManufacturer='" + gys + "' ";
        if (!string.IsNullOrEmpty(networktype))
            where += " And NetworkSubsystem='" + networktype + "' ";
        string sql = "select  (select COUNT(0) from dbo.TowerStationInfo where  BuildTime>'2020-06-01' And BuildState='1'  " + where + " )已建成 ,(select COUNT(0) from dbo.TowerStationInfo where  BuildTime>'2020-06-01' And  BuildState='2'  " + where + " )建设中,(select COUNT(0) from dbo.TowerStationInfo where  BuildTime>'2020-06-01' And  BuildState='3'  " + where + " )规划中";
        return publicDbOpClass.DataTableQuary(sql);
    }
    /// <summary>
    /// 获取地图展示经纬度 铁塔数据源
    /// </summary>
    /// <param name="gys"></param>
    /// <param name="networktype"></param>
    /// <returns></returns>
    private DataTable GetMapData_Tower(string gys, string networktype)
    {
        string sql = "select TowerStationGUID, Province, City, Area, Address, Liaison, Phone, Email, PlaceMode, BuildState, BuildTime, IsStateOwned, IsIntelligence, NominalLongitude, NominalDimension, GPSLongitude, GPSDimension, MapLongitude, MapDimension, MapDatum, RSUManufacturer, Name from dbo.TowerStationInfo where 1=1 ";
        string where = "";
        if (!string.IsNullOrEmpty(gys))
            where += " And NetworkManufacturer='" + gys + "' ";
        if (!string.IsNullOrEmpty(networktype))
            where += " And NetworkSubsystem='" + networktype + "' ";
        return publicDbOpClass.DataTableQuary(sql + where);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterinfo"></param>
    /// <returns></returns>
    private DataSet GetChongTu(string filterinfo)
    {
        DataSet ds = new DataSet();
        string sql = "select * from V_TowerStation";
        DataTable dt = publicDbOpClass.DataTableQuary(sql);
        ds.Tables.Add(dt);
        return ds;
    }
    #endregion 


}
