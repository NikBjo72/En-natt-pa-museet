
namespace Simulator
{
	/// Defines what an virtual console application needs to implement
	public interface IApplication
	{
		// An application needs to be able to "run" with a given verb and option, will be empty strings if not given
		void Run(string verb, string[] option);
	}
}