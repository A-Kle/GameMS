using System;
using System.Collections.Generic;
using System.Linq;
using GamesMS.Api.Models;
using GamesMS.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GamesMS.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardGamesApiController : ControllerBase
    {
        private readonly ILogger<BoardGamesApiController> _logger;
        private readonly IBoardGameRepository boardGameRepository;
        private readonly IGameStatisticRepository gameStatisticRepository;

        public BoardGamesApiController(ILogger<BoardGamesApiController> logger, IBoardGameRepository boardGameRepository, IGameStatisticRepository gameStatisticRepository)
        {
            _logger = logger;
            this.boardGameRepository = boardGameRepository;
            this.gameStatisticRepository = gameStatisticRepository;
        }

        [HttpGet("Get/{total}")]
        public ActionResult<IEnumerable<GameModel>> Get(int total)
        {
            return boardGameRepository.Query(total).Select(game => new GameModel
            {
                Id = game.Id,
                Name = game.Name
            })
            .ToArray();
        }

        [HttpGet("Details/{id}")]
        public ActionResult<BoardGameDetailsModel> Details(int id)
        {
            var game = boardGameRepository.Get(id);

            if (game == null)
                return NotFound();

            UpdateStatistics(game);

            return new BoardGameDetailsModel()
            {
                Id = game.Id,
                MinPlayerAge = game.MinPlayerAge,
                MaxPlayersNumber = game.MaxPlayersNumber,
                MinPlayersNumber = game.MinPlayersNumber,
                Name = game.Name
            };
        }

        private void UpdateStatistics(BoardGameRecord boardGameRecord)
        {
            var statisticsRecord = new GameStatisticRecord() { Game = boardGameRecord, Source = GamesMS.Models.EntityViewSource.Webservice, ViewedDate = DateTime.Now };

            gameStatisticRepository.CreateOrUpdate(statisticsRecord);
        }
    }
}
