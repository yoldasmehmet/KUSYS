using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Libary.Common.Service
{    /// <summary>
     /// Kullanımı için startup.cs ye services.AddHostedService<Deneme>(); eklenmesi gerekiyor
     /// </summary>
    public abstract class ForcedTimeHostedService : IHostedService, IDisposable
    {
        public abstract int Hour { get; }
        public abstract int Minute { get; }


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
            var ticks = Math.Abs(DateTime.Now.Ticks - DateTime.Today.AddHours(Hour).AddMinutes(Minute).Ticks);
            TimeSpan delay = TimeSpan.FromTicks(ticks);
            timer = new Timer(cycl, null, delay, TimeSpan.FromMinutes(60 * 24));
        }
        public void Dispose()
        {
            timer?.Dispose();
        }
        public abstract Task Stop(CancellationToken cancellationToken);
        /// <summary>
        /// Servis ilk çalışmasında buraya girer tek seferlik.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task Start(CancellationToken cancellationToken);
        public Task StartAsync(CancellationToken cancellationToken)
        {
            SetTime();
            return Start(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Stop(cancellationToken);
        }
    }
}
