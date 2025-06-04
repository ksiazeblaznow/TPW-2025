using Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Logic
{
    public class CollisionLogger
    {
        private static readonly Lazy<CollisionLogger> _instance = new(() => new CollisionLogger());
        public static CollisionLogger Instance => _instance.Value;

        private readonly ConcurrentQueue<CollisionEvent> _eventQueue = new();
        private readonly string _logFilePath;
        private readonly CancellationTokenSource _cts = new();
        private readonly object _fileLock = new();

        private CollisionLogger()
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(logDir);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            _logFilePath = Path.Combine(logDir, $"collision_log_{timestamp}.json");

            Task.Run(() => ProcessQueueAsync(_cts.Token));
        }

        public void LogCollision(Ball a, Ball b)
        {
            _eventQueue.Enqueue(new CollisionEvent(a, b));
        }

        private async Task ProcessQueueAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), token);

                var eventsToWrite = new List<CollisionEvent>();
                while (_eventQueue.TryDequeue(out var evt))
                    eventsToWrite.Add(evt);

                if (eventsToWrite.Count > 0)
                {
                    lock (_fileLock)
                    {
                        using FileStream stream = new(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.None);
                        foreach (var ev in eventsToWrite)
                        {
                            string json = JsonSerializer.Serialize(ev);
                            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json + Environment.NewLine);
                            stream.Write(jsonBytes, 0, jsonBytes.Length);
                        }
                    }
                }
            }
        }

        public void Stop() => _cts.Cancel();
    }
}
