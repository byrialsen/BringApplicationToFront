using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("Command mdarguments:");
            foreach(string arg in args)
            {
                System.Diagnostics.Debug.WriteLine(arg);
            }
            
            if (args.Length > 0)
            {
                string app = args[args.Length - 1].TrimStart(new char[] { '/', '-' });

                IntPtr? id = Process.GetProcesses().FirstOrDefault(x => x.ProcessName == app)?.MainWindowHandle;
                if (id.HasValue && id.Value != IntPtr.Zero)
                {
                    System.Diagnostics.Debug.WriteLine($"BringIntoFocus -> {app}/{id.Value}");
                    LauncherHelper.BringApplicationToFront(id.Value);
                }
            }
        }
    }
}
