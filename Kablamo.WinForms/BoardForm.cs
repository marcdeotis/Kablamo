using Kablamo.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kablamo.WinForms
{
    public partial class BoardForm : Form
    {
        public BoardForm()
        {
            InitializeComponent();

            _gameBoard = new Board();

            FormatTableLayout();

            RefreshButtons();
        }

        private void RefreshButtons()
        {
            foreach (var button in gameButtons)
            {
                Color squareColor;

                switch (_gameBoard.BoardState[button.squareCoord])
                {
                    case SquareStatus.BlueBase:
                        squareColor = Color.Blue;
                        break;
                    case SquareStatus.DoubleBlue:
                        squareColor = Color.FromArgb(100, 100, 255);
                        break;
                    case SquareStatus.DoubleRed:
                        squareColor = Color.FromArgb(255, 100, 100);
                        break;
                    case SquareStatus.Empty:
                        squareColor = Color.White;
                        break;
                    case SquareStatus.RedBase:
                        squareColor = Color.Red;
                        break;
                    case SquareStatus.SingleBlue:
                        squareColor = Color.FromArgb(200, 200, 255);
                        break;
                    case SquareStatus.SingleRed:
                        squareColor = Color.FromArgb(255, 200, 200);
                        break;
                    case SquareStatus.Wall:
                        squareColor = Color.Black;
                        break;
                    default:
                        squareColor = Color.White;
                        break;
                }

                button.BackColor = squareColor;
            }
        }

        private void FormatTableLayout()
        {
            minX = _gameBoard.BoardState.Select(s => s.Key.XCoordinate).Min();
            maxX = _gameBoard.BoardState.Select(s => s.Key.XCoordinate).Max();
            minY = _gameBoard.BoardState.Select(s => s.Key.YCoordinate).Min();
            maxY = _gameBoard.BoardState.Select(s => s.Key.YCoordinate).Max();

            xRowCount = Math.Abs(minX - maxX) + 1;
            yColCount = Math.Abs(minY - maxY) + 1;

            gameTable.ColumnStyles.Clear();
            gameTable.RowStyles.Clear();

            gameTable.ColumnCount = yColCount;
            gameTable.RowCount = xRowCount;

            for(int count = 0; count < yColCount; count++)
            {
                gameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, yColCount / 100f));
            }

            for (int count = 0; count < xRowCount; count++)
            {
                gameTable.RowStyles.Add(new RowStyle(SizeType.Percent, xRowCount / 100f));
            }

            gameButtons = new List<GameButton>();

            foreach (var coord in _gameBoard.BoardState)
            {
                singleButton = new GameButton()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    FlatStyle = FlatStyle.Flat,
                    squareCoord = coord.Key
                };

                singleButton.Click += new System.EventHandler(this.MakeMove_Click);

                gameButtons.Add(singleButton);

                gameTable.Controls.Add(
                    singleButton,
                    coord.Key.YCoordinate,
                    coord.Key.XCoordinate);
            }
        }

        Board _gameBoard;
        int minX;
        int maxX;
        int minY;
        int maxY;
        int xRowCount;
        int yColCount;
        List<GameButton> gameButtons;
        GameButton singleButton;
        
        private void MakeMove_Click(object sender, EventArgs e)
        {
            singleButton = (GameButton)sender;

            _gameBoard.MakeMove(singleButton.squareCoord);

            RefreshButtons();

            //check for win
        }
    }
}
