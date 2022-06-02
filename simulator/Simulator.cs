using System;
using System.Collections.Generic;

namespace Simulator
{
	/// The Virtual Console Simulator
	public class VirtualConsole
	{
		/// Keeps track of the registred applications available from the console
		private Dictionary<string, IApplication> _registry = new Dictionary<string, IApplication>();

		/// Use this to register all common, example, and standard applications for the virtual console
		public void RegisterStandardApplications() {
			RegisterApplication("logout", new VirtualLogoutConsole());
			RegisterApplication("exit", new VirtualLogoutConsole());
			RegisterApplication("git", new VirtualGit());
			RegisterApplication("clear", new VirtualClearConsole());
			RegisterApplication("c", new VirtualClearConsole());
		}

		/// Use this to register a custom application for the console
		public void RegisterApplication(string name, IApplication application)
		{
			_registry.Add(name, application);
		}

		/// Starts the console simulation
		public void Simulate()
		{
			// Header
			System.Console.Clear();
			System.Console.WriteLine("VIRTUAL CONSOLE SIMULATOR\n");

			// Repeats until the user logs out
			while (true)
			{
				// Get user input
				System.Console.Write(">>> ");
				string input = System.Console.ReadLine();

				// Parse user input into program, verb and options
				string[] args = input.Split(' ', 3);
				string program = (args.Length >= 1 ? args[0] : "").ToLower();
				string verb = (args.Length >= 2 ? args[1] : "").ToLower();
				string[] options = (args.Length >= 3 ? args[2].Split(' ') : new string[0]);

				// Execute the registred program
				if (!_registry.ContainsKey(program))
				{
					System.Console.WriteLine("[SYSTEM] Program not found!");
					System.Console.Beep();
				}
				else
				{
					try
					{
						_registry[program].Run(verb, options);
					}
					catch
					{
						// Prevents the virtual console from crashing due to any application error
						System.Console.WriteLine("[SYSTEM] Program exited unexpectedly");
						System.Console.Beep();
					}
				}
			}
		}
	}
}