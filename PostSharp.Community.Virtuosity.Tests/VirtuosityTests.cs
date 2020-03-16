using System.Reflection;
using Xunit;

namespace PostSharp.Community.Virtuosity.Tests
{
    public class VirtuosityTests
    {
        [Fact]
        public void Reflection()
        {
            Assert.True(typeof(JavaLikeClass).GetMethod("Ha").IsVirtual);
            foreach (MethodInfo methodInfo in typeof(JavaLikeClass).GetRuntimeMethods())
            {
                Assert.True(methodInfo.IsVirtual || 
                            methodInfo.IsPrivate || 
                    methodInfo.DeclaringType != typeof(JavaLikeClass), methodInfo.ToString());
            }
        }

        [Fact]
        public void CallsAreVirtual()
        {          
            JavaLikeClass instance = new JavaInheritor();
            Assert.Equal("Subclass", instance.Ha());
            Assert.Equal("Subclass", instance.He());
        }
    }
}