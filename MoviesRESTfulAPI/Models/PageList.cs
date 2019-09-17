using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesRESTfulAPI.Models
{
    public class PageList<T> : List<T>
    {
        public int TotalCount { get; private set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);


        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PageList(List<T> itemsInPage, int totalCount, int pgNumber, int pgSize)
        {
            AddRange(itemsInPage);
            TotalCount = totalCount;
            PageCount = itemsInPage.Count;
            PageSize = pgSize;
            CurrentPage = pgNumber;
        }
    }
}