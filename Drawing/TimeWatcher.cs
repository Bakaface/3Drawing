using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Drawing
{
    enum TimeWatcherState
    {
        Running,
        Stopped
    }


    class TimeWatcher
    {
        DispatcherTimer timer;

        public TimeWatcherState State { get; set; }
        public Solid Solid { get; set; }
        public Scene Scene { get; private set; }
        public Direction Direction { get; set; }

        public TimeWatcher(Solid solid, Scene scene)
        {
            Solid = solid;
            Scene = scene;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += delegate
            {
                MovementTools.MoveOnX(Solid, Direction == Direction.Forward ? 0.05 : -0.05);
                Scene.Refresh();
            };
            State = TimeWatcherState.Stopped;
        }

        public void Stop()
        {
            if (timer.IsEnabled)
                timer.Stop();
            State = TimeWatcherState.Stopped;
        }

        public void Start()
        {
            if (!timer.IsEnabled)
                timer.Start();
            State = TimeWatcherState.Running;
        }


    }
}
