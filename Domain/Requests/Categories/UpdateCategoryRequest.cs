using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker.Domain.Requests.Categories
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; }
    }
}
