using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public enum DistrictTypes{
    Undefined,
    Utility,
    Residential,
    Cultural,
    Industrial,
    Government,
    Mercantile
}

public class District{

    public District(DistrictTypes newType, Building origin){
        type = newType;
        AddBuildingToDistrict(origin);
    }
    List<Building> buildings = new();

    int districtLevel = 0; 


    public DistrictTypes type = DistrictTypes.Undefined;

    public bool AddBuildingToDistrict(Building newBuilding){
        buildings.Add(newBuilding);
        newBuilding.SetGoverningDistrict(this);
        UpdateBuildings();
        
        return true;
    }

    public bool RemoveBuildingFromDistrict(Building newBuilding){
        bool Out =  buildings.Remove(newBuilding);
        
        UpdateBuildings();
        return Out;
    }

    public bool MergeDistricts(District otherDistrict){
        if(otherDistrict.type == type){
            buildings.AddRange(otherDistrict.buildings);
            foreach(Building building in otherDistrict.buildings){
                building.SetGoverningDistrict(this);
            }
            return true;
        }

        return false;
    }

    void UpdateBuildings(){
        foreach(Building building in buildings){
            building.OnDistrictUpdated();
        }
    }
}

//TODO:
// - Send event to Buildings on Add/Remove/Merge