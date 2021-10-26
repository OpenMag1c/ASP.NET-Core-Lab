using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp1.Controllers
{
    public interface IMessageSender
    {
        string GetInfo();
    }

    public class HomeController
    {
        public string GetInfo() => "Hello, world!";
    }

    public class TextMessage : IMessageSender
    {
        public string GetInfo() => "Hello, world!";
    }

    public class MessageService
    {
        IMessageSender _sender;
        public MessageService(IMessageSender sender)
        {
            _sender = sender;
        }
        public string Getinfo()
        {
            return _sender.GetInfo();
        }
    }

    public class MessageMiddleware
    {
        private readonly RequestDelegate _next;

        public MessageMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMessageSender messageSender)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(messageSender.GetInfo());
        }
    }
}