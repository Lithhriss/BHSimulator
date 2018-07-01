using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

public class RngLogger<T> : IDisposable {

    private  readonly FileStream _fileStream;
    private  readonly StreamWriter _writer;
    private  readonly ConcurrentQueue<T> _buffer = new ConcurrentQueue<T>();
    private  readonly Timer _timer;

    public RngLogger()
    {
        _fileStream = new FileStream("C:/Users/Cesar Jaimes/Documents/GitHub/BHSimulator/RngLogger.txt", FileMode.Append, FileAccess.Write, FileShare.Read);
        _writer = new StreamWriter(_fileStream);
        var flushPeriod = TimeSpan.FromSeconds(5);
        _timer = new Timer(FlushBuffer, null, flushPeriod, flushPeriod);
    }

    public void WriteLine(T result)
    {
        _buffer.Enqueue(result);
    }

    public void Dispose()
    {
        _timer.Dispose();
        FlushBuffer(); // flush anything left over in the buffer
        _writer.Dispose();
        _fileStream.Dispose();
    }

    private void FlushBuffer(object unused = null)
    {
        T current;
        while (_buffer.TryDequeue(out current))
        {
            _writer.WriteLine(current);
        }
        _writer.Flush();
    }
}
