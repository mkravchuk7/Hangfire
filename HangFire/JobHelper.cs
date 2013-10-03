﻿using System;
using System.Linq;

using ServiceStack.Text;

namespace HangFire
{
    internal static class JobHelper
    {
        public static string TryToGetQueueName(string jobType)
        {
            var type = Type.GetType(jobType);
            if (type == null)
            {
                return null;
            }

            return GetQueueName(type);
        }

        public static string GetQueueName(Type jobType)
        {
            var attribute = jobType
                .GetCustomAttributes(true)
                .Cast<QueueNameAttribute>()
                .FirstOrDefault();

            return attribute != null ? attribute.Name : "default";
        }

        public static string ToJson(object value)
        {
            return JsonSerializer.SerializeToString(value);
        }

        public static T FromJson<T>(string value)
        {
            return JsonSerializer.DeserializeFromString<T>(value);
        }

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }

        public static DateTime FromTimestamp(long value)
        {
            return Epoch.AddSeconds(value);
        }

        public static string ToStringTimestamp(DateTime value)
        {
            return ToTimestamp(value).ToString();
        }

        public static DateTime FromStringTimestamp(string value)
        {
            return FromTimestamp(long.Parse(value));
        }
    }
}