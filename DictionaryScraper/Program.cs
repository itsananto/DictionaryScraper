using DictionaryScraper.Business;
using DictionaryScraper.DAL;
using DictionaryScraper.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            IWordRespository wordRepo = new WordRespository();
            IDetailRepository detailRepo = new DetailRepository();
            IExampleRepository exampleRepo = new ExampleRepository();
            ISynonymRepository synonymRepo = new SynonymRepository();
            ISubsenseRepository subsenseRepo = new SubsenseRepository();

            Scraper obj = new Scraper(wordRepo, detailRepo, exampleRepo, synonymRepo, subsenseRepo);
            obj.ScrapeAll();
            
        }
    }
}
