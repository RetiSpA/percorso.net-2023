namespace BackgroundTasks.Tasks
{
    public class PeriodicHostedService : BackgroundService
    {
        private readonly ILogger<PeriodicHostedService> _logger;

        private int _executionCount = 0;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(5);

        public PeriodicHostedService(ILogger<PeriodicHostedService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWorkAsync();
            }
        }

        private async Task DoWorkAsync()
        {
            var count = Interlocked.Increment(ref _executionCount);
            _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);

            // Do something...
        }
    }
}
