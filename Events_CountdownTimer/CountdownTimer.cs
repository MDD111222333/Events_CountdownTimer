using System;
using System.Threading;

namespace Events_CountdownTimer
{
    public class CountdownTimer
    {
        public event EventHandler<int>? Tick;
        public event EventHandler? Finished;

        public void Start(int seconds)
        {
            for (int i = seconds; i >= 0; i--)
            {
                if (i > 0)
                {
                    Tick?.Invoke(this, i);
                    Thread.Sleep(500);
                }
                else
                {
                    Finished?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}