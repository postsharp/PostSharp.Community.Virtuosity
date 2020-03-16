﻿namespace PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess
{
    public class PropertyRedirectionChildClass : PropertyRedirectionBaseClass
    {
        public new string Property2 => "Child";
    }
}