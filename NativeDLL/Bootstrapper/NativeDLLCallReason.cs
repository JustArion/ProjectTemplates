namespace NativeDLLTemplate;

public enum NativeDLLCallReason
{
    DLL_PROCESS_DETACH = 0,
    DLL_PROCESS_ATTACH = 1,
    DLL_THREAD_ATTACH = 2,
    DLL_THREAD_DETACH = 3
}