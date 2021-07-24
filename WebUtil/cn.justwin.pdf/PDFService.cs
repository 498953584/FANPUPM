namespace cn.justwin.pdf
{
    using cn.justwin.Web;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// pdf文件操作
    /// </summary>
    public class PDFService
    {
        private List<string> imageWatermark;
        private string inputFile;
        private string outputFile;
        private string textWatermark;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inputFile"></param>
        public PDFService(string inputFile)
        {
            if (string.IsNullOrWhiteSpace(inputFile))
            {
                throw new ArgumentNullException("文件路径不能为空");
            }
            this.inputFile = inputFile;
        }

        /// <summary>
        /// 添加图片水印,水平间距10f的一排水印
        /// </summary>
        private void AddImageWatermark(PdfReader pdfReader, PdfStamper pdfStamper, List<string> imageWatermark, Point imgPoint, float rotation)
        {
            int numberOfPages = pdfReader.NumberOfPages;
            Rectangle pageSize = pdfReader.GetPageSize(1);
            float width = pageSize.Width;
            float height = pageSize.Height;
            float left = imgPoint.Left;
            float top = imgPoint.Top;
            for (int i = 1; i <= 1; i++)
            {
                int num4 = 0;
                top = imgPoint.Top;
                int count = imageWatermark.Count;
                float newWidth = 60f;
                float newHeight = 20f;
                foreach (string str in imageWatermark)
                {
                    PdfContentByte overContent = pdfStamper.GetOverContent(i);
                    try
                    {
                        Image image = this.CreateImage(str, left, top, rotation);
                        image.ScaleAbsolute(newWidth, newHeight);
                        overContent.AddImage(image);
                        DataRow row = this.auditInfo.Rows[num4];
                        string textWatermark = DateTime.Parse(row["AuditDate"].ToString()).ToString("yyyy.MM.dd HH:mm:ss");
                        this.AddTextWatermark(pdfReader, pdfStamper, textWatermark, new Point(135f, top + 10f), 0f);
                        string str3 = StripTagsCharArray(row["AuditInfo"].ToString());
                        this.AddTextWatermark(pdfReader, pdfStamper, str3, new Point(135f, top - 1f), 0f);
                        top -= newHeight + 2f;
                    }
                    catch
                    {
                    }
                    num4++;
                }
            }
        }

        /// <summary>
        /// 添加文字水印
        /// </summary>
        private void AddTextWatermark(PdfReader pdfReader, PdfStamper pdfStamper, string textWatermark, Point textPoint, float textRotation)
        {
            int numberOfPages = pdfReader.NumberOfPages;
            Rectangle pageSize = pdfReader.GetPageSize(1);
            float width = pageSize.Width;
            float height = pageSize.Height;
            BaseFont bf = BaseFont.CreateFont(@"C:\WINDOWS\FONTS\STZHONGS.TTF", "Identity-H", false);
            int length = textWatermark.Length;
            for (int i = 1; i <= 1; i++)
            {
                PdfContentByte overContent = pdfStamper.GetOverContent(i);
                overContent.BeginText();
                overContent.SetColorFill(BaseColor.BLACK);
                overContent.SetFontAndSize(bf, 8f);
                overContent.SetTextMatrix(20f, 25f);
                float x = (textPoint == null) ? 100f : textPoint.Left;
                float y = (textPoint == null) ? 100f : textPoint.Top;
                if (x < 0f)
                {
                    x = width + x;
                }
                overContent.ShowTextAligned(0, textWatermark, x, y, textRotation);
                overContent.EndText();
            }
        }

        /// <summary>
        /// 水印文件覆盖原文件
        /// </summary>
        /// <param name="ouputFilePath">水印文件</param>
        private void CoverInputFile(string ouputFilePath)
        {
            FileInfo info = new FileInfo(ouputFilePath);
            FileInfo info2 = new FileInfo(this.inputFile);
            try
            {
                if (info.Exists && info2.Exists)
                {
                    Thread.Sleep(0x7d0);
                    info2.Delete();
                    info.MoveTo(this.inputFile);
                }
            }
            catch
            {
                try
                {
                    string name = info2.Name;
                    name = name.Substring(0, name.IndexOf('.')) + "(签章)" + info2.Extension;
                    string destFileName = this.inputFile.Substring(0, this.inputFile.IndexOf('/') + 1) + name;
                    info.MoveTo(destFileName);
                }
                catch (Exception exception)
                {
                    Log4netHelper.Error(exception, "pdfMoveDeleteError", "");
                }
            }
        }

        /// <summary>
        /// 创建空内容PDF文件，路径与inputFile路径一致，并返回路径
        /// 此pdf文件用于打水印后覆盖inputFile
        /// </summary>
        private string CreateEmptyPDF()
        {
            Document document = new Document();
            string path = this.inputFile.Substring(0, this.inputFile.LastIndexOf(@"\") + 1) + Guid.NewGuid().ToString() + ".pdf";
            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();
            document.Add(new Paragraph(path));
            document.Close();
            return path;
        }

        /// <summary>
        /// 创建水印图片
        /// </summary>
        /// <returns></returns>
        private Image CreateImage(string imgPath, float left, float top, float rotation)
        {
            Image instance = Image.GetInstance(imgPath);
            instance.GrayFill = 20f;
            instance.ScalePercent(80f);
            instance.Rotation = rotation;
            instance.SetAbsolutePosition(left, top);
            return instance;
        }

        private void FindText(PdfReader pdfReader, string text)
        {
        }

        /// <summary>
        /// 对原pdf文件打水印（图片水印和文字水印）
        /// </summary>
        /// <returns></returns>
        public bool PDFWatermark(float imageRotation, float textRotation, Point imagePoint, Point textPoint)
        {
            PdfReader reader = null;
            PdfStamper pdfStamper = null;
            bool flag;
            string path = this.CreateEmptyPDF();
            try
            {
                reader = new PdfReader(this.inputFile);
                pdfStamper = new PdfStamper(reader, new FileStream(path, FileMode.Create));
                this.AddImageWatermark(reader, pdfStamper, this.imageWatermark, imagePoint, imageRotation);
                flag = true;
            }
            catch (Exception exception)
            {
                exception.Message.Trim();
                flag = false;
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
                this.CoverInputFile(path);
            }
            return flag;
        }

        /// <summary>
        /// 字符串格式换行(复制stringUtility)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        /// <summary>
        /// 去除HTML标签获得纯文本内容  
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static string StripTagsCharArray(string source)
        {
            char[] chArray = new char[source.Length];
            int index = 0;
            bool flag = false;
            for (int i = 0; i < source.Length; i++)
            {
                char ch = source[i];
                switch (ch)
                {
                    case '<':
                        flag = true;
                        break;

                    case '>':
                        flag = false;
                        break;

                    default:
                        if (!flag)
                        {
                            chArray[index] = ch;
                            index++;
                        }
                        break;
                }
            }
            return new string(chArray, 0, index);
        }

        /// <summary>
        /// 审批信息
        /// 审批人电子签名、审批日期，审批意见
        /// </summary>
        public DataTable auditInfo { get; set; }

        /// <summary>
        /// 水印图片路径
        /// </summary>
        public List<string> ImageWatermark
        {
            get
            {
                return this.imageWatermark;
            }
            set
            {
                this.imageWatermark = value;
            }
        }

        /// <summary>
        /// 输入pdf文件（包含路径）
        /// </summary>
        public string InputFile
        {
            get
            {
                return this.inputFile;
            }
            set
            {
                this.inputFile = value;
            }
        }

        /// <summary>
        /// 输出pdf文件（包含路径）
        /// </summary>
        public string OutputFile
        {
            get
            {
                return this.outputFile;
            }
            set
            {
                this.outputFile = value;
            }
        }

        /// <summary>
        /// 水印文本
        /// </summary>
        public string TextWatermark
        {
            get
            {
                return this.textWatermark;
            }
            set
            {
                this.textWatermark = value;
            }
        }
    }
}

