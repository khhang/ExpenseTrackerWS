using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Requests.Categories
{
    public class CreateSubcategoryRequest
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
