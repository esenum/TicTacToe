using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


// Change the line 24 & 148 to manage the square of the game [X by X]


namespace TicTacToeForm
{
	public partial class TicTacToeForm : Form
	{
		public TicTacToeForm() 
		{
			InitializeComponent();
			GenerateButtons();
		}

		static int n = 3;
		Button[,] buttons = new Button[n, n];

		private void GenerateButtons()
		{
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					buttons[i, j] = new Button();
					buttons[i, j].Size = new Size(100, 100);
					buttons[i, j].Location = new Point(i * 100, j * 100);
					buttons[i, j].FlatStyle = FlatStyle.Flat;
					buttons[i, j].Font = new System.Drawing.Font(DefaultFont.FontFamily, 60, FontStyle.Bold);

					// Define button click event
					buttons[i, j].Click += new EventHandler(button_Click);

					// Add button in to the panel
					panel1.Controls.Add(buttons[i, j]);
				}			 
			}
		}
		
		void button_Click(object sender, EventArgs e)
		{
			// Load the clicked button into a local variable
			Button button = sender as Button;

			// Don't do anything if the block is already marked
			if (button.Text != "")
			{
				return;
			}

			// Mark the block with current players icon
			button.Text = PlayerButton.Text;

			TogglePlayer();
		}

		private void TogglePlayer()
		{
			CheckIfGameEnds();

			if (PlayerButton.Text == "X")
			{
				PlayerButton.Text = "O";
			}
			else
			{
				PlayerButton.Text = "X";
			} 
		}
		
		private void CheckIfGameEnds()
		{
			List<Button> winnerButtons = new List<Button>();
			
			#region // vertically
			for (int i = 0; i < n; i++)
			{
				winnerButtons = new List<Button>();
				for (int j = 0; j < n; j++)
				{
					if (buttons[i, j].Text != PlayerButton.Text)
					{
						break;
					}

					winnerButtons.Add(buttons[i, j]);
					if (j == n-1)
					{
						ShowWinner(winnerButtons);
						return;
					}
				}
			}
			#endregion     
			

			#region // horizontally
			for (int i = 0; i < n; i++)
			{
				winnerButtons = new List<Button>();
				for (int j = 0; j < n; j++)
				{
					if (buttons[j, i].Text != PlayerButton.Text)
					{
						break;
					}

					winnerButtons.Add(buttons[j, i]);
					if (j == n-1)
					{
						ShowWinner(winnerButtons);
						return;
					}
				}
			}
			#endregion    
			

			#region// diagonally 1 (top-left to bottom-right)
			winnerButtons = new List<Button>();
			for (int i = 0, j = 0; i < n; i++, j++)
			{
				if (buttons[i, j].Text != PlayerButton.Text)
				{
					break;
				}

				winnerButtons.Add(buttons[i, j]);
				if (j == n-1)
				{
					ShowWinner(winnerButtons);
					return;
				}
			}
			#endregion


			#region// diagonally 2 (bottom-left to top-right)
			winnerButtons = new List<Button>();
			for (int i = 2, j = 0; j < n; i--, j++)
			{
				if (buttons[i, j].Text != PlayerButton.Text)
				{
					break;
				}

				winnerButtons.Add(buttons[i, j]);
				if (j == n-1)
				{
					ShowWinner(winnerButtons);
					return;   
				}
			}
			#endregion

			// check if all the blocks are marked
			foreach (var button in buttons)
			{
				if (button.Text == "")
					return;
			}

			MessageBox.Show("Game Draw");
			Application.Restart();
		}

		private void ShowWinner(List<Button> winnerButtons)
		{
			// color all the winner blocks
			foreach (var button in winnerButtons)
			{
				button.BackColor = Color.YellowGreen;
			}
			MessageBox.Show("Player " + PlayerButton.Text + " wins");
			Application.Restart();
		}
	}
}
