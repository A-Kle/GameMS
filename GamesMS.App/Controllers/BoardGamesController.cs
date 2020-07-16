﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GamesMS.Records;
using GamesMS.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System;
using GamesMS.Core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace GamesMS.Controllers
{
    public class BoardGamesController : Controller
    {
        private readonly ILogger<BoardGamesController> _logger;
        private readonly IBoardGameRepository gameRepository;
        private readonly IGameStatisticRepository gameStatisticRepository;

        public BoardGamesController(ILogger<BoardGamesController> logger, IBoardGameRepository gameRepository, IGameStatisticRepository gameStatisticRepository)
        {
            _logger = logger;
            this.gameRepository = gameRepository;
            this.gameStatisticRepository = gameStatisticRepository;
        }

        public IActionResult Index(int? pageNumber, int? pageSize)
        {
            var games = gameRepository.Query();

            var model = new GamesIndexViewModel()
            {
                PageNumber = pageNumber ?? 1,
                GameModels = gameRepository.QueryOver(pageNumber ?? 1, pageSize ?? 10)
                .Select(g => new GamesModel() { Id = g.Id, Name = g.Name })
                .ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = gameRepository.Get(id);

            if(game == null)
                throw new InvalidOperationException("Entity does not exist.");

            UpdateStatistics(game);

            var model = new BoardGameDetailsViewModel()
            {
                GameId = game.Id,
                Name = game.Name,
                MinPlayerAge = game.MinPlayerAge,
                MaxPlayersNumber = game.MaxPlayersNumber,
                MinPlayersNumber = game.MinPlayersNumber,
                Statistics = game.Statistics
                    .Select(s => new GameStatisticViewModel() { Source = s.Source.GetAttribute<DisplayAttribute>().Name, ViewedDate = s.ViewedDate })
                    .ToList()
            };

            return View(model);
        }

        private void UpdateStatistics(BoardGameRecord boardGameRecord)
        {
            var statisticsRecord = new GameStatisticRecord() { Game = boardGameRecord, Source = GamesMS.Models.EntityViewSource.Webservice, ViewedDate = DateTime.Now };

            gameStatisticRepository.CreateOrUpdate(statisticsRecord);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string param)
        {
            var model = new BoardGameCreateUpdateViewModel();

            if(await TryUpdateModelAsync(model))
            {
                var newRecord = new BoardGameRecord()
                {
                    MinPlayerAge = model.MinPlayerAge,
                    MaxPlayersNumber = model.MaxPlayersNumber,
                    MinPlayersNumber = model.MinPlayersNumber,
                    Name = model.Name
                };

                gameRepository.CreateOrUpdate(newRecord);
            }

            if(ModelState.ErrorCount > 0)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var game = gameRepository.Get(id);

            if (game == null)
                throw new InvalidOperationException("Entity does not exist.");

            var model = new BoardGameCreateUpdateViewModel()
            {
                Id = game.Id,
                MinPlayerAge= game.MinPlayerAge,
                MaxPlayersNumber = game.MaxPlayersNumber,
                MinPlayersNumber = game.MinPlayersNumber,
                Name = game.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string param)
        {
            var game = gameRepository.Get(id);

            if (game == null)
                throw new InvalidOperationException("Entity does not exist.");

            var model = new BoardGameCreateUpdateViewModel();

            if (await TryUpdateModelAsync(model))
            {
                game.MaxPlayersNumber = model.MaxPlayersNumber;
                game.MinPlayerAge = model.MinPlayerAge;
                game.MinPlayersNumber = model.MinPlayersNumber;
                game.Name = model.Name;

                gameRepository.CreateOrUpdate(game);
            }

            if (ModelState.ErrorCount > 0)
            {

            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var game = gameRepository.Get(id);

            if(game != null)
                gameRepository.Delete(game);

            return RedirectToAction("Index");
        }
    }
}
