using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Filters.Infrastructure
{
    public class CustomOverrideActionFiltersAttribute:FilterAttribute,IOverrideFilter
    {
        public Type FiltersToOverride => typeof(IActionFilter);
    }
}