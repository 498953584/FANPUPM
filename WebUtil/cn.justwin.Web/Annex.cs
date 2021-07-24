namespace cn.justwin.Web
{
    using System;
    using System.Text;
    using System.Web;

    /// <summary>
    /// 文件信息
    /// 包含文件名称、文件大小
    /// </summary>
    public class Annex
    {
        private string length;
        private string name;
        private string path;
        /// <summary>
        /// 文件是否只读，默认可删除
        /// </summary>
        private bool readOnly;

        /// <summary>
        /// 文件大小
        /// </summary>
        public string Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name
        {
            get
            {
                return HttpUtility.UrlEncode(this.name, Encoding.UTF8).Replace("+", "%20").Replace("%2b", "+");
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// 文件路径
        /// 当加载不同目录下的文件，可是实现文件删除
        /// </summary>
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
            set
            {
                this.readOnly = value;
            }
        }
    }
}

