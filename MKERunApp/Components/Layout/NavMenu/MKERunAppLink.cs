﻿using MudBlazor;

namespace MKERunApp.Components.Layout.NavMenu
{
    public class MKERunAppLink(string link, string icon)
    {
        public string Link => _Link;
        public string Icon => _Icon;

        private readonly string _Link = link;
        private readonly string _Icon = icon;

        public static readonly MKERunAppLink[] AllNavLinks =
        [
            new MKERunAppLink("/", Icons.Material.Filled.Today),
            new MKERunAppLink("/CoachedTeams", Icons.Material.Filled.Sports),
            new MKERunAppLink("/User", Icons.Material.Filled.AccountCircle)
        ];

        public static readonly MKERunAppLink[] MobileNavLinks =
        [
            new MKERunAppLink("/", Icons.Material.Filled.Today),
            new MKERunAppLink("/User", Icons.Material.Filled.AccountCircle)
        ];
    }
}
