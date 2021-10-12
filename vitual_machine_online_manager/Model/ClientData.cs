using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vitual_machine_online_manager.Model
{
    public class ClientData
    {
        public String vmName;
        public String clipboard;
        public ClientData(String vmName, String clipboard)
        {
            this.vmName = vmName;
            this.clipboard = clipboard;
        }
    }
}
