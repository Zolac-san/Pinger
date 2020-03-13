using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Pinger2000
{
    class Pinger
    {
        private List<long> listDelay;
        private static readonly int MAX_HIST = 100;

        public List<long> ListDelay => listDelay;

        public Pinger()
        {
            this.listDelay = new List<long>();
        }


        public void PingHost(string nameOrAddress)
        {
            Console.WriteLine("IP : "+ nameOrAddress);
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                if (reply != null)
                {
                    if(reply.Status == IPStatus.Success)
                        addValue(reply.RoundtripTime);
                    else
                        addValue(-1);
                }
                    
          
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

        }


        private void addValue(long val)
        {
            listDelay.Add(val);
            
            if (listDelay.Count > MAX_HIST)
            {
                listDelay.RemoveAt(0);
            }
        }

        public void Clear()
        {
            listDelay.Clear();
        }
    }
}