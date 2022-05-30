using HomeWork_26.Infrastructure.Commands;
using HomeWork_26.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace HomeWork_26.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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

        #region Команды

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

            #endregion

        }
    }


}
