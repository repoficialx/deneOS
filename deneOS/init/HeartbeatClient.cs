using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

public class HeartbeatClient
{
    private const string PipeName = "deneos-heartbeat";

    private NamedPipeClientStream? pipe;
    private StreamReader? reader;
    private StreamWriter? writer;

    public bool Connected => pipe?.IsConnected == true;

    public async Task<bool> ConnectAsync()
    {
        try
        {
            pipe = new NamedPipeClientStream(
                ".",
                PipeName,
                PipeDirection.InOut,
                PipeOptions.Asynchronous);

            await pipe.ConnectAsync(2000);

            reader = new StreamReader(pipe);
            writer = new StreamWriter(pipe)
            {
                AutoFlush = true
            };

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SendHeartbeatAsync()
    {
        try
        {
            if (!Connected || writer == null || reader == null)
                return false;

            await writer.WriteLineAsync("PING");

            var response = await reader.ReadLineAsync();

            return response == "PONG";
        }
        catch
        {
            return false;
        }
    }

    public void Disconnect()
    {
        reader?.Dispose();
        writer?.Dispose();
        pipe?.Dispose();
    }
}