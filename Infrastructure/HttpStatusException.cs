using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace expense_tracker.Infrastructure
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode Status { get; set; }
        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
