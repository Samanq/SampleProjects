using System.Diagnostics;
using System.Text;

namespace ExecuteWindowsCommandSample.ConsoleApp;

public class CommandExecuter
{
    public string ExecuteCmd(string command)
    {
        StringBuilder result = new();

        Process process = new();
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = @"/C " + command;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = true;
        process.Start();

        while (!process.HasExited)
            result.Append(process.StandardOutput.ReadToEnd());

        return result.ToString();
    }
}
