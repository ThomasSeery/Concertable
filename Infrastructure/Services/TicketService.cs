﻿using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Exceptions;
using Core.Responses;

public class TicketService : ITicketService
{
    private readonly ITicketRepository ticketRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserPaymentService userPaymentService;
    private readonly IQrCodeService qrCodeService;
    private readonly IAuthService authService;
    private readonly IManagerService managerService;

    public TicketService(
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork,
        IUserPaymentService userPaymentService,
        IQrCodeService qrCodeService,
        IAuthService authService,
        IManagerService managerService
        )
    {
        this.ticketRepository = ticketRepository;
        this.unitOfWork = unitOfWork;
        this.userPaymentService = userPaymentService;
        this.qrCodeService = qrCodeService;
        this.authService = authService;
        this.managerService = managerService;
    }

    public async Task<TicketPurchaseResponse> PurchaseAsync(string paymentMethodId, int eventId)
    {
        var user = await authService.GetCurrentUserAsync();
        var role = await authService.GetFirstUserRoleAsync(user);

        if (role != "Customer")
            throw new ForbiddenException("Only Customers can buy tickets");

        var paymentResponse = await userPaymentService.PayVenueManagerByEventIdAsync(eventId, paymentMethodId);

        return new TicketPurchaseResponse
        {
            Success = paymentResponse.Success,
            RequiresAction = paymentResponse.RequiresAction,
            Message = paymentResponse.Message ?? (paymentResponse.Success ? "Payment successful" : "Payment failed"),
            TransactionId = paymentResponse.TransactionId,
            UserEmail = user.Email,
            ClientSecret = paymentResponse.ClientSecret // Ensure this is always sent to the frontend
        };
    }

    /// <summary>
    /// Called by the Webhook controller once the payment process is complete
    /// Adds the Ticket to the Database whilst updating Event Data
    /// </summary>
    public async Task<TicketPurchaseResponse> CompleteAsync(string transactionId, int eventId, int userId, string email)
    {
        var ticketRepository = unitOfWork.GetRepository<Ticket>();
        var eventRepository = unitOfWork.GetRepository<Event>();

        var eventEntity = await eventRepository.GetByIdAsync(eventId);

        using var transaction = await unitOfWork.BeginTransactionAsync();

        Ticket? ticket = null;

        try
        {
            // Create and save the ticket
            ticket = new Ticket
            {
                UserId = userId,
                EventId = eventId,
                PurchaseDate = DateTime.Now
            };

            var ticketResponse = await ticketRepository.AddAsync(ticket);
            await unitOfWork.SaveChangesAsync(); //Have to save here as its the only way to get the Id

            // Adds newly created QR Code to the ticket
            ticket.QrCode = qrCodeService.GenerateFromTicketId(ticketResponse.Id);
            ticketRepository.Update(ticket);

            // Reduce available tickets and save changes
            // Made Sure tickets are available in PurchaseAsync method
            eventEntity.AvailableTickets -= 1;
            eventRepository.Update(eventEntity);

            // Save & Commit Changes if all db operations executed without error
            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            /* 
             * Since we are forced to save midway through unitOfWork,
             * Rollback if the any of the try block fails
             */
            await transaction.RollbackAsync();
            throw new InternalServerException("Failed to Create Ticket. Please contact support");
        }

        if(ticket is null)
            throw new InternalServerException("Ticket created, but not found");

        return new TicketPurchaseResponse
        {
            Success = true,
            Message = "Ticket purchased successfully!",
            TicketId = ticket.Id,
            EventId = eventId,
            PurchaseDate = ticket.PurchaseDate,
            Amount = eventEntity.Price,
            Currency = "GBP",
            TransactionId = transactionId,
            UserEmail = email
        };
    }

    public Task<byte[]> GetQrCodeByIdAsync(int id)
    {
        return ticketRepository.GetQrCodeByIdAsync(id);
    }
}
