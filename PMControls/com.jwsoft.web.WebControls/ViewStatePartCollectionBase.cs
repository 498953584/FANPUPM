namespace com.jwsoft.web.WebControls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Web.UI;

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public abstract class ViewStatePartCollectionBase : CollectionBase, IStateManager
    {
        protected bool _isTrackingViewState;

        protected ViewStatePartCollectionBase()
        {
        }

        protected virtual int AddItem(ViewStatePartBase item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "item can't be null!");
            }
            if (this._isTrackingViewState)
            {
                item.TrackViewState();
                item.SetDirty();
            }
            base.InnerList.Add(item);
            return (base.InnerList.Count - 1);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                Pair pair = (Pair) savedState;
                ArrayList first = (ArrayList) pair.First;
                ArrayList second = (ArrayList) pair.Second;
                for (int i = 0; i < first.Count; i++)
                {
                    int num2 = (int) second[i];
                    if (num2 < base.Count)
                    {
                        ((ViewStatePartBase) base.InnerList[num2]).LoadViewState(first[i]);
                    }
                    else
                    {
                        ViewStatePartBase item = this.NewItem();
                        item.LoadViewState(first[i]);
                        this.AddItem(item);
                    }
                }
            }
        }

        protected abstract ViewStatePartBase NewItem();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual object SaveViewState()
        {
            if (base.Count > 0)
            {
                ArrayList x = new ArrayList();
                ArrayList y = new ArrayList();
                for (int i = 0; i < base.Count; i++)
                {
                    object obj2 = ((ViewStatePartBase) base.InnerList[i]).SaveViewState();
                    if (obj2 != null)
                    {
                        x.Add(obj2);
                        y.Add(i);
                    }
                }
                if (x.Count > 0)
                {
                    return new Pair(x, y);
                }
            }
            return null;
        }

        public virtual void SetDirty()
        {
            for (int i = 0; i < base.Count; i++)
            {
                ((ViewStatePartBase) base.InnerList[i]).SetDirty();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void TrackViewState()
        {
            this._isTrackingViewState = true;
            for (int i = 0; i < base.Count; i++)
            {
                ((ViewStatePartBase) base.InnerList[i]).TrackViewState();
            }
        }

        public virtual bool IsEmpty
        {
            get
            {
                return (base.Count == 0);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public virtual bool IsTrackingViewState
        {
            get
            {
                return this._isTrackingViewState;
            }
        }
    }
}

