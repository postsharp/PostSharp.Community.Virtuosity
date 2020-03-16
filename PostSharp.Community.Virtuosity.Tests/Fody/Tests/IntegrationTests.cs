using System;
using System.Reflection;
using PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess;
using Xunit;

namespace PostSharp.Community.Virtuosity.Tests.Fody.Tests
{

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

    public class IntegrationTests
    {
        private readonly System.Reflection.Assembly theAssembly;

        public IntegrationTests()
        {
            theAssembly = this.GetType().Assembly;
        }
        
        [Fact]
        public void MethodsAndPropertiesAreMarkedAsVirtual()
        {
            theAssembly.EnsureMembersAreVirtual("MethodsAndPropertiesAreMarkedAsVirtualClass", "Method1", "Property1");
        }

      

        [Fact]
        public void NonAbstractMethodsAndPropertiesOnAbstractClassAreMarkedAsVirtual()
        {
            theAssembly.EnsureMembersAreVirtual("AbstractClass", "NonAbstractMethod", "NonAbstractProperty");
        }

        [Fact]
        public void InterfaceSealedClass()
        {
            theAssembly.EnsureMembersAreSealed("InterfaceSealedClass", "Property");
            theAssembly.EnsureMembersAreVirtual("InterfaceSealedClass", "Property");
        }
        
        [Fact]
        public void EnsureNested()
        {
            theAssembly.EnsureMembersAreVirtual("Outer+Inner", "Property");
        }

        [Fact]
        public void EnsureNewToOverrideWithInterface()
        {
            var child = this.GetType().Assembly.GetType("PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess.EnsureNewToOverrideWithInterface.ChildImplementation");
            var baseProperty = this.GetType().Assembly.GetType("PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess.EnsureNewToOverrideWithInterface.BaseImplementation")
                .GetProperty("Property", BindingFlags.Public | BindingFlags.Instance);
            var propValue = baseProperty.GetValue(Activator.CreateInstance(child), null);
            Assert.Equal("Bravo", propValue);
        }

        [Fact]
        public void InterfaceVirtualClass()
        {
            theAssembly.EnsureMembersAreVirtual("InterfaceVirtualClass", "Property");
            theAssembly.EnsureMembersAreNotSealed("InterfaceVirtualClass", "Property");
        }

        [Fact]
        public void EnsurePropertyCallIsRedirected()
        {
            var instance = new PropertyRedirectionChildClass();
            Assert.Equal("Child", instance.Property1);
        }

        [Fact]
        public void SealedNotMarkedVirtual()
        {
            theAssembly.EnsureMembersAreNotVirtual("SealedClass", "Method1", "Property1");
        }
    }
}