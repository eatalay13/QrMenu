using System;
using System.Collections.Generic;
using System.Text;

namespace WebUI.Framework.Themes
{
    public interface IThemeContext
    {
        /// <summary>
        /// Get or set current theme system name
        /// </summary>
        string WorkingThemeName { get; set; }
    }
}
