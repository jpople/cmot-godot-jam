using Godot;

public class Building{
    
    Vector2 GridPosition{
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

    void OnPlaced(Vector2 position){
        GridPosition = position;

        //- Check card logic

        //Iterate over [-1,0], [1,0], [0,-1], [0,1] for neighbors
        for (int i = 0; i < 4; i++){
            int xpos = (1-(i/2))*(i%2 * -1);
            int ypos = (i/2)*(1-(i%2 * -1));

            Building neighbor = /*Grid.GetBuilding(GridPosition.X-xpos, GridPosition.Y-ypos)*/new Building();
            if(neighbor != null){
                if(neighbor.districtType == districtType){
                    if(neighbor.governingDistrict != null){
                        if(governingDistrict == null){
                            neighbor.governingDistrict.AddBuildingToDistrict(this);
                        } else{
                            governingDistrict.MergeDistricts(neighbor.governingDistrict);
                        }
                    }
                }
            }
        }

        if(governingDistrict == null){
            new District(districtType, this);
        }

    }
}

//TODO: 
// - OnPlacementLogic():
//      
//      - Send event to neighbors