namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        private const bool IS_DEBUG_CONST =
#if DEBUG
        true
#else
         false
#endif
            ;

        public static bool IsDevelopment => IS_DEBUG_CONST;
    }
}