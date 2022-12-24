//using OrderManagement.Logic;

//namespace OrderManagement.Api.BackgroundServices
//{
   // public class QueuedUpdateService : BackgroundService
    //{
        //private readonly IOrderManagementLogic logic;
        //private readonly ILogger<QueuedUpdateService> logger;
        //private readonly UpdateChannel updateChannel;
        
        //public QueuedUpdateService(IOrderManagementLogic logic,
        //    ILogger<QueuedUpdateService> logger,
        //    UpdateChannel updateChannel)
        //{
        //    this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        //    this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //    this.updateChannel = updateChannel ?? throw new ArgumentNullException(nameof(updateChannel)); 
        //}

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    //logger.LogInformation("Service started");
        //    //while (!stoppingToken.IsCancellationRequested)
        //    //{
        //    //    await Task.Delay(1000, stoppingToken);
        //    //    Console.WriteLine("do something");
        //    //}
        //    //await Task.CompletedTask;
        //    await foreach (var customerId in updateChannel.ReadAllAsync(stoppingToken))
        //    {
        //        await logic.UpdateTotalRevenue(customerId);
        //        logger.LogInformation($"revenue updated for customer: {customerId}");
        //    }
        //}
    //}
//}
