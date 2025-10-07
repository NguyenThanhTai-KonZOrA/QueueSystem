using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QueueSystem.Implement.EntityModels;
using QueueSystem.Implement.Repositories;
using QueueSystem.Implement.Repositories.Interface;
using QueueSystem.Implement.Services;
using QueueSystem.Implement.UnitOfWork;
using QueueSystem.Implement.ViewModels.Request;
using QueueSystem.Implement.ViewModels.Response;

namespace QueueSystem.Services
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QueueService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IQueueTicketRepository _queueTicketRepository;
        private readonly ICountersRepository _countersRepository;
        private readonly IPatronRepository _patronRepository;
        private readonly ICounterSequenceRepository _counterSequenceRepository;
        private readonly ITicketArchiveRepository _ticketArchiveRepository;

        public QueueService(IQueueRepository repository,
            ILogger<QueueService> logger,
            IConfiguration configuration,
            IQueueTicketRepository queueTicketRepository,
            ICountersRepository countersRepository,
            IPatronRepository patronRepository,
            ICounterSequenceRepository counterSequenceRepository,
            ITicketArchiveRepository ticketArchiveRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
            _queueTicketRepository = queueTicketRepository;
            _countersRepository = countersRepository;
            _patronRepository = patronRepository;
            _counterSequenceRepository = counterSequenceRepository;
            _ticketArchiveRepository = ticketArchiveRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var patron = await _patronRepository.GetUserByPhoneOrEmailAsync(request.Phone, request.Email);

            if (patron == null)
            {
                patron = new Patron
                {
                    FullName = request.FullName,
                    Phone = request.Phone,
                    Email = request.Email,
                    CreatedAt = DateTime.UtcNow
                };
                await _patronRepository.AddAsync(patron);
                await _unitOfWork.CompleteAsync();
            }

            var today = DateTime.UtcNow.Date;

            var sequence = await _counterSequenceRepository.GetCounterSequencesAsync(request.CounterId, today);
            if (sequence == null)
            {
                sequence = new CounterSequence
                {
                    CounterId = request.CounterId,
                    SequenceDate = today,
                    LastNumber = 1
                };
                await _counterSequenceRepository.AddAsync(sequence);
            }
            else
            {
                sequence.LastNumber += 1;
                _counterSequenceRepository.Update(sequence);
            }
            await _unitOfWork.CompleteAsync();

            var ticket = new QueueTicket
            {
                TicketNumber = sequence.LastNumber,
                TicketDate = today,
                CounterId = request.CounterId,
                PatronId = patron.Id,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await _queueTicketRepository.AddAsync(ticket);

            await _unitOfWork.CompleteAsync();
            return new RegisterResponse
            {
                TicketId = ticket.Id,
                UserId = ticket.PatronId,
                CounterId = ticket.CounterId,
                TicketNumber = $"{(await _countersRepository.GetByIdAsync(ticket.CounterId))?.Name ?? $"C{ticket.CounterId}"}{ticket.TicketNumber:D3}",
                TicketDate = ticket.TicketDate.ToString("yyyy-MM-dd"),
                Status = ticket.Status
            };
        }

        public async Task ArchiveTicketsOlderThanAsync(DateTime cutoffDate)
        {
            var oldTickets = await _queueTicketRepository.GetTicketsOlder(cutoffDate);

            if (oldTickets.Count == 0)
                return;

            var archives = oldTickets.Select(t => new TicketArchive
            {
                OriginalTicketId = t.Id,
                TicketNumber = t.TicketNumber,
                TicketDate = t.TicketDate,
                CounterId = t.CounterId,
                PatronId = t.PatronId,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            }).ToList();

            _ticketArchiveRepository.AddRange(archives);
            _queueTicketRepository.RemoveRange(oldTickets);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<TicketInforResponse>> GetAllTicketsAsync()
        {
            var tickets = await _queueTicketRepository.GetAllTicketsWithUserAsync();

            return tickets.Select(t => new TicketInforResponse
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
