namespace Simulator
{
	/// Simulates clearing the virtual console
	class VirtualClearConsole : IApplication
	{
		public void Run(string verb, string[] option)
		{
			System.Console.Clear();
		}
	}
}