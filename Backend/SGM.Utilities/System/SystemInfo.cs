using System;
using System.Linq;
using System.Diagnostics;
using System.Management;
using System.Threading;

namespace Orion.Utilities.System {
    /// <summary>
    /// This class encapsulates static methods that retrieve system information.
    /// </summary>
    public static class SystemInfo {
        /// <summary>
        /// This method retrieves the current CPU and RAM usage.
        /// </summary>
        /// <returns>An anonymous object containing the current CPU and RAM usage.</returns>
        public static object GetSystemStats() {
            // Getting CPU usage
            PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpu.NextValue();
            Thread.Sleep(1000);
            var cpuUsage = (double)cpu.NextValue(); // cpu usage in %

            // Getting RAM usage
            PerformanceCounter ram = new PerformanceCounter("Memory", "Available MBytes");
            var physicalRam = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory") // installed memory
                                  .Get().Cast<ManagementObject>()
                                  .Sum(x => Convert.ToInt64(x.Properties["Capacity"].Value)) / (1024 * 1024);
            var ramCurrent = (double)ram.NextValue(); // used memory
            var ramUsage = (1 - ramCurrent / physicalRam) * 100; // total memory

            return new {
                CPU = $"{cpuUsage:F}%",
                RAM = $"{ramUsage:F}%"
            };
        }
    }
}
