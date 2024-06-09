using System.Collections.Generic;
using Godot;

public class Building{
    
    public Building(){
        
    }
    public Building(BuildingData buildingData){
        districtType = buildingData.DistrictType;
    }

    Vector2I GridPosition{
        get;set;
    }

    DistrictTypes districtType = DistrictTypes.Undefined;

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

        //- Check card logic
        //Iterate over neighbors, accounting for Isometric bullshit
        Vector2I[] neighbors = { new Vector2I(-1,-1), new Vector2I(0,-1), new Vector2I(-1,1), new Vector2I(0,1) };
        for (int i = 0; i < 4; i++){

            Building neighbor = gameboard.GetBuilding(GridPosition + neighbors[i]);
            if(neighbor != null){
                if(neighbor.districtType == districtType){
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
            new District(districtType, this);
        }

        GD.Print("District: " + (governingDistrict != null ? governingDistrict.type.ToString() : "Null"));
    }
}

//TODO: 
// - OnPlacementLogic():
//      
//      - Send event to neighbors