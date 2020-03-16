#pragma warning disable 108,114

namespace PostSharp.Community.Virtuosity.Tests
{
    [Virtual]
    public class JavaInheritor : JavaLikeClass
    {
        public string Ha()
        {
            return "Subclass";
        }
        public new string He()
        {
            return "Subclass";
        }
    }
}