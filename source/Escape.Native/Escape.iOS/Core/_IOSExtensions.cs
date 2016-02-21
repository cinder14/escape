using System;
using Foundation;
using System.Collections.Generic;

namespace Escape.iOS
{
    public static class _IOSExtensions
    {
        public static DateTime ToDateTime(this NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0) );
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

        public static NSDate ToNSDate(this DateTime date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0) );
            return NSDate.FromTimeIntervalSinceReferenceDate((date - reference).TotalSeconds);
        }

        public static NSMutableDictionary ToNSDictionary(this Dictionary<string, string> dictionary) 
        {
            NSMutableDictionary result = new NSMutableDictionary();
            if (dictionary != null)
            {
                foreach (var item in dictionary)
                {
                    result.Add(NSObject.FromObject(item.Key), NSObject.FromObject(item.Value));
                }
            }
            return result;
        }
    }
}

