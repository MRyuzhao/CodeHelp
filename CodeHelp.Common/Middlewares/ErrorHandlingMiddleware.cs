using System;
using System.Text;
using System.Threading.Tasks;
using CodeHelp.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodeHelp.Common.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AggregateException aggregateException)
            {
                aggregateException.Handle(ex =>
                {
                    switch (ex)
                    {
                        case DomainException domainException:
                            return HandleException(context, domainException);
                        case DataException dataException:
                            return HandleException(context, dataException);
                        case DomainServiceException domainServiceException:
                            return HandleException(context, domainServiceException);
                        case NotFoundException notFoundException:
                            return HandleNotFoundException(context, notFoundException);
                    }
                    return HandleException(context, ex);
                });
            }
            catch (DomainException domainException)
            {
                //_logger.RecieveWebApiRequestFailed(context.Request.GetUri(), userId, domainException);
                HandleDomainException(context, domainException);
            }
            catch (DomainServiceException domainServiceException)
            {
                //_logger.RecieveWebApiRequestFailed(context.Request.GetUri(), userId, domainServiceException);
                HandleDomainServiceException(context, domainServiceException);
            }
            catch (NotFoundException notFoundException)
            {
                //_logger.RecieveWebApiRequestFailed(context.Request.GetUri(), userId, notFoundException);
                HandleNotFoundException(context, notFoundException);
            }
            catch (UnauthorizedException unauthorizedException)
            {
                //_logger.RecieveWebApiRequestFailed(context.Request.GetUri(), userId, unauthorizedException);
                HandleUnauthorizedException(context, unauthorizedException);
            }
            catch (ForbiddenException forbiddenException)
            {
                //_logger.RecieveWebApiRequestFailed(context.Request.GetUri(), userId, forbiddenException);
                HandleForbiddenException(context, forbiddenException);
            }
            catch (Exception exception)
            {
                //_logger.RecieveWebApiRequestFailed(context.Request.GetUri(), userId, exception);
                HandleException(context, exception);
            }
        }

        private bool HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            context.Response.WriteAsync(
                new InternalErrorViewModel(exception.Message, exception.StackTrace).ToString(),
                Encoding.UTF8).GetAwaiter().GetResult();
            return true;
        }
        private void HandleUnauthorizedException(HttpContext context, UnauthorizedException exception)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            context.Response.WriteAsync(
                new InternalErrorViewModel(exception.Message).ToString(),
                Encoding.UTF8).GetAwaiter().GetResult();
        }

        private void HandleForbiddenException(HttpContext context, ForbiddenException exception)
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";

            context.Response.WriteAsync(
                new InternalErrorViewModel(exception.Message).ToString(),
                Encoding.UTF8).GetAwaiter().GetResult();
        }

        private bool HandleNotFoundException(HttpContext context, NotFoundException exception)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";

            context.Response.WriteAsync(
                new InternalErrorViewModel(exception.Message).ToString(),
                Encoding.UTF8).GetAwaiter().GetResult();
            return true;
        }

        private static bool HandleDomainException(HttpContext context, DomainException domainException)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            context.Response.WriteAsync(
                new InternalErrorViewModel(domainException.Message, domainException.StackTrace).ToString(),
                Encoding.UTF8).GetAwaiter().GetResult();
            return true;
        }

        private static bool HandleDomainServiceException(HttpContext context, DomainServiceException domainServiceException)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            context.Response.WriteAsync(
                new InternalErrorViewModel(domainServiceException.Message, domainServiceException.StackTrace).ToString(),
                Encoding.UTF8).GetAwaiter().GetResult();
            return true;
        }

        internal class InternalErrorViewModel
        {
            public string Message { get; set; }
            public string StackTrace { get; set; }
            public InternalErrorViewModel(string message) { this.Message = message; }
            public InternalErrorViewModel(string message, string stackTrace)
            {
                this.Message = message;
                StackTrace = stackTrace;
            }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(
                    this,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() : base("未找到资源") { }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("未认证") { }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("禁止访问") { }
    }
}