using AutoMapper;
using ITServiceApp.Data;
using ITServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Components
{
    public class PricingTableViewComponent:ViewComponent
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;

        public PricingTableViewComponent(MyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var data = _dbContext.SubscriptionTypes.OrderBy(x => x.Price).ToList().Select(x => _mapper.Map<SubscriptionTypeViewModel>(x)).ToList();
            return View(data);
        }
    }
}
