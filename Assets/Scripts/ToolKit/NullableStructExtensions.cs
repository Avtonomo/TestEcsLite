using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Debug = UnityEngine.Debug;

namespace Studio3.Toolkit
{
    public static class NullableStructExtensions
    {
        [ContractAnnotation("nullable:null => halt")]
        public static T GetOrThrow<T>(
            this T? nullable,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : struct
        {
            return nullable ?? throw BuildException(memberName);
        }

        [NotNull]
        [ContractAnnotation("nullable:null => halt")]
        public static T GetOrThrow<T>(
            [CanBeNull] this T nullable,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            return nullable ?? throw BuildException(memberName);
        }

        [ContractAnnotation("nullable:null => halt")]
        public static void VerifyNotNull<T>(
            this T? nullable,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : struct
        {
            if (!nullable.HasValue)
                throw BuildException(memberName);
        }

        [ContractAnnotation("nullable:null => halt")]
        public static void VerifyNotNull<T>(
            [CanBeNull] this T nullable,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (nullable == null)
                throw BuildException(memberName);
        }

        [ContractAnnotation("source:null => halt")]
        public static void VerifyNotNullOrEmpty<T>(
            [CanBeNull] this IEnumerable<T> source,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (source.IsNullOrEmpty())
                throw BuildException(memberName);
        }
        
        [ContractAnnotation("source:null => halt")]
        public static void VerifyNotNullOrEmpty<T>(
            [CanBeNull] this IReadOnlyCollection<T> source,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (source.IsNullOrEmpty())
                throw BuildException(memberName);
        }
        
        [ContractAnnotation("source:null => halt")]
        public static void VerifyNotNullOrEmpty<T>(
            [CanBeNull] this T[] source,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (source.IsNullOrEmpty())
                throw BuildException(memberName);
        }
        
        [ContractAnnotation("source:null => halt")]
        public static void VerifyNotNullOrEmptyOrNullItems<T>(
            [CanBeNull] this T[] source,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (source.IsNullOrEmpty() || source.Any(s => s == null))
                throw BuildException(memberName);
        }

        [ContractAnnotation("source:null => halt")]
        public static IReadOnlyCollection<T> GetOrThrowIfNullOrEmpty<T>(
            [CanBeNull] this IReadOnlyCollection<T> source,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (source.IsNullOrEmpty())
                throw BuildException(memberName);
                    
            return source;
        }
        
        [ContractAnnotation("source:null => halt")]
        public static T[] GetOrThrowIfNullOrEmpty<T>(
            [CanBeNull] this T[] source,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (source.IsNullOrEmpty())
                throw BuildException(memberName);
                    
            return source;
        }
        
        [ContractAnnotation("source:null => halt")]
        public static T[] GetOrThrowIfNullOrEmptyOrNullItems<T>(
            [CanBeNull] this T[] source,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            where T : class
        {
            if (source.IsNullOrEmpty() || source.Any(s => s == null))
                throw BuildException(memberName);
                    
            return source;
        }

        private static Exception BuildException(string memberName)
        {
            var exception = new InvalidOperationException($"Expected nullable (in {memberName} method) to have value");
            PrintError(exception);
            return exception;
        }

        [Conditional("UNITY_EDITOR")]
        private static void PrintError(
            Exception ex)
        {
            Debug.LogError(ex.StackTrace == null
                ? ex.Message
                : $"'{ex.Message}' {ex.StackTrace}");
        }
    }
}