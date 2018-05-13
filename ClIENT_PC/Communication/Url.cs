using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller.Communacation
{
    class URL_STRUCT
    {
        public string url { get; set; }
        public string title { get; set; }
        public string browser { get; set; }
    }
    class URL
    {
        string url;
        string title;
        string browser;
        public URL(string url, string title, string browser)
        {
            this.url = url;
            this.title = title;
            this.browser = browser;
        }

        public string getData()
        {
            return browser + " - " + title + " - " + url;
        }
    }
}
