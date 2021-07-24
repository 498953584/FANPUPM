namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Web;

    public class KnowledgeFileUp
    {
        private string _extName = "*";
        private int _fileSize;
        private string _messageString = "";
        private string _uploadPath = "";

        public bool CreateDirectory(string strParentDirectory, string strDirectory)
        {
            strDirectory = strParentDirectory + strDirectory;
            strParentDirectory = HttpContext.Current.Server.MapPath(strParentDirectory);
            strDirectory = HttpContext.Current.Server.MapPath(strDirectory);
            if (!Directory.Exists(strParentDirectory))
            {
                return false;
            }
            if (Directory.Exists(strDirectory))
            {
                return true;
            }
            try
            {
                Directory.CreateDirectory(strDirectory);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string DelFile(string strFilePath)
        {
            string str = "1";
            if (!File.Exists(HttpContext.Current.Server.MapPath(strFilePath)))
            {
                return str;
            }
            File.Delete(HttpContext.Current.Server.MapPath(strFilePath));
            if (true)
            {
                return "2";
            }
            return "3";
        }

        public bool DirExists(string strDirectory)
        {
            strDirectory = HttpContext.Current.Server.MapPath(strDirectory);
            return Directory.Exists(strDirectory);
        }

        public string getAnnexPath(int flag)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select ModuleCode from XPM_Basic_AnnexSettings where ModuleId = " + flag);
            if (table.Rows.Count == 0)
            {
                return "";
            }
            return table.Rows[0][0].ToString();
        }

        private string GetExtName(string filename)
        {
            int num = filename.LastIndexOf(".");
            if (num > 0)
            {
                return filename.Substring(num + 1);
            }
            return "";
        }

        public bool Upload(HttpPostedFile postedFile, string fileName, bool isCreateDir)
        {
            bool flag = false;
            if (this._fileSize < 0)
            {
                this._messageString = "允许文件大小没有设置！";
                return flag;
            }
            if (this._extName != "*")
            {
                bool flag2 = false;
                string[] strArray = this._extName.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString().ToUpper() == this.GetExtName(postedFile.FileName.ToString().ToUpper()))
                    {
                        flag2 = true;
                        break;
                    }
                }
                if (!flag2)
                {
                    this._messageString = "文件类型不在许可范围！";
                    return false;
                }
            }
            if (postedFile.ContentLength == 0)
            {
                this._messageString = "你可能没有选择文件，请选择文件！";
                return false;
            }
            if (this._fileSize == 0)
            {
                try
                {
                    postedFile.SaveAs(this._uploadPath + fileName);
                    return true;
                }
                catch
                {
                    this._messageString = "上传过程中错误!";
                    return false;
                }
            }
            if (postedFile.ContentLength < this._fileSize)
            {
                if (isCreateDir)
                {
                    if (!Directory.Exists(this._uploadPath + fileName.Substring(0, 6)))
                    {
                        try
                        {
                            Directory.CreateDirectory(this._uploadPath + fileName.Substring(0, 6));
                        }
                        catch
                        {
                            this._messageString = "创建目录失败！";
                            return false;
                        }
                    }
                    try
                    {
                        postedFile.SaveAs(this._uploadPath + fileName.Substring(0, 6) + @"\" + fileName);
                        return true;
                    }
                    catch
                    {
                        this._messageString = "上传过程中错误!";
                        return false;
                    }
                }
                try
                {
                    postedFile.SaveAs(this._uploadPath + fileName);
                    return true;
                }
                catch
                {
                    this._messageString = "上传过程中错误!";
                    return false;
                }
            }
            this._messageString = "该文件大小超出范围！";
            return false;
        }

        public string[] UploadSingleFile(HttpPostedFile postedFile, int flag)
        {
            string[] strArray = new string[] { "", "", "", "", "" };
            string str = postedFile.FileName.Trim().Substring(postedFile.FileName.LastIndexOf(@"\") + 1);
            string fileName = DateTime.Now.Ticks.ToString() + str;
            strArray[0] = str;
            strArray[1] = "";
            strArray[2] = "";
            strArray[3] = "";
            strArray[4] = fileName;
            string strDirectory = this.getAnnexPath(flag);
            string strParentDirectory = ConfigurationSettings.AppSettings["FileUpLoad"];
            HttpContext.Current.Server.MapPath(strParentDirectory + strDirectory + "/");
            string str5 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/";
            bool flag2 = this.DirExists(strParentDirectory + strDirectory);
            bool flag3 = false;
            if (!flag2)
            {
                flag3 = this.CreateDirectory(strParentDirectory, strDirectory);
            }
            else
            {
                flag3 = true;
            }
            flag2 = this.DirExists(strParentDirectory + strDirectory + "/" + str5);
            flag3 = false;
            if (!flag2)
            {
                flag3 = this.CreateDirectory(strParentDirectory + strDirectory + "/", str5);
            }
            else
            {
                flag3 = true;
            }
            if (flag3)
            {
                this.UploadPath = strParentDirectory + strDirectory + "/" + str5;
                if (this.Upload(postedFile, fileName, false))
                {
                    strArray[1] = strParentDirectory + strDirectory + "/" + str5 + fileName;
                    strArray[3] = postedFile.ContentLength.ToString();
                }
            }
            strArray[2] = this._messageString;
            return strArray;
        }

        public string UploadPath
        {
            get
            {
                return this._uploadPath;
            }
            set
            {
                this._uploadPath = HttpContext.Current.Server.MapPath(value);
            }
        }
    }
}

