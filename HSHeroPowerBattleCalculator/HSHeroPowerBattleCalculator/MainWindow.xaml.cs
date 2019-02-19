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

            //After the battle comletes, refresh the heros. 
            //Otherwise, if the user brawls again without changing the combo boxes, they'll
            //be dead/nearly dead from the previous game.
            RefreshHero(ref p1);
            RefreshHero(ref p2);
        }

        void RefreshHero(ref Hero hero)
        {
            //If the user runs the simulation >1 time without changing at least one ComboBox selection,
            //the hero will be dead still from the previous run. This detects which type the hero was
            //and re-instantiates it.
            if (hero == null)
                return;

            Type heroType = hero.GetType();
            if (heroType == typeof(Druid))
            {
                hero = new Druid();
            }
            else if (heroType == typeof(Hunter))
            {
                hero = new Hunter();
            }
            else if (heroType == typeof(Mage))
            {
                hero = new Mage();
            }
            else if (heroType == typeof(Paladin))
            {
                hero = new Paladin();
            }
            else if (heroType == typeof(Priest))
            {
                hero = new Priest();
            }
            else if (heroType == typeof(Rogue))
            {
                hero = new Rogue();
            }
            else if (heroType == typeof(Shaman))
            {
                hero = new Shaman();
            }
            else if (heroType == typeof(Warlock))
            {
                hero = new Warlock();
            }
            else if (heroType == typeof(Warrior))
            {
                hero = new Warrior();
            }
            else
            {
                throw new NotSupportedException($"Unknown hero type: {heroType.ToString()}");
            }
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

            ClearOldValues(dataGrid);
            InitializeColumns(dataGrid, columnHeaders);
            InitializeRows(dataGrid, log);
        }

        void ClearOldValues(DataGrid dataGrid)
        {
            Debug.Assert(dataGrid != null);

            dataGrid.Items.Clear();
            dataGrid.Items.Refresh();
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
