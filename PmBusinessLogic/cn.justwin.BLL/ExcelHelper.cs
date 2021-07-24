namespace cn.justwin.BLL
{
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;

    public class ExcelHelper
    {
        private static Stream ExportDataTableToExcel(DataTable sourceTable, string sheetName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream stream = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet) workbook.CreateSheet(sheetName);
            HSSFRow row = (HSSFRow) sheet.CreateRow(0);
            foreach (DataColumn column in sourceTable.Columns)
            {
                row.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }
            int rownum = 1;
            foreach (DataRow row2 in sourceTable.Rows)
            {
                HSSFRow row3 = (HSSFRow) sheet.CreateRow(rownum);
                foreach (DataColumn column2 in sourceTable.Columns)
                {
                    row3.CreateCell(column2.Ordinal).SetCellValue(row2[column2].ToString());
                }
                rownum++;
            }
            workbook.Write(stream);
            stream.Flush();
            stream.Position = 0L;
            sheet = null;
            row = null;
            workbook = null;
            return stream;
        }

        public static void ExportDataTableToExcel(DataTable sourceTable, string fileName, string sheetName)
        {
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "sheet1";
            }
            MemoryStream stream = ExportDataTableToExcel(sourceTable, sheetName) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            HttpContext.Current.Response.End();
            stream.Close();
            stream = null;
        }

        public static Stream ExportDataTableToExcelToSchedule(DataTable sourceTable, string sheetName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream stream = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet) workbook.CreateSheet(sheetName);
            HSSFRow row = (HSSFRow) sheet.CreateRow(0);
            foreach (DataColumn column in sourceTable.Columns)
            {
                row.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }
            int rownum = 1;
            foreach (DataRow row2 in sourceTable.Rows)
            {
                HSSFRow row3 = (HSSFRow) sheet.CreateRow(rownum);
                foreach (DataColumn column2 in sourceTable.Columns)
                {
                    row3.CreateCell(column2.Ordinal).SetCellValue(row2[column2].ToString());
                    if (((column2.Ordinal == 9) || (column2.Ordinal == 10)) || (((column2.Ordinal == 11) || (column2.Ordinal == 12)) || (column2.Ordinal == 13)))
                    {
                        HSSFFont font = (HSSFFont) workbook.CreateFont();
                        font.FontHeightInPoints = 3;
                        HSSFCell cell = (HSSFCell) row3.GetCell(column2.Ordinal);
                        if (cell.StringCellValue == "1")
                        {
                            font.Color = 10;
                            cell.CellStyle.SetFont(font);
                            cell.SetCellValue("●");
                        }
                        else if (cell.StringCellValue == "0")
                        {
                            font.Color = 0x7fff;
                            cell.CellStyle.SetFont(font);
                            cell.SetCellValue("●");
                        }
                    }
                }
                rownum++;
            }
            workbook.Write(stream);
            stream.Flush();
            stream.Position = 0L;
            sheet = null;
            row = null;
            workbook = null;
            return stream;
        }

        public static void ExportExcel(DataTable dtSource, string[] headerName, string[] fieldName, string[] totalField, string excelName, string browser)
        {
            if ((headerName.Length == 0) || (headerName.Length != fieldName.Length))
            {
                throw new Exception("列数不匹配！");
            }
            DataTable sourceTable = new DataTable();
            for (int i = 0; i < headerName.Length; i++)
            {
                sourceTable.Columns.Add(headerName[i]);
            }
            for (int j = 0; j < dtSource.Rows.Count; j++)
            {
                DataRow row = sourceTable.NewRow();
                if (fieldName[0] == "")
                {
                    row[0] = (j + 1).ToString();
                    for (int k = 1; k < fieldName.Length; k++)
                    {
                        row[k] = dtSource.Rows[j][fieldName[k]].ToString();
                    }
                }
                else
                {
                    for (int m = 0; m < fieldName.Length; m++)
                    {
                        row[m] = dtSource.Rows[j][fieldName[m]].ToString();
                    }
                }
                sourceTable.Rows.Add(row);
            }
            if (totalField.Length > 0)
            {
                DataRow row2 = sourceTable.NewRow();
                row2[0] = "合计";
                for (int n = 1; n < fieldName.Length; n++)
                {
                    if (totalField.Contains<string>(fieldName[n]))
                    {
                        row2[n] = dtSource.Compute("SUM(" + fieldName[n] + ")", string.Empty).ToString();
                    }
                    else
                    {
                        row2[n] = "";
                    }
                }
                sourceTable.Rows.Add(row2);
            }
            if (browser.ToUpper() != "SAFARI")
            {
                excelName = HttpUtility.UrlEncode(excelName, Encoding.UTF8);
            }
            ExportDataTableToExcel(sourceTable, excelName, "sheet1");
        }

        public static void ExportExcel(DataTable sourceTable, string title, string sheetName, string fileName, List<ExcelHeader> headers, List<int> totalColumnIndexs, int totalTextColSpan, string browser)
        {
            if ((browser.ToUpper() == "IE") || (browser.ToUpper() == "CHROME"))
            {
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }
            MemoryStream stream = GetStreamFromSourceDataTable2(sourceTable, sheetName, title, headers, totalColumnIndexs, totalTextColSpan, string.Empty) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            HttpContext.Current.Response.End();
            stream.Close();
            stream = null;
        }

        public static void ExportFooterExcel(DataTable sourceTable, string title, string sheetName, string fileName, List<ExcelHeader> headers, List<int> totalColumnIndexs, int totalTextColSpan, string SummarizingInfo, string browser)
        {
            if (browser.ToUpper() != "SAFARI")
            {
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }
            MemoryStream stream = GetStreamFromSourceDataTable2(sourceTable, sheetName, title, headers, totalColumnIndexs, totalTextColSpan, SummarizingInfo) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            HttpContext.Current.Response.End();
            stream.Close();
            stream = null;
        }

        private static HSSFRow GetHeadRow(HSSFSheet sheet)
        {
            for (int i = sheet.FirstRowNum; i < sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow) sheet.GetRow(i);
                int firstCellNum = 0;
                if (row != null)
                {
                    firstCellNum = row.FirstCellNum;
                    int count = row.Cells.Count;
                    if (count >= 3)
                    {
                        for (int j = row.FirstCellNum; j < count; j++)
                        {
                            Cell cell = row.GetCell(j);
                            if (cell == null)
                            {
                                break;
                            }
                            try
                            {
                                if (string.IsNullOrEmpty(cell.ToString()))
                                {
                                    break;
                                }
                            }
                            catch
                            {
                                break;
                            }
                            firstCellNum++;
                        }
                        if (firstCellNum == count)
                        {
                            return row;
                        }
                    }
                }
            }
            return null;
        }

        private static List<int> getMergedColumns(HSSFSheet sheet, int startRowIndex)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < sheet.NumMergedRegions; i++)
            {
                CellRangeAddress mergedRegion = sheet.GetMergedRegion(i);
                int firstColumn = mergedRegion.FirstColumn;
                int lastColumn = mergedRegion.LastColumn;
                if (mergedRegion.FirstRow == startRowIndex)
                {
                    for (int j = firstColumn + 1; j <= lastColumn; j++)
                    {
                        list.Add(j);
                    }
                }
            }
            return list;
        }

        public static int GetSheetCounts(string excelFilePath)
        {
            using (FileStream stream = File.OpenRead(excelFilePath))
            {
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                return workbook.NumberOfSheets;
            }
        }

        public static DataTable GetSheets(string excelFilePath)
        {
            using (FileStream stream = File.OpenRead(excelFilePath))
            {
                DataTable table = new DataTable();
                table.Columns.Add("sheetName");
                table.Columns.Add("sheetIndex");
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                int numberOfSheets = workbook.NumberOfSheets;
                for (int i = 0; i < numberOfSheets; i++)
                {
                    DataRow row = table.NewRow();
                    HSSFSheet sheetAt = (HSSFSheet) workbook.GetSheetAt(i);
                    if (sheetAt != null)
                    {
                        row["sheetName"] = sheetAt.SheetName;
                        row["sheetIndex"] = i.ToString();
                    }
                    table.Rows.Add(row);
                }
                return table;
            }
        }

        public static Stream GetStreamFromSourceDataTable2(DataTable sourceTable, string sheetName, string title, List<ExcelHeader> headers, List<int> totalIndexs, int totalTextColSpan, string SummarizingInfo)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream stream = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet) workbook.CreateSheet(sheetName);
            HSSFRow row = (HSSFRow) sheet.CreateRow(0);
            sheet.AddMergedRegion(new Region(0, 0, 0, sourceTable.Columns.Count - 1));
            HSSFCell cell = (HSSFCell) row.CreateCell(0);
            HSSFCellStyle style = (HSSFCellStyle) workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            HSSFFont font = workbook.CreateFont() as HSSFFont;
            font.FontHeight = 400;
            style.SetFont(font);
            cell.CellStyle = style;
            cell.SetCellValue(title);
            row.Height = 500;
            if (headers == null)
            {
                headers = new List<ExcelHeader>();
                foreach (DataColumn column in sourceTable.Columns)
                {
                    ExcelHeader item = ExcelHeader.Create(column.ColumnName, 1, 0, 0, 1);
                    headers.Add(item);
                }
            }
            int colFrom = 0;
            HSSFCellStyle style2 = workbook.CreateCellStyle() as HSSFCellStyle;
            style2.Alignment = HorizontalAlignment.CENTER;
            style2.VerticalAlignment = VerticalAlignment.CENTER;
            HSSFFont font2 = workbook.CreateFont() as HSSFFont;
            font2.FontHeight = 200;
            style2.SetFont(font2);
            foreach (ExcelHeader header2 in headers)
            {
                int startColSpanIndex = 0;
                int rowIndex = 0;
                if (header2.colSpan != 0)
                {
                    sheet.AddMergedRegion(new Region(header2.headerRowIndex, header2.startColSpanIndex, header2.headerRowIndex, (header2.colSpan + header2.startColSpanIndex) - 1));
                    startColSpanIndex = header2.startColSpanIndex;
                    rowIndex = header2.headerRowIndex;
                }
                else
                {
                    if (header2.rowSpan != 0)
                    {
                        int num4 = (from h in headers group h by h.headerRowIndex).Count<System.Linq.IGrouping<int, ExcelHeader>>();
                        if (header2.rowSpan > num4)
                        {
                            throw new Exception("Excel表头合并失败!!!");
                        }
                        int rowFrom = (num4 - header2.rowSpan) + headers.Min<ExcelHeader>(((System.Func<ExcelHeader, int>) (h => h.headerRowIndex)));
                        sheet.AddMergedRegion(new Region(rowFrom, colFrom, (rowFrom + header2.rowSpan) - 1, colFrom));
                        rowIndex = rowFrom;
                    }
                    else
                    {
                        rowIndex = header2.headerRowIndex;
                    }
                    startColSpanIndex = colFrom;
                    colFrom++;
                }
                HSSFCell cell2 = null;
                if (sheet.GetRow(rowIndex) == null)
                {
                    cell2 = sheet.CreateRow(rowIndex).CreateCell(startColSpanIndex) as HSSFCell;
                    cell2.SetCellValue(header2.headerColName);
                    cell2.CellStyle = style2;
                }
                else
                {
                    cell2 = sheet.GetRow(rowIndex).CreateCell(startColSpanIndex) as HSSFCell;
                    cell2.SetCellValue(header2.headerColName);
                    cell2.CellStyle = style2;
                }
            }
            int rownum = headers.Max<ExcelHeader>(((System.Func<ExcelHeader, int>) (h => h.headerRowIndex))) + 1;
            foreach (DataRow row2 in sourceTable.Rows)
            {
                HSSFRow row3 = (HSSFRow) sheet.CreateRow(rownum);
                foreach (DataColumn column2 in sourceTable.Columns)
                {
                    HSSFCell cell3 = row3.CreateCell(column2.Ordinal) as HSSFCell;
                    if ((totalIndexs != null) && totalIndexs.Contains(column2.Ordinal))
                    {
                        cell3.SetCellType(CellType.NUMERIC);
                        double num7 = Convert.ToDouble(row2[column2.ColumnName]);
                        cell3.SetCellValue(num7);
                    }
                    else
                    {
                        cell3.SetCellValue(row2[column2.ColumnName].ToString());
                    }
                }
                rownum++;
            }
            if ((totalIndexs != null) && (totalIndexs.Count != 0))
            {
                HSSFRow row4 = sheet.CreateRow(sheet.LastRowNum + 1) as HSSFRow;
                sheet.AddMergedRegion(new Region(sheet.LastRowNum, 0, sheet.LastRowNum, totalTextColSpan - 1));
                HSSFCell cell4 = row4.CreateCell(0) as HSSFCell;
                cell4.SetCellValue("合计");
                cell4.CellStyle = style2;
                int num8 = headers.Max<ExcelHeader>(((System.Func<ExcelHeader, int>) (h => h.headerRowIndex))) + 2;
                int rowNum = row4.RowNum;
                foreach (DataColumn column3 in sourceTable.Columns)
                {
                    if (totalIndexs.Contains(column3.Ordinal))
                    {
                        int num10 = column3.Ordinal + 0x61;
                        string str = ((char) num10).ToString();
                        row4.CreateCell(column3.Ordinal).CellFormula = string.Concat(new object[] { "Sum(", str, num8, ":", str, rowNum, ")" });
                    }
                }
            }
            if (!string.IsNullOrEmpty(SummarizingInfo))
            {
                HSSFRow row5 = sheet.CreateRow(sheet.LastRowNum + 1) as HSSFRow;
                sheet.AddMergedRegion(new Region(sheet.LastRowNum, 0, sheet.LastRowNum, sourceTable.Columns.Count - 1));
                HSSFCell cell5 = row5.CreateCell(0) as HSSFCell;
                cell5.SetCellValue(SummarizingInfo);
                HSSFCellStyle style3 = workbook.CreateCellStyle() as HSSFCellStyle;
                style3.Alignment = HorizontalAlignment.LEFT;
                HSSFFont font3 = workbook.CreateFont() as HSSFFont;
                font3.FontHeight = 0x12;
                cell5.CellStyle = style3;
            }
            workbook.Write(stream);
            stream.Flush();
            stream.Position = 0L;
            sheet = null;
            workbook = null;
            return stream;
        }

        public static DataTable ImportDataTableFromExcel(Stream excelFileStream, string sheetName, int headerRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            HSSFSheet sheet = (HSSFSheet) workbook.GetSheet(sheetName);
            DataTable table = new DataTable();
            HSSFRow row = (HSSFRow) sheet.GetRow(headerRowIndex);
            int lastCellNum = row.LastCellNum;
            for (int i = row.FirstCellNum; i < lastCellNum; i++)
            {
                DataColumn column = new DataColumn(row.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int j = sheet.FirstRowNum + 1; j <= sheet.LastRowNum; j++)
            {
                HSSFRow row2 = (HSSFRow) sheet.GetRow(j);
                DataRow row3 = table.NewRow();
                for (int k = row2.FirstCellNum; k < lastCellNum; k++)
                {
                    row3[k] = row2.GetCell(k).ToString();
                }
            }
            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static DataTable ImportDataTableFromExcel(string excelFilePath, int sheetIndex, List<string> errors)
        {
            using (FileStream stream = File.OpenRead(excelFilePath))
            {
                return ImportDataTableFromExcel(stream, 0, sheetIndex, errors);
            }
        }

        public static DataTable ImportDataTableFromExcel(string excelFilePath, string sheetName, int headerRowIndex)
        {
            using (FileStream stream = File.OpenRead(excelFilePath))
            {
                return ImportDataTableFromExcel(stream, sheetName, headerRowIndex);
            }
        }

        public static DataTable ImportDataTableFromExcel(Stream excelFileStream, int startRowIndex, int sheetIndex, List<string> errors)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            HSSFSheet sheetAt = (HSSFSheet) workbook.GetSheetAt(sheetIndex);
            DataTable table = new DataTable();
            if (sheetAt.LastRowNum != 0)
            {
                HSSFRow row = (HSSFRow) sheetAt.GetRow(startRowIndex);
                int columnCount = 0;
                if (row == null)
                {
                    string error = "在你的Excel中：工作表" + sheetAt.SheetName + "中找不到标题行,请修改！";
                    if (!errors.Any<string>(er => (er == error)))
                    {
                        throw new Exception(error);
                    }
                }
                else
                {
                    columnCount = row.LastCellNum;
                    int num2 = 1;
                    List<int> mergedColumns = getMergedColumns(sheetAt, startRowIndex);
                    for (int i = row.FirstCellNum; i < columnCount; i++)
                    {
                        if (!isValidColumn(mergedColumns, i))
                        {
                            Cell cell = row.GetCell(i);
                            if (!sheetAt.IsColumnHidden(i))
                            {
                                try
                                {
                                    DataColumn column = new DataColumn(cell.ToString());
                                    table.Columns.Add(column);
                                }
                                catch
                                {
                                    DataColumn column2 = null;
                                    if (cell == null)
                                    {
                                        column2 = new DataColumn("空白列" + num2);
                                        num2++;
                                        table.Columns.Add(column2);
                                    }
                                    else
                                    {
                                        ReColumnName(cell.ToString(), columnCount, table);
                                    }
                                }
                            }
                        }
                    }
                    for (int j = row.RowNum + 1; j <= sheetAt.LastRowNum; j++)
                    {
                        HSSFRow row2 = (HSSFRow) sheetAt.GetRow(j);
                        if (row2 != null)
                        {
                            DataRow row3 = table.NewRow();
                            int num5 = 0;
                            for (int k = row2.FirstCellNum; k < columnCount; k++)
                            {
                                if (isValidColumn(mergedColumns, k))
                                {
                                    continue;
                                }
                                Cell cell2 = row2.GetCell(k);
                                if (sheetAt.IsColumnHidden(k))
                                {
                                    continue;
                                }
                                if (cell2 != null)
                                {
                                    switch (cell2.CellType)
                                    {
                                        case CellType.Unknown:
                                            row3[num5] = "Unknown";
                                            break;

                                        case CellType.NUMERIC:
                                            if (!DateUtil.IsCellDateFormatted(cell2))
                                            {
                                                goto Label_0264;
                                            }
                                            row3[num5] = DateUtil.GetJavaDate(cell2.NumericCellValue);
                                            break;

                                        case CellType.STRING:
                                            row3[num5] = cell2.ToString();
                                            break;

                                        case CellType.FORMULA:
                                            row3[num5] = cell2.NumericCellValue;
                                            break;

                                        case CellType.BLANK:
                                            row3[num5] = "";
                                            break;

                                        case CellType.BOOLEAN:
                                            row3[num5] = cell2.BooleanCellValue;
                                            break;

                                        case CellType.ERROR:
                                            row3[num5] = cell2.CellErrorValue;
                                            break;
                                    }
                                }
                                goto Label_029B;
                            Label_0264:
                                row3[num5] = cell2.NumericCellValue;
                            Label_029B:
                                num5++;
                            }
                            if (row2.Cells.Count > 0)
                            {
                                table.Rows.Add(row3);
                            }
                        }
                    }
                }
            }
            excelFileStream.Close();
            workbook = null;
            sheetAt = null;
            return table;
        }

        public static DataTable ImportDataTableFromExcel(string excelFilePath, int startRowIndex, int sheetIndex, List<string> errors)
        {
            using (FileStream stream = File.OpenRead(excelFilePath))
            {
                return ImportDataTableFromExcel(stream, startRowIndex, sheetIndex, errors);
            }
        }

        private static bool isValidColumn(List<int> mergedColumns, int columnIndex)
        {
            bool flag = false;
            if (((mergedColumns != null) && (mergedColumns.Count > 0)) && (mergedColumns.IndexOf(columnIndex) != -1))
            {
                flag = true;
            }
            return flag;
        }

        private static void ReColumnName(string name, int columnCount, DataTable table)
        {
            for (int i = 1; i < columnCount; i++)
            {
                try
                {
                    table.Columns.Add(name + i.ToString());
                    break;
                }
                catch
                {
                }
            }
        }

        public static void RenderToBrowser(MemoryStream ms, HttpContext context, string fileName)
        {
            if ((context.Request.Browser.Browser == "IE") || (context.Request.Browser.Browser == "CHROME"))
            {
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }
            context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
            context.Response.BinaryWrite(ms.ToArray());
        }

        public static MemoryStream RenderToExcel(IList<DataTable> tableList)
        {
            MemoryStream stream = new MemoryStream();
            int num = 1;
            using (Workbook workbook = new HSSFWorkbook())
            {
                foreach (DataTable table in tableList)
                {
                    Sheet sheet = workbook.CreateSheet("sheet" + num);
                    Row row = sheet.CreateRow(0);
                    foreach (DataColumn column in table.Columns)
                    {
                        row.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                    }
                    int rownum = 1;
                    foreach (DataRow row2 in table.Rows)
                    {
                        Row row3 = sheet.CreateRow(rownum);
                        foreach (DataColumn column2 in table.Columns)
                        {
                            row3.CreateCell(column2.Ordinal).SetCellValue(row2[column2].ToString());
                        }
                        rownum++;
                    }
                    num++;
                }
                workbook.Write(stream);
                stream.Flush();
                stream.Position = 0L;
            }
            return stream;
        }
    }
}

