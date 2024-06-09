using Godot;

[GlobalClass]
public partial class BuildingData : Resource{
    [Export] public string Name {get; set;}
    [Export] public int Tier {get; set;}
    [Export] public DistrictTypes DistrictType{get; set;}
    [Export] public Image CardImage{get; set;}
    [Export] public string CardDescription{get; set;}

    public BuildingData(){
        Name = "";
        Tier = 0;
        DistrictType = DistrictTypes.Undefined;
        CardImage = null;
        CardDescription = "";
    }
}