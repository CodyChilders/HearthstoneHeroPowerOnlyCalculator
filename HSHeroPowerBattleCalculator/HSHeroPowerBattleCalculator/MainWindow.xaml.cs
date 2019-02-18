using HeroPowerBattleCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            Debug.Assert(sender is ComboBox);
            int newIndex = (sender as ComboBox).SelectedIndex;
            p1 = GetHeroFromCombobox(newIndex);
        }

        private void P2_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.Assert(sender is ComboBox);
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
                    throw new IndexOutOfRangeException($"{index} is not in the valid range 0-8.");
            }
        }

        private void Fight_Button_Click(object sender, RoutedEventArgs e)
        {
            Turn[] battleLog = BattleSimulation.RunBattle(p1, p2, true);
            WriteBattleLogToGrid(battleLog, dataGrid);
        }

        void WriteBattleLogToGrid(Turn[] log, DataGrid dataGrid)
        {
            Debug.Assert(log != null && log.Length > 0);
            Debug.Assert(dataGrid != null);

            //Item1 contains the variable name based on the definition of "Turn"
            //Item2 contains the  displayed value as intended to be read by the user.
            Tuple<string, string>[] columnHeaders = new Tuple<string, string>[]
            {
                new Tuple<string, string>("TurnNumber", "Turn number"),
                new Tuple<string, string>("Mana", "Mana"),
                new Tuple<string, string>("P1Health", "Player 1 health"),
                new Tuple<string, string>("P1Armor", "Player 1 armor"),
                new Tuple<string, string>("P2Health", "Player 2 health"),
                new Tuple<string, string>("P2Armor", "Player 2 armor")
            };

            InitializeColumns(dataGrid, columnHeaders);
            InitializeRows(dataGrid, log);
        }

        void InitializeRows(DataGrid dataGrid, Turn[] log)
        {
            Debug.Assert(dataGrid != null);
            Debug.Assert(log != null && log.Length > 0);

            foreach(Turn turn in log)
            {
                dataGrid.Items.Add(turn);
            }
        }

        void InitializeColumns(DataGrid dataGrid, Tuple<string, string>[] columnHeaders)
        {
            Debug.Assert(dataGrid != null);
            Debug.Assert(columnHeaders != null && columnHeaders.Length > 0);

            for (int i = 0; i < columnHeaders.Length; ++i)
            {
                DataGridTextColumn newColumn = new DataGridTextColumn();
                newColumn.Binding = new Binding(columnHeaders[i].Item1);
                newColumn.Header = columnHeaders[i].Item2;
                dataGrid.Columns.Add(newColumn);
            }
        }
    }
}
