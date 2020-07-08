using System;
using System.Collections.Generic;
using System.Text;

namespace WebUI.Framework.Themes
{
    public class ThemeContext : IThemeContext
    {
        public string WorkingThemeName { get; set; } = "Default";
    }
}
