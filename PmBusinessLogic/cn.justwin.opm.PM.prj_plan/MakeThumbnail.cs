namespace cn.justwin.opm.PM.prj_plan
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.action;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Web;

    public class MakeThumbnail : AnnexAction
    {
        private AnnexAction annexAction;
        private PrjInfoPhotoAction intendancePhotoListAction;

        public MakeThumbnail()
        {
            this.intendancePhotoListAction = new PrjInfoPhotoAction();
        }

        public MakeThumbnail(AnnexAction annexAction)
        {
            this.intendancePhotoListAction = new PrjInfoPhotoAction();
            this.annexAction = annexAction;
        }

        public int AddAnnex(HttpPostedFile postedFile, AnnexInfo annexInfo, AnnexModuleSettingsInfo settingsInfo)
        {
            if (this.AddAnnexs(postedFile, annexInfo, settingsInfo) == 1)
            {
                AnnexInfo singleAnnexInfo = this.annexAction.GetSingleAnnexInfo(annexInfo.AnnexCode);
                string thumbnailPath = HttpContext.Current.Server.MapPath(singleAnnexInfo.FilePath);
                string originalImagePath = thumbnailPath + singleAnnexInfo.AnnexName;
                string thumbnailName = Guid.NewGuid().ToString() + ".jpg";
                if (this.MakeToThumbnail(originalImagePath, thumbnailPath, thumbnailName, 80, 0x37, "HW") && this.SaveToDataBase(singleAnnexInfo.AnnexCode, singleAnnexInfo.FilePath, thumbnailName))
                {
                    return 1;
                }
            }
            return 0;
        }

        public int AddAnnexs(HttpPostedFile postedFile, AnnexInfo annexInfo, AnnexModuleSettingsInfo settingsInfo)
        {
            FileUpload upload = new FileUpload {
                ExtName = settingsInfo.ExtName,
                FileSize = settingsInfo.FileSize,
                UploadPath = settingsInfo.FilePath + annexInfo.AnnexType.ToString() + "/"
            };
            if (upload.UploadValidator(postedFile))
            {
                annexInfo.OriginalName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1);
                string fileName = "";
                fileName = ((DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0')) + "_" + DateTime.Now.ToLongTimeString().Replace(":", "")) + annexInfo.OriginalName;
                annexInfo.AnnexName = fileName;
                annexInfo.FilePath = settingsInfo.FilePath + annexInfo.AnnexType.ToString() + "/" + fileName.Substring(0, 6) + "/";
                annexInfo.FileSize = postedFile.ContentLength;
                string str2 = "";
                str2 = "insert into XPM_Basic_AnnexList(AnnexCode,ModuleID,RecordCode,AnnexType,FileCode,AnnexName,OriginalName,FilePath,FileSize,AddDate,State,HumanCode,Remark)";
                string str3 = str2;
                object obj2 = str3 + " values('" + annexInfo.AnnexCode.ToString() + "'," + annexInfo.ModuleID.ToString() + ",'" + annexInfo.RecordCode.ToString() + "'," + annexInfo.AnnexType.ToString() + ",'" + annexInfo.FileCode + "',";
                string str4 = string.Concat(new object[] { obj2, "'", annexInfo.AnnexName, "','", annexInfo.OriginalName, "','", annexInfo.FilePath, "',", annexInfo.FileSize, ",'", annexInfo.AddDate.ToShortDateString(), "'," });
                if (publicDbOpClass.ExecSqlString(str4 + "-1,'" + annexInfo.HumanCode + "','" + annexInfo.Remark + "') ") != 1)
                {
                    return -2;
                }
                if (upload.Upload(postedFile, fileName, true))
                {
                    return publicDbOpClass.ExecSqlString("update XPM_Basic_AnnexList set State = 1 where AnnexCode = '" + annexInfo.AnnexCode.ToString() + "'");
                }
            }
            return -1;
        }

        public int AddIntendancePhotoListAction(HttpPostedFile postedFile, PrjInfoPhotoModel annexInfo)
        {
            if (this.intendancePhotoListAction.AddpHoteInfo(postedFile, annexInfo) == 1)
            {
                PrjInfoPhotoModel singlePhotoInfo = this.intendancePhotoListAction.GetSinglePhotoInfo(annexInfo.UID);
                string thumbnailPath = HttpContext.Current.Server.MapPath(singlePhotoInfo.PhotoPath);
                string originalImagePath = thumbnailPath + singlePhotoInfo.PhotoName;
                string thumbnailName = Guid.NewGuid().ToString() + ".jpg";
                if (this.MakeToThumbnail(originalImagePath, thumbnailPath, thumbnailName, 150, 150, "HW") && this.SavePhotoToDataBase(singlePhotoInfo.UID, singlePhotoInfo.PhotoPath, thumbnailName))
                {
                    return 1;
                }
            }
            return 0;
        }

        public bool AddThumbnai(Thumbnai thumbnai)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into XPM_Basic_Thumbnai (");
            builder.Append("ThumbnaiCode,ShowContent,ThumbnaImgPath,ThumbnaName,AddDate,ImgPath,Remark");
            builder.Append(") values('");
            builder.Append(thumbnai.ThumbnaiCode + "','");
            builder.Append(thumbnai.ShowContent + "','");
            builder.Append(thumbnai.ThumbnaImgPath + "','");
            builder.Append(thumbnai.ThumbnaName + "','");
            builder.Append(thumbnai.AddDate + "','");
            builder.Append(thumbnai.ImgPath + "','");
            builder.Append(thumbnai.Remark + "'");
            builder.Append(")");
            return (publicDbOpClass.ExecSqlString(builder.ToString()) == 1);
        }

        public bool DelThumbnai(string thumbnaiCode)
        {
            FileUpload upload = new FileUpload();
            DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_Thumbnai where ThumbnaiCode='" + thumbnaiCode + "'");
            string strFileName = table.Rows[0]["ThumbnaImgPath"].ToString() + table.Rows[0]["ThumbnaName"].ToString();
            if (upload.Delete(strFileName))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("delete from XPM_Basic_Thumbnai where ThumbnaiCode = '");
                builder.Append(thumbnaiCode + "'");
                if (publicDbOpClass.ExecSqlString(builder.ToString()) == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool MakeToThumbnail(string originalImagePath, string thumbnailPath, string thumbnailName, int width, int height, string mode)
        {
            try
            {
                Image image2;
                string str3;
                Image image = Image.FromFile(originalImagePath);
                int num = width;
                int num2 = height;
                int x = 0;
                int y = 0;
                int num5 = image.Width;
                int num6 = image.Height;
                if (((str3 = mode) != null) && (str3 != "HW"))
                {
                    if (!(str3 == "W"))
                    {
                        if (str3 == "H")
                        {
                            goto Label_007F;
                        }
                        if (str3 == "Cut")
                        {
                            goto Label_0092;
                        }
                    }
                    else
                    {
                        num2 = (image.Height * width) / image.Width;
                    }
                }
                goto Label_00F1;
            Label_007F:
                num = (image.Width * height) / image.Height;
                goto Label_00F1;
            Label_0092:
                if ((((double) image.Width) / ((double) image.Height)) > (((double) num) / ((double) num2)))
                {
                    num6 = image.Height;
                    num5 = (image.Height * num) / num2;
                    y = 0;
                    x = (image.Width - num5) / 2;
                }
                else
                {
                    num5 = image.Width;
                    num6 = (image.Width * height) / num;
                    x = 0;
                    y = (image.Height - num6) / 2;
                }
            Label_00F1:
                image2 = new Bitmap(num, num2);
                Graphics graphics = Graphics.FromImage(image2);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
                try
                {
                    string path = thumbnailPath + @"thumbnail\";
                    if (!Directory.Exists(path))
                    {
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch
                        {
                        }
                    }
                    string filename = path + thumbnailName;
                    image2.Save(filename, ImageFormat.Jpeg);
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    image.Dispose();
                    image2.Dispose();
                    graphics.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool SavePhotoToDataBase(Guid annexCode, string thumbnailPath, string thumbnaName)
        {
            PrjInfoPhotoModel singlePhotoInfo = this.intendancePhotoListAction.GetSinglePhotoInfo(annexCode);
            Thumbnai thumbnai = new Thumbnai {
                ThumbnaiCode = new Guid(annexCode.ToString()),
                ShowContent = DateTime.Now.ToString("yyyyMMdd"),
                ThumbnaImgPath = thumbnailPath + "thumbnail/",
                ThumbnaName = thumbnaName,
                AddDate = DateTime.Now,
                Remark = singlePhotoInfo.Remark,
                ImgPath = singlePhotoInfo.PhotoPath + singlePhotoInfo.PhotoName
            };
            HttpContext.Current.Session["PrjInfo-thuPhoto-success"] = thumbnai;
            return this.AddThumbnai(thumbnai);
        }

        private bool SaveToDataBase(Guid annexCode, string thumbnailPath, string thumbnaName)
        {
            AnnexInfo singleAnnexInfo = this.annexAction.GetSingleAnnexInfo(annexCode);
            DataTable table = publicDbOpClass.DataTableQuary("select a.projectcode,a.Remark from EPM_Book_ConstructTask a,Xpm_basic_AnnexList b where cast(a.noteid as nvarchar)=b.recordcode and b.recordcode='" + singleAnnexInfo.RecordCode + "'");
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                str2 = (table.Rows[0]["Remark"] as string) ?? string.Empty;
                table = publicDbOpClass.DataTableQuary("select prjName from PT_PrjInfo where prjguid='" + table.Rows[0]["projectcode"] + "'");
            }
            string str3 = "";
            if (table.Rows.Count > 0)
            {
                str3 = table.Rows[0]["prjName"].ToString();
            }
            Thumbnai thumbnai = new Thumbnai {
                ThumbnaiCode = new Guid(annexCode.ToString()),
                ShowContent = str3 + " " + DateTime.Now.ToString("yyyyMMdd"),
                ThumbnaImgPath = thumbnailPath + "thumbnail/",
                ThumbnaName = thumbnaName,
                AddDate = DateTime.Now,
                Remark = str2,
                ImgPath = singleAnnexInfo.FilePath + singleAnnexInfo.AnnexName
            };
            return this.AddThumbnai(thumbnai);
        }
    }
}

