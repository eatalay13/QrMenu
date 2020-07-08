using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Framework.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetFullErrorMessage(this ModelStateDictionary modelState)
        {
            var messages = (from entry in modelState
                            from error in entry.Value.Errors
                            select error.ErrorMessage).ToList();

            return string.Join(" ", messages);
        }
    }
}
