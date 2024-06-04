using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class DataLogger
    {
        private static readonly Lazy<DataLogger> _instance = new Lazy<DataLogger>(() => new DataLogger("../../../../Data/logFile.json"));
        private ConcurrentQueue<BallLogEntry> _logQueue; //Bufor
        private string _logFilePath;
        private Thread _logThread;
        private bool _isRunning;
        private AutoResetEvent _logEvent;
        private const int MaxSize = 10000;




        private DataLogger(string logFileName)
        {
            _logEvent = new AutoResetEvent(false);
            _logFilePath = logFileName;
            _logQueue = new ConcurrentQueue<BallLogEntry>();
            Debug.WriteLine($"Powstał obiekt loggera i będzie pisał do {_logFilePath}");

            _isRunning = true;
            _logThread = new Thread(ProcessQueue);

            _logThread.Start();

        }

        private void ProcessQueue()
        {
            while (_isRunning)
            {
                _logEvent.WaitOne();
                while (_logQueue.TryDequeue(out var logEntry))
                {
                    string jsonString = JsonSerializer.Serialize(logEntry);
                    //Debug.WriteLine($"Logger: zapisałem do pliku {_logFilePath} treść : {jsonString}");

                    File.AppendAllText(_logFilePath, jsonString + Environment.NewLine);
                }
            }
        }

        public static DataLogger Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public void Log(Vector2 position, Vector2 speed, double radius)
        {
            _isRunning = true;
            if (_logQueue.Count < MaxSize)
            {
                BallLogEntry logEntry = new BallLogEntry(position, speed, radius, DateTime.UtcNow);
                _logQueue.Enqueue(logEntry);
                _logEvent.Set();
            }
            else
            {
                //Jeżeli bufor jest pusty to odrzucamy zdarzenie
                Debug.WriteLine("Queue is full");
            }
        }
        public void Stop()
        {
            _isRunning = false;
            _logEvent.Set();
            _logThread.Join();
        }


        internal class BallLogEntry
        {
            public PositionOfBall positionOfBall { get; set; }
            public SpeedOfBall speedOfBall { get; set; }
            public double Radius { get; set; }
            public System.DateTime Time { get; set; }
            internal BallLogEntry(Vector2 position, Vector2 speed, double radius, System.DateTime time)
            {
                positionOfBall = new PositionOfBall(position.X, position.Y);
                speedOfBall = new SpeedOfBall(speed.X, speed.Y);
                Radius = radius;
                Time = time;
            }
        }
    }
    public record PositionOfBall(float X, float Y);
    public record SpeedOfBall(float X, float Y);
}
