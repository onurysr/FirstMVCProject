using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.ViewModels
{
    public class JsonResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
}
