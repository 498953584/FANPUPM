namespace cn.justwin.fileInfoBll
{
    using cn.justwin.fileInfoDal;
    using cn.justwin.fileInfoModel;
    using System;
    using System.Collections.Generic;

    public class FileTypeBll
    {
        private FileType fileType = new FileType();

        public int Add(FileTypeModel model)
        {
            return this.fileType.Add(model);
        }

        public int Delete(string TypeId)
        {
            return this.fileType.Delete(TypeId);
        }

        public List<FileTypeModel> GetListArray(string strWhere)
        {
            return this.fileType.GetListArray(strWhere);
        }

        public FileTypeModel GetModel(string TypeId)
        {
            return this.fileType.GetModel(TypeId);
        }

        public int Update(FileTypeModel model)
        {
            return this.fileType.Update(model);
        }
    }
}

