namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class OAFileDestroy
    {
        private int _destroyfileid;
        private Guid _destroyrecordid;
        private com.jwsoft.pm.entpm.model.OAFileCatalog _OAFileCatalog = new com.jwsoft.pm.entpm.model.OAFileCatalog();

        public int DestroyFileID
        {
            get
            {
                return this._destroyfileid;
            }
            set
            {
                this._destroyfileid = value;
            }
        }

        public Guid DestroyRecordID
        {
            get
            {
                return this._destroyrecordid;
            }
            set
            {
                this._destroyrecordid = value;
            }
        }

        public com.jwsoft.pm.entpm.model.OAFileCatalog OAFileCatalog
        {
            get
            {
                return this._OAFileCatalog;
            }
            set
            {
                this._OAFileCatalog = value;
            }
        }
    }
}

