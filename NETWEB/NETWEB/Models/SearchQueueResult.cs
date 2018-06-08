using Model.Enum;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NETWEB.Models
{
    public class SearchQueueResult:SearchResultViewModel
    {
        public LuceneEnum LuceneEnum { get; set; }
    }
}