using System;
using System.Runtime.InteropServices;

namespace LauncherExtension
{
    public static class LauncherHelper
    {
        private const int SHOWNORMAL = 1;
        private const int SHOWMINIMIZED = 2;
        private const int SHOWMAXIMIZED = 3;
        private const int SHOWRESTORE = 9;

        private const uint LSFWLOCK = 1;
        private const uint LSFWUNLOCK = 2;

        private const int ASFWANY = -1;

        private const uint SPIGETFOREGROUNDLOCKTIMEOUT = 0x2000;
        private const uint SPISETFOREGROUNDLOCKTIMEOUT = 0x2001;

        public static void BringApplicationToFront(IntPtr window)
        {
            uint currentThread = GetCurrentThreadId();

            IntPtr activeWindow = GetForegroundWindow();
            uint activeProcess;
            uint activeThread = GetWindowThreadProcessId(activeWindow, out activeProcess);

            uint windowProcess;
            uint windowThread = GetWindowThreadProcessId(window, out windowProcess);

            if (currentThread != activeThread)
            {
                AttachThreadInput(currentThread, activeThread, true);
            }

            if (windowThread != currentThread)
            {
                AttachThreadInput(windowThread, currentThread, true);
            }

            uint oldTimeout = 0, newTimeout = 0;
            SystemParametersInfoGet(SPIGETFOREGROUNDLOCKTIMEOUT, 0, ref oldTimeout, 0);
            SystemParametersInfoSet(SPISETFOREGROUNDLOCKTIMEOUT, 0, newTimeout, 0);
            LockSetForegroundWindow(LSFWUNLOCK);
            AllowSetForegroundWindow(ASFWANY);

            SetForegroundWindow(window);

            if (!IsIconic(window))
            {
                ShowWindow(window, SHOWRESTORE);
            }
            else
            {
                ShowWindow(window, SHOWRESTORE);
            }

            SystemParametersInfoSet(SPISETFOREGROUNDLOCKTIMEOUT, 0, oldTimeout, 0);

            if (currentThread != activeThread)
            {
                AttachThreadInput(currentThread, activeThread, false);
            }

            if (windowThread != currentThread)
            {
                AttachThreadInput(windowThread, currentThread, false);
            }
        }

        [DllImport("user32.dll")]
        internal static extern bool IsIconic(IntPtr handle);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LockSetForegroundWindow(uint lockCode);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        internal static extern bool SystemParametersInfoGet(uint action, uint param, ref uint vparam, uint init);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        internal static extern bool SystemParametersInfoSet(uint action, uint param, uint vparam, uint init);

        [DllImport("user32.dll")]
        internal static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool attach);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr windowHandle, out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId(IntPtr windowHandle, IntPtr ProcessId);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("kernel32.dll")]
        internal static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr windowHandle);

        [DllImport("user32.dll")]
        internal static extern bool AllowSetForegroundWindow(int processId);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr windowHandle, int cmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetActiveWindow(IntPtr windowHandle);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetFocus(IntPtr windowHandle);
    }
}
