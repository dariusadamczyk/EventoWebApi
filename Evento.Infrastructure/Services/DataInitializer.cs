using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using NLog.Fluent;

namespace Evento.InfraStructure.Services
{
    public class DataInitializer : IDataInitializer
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IUserService _userService;
        private IEventService _eventSerice;

        

        public DataInitializer(IUserService userService, IEventService eventService)
        {
            _userService = userService;
            _eventSerice = eventService;
        }

        public async Task SeedAsync()
        {
            logger.Info("Initializing data");
            var tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "user@email.com", "deafultuser", "secret"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "admin@email.com", "admin", "secret", "admin"));
            logger.Info("Created users");

            for (int i = 0; i < 10; i++)
            {
                var eventId = Guid.NewGuid();
                var name = $"Event {i}";
                var description = name;
                var startDate = DateTime.UtcNow.AddHours(i);
                var endDate = startDate.AddHours(3);

                tasks.Add(_eventSerice.CreateAync(eventId,name,description,startDate,endDate));
                tasks.Add(_eventSerice.AddTicketAsync(eventId, 100, 100));
            }

            await Task.WhenAll(tasks);

        }
    }
}
