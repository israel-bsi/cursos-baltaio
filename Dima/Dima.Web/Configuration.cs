﻿using MudBlazor;
using MudBlazor.Utilities;

namespace Dima.Web;

public static class Configuration
{
    public const string HttpClientName = "dima";
    public static string S { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = "http://localhost:5168";
    public static string StripePublicKey { get; set; } = "";
    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new Default
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        },
        Palette = new PaletteLight
        {
            Primary = "#1EFA2D",
            PrimaryContrastText = new MudColor("#000000"),
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Grey.Lighten4,
            AppbarBackground = "#1EFA2D",
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.Black,
            DrawerBackground = Colors.LightGreen.Darken4,
        },
        PaletteDark = new PaletteDark
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            //Background = Colors.LightGreen.Darken4
            AppbarBackground = Colors.LightGreen.Accent3,
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = new MudColor("#000000")
        }
    };
}