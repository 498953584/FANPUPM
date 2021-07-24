namespace com.jwsoft.web.WebControls
{
    using System;
    using System.Reflection;
    using System.Web.UI;

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public class DropDownTreeItemStyleCollection : ViewStatePartCollectionBase
    {
        public int Add(DropDownTreeItemStyle item)
        {
            return base.AddItem(item);
        }

        protected override ViewStatePartBase NewItem()
        {
            return new DropDownTreeItemStyle();
        }

        public DropDownTreeItemStyle this[int index]
        {
            get
            {
                if ((index >= base.Count) || (index < 0))
                {
                    return null;
                }
                return (DropDownTreeItemStyle) base.InnerList[index];
            }
        }
    }
}

