namespace Commons.Collections
{
    using System;
    using System.IO;
    using System.Text;

    internal class PropertiesReader : StreamReader
    {
        public PropertiesReader(StreamReader reader) : base(reader.BaseStream)
        {
        }

        public virtual string ReadProperty()
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                string str;
            Label_0008:
                str = this.ReadLine().Trim();
                if ((str.Length == 0) || (str[0] == '#'))
                {
                    goto Label_0008;
                }
                if (str.EndsWith(@"\"))
                {
                    str = str.Substring(0, str.Length - 1);
                    builder.Append(str);
                    goto Label_0008;
                }
                builder.Append(str);
            }
            catch (NullReferenceException)
            {
                return null;
            }
            return builder.ToString();
        }
    }
}

