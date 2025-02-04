﻿// Original code from https://github.com/javiholcman/Wapps.Forms.Map/
// Cacheing implemented by Gadzair

using Android.Graphics;
using Android.Views;

using Microsoft.Maui.Platform;

namespace Maui.GoogleMaps.Android;

public static class Utils
{
    /// <summary>
    /// convert from dp to pixels
    /// </summary>
    /// <param name="dp">Dp.</param>
    public static int DpToPx(double dp)
    {
        var metrics = global::Android.App.Application.Context.Resources.DisplayMetrics;
        return (int)(dp * metrics.Density);
    }

    /// <summary>
    /// convert from px to dp
    /// </summary>
    /// <param name="px">Px.</param>
    public static float PxToDp(int px)
    {
        var metrics = global::Android.App.Application.Context.Resources.DisplayMetrics;
        return px / metrics.Density;
    }

    public static global::Android.Views.View ConvertMauiToNative(Microsoft.Maui.Controls.View view, IElementHandler handler)
    {
        return ConvertMauiToNative(view, handler.MauiContext);
    }

    public static global::Android.Views.View ConvertMauiToNative(Microsoft.Maui.Controls.View view, IMauiContext mauiContext)
    {
        var nativeView = view.ToPlatform(mauiContext);
        return nativeView;
    }

    public static Bitmap ConvertMauiViewToBitmap(Microsoft.Maui.Controls.View view, IMauiContext mauiContext)
    {
        var androidView = ConvertMauiToNative(view, mauiContext);

        var measure = view.Measure(double.PositiveInfinity, double.PositiveInfinity, MeasureFlags.IncludeMargins);

        var width = (int)androidView.Context.ToPixels(measure.Request.Width);
        var height = (int)androidView.Context.ToPixels(measure.Request.Height);

        androidView.Layout(0, 0, width, height);

        Bitmap bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);

        var canvas = new Canvas(bitmap);
        androidView.Draw(canvas);

        return bitmap;
    }

    public static global::Android.Gms.Maps.Model.BitmapDescriptor ConvertMauiViewToBitmapDescriptor(Microsoft.Maui.Controls.View view, IMauiContext mauiContext)
    {
        var bitmap = ConvertMauiViewToBitmap(view, mauiContext);
        var bitmapDescriptor = global::Android.Gms.Maps.Model.BitmapDescriptorFactory.FromBitmap(bitmap);
        return bitmapDescriptor;
    }

    public static global::Android.Widget.FrameLayout AddViewOnFrameLayout(global::Android.Views.View view, int width, int height)
    {
        var layout = new global::Android.Widget.FrameLayout(view.Context);
        layout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
        view.LayoutParameters = new global::Android.Widget.FrameLayout.LayoutParams(width, height);
        layout.AddView(view);
        return layout;
    }
}