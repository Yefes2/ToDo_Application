using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Task = Data.Entities.Task;
using TaskStatus = Data.Entities.TaskStatus;

namespace Business.Interfaces
{
    public interface ITaskService
    {
        Task GetTaskById(int id);
        IEnumerable<Task> GetTasksByListId(int listId);
        void CreateTask(int listId, string name, string description, DateTime? dueDate);
        void UpdateTask(int taskId, string name, string description, DateTime? dueDate);
        void ChangeTaskStatus(int taskId, TaskStatus status);
        void DeleteTask(int taskId);
    }
}
