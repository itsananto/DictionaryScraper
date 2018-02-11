using DictionaryScraper.DAL;
using DictionaryScraper.Models;
using HtmlAgilityPack;
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

        public Scraper(IWordRespository _wordRepo, IDetailRepository _detailRepo, IExampleRepository _exampleRepo, ISynonymRepository _synonymRepo)
        {
            wordRepo = _wordRepo;
            detailRepo = _detailRepo;
            exampleRepo = _exampleRepo;
            synonymRepo = _synonymRepo;
        }

        public void Scrape(Word word)
        {
            var url = "https://en.oxforddictionaries.com/definition/" + word.Wrd;
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var val = doc.DocumentNode.SelectNodes("//div[@class='entryWrapper']").First();
            var value = val.SelectNodes(".//section[@class='gramb']");

            foreach (var item in value)
            {
                var pos = item.SelectSingleNode(".//span[@class='pos']").InnerText;

                var sembList = item.SelectNodes(".//ul[@class='semb']/li");

                foreach (var li in sembList)
                {
                    var def = li.SelectSingleNode("./div[@class='trg']/p//span[@class='ind']");

                    Detail detail = new Detail
                    {
                        WordID = word.ID,
                        Pos = pos,
                        Definition = (def != null) ? def.InnerText : ""
                    };

                    int id = detailRepo.InsertDetail(detail);

                    var examples = li.SelectNodes("./div[@class='trg']/div[@class='exg']/div[@class='ex']/em");
                    AddExample(examples, id);

                    var moreExamples = li.SelectNodes("./div[@class='trg']/div[@class='examples']/div[@class='exg']/ul/li[@class='ex']/em");
                    AddExample(moreExamples, id);

                    var synonyms = li.SelectSingleNode("./div[@class='trg']/div[@class='synonyms']/div[@class='exg']/div[@class='exs']");
                    if (synonyms != null)
                    {
                        synonymRepo.InsertSynonym(new Synonym
                        {
                            DetailsID = id,
                            Syn = synonyms.InnerText
                        });
                    }
                }
            }
        }

        public void AddExample(HtmlNodeCollection list, int detailsID)
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
                        Ex = descr
                    };

                    exampleRepo.InsertExample(example);
                }
            }
        }

        public void ScrapeAll()
        {
            var list = wordRepo.GetWords().Where(x => x.Wrd == "water");

            foreach (var word in list)
            {
                IEnumerable<Detail> ret = new List<Detail>();
                Scrape(word);
            }

        }
    }
}
