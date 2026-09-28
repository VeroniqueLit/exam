using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using exam.Infrastructure;
using exam.Models;

namespace exam.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<RectangleItem> Rectangles { get; set; }
        public ICommand DeleteAllCommand { get; }

        public MainViewModel()
        {
            Rectangles = new ObservableCollection<RectangleItem>();

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            LoadFromDatabase();
            DeleteAllCommand = new RelayCommand(DeleteAll);
        }

        private readonly Random _rnd = new Random();

        public void AddRectangle(double x, double y)
        {
            double randomWidth = _rnd.Next(15, 101);
            double randomHeight = _rnd.Next(15, 101);

            string randomColor = $"#{_rnd.Next(0, 256):X2}{_rnd.Next(0, 256):X2}{_rnd.Next(0, 256):X2}";

            var newRect = new RectangleItem
            {
                X = x,
                Y = y,
                Width = randomWidth,
                Height = randomHeight,
                Color = randomColor 
            };

            Rectangles.Add(newRect);
            SaveToDatabase(newRect);
        }

        private void LoadFromDatabase()
        {
            using (var context = new AppDbContext())
            {
                var items = context.Rectangles.ToList();
                foreach (var item in items)
                {
                    Rectangles.Add(item);
                }
            }
        }

        private void SaveToDatabase(RectangleItem rect)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Rectangles.Add(rect);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void DeleteAll(object obj)
        {
            using (var context = new AppDbContext())
            {
                context.Rectangles.RemoveRange(context.Rectangles);
                context.SaveChanges();
            }
            Rectangles.Clear();
        }
    }
}