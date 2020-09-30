using System.Reflection;
using System.Runtime.CompilerServices;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class Hook
        {
            public static void ReplaceMethod(MethodInfo source, MethodInfo destination)
            {
                if (source == destination) return;

                RuntimeHelpers.PrepareMethod(source.MethodHandle);
                RuntimeHelpers.PrepareMethod(destination.MethodHandle);

                // Only 64 bit

                if (source.IsVirtual)
                {
                    unsafe
                    {
                        var methodDesc = (ulong*)source.MethodHandle.Value.ToPointer();
                        int index = (int)(((*methodDesc) >> 32) & 0xFF);
                        //if (IntPtr.Size == 4)
                        //{
                        //    var classStart = (uint*)source.DeclaringType.TypeHandle.Value.ToPointer();
                        //    classStart += 10;
                        //    classStart = (uint*)*classStart;
                        //    var tar = classStart + index;
                        //    var inj = (uint*)destination.MethodHandle.Value.ToPointer() + 2;
                        //    *tar = *inj;
                        //}
                        //else
                        //{
                        var classStart = (ulong*)source.DeclaringType.TypeHandle.Value.ToPointer();
                        classStart += 8;
                        classStart = (ulong*)*classStart;
                        var tar = classStart + index;
                        var inj = (ulong*)destination.MethodHandle.Value.ToPointer() + 1;
                        *tar = *inj;
                        //}
                    }
                    return;
                }

                unsafe
                {
                    //                    if (IntPtr.Size == 4)
                    //                    {
                    //                        var inj = (int*)destination.MethodHandle.Value.ToPointer() + 2;
                    //                        var tar = (int*)source.MethodHandle.Value.ToPointer() + 2;
                    //#if DEBUG
                    //                        var injInst = (byte*)*inj;
                    //                        var tarInst = (byte*)*tar;
                    //                        var injSrc = (int*)(injInst + 1);
                    //                        var tarSrc = (int*)(tarInst + 1);
                    //                        *tarSrc = ((int)injInst + 5) + *injSrc - ((int)tarInst + 5);
                    //#else
                    //                        *tar = *inj;
                    //#endif
                    //                    }
                    //                    else
                    //                    {
                    var inj = (long*)destination.MethodHandle.Value.ToPointer() + 1;
                    var tar = (long*)source.MethodHandle.Value.ToPointer() + 1;
                    *tar = *inj;
                    //}
                }
            }
        }
    }
}