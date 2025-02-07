﻿using AutoMapper;
using Core.Entities;
using Application.Interfaces;
using Core.Parameters;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Event>> GetUpcomingByVenueIdAsync(int id)
        {
            return await eventRepository.GetUpcomingByVenueIdAsync(id);
        }

        public async Task<IEnumerable<EventHeaderDto>> GetHeadersAsync(SearchParams searchParams)
        {
            var headers = await eventRepository.GetHeadersAsync(searchParams);
            return mapper.Map<IEnumerable<EventHeaderDto>>(headers);
        }
    }
}
