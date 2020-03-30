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
            // already virtual: no effect
            var method = typeof(A).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "A.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "A.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "A.M is Final");
        }

        [Fact]
        public void HidingMethod_WillNotBeChanged()
        {
            // method specifically made new even though it could have been override: no effect
            var method = typeof(B).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "B.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "B.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "B.M is Final");
        }

        [Fact]
        public void NewSlotVirtual_WillNotBeChanged()
        {
            // method specifically made new: no effect
            var method = typeof(C).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "C.M is Virtual");
            Assert.True((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "C.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "C.M is Final");
        }

        [Fact]
        public void Override_WillNotBeChanged()
        {
            // method already virtual: no effect
            var method = typeof(D).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "D.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "D.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "D.M is Final");
        }

        [Fact]
        public void SealedOverride_WillBeUnsealed()
        {
            // Sealed methods are unsealed, just like in Fody
            var method = typeof(E).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "E.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "E.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "E.M is Final");
        }

        [Fact]
        public void HidingMethodAfterSealed_WillBeVirtualOnly()
        {
            // The base is no longer sealed, so we can override it:
            var method = typeof(F).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "F.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "F.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "F.M is Final");
        }

        [Fact]
        public void OverrideAfterHiding_WillBeVirtual()
        {
            // Base was affected by us, so we change new to override:
            var method = typeof(G).GetMethod("M");

            Assert.True((method.Attributes & System.Reflection.MethodAttributes.Virtual) != 0, "G.M is Virtual");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.NewSlot) != 0, "G.M is NewSlot");
            Assert.False((method.Attributes & System.Reflection.MethodAttributes.Final) != 0, "G.M is Final");
        }

        [Fact]
        public void ReturnValues()
        {
            Assert.Equal(1, (new G() as A).M());
            Assert.Equal(2, (new G() as B).M());
            Assert.Equal(7, (new G() as C).M());
            Assert.Equal(7, (new G() as D).M());
            Assert.Equal(7, (new G() as E).M());
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
