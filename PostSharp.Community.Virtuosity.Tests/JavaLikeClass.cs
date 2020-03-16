namespace PostSharp.Community.Virtuosity.Tests
{
    [Virtual]
    public class JavaLikeClass
    {
        public virtual int A { get; set; }
        
        public int B { get; set; }

        public virtual void Hello()
        {
            
        }

        public string Ha()
        {
            return "BaseClass";
        }
        public string He()
        {
            return "BaseClass";
        }

        private void Noro()
        {
            
        }

    }
}