using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.UI.Event;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private readonly string _detailViewModelName;
        private readonly IEventAggregator _eventAggregator;
        private string _displayMember;

        public NavigationItemViewModel(int id, string displaymember, string detailViewModelName ,IEventAggregator eventAggregator)
        {
            _detailViewModelName = detailViewModelName;
            this._eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displaymember;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

        

        public int Id { get; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenDetailViewCommand { get; }

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(new OpenDetailViewEventArgs()
                {
                    Id = Id,
                    ViewModelName = _detailViewModelName
                });
        }
    }
}
