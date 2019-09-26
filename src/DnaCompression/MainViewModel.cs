using DnaCompression.Annotations;
using DnaCompression.Lib;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


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
        private bool _isInIdle = true;
        private int _progress;

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

        public ICommand CompressCommand { get; set; }

        public MainViewModel()
        {
            ChooseInputFile = new DelegateCommand(ob => InputFileName = ChoseFile(), CanExecuteCommands);
            ChooseOutputFile = new DelegateCommand(ob => OutputFileName = ChoseFile(), CanExecuteCommands);
            CompressCommand = new DelegateCommand(ob => Compress(), CanExecuteCommands);
        }

        private async void Compress()
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();

                IsInIdle = false;
                CommandManager.InvalidateRequerySuggested();

                var progress = new Progress<int>();
                progress.ProgressChanged += (s, e) => Progress = e;


                string[] input;

                if (InputAsText)
                    input = InputTextBox.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                else
                    input = File.ReadAllLines(InputFileName);

                await Task.Run(() =>
                {
                    var compressor = new DnaCompressor();
                    compressor.Compress(input, progress);
                });

                var output = input.Where(l => l != null).OrderBy(x=>x).ToArray();

                if (OutputAsText) OutputTextBox = string.Join(Environment.NewLine, output);
                else File.WriteAllLines(OutputFileName, output);

                sw.Stop();

                MessageBox.Show($"Input: {input.Length}, Output: {output.Length}, Elapsed: {sw.Elapsed}", 
                    "Done.", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsInIdle = true;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }



        public bool IsInIdle
        {
            get => _isInIdle;
            set
            {
                if (value == _isInIdle) return;
                _isInIdle = value;
                OnPropertyChanged();
            }
        }

        private bool CanExecuteCommands(object parameter)
        {
            return IsInIdle;
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