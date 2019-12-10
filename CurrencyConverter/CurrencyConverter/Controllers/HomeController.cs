using CurrencyConverter.Models;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyConverter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel();
            model.Currencies = GetCurrenciesFromDB();
            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitForm(HomeModel model)
        {
            model.Currencies = GetCurrenciesFromDB();
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filePath = Path.Combine(appData, @"MyData.db");

            using (var db = new LiteDatabase(filePath))
            {
                var searchinfos = db.GetCollection<SearchInfo>("searchinfos");

                var searchInfo = new SearchInfo
                {

                    Amount = model.ConveterForm.GbrAmount.ToString(),
                    Currency = model.ConveterForm.CurrencyRatio,
                    SearchedOn = DateTime.Now
                };

                searchinfos.Insert(searchInfo);

                searchinfos.EnsureIndex(x => x.SearchedOn);
            }

            model.ConveterForm.ConvertedAmount = (model.ConveterForm.GbrAmount * double.Parse(model.ConveterForm.CurrencyRatio)).ToString();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult GetOldSearch(HomeModel model)
        {
            model.Currencies = GetCurrenciesFromDB();
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            var results = new List<SearchInfo>();

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var filePath = Path.Combine(appData, @"MyData.db");

            using (var db = new LiteDatabase(filePath))
            {
                var searchinfos = db.GetCollection<SearchInfo>("searchinfos");

                results = searchinfos.Find(x => x.SearchedOn >= model.FromDate && x.SearchedOn <= model.ToDate).ToList();
            }

            model.SearchInfos = results;

            return View("Index", model);
        }

        private Dictionary<string, double> GetCurrenciesFromDB()
        {
            return new Dictionary<string, double>
            {
                {"USD", 1.32},
                {"AUD", 1.94},
                {"EUR", 1.19}
            };
        }
    }
}