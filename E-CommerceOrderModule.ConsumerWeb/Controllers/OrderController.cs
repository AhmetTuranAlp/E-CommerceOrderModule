using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.ConsumerWeb.RabbitMQ;
using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Core.Asbtract;
using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Core.Asbtract.Services;
using E_CommerceOrderModule.Repository.Concrete;
using E_CommerceOrderModule.Repository.Concrete.Repositories;
using E_CommerceOrderModule.Repository.Context;
using E_CommerceOrderModule.Services.Mapping;
using E_CommerceOrderModule.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceOrderModule.ConsumerWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly RabbitMQConsumer _rabbitMQConsumer;
 
        public OrderController(RabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
           
        }

        public IActionResult Index()
        {
            _rabbitMQConsumer.Consumer();
            return View();
        }

    
    }
}
