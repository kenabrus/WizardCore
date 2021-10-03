using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Dto;

namespace Infrastructure.Services
{
    public interface IWizardService
    {
        Task<MessageDto> GetAsync(int id);
        IEnumerable<MessageDto> GetAllAsync();
        int Add(MessageDto messageDto);
    }
}