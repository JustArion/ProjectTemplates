namespace NativeDLLTemplate;

internal static class EntryPoint
{
    private static bool _initialized;
    
    [ModuleInitializer]
    [Obsolete("This function can only be called by the runtime!", true)]
    internal static void Initialize()
    {
        HModule = GetModuleHandle();
        DisableThreadLibraryCalls(HModule);
        CreateThread(null, default, MainThread, nint.Zero, CREATE_THREAD_FLAGS.RUN_IMMEDIATELY, out _);
    }

    internal static SafeHINSTANCE HModule { get; private set; } = null!;
    /// <summary>
    /// https://learn.microsoft.com/en-us/windows/win32/dlls/dllmain
    /// </summary>
    [UnmanagedCallersOnly(EntryPoint = nameof(DllMain), CallConvs = [typeof(CallConvStdcall)])]
    internal static bool DllMain(HINSTANCE module, NativeDLLCallReason fdwReason, nint lpReserved) => true;
    
    private static uint MainThread(nint _)
    {
        try
        {
            Task.Run(Start.DllMain);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
        }

        return 0;
    }
}
