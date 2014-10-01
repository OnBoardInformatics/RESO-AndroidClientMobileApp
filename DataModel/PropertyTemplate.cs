using System;
using System.Collections.Generic;
using System.Text;

namespace ODataClientApp.DataModel
{
    class PropertyTemplate
    {
        public string metadata;
        public ResponsePropertyResult[] value;
    }

    public class ResponsePropertyResult
    {
        public string Id;
        public string ListingId;
        public string OriginatingSystemName;
        public string OriginatingSystemKey;
        public string MLSStatus;
        public string ListingContractDate;
        public string ContractStatusChangeDate;
        public string PurchaseContractDate;
        public string CloseDate;
        public string StatusChangeTimestamp;
        public string DaysOnMarket;
        public string ClosePrice;
        public string ListPrice;
        public string OriginalListPrice;
        public string ListPriceLow;
        public string PreviousListPrice;
        public string VOWEntireListingDisplayYN;
        public string VOWAutomatedValuationDisplayYN;
        public string PhotosCount;
        public string PublicRemarks;
        public string ShowingInstructions;
        public string PropertyType;
        public string PropertySubType;
        public string LotSizeAcres;
        public string LotSizeSquareFeet;
        public string View;
        public string ViewYN;
        public string LotFeatures;
        public string CurrentUse;
        public string Topography;
        public string HorseYN;
        public string PoolFeatures;
        public string PoolPrivateYN;
        public string BedroomsTotal;
        public string BathroomsTotalstringeger;
        public string LivingArea;
        public string BuildingAreaTotal;
        public string GarageYN;
        public string GarageSpaces;
        public string StoriesTotal;
        public string YearBuilt;
        public string NewConstructionYN;
        public string TaxAnnualAmount;
        public string BasementYN;
        public string FirePlaceYN;
        public string ForcedAirYN;
        public string ForeClosureYN;
        public string GateCommunityYN;
        public string GolfCourseYN;
        public string WaterfrontYN;
        public string ElevatorYN;
        public string BalconyYN;
        public string GardenYN;
        public string MultiLevelYN;
        public string DiningRoomYN;
        public string LiveWorkYN;
        public string FurnishedYN;
        public string PetsYN;
        public string AirConditioningYN;
        public string UtilitiesIncludedYN;
        public string StorageYN;
        public string RoofGardenYN;
        public string BoardApprovalYN;
        public string ReoYN;
        public string ShortSaleYN;
        public string PreForeClosureYN;
        public string DistressedYN;

    }

    public class PropertyForDisplay
    {
        public string ListingId { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemKey { get; set; }
        public string MLSStatus { get; set; }
        public string ListingContractDate { get; set; }

        public PropertyForDisplay(string listingId, string originatingSystemName, string originatingSystemKey, string mlsStatus, string listingContractDate)
        {
            this.ListingId = listingId;
            this.OriginatingSystemName = originatingSystemName;
            this.OriginatingSystemKey = originatingSystemKey;
            this.MLSStatus = mlsStatus;
            this.ListingContractDate = listingContractDate;
        }
    }
}