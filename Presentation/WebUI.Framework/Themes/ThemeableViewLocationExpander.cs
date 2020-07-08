using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace WebUI.Framework.Themes
{
    public class ThemeableViewLocationExpander : IViewLocationExpander
    {
        private const string THEME_KEY = "ea.themename";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue(THEME_KEY, out string theme))
            {
                viewLocations = new[] {
                        $"/Themes/{theme}/Views/{{1}}/{{0}}.cshtml",
                        $"/Themes/{theme}/Views/Shared/{{0}}.cshtml",
                    }
                    .Concat(viewLocations);
            }


            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context.AreaName?.Equals("Admin") ?? false)
                return;

            var themeContext = (IThemeContext)context.ActionContext.HttpContext.RequestServices.GetService(typeof(IThemeContext));
            context.Values[THEME_KEY] = themeContext.WorkingThemeName;
        }
    }
}
