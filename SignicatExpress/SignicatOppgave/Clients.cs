using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignicatOppgave
{
    class Clients
    {
        private string clientId;
        private string clientSecret;

        public Clients()
        {
            clientId = "t780ce0c9da4f48fbad63d30ae59d2309";
            clientSecret = "NgMhU4LtXncPIdREGCGY1FJWJA77PWoY3nWYtsEx3eoEBg0Bg0NvBFh5aL3NdtJ3";
        }

        internal string GetClientId()
        {
            return clientId;
        }
        internal string GetClientSecret()
        {
            return clientSecret;
        }
    }
}
