namespace Commons.Collections
{
    using System;
    using System.Collections;

    public class StringTokenizer
    {
        private string delimiters;
        private ArrayList elements;
        private string source;

        public StringTokenizer(string source)
        {
            this.delimiters = " \t\n\r";
            this.elements = new ArrayList();
            this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
            this.RemoveEmptyStrings();
            this.source = source;
        }

        public StringTokenizer(string source, string delimiters)
        {
            this.delimiters = " \t\n\r";
            this.elements = new ArrayList();
            this.delimiters = delimiters;
            this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
            this.RemoveEmptyStrings();
            this.source = source;
        }

        public bool HasMoreTokens()
        {
            return (this.elements.Count > 0);
        }

        public string NextToken()
        {
            if (this.source == "")
            {
                throw new Exception();
            }
            this.elements = new ArrayList();
            this.elements.AddRange(this.source.Split(this.delimiters.ToCharArray()));
            this.RemoveEmptyStrings();
            string oldValue = (string) this.elements[0];
            this.elements.RemoveAt(0);
            this.source = this.source.Replace(oldValue, "");
            this.source = this.source.TrimStart(this.delimiters.ToCharArray());
            return oldValue;
        }

        public string NextToken(string delimiters)
        {
            this.delimiters = delimiters;
            return this.NextToken();
        }

        private void RemoveEmptyStrings()
        {
            for (int i = 0; i < this.elements.Count; i++)
            {
                if (((string) this.elements[i]) == "")
                {
                    this.elements.RemoveAt(i);
                    i--;
                }
            }
        }

        public int Count
        {
            get
            {
                return this.elements.Count;
            }
        }
    }
}

