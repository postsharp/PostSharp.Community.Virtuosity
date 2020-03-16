using System;
using System.Runtime.Serialization;

namespace PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess
{
    [Serializable]
    public class SerializationCallbackMethods
    {
        [OnSerializing]
        public void Serializing(StreamingContext context) { }

        [OnSerialized]
        public void Serialized(StreamingContext context) { }

        [OnDeserializing]
        public void Deserializing(StreamingContext context) { }

        [OnDeserialized]
        public void Deserialized(StreamingContext context) { }
    }
}