namespace cn.justwin.stockBLL
{
    using System;

    public class SmEnum
    {
        public enum AuditSate
        {
        }

        public enum FlowState
        {
        }

        public enum PermitType
        {
            Person,
            Department,
            Post,
            Groups
        }

        public enum SmSetName
        {
            DepotType,
            DepotBindType,
            ProjectAlarm,
            ProjectTransparentSet
        }

        public enum SmSetValue
        {
            ParallelMode,
            TotalMode,
            UnBind,
            Bind,
            UnAlarm,
            Alarm,
            UnTransparent,
            Transparent
        }
    }
}

