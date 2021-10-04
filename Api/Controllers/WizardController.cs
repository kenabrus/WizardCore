using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Infrastructure.Mappers;
using Infrastructure.Data;
using Infrastructure.Dto;
using Infrastructure.Services;
using Api.Models;
using Core.Entities;


namespace Api.Controllers
{
    public class WizardController : Controller
    {
        private readonly ILogger<WizardController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly EmailConfigSettings _emailConfigurationSettings;
        private readonly IEmailSender _emailSender;
        private readonly IWizardService _wizardService;

        public WizardController(
            ILogger<WizardController> logger, 
            ApplicationDbContext context,
            EmailConfigSettings emailConfigSettings,
            IEmailSender emailSender,
            IWizardService wizardService)
        {
            _logger = logger;
            _context = context;
            _emailSender = emailSender;
            _emailConfigurationSettings = emailConfigSettings;
            _wizardService = wizardService;
        }

        public async Task<IActionResult> Index()
        {
            Dictionary<string, StepViewModel> wiz = new Dictionary<string, StepViewModel>();
            wiz.Add("S1", new StepViewModel());
            wiz.Add("S2", new StepViewModel());

            
            return View(wiz);
        }

        public async Task<IActionResult> Messages()
        {
            // var viewModel = await _context.Messages
            //     .Include(i => i.Topic)
            //     .ToListAsync();

            var m = _wizardService.GetAllAsync();
            return View(m);
        }

        public IActionResult StepOne()
        {
            var topics = from bc in _context.Topics
                                 orderby bc.Name
                                 select bc;
            ViewBag.TopicId = new SelectList(topics, "Id", "Name", null);

            _logger.LogInformation($"   -   StepOne {DateTime.UtcNow.ToLongTimeString()}");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StepTwo(MessageDto mvm)
        {
            if (ModelState.IsValid)
            {
                var topics = from bc in _context.Topics
                                    orderby bc.Name
                                    select bc;
                ViewBag.TopicId = new SelectList(topics, "Id", "Name", null);

                _logger.LogInformation($"   -   StepTwo {DateTime.UtcNow.ToLongTimeString()}");
                return View(mvm);
            }
            return View("StepOne");
        }



        [HttpPost]
        public async Task<IActionResult> SendForm(MessageDto m)
        {
            if (ModelState.IsValid)
            {
                EmailSender emailSenderFromConfig = new EmailSender(_emailConfigurationSettings);
                bool sendEmailResult = false;
                sendEmailResult = await emailSenderFromConfig.SendEmailAsync(m.EmailTo, "temat", m.MessageText, false);
                if(sendEmailResult)
                {
                    _logger.LogInformation($"   -   SendMessageAsync success");
                }
                else
                {
                    _logger.LogInformation($"   -   SendMessageAsync failed");
                }

                var info = new Info();
                

                int createdId = _wizardService.Add(m);
                if(sendEmailResult && createdId > 0)
                {
                   info.Id = createdId;
                   info.Information = "Wysłano wiadomość i zarejestrowano pod numerem ";    
                }
                else
                {
                    info.Information = "Nie udało się utworzyć i wysłać wiadomości";
                }
                return View("Result", info);
                //return RedirectToAction("Messages");
            }
            return View("StepTwo");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
