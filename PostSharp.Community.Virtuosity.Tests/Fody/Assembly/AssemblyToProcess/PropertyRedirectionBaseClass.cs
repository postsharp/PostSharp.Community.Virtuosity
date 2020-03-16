﻿﻿namespace PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess
 {
     public class PropertyRedirectionBaseClass
     {
         public string Property1 => Property2;

         public string Property2 => "Base";
     }
 }