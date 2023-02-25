using Schotejil.Clubkampioen.Data.Lenex.Xml;

namespace Schotejil.Clubkampioen;

public static class LenexFormatExtensions
{
    public static string Format(this SwimStyleElement element)
    {
        return $"{element.Distance}m {element.Stroke.Format()}"; 
    }

    public static string Format(this Stroke stroke)
    {
        return stroke switch
        {
            Stroke.Freestyle => "vrije slag",
            Stroke.Fly => "vlinderslag",
            Stroke.Backstroke => "rugslag",
            Stroke.Breaststroke => "schoolslag",
            Stroke.Medley => "wisselslag",
            _ => stroke.ToString(),
        };
    }

    public static string Format(this TimeSpan? time)
    {
        if (time is not null)
        {
            return (time is { TotalSeconds: < 0 } ? "-" : "") + time?.ToString(@"mm\:ss\.ff");
        }

        return "NT";
    }
}
