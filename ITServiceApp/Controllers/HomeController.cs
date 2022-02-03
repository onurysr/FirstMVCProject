using AutoMapper;
using ITServiceApp.Data;
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

            var data = _dbContext.SubscriptionTypes.OrderBy(x => x.Price).Select(x=>_mapper.Map<SubscriptionTypeViewModel>(x)).ToList();
            return View(data);
        }
    }
}
