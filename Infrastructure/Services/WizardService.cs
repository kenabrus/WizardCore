using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Entities;
using Infrastructure.Dto;
using Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class WizardService : IWizardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        

        public WizardService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<MessageDto> GetAsync(int id)
        {
            var message = await _context.Messages.SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Message, MessageDto>(message);

        }

        public IEnumerable<MessageDto> GetAllAsync()
            {
                var messages = _context.Messages.ToList();
                return _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages); 
            }

        public int Add(MessageDto m)
        {
            Console.WriteLine($" - AddAsync m {m.FirstName} {m.LastName} {m.EmailTo} {m.EmailCc} {m.TopicId} {m.MessageText}");
            var message = _mapper.Map<MessageDto, Message>(m);
            EmailConfigSettings _emailConfigSettings = _configuration.GetSection("EmailConfig").Get<EmailConfigSettings>();
            //var EmailCc = _configuration.GetSection("EmailConfig:smtpMessageTo").Value;

            message.EmailCc = _emailConfigSettings.SmtpMessageTo;
            message.SendDateTime = DateTime.UtcNow;
            //Console.WriteLine($" - AddAsync m2 {m2.FirstName} {m2.LastName} {m2.Email} {m2.TopicId} {m2.MessageText}");
            _context.Messages.Add(message);
           _context.SaveChanges();
            int createdId = message.Id;
            return createdId;
        }


    }
}
