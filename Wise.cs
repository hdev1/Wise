using System;
using System.Collections.Generic;

namespace Wise
{
	public enum MenuOrientation {
		Vertical,
		Horizontal
	}

	public class MenuController {
		public Menu[] Menus { get; set; }

		// Keys
		public ConsoleKey AbortKey;
		public ConsoleKey UpKey;
		public ConsoleKey DownKey;
		public ConsoleKey LeftKey;
		public ConsoleKey RightKey;

		// Prompt
		private bool Prompting = true;

		// Initialization
		public MenuController(Menu[] menus = null) {
			if (menus != null) {
				Menus = menus;
				foreach (Menu menu in Menus) menu.MenuController = this;
			}
		}

		// Unprompt
		public void Stop() {
			Prompting = false;
		}

		// Prompt
		public void Prompt(string name = null) {
			Prompting = true;
			if (name == null) name = Menus[0].Name;
			foreach (Menu menu in Menus) if (menu.Name == name) menu.Prompt();
		}

		// Draw functions
		public void DrawAll() {
			foreach (Menu menu in Menus) menu.Draw();
		}

		public void DrawByName(string name) {
			foreach (Menu menu in Menus) if (menu.Name == name) menu.Draw();
		}
	}

	public class Menu {
		// Main controlelr
		public MenuController MenuController;

		// Display
		public MenuOrientation MenuOrientation = MenuOrientation.Vertical;
		
		// Contents
		public List<string> Items { get; set; }
		public Action ActionHandler { get; set; }
		
		// Measurements
		public int Width { get; set; }
		public int Height { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		// Misc
		public string Name;

		// View
		private int ViewRangeStart;
		private int ViewRangeEnd;

		// Selection
		public bool Prompting = false;
		private int SelectedIndex;

		// Initialization
 		public Menu(MenuController menuController = null) {
 			if (menuController != null) MenuController = menuController;
		}

		// Draw menu
		public void Draw() {
			if (MenuController == null) throw new ArgumentNullException("MenuController");

			// Display words
			int itemIndex = 0;

			// View range end
			ViewRangeEnd = ViewRangeStart + Height > Items.Count ? Items.Count : ViewRangeStart + Height;

			// Draw
			foreach (string item in Items.GetRange(ViewRangeStart, ViewRangeEnd)) { /* Get from e.g. 1 to 1 + h = 8 */
				Console.SetCursorPosition(X, Y + itemIndex);
				itemIndex++;

				// Check width, add ellipse
				if (item.Length > Width) {
					Console.WriteLine(item.Substring(0, item.Length - 3) + "...");
					continue;
				}
				Console.WriteLine(item);

			}
		}

		// Prompt
		public void Prompt() {
			ConsoleKeyInfo key;

			do {
				// Read input
				key = Console.ReadKey(true);
				if (key.Key == MenuController.UpKey) {
					Console.WriteLine("up");
				} else if (key.Key == MenuController.DownKey) {

				} else if (key.Key == MenuController.RightKey) {

				} else if (key.Key == MenuController.LeftKey) {

				}
			} while (MenuController.AbortKey != key.Key && Prompting == true);
		}
	}

	public class Test {
		public static void Main(string[] args) {
			Menu b = new Menu() {
				X = 5,
				Y = 5,
				Width = 8,
				Height = 6,
				Items = new List<string> { "test", "reallylongtest", "ssffffffffff", "reallylongtest", "reallylongtest", "reallylongtest", "reallylongtest", "reallylongtest" }
			};
			MenuController a = new MenuController( new Menu[] { b } );
			a.DrawAll();
			a.Prompt();
		}
	}
}
