using System;
using Simulator;
using Museet.Models;
using System.Linq;

namespace Museet
{
	internal class VirtualMuseumProgram : IApplication
	{
		BuildingCollection buildingCollection = new BuildingCollection();
		public void Run(string verb, string[] options)
		{
			bool showHelp = false;
			System.Console.Clear();

			switch (verb)
			{
				case "preset":
					AddPresetContent();
					break;

				case "select":
					//*** Går ut ur byggnad ***
					if (AnyActiveBuilding()){
					    if (options.Length != 0 && options[0] == ".."){
							buildingCollection.SetAllBuildingsToNotActive();
							System.Console.WriteLine("\u2757 [MU] Ingen byggnad är längre förvald.\n" + 
							"För att gå in i en ny byggnad skriv >>> mu select [byggnadsnamn].\n");
							break;
						}
						else if (options.Length > 0){
							System.Console.WriteLine("\u2757 [MU] Angivet kommando är inte giltigt i en byggnad.\n" +
							"Skriv >>> select .. för att gå ut ur byggnaden.");
							break;
						}
						else {
							System.Console.WriteLine("\u2757 [MU] Inget kommando angivet.\n");
							break;
						}
					}
					//*** Går in i byggnad. ***
					else if (options.Length > 0){
						if (buildingCollection.CheckIfBuildingInList(String.Join(' ', options))){
							buildingCollection.SetEnteredTheBuildingInList(String.Join(' ', options));
							System.Console.WriteLine("\nDu är nu inne i byggnaden: \u26EA {0}", String.Join(' ', options));
							break;
						}
						else if (options[0] == ".."){
							System.Console.WriteLine("\u2757 [MU] Du har ännu inte gått in i någon byggnad.\n");
							break;	
						}
						else {
							System.Console.WriteLine("\u2757 [MU] Byggnaden du ville gå in i finns inte tillagd.\n");
							break;
						}
					}

					System.Console.WriteLine("\u2757 [MU] Du har inte skrivit in något val av byggnad.\n" +
					"Skriv >>> mu list, för att lista dina byggnader.");
					break;

				case "list":
					//*** Om det finns ett kommando efter "list" ***
					if (!AnyActiveBuilding() && options.Length > 0) {
					System.Console.WriteLine("\n\u2757 [MU] Du har angivit ett kommando som inte finns." +
					"Skriv [mu help] för att se alla tillgängliga kommandon.");
					break;
					}
					else if (AnyActiveBuilding() && options.Length > 0){
						if (RoomInActiveBuilding(String.Join(' ', options))){
							System.Console.WriteLine($"{String.Join(' ', options)} rums olika konstverk är:\n");
							System.Console.WriteLine(ListAllArtsInARoom(ActiveBuildingName(), String.Join(' ', options)));
							break;
						}
						System.Console.WriteLine("\n\u2757 [MU] Du har angivit ett rum i byggnaden som inte finns\n" +
							"Använd kommandot [mu list] för att lista alla rum i byggnaden.");
							break;
					}
					//*** Listar alla rum i den aktiva byfggnaden ***
					else if (AnyActiveBuilding())
					{
						System.Console.WriteLine($"\nDina rum i byggnaden \u26EA {ActiveBuildingName()}:\n");
						Console.WriteLine(ListRoomsAndArtInActiveBuilding());
						break;
					}
					//*** Listar alla byggnader ***
					if (buildingCollection.GetBuildingList().Count < 1){
						System.Console.WriteLine("\n\u2757 [MU] Du har ännu inte lagt till några byggnader i [MU].\n" +
						"Skriv: mu add [byggnadsnamn] för att lägga till en byggnad.");
						break;
					}
					System.Console.WriteLine("\n[MU] Dina byggnader:");
					Console.WriteLine(ListAllBuildings());
					Console.WriteLine();
					break;

				case "add":
					//*** Lägger till en byggnad eller rum beroende på om byggnad är vald med (select) eller inte ***
					if(options.Length == 0) {
						if (AnyActiveBuilding()){
							System.Console.WriteLine("\u2757 [MU] Inget rum angivet.");
							break;
						}
						System.Console.WriteLine("\u2757 [MU] Ingen byggnad angiven.");
						break;
					}
					//*** Lägger till ett konstverk i ett rum i aktiv byggnad med argument (-art) ***
					else if (AnyActiveBuilding()){
						if (options[0] == "-art"){
							if (options.Length > 1){
								string addOptionsString = String.Join(' ', options.Skip(1));
                				string[] newAddOptons  = addOptionsString.Split('/');
								if (newAddOptons.Length == 4){
									string addRoomName = newAddOptons[0];
									string addArtName = newAddOptons[1];
									string addArtDescription = newAddOptons[2];
									string addArtAuthor = newAddOptons[3];
									if (RoomInActiveBuilding(addRoomName)){ //Kollar om rumsnamnet finns i byggnaden
										int roomIdx = GetIdxForRommInActiveBuilding(addRoomName);
										System.Console.WriteLine(buildingCollection.GetBuildingInBuildingList(ActiveBuildingIdx()).GetRoomInRoomList(roomIdx).AddArtInRoom(addArtName, addArtDescription, addArtAuthor));	
										break;
									}
									System.Console.WriteLine("\u2757 [MU] Rummet du angav finns inte i denna byggnad.");
									break;	
								}
								System.Console.WriteLine("\u2757 [MU] Du behöver skriva in: rumsnamn/titel/beskrivning/upphovsmakare.");
								break;
							}
						System.Console.WriteLine("\u2757 [MU] Inga objekt angivna.");
						break;
						}
						//*** Lägger till rum i aktiv byggnad  om rummet inte redan finns***
						else if (!RoomInActiveBuilding(String.Join(' ', options))){
							buildingCollection.GetBuildingInBuildingList(ActiveBuildingIdx()).AddRoomInBuliding(String.Join(' ', options));
							System.Console.WriteLine($"\u2705 [MU] Rum {String.Join(' ', options)} är tillagt i byggnad {ActiveBuildingName()}.\n");
							break;
						}
						System.Console.WriteLine("\u2757 [MU] Rummet du försöker lägga till finns redan tillagd i din byggnad.");
						break;
					}
					//*** Lägger till en ny byggnad om buggnaden inte redan finns ***
					else if (!buildingCollection.CheckIfBuildingInList(String.Join(' ', options))){
						buildingCollection.AddBuilding(String.Join(' ', options));
						System.Console.WriteLine("\u2705 [MU] Följande byggnad är tillagd: {0}\n", String.Join(' ', options));
					break;
					}
					System.Console.WriteLine("\u2757 [MU] Byggnaden du försöker lägga till finns redan tillagd.");
					break;

				case "delete":
					if(options.Length == 0) {
						if (AnyActiveBuilding()){
							System.Console.WriteLine("\u2757 [MU] Inget rum angivet.");
							break;
						}
						System.Console.WriteLine("\u2757 [MU] Ingen byggnad angiven.");
						break;
					}
					//*** Tar bort ett konstverk i ett rum i aktiv byggnad med argument (-art) ***
					else if (AnyActiveBuilding()){
						if (options[0] == "-art"){
							if (options.Length > 1){
								string deleteOptionsString = String.Join(' ', options.Skip(1));
                				string[] newAddOptons  = deleteOptionsString.Split('/');
								if (newAddOptons.Length == 2){
									string deleteRoomName = newAddOptons[0];
									string deleteArtTitle = newAddOptons[1];
									if (RoomInActiveBuilding(deleteRoomName)){ //Kollar om rumsnamnet finns i byggnaden
										int deleteRoomIdx = GetIdxForRommInActiveBuilding(deleteRoomName);
										System.Console.WriteLine(buildingCollection.GetBuildingInBuildingList(ActiveBuildingIdx()).GetRoomInRoomList(deleteRoomIdx).DeleteArtInRoom(deleteArtTitle));
										break;
									}
									System.Console.WriteLine("\u2757 [MU] Rummet du angav finns inte i denna byggnad.");
									break;	
								}
								System.Console.WriteLine("\u2757 [MU] Du behöver skriva in: rumsnamn/titel");
								break;
							}
						System.Console.WriteLine("\u2757 [MU] Inga objekt angivna.");
						break;
						}
						//*** Tar bort rum i aktiv byggnad  om rummet finns i byggnad ***
						else if (RoomInActiveBuilding(String.Join(' ', options))){
							System.Console.WriteLine(buildingCollection.GetBuildingInBuildingList(ActiveBuildingIdx()).DeleteRoomInBuliding(String.Join(' ', options)));
							break;
						}
						System.Console.WriteLine("\u2757 [MU] Rummet du försöker ta bort finns inte i din byggnad.\n");
						break;
					}

					System.Console.WriteLine(buildingCollection.DeleteBuilding(String.Join(' ', options)));
					break;

				case "help":
					showHelp = true;
					break;

				default:
					// *** Visar hjälp när verb är okänt ***
					System.Console.WriteLine("\u2757 [MU) Okänt kommando.\n");
					showHelp = true;
					break;
			}

			// *** Visar hjälp när det efterfrågas ***
			if (showHelp)
			{
				System.Console.WriteLine("*** [MU] HELP ***\n" +
				"[MU] är ett verktyg, där du kan skapa byggnader, rum och placera konstverk i rummen.\n" +
				"\n** Grundkommandon **\n" +
				">>> mu list # Listar alla byggnader i [MU]. *Du kan lägga till så många byggnader du vill*\n" +
				">>> mu add [byggnadsnamn] # Lägger till en byggnad i [MU].\n" +
				">>> mu select [byggnadsnamn] # Gör att du kliver in i en byggnad och kan hantera rum samt konstverk.\n" +
				">>> mu delete [byggnadsnamn] # Tar bort byggnad tillsammans med allt innehåll.\n" +
				
				"\n** \u26EA Kommandon i en byggnad **\n" + 
				">>> mu add [rumsnamn] # Lägger till ett rum i [select]ed byggnad.\n" +
				">>> mu delete [rumsnamn] # Tar bort rum i [select]ed byggnad. *Rummet måste var tomt på konstverk för att kunna tas bort*\n" +
				">>> mu add -art [rumsnamn/titel/beskrivning/upphovsmakare] # Lägger till ett konstverk i [select]ed byggnad och angivet rum. *Max tre konstverk i varje rum.*\n" +
				">>> mu delete -art [rumsnamn/konstverksnamn] # Tar bort ett konstverk i [select]ed byggnad och angivet rum.\n" +
				">>> mu list # Listar alla rum med sina konstverk i [select]ed byggnad.\n" +
				">>> mu list [rumsnamn] # Listar alla konsverk i angivet rum och i [select]ed byggnad.\n" +
				">>> mu select .. # Går ut ur den byggnad som du befinner dig i.\n");
			}
		}
		public string ListAllBuildings(){ //Listar alla byggnader.
			string buildings = "";
			foreach (var building in buildingCollection.GetBuildingList()){
				buildings += $"\u26EA {building.ToString()}\n";
				}
			return buildings;				
		}

		public string ListAllArtsInARoom(string buildingName, string roomName ){ //Listar alla konstverk i specifikt rum.
			string arts = "";
			foreach (var art in buildingCollection.GetBuildingInBuildingList(buildingName).GetRoomInRoomList(roomName).GetArtList()){
				arts += $"{art.ToString()}\n";
			}
			return arts;
		}
		public string ListRoomsAndArtInActiveBuilding(){ //Listar alla rum samt konstverk i aktiv byggnad.
			string roomAndArt = "";
			foreach (var room in buildingCollection.GetBuildingInBuildingList(ActiveBuildingName()).GetRoomList()){
				roomAndArt += $"RUM => {room.GetRoomName().ToString()}\n{room.ListArt()}\n";
				}
			return roomAndArt;
		}
		public string ActiveBuildingName(){ //Namnet på den aktiva byggnaden,
			return buildingCollection.GetEnteredTheBuildingNameInList();
		}
		public bool AnyActiveBuilding(){ //Finns det en aktiv byggnad.
			return buildingCollection.AnyActiveBuilding();
		}
		public int ActiveBuildingIdx(){ //Ger index på aktiv byggnad.
			return buildingCollection.GetIndexForBuilding(ActiveBuildingName());
		}
		public bool RoomInActiveBuilding(string roomName){ //Kollar om ett rumsnamn finns i den aktiva byggnaden
			return buildingCollection.GetBuildingInBuildingList(ActiveBuildingIdx()).IsRoomInRoomList(roomName);
		}
		public int GetIdxForRommInActiveBuilding(string roomName){ //Ger index på angivet rum i den aktiva byggnaden
			return buildingCollection.GetBuildingInBuildingList(ActiveBuildingIdx()).GetRoomIdx(roomName);
		}
		public void AddPresetContent(){ //Preset på uppsättning av byggnader, rum samt konstverk.
			buildingCollection.AddBuilding("Hotellet");
			buildingCollection.AddBuilding("Museet");
			buildingCollection.GetBuildingInBuildingList(0).AddRoomInBuliding("Rum 001");
			buildingCollection.GetBuildingInBuildingList(0).AddRoomInBuliding("Rum 002");
			buildingCollection.GetBuildingInBuildingList(1).AddRoomInBuliding("Blå rummet");
			buildingCollection.GetBuildingInBuildingList(1).AddRoomInBuliding("Röda rummet");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Mona Lisa", "Tavla", "Leonardo da Vinci");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Stol", "Myran", "Fritz Hansen");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Lampa", "PH Kotte pendel", "Poul Henningsen");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(1).AddArtInRoom("Säng", "Hästens", "Lars Nilssons");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(1).AddArtInRoom("Matta", "Ghom silke", "Carpet Vista");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Venus födelse", "Oljemålning", "Sandro Botticelli,");
			buildingCollection.GetBuildingInBuildingList(1).GetRoomInRoomList(0).AddArtInRoom("David", "Marmorstaty", "Michelangelo Buonarroti");
			buildingCollection.GetBuildingInBuildingList(1).GetRoomInRoomList(0).AddArtInRoom("Las Meninas (1656)", "Oljemålning", "Diego Velázquez");
			buildingCollection.GetBuildingInBuildingList(1).GetRoomInRoomList(1).AddArtInRoom("Stjärnenatt", "Oljemålning", " Vincent van Gogh ");
			buildingCollection.GetBuildingInBuildingList(1).GetRoomInRoomList(1).AddArtInRoom("Kyssen (1908)", "Målning", "Gustav Klimt");

		}
	}
}