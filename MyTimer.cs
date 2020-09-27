using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyTimer
{
    sealed public class MyTimer
    {
        public delegate void ObjectHandler(object sender, object obj);
        public event ObjectHandler Notify;

        private List<MyTimerModel> _objects;
        private bool _working;
        private object _locker;

        public void AddObject(List<MyTimerModel> myTimerModels)
        {
            lock (_locker)
            {
                _objects.AddRange(myTimerModels);
                _objects.Sort(new MyTimerModel());
            }
        }
        public void AddObject(MyTimerModel myTimerModel)
        {
            lock (_locker)
            {
                _objects.Add(myTimerModel);
                _objects.Sort(new MyTimerModel());
            }
        }
        public void AddObject(object obj, DateTime whenNotify)
        {
            lock (_locker)
            {
                _objects.Add(new MyTimerModel() { obj = obj, whenNotify = whenNotify });
                _objects.Sort(new MyTimerModel());
            }
        }
        public void RemoveObject(MyTimerModel obj)
        {
            lock (_locker)
            {
                _objects.Remove(obj);
            }
        }
        public void Start()
        {
            _working = true;
            Thread notifierThread = new Thread(Timer);
            notifierThread.Start();
        }
        public void Stop()
        {
            _working = false;
            Thread.Sleep(1000);
        }
        private void Timer()
        {
            while (_working)
            {
                lock (_locker)
                {
                    foreach (var item in _objects.ToArray())
                    {
                        if (item.whenNotify <= DateTime.Now)
                        {
                            Notify?.Invoke(this, item.obj);
                            RemoveObject(item);
                        }
                    }
                }
            }
        }

        public MyTimer(List<MyTimerModel> objects = null)
        {
            _objects = objects ?? new List<MyTimerModel>();
            _locker = new object();
            _working = false;
        }
    }
}
