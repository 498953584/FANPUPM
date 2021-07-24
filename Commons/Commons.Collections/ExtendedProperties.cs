namespace Commons.Collections
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    public class ExtendedProperties : Hashtable
    {
        protected internal string basePath;
        private static readonly bool DEFAULT_BOOLEAN = false;
        private static readonly byte DEFAULT_BYTE = 0;
        private static readonly double DEFAULT_DOUBLE = 0.0;
        private static readonly short DEFAULT_INT16 = 0;
        private static readonly int DEFAULT_INT32 = 0;
        private static readonly long DEFAULT_INT64 = 0L;
        private static readonly float DEFAULT_SINGLE = 0f;
        private ExtendedProperties defaults;
        protected internal string file;
        protected internal string fileSeparator;
        protected internal static string include = "include";
        protected internal bool isInitialized;
        protected internal ArrayList keysAsListed;

        public ExtendedProperties()
        {
            this.fileSeparator = @"\";
            this.isInitialized = false;
            this.keysAsListed = new ArrayList();
        }

        public ExtendedProperties(string file) : this(file, null)
        {
        }

        public ExtendedProperties(string file, string defaultFile)
        {
            this.fileSeparator = @"\";
            this.isInitialized = false;
            this.keysAsListed = new ArrayList();
            this.file = file;
            this.basePath = new FileInfo(file).FullName;
            this.basePath = this.basePath.Substring(0, this.basePath.LastIndexOf(this.fileSeparator) + 1);
            this.Load(new FileStream(file, FileMode.Open, FileAccess.Read));
            if (defaultFile != null)
            {
                this.defaults = new ExtendedProperties(defaultFile);
            }
        }

        public virtual void AddProperty(string key, object token)
        {
            object obj2 = this[key];
            if (obj2 is string)
            {
                ArrayList newValue = new ArrayList(2);
                newValue.Add(obj2);
                newValue.Add(token);
                CollectionsUtil.PutElement(this, key, newValue);
            }
            else if (obj2 is ArrayList)
            {
                ((ArrayList) obj2).Add(token);
            }
            else if ((token is string) && (((string) token).IndexOf(",") > 0))
            {
                PropertiesTokenizer tokenizer = new PropertiesTokenizer((string) token);
                while (tokenizer.HasMoreTokens())
                {
                    string str = tokenizer.NextToken();
                    this.AddStringProperty(key, str);
                }
            }
            else
            {
                this.AddPropertyDirect(key, token);
            }
        }

        private void AddPropertyDirect(string key, object obj)
        {
            if (!this.ContainsKey(key))
            {
                this.keysAsListed.Add(key);
            }
            CollectionsUtil.PutElement(this, key, obj);
        }

        private void AddStringProperty(string key, string token)
        {
            object obj2 = this[key];
            if (obj2 is string)
            {
                ArrayList newValue = new ArrayList(2);
                newValue.Add(obj2);
                newValue.Add(token);
                CollectionsUtil.PutElement(this, key, newValue);
            }
            else if (obj2 is ArrayList)
            {
                ((ArrayList) obj2).Add(token);
            }
            else
            {
                this.AddPropertyDirect(key, token);
            }
        }

        public virtual void ClearProperty(string key)
        {
            if (this.ContainsKey(key))
            {
                for (int i = 0; i < this.keysAsListed.Count; i++)
                {
                    if (((string) this.keysAsListed[i]).Equals(key))
                    {
                        this.keysAsListed.RemoveAt(i);
                        break;
                    }
                }
                this.Remove(key);
            }
        }

        public virtual void Combine(ExtendedProperties c)
        {
            IEnumerator keys = c.Keys;
            while (keys.MoveNext())
            {
                string key = keys.Current.ToString();
                this.SetProperty(key, c[key]);
            }
        }

        public static ExtendedProperties ConvertProperties(ExtendedProperties p)
        {
            ExtendedProperties properties = new ExtendedProperties();
            IEnumerator keys = p.Keys;
            while (keys.MoveNext())
            {
                string current = (string) keys.Current;
                string str2 = p.GetProperty(current).ToString();
                properties.SetProperty(current, str2);
            }
            return properties;
        }

        public virtual bool GetBoolean(string key)
        {
            bool boolean = this.GetBoolean(key, DEFAULT_BOOLEAN);
            if (!boolean)
            {
                throw new Exception('\'' + key + "' doesn't map to an existing object");
            }
            return boolean;
        }

        public virtual bool GetBoolean(string key, bool defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is bool)
            {
                return (bool) obj2;
            }
            if (obj2 is string)
            {
                bool newValue = this.TestBoolean((string) obj2).ToUpper().Equals("TRUE");
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Boolean object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetBoolean(key, defaultValue);
            }
            return defaultValue;
        }

        public virtual sbyte GetByte(string key)
        {
            if (!this.ContainsKey(key))
            {
                throw new Exception('\'' + key + " doesn't map to an existing object");
            }
            return (sbyte) this.GetByte(key, DEFAULT_BYTE);
        }

        public virtual byte GetByte(string key, byte defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is byte)
            {
                return (byte) obj2;
            }
            if (obj2 is string)
            {
                byte newValue = byte.Parse((string) obj2);
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Byte object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetByte(key, defaultValue);
            }
            return defaultValue;
        }

        public virtual sbyte GetByte(string key, sbyte defaultValue)
        {
            return this.GetByte(key, defaultValue);
        }

        public virtual double GetDouble(string key)
        {
            double num = this.GetDouble(key, DEFAULT_DOUBLE);
            if (num == 0)
            {
                throw new Exception('\'' + key + "' doesn't map to an existing object");
            }
            return num;
        }

        public virtual double GetDouble(string key, double defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is double)
            {
                return (double) obj2;
            }
            if (obj2 is string)
            {
                double newValue = double.Parse((string) obj2);
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Double object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetDouble(key, defaultValue);
            }
            return defaultValue;
        }

        public virtual float GetFloat(string key)
        {
            float @float = this.GetFloat(key, DEFAULT_SINGLE);
            if (@float == 0)
            {
                throw new Exception('\'' + key + "' doesn't map to an existing object");
            }
            return @float;
        }

        public virtual float GetFloat(string key, float defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is float)
            {
                return (float) obj2;
            }
            if (obj2 is string)
            {
                float newValue = float.Parse((string) obj2);
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Float object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetFloat(key, defaultValue);
            }
            return defaultValue;
        }

        public virtual int GetInt(string name)
        {
            return this.GetInteger(name);
        }

        public virtual int GetInt(string name, int def)
        {
            return this.GetInteger(name, def);
        }

        public virtual int GetInteger(string key)
        {
            int integer = this.GetInteger(key, DEFAULT_INT32);
            if (integer == 0)
            {
                throw new Exception('\'' + key + "' doesn't map to an existing object");
            }
            return integer;
        }

        public virtual int GetInteger(string key, int defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is int)
            {
                return (int) obj2;
            }
            if (obj2 is string)
            {
                int newValue = int.Parse((string) obj2);
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Integer object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetInteger(key, defaultValue);
            }
            return defaultValue;
        }

        public virtual IEnumerator GetKeys(string prefix)
        {
            IEnumerator keys = this.Keys;
            ArrayList list = new ArrayList();
            while (keys.MoveNext())
            {
                object current = keys.Current;
                if ((current is string) && ((string) current).StartsWith(prefix))
                {
                    list.Add(current);
                }
            }
            return list.GetEnumerator();
        }

        public virtual long GetLong(string key)
        {
            long @long = this.GetLong(key, DEFAULT_INT64);
            if (@long == 0)
            {
                throw new Exception('\'' + key + "' doesn't map to an existing object");
            }
            return @long;
        }

        public virtual long GetLong(string key, long defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is long)
            {
                return (long) obj2;
            }
            if (obj2 is string)
            {
                long newValue = long.Parse((string) obj2);
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Long object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetLong(key, defaultValue);
            }
            return defaultValue;
        }

        public virtual Hashtable GetProperties(string key)
        {
            return this.GetProperties(key, new Hashtable());
        }

        public virtual Hashtable GetProperties(string key, Hashtable defaults)
        {
            string[] stringArray = this.GetStringArray(key);
            Hashtable hashTable = new Hashtable(defaults);
            for (int i = 0; i < stringArray.Length; i++)
            {
                string str = stringArray[i];
                int index = str.IndexOf('=');
                if (index <= 0)
                {
                    throw new ArgumentException('\'' + str + "' does not contain an equals sign");
                }
                string str2 = str.Substring(0, index).Trim();
                string newValue = str.Substring(index + 1).Trim();
                CollectionsUtil.PutElement(hashTable, str2, newValue);
            }
            return hashTable;
        }

        public virtual object GetProperty(string key)
        {
            object obj2 = this[key];
            if ((obj2 == null) && (this.defaults != null))
            {
                obj2 = this.defaults[key];
            }
            return obj2;
        }

        public virtual short GetShort(string key)
        {
            short @short = this.GetShort(key, DEFAULT_INT16);
            if (@short == 0)
            {
                throw new Exception('\'' + key + "' doesn't map to an existing object");
            }
            return @short;
        }

        public virtual short GetShort(string key, short defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is short)
            {
                return (short) obj2;
            }
            if (obj2 is string)
            {
                short newValue = short.Parse((string) obj2);
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Short object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetShort(key, defaultValue);
            }
            return defaultValue;
        }

        public virtual string GetString(string key)
        {
            return this.GetString(key, null);
        }

        public virtual string GetString(string key, string defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is string)
            {
                return (string) obj2;
            }
            if (obj2 == null)
            {
                if (this.defaults != null)
                {
                    return this.defaults.GetString(key, defaultValue);
                }
                return defaultValue;
            }
            if (!(obj2 is ArrayList))
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a String object");
            }
            return (string) ((ArrayList) obj2)[0];
        }

        public virtual string[] GetStringArray(string key)
        {
            ArrayList list;
            object obj2 = this[key];
            if (obj2 is string)
            {
                list = new ArrayList(1);
                list.Add(obj2);
            }
            else if (obj2 is ArrayList)
            {
                list = (ArrayList) obj2;
            }
            else
            {
                if (obj2 != null)
                {
                    throw new InvalidCastException('\'' + key + "' doesn't map to a String/Vector object");
                }
                if (this.defaults != null)
                {
                    return this.defaults.GetStringArray(key);
                }
                return new string[0];
            }
            string[] strArray = new string[list.Count];
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = (string) list[i];
            }
            return strArray;
        }

        public virtual ArrayList GetVector(string key)
        {
            return this.GetVector(key, null);
        }

        public virtual ArrayList GetVector(string key, ArrayList defaultValue)
        {
            object obj2 = this[key];
            if (obj2 is ArrayList)
            {
                return (ArrayList) obj2;
            }
            if (obj2 is string)
            {
                ArrayList newValue = new ArrayList(1);
                newValue.Add((string) obj2);
                CollectionsUtil.PutElement(this, key, newValue);
                return newValue;
            }
            if (obj2 != null)
            {
                throw new InvalidCastException('\'' + key + "' doesn't map to a Vector object");
            }
            if (this.defaults != null)
            {
                return this.defaults.GetVector(key, defaultValue);
            }
            return ((defaultValue == null) ? new ArrayList() : defaultValue);
        }

        private void Init(ExtendedProperties exp)
        {
            this.isInitialized = true;
        }

        public virtual bool IsInitialized()
        {
            return this.isInitialized;
        }

        public virtual void Load(Stream input)
        {
            this.Load(input, null);
        }

        public virtual void Load(Stream input, string enc)
        {
            lock (this)
            {
                PropertiesReader reader = null;
                if (enc != null)
                {
                    try
                    {
                        reader = new PropertiesReader(new StreamReader(input, Encoding.GetEncoding(enc)));
                    }
                    catch (IOException)
                    {
                    }
                }
                if (reader == null)
                {
                    reader = new PropertiesReader(new StreamReader(input));
                }
                try
                {
                    string str;
                Label_0039:
                    str = reader.ReadProperty();
                    int index = str.IndexOf('=');
                    if (index > 0)
                    {
                        string key = str.Substring(0, index).Trim();
                        string str3 = str.Substring(index + 1).Trim();
                        if (!string.Empty.Equals(str3))
                        {
                            if ((this.Include != null) && key.ToUpper().Equals(this.Include.ToUpper()))
                            {
                                bool flag;
                                FileInfo info = null;
                                if (str3.StartsWith(this.fileSeparator))
                                {
                                    info = new FileInfo(str3);
                                }
                                else
                                {
                                    if (str3.StartsWith("." + this.fileSeparator))
                                    {
                                        str3 = str3.Substring(2);
                                    }
                                    info = new FileInfo(this.basePath + str3);
                                }
                                if (File.Exists(info.FullName))
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = Directory.Exists(info.FullName);
                                }
                                if ((info != null) && flag)
                                {
                                    this.Load(new FileStream(info.FullName, FileMode.Open, FileAccess.Read));
                                }
                            }
                            else
                            {
                                this.AddProperty(key, str3);
                            }
                        }
                    }
                    goto Label_0039;
                }
                catch (NullReferenceException)
                {
                }
            }
        }

        public virtual void Save(TextWriter output, string Header)
        {
            lock (this)
            {
                if (output != null)
                {
                    TextWriter writer = output;
                    if (Header != null)
                    {
                        writer.WriteLine(Header);
                    }
                    IEnumerator keys = this.Keys;
                    while (keys.MoveNext())
                    {
                        string current = (string) keys.Current;
                        object obj2 = this[current];
                        if (obj2 != null)
                        {
                            if (obj2 is string)
                            {
                                StringBuilder builder = new StringBuilder();
                                builder.Append(current);
                                builder.Append("=");
                                builder.Append((string) obj2);
                                writer.WriteLine(builder.ToString());
                            }
                            else if (obj2 is ArrayList)
                            {
                                IEnumerator enumerator = ((ArrayList) obj2).GetEnumerator();
                                while (enumerator.MoveNext())
                                {
                                    string str2 = (string) enumerator.Current;
                                    StringBuilder builder2 = new StringBuilder();
                                    builder2.Append(current);
                                    builder2.Append("=");
                                    builder2.Append(str2);
                                    writer.WriteLine(builder2.ToString());
                                }
                            }
                        }
                        writer.WriteLine();
                        writer.Flush();
                    }
                }
            }
        }

        public virtual void SetProperty(string key, object value_Renamed)
        {
            this.ClearProperty(key);
            this.AddProperty(key, value_Renamed);
        }

        public virtual ExtendedProperties Subset(string prefix)
        {
            ExtendedProperties properties = new ExtendedProperties();
            IEnumerator keys = this.Keys;
            bool flag = false;
            while (keys.MoveNext())
            {
                object current = keys.Current;
                if ((current is string) && ((string) current).StartsWith(prefix))
                {
                    if (!flag)
                    {
                        flag = true;
                    }
                    string key = null;
                    if (((string) current).Length == prefix.Length)
                    {
                        key = prefix;
                    }
                    else
                    {
                        key = ((string) current).Substring(prefix.Length + 1);
                    }
                    properties.AddPropertyDirect(key, this[current]);
                }
            }
            if (flag)
            {
                return properties;
            }
            return null;
        }

        public virtual string TestBoolean(string value_Renamed)
        {
            string str = value_Renamed.ToLower();
            if ((str.Equals("true") || str.Equals("on")) || str.Equals("yes"))
            {
                return "true";
            }
            if ((str.Equals("false") || str.Equals("off")) || str.Equals("no"))
            {
                return "false";
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            IEnumerator keys = this.Keys;
            keys.Reset();
            while (keys.MoveNext())
            {
                string current = (string) keys.Current;
                object obj2 = this[current];
                builder.Append(current + " => " + this.ValueToString(obj2)).Append(Environment.NewLine);
            }
            return builder.ToString();
        }

        private string ValueToString(object value)
        {
            if (value is ArrayList)
            {
                string str = "ArrayList :: ";
                foreach (object obj2 in (ArrayList) value)
                {
                    if (!str.EndsWith(", "))
                    {
                        str = str + ", ";
                    }
                    str = str + "[" + obj2.ToString() + "]";
                }
                return str;
            }
            return value.ToString();
        }

        public virtual string Include
        {
            get
            {
                return include;
            }
            set
            {
                include = value;
            }
        }

        public virtual IEnumerator Keys
        {
            get
            {
                return this.keysAsListed.GetEnumerator();
            }
        }
    }
}

