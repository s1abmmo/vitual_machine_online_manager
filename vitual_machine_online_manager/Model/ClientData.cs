using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vitual_machine_online_manager.Model
{
    public class ClientData
    {
        public String vmName { get; private set; }
        //public String? imageBase64 { get; private set; }
        public String? clipboard { get; private set; }
        public ClientData(String vmName, String? clipboard)
        {
            this.vmName = vmName;
            //this.imageBase64 = imageBase64;
            this.clipboard = clipboard;
        }
    }
}
