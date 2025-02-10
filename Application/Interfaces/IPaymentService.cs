﻿using Application.DTOs;
using Core.Parameters;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessAsync(PaymentParams paymentParams, TransactionDto transactionDto);
    }
}
