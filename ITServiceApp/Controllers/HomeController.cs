using AutoMapper;
using ITServiceApp.Data;
using ITServiceApp.Models.Entities;
using ITServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;
        public HomeController(MyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {

            var data = _dbContext.SubscriptionTypes.OrderBy(x => x.Price).ToList().Select(x=>_mapper.Map<SubscriptionTypeViewModel>(x)).ToList();

            //var model = new List<SubscriptionTypeViewModel>();

            //var query = _dbContext.SubscriptionTypes.OrderBy(x => x.Price).ToList();

            //foreach (var item in query)              ---------------- Uzun YOl-------------
            //{
            //    var dataItem = _mapper.Map<SubscriptionTypeViewModel>(SubscriptionType);
            //    model.Add(dataItem);
            //}

            return View(data);
        }
    }
}
