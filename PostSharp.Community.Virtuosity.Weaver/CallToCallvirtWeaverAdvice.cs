using PostSharp.Sdk.CodeModel;
using PostSharp.Sdk.CodeWeaver;

namespace PostSharp.Community.Virtuosity.Weaver
{
    internal class CallToCallvirtWeaverAdvice : IAdvice
    {
        public bool RequiresWeave(WeavingContext context)
        {
            return context.InstructionReader.CurrentInstruction.OpCodeNumber == OpCodeNumber.Call;
        }

        public void Weave(WeavingContext context, InstructionBlock block)
        {       
            IMethod oldOperand = context.InstructionReader.CurrentInstruction.MethodOperand;
            InstructionSequence sequence = block.MethodBody.CreateInstructionSequence();
            block.AddInstructionSequence( sequence );
            context.InstructionWriter.AttachInstructionSequence( sequence );
            context.InstructionWriter.EmitInstructionMethod(OpCodeNumber.Callvirt, oldOperand);
            context.InstructionWriter.DetachInstructionSequence();
        }

        public int Priority => 0;
    }
}