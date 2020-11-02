using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleManager.Models
{
    public class RequestModel
    {
        public string Link { get; set; }
        public string Html { get; set; }
        public string Title { get; set; }
        public bool ReSend { get; set; }
    }
}
