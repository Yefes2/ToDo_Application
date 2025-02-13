using Business.Interfaces;
using Data.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using Task = Data.Entities.Task;
using TaskStatus = Data.Entities.TaskStatus;

namespace Business.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task GetTaskById(int id) => _taskRepository.GetById(id);

        public IEnumerable<Task> GetTasksByListId(int listId) => _taskRepository.GetByListId(listId);

        public void CreateTask(int listId, string name, string description, DateTime? dueDate)
        {
            var newTask = new Task
            {
                Name = name,
                Description = description,
                DueDate = dueDate,
                ListId = listId,
                Status = TaskStatus.Pending
            };
            _taskRepository.Add(newTask);
        }

        public void UpdateTask(int taskId, string name, string description, DateTime? dueDate)
        {
            var task = _taskRepository.GetById(taskId);
            if (task != null)
            {
                task.Name = name;
                task.Description = description;
                task.DueDate = dueDate;
                _taskRepository.Update(task);
            }
        }

        public void ChangeTaskStatus(int taskId, TaskStatus status)
        {
            var task = _taskRepository.GetById(taskId);
            if (task != null)
            {
                task.Status = status;
                _taskRepository.Update(task);
            }
        }

        public void DeleteTask(int taskId)
        {
            _taskRepository.Delete(taskId);
        }
    }
}
