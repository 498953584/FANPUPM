namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Data;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WinPopItem : ViewStatePartBase
    {
        public const string ReplaceKey = "E";
        public const string TargetFeaturesKey = "D";
        public const string TargetFrameKey = "C";
        public const string TargetUrlKey = "B";

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void CopyFrom(WinPopItem s)
        {
            base.CopyFrom(s);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual string GetPreRenderJScript()
        {
            return (string.Format("window.open({0},{1},{2},{3})", new object[] { RenderHelper.StrToJavaPara(this.TargetUrl), RenderHelper.StrToJavaPara(this.TargetFrame), RenderHelper.StrToJavaPara(this.TargetFeatures), RenderHelper.BoolToJavaPara(this.Replace) }) + ";\n\r");
        }

        public virtual void LoadFromDataRow(DataRow row)
        {
            DataTable table = row.Table;
            if (table.Columns.Contains("TargetURL"))
            {
                this.TargetUrl = (string) row["TargetURL"];
            }
            if (table.Columns.Contains("TargetFrame"))
            {
                this.TargetFrame = (string) row["TargetFrame"];
            }
            if (table.Columns.Contains("TargetFeatures"))
            {
                this.TargetFeatures = (string) row["TargetFeatures"];
            }
            if (table.Columns.Contains("Replace"))
            {
                this.Replace = (bool) row["Replace"];
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void MergeWith(WinPopItem s)
        {
            base.MergeWith(s);
        }

        [NotifyParentProperty(true), DefaultValue(true)]
        public bool Replace
        {
            get
            {
                object obj2 = this.ViewState["E"];
                return ((obj2 == null) || ((bool) obj2));
            }
            set
            {
                this.ViewState["E"] = value;
            }
        }

        [Category("Appearance"), Bindable(true), DefaultValue(""), NotifyParentProperty(true)]
        public string TargetFeatures
        {
            get
            {
                object obj2 = this.ViewState["D"];
                return ((obj2 == null) ? string.Empty : ((string) obj2));
            }
            set
            {
                this.ViewState["D"] = value;
            }
        }

        [Category("Appearance"), DefaultValue(""), NotifyParentProperty(true), Bindable(true)]
        public string TargetFrame
        {
            get
            {
                object obj2 = this.ViewState["C"];
                return ((obj2 == null) ? string.Empty : ((string) obj2));
            }
            set
            {
                this.ViewState["C"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), NotifyParentProperty(true)]
        public string TargetUrl
        {
            get
            {
                object obj2 = this.ViewState["B"];
                return ((obj2 == null) ? string.Empty : ((string) obj2));
            }
            set
            {
                this.ViewState["B"] = value;
            }
        }
    }
}

