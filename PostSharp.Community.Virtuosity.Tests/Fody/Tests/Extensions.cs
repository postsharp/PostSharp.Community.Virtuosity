using System.Linq;
using System.Reflection;
using Xunit;

namespace PostSharp.Community.Virtuosity.Tests.Fody.Tests
{
    public static class Extensions
    {
        public static void EnsureMembersAreSealed(this System.Reflection.Assembly assembly, string className, params string[] memberNames)
        {
            var type = assembly.GetType("PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess." + className, true);

            foreach (var memberName in memberNames)
            {
                var member = type.GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).First();
                if (member is MethodInfo methodInfo)
                {
                    Assert.True(methodInfo.IsFinal, methodInfo.Name);
                }

                if (member is PropertyInfo propertyInfo)
                {
                    var setMethod = propertyInfo.GetSetMethod();
                    if (setMethod != null)
                    {
                        Assert.True(setMethod.IsFinal, propertyInfo.Name);
                    }

                    var getMethod = propertyInfo.GetGetMethod();
                    if (getMethod != null)
                    {
                        Assert.True(getMethod.IsFinal, propertyInfo.Name);
                    }
                }
            }
        }
        public static void EnsureMembersAreNotSealed(this System.Reflection.Assembly assembly, string className, params string[] memberNames)
        {
            var type = assembly.GetType("PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess." + className, true);

            foreach (var memberName in memberNames)
            {
                var member = type.GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).First();
                if (member is MethodInfo methodInfo)
                {
                    Assert.False(methodInfo.IsFinal, methodInfo.Name);
                }

                if (member is PropertyInfo propertyInfo)
                {
                    var setMethod = propertyInfo.GetSetMethod();
                    if (setMethod != null)
                    {
                        Assert.False(setMethod.IsFinal, propertyInfo.Name);
                    }

                    var getMethod = propertyInfo.GetGetMethod();
                    if (getMethod != null)
                    {
                        Assert.False(getMethod.IsFinal, propertyInfo.Name);
                    }
                }
            }
        }
        
        public static void EnsureMembersAreNotVirtual(this System.Reflection.Assembly assembly, string className, params string[] memberNames)
        {
            var type = assembly.GetType("PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess." + className, true);

            foreach (var memberName in memberNames)
            {
                var member = type.GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).First();
                if (member is MethodInfo methodInfo)
                {
                    Assert.False(methodInfo.IsVirtual, methodInfo.Name);
                }

                if (member is PropertyInfo propertyInfo)
                {
                    var setMethod = propertyInfo.GetSetMethod();
                    if (setMethod != null)
                    {
                        Assert.False(setMethod.IsVirtual, propertyInfo.Name);
                    }

                    var getMethod = propertyInfo.GetGetMethod();
                    if (getMethod != null)
                    {
                        Assert.False(getMethod.IsVirtual, propertyInfo.Name);
                    }
                }
            }
        }
        
        public static void EnsureMembersAreVirtual(this System.Reflection.Assembly assembly, string className, params string[] memberNames)
        {
            var type = assembly.GetType("PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess." + className, true);

            foreach (var memberName in memberNames)
            {
                var member = type.GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).First();
                if (member is MethodInfo methodInfo)
                {
                    Assert.True(methodInfo.IsVirtual, methodInfo.Name);
                }

                if (member is PropertyInfo propertyInfo)
                {
                    var setMethod = propertyInfo.GetSetMethod();
                    if (setMethod != null)
                    {
                        Assert.True(setMethod.IsVirtual, propertyInfo.Name);
                    }

                    var getMethod = propertyInfo.GetGetMethod();
                    if (getMethod != null)
                    {
                        Assert.True(getMethod.IsVirtual, propertyInfo.Name);
                    }
                }
            }
        }

    }
}