﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Application;
using Application.ApplicationResults;
using Domain.Contracts.Infrastructure.Crosscutting.Logging;
using WebApi.HttpActionResults;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InventAppApiController : ApiController
    {
        protected readonly ILogService _logService;

        public InventAppApiController(ILogService logService)
        {
            _logService = logService;
        }

        protected IHttpActionResult Execute<TResult>(Func<TResult> service, MediaType mediaType = MediaType.ApplicationJson) where TResult : IApplicationResult
        {
            InventAppContext.SecurityToken = ExtractToken(Request);

            var serviceResult = service.Invoke();

            switch (serviceResult.Status)
            {
                case ApplicationResultStatus.Ok:
                    if (serviceResult is EmptyResult) return Content(HttpStatusCode.OK, serviceResult.Message);
                    switch (mediaType)
                    {
                        case MediaType.ApplicationJson:
                            return Ok(((dynamic)serviceResult).Data);

                        case MediaType.TextHtml:
                            return new HtmlActionResult(((dynamic)serviceResult).Data, Request);

                        default:
                            return Ok(((dynamic)serviceResult).Data);
                    }

                case ApplicationResultStatus.BadRequest:
                    return Content(HttpStatusCode.BadRequest, serviceResult.Message);

                case ApplicationResultStatus.Unauthenticated:
                    return Content(HttpStatusCode.Unauthorized, serviceResult.Message);

                case ApplicationResultStatus.Unauthorized:
                    return Content(HttpStatusCode.Forbidden, serviceResult.Message);

                case ApplicationResultStatus.NotFound:
                    return Content(HttpStatusCode.NotFound, serviceResult.Message);

                case ApplicationResultStatus.UnsupportedMediaType:
                    return Content(HttpStatusCode.UnsupportedMediaType, serviceResult.Message);

                case ApplicationResultStatus.InternalServerError:
                    return Content(HttpStatusCode.InternalServerError, serviceResult.Message);

                default:
                    return Content(HttpStatusCode.InternalServerError, "Invalid internal ApplicationStatus");
            }
        }

        private static string ExtractToken(HttpRequestMessage request)
        {
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1) return null;

            var bearerToken = authzHeaders.ElementAt(0);
            return bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
        }
    }
}