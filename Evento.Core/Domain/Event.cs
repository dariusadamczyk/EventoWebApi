using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Ticket> PurchasedTickets => Tickets.Where(x=>x.Purchased);

        public IEnumerable<Ticket> AvailableTickets => Tickets.Except(PurchasedTickets);

        protected Event()
        {

        }

        public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate )
        {
            Id = id;
            SetName(name);
            SetDescription(description);
            SetDates(startDate, endDate);
            CreatedAd = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            if (startDate>=endDate)
            {
                throw new Exception($"Event with id {Id} must have a end date grater then start date");
            }

            StartDate = startDate;
            EndDate = endDate;

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

        public void SetName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"Event with id '{Id}' can not have emtpy name ");
            }

            Name = name;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            if (String.IsNullOrWhiteSpace(description))
            {
                throw new Exception($"Event with id '{Id}' can not have emtpy description ");
            }

            Description = description;
            UpdateAt = DateTime.UtcNow;
        }

        public void PurchaseTickets(User user, int amount)
        {
            if (AvailableTickets.Count() < amount)
            {
                throw new Exception($"Not enough available tickets to purchase by user");
            }

            var tickets = AvailableTickets.Take(amount);
            foreach (var ticket in tickets)
            {
                ticket.Purchase(user);
            }
        }

        public void CancelTickets(User user, int amount)
        {

            var tickets = GetTicketsPurchesByUser(user);
            if (tickets.Count() < amount)
            {
                throw new Exception($"Not enough tickets to be cancel by user");
            }

            
            foreach (var ticket in tickets.Take(amount))
            {
                ticket.Cancel();
            }
        }

        public IEnumerable<Ticket> GetTicketsPurchesByUser(User user)
         => PurchasedTickets.Where(x => x.UserId == user.Id);
        
    }
}
