namespace LinkPoint.Business.Utilities.Extentions;

public static class TimeManager
{
    public static string GetElapsedTime( this DateTime postTime)   //post paylasilan vaxtdan indiki vaxta qeder olan hisseni gosterir
    {
        TimeSpan elapsed = DateTime.UtcNow - postTime;
        if (elapsed.TotalSeconds < 60)
        {
            return $"{(int)elapsed.TotalSeconds} seconds ago";
        }
        else if (elapsed.TotalMinutes < 60)
        {
            return $"{(int)elapsed.TotalMinutes} minutes ago";
        }
        else if (elapsed.TotalHours < 24)
        {
            return $"{(int)elapsed.TotalHours} hours ago";
        }
        else
        {
            return $"{(int)elapsed.TotalDays} days ago";
        }
    }
}
