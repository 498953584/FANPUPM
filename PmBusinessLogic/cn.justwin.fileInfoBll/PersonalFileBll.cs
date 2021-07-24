namespace cn.justwin.fileInfoBll
{
    using cn.justwin.fileInfoDal;
    using cn.justwin.fileInfoModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PersonalFileBll
    {
        private PersonalFile personalFile = new PersonalFile();

        public int Add(PersonalFileModel model)
        {
            return this.personalFile.Add(model);
        }

        public int Delete(string Id)
        {
            return this.personalFile.Delete(Id);
        }

        public static DataTable GetAllFiles(string id, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string userCode, bool isGetAll, bool isShare, int pageSize, int pageIndex)
        {
            return PersonalFile.GetAllFiles(id, startTime, endTime, fileName, fileLowerSize, fileHighSize, userCode, isGetAll, isShare, pageSize, pageIndex);
        }

        public static int GetAllFilesCount(string id, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string userCode, bool isGetAll, bool isShare)
        {
            return PersonalFile.GetAllFilesCount(id, startTime, endTime, fileName, fileLowerSize, fileHighSize, userCode, isGetAll, isShare);
        }

        public List<PersonalFileModel> GetListArray(string strWhere)
        {
            return this.personalFile.GetListArray(strWhere);
        }

        public PersonalFileModel GetModel(string Id)
        {
            return this.personalFile.GetModel(Id);
        }

        public DataTable GetPersonalFile(string parentId, string startTime, string endTime, string fileName, double startSize, double endSize, string fileOwner, string isLoadFile, string sparentId, string strWhere)
        {
            return this.personalFile.GetPersonalFile(parentId, startTime, endTime, fileName, startSize, endSize, fileOwner, isLoadFile, sparentId, strWhere);
        }

        public static DataTable GetShareFolders(string ShareFolderIds, int pageSize, int pageIndex)
        {
            return PersonalFile.GetShareFolders(ShareFolderIds, pageSize, pageIndex);
        }

        public static int GetShareFolersCount(string ShareFolderIds)
        {
            return PersonalFile.GetShareFolersCount(ShareFolderIds);
        }

        public int Update(PersonalFileModel model)
        {
            return this.personalFile.Update(model);
        }

        public static void UpdateFolderShareUser(string id, string shareUser)
        {
            PersonalFile.UpdateFolderShareUser(id, shareUser);
        }
    }
}

