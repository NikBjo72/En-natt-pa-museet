using System.Collections.Generic;
using System.Linq;

namespace Museet.Models
{
	public class Room
	{
		string roomName;
		int maxAmountArtWorks;
		List <Art> artList;

		public Room(string roomName){
			
			this.roomName = roomName;
			this.maxAmountArtWorks = 3;
			this.artList = new List <Art>();
		}

		public string AddArtInRoom(string artTitle, string artDescription, string artAuthor){

			if (ArtWithTheSameName(artTitle)){
				return "\u2757 [MU] Utställningsobjektet finns redan i rummet.\n";
			}
			else if (!MaxAmountOfArtWorkReached()){
				this.artList.Add(new Art(artTitle, artDescription, artAuthor));
				return "\u2705 [MU] Utställningsobjektet är tillagt i rummet\n";
			}
			else return "\u2757 [MU] Du har uppnått maxgränsen för utställningsobjekt i rummet.\n" +
				"För att lägga till ett nytt utställningsobjekt behöver du ta bort något av de befintliga.";
		}
		public string DeleteArtInRoom(string artTitle){

			if (!ArtWithTheSameName(artTitle)){
				return "\u2757 [MU] Utställningsobjektet finns inte i rummet.\n";
			}
			int artIdx = GetArtIdx(artTitle);
			this.artList.RemoveAt(artIdx);
			return $"\u274C [MU] Utställningsobjektet {artTitle} är borttaget ur rummet\n";
		}
		public int GetArtIdx(string artTitle){
			var index = this.artList.FindIndex(room => room.GetArtTitle() == artTitle);
			return index;
		}
		public string GetRoomName(){
			return this.roomName;
		}
		public string ListArt(){
			string artWorkInfo = "";
			foreach (var artWork in artList){
				artWorkInfo += artWork.ToString() + "\n";
				}
			return artWorkInfo;
		}					
		public List<Art> GetArtList(){
			return this.artList;
		}
		public bool MaxAmountOfArtWorkReached(){
			if (this.artList.Count == this.maxAmountArtWorks){
				return true;
			} else return false;
		}
		public bool RoomContainArt(){
			if (artList.Count > 0){
				return true;
			}else{
				return false;
			}
		}
		public bool ArtWithTheSameName(string artName){
			return artList.Any(art => art.GetArtTitle() == artName);
		}
	}
}
