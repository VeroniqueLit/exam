using System.Windows;
using System.Windows.Input;
using exam.ViewModels; 

namespace exam
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition((IInputElement)sender);

            if (DataContext is MainViewModel vm)
            {
                vm.AddRectangle(position.X, position.Y);
            }
        }
    }
}