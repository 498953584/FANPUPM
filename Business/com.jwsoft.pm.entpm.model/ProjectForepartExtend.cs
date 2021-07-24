namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProjectForepartExtend : ProjectForepart
    {
        private Guid _InviteBidCode = Guid.Empty;

        public Guid InviteBidCode
        {
            get
            {
                return this._InviteBidCode;
            }
            set
            {
                this._InviteBidCode = value;
            }
        }
    }
}

