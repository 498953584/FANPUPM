namespace Commons.Collections
{
    using System;
    using System.Text;

    internal class PropertiesTokenizer : StringTokenizer
    {
        internal const string DELIMITER = ",";

        public PropertiesTokenizer(string string_Renamed) : base(string_Renamed, ",")
        {
        }

        public bool HasMoreTokens()
        {
            return base.HasMoreTokens();
        }

        public string NextToken()
        {
            StringBuilder builder = new StringBuilder();
            while (this.HasMoreTokens())
            {
                string str = base.NextToken();
                if (str.EndsWith(@"\"))
                {
                    builder.Append(str.Substring(0, str.Length - 1));
                    builder.Append(",");
                }
                else
                {
                    builder.Append(str);
                    break;
                }
            }
            return builder.ToString().Trim();
        }
    }
}

