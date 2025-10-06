using QueueSystem.Implement.Repositories;
using QueueSystem.Implement.Services;
using QueueSystem.Implement.ViewModels;

namespace QueueSystem.Services
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _repository;

        public QueueService(IQueueRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterResultDto> RegisterAsync(RegisterRequestDto request)
        {
            var ticket = await _repository.RegisterTicketTransactionAsync(
                request.FullName,
                request.Phone,
                request.Email,
                request.CounterId
            );

            var counterName = ticket.Counter?.Name ?? $"C{ticket.CounterId}";
            var formattedNumber = $"{counterName}{ticket.TicketNumber:D3}";

            return new RegisterResultDto
            {
                TicketId = ticket.Id,
                UserId = ticket.PatronId,
                CounterId = ticket.CounterId,
                TicketNumber = formattedNumber,
                TicketDate = ticket.TicketDate.ToString("yyyy-MM-dd"),
                Status = ticket.Status
            };
        }

        public async Task<List<TicketInfoDto>> GetAllTicketsAsync()
        {
            var tickets = await _repository.GetAllTicketsWithUserAsync();

            return tickets.Select(t => new TicketInfoDto
            {
                TicketId = t.Id,
                TicketNumber = $"{t.Counter?.Name ?? "C"}{t.TicketNumber:D3}",
                FullName = t.Patron?.FullName ?? "",
                Phone = t.Patron?.Phone,
                Email = t.Patron?.Email,
                CounterName = t.Counter?.Name ?? "",
                TicketDate = t.TicketDate,
                Status = t.Status
            }).ToList();
        }
    }
}
