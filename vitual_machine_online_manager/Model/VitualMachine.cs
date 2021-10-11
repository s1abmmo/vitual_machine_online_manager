using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vitual_machine_online_manager.Model
{
    class VitualMachine
    {
        public String name { get; private set; }
        public DateTime? lastTimePing { get; private set; }
        public TimeSpan? durationFromLastTimePing { get; private set; }
        public bool? isOk { get; private set; }
        public List<String> listNameScreenshot { get; private set; }

        public VitualMachine(String name)
        {
            this.name = name;
            this.lastTimePing = null;
            this.durationFromLastTimePing = null;
            this.isOk = null;
            this.listNameScreenshot = listNameScreenshot ?? new List<String>();
        }

        public void Load(DateTime? lastTimePing, TimeSpan? durationFromLastTimePing, bool? isOk, List<String>? listNameScreenshot)
        {
            this.lastTimePing = lastTimePing;
            this.durationFromLastTimePing = durationFromLastTimePing;
            this.isOk = isOk;
            this.listNameScreenshot = listNameScreenshot ?? new List<String>();
        }

    }
}
