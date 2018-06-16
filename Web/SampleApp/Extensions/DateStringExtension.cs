using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerce
{
    public static class DateStringExtension
    {
        public static string FriendlyDaysPast(this DateTime dt)
        {
            TimeSpan span = (DateTime.Now - dt);

            // Normalize time span
            bool future = false;
            if (span.TotalSeconds < 0)
            {
                // In the future
                span = -span;
                future = true;
            }

            // Test for Now
            double totalSeconds = span.TotalSeconds;
            if (totalSeconds < 0.9)
            {
                return "Now";
            }

            // Date/time near current date/time
            string format = (future) ? "In {0} {1}" : "{0} {1} ago";
            if (totalSeconds < 55)
            {
                // Seconds
                int seconds = Math.Max(1, span.Seconds);
                return String.Format(format, seconds,
                    (seconds == 1) ? "second" : "seconds");
            }

            if (totalSeconds < (55 * 60))
            {
                // Minutes
                int minutes = Math.Max(1, span.Minutes);
                return String.Format(format, minutes,
                    (minutes == 1) ? "minute" : "minutes");
            }
            if (totalSeconds < (24 * 60 * 60))
            {
                // Hours
                int hours = Math.Max(1, span.Hours);
                return String.Format(format, hours,
                    (hours == 1) ? "hour" : "hours");
            }

            // Format both date and time
            if (totalSeconds < (48 * 60 * 60))
            {
                // 1 Day
                format = (future) ? "Tomorrow" : "Yesterday";
            }
            else if (totalSeconds < (3 * 24 * 60 * 60))
            {
                // 2 Days
                format = String.Format(format, 2, "days");
            }
            else
            {
                // Absolute date
                if (dt.Year == DateTime.Now.Year)
                    format = dt.ToString(@"MMM d");
                else
                    format = dt.ToString(@"MMM d, yyyy");
            }

            // Add time
            return String.Format("{0} at {1:h:mmt}", format, dt);
        }

        /// <summary>
        /// Converts a datetime object into a human readable format such as "1 hour ago", "Yesterday", etc
        /// </summary>
        public static string ToFriendlyDateTime(this DateTime dateTime)
        {
            var now = DateTime.Now;

            // Work out difference
            var diff = now.Subtract(dateTime);

            // Are we in the same day?
            if (dateTime.Date == DateTime.Today)
            {
                // Less than one minute ago
                if (diff.TotalSeconds < 60)
                {
                    return "Just now";
                }

                // Less than 2 minutes ago
                if (diff.TotalMinutes < 2)
                {
                    return "1 minute ago";
                }

                // Less than 1 hour ago
                if (diff.TotalHours < 1)
                {
                    return $"{Math.Floor(diff.TotalSeconds / 60)} minutes ago";
                }

                // Less than 2 hours ago
                if (diff.TotalHours < 2)
                {
                    return "1 hour ago";
                }

                // Any other number of hours (up to 24 hours ago, but will always be within the current day)
                if (diff.TotalHours < 24)
                {
                    return $"{Math.Floor(diff.TotalSeconds / 3600)} hours ago";
                }
            }

            // Is it yesterday?
            if (dateTime.Date == DateTime.Today.AddDays(-1))
            {
                return "Yesterday";
            }

            // Otherwise, it's days, weeks, months or years
            var currentWeekStart = now.StartOfWeek();
            var lastWeekStart = currentWeekStart.AddDays(-7);

            // Within this week
            if (dateTime >= currentWeekStart && dateTime < now)
            {
                var weekDiff = dateTime.Subtract(currentWeekStart);

                if (weekDiff.Days < 7)
                {
                    return $"{weekDiff.Days} days ago";
                }
            }

            // Last week
            if (dateTime >= lastWeekStart && dateTime < currentWeekStart)
            {
                return "Last week";
            }

            // Within the current month
            if (dateTime.Month == now.Month && dateTime.Year == now.Year)
            {
                return $"{Math.Ceiling(diff.TotalDays / 7)} weeks ago";
            }

            // Last month
            if (dateTime.Month == now.AddMonths(-1).Month && dateTime.Year == now.Year)
            {
                return "Last month";
            }

            // Within the current year
            if (dateTime.Year == now.Year && dateTime < now)
            {
                if (dateTime.Month <= now.Month)
                {
                    var monthDiff = now.Month - dateTime.Month;

                    return $"{monthDiff} months ago";
                }
            }

            // Last year
            if (dateTime.Year == now.AddYears(-1).Year)
            {
                return "Last year";
            }


            // TODO -- x number of years



            // Failsafe
            return dateTime.ToLongDateString();
        }


    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt)
        {
            var diff = dt.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }
}
