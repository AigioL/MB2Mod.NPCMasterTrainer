using System;

namespace MB2Mod.NPCMasterTrainer
{
    internal sealed class Int32Attribute : Attribute
    {
        public Int32Attribute(int value) => Value = value;

        public int Value { get; }
    }
}