using DotNetCodeGenerator.Domain.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetCodeGenerator.Controllers
{
    public abstract class BaseController : Controller
    {
        [Inject]
        public TableService TableService { get; set; }
    }
}