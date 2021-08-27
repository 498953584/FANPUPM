using cn.justwin.BLL;
using com.jwsoft.pm.data;
using DomainServices.cn.justwin.Domain.EasyUI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.SessionState;

public partial class OaSysAdminUserManageJurisdictionSettings : NBasePage, IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            JurisdictionBind();
        }
    }

    /// <summary>
    /// 数据绑定
    /// </summary>
    private void JurisdictionBind()
    {
        var id = Request.QueryString["id"];
        if (string.IsNullOrEmpty(id))
        {
            return;
        }

        var strSql = @"
SELECT province_id,name FROM dbo.province ORDER BY province_id;
SELECT province_id,name,city_id FROM dbo.city ORDER BY city_id;
SELECT city_id,name,country_id FROM dbo.country ORDER BY country_id;
SELECT AreaId FROM dbo.UserJurisdiction WHERE v_yhdm='" + id + "';";
        var ds = publicDbOpClass.DataSetQuary(strSql);
        HidJurisdiction.Value = new Jurisdiction().GetJson(ds.Tables[0], ds.Tables[1], ds.Tables[2], ds.Tables[3]);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        var id = Request.QueryString["id"];
        var sqlList = new List<string>();
        if (string.IsNullOrEmpty(id))
        {
            return;
        }
        sqlList.Add("DELETE FROM dbo.UserJurisdiction WHERE v_yhdm = '" + id + "';");
        var dt = new DataTable();
        if (!string.IsNullOrEmpty(HidChecked.Value))
        {
            dt = JsonConvert.DeserializeObject<DataTable>(HidChecked.Value);
        }

        var dc = new DataColumn("v_yhdm", typeof(string))
        {
            DefaultValue = id
        };
        dt.Columns.Add(dc);

        RegisterScript(publicDbOpClass.SqlBulkCopy(sqlList.ToArray(), dt, "UserJurisdiction", null)
            ? "top.ui.winSuccess({ parentName: '_userlist'})"
            : "top.ui.alert('保存失败');");
    }
    
}