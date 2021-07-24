namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Web;

    public class FileUpload
    {
        private string _extName = "*";
        private int _fileSize;
        private string _messageString = "";
        private string _uploadPath = "";

        public byte[] ConvertStreamToByteBuffer(FileStream theStream)
        {
            int num;
            MemoryStream stream = new MemoryStream();
            while ((num = theStream.ReadByte()) != -1)
            {
                stream.WriteByte((byte) num);
            }
            return stream.ToArray();
        }

        public int CopyFile(string sourFile, string destFile)
        {
            int num = 0;
            if (!File.Exists(sourFile))
            {
                num = 1;
            }
            try
            {
                File.Copy(sourFile, destFile, false);
                num = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        public int CopyFile(string strSourFileName, string strSourDirectory, string strDestFileName, string strDestDirectory)
        {
            int num = 0;
            strSourDirectory = HttpContext.Current.Server.MapPath(strSourDirectory);
            strDestDirectory = HttpContext.Current.Server.MapPath(strDestDirectory);
            if (!Directory.Exists(strSourDirectory))
            {
                num = 1;
            }
            if (!Directory.Exists(strDestDirectory))
            {
                num = 2;
            }
            if (!File.Exists(strSourDirectory + strSourFileName))
            {
                num = 3;
            }
            try
            {
                File.Copy(strSourDirectory + strSourFileName, strDestDirectory + strDestFileName, false);
                return 0;
            }
            catch
            {
                return 4;
            }
        }

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

        public bool Delete(string strFileName)
        {
            strFileName = HttpContext.Current.Server.MapPath(strFileName);
            if (File.Exists(strFileName))
            {
                try
                {
                    File.Delete(strFileName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteFile(string strFileName)
        {
            if (File.Exists(strFileName))
            {
                try
                {
                    File.Delete(strFileName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public bool DirExists(string strDirectory)
        {
            strDirectory = HttpContext.Current.Server.MapPath(strDirectory);
            return Directory.Exists(strDirectory);
        }

        public string GetAnnexID()
        {
            return publicDbOpClass.QuaryMaxid("PT_Annex", "i_AnnexID");
        }

        public string getAnnexPath(int flag)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select ModuleCode from XPM_Basic_AnnexSettings where ModuleID = " + flag);
            if (table.Rows.Count == 0)
            {
                return "";
            }
            return table.Rows[0][0].ToString();
        }

        public byte[] GetBinaryFile(string strFileName)
        {
            strFileName = HttpContext.Current.Server.MapPath(strFileName);
            if (File.Exists(strFileName))
            {
                try
                {
                    FileStream theStream = File.OpenRead(strFileName);
                    return this.ConvertStreamToByteBuffer(theStream);
                }
                catch
                {
                    return new byte[0];
                }
            }
            return new byte[0];
        }

        public string GetExtName(string filename)
        {
            int num = filename.LastIndexOf(".");
            if (num > 0)
            {
                return filename.Substring(num + 1);
            }
            return "";
        }

        public int MoveFile(string strFileName, string strSourDirectory, string strDestDirectory)
        {
            strSourDirectory = HttpContext.Current.Server.MapPath(strSourDirectory);
            strDestDirectory = HttpContext.Current.Server.MapPath(strDestDirectory);
            if (!Directory.Exists(strSourDirectory))
            {
                return 1;
            }
            if (!Directory.Exists(strDestDirectory))
            {
                return 2;
            }
            if (!File.Exists(strSourDirectory + strFileName))
            {
                return 3;
            }
            try
            {
                File.Move(strSourDirectory + strFileName, strDestDirectory + strFileName);
                return 0;
            }
            catch
            {
                return 4;
            }
        }

        public DataTable SingleUploadFileQuery(string annexid)
        {
            return publicDbOpClass.DataTableQuary("select i_AnnexID,i_AnnexModelID,i_OperationID,nv_NewFileName,nv_OldFileName,nv_FilePath from PT_Annex where i_AnnexID=" + annexid);
        }

        public string[] Upload(HttpPostedFile postedFile, int flag)
        {
            string[] strArray = new string[] { "", "", "", "" };
            string str = postedFile.FileName.Trim().Substring(postedFile.FileName.LastIndexOf(@"\") + 1);
            string str2 = postedFile.FileName.Trim().Substring(postedFile.FileName.Trim().LastIndexOf("."));
            string fileName = DateTime.Now.Ticks.ToString() + str2;
            strArray[0] = str;
            strArray[1] = "";
            strArray[2] = "";
            strArray[3] = "";
            string strDirectory = this.getAnnexPath(flag);
            string strParentDirectory = ConfigurationSettings.AppSettings["FileUpLoad"];
            HttpContext.Current.Server.MapPath(strParentDirectory + strDirectory + "/");
            string str6 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/";
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
            flag2 = this.DirExists(strParentDirectory + strDirectory + "/" + str6);
            flag3 = false;
            if (!flag2)
            {
                flag3 = this.CreateDirectory(strParentDirectory + strDirectory + "/", str6);
            }
            else
            {
                flag3 = true;
            }
            if (flag3)
            {
                this.UploadPath = strParentDirectory + strDirectory + "/" + str6;
                if (this.Upload(postedFile, fileName, false))
                {
                    strArray[1] = strParentDirectory + strDirectory + "/" + str6 + fileName;
                    strArray[3] = postedFile.ContentLength.ToString();
                }
            }
            strArray[2] = this._messageString;
            return strArray;
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

        public bool UploadFileAdd(string annexid, string annexmodelid, string operationid, string newfilename, string oldfilename, string filepath)
        {
            return publicDbOpClass.NonQuerySqlString("insert into PT_Annex(i_AnnexID,i_AnnexModelID,i_OperationID,nv_NewFileName,nv_OldFileName,nv_FilePath) values(" + annexid + "," + annexmodelid + "," + operationid + ",'" + newfilename + "','" + oldfilename + "','" + filepath + "')");
        }

        public bool UploadFileDel(string annexid)
        {
            return publicDbOpClass.NonQuerySqlString("delete from PT_Annex where i_AnnexID='" + annexid + "'");
        }

        public DataTable UploadFileQuery(string operationid, string annexmodelid)
        {
            return publicDbOpClass.DataTableQuary("select i_AnnexID,i_AnnexModelID,i_OperationID,nv_NewFileName,nv_OldFileName,nv_FilePath from PT_Annex where i_AnnexModelID='" + annexmodelid + "' and i_OperationID='" + operationid + "'");
        }

        public bool UploadValidator(HttpPostedFile postedFile)
        {
            if (this._fileSize < 0)
            {
                this._messageString = "允许文件大小设置错误！";
                return false;
            }
            if (this._extName != "*")
            {
                bool flag = false;
                string[] strArray = this._extName.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString().ToUpper() == this.GetExtName(postedFile.FileName.ToString().ToUpper()))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
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
                this._messageString = "文件检查通过！";
                return true;
            }
            if (postedFile.ContentLength < this._fileSize)
            {
                this._messageString = "文件检查通过！";
                return true;
            }
            this._messageString = "该文件大小超出范围！";
            return false;
        }

        public bool UploadValidator(HttpPostedFile postedFile, string fileName, bool isCreateDir)
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

        public string ExtName
        {
            get
            {
                return this._extName;
            }
            set
            {
                this._extName = value;
            }
        }

        public int FileSize
        {
            get
            {
                return this._fileSize;
            }
            set
            {
                this._fileSize = value;
            }
        }

        public string MessageString
        {
            get
            {
                return this._messageString;
            }
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

