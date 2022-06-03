using HomeWork_26.Infrastructure.Commands;
using HomeWork_26.Models;
using HomeWork_26.Services;
using HomeWork_26.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeWork_26.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        static string Path = "_DB.json";

        #region Заголовок окна

        /// <summary>Заголовок окна</summary>
        /// 
        private string _Title = "Bank A";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Status : string - Статус программы
        private string _Status = "Готов!";

        /// <summary>Статус программы</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion

        #region NameClient : string - Имя клиента
        private string _NameClient;

        /// <summary>Имя клиента</summary>
        public string NameClient
        {
            get => _NameClient;
            set => Set(ref _NameClient, value);
        }
        #endregion

        #region TypeClient : string - Тип клиента
        private string _TypeClient;

        /// <summary>Тип клиента</summary>
        public string TypeClient
        {
            get => _TypeClient;
            set => Set(ref _TypeClient, value);
        }
        #endregion

        #region TypeAccount : string - Тип счета клиента
        private string _TypeAccount = "Deposit";

        /// <summary>Тип счета клиента</summary>
        public string TypeAccount
        {
            get => _TypeAccount;
            set => Set(ref _TypeAccount, value);
        }
        #endregion

        #region AmountClient : string - Сумма пополнения счета клиента
        private string _AmountClient = "0";

        /// <summary>Сумма пополнения счета клиента</summary>
        public string AmountClient
        {
            get => _AmountClient;
            set => Set(ref _AmountClient, value);
        }
        #endregion        

        #region DBClients : ObservableCollection - База данных клиентов банка
        private ObservableCollection<Client> _DBClients;

        /// <summary>База клиентов</summary>
        public ObservableCollection<Client> DBClients
        {
            get => _DBClients;
            set => Set(ref _DBClients, value);
        }
        #endregion

        #region Clear : void - Очистка полей
        private void Clear()
        {
            NameClient = string.Empty;
            AmountClient = "0";
        }
        #endregion

        #region Команды

        #region CreateClientCommand
        public ICommand CreateClientCommand { get; }

        private bool CanCreateClientCommandExecute(object p)
        {
            if (NameClient!="")
            {
                return true;
            }
            return false;
        }
        private void OnCreateClientCommandExecuted(object p)
        {          
            
            DBClients.Add(new Client(NameClient, TypeAccount, double.Parse(AmountClient)));
            LoadSave<Client>.SaveDB(Path, DBClients);
            Clear();
        }
        #endregion

        #region CreateEntityClientCommand
        public ICommand CreateEntityClientCommand { get; }

        private bool CanCreateEntityClientCommandExecute(object p)
        {
            if (NameClient != "")
            {
                return true;
            }
            return false;

        }
        private void OnCreateEntityClientCommandExecuted(object p)
        {

            DBClients.Add(new EntityClient(NameClient, TypeAccount, double.Parse(AmountClient)));
            LoadSave<Client>.SaveDB(Path, DBClients);
            Clear();
        }
        #endregion

        #region CreateVIPClientCommand
        public ICommand CreateVIPClientCommand { get; }

        private bool CanCreateVIPClientCommandExecute(object p)
        {
            if (NameClient != "")
            {
                return true;
            }
            return false;

        }
        private void OnCreateVIPClientCommandExecuted(object p)
        {

            DBClients.Add(new VIPClient(NameClient, TypeAccount, double.Parse(AmountClient)));
            LoadSave<Client>.SaveDB(Path, DBClients);
            Clear();
        }
        #endregion

        #region CloseAppicationCommand
        public ICommand CloseAppicationCommand { get; }

        private bool CanCloseAppicationCommandExecute(object p) => true;
        private void OnCloseAppicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion



        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseAppicationCommand = new LambdaCommand(OnCloseAppicationCommandExecuted, CanCloseAppicationCommandExecute);
            CreateClientCommand = new LambdaCommand(OnCreateClientCommandExecuted, CanCreateClientCommandExecute);
            CreateEntityClientCommand = new LambdaCommand(OnCreateEntityClientCommandExecuted, CanCreateEntityClientCommandExecute);
            CreateVIPClientCommand = new LambdaCommand(OnCreateVIPClientCommandExecuted, CanCreateVIPClientCommandExecute);
            #endregion

            #region Данные

            DBClients = LoadSave<Client>.LoadDB(Path);

            #endregion

            #region Данные

            DBClients = LoadSave<Client>.LoadDB(Path);

            #endregion
        }
    }


}
