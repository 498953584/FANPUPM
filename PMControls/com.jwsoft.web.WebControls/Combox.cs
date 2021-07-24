namespace com.jwsoft.web.WebControls
{
    using System;
    using System.Collections;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:Combox runat=\"server\" />")] 
    public class Combox : TextBox
    {
        public DropDownList _DropDownList = new DropDownList();
        private Hashtable _values = new Hashtable();

        protected override void Render(HtmlTextWriter Output)
        {
            int num = Convert.ToInt32(base.Width.Value);
            if (num == 0)
            {
                num = 0x66;
            }
            int num2 = num + 0x10;
            int num3 = num2 - 0x12;
            Output.Write("<div style=\"POSITION:relative\">");
            Output.Write("<span style=\"MARGIN-LEFT:" + num3 + "px;OVERFLOW:hidden;WIDTH:18px\">");
            this._DropDownList.Width = Unit.Parse(num2 + "px");
            this._DropDownList.Style.Add("MARGIN-LEFT", "-" + num3 + "px");
            this._DropDownList.Attributes.Add("onchange", "this.parentNode.nextSibling.value=this.value");
            if (this._values.Count > 0)
            {
                foreach (string str in this._values.Keys)
                {
                    ListItem item = new ListItem {
                        Value = str,
                        Text = this._values[str].ToString()
                    };
                    this._DropDownList.Items.Add(item);
                }
            }
            if (this._DropDownList.Items.Count == 1)
            {
                ListItem item2 = new ListItem {
                    Value = "",
                    Text = " "
                };
                this._DropDownList.Items.Add(item2);
                this._DropDownList.SelectedIndex = 1;
            }
            this._DropDownList.RenderControl(Output);
            Output.Write("</span>");
            base.Style.Clear();
            base.Width = Unit.Parse(num + "px");
            base.Style.Add("left", "0px");
            base.Style.Add("POSITION", "absolute");
            base.Render(Output);
            Output.Write("</div>");
        }

        public Hashtable Values
        {
            get
            {
                return this._values;
            }
            set
            {
                this._values = value;
            }
        }
    }
}

