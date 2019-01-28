using HeroPowerBattleCalculator;
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

namespace HSHeroPowerBattleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Hero p1 = null;
        Hero p2 = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void P1_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int newIndex = (sender as ComboBox).SelectedIndex;
            p1 = GetHeroFromCombobox(newIndex);
        }

        private void P2_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int newIndex = (sender as ComboBox).SelectedIndex;
            p2 = GetHeroFromCombobox(newIndex);
        }

        Hero GetHeroFromCombobox(int index)
        {
            switch(index)
            {
                case 0:
                    return new Druid();
                case 1:
                    return new Hunter();
                case 2:
                    return new Mage();
                case 3:
                    return new Paladin();
                case 4:
                    return new Priest();
                case 5:
                    return new Rogue();
                case 6:
                    return new Shaman();
                case 7:
                    return new Warlock();
                case 8:
                    return new Warrior();
                default:
                    throw new IndexOutOfRangeException($"{index} is not in the valid range 0-9.");
            }
        }

        private void Fight_Button_Click(object sender, RoutedEventArgs e)
        {
            Turn[] battleLog = BattleSimulation.RunBattle(p1, p2, true);
        }
    }
}
