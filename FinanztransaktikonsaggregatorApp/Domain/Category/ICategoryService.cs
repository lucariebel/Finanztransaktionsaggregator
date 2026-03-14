using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanztransaktikonsaggregatorApp.Domain.Category;

public interface ICategoryService
{
    string GetCategoryForDescription(string description);
}
