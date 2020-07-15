using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Requests.Categories
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
    }
}
