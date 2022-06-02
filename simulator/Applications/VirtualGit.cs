using System;

namespace Simulator
{
	/// Simulates the 'git' source versioning and collaboration application
	class VirtualGit : IApplication
	{
		public void Run(string verb, string[] options)
		{
			bool showHelp = false;

			// Determine the verb passed
			switch (verb)
			{
				case "status":
					System.Console.WriteLine("GIT Repo is OK!");
					break;
				case "add":
					if(options.Length == 0) {
						System.Console.WriteLine("[GIT] No files specified!");
						throw new Exception("Git error!");
					}
					System.Console.WriteLine("The following files were added to the next commit: {0}", String.Join(',', options));
					break;
				case "select":
					if(options.Length == 0) {
						System.Console.WriteLine("[GIT] No message specified!");
						throw new Exception("Git error!");
					}
					System.Console.WriteLine("new GIT commit created named {0}", options[0]);
					break;
				case "help":
					showHelp = true;
					break;
				default:
					// Show the help menu when the verb was unrecognized
					System.Console.WriteLine("Unkown command!");
					showHelp = true;
					break;
			}

			// Show the help menu if requested
			if (showHelp)
			{
				System.Console.WriteLine("VIRTUAL GIT: HELP");
				System.Console.WriteLine("(This GIT program is not real, it will not perform any actual git operations)");
				System.Console.WriteLine("Commands:");
				System.Console.WriteLine(" git status # Outputs the current status of the git-repo.");
				System.Console.WriteLine(" git add [file(s)] # Stages one or more files for the next commit.");
				System.Console.WriteLine(" git commit [message] # Creates a new GIT commit with the given message.");
				System.Console.WriteLine(" git help # Outputs.");
			}
		}
	}
}