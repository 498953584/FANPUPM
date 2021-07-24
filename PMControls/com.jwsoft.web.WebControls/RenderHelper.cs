namespace com.jwsoft.web.WebControls
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Web.UI.WebControls;

    public class RenderHelper
    {
        private RenderHelper()
        {
        }

        public static string BoolToJavaPara(bool fromBool)
        {
            return fromBool.ToString().ToLower();
        }

        public static string ColorToJavaPara(Color fromColor)
        {
            return ((fromColor == Color.Empty) ? "null" : string.Format("'{0}'", ColorTranslator.ToHtml(fromColor)));
        }

        public static string GetDarkColor(Color color)
        {
            Color black;
            if (color != Color.Empty)
            {
                int red = (color.R * 2) / 3;
                int green = (color.G * 2) / 3;
                int blue = (color.B * 2) / 3;
                black = Color.FromArgb(red, green, blue);
            }
            else
            {
                black = Color.Black;
            }
            return ColorTranslator.ToHtml(black);
        }

        public static string GetLightColor(Color color)
        {
            Color white;
            if (color != Color.Empty)
            {
                int red = (color.R * 4) / 3;
                if (red > 0xff)
                {
                    red = 0xff;
                }
                int green = (color.G * 4) / 3;
                if (green > 0xff)
                {
                    green = 0xff;
                }
                int blue = (color.B * 4) / 3;
                if (blue > 0xff)
                {
                    blue = 0xff;
                }
                white = Color.FromArgb(red, green, blue);
            }
            else
            {
                white = Color.White;
            }
            return ColorTranslator.ToHtml(white);
        }

        public static string GetShareJScript()
        {
            string str = null;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            if (executingAssembly != null)
            {
                StreamReader reader = new StreamReader(executingAssembly.GetManifestResourceStream("Keyss.WebControls.share.js"));
                str = reader.ReadToEnd();
                reader.Close();
            }
            return str;
        }

        public static string StrToJavaPara(string fromStr)
        {
            return (((fromStr == null) || (fromStr == string.Empty)) ? "null" : string.Format("'{0}'", fromStr));
        }

        public static string UnitToJavaPara(Unit fromUnit)
        {
            return ((fromUnit == Unit.Empty) ? "null" : string.Format("'{0}'", fromUnit.ToString()));
        }
    }
}

