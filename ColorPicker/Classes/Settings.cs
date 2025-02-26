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
using ColorPicker.Enums;
using LeoCorpLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace ColorPicker.Classes;

/// <summary>
/// Settings of ColorPicker
/// </summary>
public class Settings
{
	/// <summary>
	/// True if the theme of ColorPicker is set to dark.
	/// </summary>
	public bool IsDarkTheme { get; set; }

	/// <summary>
	/// The language of the app (country code). Can be _default, en-US, fr-FR...
	/// </summary>
	public string Language { get; set; }

	/// <summary>
	/// True if ColorPicker should check updates on start.
	/// </summary>
	public bool CheckUpdatesOnStart { get; set; }

	/// <summary>
	/// True if ColorPicker should show a notification to the user.
	/// </summary>
	public bool NotifyUpdates { get; set; }

	/// <summary>
	/// The seprator for an RGB color.
	/// </summary>
	public string RGBSeparator { get; set; }

	public bool? HEXUseUpperCase { get; set; }

	/// <summary>
	/// True if ColorPicker's shortcuts should be enabled.
	/// </summary>
	public bool? EnableKeyBoardShortcuts { get; set; }

	/// <summary>
	/// True if ColorPicker's theme should match system.
	/// </summary>
	public bool? IsThemeSystem { get; set; }

	/// <summary>
	/// True if ColorPicker should restore previously generated colors back to history in "Picker" page.
	/// </summary>
	public bool? RestoreColorHistory { get; set; }

	/// <summary>
	/// True if ColorPicker should restore previously generated color palettes back to history in "Palette" page.
	/// </summary>
	public bool? RestorePaletteColorHistory { get; set; }

	/// <summary>
	/// True if this is the first time ColorPicker is launched.
	/// </summary>
	public bool? IsFirstRun { get; set; }

	/// <summary>
	/// The favorite color type of the user.
	/// </summary>
	public ColorTypes? FavoriteColorType { get; set; }

	/// <summary>
	/// The keyboard shortcut used to copy a color when selecting one.
	/// </summary>
	public string CopyKeyboardShortcut { get; set; }

	/// <summary>
	/// The keyboard shortcut used to start the selection of a color.
	/// </summary>
	public string SelectKeyboardShortcut { get; set; }

	/// <summary>
	/// True if the window is pinned
	/// </summary>
	public bool? IsPinned { get; set; }

	/// <summary>
	/// The startup page of ColorPicker.
	/// </summary>
	public Enums.Pages? StartupPage { get; set; }

	/// <summary>
	/// True if ColorPicker should enable compact mode on start.
	/// </summary>
	public bool? UseCompactMode { get; set; }

	/// <summary>
	///	The default font of the Text tool.
	/// </summary>
	public string TextToolFont { get; set; }

	/// <summary>
	/// The default font size of the Text tool.
	/// </summary>
	public int? TextToolFontSize { get; set; }

	/// <summary>
	/// The default foreground color of the Text tool.
	/// </summary>
	public string TextToolFontColor { get; set; }

	/// <summary>
	/// The default background color of the Text tool.
	/// </summary>
	public string TextToolBackgroundColor { get; set; }
}

/// <summary>
/// Class that contains methods that can manage ColorPicker' settings.
/// </summary>
public static class SettingsManager
{
	/// <summary>
	/// Loads ColorPicker settings.
	/// </summary>
	public static void Load()
	{
		string path = Env.AppDataPath + @"\Léo Corporation\ColorPicker\Settings.xml"; // The path of the settings file

		if (File.Exists(path)) // If the file exist
		{
			XmlSerializer xmlSerializer = new(typeof(Settings)); // XML Serializer
			StreamReader streamReader = new(path); // Where the file is going to be read

			Global.Settings = (Settings)xmlSerializer.Deserialize(streamReader); // Read

			streamReader.Dispose();
		}
		else
		{
			Global.Settings = new Settings
			{
				IsDarkTheme = false,
				Language = "_default",
				CheckUpdatesOnStart = false,
				NotifyUpdates = true,
				RGBSeparator = ";",
				HEXUseUpperCase = false,
				EnableKeyBoardShortcuts = true,
				IsThemeSystem = true,
				RestoreColorHistory = true,
				RestorePaletteColorHistory = true,
				IsFirstRun = true,
				FavoriteColorType = ColorTypes.RGB,
				CopyKeyboardShortcut = "Shift+C",
				SelectKeyboardShortcut = "Shift+S",
				IsPinned = false,
				StartupPage = Enums.Pages.Picker,
				UseCompactMode = false,
				TextToolFont = "Arial",
				TextToolFontSize = 16,
				TextToolFontColor = "_default", // Default value depends of the theme
				TextToolBackgroundColor = "_default",
			}; // Create a new settings file

			Save(); // Save the changes
		}
	}

	/// <summary>
	/// Saves ColorPicker settings.
	/// </summary>
	public static void Save()
	{
		string path = Env.AppDataPath + @"\Léo Corporation\ColorPicker\Settings.xml"; // The path of the settings file

		XmlSerializer xmlSerializer = new(typeof(Settings)); // Create XML Serializer

		if (!Directory.Exists(Env.AppDataPath + @"\Léo Corporation\ColorPicker")) // If the directory doesn't exist
		{
			Directory.CreateDirectory(Env.AppDataPath + @"\Léo Corporation\"); // Create the directory
			Directory.CreateDirectory(Env.AppDataPath + @"\Léo Corporation\ColorPicker"); // Create the directory
		}

		StreamWriter streamWriter = new(path); // The place where the file is going to be written
		xmlSerializer.Serialize(streamWriter, Global.Settings);

		streamWriter.Dispose();
	}

	/// <summary>
	/// Exports current settings.
	/// </summary>
	/// <param name="path">The path where the settings file should be exported.</param>
	public static void Export(string path)
	{
		try
		{
			XmlSerializer xmlSerializer = new(typeof(Settings)); // Create XML Serializer

			StreamWriter streamWriter = new(path); // The place where the file is going to be written
			xmlSerializer.Serialize(streamWriter, Global.Settings);

			streamWriter.Dispose();

			MessageBox.Show(Properties.Resources.SettingsExportedSucessMsg, Properties.Resources.ColorPicker, MessageBoxButton.OK, MessageBoxImage.Information); // Show message
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Properties.Resources.ColorPicker, MessageBoxButton.OK, MessageBoxImage.Error); // Show error message
		}
	}

	/// <summary>
	/// Imports settings.
	/// </summary>
	/// <param name="path">The path to the settings file.</param>
	public static void Import(string path)
	{
		try
		{
			if (File.Exists(path)) // If the file exist
			{
				XmlSerializer xmlSerializer = new(typeof(Settings)); // XML Serializer
				StreamReader streamReader = new(path); // Where the file is going to be read

				Global.Settings = (Settings)xmlSerializer.Deserialize(streamReader); // Read

				streamReader.Dispose();
				Save(); // Save
				MessageBox.Show(Properties.Resources.SettingsImportedMsg, Properties.Resources.ColorPicker, MessageBoxButton.OK, MessageBoxImage.Information); // Show error message

				// Restart app
				Process.Start(Directory.GetCurrentDirectory() + @"\ColorPicker.exe"); // Start app
				Application.Current.Shutdown(); // Quit
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Properties.Resources.ColorPicker, MessageBoxButton.OK, MessageBoxImage.Error); // Show error message
		}
	}
}
