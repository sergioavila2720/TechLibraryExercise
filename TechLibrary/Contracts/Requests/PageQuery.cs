using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;

namespace TechLibrary.Contracts.Requests
{
    // Created a page query class that holds the pagenumber and page size when they are passed. 
    // Also set a max num of page size so that we can't just get the maximum page size
    public class PageQuery
    {
        public PageQuery()
        {
            // Defaults
            PageNumber = 1;
            PageSize = maxPageSize;
        }
        public PageQuery(int pageNumber, int pageSize) 
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        const int maxPageSize = 50;
        public int PageNumber { get; set; }

        public int PageSize {get; set; }
    }
}
