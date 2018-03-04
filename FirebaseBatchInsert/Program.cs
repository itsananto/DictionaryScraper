using Dapper;
using DictionaryScraperAPI.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseBatchInsert
{
    class Program
    {
        static IDbConnection db = new SqlConnection("Data Source=localhost;Initial Catalog=Dictionary;User Id=sa;Password=Ananto123;Integrated Security=True");
        static async void func()
        {

            var words = db.GetList<Words>().Where(x=>x.Wrd=="water");
            var details = db.GetList<Details>();
            var examples = db.GetList<Examples>();
            var subsenses = db.GetList<Subsenses>();
            var synonyms = db.GetList<Synonyms>();

            var auth = "sWRm0CJAYaQr6I2UwZ99SbqLwdanX0SJ8M3gAk4d"; // your app secret
            var firebaseClient = new FirebaseClient(
              "https://dictionary-ea2bb.firebaseio.com/",
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth)
              });

            foreach (var w in words)
            {
                w.Details = details.Where(x => x.WordID == w.ID);
                foreach (var d in w.Details)
                {
                    d.Examples = examples.Where(x => x.DetailsID == d.ID && x.SubsenseID == null);
                    d.ExampleList = d.Examples.Select(x=>x.Ex).ToArray();

                    d.Synonyms = synonyms.Where(x => x.DetailsID == d.ID);
                    d.SynonymList = d.Synonyms.Select(x=>x.Syn).ToArray();

                    d.Subsenses = subsenses.Where(x => x.DetailsID == d.ID);
                    foreach (var s in d.Subsenses)
                    {
                        s.Examples = examples.Where(x => x.SubsenseID == s.ID);
                        s.ExampleList = s.Examples.Select(x => x.Ex).ToArray();
                    }
                }

                var dino = await firebaseClient
              .Child("dictionary").Child(w.Wrd).PostAsync(w.Details, false);
            }
        }
        static void Main(string[] args)
        {
            func();
            Console.ReadLine();
        }
    }
}
