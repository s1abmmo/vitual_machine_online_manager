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
        public List<String> listNameScreenshot { get; private set; }
        private bool loaded;

        public VitualMachine(String name, bool createByClient = false)
        {
            this.name = name;
            this.lastTimePing = null;
            if (createByClient)
            {
                var dateTimeUtc = DateTime.Now.ToUniversalTime();
                lastTimePing = dateTimeUtc.AddHours(7);
            }
            this.listNameScreenshot = new List<String>();
            this.loaded = false;
        }

        public void loadFromStorage(DateTime? lastTimePing, TimeSpan? durationFromLastTimePing, bool? isOk, List<String>? listNameScreenshot)
        {
            if (!this.loaded)
            {
                this.lastTimePing = lastTimePing;
                this.listNameScreenshot = listNameScreenshot ?? new List<String>();
                this.loaded = true;
            }
        }

        public void updateData(List<String> listNameScreenshot)
        {
            var dateTimeUtc = DateTime.Now.ToUniversalTime();
            lastTimePing = dateTimeUtc.AddHours(7);
        }

    }
}
