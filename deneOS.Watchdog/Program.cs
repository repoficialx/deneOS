using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

BootContext ctx = new BootContext();

// -------------------- ARG PARSE --------------------

foreach (var arg in args)
{
    if (arg.StartsWith("/mode:"))
        ctx.Mode = arg.Substring(6);
    else
        ctx.Flags.Add(arg);
}

// -------------------- START PIPE SERVER --------------------

_ = Task.Run(RunPipeServer);

// -------------------- MODE SWITCH --------------------

switch (ctx.Mode)
{
    case "ShellBoot":
        RunShellBoot(ctx);
        break;

    case "StartupBoot":
        RunStartupBoot(ctx);
        break;

    case "Recovery":
        RunRecovery(ctx);
        break;

    default:
        ShowError();
        return;
}

// -------------------- MODES --------------------

void RunShellBoot(BootContext ctx)
{
    var watchdog = new WatchdogController();

    watchdog.StartMode(WatchdogMode.ShellBoot);

    watchdog.WaitForDeneOSOrLaunch();
    watchdog.StartMonitoring();
}

void RunStartupBoot(BootContext ctx)
{
    Process.Start(@"C:\DENEOS\core\deneOS.exe");
}

void RunRecovery(BootContext ctx)
{
    Process.Start("explorer.exe");
}

void ShowError()
{
    Console.WriteLine("Invalid mode specified.");
}

// -------------------- PIPE SERVER --------------------

async Task RunPipeServer()
{
    const string PipeName = "deneos-heartbeat";

    var server = new NamedPipeServerStream(PipeName, PipeDirection.InOut);

    DateTime lastSeen = DateTime.Now;

    while (true)
    {
        await server.WaitForConnectionAsync();

        using var reader = new StreamReader(server);
        using var writer = new StreamWriter(server) { AutoFlush = true };

        while (server.IsConnected)
        {
            var msg = await reader.ReadLineAsync();

            if (msg != null)
            {
                lastSeen = DateTime.Now;
                await writer.WriteLineAsync("PONG");
            }
        }
    }
}

// -------------------- CONTEXT --------------------

class BootContext
{
    public string Mode { get; set; } = "";
    public HashSet<string> Flags { get; set; } = new();
}

// -------------------- WATCHDOG --------------------

class WatchdogController
{
    private Process? deneOSProcess;
    private DateTime lastHeartbeat = DateTime.Now;

    public void StartMode(WatchdogMode mode)
    {
        // future extension
    }

    public void WaitForDeneOSOrLaunch()
    {
        if (!IsProcessRunning("deneOS"))
            LaunchDeneOS();

        WaitForReadySignal(10000);
    }

    public void StartMonitoring()
    {
        while (true)
        {
            if (!IsProcessRunning("deneOS"))
                LaunchDeneOS();

            if (HasHeartbeatTimedOut())
                RestartDeneOS();

            Thread.Sleep(1000);
        }
    }

    private void LaunchDeneOS()
    {
        deneOSProcess = Process.Start(@"C:\DENEOS\core\deneOS.exe");
    }

    private void WaitForReadySignal(int timeout)
    {
        var start = DateTime.Now;

        while ((DateTime.Now - start).TotalMilliseconds < timeout)
        {
            if (IsDenesOSReady())
                return;

            Thread.Sleep(200);
        }

        LaunchDeneOS();
    }

    private bool IsProcessRunning(string name)
        => Process.GetProcessesByName(name).Length > 0;

    private bool IsDenesOSReady()
        => true;

    private bool HasHeartbeatTimedOut()
    {
        return (DateTime.Now - lastHeartbeat).TotalSeconds > 5;
    }

    private void RestartDeneOS()
    {
        LaunchDeneOS();
    }
}

// -------------------- ENUM --------------------

enum WatchdogMode
{
    ShellBoot,
    StartupBoot,
    Recovery
}