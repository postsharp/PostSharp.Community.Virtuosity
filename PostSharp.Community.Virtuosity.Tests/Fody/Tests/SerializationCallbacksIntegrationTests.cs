using System;
using System.Reflection;
using Xunit;

namespace PostSharp.Community.Virtuosity.Tests.Fody.Tests
{
    public class SerializationCallbacksIntegrationTests  
    {
        Type type;
        System.Reflection.Assembly assembly;

        public SerializationCallbacksIntegrationTests()
        {
            assembly = this.GetType().Assembly;
            type = assembly.GetType("PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess.SerializationCallbackMethods", true);
        }

        [Fact]
        public void Method_MarkedByOnSerializingAttribute_MustNotMakeVirtual()
        {
            AssertUnmodifiedMethod("Serializing");
        }

        [Fact]
        public void Method_MarkedByOnSerializedAttribute_MustNotMakeVirtual()
        {
            AssertUnmodifiedMethod("Serialized");
        }

        [Fact]
        public void Method_MarkedByOnDeserializingAttribute_MustNotMakeVirtual()
        {
            AssertUnmodifiedMethod("Deserializing");
        }

        [Fact]
        public void Method_MarkedByOnDeserializedAttribute_MustNotMakeVirtual()
        {
            AssertUnmodifiedMethod("Deserialized");
        }

        [Fact]
        public void MustBeAbleToInstantiateType()
        {
            Activator.CreateInstance(type);
        }

        void AssertUnmodifiedMethod(string methodName)
        {
            var method = type.GetMethod(methodName);

            Assert.False(method.IsAbstract, $"{method.Name} IsAbstract");
            Assert.False(method.IsSpecialName, $"{method.Name} IsSpecialName");
            Assert.False(method.IsVirtual, $"{method.Name} IsVirtual");
            Assert.False(method.IsStatic, $"{method.Name} IsStatic");
            Assert.False(method.IsFinal, $"{method.Name} IsFinal");
            Assert.False(method.Attributes.HasFlag(MethodAttributes.NewSlot), $"{method.Name} HasFlag(NewSlot)");
            Assert.True(method.IsHideBySig, $"{method.Name} IsHideBySig");
            Assert.True(method.IsPublic, $"{method.Name} IsPublic");
        }
    }
}