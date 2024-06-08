using Godot;

[GlobalClass]
public partial class BuildingData : Resource{
    [Export] string Name {get; set;}
    [Export] int Tier {get; set;}
    [Export] DistrictTypes DistrictType{get; set;}
    [Export] Image CardImage{get; set;}
    [Export] string CardDescription{get; set;}

    public BuildingData(){
        Name = "";
        Tier = 0;
        DistrictType = DistrictTypes.Undefined;
        CardImage = null;
        CardDescription = "";
    }
}