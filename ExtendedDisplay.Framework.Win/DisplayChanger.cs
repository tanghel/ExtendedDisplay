using System.Diagnostics;

namespace ExtendedDisplay
{
    public class DisplayChanger
    {
        public static DisplayChanger Instance { get; private set; }

        static DisplayChanger()
        {
            Instance = new DisplayChanger();
        }

        private DisplayChanger()
        {
        }

        private void ChangeSettings(string arguments)
        {
            var displayChanger = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "DisplaySwitch.exe",
                    Arguments = arguments
                }
            };
            displayChanger.Start();
        }

        public void Extend()
        {
            this.ChangeSettings("/extend");
        }

        public void Compress()
        {
            this.ChangeSettings("/internal");
        }
    }
}
