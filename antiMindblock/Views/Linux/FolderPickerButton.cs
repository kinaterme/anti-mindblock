using Avalonia.Controls;

using antiMindblock.Views;

namespace antiMindblock.LinuxOS
{
    public class linuxFolderPickerButton
    {
        private MainWindow _mainWindow;

        public linuxFolderPickerButton(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        public async void FolderPickerButton()
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Select a folder"
            };

            string result = await dialog.ShowAsync(_mainWindow);

            if (!string.IsNullOrEmpty(result))
            {
                _mainWindow.selectedFolderPath = result;

                _mainWindow.FolderPathTextBlock.Text = $"Selected Folder: {_mainWindow.selectedFolderPath}";
                _mainWindow.FolderPathTextBlockMisc.Text = $"Selected Folder: {_mainWindow.selectedFolderPath}";
            }
            else
            {
                _mainWindow.FolderPathTextBlock.Text = "No skin selected.";
                _mainWindow.FolderPathTextBlockMisc.Text = "No skin selected.";
            }
        }
    }
}