using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AttachedPropertyTest
{
    public class UserViewModel : ViewModelBase
    {
        ObservableCollection<UserModel> _UserList;
        ObservableCollection<UserModel> _SelectedItems;

        public ObservableCollection<UserModel> UserList { get => _UserList; }
        public ObservableCollection<UserModel> SelectedItems
        {
            get => _SelectedItems;
            set => _SelectedItems = value;
        }

        public UserViewModel()
        {
            _UserList = new ObservableCollection<UserModel>()
            {
                new UserModel(){Name="박지성", Age=36, City="서울"},
                new UserModel(){Name="김연아", Age=26, City="대전"},
                new UserModel(){Name="손흥민", Age=36, City="대구"},
                new UserModel(){Name="박찬호", Age=37, City="부산"},
                new UserModel(){Name="추신수", Age=31, City="인천"},
            };
        }


        ICommand _lvMouseDownCommand;
        public ICommand lvMouseDownCommand { get => _lvMouseDownCommand ?? (_lvMouseDownCommand = new RelayCommand<UserModel>(lvMouseDown)); }

        private void lvMouseDown(UserModel user)
        {
            if (user == null)
                SelectedItems.Clear();
        }
    }
}
