using DictionaryScraper.DAL;
using DictionaryScraper.Models;
using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DictionaryScraper.Business
{
    class Scraper
    {
        IWordRespository wordRepo;
        IDetailRepository detailRepo;
        IExampleRepository exampleRepo;
        ISynonymRepository synonymRepo;
        ISubsenseRepository subsenseRepo;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Scraper(IWordRespository _wordRepo, IDetailRepository _detailRepo, IExampleRepository _exampleRepo, ISynonymRepository _synonymRepo, ISubsenseRepository _subsenseRepo)
        {
            wordRepo = _wordRepo;
            detailRepo = _detailRepo;
            exampleRepo = _exampleRepo;
            synonymRepo = _synonymRepo;
            subsenseRepo = _subsenseRepo;
        }

        public void Scrape(Word word)
        {
            var url = "https://en.oxforddictionaries.com/definition/" + word.Wrd;
            var web = new HtmlWeb();
            var doc = web.Load(url);

            logger.Info("Scraping starting for {0}", word.Wrd);

            var val = doc.DocumentNode.SelectNodes("//div[@class='entryWrapper']").First();
            var value = val.SelectNodes(".//section[@class='gramb']");

            foreach (var item in value)
            {
                var pos = item.SelectSingleNode(".//span[@class='pos']").InnerText;
                var sembList = item.SelectNodes(".//ul[@class='semb']/li");

                foreach (var li in sembList)
                {
                    var def = li.SelectSingleNode("./div[@class='trg']/p//span[@class='ind']");
                    int id = AddDetail(def, word.ID, pos);

                    var examples = li.SelectNodes("./div[@class='trg']/div[@class='exg']/div[@class='ex']/em");
                    AddExample(examples, id, null);

                    var moreExamples = li.SelectNodes("./div[@class='trg']/div[@class='examples']/div[@class='exg']/ul/li[@class='ex']/em");
                    AddExample(moreExamples, id, null);

                    var synonyms = li.SelectSingleNode("./div[@class='trg']/div[@class='synonyms']/div[@class='exg']/div[@class='exs']");
                    AddSynonym(synonyms, id);

                    var subsenseList = li.SelectNodes("./div[@class='trg']/ol[@class='subSenses']/li[@class='subSense']");
                    AddSubsense(subsenseList, id);
                }
            }
        }

        public void AddSynonym(HtmlNode node, int detailsID)
        {
            if (node != null)
            {
                synonymRepo.InsertSynonym(new Synonym
                {
                    DetailsID = detailsID,
                    Syn = node.InnerText
                });
            }
        }

        public int AddDetail(HtmlNode node, int wordsID, string pos)
        {
            Detail detail = new Detail
            {
                WordID = wordsID,
                Pos = pos,
                Definition = (node != null) ? node.InnerText : ""
            };

            return detailRepo.InsertDetail(detail);
        }

        public void AddSubsense(HtmlNodeCollection list, int detailsID)
        {
            if (list != null)
            {
                foreach (var subsense in list)
                {
                    var definition = subsense.SelectSingleNode("./span[@class='ind']");
                    if (definition != null)
                    {
                        Subsense sub = new Subsense
                        {
                            DetailsID = detailsID,
                            Definition = definition.InnerText
                        };

                        int subsenseID = subsenseRepo.InsertSubsense(sub);

                        var examples = subsense.SelectNodes("./div[@class='trg']/div[@class='exg']/div[@class='ex']/em");
                        var moreExamples = subsense.SelectNodes("./div[@class='trg']/div[@class='examples']/div[@class='exg']/ul/li[@class='ex']/em");

                        AddExample(examples, detailsID, subsenseID);
                        AddExample(moreExamples, detailsID, subsenseID);
                    }
                }
            }
        }

        public void AddExample(HtmlNodeCollection list, int detailsID, int? subsenseID)
        {
            if (list != null)
            {
                foreach (var ex in list)
                {
                    string descr = ex.InnerText;
                    descr = descr.Replace("&lsquo;", "");
                    descr = descr.Replace("&rsquo;", "");

                    Example example = new Example
                    {
                        DetailsID = detailsID,
                        SubsenseID = subsenseID,
                        Ex = descr
                    };

                    exampleRepo.InsertExample(example);
                }
            }
        }

        public void ScrapeAll()
        {
            //var list = wordRepo.GetWords().Where(x => x.Wrd == "action");
            var list = wordRepo.GetWords();

            foreach (var word in list)
            {
                logger.Info("Scraping starting for {0}", word.Wrd);
                IEnumerable<Detail> ret = new List<Detail>();
                Scrape(word);
                logger.Info("Scraping ended for {0}", word.Wrd);
            }

        }
    }
}
