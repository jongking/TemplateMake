using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorEngine.Text;

namespace TemplateMake
{
    public class RazorHelper
    {
        public RawString HtmlRaw(string str)
        {
            return new RawString(str);
        }
    }
}
