namespace cn.justwin.Files
{
    using cn.justwin.DAL;
    using com.jwsoft.pm.entpm.action;
    using System;
    using System.Collections;
    using System.Data;

    public class fileTypeService
    {
        public bool veriFileType(int tid, int _typeid)
        {
            ArrayList list = new ArrayList();
            list.Add(string.Concat(new object[] { "SELECT * FROM EPM_Datum_Affair eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM v_Quality_Goal eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            string saftyViewName = SafetyMeasureAction.GetSaftyViewName();
            list.Add(string.Concat(new object[] { "SELECT * FROM ", saftyViewName, " eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_V_ScienceInnovate eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_V_ExpertProject eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_TechnologyCriterion eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_TechnologyManage eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_V_TechnologyJD eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_Summary eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_ItemInspect eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            list.Add(string.Concat(new object[] { "SELECT * FROM Prj_ItemProg eda  LEFT JOIN XPM_Basic_CodeList xbcl ON xbcl.CodeID=eda.filesType LEFT JOIN XPM_Basic_CodeType xbct  ON xbct.TypeID = xbcl.TypeID WHERE xbct.SignCode='DocumentType' AND eda.filesType=", tid, " AND xbcl.TypeID=", _typeid }));
            for (int i = 0; i < list.Count; i++)
            {
                DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, list[i].ToString().ToString(), null);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

