using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CellularAutomaton
{
    public partial class MainWindow : Window
    {
        private const int RowCount = 20;
        private const int ColumnCount = 20;
        private readonly int[,] _state = new int[RowCount, ColumnCount];

        public MainWindow()
        {
            InitializeComponent();
            InitializeGrid();
            UpdateView();
        }

        private void InitializeGrid()
        {
            CellsGrid.Rows = RowCount;
            CellsGrid.Columns = ColumnCount;
            CellsGrid.Children.Clear();

            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    var button = new Button
                    {
                        Margin = new Thickness(1),
                        Tag = new CellPosition(row, column)
                    };
                    button.Click += CellClick;
                    CellsGrid.Children.Add(button);
                }
            }
        }

        private void CellClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is CellPosition position)
            {
                int row = position.Row;
                int column = position.Column;
                _state[row, column] = (_state[row, column] + 1) % 4;
                UpdateButtonColor(button, _state[row, column]);
            }
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    _state[row, column] = 0;
                }
            }

            UpdateView();
        }

        private void StepClick(object sender, RoutedEventArgs e)
        {
            int[,] nextState = new int[RowCount, ColumnCount];

            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    nextState[row, column] = GetNextState(row, column);
                }
            }

            Array.Copy(nextState, _state, nextState.Length);
            UpdateView();
        }

        private int GetNextState(int row, int column)
        {
            int current = _state[row, column];

            int countState1 = 0;
            int countState2 = 0;
            int countState3 = 0;

            void CountNeighbor(int r, int c)
            {
                if (r < 0 || r >= RowCount || c < 0 || c >= ColumnCount)
                {
                    return;
                }

                int value = _state[r, c];
                if (value == 1)
                {
                    countState1++;
                }
                else if (value == 2)
                {
                    countState2++;
                }
                else if (value == 3)
                {
                    countState3++;
                }
            }

            CountNeighbor(row - 1, column);
            CountNeighbor(row + 1, column);
            CountNeighbor(row, column - 1);
            CountNeighbor(row, column + 1);

            if (current == 0)
            {
                if (countState1 == 1)
                {
                    return 2;
                }

                if (countState2 == 1)
                {
                    return 3;
                }

                if (countState3 == 4)
                {
                    return 3;
                }

                return 0;
            }

            if (current == 1)
            {
                if (countState3 >= 1)
                {
                    return 3;
                }

                return 1;
            }

            if (current == 2)
            {
                if (countState3 >= 1)
                {
                    return 3;
                }

                return 2;
            }

            return 3;
        }

        private void UpdateView()
        {
            int index = 0;
            foreach (UIElement element in CellsGrid.Children)
            {
                if (element is Button button)
                {
                    int row = index / ColumnCount;
                    int column = index % ColumnCount;
                    UpdateButtonColor(button, _state[row, column]);
                    index++;
                }
            }
        }

        private static void UpdateButtonColor(Button button, int state)
        {
            Color color = Colors.White;

            if (state == 1)
            {
                color = Colors.Red;
            }
            else if (state == 2)
            {
                color = Colors.Green;
            }
            else if (state == 3)
            {
                color = Colors.Blue;
            }

            button.Background = new SolidColorBrush(color);
        }

        private sealed class CellPosition
        {
            public CellPosition(int row, int column)
            {
                Row = row;
                Column = column;
            }

            public int Row { get; }
            public int Column { get; }
        }
    }
}
