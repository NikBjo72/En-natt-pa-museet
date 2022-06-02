
using System.Collections.Generic;
using System.Linq;

namespace Museet.Models
{
   public class BuildingCollection
	{
		List <Building> buildingList;

		public BuildingCollection(){

			this.buildingList = new List<Building>();
		}

		public bool CheckIfBuildingInList(string building){
			bool exists = buildingList.Any(x => x.GetBuildingName() == building);
			return exists;
		}
		public List<Building> GetBuildingList(){
			return buildingList;
		}
		public Building GetBuildingInBuildingList(int idx){
			return buildingList[idx];
		}
		public Building GetBuildingInBuildingList(string buildingName){
			int idx = GetIndexForBuilding(buildingName);
			Building building = GetBuildingInBuildingList(idx);
			return building;
		}
		public int GetIndexForBuilding(string buildingName){
			var idx = this.buildingList.FindIndex(building => building.GetBuildingName() == buildingName);
			return idx;
		}
		public void AddBuilding( string buildingName){
			this.buildingList.Add(new Building (buildingName));
		}
		public string DeleteBuilding(string buildingName){
			int buildingIdx = GetIndexForBuilding(buildingName);
			if (buildingIdx >= 0){
			this.buildingList.RemoveAt(GetIndexForBuilding(buildingName));
			return $"[MU] \u274c Byggnaden {buildingName} är borttagen!\n";
			} else{
				return $"[MU] \u2757 Det finns ingen byggnad med namnet: {buildingName}\n";
			}
		}
		//*** Metoder för enteredBuilding ***
		public void SetEnteredTheBuildingInList(string buildingName){
			Building enteredBuilding = this.buildingList.Find(building => building.GetBuildingName() == buildingName);
			enteredBuilding.SetEnteredTheBuilding(true);
		}
		public string GetEnteredTheBuildingNameInList(){
			Building enteredBuilding = this.buildingList.Find(building => building.GetEnteredTheBuilding() == true);
			return enteredBuilding.GetBuildingName();
		}
		public bool AnyActiveBuilding(){
			return buildingList.Any(x => x.GetEnteredTheBuilding() == true);
		}
		public void SetAllBuildingsToNotActive(){
			foreach (var building in buildingList){
				building.SetEnteredTheBuilding(false);
			}
		}
	}
}