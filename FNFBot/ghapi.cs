using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FNFBot
{
class ghapi { [Flags]
        public enum ProcessAccessFlags: uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
        ProcessAccessFlags processAccess, bool bInheritHandle, int processId);
        [StructLayout(LayoutKind.Sequential)]
   
        public struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szExeFile;
        };
   
        [StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
   
        public struct MODULEENTRY32 {
            internal uint dwSize;
            internal uint th32ModuleID;
            internal uint th32ProcessID;
            internal uint GlblcntUsage;
            internal uint ProccntUsage;
            internal IntPtr modBaseAddr;
            internal uint modBaseSize;
            internal IntPtr hModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            internal string szModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string szExePath;
        }
   
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
        IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
   
        public static extern bool WriteProcessMemory(
        IntPtr hProcess, IntPtr lpBaseAddress, [MarshalAs(UnmanagedType.AsAny)] object lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll")]
        static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);
   
        [DllImport("kernel32.dll")]
        static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
   
        [DllImport("kernel32.dll")]
        static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
   
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);

        [Flags]
        private enum SnapshotFlags: uint {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, int th32ProcessID);

        public static IntPtr GetModuleBaseAddress(Process proc, string modName)
        {
            IntPtr addr = IntPtr.Zero;

            foreach(ProcessModule m in proc.Modules)
            {
                if (m.ModuleName == modName)
                {
                    addr = m.BaseAddress;
                    break;;
                }
            }
            return addr;
        }

        const int INVALID_HANDLE_VALUE = -1;
   
        public static IntPtr GetModuleBaseAddress(int procId, string modName)
        {
            IntPtr modBaseAddr = IntPtr.Zero;
       
            IntPtr hSnap = CreateToolhelp32Snapshot(SnapshotFlags.Module | SnapshotFlags.Module32, procId);

            if (hSnap.ToInt64() != INVALID_HANDLE_VALUE)
            {
                MODULEENTRY32 modEntry = new MODULEENTRY32();
                modEntry.dwSize = (uint) Marshal.SizeOf(typeof(MODULEENTRY32));

                if (Module32First(hSnap, ref modEntry))
                {
                    do
                    {
                        if (modEntry.szModule.Equals(modName))
                        {
                            modBaseAddr = modEntry.modBaseAddr;
                            break;
                        }
                    } while ( Module32Next ( hSnap , ref modEntry ));
                }
            }
       
            CloseHandle(hSnap);
            return modBaseAddr;
        }

        public static IntPtr FindDMAAddy(IntPtr hProc, IntPtr ptr, int[] offsets)
        {
            var buffer = new byte[IntPtr.Size];

            foreach(int i in offsets)
            {
                ReadProcessMemory(hProc, ptr, buffer, buffer.Length, out
                var read);
                ptr = (IntPtr.Size == 4) ? IntPtr.Add(new IntPtr(BitConverter.ToInt32(buffer, 0)), i) : ptr = IntPtr.Add(new IntPtr(BitConverter.ToInt64(buffer, 0)), i);
            }
            return ptr;
        }

    }
}
