using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebPref.Web.Models;

namespace WebPref.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        [HttpGet]    
        [Route("GetTableList")]
        public List<TableModel> GetTableList()
        {
            return new List<TableModel> { 
                new TableModel
                {
                    Name = "Первый стол",
                    State = TableState.Waiting,
                    PlayerCount = 2
                },
                new TableModel
                {
                    Name = "Ещё стол",
                    State = TableState.Playing,
                    PlayerCount = 4
                }
            };
        }
    }
}