
namespace Museet.Models
{
   public class Art
	{
		string artTitle;
		string artDescription;
		string artAuthor;

		public Art(string artTitle, string artDescription, string artAuthor){
			
			this.artTitle = artTitle;
			this.artDescription = artDescription;
			this.artAuthor = artAuthor;
		}

		public override string ToString(){
		return "KONSTVERK => Titel: " + this.artTitle.ToString() + ", Beskrivning: " + this.artDescription.ToString() + ", Upphovsmakare: " + this.artAuthor.ToString();
		}
		public string GetArtTitle(){
			return this.artTitle;
		}
	}
}