﻿/*
MIT License

Copyright (c) Léo Corporation

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/
using ColorHelper;
using ColorPicker.Classes;
using ColorPicker.Enums;
using LeoCorpLibrary;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker.UserControls;

/// <summary>
/// Interaction logic for PaletteHistoryItem.xaml
/// </summary>
public partial class PaletteHistoryItem : UserControl
{
	internal RGB[] Colors { get; init; }
	private StackPanel ParentStackPanel { get; init; }

	readonly List<int[]> IntColors = new();
	public PaletteHistoryItem(RGB[] colors, StackPanel parent, bool addToHistory = true, List<int[]> intColors = null)
	{
		InitializeComponent();
		Colors = colors;
		ParentStackPanel = parent; // Set

		IntColors = intColors;

		if (Global.Settings.RestorePaletteColorHistory.Value && addToHistory)
		{
			if (IntColors == null)
			{
				IntColors = new();
				for (int i = 0; i < Colors.Length; i++)
				{
					IntColors.Add(new int[] { Colors[i].R, Colors[i].G, Colors[i].B });
				}
			}

			Global.ColorContentHistory.PaletteColorsRGB.Add(IntColors);
		}

		InitUI(); // Load the UI
	}

	private void InitUI()
	{
		// Borders
		ColorB1.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[0].R, Colors[0].G, Colors[0].B) }; // Set the background color
		ColorB2.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[1].R, Colors[1].G, Colors[1].B) }; // Set the background color
		ColorB3.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[2].R, Colors[2].G, Colors[2].B) }; // Set the background color
		ColorB4.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[3].R, Colors[3].G, Colors[3].B) }; // Set the background color
		ColorB5.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[4].R, Colors[4].G, Colors[4].B) }; // Set the background color
		ColorB6.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[5].R, Colors[5].G, Colors[5].B) }; // Set the background color
		ColorB7.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[6].R, Colors[6].G, Colors[6].B) }; // Set the background color
		ColorB8.Background = new SolidColorBrush { Color = Color.FromRgb(Colors[7].R, Colors[7].G, Colors[7].B) }; // Set the background color

		// Get HEX
		List<string> hexColors = new();
		for (int i = 0; i < Colors.Length; i++)
		{
			hexColors.Add(Global.Settings.HEXUseUpperCase.Value ? ColorHelper.ColorConverter.RgbToHex(Colors[i]).Value.ToUpper() : ColorHelper.ColorConverter.RgbToHex(Colors[i]).Value.ToLower());
		}

		// Tooltips
		B1ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[0].R}{Global.Settings.RGBSeparator}{Colors[0].G}{Global.Settings.RGBSeparator}{Colors[0].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[0]}"; // Set tooltip text
		B2ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[1].R}{Global.Settings.RGBSeparator}{Colors[1].G}{Global.Settings.RGBSeparator}{Colors[1].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[1]}"; // Set tooltip text
		B3ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[2].R}{Global.Settings.RGBSeparator}{Colors[2].G}{Global.Settings.RGBSeparator}{Colors[2].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[2]}"; // Set tooltip text
		B4ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[3].R}{Global.Settings.RGBSeparator}{Colors[3].G}{Global.Settings.RGBSeparator}{Colors[3].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[3]}"; // Set tooltip text
		B5ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[4].R}{Global.Settings.RGBSeparator}{Colors[4].G}{Global.Settings.RGBSeparator}{Colors[4].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[4]}"; // Set tooltip text
		B6ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[5].R}{Global.Settings.RGBSeparator}{Colors[5].G}{Global.Settings.RGBSeparator}{Colors[5].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[5]}"; // Set tooltip text
		B7ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[6].R}{Global.Settings.RGBSeparator}{Colors[6].G}{Global.Settings.RGBSeparator}{Colors[6].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[6]}"; // Set tooltip text
		B8ToolTip.Content = $"{Properties.Resources.RGB}: {Colors[7].R}{Global.Settings.RGBSeparator}{Colors[7].G}{Global.Settings.RGBSeparator}{Colors[7].B}" +
			$"\n{Properties.Resources.HEX}: #{hexColors[7]}"; // Set tooltip text
	}

	private void ColorB1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		Border border = (Border)sender; // Convert to border
		var color = ((SolidColorBrush)border.Background).Color; // Get background color
		Clipboard.SetText(Global.Settings.FavoriteColorType switch
		{
			ColorTypes.RGB => $"{color.R}{Global.Settings.RGBSeparator}{color.G}{Global.Settings.RGBSeparator}{color.B}",
			ColorTypes.HEX => "#" + (Global.Settings.HEXUseUpperCase.Value ? ColorsConverter.RGBtoHEX(color.R, color.G, color.B).Value.ToUpper() : ColorsConverter.RGBtoHEX(color.R, color.G, color.B).Value.ToLower()),
			ColorTypes.HSV => Global.GetHsvString(ColorHelper.ColorConverter.RgbToHsv(new(color.R, color.G, color.B))),
			ColorTypes.HSL => Global.GetHslString(ColorHelper.ColorConverter.RgbToHsl(new(color.R, color.G, color.B))),
			ColorTypes.CMYK => Global.GetCmykString(ColorHelper.ColorConverter.RgbToCmyk(new(color.R, color.G, color.B))),
			_ => $"{color.R}{Global.Settings.RGBSeparator}{color.G}{Global.Settings.RGBSeparator}{color.B}"
		}); // Copy
	}

	private void DeleteBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.PalettePage.SavedColorPalettes.Remove($"{Colors[7].R};{Colors[7].G};{Colors[7].B}"); // Remove from virtual history
		Global.ColorContentHistory.PaletteColorsRGB.Remove(IntColors);

		HistoryManager.Save();

		ParentStackPanel.Children.Remove(this); // Remove color palette
	}

	private void RegenerateBtn_Click(object sender, RoutedEventArgs e)
	{
		Global.PalettePage.HistoryBtn_Click(this, null); // Close history
		Global.PalettePage.ColorTypeComboBox.SelectedIndex = 0; // Set color type to RGB
		Global.PalettePage.RGBTxt.Text = $"{Colors[7].R}{Global.Settings.RGBSeparator}{Colors[7].G}{Global.Settings.RGBSeparator}{Colors[7].B}"; // Set text and regenerate
	}
}
