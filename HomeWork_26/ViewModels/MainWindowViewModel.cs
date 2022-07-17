using HomeWork_26.Infrastructure.Commands;
using HomeWork_26.Models;
using HomeWork_26.Services;
using HomeWork_26.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace HomeWork_26.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private const string Path = "_DB.json";

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
        private string _TypeAccount = "Недепозитный счет";

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

        #region DepositAmountClient : string - Сумма на депозитном счете клиента
        private string? _DepositAmountClient;

        public string? DepositAmountClient
        {
            get => _DepositAmountClient;
            set => Set(ref _DepositAmountClient, value);
        }

        /// <summary> Сумма на депозитном счете клиента</summary>
        private string? GetDepositAmountClient(int selectedClient)
        {
            if (selectedClient == -1)
            {
                return "Нет счета";
            }
            return DbClients?[selectedClient].Deposit!=null?DbClients[selectedClient].Deposit?.Amount.ToString(CultureInfo.InvariantCulture):"Нет счета";
        }
        #endregion

        #region NotDepositAmountClient : string - Сумма на недепозитном счете клиента


        private string? _NotDepositAmountClient;

        public string? NotDepositAmountClient
        {
            get => _NotDepositAmountClient;
            set => Set(ref _NotDepositAmountClient, GetNotDepositAmountClient(SelectedClient));
        }

        /// <summary> Сумма на депозитном счете клиента</summary>
        private string? GetNotDepositAmountClient(int selectedClient)
        {
            if (selectedClient == -1)
            {
                return "Нет счета";
            }
            return DbClients?[selectedClient].NotDeposit != null ? DbClients[selectedClient].NotDeposit?.Amount.ToString(CultureInfo.InvariantCulture) : "Нет счета";
        }
        #endregion    


        #region DBClients : ObservableCollection - База данных клиентов банка
        private ObservableCollection<Client>? _DbClients;

        /// <summary>База клиентов</summary>
        public ObservableCollection<Client>? DbClients
        {
            get => _DbClients;
            private set => Set(ref _DbClients, value);
        }
        #endregion
        
        #region SelectedClient : int - Выбранный клиент банка
        private int _SelectedClient = -1;

        /// <summary>База клиентов</summary>
        public int SelectedClient
        {
            get => _SelectedClient;
            set
            {
                Set(ref _SelectedClient, value);
                DepositAmountClient = GetDepositAmountClient(SelectedClient);
                NotDepositAmountClient = GetNotDepositAmountClient(SelectedClient);

            }
        }
        #endregion

        #region LogText : string - Текст для логирования
        private string _LogText;

        /// <summary>База клиентов</summary>
        public string LogText
        {
            get => _LogText;
            set
            {
                Set(ref _LogText, value);
                Status = LogText;

            } 
        }
        #endregion

        #region Clear : void - Очистка полей
        private void Clear()
        {
            NameClient = string.Empty;
            AmountClient = "0";
            TypeClient = string.Empty;
            switch (TypeAccount)
            {
                case "Депозитный счет":
                    DepositAmountClient = GetDepositAmountClient(SelectedClient);
                    break;
                case "Недепозитный счет":
                    NotDepositAmountClient = GetNotDepositAmountClient(SelectedClient);
                    break;
            }
            TypeAccount = "Депозитный счет";
        }
        #endregion



        #region Команды

        #region CreateClientCommand
        public ICommand CreateClientCommand { get; }

        private bool CanCreateClientCommandExecute(object p)
        {
            return NameClient!="" && TypeClient!=string.Empty;
        }
        private void OnCreateClientCommandExecuted(object p)
        {
            var client = TypeClient switch
            {
                "Юридическое лицо" => new EntityClient(NameClient, TypeAccount, double.Parse(AmountClient)),
                "VIP клиент банка" => new VipClient(NameClient, TypeAccount, double.Parse(AmountClient)),
                _ => new Client(NameClient, TypeAccount, double.Parse(AmountClient))
            };
            DbClients?.Add(client);
            
            LoadSave<Client>.SaveDb(Path, DbClients);
            Clear();
        }
        #endregion

       
        #region CloseAppicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region RemoveClientCommand
        public ICommand RemoveClientCommand { get; }

        private bool CanRemoveClientCommandExecute(object p)
        {
            return SelectedClient != -1 && DbClients?[SelectedClient].Deposit==null && DbClients?[SelectedClient].NotDeposit == null;
        }
        private void OnRemoveClientCommandExecuted(object p)
        {
            if (DbClients != null)
            {
                LogText =
                    $"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} удален клиент {DbClients[SelectedClient].Id}";
                LoadSave<Client>.Log(LogText);
                DbClients.Remove(DbClients[SelectedClient]);
                LoadSave<Client>.SaveDb(Path, DbClients);
            }

            Clear();
        }
        #endregion

        #region CreateAccountClientCommand
        public ICommand CreateAccountClientCommand { get; }

        private bool CanCreateAccountClientCommandExecute(object p)
        {
            if (TypeAccount == string.Empty || SelectedClient == -1 || (DbClients?[SelectedClient].Deposit != null &&
                                                                        DbClients[SelectedClient].NotDeposit != null))
                return false;
            switch (TypeAccount)
            {
                case "Депозитный счет":
                    if (DbClients?[SelectedClient].Deposit == null)
                    {
                        return true;
                    }
                    break;
                case "Недепозитный счет":
                    if (DbClients?[SelectedClient].NotDeposit == null)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
        private void OnCreateAccountClientCommandExecuted(object p)
        {
            if (DbClients != null)
            {
                DbClients[SelectedClient].LogAction += LoadSave<Client>.Log;
                DbClients[SelectedClient].OpenAccount(TypeAccount, double.Parse(AmountClient));
                LoadSave<Client>.SaveDb(Path, DbClients);
            }

            Clear();
        }
        #endregion

        #region CloseAccountClientCommand
        public ICommand CloseAccountClientCommand { get; }

        private bool CanCloseAccountClientCommandExecute(object p)
        {
            return SelectedClient != -1 && DbClients?[SelectedClient].Deposit != null | DbClients?[SelectedClient].NotDeposit != null;
        }
        private void OnCloseAccountClientCommandExecuted(object p)
        {
            if (DbClients != null)
            {
                DbClients[SelectedClient].LogAction += LoadSave<Client>.Log;
                MessageBox.Show(DbClients[SelectedClient].CloseAccount(TypeAccount));
                DbClients[SelectedClient].CloseAccount(TypeAccount);
                LoadSave<Client>.SaveDb(Path, DbClients);
            }

            Clear();
        }
        #endregion

       

        #endregion

        public MainWindowViewModel()
        {
            

            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            CreateClientCommand = new LambdaCommand(OnCreateClientCommandExecuted, CanCreateClientCommandExecute);
            RemoveClientCommand = new LambdaCommand(OnRemoveClientCommandExecuted, CanRemoveClientCommandExecute);
            CreateAccountClientCommand = new LambdaCommand(OnCreateAccountClientCommandExecuted, CanCreateAccountClientCommandExecute);
            CloseAccountClientCommand = new LambdaCommand(OnCloseAccountClientCommandExecuted, CanCloseAccountClientCommandExecute);
            #endregion

            #region Данные

            DbClients = LoadSave<Client>.LoadDb(Path);
            

            #endregion

            
        }

       
    }


}
