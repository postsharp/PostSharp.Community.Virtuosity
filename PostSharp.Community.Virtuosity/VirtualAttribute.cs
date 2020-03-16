using System;
using PostSharp.Extensibility;

namespace PostSharp.Community.Virtuosity
{
    /// <summary>
    /// All non-private properties and methods of the classes affected by this attribute will be changed to virtual.
    /// Affected methods that hide these methods are going to be changed to <c>override</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    [MulticastAttributeUsage(MulticastTargets.Method | MulticastTargets.Property)]
    [RequirePostSharp("PostSharp.Community.Virtuosity.Weaver", "VirtuosityTask")]
    public class VirtualAttribute : MulticastAttribute
    {
    }
}