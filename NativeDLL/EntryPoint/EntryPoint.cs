namespace NativeDLLTemplate.EntryPoint;

internal static class EntryPoint
{
    internal static HINSTANCE HModule { get; private set; }
    /// <summary>
    /// https://learn.microsoft.com/en-us/windows/win32/dlls/dllmain
    /// </summary>
    [UnmanagedCallersOnly(EntryPoint = nameof(DllMain), CallConvs = [typeof(CallConvStdcall)])]
    internal static bool DllMain(HINSTANCE module, NativeDLLCallReason fdwReason, nint lpReserved)
    {
        if (fdwReason != DLL_PROCESS_ATTACH) 
            return true;
        
        DisableThreadLibraryCalls(module);
        CreateThread(null, default, MainThread, (nint)module, CREATE_THREAD_FLAGS.RUN_IMMEDIATELY, out _);

        return true;
    }
    
    private static uint MainThread(nint hModule)
    {
        try
        {
            HModule = hModule;
            Start.DllMain();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
        }
        
        return 1;
    }
}
