﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts.Factories;
using Application.Contracts.Infrastructure.Mailing;
using Application.Contracts.Infrastructure.Redering;
using Application.Infrastructure.Mailing;
using Domain.Aggregates;

namespace Application.Factories
{
    public class EmailFactory : IEmailFactory
    {
        private readonly ITemplateFactory _templateFactory;
        private readonly ITemplateService _templateService;

        public EmailFactory(ITemplateFactory templateFactory, ITemplateService templateService)
        {
            _templateFactory = templateFactory;
            _templateService = templateService;
        }

        public async Task<IEmail> CreateForUserReportRequestedAsync(User user, byte[] report)
        {
            var emailBodytemplate = await _templateFactory.CreateForUserReportRequestedAsync(user);
            var emailBodyTemplateRendered = await _templateService.RenderAsync<string>(emailBodytemplate);

            return new Email
            {
                UseCustomSmtpServer = false,
                To = user.Email,
                Subject = "InventApp - Inventions Report",
                Body = emailBodyTemplateRendered,
                Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        FileContent = report,
                        FileName = $"inventions_{Guid.NewGuid()}.pdf"
                    }
                }
            };
        }

        public async Task<IEmail> CreateForUserCreatedAsync(User user)
        {
            var emailBodytemplate = await _templateFactory.CreateForUserCreatedAsync(user);
            var emailBodyTemplateRendered = await _templateService.RenderAsync<string>(emailBodytemplate);

            return new Email
            {
                To = user.Email,
                Subject = "InventApp - User Created",
                Body = emailBodyTemplateRendered
            };
        }

        public async Task<IEmail> CreateForUserForgotPasswordAsync(User user)
        {
            var emailBodytemplate = await _templateFactory.CreateForUserForgotPasswordAsync(user);
            var emailBodyTemplateRendered = await _templateService.RenderAsync<string>(emailBodytemplate);

            return new Email
            {
                To = user.Email,
                Subject = "InventApp - Forgot Password",
                Body = emailBodyTemplateRendered
            };
        }
    }
}