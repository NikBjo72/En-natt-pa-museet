using System;
using Xunit;
using Museet.Models;

namespace tests
{
    public class VirtualMuseumTest
    {
        [Fact]
        public void NoMoreArtCanBeAddedInARoom()
        {
            // Arrange
            BuildingCollection buildingCollection = new BuildingCollection();
            buildingCollection.AddBuilding("Naturhistoriska museet");
            string expectedAnswer = "\u2757 [MU] Du har uppnått maxgränsen för utställningsobjekt i rummet.\n" +
				"För att lägga till ett nytt utställningsobjekt behöver du ta bort något av de befintliga.";
            int expectedAmountOfArt = 3;
            string redRoom = "Röda rummet";

            string artTitleRed = "En hund bakom ratten";
            string artDescriptionRed ="En bild som måste upplevas för att förstås";
            string artAuthorRed = "Lyret";

            string artTitleBlue = "En katt bakom ratten";
            string artDescriptionBlue ="En bild som måste ses för att förstås";
            string artAuthorBlue = "Hipp Hopp";

            string artTitleGreen = "En mus bakom ratten";
            string artDescriptionGreen ="En bild som måste begrundas för att förstås";
            string artAuthorGreen = "Musse pigg";

            string artTitleYellow = "En anka bakom ratten";
            string artDescriptionYellow ="En bild som måste gå förlorad för att bli värdefull";
            string artAuthoryellow = "Kalle Anka";

            // Act
            Building naturhistoriskaMuseet = buildingCollection.GetBuildingInBuildingList("Naturhistoriska museet");
            naturhistoriskaMuseet.AddRoomInBuliding(redRoom);
            naturhistoriskaMuseet.GetRoomList()[naturhistoriskaMuseet.GetRoomIdx(redRoom)].AddArtInRoom(artTitleRed, artDescriptionRed, artAuthorRed);
            naturhistoriskaMuseet.GetRoomList()[naturhistoriskaMuseet.GetRoomIdx(redRoom)].AddArtInRoom(artTitleBlue, artDescriptionBlue, artAuthorBlue);
            naturhistoriskaMuseet.GetRoomList()[naturhistoriskaMuseet.GetRoomIdx(redRoom)].AddArtInRoom(artTitleGreen, artDescriptionGreen, artAuthorGreen);
            string noMoreArt = naturhistoriskaMuseet.GetRoomList()[naturhistoriskaMuseet.GetRoomIdx(redRoom)].AddArtInRoom(artTitleYellow, artDescriptionYellow, artAuthoryellow);
            int amountOfArt = buildingCollection.GetBuildingInBuildingList("Naturhistoriska museet").GetRoomInRoomList("Röda rummet").GetArtList().Count;
            // Assert
            Assert.Equal(expectedAnswer, noMoreArt);
            Assert.Equal(expectedAmountOfArt, amountOfArt);

        }

        [Fact]
        public void NotAbleToEraseRoomWithArt()
        {
            // Arrange
            BuildingCollection buildingCollection = new BuildingCollection();
            buildingCollection.AddBuilding("Naturhistoriska museet");
            string redRoom = "Röda rummet";
            string artTitleRed = "En hund bakom ratten";
            string artDescriptionRed ="En bild som måste upplevas för att förstås";
            string artAuthorRed = "Lyret";
            string expectedAnswer = $"\u2757 [MU] Rummet {redRoom} innehåller konst.\n" + 
				"Ta först bort alla konstverk [>>> mu delete -art [rumsnamn/titel på konstverk], \n" + 
				"sedan går det att ta bort rummet.";
            int expectedAmoutOfArtInRoom = 1;

            // Act
            Building naturhistoriskaMuseet = buildingCollection.GetBuildingInBuildingList("Naturhistoriska museet");
            naturhistoriskaMuseet.AddRoomInBuliding(redRoom);
            naturhistoriskaMuseet.GetRoomList()[naturhistoriskaMuseet.GetRoomIdx(redRoom)].AddArtInRoom(artTitleRed, artDescriptionRed, artAuthorRed);
            string answer = buildingCollection.GetBuildingInBuildingList(0).DeleteRoomInBuliding(redRoom);
            int amoutOfArtInRoom = buildingCollection.GetBuildingInBuildingList("Naturhistoriska museet").GetRoomInRoomList("Röda rummet").GetArtList().Count;

            // Assert
            Assert.Equal(expectedAnswer, answer);
            Assert.Equal(expectedAmoutOfArtInRoom, amoutOfArtInRoom);
        }

        [Fact]
        public void CountAndCompareBulidingsRoomsAndArt()
        {
            // Arrange;
            BuildingCollection buildingCollection = new BuildingCollection();
            buildingCollection.AddBuilding("Hotellet");
			buildingCollection.AddBuilding("Museet");
			buildingCollection.GetBuildingInBuildingList(0).AddRoomInBuliding("Rum 001");
			buildingCollection.GetBuildingInBuildingList(0).AddRoomInBuliding("Rum 002");
			buildingCollection.GetBuildingInBuildingList(1).AddRoomInBuliding("Blå rummet");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Mona Lisa", "Tavla", "Leonardo da Vinci");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Stol", "Myran", "Fritz Hansen");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Lampa", "PH Kotte pendel", "Poul Henningsen");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(1).AddArtInRoom("Säng", "Hästens", "Lars Nilssons");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(1).AddArtInRoom("Matta", "Ghom silke", "Carpet Vista");
			buildingCollection.GetBuildingInBuildingList(0).GetRoomInRoomList(0).AddArtInRoom("Venus födelse", "Oljemålning", "Sandro Botticelli,");
			buildingCollection.GetBuildingInBuildingList(1).GetRoomInRoomList(0).AddArtInRoom("David", "Marmorstaty", "Michelangelo Buonarroti");
			buildingCollection.GetBuildingInBuildingList(1).GetRoomInRoomList(0).AddArtInRoom("Las Meninas (1656)", "Oljemålning", "Diego Velázquez");
            int expectedAmountBuildings = 2;
            int expectedAmoutRommsInBuildingHotellet = 2;
            int expectedAmoutRommsInBuildingMuseet = 1;
            int expectedAmoutArtInRoom001 = 3;
            int expectedAmoutArtInRoom002 = 2;
            int expectedAmoutArtInBlåRummet = 2;

            // Act
            int amountBuildings = buildingCollection.GetBuildingList().Count;
            int amountRoomsInBuildingHotellet = buildingCollection.GetBuildingInBuildingList("Hotellet").GetRoomList().Count;
            int amountRoomsInBuildingMuseet = buildingCollection.GetBuildingInBuildingList("Museet").GetRoomList().Count;
            int amountArtInRoom001 = buildingCollection.GetBuildingInBuildingList("Hotellet").GetRoomInRoomList("Rum 001").GetArtList().Count;
            int amountArtInRoom002 = buildingCollection.GetBuildingInBuildingList("Hotellet").GetRoomInRoomList("Rum 002").GetArtList().Count;
            int amountArtInBlåRummet = buildingCollection.GetBuildingInBuildingList("Museet").GetRoomInRoomList("Blå rummet").GetArtList().Count;

            // Assert
            Assert.Equal(expectedAmountBuildings, amountBuildings);
            Assert.Equal(expectedAmoutRommsInBuildingHotellet, amountRoomsInBuildingHotellet);
            Assert.Equal(expectedAmoutRommsInBuildingMuseet, amountRoomsInBuildingMuseet);
            Assert.Equal(expectedAmoutArtInRoom001, amountArtInRoom001);
            Assert.Equal(expectedAmoutArtInRoom002, amountArtInRoom002);
            Assert.Equal(expectedAmoutArtInBlåRummet, amountArtInBlåRummet);
        }
    }
}
