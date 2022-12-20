using System;
namespace NotificationDotNet6SignalR.Commons.Extensions;

internal static class StringExtensions
{
	public static Guid ToGuid(this string source) => new(source);
	
}