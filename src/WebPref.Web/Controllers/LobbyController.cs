using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebPref.Web.Models;
using WebPref.Web.Services;
using WebPref.Core.Lobby;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace WebPref.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private TableService tableService;
        private UserManager<IdentityUser> userManager;

        public LobbyController(UserManager<IdentityUser> userManager, TableService tableService)
        {
            this.tableService = tableService;
            this.userManager = userManager;
        }

        [HttpGet]    
        [Route("GetTableList")]
        public List<TableModel> GetTableList([FromQuery] bool dbRefresh = false)
        {
            IEnumerable<Table> tables = tableService.GetTables();
            List<TableModel> result = new List<TableModel>();
            foreach(var table in tables)
            {
                TableModel tableModel = new TableModel(table);
                //tableModel.Name = "Стол №" + table.Number.ToString();
                //tableModel.State = TableState.Waiting; //TODO - подумать, что тут можно отдавать
                //tableModel.PlayerCount = table.TablePlayers.Count();
                result.Add(tableModel);
            }
            return result;            
        }

        [HttpPost]
        [Route("CreateTable")]
        public async Task<ResultModel> CreateTable([FromBody]Dictionary<string, string> tableParams)
        {
            string playerId, playerName;
            var user = await userManager.GetUserAsync(HttpContext.User);
            
            Table newTable;
            playerId = user.Id;
            playerName = user.UserName;

            TableSettings tableSettings = new TableSettings(Core.Playing.PlayersCountEnum.Three, Core.Playing.GameTypeEnum.Leningrad, false);
            string resultDescription;
            ResultModel result = new ResultModel();
            if (tableService.CreateTable(playerId, playerName, tableSettings, out resultDescription, out newTable))
            {
                result.Success = true;
                result.Description = resultDescription;
                //прямо стол не отдать, там циклические ссылки получаются
                TableModel tableModel = new TableModel(newTable);                
                result.Data = JsonConvert.SerializeObject(tableModel);
            }
            else
            {
                result.Success = false;
                result.Description = resultDescription;
                result.Data = "";
            }
            return result;            
        }
    }
}