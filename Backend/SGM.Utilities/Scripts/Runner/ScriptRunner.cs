using System;
using System.Diagnostics;
using System.IO;

namespace Orion.Utilities.Scripts.Runner {
    /// <summary>
    /// Consolidates static methods that run scripts.
    /// Note: In order to run a script, the necessary dependencies must be installed on the machine executing this code.
    /// For example, to run a Powershell script, make sure the machine running this code has Powershell installed and the user running the application has the necessary permissions to run Powershell scripts.
    /// </summary>
    public static class ScriptRunner {
        /// <summary>
        /// Runs a script.
        /// </summary>
        /// <param name="scriptFile">The full file name, including physical path, of the script to run.</param>
        /// <param name="executor">The full physical path of the executable that runs the script (For Powershell scripts, typically just "powershell" will suffice).</param>
        /// <param name="args">A list of arguments the script takes in.</param>
        /// <returns>A string containing the results from the script execution.</returns>
        public static string Run(string scriptFile, string executor, string[] args = null) {
            if (string.IsNullOrWhiteSpace(scriptFile))
                throw new Exception($"Script file not specified.");
            
            if (string.IsNullOrWhiteSpace(executor))
                throw new Exception($"Executor not specified.");

            string result = string.Empty;

            try {
                var info = new ProcessStartInfo();
                info.FileName = executor;
                info.WorkingDirectory = Path.GetDirectoryName(executor);
                info.Arguments = scriptFile + " " + (args != null ? string.Join(' ', args) : string.Empty);
                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;

                using (var proc = new Process()) {
                    proc.StartInfo = info;
                    proc.Start();
                    result = proc.StandardOutput.ReadToEnd();
                }

                return result;
            }
            catch (Exception e) {
                throw new Exception($"Script failed to run. \n{result} \n{e.Message + " " + (e.InnerException != null ? e.InnerException.Message : string.Empty)}");
            }
        }
    }
}
