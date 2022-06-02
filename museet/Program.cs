using Simulator;
using Museet.Models;

namespace Museet
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a virtual console that looks like the museums workstation
			var console = new VirtualConsole();

			// Make sure all standard applications are available in the virtual console
			console.RegisterStandardApplications();

			// Register the Museum application as "mu"
			console.RegisterApplication("mu", new VirtualMuseumProgram());

			// Start the virtual console simulation
			console.Simulate();
		}
	}
}
