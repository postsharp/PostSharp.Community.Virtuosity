using System.Collections.Generic;
using System.Reflection;
using PostSharp.Reflection;
using PostSharp.Sdk.CodeModel;
using PostSharp.Sdk.CodeWeaver;
using PostSharp.Sdk.Extensibility;
using PostSharp.Sdk.Extensibility.Tasks;

namespace PostSharp.Community.Virtuosity.Weaver
{
    [ExportTask(Phase = TaskPhase.Transform, TaskName = nameof(VirtuosityTask))]
    public class VirtuosityTask : Task
    {
        [ImportService]
        private IAnnotationRepositoryService annotationService;

        // TODO add in-build copyright notice

        public override bool Execute()
        {
            List<MethodDefDeclaration> alteredMethods = FindAndProcessMethodsToVirtualize();
            ConvertCallToCallvirt(alteredMethods);
            ConvertNewToOverride(alteredMethods);
            return true;
        }

        private void ConvertNewToOverride(List<MethodDefDeclaration> alteredMethods)
        {
            foreach (MethodDefDeclaration methodDefDeclaration in alteredMethods)
            {
                if (methodDefDeclaration.IsAbstract ||
                    !methodDefDeclaration.HasBody ||
                    !methodDefDeclaration.IsNew)
                {
                    // Nothing to be done.
                    continue;
                }
                foreach (MethodDefDeclaration baseMethod in alteredMethods)
                {
                    if (methodDefDeclaration.DeclaringType.BaseTypeDef != null && methodDefDeclaration.DeclaringType.BaseType == baseMethod.DeclaringType)
                    {
                        if (((IMethodSignature)methodDefDeclaration).DefinitionMatchesReference(baseMethod))
                        {
                            // No longer new
                            methodDefDeclaration.Attributes &= ~MethodAttributes.NewSlot;
                            break;
                        }
                    }
                }
            }
        }

        private void ConvertCallToCallvirt(List<MethodDefDeclaration> alteredMethods)
        {
            using Sdk.CodeWeaver.Weaver weaver = new Sdk.CodeWeaver.Weaver(this.Project);
            weaver.AddMethodLevelAdvice(new CallToCallvirtWeaverAdvice(), null, JoinPointKinds.InsteadOfCall, alteredMethods);
            weaver.Weave();
        }

        private List<MethodDefDeclaration> FindAndProcessMethodsToVirtualize()
        {
            List<MethodDefDeclaration> alteredMethods = new List<MethodDefDeclaration>();
            var tor = annotationService.GetAnnotationsOfType(typeof(VirtualAttribute), false, false);
            while (tor.MoveNext())
            {
                var possibleTarget = tor.Current.TargetElement;
                if (possibleTarget is MethodDefDeclaration method)
                {
                    if (ProcessMethod(method))
                    {
                        alteredMethods.Add(method);
                    }
                }
            }

            return alteredMethods;
        }

        private bool ProcessMethod(MethodDefDeclaration method)
        {
            if (method.IsSealed && method.IsVirtual && !method.DeclaringType.IsSealed)
            {
                method.Attributes =
                    method.Attributes & ~MethodAttributes.Final;
            }
            else if (method.Name == ".ctor" ||
                     method.IsSealed ||
                     method.IsVirtual ||
                     method.IsStatic ||
                     method.DeclaringType.IsSealed ||
                     method.Visibility == Visibility.Private ||
                     MethodIsSerializationCallback(method))
            {
                return false;
            }
            else
            {
                method.Attributes |= MethodAttributes.Virtual;
                method.Attributes |= MethodAttributes.NewSlot;
            }
            
            // Further processing
            return true;
        }

        private bool MethodIsSerializationCallback(MethodDefDeclaration method)
        {
            return MethodContainsSerializationAttribute(method, "OnSerializingAttribute")
                || MethodContainsSerializationAttribute(method, "OnSerializedAttribute")
                || MethodContainsSerializationAttribute(method, "OnDeserializingAttribute")
                || MethodContainsSerializationAttribute(method, "OnDeserializedAttribute");
        }

        private bool MethodContainsSerializationAttribute(MethodDefDeclaration method, string simpleName)
        {
            return method.CustomAttributes.GetOneByType("System.Runtime.Serialization." + simpleName)
                   != null;
        }
    }
}