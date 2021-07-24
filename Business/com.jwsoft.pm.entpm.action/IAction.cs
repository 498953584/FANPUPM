namespace com.jwsoft.pm.entpm.action
{
    using System;

    public interface IAction
    {
        int Add(object value);
        int Delete(object value);
        int Update(object value);
    }
}

