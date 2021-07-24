namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DropDownTreeItemStyle : ViewStatePartBase
    {
        public const string BackColorKey = "B";
        public const string ForeColorKey = "A";

        public virtual void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (this.ForeColor != Color.Empty)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(this.ForeColor));
            }
            if (this.BackColor != Color.Empty)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(this.BackColor));
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void CopyFrom(DropDownTreeItemStyle s)
        {
            base.CopyFrom(s);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void MergeWith(DropDownTreeItemStyle s)
        {
            base.MergeWith(s);
        }

        [TypeConverter(typeof(WebColorConverter)), DefaultValue(typeof(Color), ""), NotifyParentProperty(true), Category("Appearance"), Bindable(true)]
        public Color BackColor
        {
            get
            {
                object obj2 = this.ViewState["B"];
                return ((obj2 == null) ? Color.Empty : ((Color) obj2));
            }
            set
            {
                this.ViewState["B"] = value;
            }
        }

        [NotifyParentProperty(true), Category("Appearance"), Bindable(true), DefaultValue(typeof(Color), ""), TypeConverter(typeof(WebColorConverter))]
        public Color ForeColor
        {
            get
            {
                object obj2 = this.ViewState["A"];
                return ((obj2 == null) ? Color.Empty : ((Color) obj2));
            }
            set
            {
                this.ViewState["A"] = value;
            }
        }
    }
}

