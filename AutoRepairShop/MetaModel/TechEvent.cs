﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.MetaModel
{
    public class TechEvent : INotifyPropertyChanged
    {
        private int? _id;
        private Car _car;
        private DateTime? _eventStartDate;
        private DateTime? _eventEndDate;
        private ObservableCollection<WorkMate> _works;

        public int? Id { get => _id; set => _id = value; }
        public Car Car { get => _car; set => _car = value; }

        public DateTime? EventStartDate
        {
            get
            {
                return _eventStartDate;
            }
            set
            {
                _eventStartDate = value;
                OnPropertyChanged(nameof(EventStartDate));
            }
        }

        public DateTime? EventEndDate
        {
            get
            {
                return _eventEndDate;
            }
            set
            {
                _eventEndDate = value;
                OnPropertyChanged(nameof(EventEndDate));
            }
        }
                
        public ObservableCollection<WorkMate> Works
        {
            get
            {
                return _works;
            }
            set
            {
                
                if (_works != value)
                {
                    _works = value;
                    OnPropertyChanged(nameof(Works));
                }
                
            }
        }

        public TechEvent()
        { 
        }

        public TechEvent(int? id, Car car , DateTime? eventStartDate = null, DateTime? eventEndDate = null)
        {
            Id = id;
            Car = car;
            EventEndDate = eventEndDate;
            EventStartDate = eventStartDate;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
