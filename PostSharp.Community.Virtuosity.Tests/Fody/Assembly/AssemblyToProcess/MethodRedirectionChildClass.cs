﻿﻿namespace PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess
 {
     public class MethodRedirectionChildClass : MethodRedirectionBaseClass
     {
         public new string Method2()
         {
             return "Child";
         }
     }
 }