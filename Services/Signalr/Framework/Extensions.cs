using System;

namespace FliGen.Services.Signalr.Framework
{
    public static class Extensions
    {
        public static string ToUserGroup(this Guid userId)
        {
            return $"users:{userId}";
        }
    }
}