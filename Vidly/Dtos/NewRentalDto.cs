using System;
using System.Collections.Generic;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public String CustomerName { get; set; }
        public List<int> BookIds { get; set; }
        public List<String> BooksNameList { get; set; }
    }
}