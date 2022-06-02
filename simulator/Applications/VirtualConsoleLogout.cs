namespace Simulator
{
	/// Simulates logging out  / closing the virtual console
	class VirtualLogoutConsole : IApplication
	{
		public void Run(string verb, string[] option)
		{
			System.Environment.Exit(0);
		}
	}
}