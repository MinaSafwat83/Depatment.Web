using DepartmentModule.UseCase.PluginInterface.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesPlugin
{
    public class MockEmailService : IEmailService
    {
        private readonly ILogger<MockEmailService> _logger;
        public MockEmailService(ILogger<MockEmailService> logger)
        {
            _logger = logger;
        }
        public Task SendEmailAsync(string toEmail, string subject, string message)
        {
            // Log the email sending or just output it to the console for testing purposes
           _logger.LogInformation($"Mock Email Sent to: {toEmail}, Subject: {subject}, Message: {message}");
            return Task.CompletedTask;
        }
    }
}
