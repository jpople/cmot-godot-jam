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
    
        //Not sure I like this --Volv
        string resource = "";
        switch(type){
        case DistrictTypes.Residential:
            resource = "Residential";
            break;
        case DistrictTypes.Utility:
            resource = "Utility";
            break;
        case DistrictTypes.Cultural:
            resource = "Cultural";
            break;
        case DistrictTypes.Industrial:
            resource = "Industrial";
            break;
        case DistrictTypes.Government:
            resource = "Government";
            break;
        case DistrictTypes.Mercantile:
            resource = "Mercantile";
            break;

        default:
            break;
        }
        districtData = (DistrictData)GD.Load("res://Resources/Districts/"+resource+".tres");

        AddBuildingToDistrict(origin);
    }
    List<Building> buildings = new();

    int districtLevel = 0; 

    DistrictData districtData;

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

    protected virtual void CheckDistrictConditions(){
    }

    void UpdateBuildings(){
        CheckDistrictConditions();

        foreach(Building building in buildings){
            building.OnDistrictUpdated();
        }
    }
}