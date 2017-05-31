using Quartz;
using Quartz.Impl;

namespace E_TRADING.Jobs
{
    public class AuctionUpdaterSheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var job = JobBuilder.Create<AuctionUpdater>().Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("Auction", "Auctions")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}