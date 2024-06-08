using System.Collections.Generic;
using Godot;

public enum DistrictLevelUpTypes{
    BuildingCount,
    UniqueActive
}

[GlobalClass]
public partial class DistrictLevelUpCondition : Resource{
    [Export] public DistrictLevelUpTypes type = DistrictLevelUpTypes.BuildingCount;
    [Export] protected Godot.Collections.Array<int> countPerLevel;

    public virtual int CalculateCondition(List<Building> districtBuildings){
        int Out = 0;
        
        for(int i = 0; i < countPerLevel.Count; i++){
            if(countPerLevel[i] <= districtBuildings.Count){
                Out++;
            } else {
                break;
            }
        }

        return Out;
    }
}