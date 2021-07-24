namespace cn.justwin.Tender
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class TypeList
    {
        private TypeList()
        {
        }

        private static TypeList AddExtraType(string text, int? value)
        {
            return new TypeList { Text = text, _Value = value };
        }

        public static void BindBuildingTypeDrop(DropDownList drop)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<Basic_CodeType> list = (from m in entities.Basic_CodeType
                    where (m.TypeCode == "ConstructType") || (m.TypeCode == "DesignType")
                    orderby m.TypeCode
                    select m).ToList<Basic_CodeType>();
                drop.DataSource = list;
                drop.DataTextField = "TypeName";
                drop.DataValueField = "TypeCode";
                drop.DataBind();
                drop.Items.Insert(0, new ListItem("", ""));
            }
        }

        public static void BindDrop(DropDownList drop)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<TypeList> list = (from m in entities.Basic_CodeList
                    where m.TypeCode == "ProjectState"
                    select new TypeList { _Value = m.ItemCode, Text = m.ItemName }).ToList<TypeList>();
                list.Insert(0, AddExtraType("所有", null));
                drop.DataSource = list;
                drop.DataValueField = "Value";
                drop.DataTextField = "Text";
                drop.DataBind();
            }
        }

        public static void BindDrop(DropDownList drop, bool isProject)
        {
            List<TypeList> prjState = GetPrjState(isProject);
            drop.DataSource = prjState;
            drop.DataValueField = "Value";
            drop.DataTextField = "Text";
            drop.DataBind();
        }

        public static void BindDrop(DropDownList drop, string code, bool isXPM_Basic_CodeType)
        {
            List<TypeList> list = isXPM_Basic_CodeType ? GetCodeList2(code) : GetCodeList(code);
            drop.DataSource = list;
            drop.DataValueField = "Value";
            drop.DataTextField = "Text";
            drop.DataBind();
        }

        public static void BindDrop(DropDownList drop, bool isProject, string definedText, int? definedValue)
        {
            List<TypeList> prjState = GetPrjState(isProject);
            TypeList item = AddExtraType(definedText, definedValue);
            prjState.Insert(0, item);
            drop.DataSource = prjState;
            drop.DataValueField = "Value";
            drop.DataTextField = "Text";
            drop.DataBind();
        }

        public static void BindDrop(DropDownList drop, int[] prjTypeCodes, string definedText, int? definedValue)
        {
            List<TypeList> codeList = GetCodeList(prjTypeCodes);
            TypeList item = AddExtraType(definedText, definedValue);
            codeList.Insert(0, item);
            drop.DataSource = codeList;
            drop.DataValueField = "Value";
            drop.DataTextField = "Text";
            drop.DataBind();
        }

        public static void BindDrop(DropDownList drop, bool isProject, string definedText, int? definedValue, List<int> displayCode)
        {
            List<TypeList> prjState = GetPrjState(isProject);
            List<TypeList> list2 = new List<TypeList>();
            if (displayCode != null)
            {
                foreach (TypeList list3 in prjState)
                {
                    if (displayCode.Contains(list3._Value.Value))
                    {
                        list2.Add(list3);
                    }
                }
            }
            TypeList item = AddExtraType(definedText, definedValue);
            list2.Insert(0, item);
            drop.DataSource = list2;
            drop.DataValueField = "Value";
            drop.DataTextField = "Text";
            drop.DataBind();
        }

        public static void BindDrop(DropDownList drop, string code, string definedText, int? definedValue, bool isXPM_Basic_CodeType)
        {
            List<TypeList> list = isXPM_Basic_CodeType ? GetCodeList2(code) : GetCodeList(code);
            TypeList item = AddExtraType(definedText, definedValue);
            list.Insert(0, item);
            drop.DataSource = list;
            drop.DataValueField = "Value";
            drop.DataTextField = "Text";
            drop.DataBind();
        }

        public static void BindXmgroupDrop(DropDownList drop)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<XPM_Basic_CodeList> list = (from m in entities.XPM_Basic_CodeList
                    where ((m.TypeID == 0x92) && m.IsValid) && (m.IsVisible == true)
                    select m).ToList<XPM_Basic_CodeList>();
                drop.DataSource = list;
                drop.DataTextField = "CodeName";
                drop.DataValueField = "NoteID";
                drop.DataBind();
                drop.Items.Insert(0, new ListItem("", ""));
            }
        }

        public static List<TypeList> GetCodeList(string code)
        {
            List<TypeList> list = new List<TypeList>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Basic_CodeList
                    where m.TypeCode == code
                    select new TypeList { _Value = m.ItemCode, Text = m.ItemName }).ToList<TypeList>();
            }
        }

        public static List<TypeList> GetCodeList(int[] codes)
        {
            List<TypeList> list = new List<TypeList>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (int code in codes)
                {
                    TypeList item = (from m in entities.Basic_CodeList
                        where (m.ItemCode == code) && (m.TypeCode == "ProjectState")
                        select new TypeList { _Value = m.ItemCode, Text = m.ItemName }).FirstOrDefault<TypeList>();
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static List<TypeList> GetCodeList2(string signCode)
        {
            List<TypeList> list = new List<TypeList>();
            using (pm2Entities entities = new pm2Entities())
            {
                int typeId = GetTypeID(signCode);
                return (from m in entities.XPM_Basic_CodeList
                    where (m.TypeID == typeId) && m.IsValid
                    select new TypeList { _Value = m.CodeID, Text = m.CodeName }).ToList<TypeList>();
            }
        }

        public static string GetNameByCode(int code)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from n in entities.Basic_CodeList
                    where (n.TypeCode == "ProjectState") && (n.ItemCode == code)
                    select n.ItemName).FirstOrDefault<string>();
            }
        }

        public static List<TypeList> GetPrjState(bool isProject)
        {
            List<TypeList> list = new List<TypeList>();
            using (pm2Entities entities = new pm2Entities())
            {
                if (isProject)
                {
                    return (from m in entities.Basic_CodeList
                        where (m.TypeCode == "ProjectState") && ((m.ItemCode >= 7) && (m.ItemCode <= 13))
                        select new TypeList { _Value = m.ItemCode, Text = m.ItemName }).ToList<TypeList>();
                }
                return (from m in entities.Basic_CodeList
                    where (m.TypeCode == "ProjectState") && ((m.ItemCode < 7) || ((m.ItemCode >= 13) && (m.ItemCode != 0x11)))
                    select new TypeList { _Value = m.ItemCode, Text = m.ItemName }).ToList<TypeList>();
            }
        }

        private static int GetTypeID(string signCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.XPM_Basic_CodeType
                    where m.SignCode == signCode
                    select m.TypeID).FirstOrDefault<int>();
            }
        }

        public static string GetTypeName(string typeCode, string type, string signCode)
        {
            int noteId;
            int.TryParse(typeCode, out noteId);
            using (pm2Entities entities = new pm2Entities())
            {
                if (type == "1")
                {
                    int typeID = GetTypeID(signCode);
                    return (from m in entities.XPM_Basic_CodeList
                        where (m.CodeID == noteId) && (m.TypeID == typeID)
                        select m.CodeName).FirstOrDefault<string>();
                }
                if (type == "2")
                {
                    return (from m in entities.Basic_CodeList
                        where (m.ItemCode == noteId) && (m.TypeCode == signCode)
                        select m.ItemName).FirstOrDefault<string>();
                }
                return (from m in entities.Basic_CodeType
                    where m.TypeCode == typeCode
                    select m.TypeName).FirstOrDefault<string>();
            }
        }

        public static string GetXmlGroupName(string xmgroup)
        {
            int noteId;
            int.TryParse(xmgroup, out noteId);
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.XPM_Basic_CodeList
                    where m.NoteID == noteId
                    select m.CodeName).FirstOrDefault<string>();
            }
        }

        private int? _Value { get; set; }

        public string Text { get; set; }

        public string Value
        {
            get
            {
                if (this._Value.HasValue)
                {
                    return this._Value.ToString();
                }
                return string.Empty;
            }
        }
    }
}

