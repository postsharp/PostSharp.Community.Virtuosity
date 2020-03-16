﻿﻿namespace PostSharp.Community.Virtuosity.Tests.Fody.Assembly.AssemblyToProcess
 {
     public class MethodRedirectionBaseClass
     {
         public string Method1()
         {
             return Method2();
         }

         public string Method2()
         {
             return "Base";
         }
     }
 }