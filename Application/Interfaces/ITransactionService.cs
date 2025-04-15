﻿using Application.DTOs;
using Application.Responses;
using Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Task LogAsync(TransactionDto purchaseDto);
        Task<PaginationResponse<TransactionDto>> GetAsync(PaginationParams pageParams);
    }
}
