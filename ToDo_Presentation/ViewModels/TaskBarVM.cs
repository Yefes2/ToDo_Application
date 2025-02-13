using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Business.Interfaces;
using Data.Entities;
using Task = Data.Entities.Task;
using GalaSoft.MvvmLight.Command;
using TaskStatus = Data.Entities.TaskStatus;

namespace ToDo_Presentation.ViewModels
{
    public class TaskBarVM : INotifyPropertyChanged
    {
        private readonly IListService _listService;
        private readonly ITaskService _taskService;
        private int _userId;

        private List _selectedList;
        public List SelectedList
        {
            get => _selectedList;
            set
            {
                _selectedList = value;
                OnPropertyChanged();
                LoadTasks();
            }
        }

        public ObservableCollection<List> UserLists { get; set; }
        public ObservableCollection<Task> TaskList { get; set; }

        private string _newTaskName;
        public string NewTaskName
        {
            get => _newTaskName;
            set { _newTaskName = value; OnPropertyChanged(); }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand CompleteTaskCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskBarVM(IListService listService, ITaskService taskService, int userId)
        {
            _listService = listService;
            _taskService = taskService;
            _userId = userId;

            UserLists = new ObservableCollection<List>(_listService.GetListsByUserId(userId));
            TaskList = new ObservableCollection<Task>();

            AddTaskCommand = new RelayCommand(AddTask);
            CompleteTaskCommand = new RelayCommand<object>(CompleteTask);
        }

        private void LoadTasks()
        {
            if (SelectedList != null)
            {
                TaskList.Clear();
                foreach (var task in _taskService.GetTasksByListId(SelectedList.Id))
                {
                    TaskList.Add(task);
                }
            }
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskName) && SelectedList != null)
            {
                _taskService.CreateTask(SelectedList.Id, NewTaskName, "", null);
                LoadTasks();
                NewTaskName = "";
            }
        }

        private void CompleteTask(object taskObj)
        {
            if (taskObj is Task task)
            {
                _taskService.ChangeTaskStatus(task.Id, TaskStatus.Completed);
                LoadTasks();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}