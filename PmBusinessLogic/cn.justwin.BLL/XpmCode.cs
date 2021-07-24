namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using System;
    using System.Runtime.CompilerServices;

    public class XpmCode
    {
        public static explicit operator XpmCode(XPM_Basic_CodeList _code)
        {
            XpmCode code = null;
            if (_code != null)
            {
                code = new XpmCode {
                    NoteID = _code.NoteID,
                    CodeID = _code.CodeID,
                    TypeID = _code.TypeID,
                    CodeName = _code.CodeName
                };
            }
            return code;
        }

        public int CodeID { get; set; }

        public string CodeName { get; set; }

        public int NoteID { get; set; }

        public int TypeID { get; set; }
    }
}

