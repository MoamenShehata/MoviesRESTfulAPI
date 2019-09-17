using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesRESTfulAPI.Models
{
    public class MovieResourceParameters
    {
        const int pageMaxSize = 10;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > pageMaxSize ? pageMaxSize : value;
        }

        public string Genre { get; set; }
        public int Rate { get; set; }
        public string rateop { get; set; }
    }
}