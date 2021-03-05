using KissLog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.Fundo.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected MainController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
