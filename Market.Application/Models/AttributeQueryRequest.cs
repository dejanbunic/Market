using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.Models
{
    public class AttributeQueryRequest: PaginationRequest
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
