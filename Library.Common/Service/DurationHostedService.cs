using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Libary.Common.Service
{
    /// <summary>
    /// Kullanımı için startup.cs ye services.AddHostedService<Deneme>(); eklenmesi gerekiyor
    /// </summary>
    public abstract class DurationHostedService : IHostedService, IDisposable
    {
        public abstract int DurationSecond { get; }

        bool isServiceRunning;
        void cycl(object state)
        {
            if (isServiceRunning)
                return;
            else
            {
                isServiceRunning = true;
                Cycl();
                isServiceRunning = false;
            }

        }
        public abstract void Cycl();

        Timer timer;
        void SetTime()
        {
            timer = new Timer(cycl, null, TimeSpan.Zero, TimeSpan.FromSeconds(DurationSecond));
        }
        public void Dispose()
        {
            timer?.Dispose();
        }
        public abstract void Stop(CancellationToken cancellationToken);
        /// <summary>
        /// Servis ilk çalışmasında buraya girer tek seferlik. 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract void Start(CancellationToken cancellationToken);
        public Task StartAsync(CancellationToken cancellationToken)
        {
            SetTime();
            Start(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Stop(cancellationToken);
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
 
}
