using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PostSharp.Community.Virtuosity.Tests
{
    public class HierarchyReverseTests
    {
        [Fact]
        public void BaseVirtualMethod_WillNotBeChanged()
        {
            var method = typeof(G).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "G.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "G.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "G.M is Final");
        }

        [Fact]
        public void HidingMethod_WillNotBeChanged()
        {
            var method = typeof(F).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "F.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "F.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "F.M is Final");
        }

        [Fact]
        public void NewSlotVirtual_WillNotBeChanged()
        {
            var method = typeof(E).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "E.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "E.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "E.M is Final");
        }

        [Fact]
        public void Override_WillNotBeChanged()
        {
            var method = typeof(D).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "D.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "D.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "D.M is Final");
        }

        [Fact]
        public void SealedOverride_WillBeUnsealed()
        {
            var method = typeof(C).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "C.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "C.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "C.M is Final");
        }

        [Fact]
        public void HidingMethodAfterSealed_WillBeUsingThatMethod()
        {
            var method = typeof(B).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "B.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "B.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "B.M is Final");
        }

        [Fact]
        public void OverrideAfterHiding_WillBeVirtual()
        {
            var method = typeof(A).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "A.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "A.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "A.M is Final");
        }

        [Fact]
        public void ReturnValues()
        {
            Assert.Equal(1, (new A() as G).M());
            Assert.Equal(2, (new A() as F).M());
            Assert.Equal(7, (new A() as E).M());
            Assert.Equal(7, (new A() as D).M());
            Assert.Equal(7, (new A() as C).M());
            Assert.Equal(7, (new A() as B).M());
            Assert.Equal(7, (new A() as A).M());
        }

        [Virtual]
        public class A : B
        {
            public new int M() => 7;
        }

        [Virtual]
        public class B : C
        {
            public new int M() => 6;
        }

        [Virtual]
        public class C : D
        {
            public sealed override int M() => 5;
        }

        [Virtual]
        public class D : E
        {
            public override int M() => 4;
        }

        [Virtual]
        public abstract class E : F
        {
            public new virtual int M() => 3;
        }

        [Virtual]
        public class F : G
        {
            public new int M() => 2; 
        }

        [Virtual]
        public class G
        {
            public virtual int M() => 1;
        }
    }
}
