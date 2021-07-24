namespace cn.justwin.BLL
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcelHeader
    {
        private ExcelHeader()
        {
        }

        public static ExcelHeader Create(string headerColName, int headerRowIndex, int colSpan, int startColSpanIndex, int rowSpan)
        {
            return new ExcelHeader { headerColName = headerColName, headerRowIndex = headerRowIndex, colSpan = colSpan, startColSpanIndex = startColSpanIndex, rowSpan = rowSpan };
        }

        public int colSpan { get; set; }

        public string headerColName { get; set; }

        public int headerRowIndex { get; set; }

        public int rowSpan { get; set; }

        public int startColSpanIndex { get; set; }
    }
}

