using ExecuteWindowsCommandSample.ConsoleApp;

Console.Write("How many more minutes should I shutdown the PC?: ");

var sleepMins = int.Parse(Console.ReadLine());
var shutdownTime = DateTime.UtcNow.AddMinutes(sleepMins);

while (DateTime.UtcNow < shutdownTime)
{
    Console.WriteLine(DateTime.UtcNow.ToString());
    Thread.Sleep(1000);
}

var cmd = new CommandExecuter();
var result = cmd.ExecuteCmd("shutdown -s -t 0000");

Console.WriteLine(result);
