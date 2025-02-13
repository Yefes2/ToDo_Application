using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using Business.Interfaces;
using Task = Data.Entities.Task;
using System.Diagnostics;
using System.ComponentModel;
using TaskStatus = Data.Entities.TaskStatus;
namespace ToDo_Presentation.Views
{
    
    public partial class TaskBar : Window, INotifyPropertyChanged
    {
        public TaskBar()
        {
            TaskList = new ObservableCollection<Task>();
            InitializeComponent();
            DataContext = this;
            loadLists();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        bool IsMaximized = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        private void newListButton_Click(object sender, RoutedEventArgs e)
        {
            NewListView newListView = new NewListView();
            newListView.ShowDialog();
            loadLists();
        }

        private void loadLists()
        {
            
            ObservableCollection<List> lists = new ObservableCollection<List>();
            foreach (List list in App.ServiceProvider.GetRequiredService<IListService>().GetListsByUserId(Session.UserId))
            {
                lists.Add(list);
            }
            taskList.ItemsSource = lists;
        }

        public ObservableCollection<Task> TaskList { get; set; }

        private void taskList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var tasks = App.ServiceProvider.GetRequiredService<ITaskService>().GetTasksByListId((taskList.SelectedItem as List).Id);
            TaskList = new ObservableCollection<Data.Entities.Task>(tasks);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
        }

        private void newTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetRequiredService<ITaskService>().CreateTask((taskList.SelectedItem as List).Id, newTaskTxt.Text, newTaskDescTxt.Text, null);
            var tasks = App.ServiceProvider.GetRequiredService<ITaskService>().GetTasksByListId((taskList.SelectedItem as List).Id);
            TaskList = new ObservableCollection<Data.Entities.Task>(tasks);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Session.UserId = 0;
            Session.UserName = "";
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();

        }

        private void deleteTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetRequiredService<ITaskService>().DeleteTask((taskListView.SelectedItem as Task).Id);
            var tasks = App.ServiceProvider.GetRequiredService<ITaskService>().GetTasksByListId((taskList.SelectedItem as List).Id);
            TaskList = new ObservableCollection<Data.Entities.Task>(tasks);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));

        }

        private void statusPending_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetRequiredService<ITaskService>().ChangeTaskStatus((taskListView.SelectedItem as Task).Id, TaskStatus.Pending);
            var tasks = App.ServiceProvider.GetRequiredService<ITaskService>().GetTasksByListId((taskList.SelectedItem as List).Id);
            TaskList = new ObservableCollection<Data.Entities.Task>(tasks);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
        }

        private void statusInProgress_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetRequiredService<ITaskService>().ChangeTaskStatus((taskListView.SelectedItem as Task).Id, TaskStatus.InProgress);
            var tasks = App.ServiceProvider.GetRequiredService<ITaskService>().GetTasksByListId((taskList.SelectedItem as List).Id);
            TaskList = new ObservableCollection<Data.Entities.Task>(tasks);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
        }

        private void statusCompleted_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetRequiredService<ITaskService>().ChangeTaskStatus((taskListView.SelectedItem as Task).Id, TaskStatus.Completed);
            var tasks = App.ServiceProvider.GetRequiredService<ITaskService>().GetTasksByListId((taskList.SelectedItem as List).Id);
            TaskList = new ObservableCollection<Data.Entities.Task>(tasks);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
        }

        private void statusCancelled_Click(object sender, RoutedEventArgs e)
        {
            App.ServiceProvider.GetRequiredService<ITaskService>().ChangeTaskStatus((taskListView.SelectedItem as Task).Id, TaskStatus.Cancelled);
            var tasks = App.ServiceProvider.GetRequiredService<ITaskService>().GetTasksByListId((taskList.SelectedItem as List).Id);
            TaskList = new ObservableCollection<Data.Entities.Task>(tasks);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskList"));
        }
    }
}
