﻿using Application.DTOs;
using Application.Interfaces;
using Core.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly IArtistService artistService;
        private readonly IPurchaseService purchaseService;
        private readonly IEmailService emailService;
        
        private readonly string webhookSecret;

        public WebhookController(
            ITicketService ticketService,
            IArtistService artistService,
            IPurchaseService purchaseService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            this.ticketService = ticketService;
            this.artistService = artistService;
            this.purchaseService = purchaseService;
            this.emailService = emailService;
            webhookSecret = configuration["Stripe:WebhookSecret"];
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], webhookSecret);

            if (stripeEvent.Data.Object is not PaymentIntent intent)
                return BadRequest("Invalid event data");

            switch (intent.Status)
            {
                case "succeeded":
                    var paymentType = intent.Metadata["type"];
                    var purchaseDto = new PurchaseDto
                    {
                        FromUserId = int.Parse(intent.Metadata["fromUserId"]),
                        ToUserId = int.Parse(intent.Metadata["toUserId"]),
                        TransactionId = intent.Id,
                        Amount = intent.AmountReceived,
                        Type = paymentType,
                        Status = intent.Status,
                        CreatedAt = DateTime.Now
                    };
                    bool processed = false;

                    var userId = int.Parse(intent.Metadata["fromUserId"]);
                    var email = intent.Metadata["fromUserEmail"];

                    TicketPurchaseResponse? ticketResponse = null;

                    if (paymentType == "event")
                    {
                        ticketResponse = await ticketService.CompleteAsync(intent.Id, int.Parse(intent.Metadata["eventId"]), userId, email);
                        processed = true;
                    }
                    else if (paymentType == "artist")
                    {
                        processed = true;
                    }
                    if (processed)
                    {
                        await purchaseService.LogAsync(purchaseDto);
                        await emailService.SendTicketEmailAsync(userId, email, ticketResponse.TicketId);
                    }

                    return Ok();
            }
            return BadRequest();
        }
    }
}
