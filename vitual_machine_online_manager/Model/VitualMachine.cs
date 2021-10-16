using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vitual_machine_online_manager.Model
{
    public class VitualMachine
    {
        public String name { get; private set; }
        public DateTime? lastTimePing { get; private set; }
        //public List<String> listNameScreenshot { get; private set; }
        public List<ClipboardStore> listClipboard { get; private set; }
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
            //this.listNameScreenshot = createByClient ? new List<String>() : null;
            this.listClipboard = createByClient ? new List<ClipboardStore>() : null;
            this.loaded = false;
        }

        public void loadFromStorage(DateTime? lastTimePing, List<ClipboardStore>? listClipboard)
        {
            if (!this.loaded)
            {
                this.lastTimePing = lastTimePing;
                //this.listNameScreenshot = listNameScreenshot ?? new List<String>();
                this.listClipboard = listClipboard ?? new List<ClipboardStore>();
                this.loaded = true;
            }
        }

        public void updateData(ClipboardStore? clipboard)
        {
            var dateTimeUtc = DateTime.Now.ToUniversalTime();
            lastTimePing = dateTimeUtc.AddHours(7);
            //if (nameScreenshot != null)
            //    this.listNameScreenshot.Add(nameScreenshot);
            if (clipboard != null)
            {
                this.listClipboard.Add(clipboard);
                while (this.listClipboard.Count > 10)
                {
                    this.listClipboard.RemoveAt(this.listClipboard.Count - 1);
                }
            }
        }
    }

    public class ClipboardStore
    {
        public String content { get; set; }
        public DateTime timeUploaded{ get; set; }
        public ClipboardStore(String content, DateTime timeUploaded)
        {
            this.content = content;
            this.timeUploaded = timeUploaded;
        }
    }

}
