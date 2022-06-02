
using System.Collections.Generic;
using System.Linq;

namespace Museet.Models
{   
	public class Building
	{
		string buildingName;
		bool enteredBuilding; 
		List <Room> roomList;

		public Building(string buildingName){
			
			this.roomList = new List<Room>();
			this.buildingName = buildingName;
			this.enteredBuilding = false;	
		}

		public string GetBuildingName(){
			return buildingName;
		}
		public void AddRoomInBuliding(string roomName){
			this.roomList.Add(new Room(roomName));
		}
		public string DeleteRoomInBuliding(string roomName){
			int roomIdx = GetRoomIdx(roomName);
			Room chosenRoom = GetRoomInRoomList(roomIdx);
			if (chosenRoom.RoomContainArt()){
				return $"\u2757 [MU] Rummet {roomName} innehåller konst.\n" + 
				"Ta först bort alla konstverk [>>> mu delete -art [rumsnamn/titel på konstverk], \n" + 
				"sedan går det att ta bort rummet."; 
			}else{
			this.roomList.RemoveAt(roomIdx);
			return $"\u274C [MU] Rum {roomName} är borttaget!";
			}
		}
		public int GetRoomIdx(string roomName){
			var index = this.roomList.FindIndex(room => room.GetRoomName() == roomName);
			return index;
		}
		public List<Room> GetRoomList(){
			return this.roomList;
		}
		public Room GetRoomInRoomList(int idx){
			return roomList[idx];
		}
		public Room GetRoomInRoomList(string roomName){
			int idx = GetRoomIdx(roomName);
			Room room = GetRoomInRoomList(idx);
			return room;
		}
		public bool IsRoomInRoomList(string roomName){
			bool roomInList = this.roomList.Any(room => room.GetRoomName() == roomName);
			return roomInList;
		}
		public override string ToString() {
           return buildingName;
        }

		//*** Metoder för enteredBuilding ***
		public void SetEnteredTheBuilding(bool set){
			this.enteredBuilding = set;
		}
		public bool GetEnteredTheBuilding(){
			return enteredBuilding;
		}
	}
}