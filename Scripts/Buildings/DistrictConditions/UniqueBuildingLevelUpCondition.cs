using Godot;
using System;
using System.Collections.Generic;

public partial class UniqueBuildingLevelUpCondition : DistrictLevelUpCondition{
    
    UniqueBuildingLevelUpCondition(){
        type = DistrictLevelUpTypes.UniqueActive;
    }
    public override int CalculateCondition(List<Building> districtBuildings) {
        int Out = 0;
        int UniqueActive = 0;

        for(int i = 0; i < districtBuildings.Count; i++){
            UniqueActive += districtBuildings[i].IsActive ? 1 : 0;
        }

        for(int i = 0; i < countPerLevel.Count; i++){
            if(countPerLevel[i] >= UniqueActive){
                Out++;
            }else{
                break;
            }
        }

        return Out;
    }
}