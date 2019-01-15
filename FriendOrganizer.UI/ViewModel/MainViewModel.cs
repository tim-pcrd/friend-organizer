using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autofac.Features.Indexed;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Annotations;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private IDetailViewModel _detailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, 
                             IIndex<string,IDetailViewModel> detailViewModelCreator,
                             IEventAggregator eventAggregator,
                             IMessageDialogService messageDialogService)
        {
            _detailViewModelCreator = detailViewModelCreator;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;


            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);

            NavigationViewModel = navigationViewModel;

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public INavigationViewModel NavigationViewModel { get; }

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            set
            {
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateNewDetailCommand { get; }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            //switch (args.ViewModelName)
            //{
            //    case nameof(FriendDetailViewModel):
            //        DetailViewModel = _friendDetailViewModelCreator();
            //        break;
            //    case nameof(MeetingDetailViewModel):
            //        DetailViewModel = _meetingDetailViewModelCreator();
            //        break;
            //    default:
            //        throw new Exception($"ViewModel {args.ViewModelName} not mapped");
            //}

            DetailViewModel = _detailViewModelCreator[args.ViewModelName];
            
            await DetailViewModel.LoadAsync(args.Id);
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs(){ViewModelName = viewModelType.Name});
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }

      

    }
}
