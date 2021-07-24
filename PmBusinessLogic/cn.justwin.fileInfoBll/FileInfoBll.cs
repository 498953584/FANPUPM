namespace cn.justwin.fileInfoBll
{
    using cn.justwin.BLL;
    using cn.justwin.fileInfoDal;
    using cn.justwin.fileInfoModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class FileInfoBll
    {
        private FileInfo fileInfo = new FileInfo();

        public int Add(FileInfoModel model)
        {
            return this.fileInfo.Add(model);
        }

        public int Delete(string Id)
        {
            this.SetDelParentIdNull(Id);
            this.fileInfo.DelProjectAnnex(Id);
            return this.fileInfo.Delete(Id);
        }

        public static DataTable GetAllFiles(string id, string userCode, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string fileOwner, bool isGetAll, int pageSize, int pageIndex)
        {
            return FileInfo.GetAllFiles(id, userCode, startTime, endTime, fileName, fileLowerSize, fileHighSize, fileOwner, isGetAll, pageSize, pageIndex);
        }

        public static int GetAllFilesCount(string id, string userCode, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string fileOwner, bool isGetAll)
        {
            return FileInfo.GetAllFilesCount(id, userCode, startTime, endTime, fileName, fileLowerSize, fileHighSize, fileOwner, isGetAll);
        }

        public static DataTable GetChildren(string directoryId)
        {
            return FileInfo.GetChildren(directoryId);
        }

        public DataTable GetFillTable(string parentId, string userCodes, string startTime, string endTime, string fileName, double fileStartSize, double fileEndSize, string fileOwner, string isLoadFile, string dparentId, string strWhere)
        {
            return this.fileInfo.GetFillTable(parentId, userCodes, startTime, endTime, fileName, fileStartSize, fileEndSize, fileOwner, isLoadFile, dparentId, strWhere);
        }

        public static int GetFlodersCount(string parentId)
        {
            return FileInfo.GetFlodersCount(parentId);
        }

        public static DataTable GetFolders(string parentId, int pageSize, int pageIndex)
        {
            return FileInfo.GetFolders(parentId, pageSize, pageIndex);
        }

        public List<FileInfoModel> GetListArray(string strWhere)
        {
            return this.fileInfo.GetListArray(strWhere);
        }

        public FileInfoModel GetModel(string Id)
        {
            return this.fileInfo.GetModel(Id);
        }

        public static DataTable GetRecycleInfo(string userCode, int pageSize, int pageIndex)
        {
            return FileInfo.GetRecycleInfo(userCode, pageSize, pageIndex);
        }

        public static int GetRecycleRecord(string userCode)
        {
            return FileInfo.GetRecycleRecord(userCode);
        }

        public static int GetRepeatDirectoryCount(string inStr)
        {
            return FileInfo.GetRepeatDirectoryCount(inStr);
        }

        public static DataTable GetRepeatDirectoryId(string inStr)
        {
            return FileInfo.GetRepeatDirectoryId(inStr);
        }

        public static void MoveRecycle(string id)
        {
            DataTable ancestorInfo = new DataTable();
            ancestorInfo = FileInfo.GetAncestorInfo(id);
            string jsonAncestorInfo = string.Empty;
            jsonAncestorInfo = JsonConvert.SerializeObject(ancestorInfo);
            FileInfo.UpdateIsValidAndAncestorInfo(true, jsonAncestorInfo, id);
        }

        public static void Recover(string recoverId, string userCode)
        {
            string ancestor = FileInfo.GetAncestor(recoverId);
            try
            {
                JArray array = (JArray) JsonConvert.DeserializeObject(ancestor);
                string str2 = "";
                if (userCode != "00000000")
                {
                    str2 = JsonHelper.Serialize(new string[] { userCode, "00000000" });
                }
                else
                {
                    str2 = JsonHelper.Serialize(new string[] { "00000000" });
                }
                bool flag = true;
                string parentId = array[array.Count - 1]["Id"].ToString();
                for (int i = array.Count - 2; i >= 0; i--)
                {
                    string str4 = array[i + 1]["Id"].ToString();
                    array[i + 1]["FileName"].ToString();
                    string id = array[i]["Id"].ToString();
                    string fileName = array[i]["FileName"].ToString();
                    if (flag)
                    {
                        flag = FileInfo.isExist(id, fileName);
                    }
                    if (!flag)
                    {
                        if (i != (array.Count - 2))
                        {
                            str4 = parentId;
                        }
                        string str7 = FileInfo.GetId(str4, fileName, recoverId);
                        if (string.IsNullOrEmpty(str7))
                        {
                            FileInfoModel model = new FileInfoModel {
                                CreateTime = new DateTime?(DateTime.Now),
                                FileName = fileName,
                                FileOwner = userCode,
                                FileSize = "",
                                FileNewName = "",
                                FileType = "2",
                                Id = Guid.NewGuid().ToString(),
                                UserCodes = str2,
                                ParentId = str4
                            };
                            new FileInfoBll().Add(model);
                            parentId = model.Id;
                        }
                        else
                        {
                            parentId = str7;
                        }
                    }
                    else
                    {
                        parentId = id;
                    }
                }
                new FileInfoBll().RecoverFoler(recoverId, parentId);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private void RecoverFoler(string recoverId, string parentId)
        {
            FileInfo info = new FileInfo();
            FileInfoModel model = info.GetModel(recoverId);
            bool flag = false;
            if (model != null)
            {
                string str = FileInfo.GetId(parentId, model.FileName, recoverId);
                if (!string.IsNullOrEmpty(str))
                {
                    if (model.FileType == "2")
                    {
                        DataTable directChildren = FileInfo.GetDirectChildren(recoverId);
                        FileInfo.UpdateParentId(str, recoverId);
                        info.Delete(recoverId);
                        flag = true;
                        if (directChildren != null)
                        {
                            foreach (DataRow row in directChildren.Rows)
                            {
                                string str2 = row["Id"].ToString();
                                this.RecoverFoler(str2, str);
                            }
                        }
                    }
                    else if ((str != "") && (str != recoverId))
                    {
                        info.Delete(str);
                    }
                }
                if (!flag && (parentId != ""))
                {
                    FileInfo.UpdateIsValidAndAncestorInfoAndParnentId(false, string.Empty, recoverId, parentId);
                }
            }
        }

        private void SetDelParentIdNull(string parentId)
        {
            FileInfo.SetDelParentId(parentId);
        }

        public int Update(FileInfoModel model)
        {
            return this.fileInfo.Update(model);
        }
    }
}

