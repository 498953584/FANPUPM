namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class PopupSettingService : Repository<PopupSetting>
    {
        public PopupSetting GetById(string id)
        {
            return (from s in this
                where s.Id == id
                select s).FirstOrDefault<PopupSetting>();
        }
    }
}

