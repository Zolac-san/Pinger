using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pinger2000
{
    class Adress
    {
        private static string nameFileConfig = "adress.json";

        private Dictionary<string, string> _adresses;




        public Adress()
        {
            _adresses = new Dictionary<string, string>();
        }

        public void Load()
        {
            try
            {
                _adresses = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(nameFileConfig));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        public string getIPOf(string name)
        {
            return _adresses[name];
        }

        public List<string> GetAllNames()
        {
            return new List<string>(_adresses.Keys);
        }

    }
}