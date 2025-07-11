using Laverna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laverna.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public bool Flag;
        public int SelectedEquipmentId;
        public Add(Equipments SelectedEquipment, bool flag)
        {
            InitializeComponent();
            Bindings();
            if (SelectedEquipment != null)
            {
                EditEquipment(SelectedEquipment);
                SelectedEquipmentId = SelectedEquipment.Id;
            }
            Flag = flag;
        }

        // Метод нужен для того, чтобы просто подтянуть данные выбранного оборудования
        private void EditEquipment(Equipments SelectedEquipment)
        {
            if(SelectedEquipment != null)
            {
                TBName.Text = SelectedEquipment.Name;
                CBType.SelectedIndex = SelectedEquipment.TypeId - 1;
                CBStatus.SelectedIndex = SelectedEquipment.StatusId - 1;
                BTSave.Content = "Сохранить";
            }
            
        }

        

        private void Bindings()
        {
            CBType.ItemsSource = App.db.Types.ToList();
            CBStatus.ItemsSource = App.db.Status.ToList();
        }

        private void BTBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BTSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Flag)
            {
                var TextUser = TBName.Text;
                int SelectedTypeId = 0;
                int SelectedStatusId = 0;

                if (CBType.SelectedItem is Types Types)
                {
                    SelectedTypeId = Types.Id;
                }

                if (CBStatus.SelectedItem is Status Status)
                {
                    SelectedStatusId = Status.Id;
                }

                if (TBName.Text == "" || CBType.SelectedItem == null || CBStatus.SelectedItem == null)
                {
                    return;
                }
                // Получили нужные нам атрибуты и добавили коллекцию в БД
                var equipment = new Equipments();
                equipment.Name = TextUser;
                equipment.TypeId = SelectedTypeId;
                equipment.StatusId = SelectedStatusId;
                App.db.Equipments.Add(equipment);
                App.db.SaveChanges();
                NavigationService.Navigate(new Pages.Main());
            }

            else
            {
                var EdittedCollection = App.db.Equipments.FirstOrDefault(p => p.Id == SelectedEquipmentId);
                EdittedCollection.Name = TBName.Text;
                EdittedCollection.TypeId = CBType.SelectedIndex + 1;
                EdittedCollection.StatusId = CBStatus.SelectedIndex + 1;
                App.db.SaveChanges();
                NavigationService.Navigate(new Pages.Main());
            }

        }
    }
}
