using ASP;
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class ftb_imagegallery : Page, IRequiresSessionState
    {
        private string NoFileMessage = "您没有选择文件。";
        private string UploadSuccessMessage = "上传成功";
        private string UploadFailureMessage = "上传失败。";
        private string NoImagesMessage = "没有图片,请先上传!";
        private string NoFolderSpecifiedMessage = "您要上传到的文件夹不存在。";
        private string NoFileToDeleteMessage = "您没有选中要删除的文件。";
        private string InvalidFileTypeMessage = "您无法上传这种类型的文件。";
        private string[] AcceptedFileTypes = new string[]
		{
			"jpg",
			"jpeg",
			"jpe",
			"gif",
			"png",
			"JPG",
			"JPEG",
			"JPE",
			"GIF"
		};
        private bool UploadIsEnabled = true;
        private bool DeleteIsEnabled = true;
        private string DefaultImageFolder = "Img";

        private void Page_Load(object sender, EventArgs e)
        {
            string a = base.Request["frame"] ?? "";
            if (a != "")
            {
                this.MainPage.Visible = true;
                this.iframePanel.Visible = false;
                string text = base.Request["rif"] ?? "";
                string text2 = base.Request["cif"] ?? "";
                if (text2 != "" && text != "")
                {
                    this.RootImagesFolder.Value = text;
                    this.CurrentImagesFolder.Value = text2;
                }
                else
                {
                    this.RootImagesFolder.Value = this.DefaultImageFolder;
                    this.CurrentImagesFolder.Value = this.DefaultImageFolder;
                }
                this.UploadPanel.Visible = this.UploadIsEnabled;
                this.DeleteImage.Visible = this.DeleteIsEnabled;
                string text3 = "";
                string str = ".*(";
                for (int i = 0; i < this.AcceptedFileTypes.Length; i++)
                {
                    str = str + "[\\." + this.AcceptedFileTypes[i] + "]";
                    if (i < this.AcceptedFileTypes.Length - 1)
                    {
                        str += "|";
                    }
                    text3 += this.AcceptedFileTypes[i];
                    if (i < this.AcceptedFileTypes.Length - 1)
                    {
                        text3 += ", ";
                    }
                }
                this.FileValidator.ValidationExpression = str + ")$";
                this.FileValidator.ErrorMessage = text3;
                if (!base.IsPostBack)
                {
                    this.DisplayImages();
                }
            }
        }
        public void UploadImage_OnClick(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (this.CurrentImagesFolder.Value != "")
                {
                    if (this.UploadFile.PostedFile.FileName.Trim() != "")
                    {
                        if (this.IsValidFileType(this.UploadFile.PostedFile.FileName))
                        {
                            try
                            {
                                string text = this.UploadFile.PostedFile.FileName;
                                text = text.Substring(text.LastIndexOf("\\") + 1);
                                string str = HttpContext.Current.Request.PhysicalApplicationPath;
                                str += this.CurrentImagesFolder.Value;
                                str += "\\";
                                this.UploadFile.PostedFile.SaveAs(str + text);
                                this.ResultsMessage.Text = this.UploadSuccessMessage;
                                goto IL_150;
                            }
                            catch (Exception)
                            {
                                this.ResultsMessage.Text = this.UploadFailureMessage;
                                goto IL_150;
                            }
                        }
                        this.ResultsMessage.Text = this.InvalidFileTypeMessage;
                    }
                    else
                    {
                        this.ResultsMessage.Text = this.NoFileMessage;
                    }
                }
                else
                {
                    this.ResultsMessage.Text = this.NoFolderSpecifiedMessage;
                }
            }
            else
            {
                this.ResultsMessage.Text = this.InvalidFileTypeMessage;
            }
        IL_150:
            this.DisplayImages();
        }
        public void DeleteImage_OnClick(object sender, EventArgs e)
        {
            if (this.FileToDelete.Value != "" && this.FileToDelete.Value != "undefined")
            {
                try
                {
                    string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                    string path = physicalApplicationPath + this.CurrentImagesFolder.Value + "\\" + this.FileToDelete.Value;
                    File.Delete(path);
                    this.ResultsMessage.Text = "已删除: " + this.FileToDelete.Value;
                    goto IL_AF;
                }
                catch (Exception)
                {
                    this.ResultsMessage.Text = "删除失败。";
                    goto IL_AF;
                }
            }
            this.ResultsMessage.Text = this.NoFileToDeleteMessage;
        IL_AF:
            this.DisplayImages();
        }
        private bool IsValidFileType(string FileName)
        {
            string a = FileName.Substring(FileName.LastIndexOf(".") + 1, FileName.Length - FileName.LastIndexOf(".") - 1);
            for (int i = 0; i < this.AcceptedFileTypes.Length; i++)
            {
                if (a == this.AcceptedFileTypes[i])
                {
                    return true;
                }
            }
            return false;
        }
        private string[] ReturnFilesArray()
        {
            if (this.CurrentImagesFolder.Value != "")
            {
                try
                {
                    string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                    string path = physicalApplicationPath + this.CurrentImagesFolder.Value;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string[] files = Directory.GetFiles(path, "*");
                    string[] result = files;
                    return result;
                }
                catch
                {
                    string[] result = null;
                    return result;
                }
            }
            return null;
        }
        private string[] ReturnDirectoriesArray()
        {
            if (this.CurrentImagesFolder.Value != "")
            {
                try
                {
                    string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                    string path = physicalApplicationPath + this.CurrentImagesFolder.Value;
                    string[] directories = Directory.GetDirectories(path, "*");
                    string[] result = directories;
                    return result;
                }
                catch
                {
                    string[] result = null;
                    return result;
                }
            }
            return null;
        }
        public void DisplayImages()
        {
            string[] array = this.ReturnFilesArray();
            string[] array2 = this.ReturnDirectoriesArray();
            string arg_1D_0 = HttpContext.Current.Request.PhysicalApplicationPath;
            string text;
            if (base.Request.ApplicationPath == "/")
            {
                text = base.Request.ApplicationPath;
            }
            else
            {
                text = base.Request.ApplicationPath + "/";
            }
            this.GalleryPanel.Controls.Clear();
            if ((array == null || array.Length == 0) && (array2 == null || array2.Length == 0))
            {
                this.gallerymessage.Text = this.NoImagesMessage + ": " + this.RootImagesFolder.Value;
                return;
            }
            int num = 94;
            int num2 = 94;
            if (this.CurrentImagesFolder.Value != this.RootImagesFolder.Value)
            {
                HtmlImage htmlImage = new HtmlImage();
                htmlImage.Src = text + "images/ftb/folder.up.gif";
                htmlImage.Attributes["unselectable"] = "on";
                htmlImage.Attributes["align"] = "absmiddle";
                htmlImage.Attributes["vspace"] = "36";
                string text2 = this.CurrentImagesFolder.Value.Substring(0, this.CurrentImagesFolder.Value.LastIndexOf("\\"));
                Panel panel = new Panel();
                panel.CssClass = "imageholder";
                panel.Attributes["unselectable"] = "on";
                panel.Attributes["onclick"] = "divClick(this,'');";
                panel.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"gotoFolder('",
					this.RootImagesFolder.Value,
					"','",
					text2.Replace("\\", "\\\\"),
					"');"
				});
                panel.Controls.Add(htmlImage);
                Panel panel2 = new Panel();
                panel2.CssClass = "imagespacer";
                panel2.Controls.Add(panel);
                Panel panel3 = new Panel();
                panel3.CssClass = "titleHolder";
                panel3.Controls.Add(new LiteralControl("向上"));
                panel2.Controls.Add(panel3);
                this.GalleryPanel.Controls.Add(panel2);
            }
            string[] array3 = array;
            for (int i = 0; i < array3.Length; i++)
            {
                string text3 = array3[i];
                try
                {
                    string text4 = text3.ToString();
                    text4 = text4.Substring(text4.LastIndexOf("\\") + 1);
                    string text5 = text;
                    text5 = text5.Substring(text5.LastIndexOf("\\") + 1);
                    text5 += this.CurrentImagesFolder.Value;
                    text5 += "/";
                    text5 += text4;
                    HtmlImage htmlImage2 = new HtmlImage();
                    htmlImage2.Src = text5;
                    System.Drawing.Image image = System.Drawing.Image.FromFile(text3.ToString());
                    htmlImage2.Attributes["unselectable"] = "on";
                    if (image.Width > image.Height)
                    {
                        if (image.Width > num)
                        {
                            htmlImage2.Width = num;
                            htmlImage2.Height = Convert.ToInt32(image.Height * num / image.Width);
                        }
                        else
                        {
                            htmlImage2.Width = image.Width;
                            htmlImage2.Height = image.Height;
                        }
                    }
                    else
                    {
                        if (image.Height > num2)
                        {
                            htmlImage2.Height = num2;
                            htmlImage2.Width = Convert.ToInt32(image.Width * num2 / image.Height);
                        }
                        else
                        {
                            htmlImage2.Width = image.Width;
                            htmlImage2.Height = image.Height;
                        }
                    }
                    if (htmlImage2.Height < num2)
                    {
                        htmlImage2.Attributes["vspace"] = Convert.ToInt32(num2 / 2 - htmlImage2.Height / 2).ToString();
                    }
                    Panel panel4 = new Panel();
                    panel4.CssClass = "imageholder";
                    panel4.Attributes["onclick"] = "divClick(this,'" + text4 + "');";
                    panel4.Attributes["ondblclick"] = string.Concat(new string[]
					{
						"returnImage('",
						text5.Replace("\\", "/"),
						"','",
						image.Width.ToString(),
						"','",
						image.Height.ToString(),
						"');"
					});
                    panel4.Controls.Add(htmlImage2);
                    Panel panel5 = new Panel();
                    panel5.CssClass = "imagespacer";
                    panel5.Controls.Add(panel4);
                    Panel panel6 = new Panel();
                    panel6.CssClass = "titleHolder";
                    panel6.Controls.Add(new LiteralControl(string.Concat(new string[]
					{
						text4,
						"<BR>",
						image.Width.ToString(),
						"x",
						image.Height.ToString()
					})));
                    panel5.Controls.Add(panel6);
                    this.GalleryPanel.Controls.Add(panel5);
                    image.Dispose();
                }
                catch
                {
                }
            }
            this.gallerymessage.Text = "";
        }
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.DeleteImage.Click += new EventHandler(this.DeleteImage_OnClick);
            base.Load += new EventHandler(this.Page_Load);
        }
    }
