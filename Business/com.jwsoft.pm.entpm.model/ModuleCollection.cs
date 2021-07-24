namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ModuleCollection : CollectionBase
    {
        public void Add(com.jwsoft.pm.entpm.model.Module value)
        {
            base.List.Add(value);
        }

        public com.jwsoft.pm.entpm.model.Module IndexOf(string moduleCode)
        {
            ModuleCollection list = (ModuleCollection) base.List;
            foreach (com.jwsoft.pm.entpm.model.Module module in list)
            {
                if (module.ModuleCode == moduleCode)
                {
                    return module;
                }
            }
            return null;
        }

        public com.jwsoft.pm.entpm.model.Module this[int index]
        {
            get
            {
                return (com.jwsoft.pm.entpm.model.Module) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

