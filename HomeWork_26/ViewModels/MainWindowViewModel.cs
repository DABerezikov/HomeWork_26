using HomeWork_26.Infrastructure.Commands;
using HomeWork_26.Models;
using HomeWork_26.Services;
using HomeWork_26.ViewModels.Base;
using System;
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
        private string _TypeClient = string.Empty;

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

        #region SelectedClient : int - Выбранный клиент банка
        private int _SelectedClient = -1;

        /// <summary>База клиентов</summary>
        public int SelectedClient
        {
            get => _SelectedClient;
            set => Set(ref _SelectedClient, value);
        }
        #endregion

        #region LogText : string - Текст для логирования
        private string _LogText;

        /// <summary>База клиентов</summary>
        public string LogText
        {
            get => _LogText;
            set => Set(ref _LogText, value);
        }
        #endregion

        #region Clear : void - Очистка полей
        private void Clear()
        {
            NameClient = string.Empty;
            AmountClient = "0";
            TypeClient = string.Empty;
            TypeAccount = string.Empty;
        }
        #endregion



        #region Команды

        #region CreateClientCommand
        public ICommand CreateClientCommand { get; }

        private bool CanCreateClientCommandExecute(object p)
        {
            if (NameClient!="" && TypeClient!=string.Empty)
            {
                return true;
            }
            return false;
        }
        private void OnCreateClientCommandExecuted(object p)
        {

            Client client;
            
            switch (TypeClient)
            {
                case "Юридическое лицо":
                    client = new EntityClient(NameClient, TypeAccount, double.Parse(AmountClient));
                    break;
                case "VIP клиент банка":
                    client = new VIPClient(NameClient, TypeAccount, double.Parse(AmountClient));
                    break;
                   
                default:
                    client = new Client(NameClient, TypeAccount, double.Parse(AmountClient));
                    break;
            }
            DBClients.Add(client);
            
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

        #region RemoveClientCommand
        public ICommand RemoveClientCommand { get; }

        private bool CanRemoveClientCommandExecute(object p)
        {
            if (SelectedClient!=-1&& DBClients[SelectedClient].Deposit==null && DBClients[SelectedClient].NotDeposit == null)
            {
                return true;
            }
            return false;
        }
        private void OnRemoveClientCommandExecuted(object p)
        {
            LoadSave<Client>.Log($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} удален клиент {DBClients[SelectedClient].ID}");
            DBClients.Remove(DBClients[SelectedClient]);
            LoadSave<Client>.SaveDB(Path, DBClients);
        }
        #endregion

        #region CreateAccountClientCommand
        public ICommand CreateAccountClientCommand { get; }

        private bool CanCreateAccountClientCommandExecute(object p)
        {
            if (TypeAccount != string.Empty && SelectedClient != -1 && (DBClients[SelectedClient].Deposit == null || DBClients[SelectedClient].NotDeposit == null))
            {
                switch (TypeAccount)
                {
                    case "Депозитный счет":
                        if (DBClients[SelectedClient].Deposit == null)
                        {
                            return true;
                        }
                        break;
                    case "Недепозитный счет":
                        if (DBClients[SelectedClient].NotDeposit == null)
                        {
                            return true;
                        }
                        break;
                }
                
            }
            return false;
        }
        private void OnCreateAccountClientCommandExecuted(object p)
        {
            DBClients[SelectedClient].LogAction += LoadSave<Client>.Log;
            DBClients[SelectedClient].OpenAccount(TypeAccount, double.Parse(AmountClient));
            OnPropertyChanged();
            LoadSave<Client>.SaveDB(Path, DBClients);
            Clear();
        }
        #endregion

        #region CloseAccountClientCommand
        public ICommand CloseAccountClientCommand { get; }

        private bool CanCloseAccountClientCommandExecute(object p)
        {
            if (SelectedClient != -1 && DBClients[SelectedClient].Deposit != null | DBClients[SelectedClient].NotDeposit != null)
            {
                return true;
            }
            return false;
        }
        private void OnCloseAccountClientCommandExecuted(object p)
        {
            DBClients[SelectedClient].LogAction += LoadSave<Client>.Log;
            MessageBox.Show(DBClients[SelectedClient].CloseAccount(TypeAccount));
            LoadSave<Client>.SaveDB(Path, DBClients);
            Clear();
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseAppicationCommand = new LambdaCommand(OnCloseAppicationCommandExecuted, CanCloseAppicationCommandExecute);
            CreateClientCommand = new LambdaCommand(OnCreateClientCommandExecuted, CanCreateClientCommandExecute);
            RemoveClientCommand = new LambdaCommand(OnRemoveClientCommandExecuted, CanRemoveClientCommandExecute);
            CreateAccountClientCommand = new LambdaCommand(OnCreateAccountClientCommandExecuted, CanCreateAccountClientCommandExecute);
            CloseAccountClientCommand = new LambdaCommand(OnCloseAccountClientCommandExecuted, CanCloseAccountClientCommandExecute);
            #endregion

            #region Данные

            DBClients = LoadSave<Client>.LoadDB(Path);
            

            #endregion

            
        }
    }


}
