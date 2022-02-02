using AutoMapper;
using ITServiceApp.Models.Entities;
using ITServiceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.MapperProfiles
{
    public class SubscriptionProfiles:Profile
    {
        CreateMap<SubscriptionType, SubscriptionTypeViewModel>().ReverseMap();
        CreateMap<Address, AddressViewModel>().ReverseMap();
    }
}
