using System.Collections.Generic;
using Godot;

public class Building{
    
    public Building(){
        
    }
    public Building(BuildingData InBuildingData){
        buildingData = InBuildingData;
    }

    Vector2I GridPosition{
        get;set;
    }

    BuildingData buildingData = null;
    DistrictTypes DistrictType{get{ return buildingData != null ? buildingData.DistrictType : DistrictTypes.Undefined;} set{}}

    District governingDistrict = null;

    public bool IsActive {get;set;} = false;

    public void SetGoverningDistrict(District newDistrict){
        //District merge takes care of removing the other district
        governingDistrict = newDistrict;
        OnDistrictUpdated();
    }

    public void OnDistrictUpdated(){
        //TODO: Add Building Status update here (checking activity conditions and such)
    }

    public void OnPlaced(Vector2I position, Gameboard gameboard){
        GridPosition = position;
        GD.Print(GridPosition.ToString());
        //- Check card logic
        //Iterate over neighbors, accounting for Isometric bullshit
        List<Vector2I> neighborCoords = new(){ new Vector2I(-1,0)+GridPosition, new Vector2I(1,0)+GridPosition, new Vector2I(0,-1)+GridPosition, new Vector2I(0,1)+GridPosition };
        List<Building> neighbors = gameboard.GetBuildings(neighborCoords);
        foreach(var item in neighbors){
            GD.Print(item);
        }

        foreach(Building neighbor in neighbors){
            if(neighbor != null){
                if(neighbor.DistrictType == DistrictType){
                    if(neighbor.governingDistrict != null){
                        if(governingDistrict == null){
                            neighbor.governingDistrict.AddBuildingToDistrict(this);
                            GD.Print("Adding to existing District");
                        } else if (governingDistrict != neighbor.governingDistrict){
                            governingDistrict.MergeDistricts(neighbor.governingDistrict);
                            GD.Print("Merging District");
                        }
                    }
                }
            }
        }

        if(governingDistrict == null){
            new District(DistrictType, this);
        }

        GD.Print("District: " + (governingDistrict != null ? governingDistrict.type.ToString() : "Null"));
    }

    public override string ToString() {
        return buildingData.Name + " " + GridPosition.ToString();
    }
}

//TODO: 
// - OnPlacementLogic():
//      
//      - Send event to neighbors