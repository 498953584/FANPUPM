namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    public abstract class ViewStatePartBase : IStateManager
    {
        protected bool _isTrackingViewState;
        protected StateBag _viewState;

        protected ViewStatePartBase()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void CopyFrom(ViewStatePartBase s)
        {
            if ((s != null) && !s.IsEmpty)
            {
                foreach (string str in s.ViewState.Keys)
                {
                    this.ViewState.Add(str, s.ViewState[str]);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool IsSet(string key)
        {
            return (!this.IsEmpty && (this._viewState[key] != null));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                ((IStateManager) this.ViewState).LoadViewState(savedState);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void MergeWith(ViewStatePartBase s)
        {
            if ((s != null) && !s.IsEmpty)
            {
                foreach (string str in s.ViewState.Keys)
                {
                    if (!this.IsSet(str))
                    {
                        this.ViewState.Add(str, s.ViewState[str]);
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void RemoveItem(string key)
        {
            if (this._viewState[key] != null)
            {
                this._viewState.Remove(key);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void Reset()
        {
            this._viewState = null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual object SaveViewState()
        {
            object obj2 = null;
            if (!this.IsEmpty)
            {
                obj2 = ((IStateManager) this._viewState).SaveViewState();
            }
            return obj2;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void SetDirty()
        {
            if (!this.IsEmpty)
            {
                foreach (string str in this._viewState.Keys)
                {
                    this._viewState.SetItemDirty(str, true);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void TrackViewState()
        {
            this._isTrackingViewState = true;
            if (this._viewState != null)
            {
                ((IStateManager) this._viewState).TrackViewState();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsEmpty
        {
            get
            {
                return ((this._viewState == null) || (this.ViewState.Count == 0));
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual bool IsTrackingViewState
        {
            get
            {
                return this._isTrackingViewState;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected virtual StateBag ViewState
        {
            get
            {
                if (this._viewState == null)
                {
                    this._viewState = new StateBag(false);
                    if (this._isTrackingViewState)
                    {
                        ((IStateManager) this._viewState).TrackViewState();
                    }
                }
                return this._viewState;
            }
        }
    }
}

