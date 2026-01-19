using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace Lessons {
    public enum TaskStatus {
        ToDo, 
        InProgress, 
        Done
    }
    public class TaskItem {
        public string TaskName;
        public string? Description = "";
        public TaskStatus status;
        public DateTime date;
        public string GUID;

        public TaskItem(string TaskName, string Description, TaskStatus status, DateTime date, string GUID) {
            this.TaskName = TaskName;
            this.Description = Description;
            this.status = status;
            this.date = date;
            this.GUID = GUID;
        }
        public string GetTaskName() {
            return TaskName;
        }
        public TaskStatus GetStatus() {
            return status;
        }
    }
    class TaskBoard {
        private List<TaskItem> tasks;
        public TaskBoard() {
            tasks = new List<TaskItem>();
        }
        public void AddTask (string name, string description, TaskStatus status) {
            var task = new TaskItem(name, description, status, DateTime.Now, Guid.NewGuid().ToString());
            tasks.Add(task);
        }
        public void ShowAll() {
            if (tasks.Count == 0) {
                Console.WriteLine("No tasks available.");
                return;
            }
            foreach (var task in tasks) {
                Console.WriteLine($"Task: {task.GetTaskName()}, Description: {task.Description}, Status: {task.GetStatus()}, Created At: {task.date}, ID: {task.GUID}");
            }
        }
        public List<TaskItem> Filter(TaskStatus status) {
            List<TaskItem> result = tasks.Where(t => t.status == status).ToList();
            if (result.Count == 0) {
                Console.WriteLine("No tasks available.");
                return result;
            }
            foreach (var task in result) {
                Console.WriteLine($"Task: {task.GetTaskName()}, Description: {task.Description}, Status: {task.GetStatus()}, Created At: {task.date}, ID: {task.GUID}");
            }
            return result;
        }
        public void ChangeStatus(string taskIDToChange, TaskStatus newStatus) {
            var task = tasks.FirstOrDefault(t => t.GUID == taskIDToChange);
            if (task != null) {
                task.status = newStatus;
            } else {
                Console.WriteLine("Task not found.");
            }
        }
        public void Delete(string taskIDToDelete) {
            var task = tasks.FirstOrDefault(t => t.GUID == taskIDToDelete);
            if (task != null) {
                tasks.Remove(task);
            } else {
                Console.WriteLine("Task not found.");
            }
        }
    }
    class TaskMenu {
        public void Run() {
            var myTasks = new TaskBoard();

            while (true) {
                Console.WriteLine("\n1. Add Task\n2. Show All\n3. Filter by Status\n4. Change Status\n5. Delete Task\n0. Exit");
                string input = Console.ReadLine();

                switch(input) {
                    case "1":
                        Console.Write("Enter task name: ");
                        string taskName = Console.ReadLine();
                        Console.Write("Enter description for this task: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter task status (ToDo, InProgress, Done): ");
                        TaskStatus status;
                        while (!Enum.TryParse(Console.ReadLine(), out status)) {
                            Console.Write("Invalid status. Enter again (ToDo, InProgress, Done): ");
                        }
                        myTasks.AddTask(taskName, description, status);
                        break;
                    case "2": 
                        myTasks.ShowAll(); 
                        break;
                    case "3":
                        Console.Write("Enter status to filter (ToDo, InProgress, Done): ");
                        TaskStatus filterStatus;
                        while (!Enum.TryParse(Console.ReadLine(), out filterStatus)) {
                            Console.Write("Invalid status. Enter again (ToDo, InProgress, Done): ");
                        }
                        myTasks.Filter(filterStatus);
                        break;
                    case "4": 
                        Console.Write("Enter task ID to change status: ");
                        string taskIDToChange = Console.ReadLine();
                        TaskStatus newStatus;
                        Console.Write("Enter new status (ToDo, InProgress, Done): ");
                        while (!Enum.TryParse(Console.ReadLine(), out newStatus)) {
                            Console.Write("Invalid status. Enter again (ToDo, InProgress, Done): ");
                        }
                        myTasks.ChangeStatus(taskIDToChange, newStatus);
                        break;
                    case "5": 
                        string taskIDToDelete;
                        Console.Write("Enter task ID to delete: ");
                        taskIDToDelete = Console.ReadLine();
                        myTasks.Delete(taskIDToDelete); 
                        break;
                    case "0": return;
                    default: Console.WriteLine("Unknown option."); break;
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    class Program {
        static void Main (string[] args) {
            var menu = new TaskMenu();
            menu.Run();
        }
    }
}