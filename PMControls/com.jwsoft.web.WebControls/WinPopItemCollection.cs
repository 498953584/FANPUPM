namespace com.jwsoft.web.WebControls
{
    using System;
    using System.Reflection;
    using System.Web.UI;

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public class WinPopItemCollection : ViewStatePartCollectionBase
    {
        public int Add(WinPopItem item)
        {
            return base.AddItem(item);
        }

        protected override ViewStatePartBase NewItem()
        {
            return new WinPopItem();
        }

        public WinPopItem this[int index]
        {
            get
            {
                if ((index >= base.Count) || (index < 0))
                {
                    return null;
                }
                return (WinPopItem) base.InnerList[index];
            }
        }
    }
}

