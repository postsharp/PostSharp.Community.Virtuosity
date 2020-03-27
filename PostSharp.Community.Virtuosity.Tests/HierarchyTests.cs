using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PostSharp.Community.Virtuosity.Tests
{
    public class HierarchyTests
    {
        [Fact]
        public void BaseVirtualMethod_WillNotBeChanged()
        {
            var method = typeof(A).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "A.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "A.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "A.M is Final");
        }

        [Fact]
        public void HidingMethod_WillBeVirtual()
        {
            var method = typeof(B).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "B.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "B.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "B.M is Final");
        }

        [Fact]
        public void NewSlotVirtual_WillNotBeChanged()
        {
            var method = typeof(C).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "C.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "C.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "C.M is Final");
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
        public void SealedOverride_WillNotBeChanged()
        {
            var method = typeof(E).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "E.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "E.M is NewSlot");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "E.M is Final");
        }

        [Fact]
        public void HidingMethodAfterSealed_WillBeVirtualNewslot()
        {
            var method = typeof(F).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "F.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "F.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "F.M is Final");
        }

        [Fact]
        public void OverrideAfterHiding_WillBeVirtual()
        {
            var method = typeof(G).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "G.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "G.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "G.M is Final");
        }

        [Fact]
        public void ReturnValues()
        {
            Assert.Equal(2, (new G() as A).M());
            Assert.Equal(2, (new G() as B).M());
            Assert.Equal(5, (new G() as C).M());
            Assert.Equal(5, (new G() as D).M());
            Assert.Equal(5, (new G() as E).M());
            Assert.Equal(7, (new G() as F).M());
            Assert.Equal(7, (new G() as G).M());
        }

        [Virtual]
        public class A
        {
            public virtual int M() => 1;
        }

        [Virtual]
        public class B : A
        {
            public new int M() => 2;
        }

        [Virtual]
        public abstract class C : B
        {
            public new virtual int M() => 3;
        }

        [Virtual]
        public class D : C
        {
            public override int M() => 4;
        }

        [Virtual]
        public class E : D
        {
            public sealed override int M() => 5;
        }

        [Virtual]
        public class F : E
        {
            public new int M() => 6;
        }

        [Virtual]
        public class G : F
        {
            public new int M() => 7;
        }
    }
}
