using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Timers;
using Newtonsoft.Json;
using System.Net;

namespace ExtendedDisplay
{
    public class ConnectionInputWindow : DialogViewController
    {
        private readonly EntryElement ipAddressElement;

        private readonly EntryElement portElement;

        private readonly StringElement okElement;

        public string IpAddress
        {
            get
            {
                return this.ipAddressElement.Value;
            }
        }

        public string Port
        {
            get
            {
                return this.portElement.Value;
            }
        }

        public event EventHandler Dismissed;

        public void NotifyDismissed()
        {
            if (this.Dismissed != null)
            {
                this.Dismissed(this, EventArgs.Empty);
            }
        }

        public ConnectionInputWindow() : base(UITableViewStyle.Grouped, new RootElement(string.Empty))
        {
            this.ipAddressElement = new EntryElement("Ip Address", string.Empty, "192.168.1.1");
            this.portElement = new EntryElement("Port", string.Empty, "8080");
            this.okElement = new StringElement("OK", () => this.DismissViewController(true, () => this.NotifyDismissed()));
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Root = new RootElement(string.Empty)
            {
                new Section()
                {
                    this.ipAddressElement,
                    this.portElement
                },
                new Section()
                {
                    this.okElement,
                }
            };
            this.LoadView();
        }
    }
}

