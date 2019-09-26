using System;
using DnaCompression.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Win32;


namespace DnaCompression
{

    public class MainViewModel : BaseViewModel
    {
        private string _inputTextBox;
        private string _outputTextBox;
        private string _inputFileName;
        private string _outputFileName;
        private bool _inputAsText = true;
        private bool _outputAsText = true;

        public string InputTextBox
        {
            get => _inputTextBox;
            set
            {
                _inputTextBox = value;
                OnPropertyChanged();
            }
        }

        public string OutputTextBox
        {
            get => _outputTextBox;
            set
            {
                _outputTextBox = value;
                OnPropertyChanged();
            }
        }

        public string InputFileName
        {
            get => _inputFileName;
            set
            {
                _inputFileName = value;
                OnPropertyChanged();
            }
        }

        public string OutputFileName
        {
            get => _outputFileName;
            set
            {
                _outputFileName = value;
                OnPropertyChanged();
            }
        }

        public bool InputAsText
        {
            get => _inputAsText;
            set
            {
                _inputAsText = value;
                OnPropertyChanged();
            }
        }

        public bool OutputAsText
        {
            get => _outputAsText;
            set
            {
                _outputAsText = value;
                OnPropertyChanged();
            }
        }


        public ICommand ChooseInputFile { get; set; }
        public ICommand ChooseOutputFile { get; set; }

        public MainViewModel()
        {
            ChooseInputFile = new DelegateCommand( ob=>InputFileName = ChoseFile(), CanExecuteCommands);
            ChooseOutputFile = new DelegateCommand(ob => OutputFileName = ChoseFile(), CanExecuteCommands);
        }

        private bool CanExecuteCommands(object parameter)
        {
            return true;
        }

        private static string ChoseFile()
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }

            return null;
        }
    }


    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }


    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}