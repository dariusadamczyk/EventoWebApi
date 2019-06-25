using System;
using System.Collections.Generic;

namespace Evento.Core.Domain
{
    public class Event: Entity
    {

        private ISet<Ticket> _tickets = new HashSet<Ticket>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public DateTime CreatedAd { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        public IEnumerable<Ticket> Tickets => _tickets;

        protected Event()
        {

        }

        public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate )
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAd = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void AddTicktets(int amount, decimal price)
        {
            var seating = _tickets.Count + 1;
            for (int a = 0; a < amount; a++)
            {
                _tickets.Add(new Ticket(this,seating,price));
                seating++;
            }
        }

        
    }
}
