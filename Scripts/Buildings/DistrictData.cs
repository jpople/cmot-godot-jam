using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class DistrictData : Resource{
    [Export] string Name {get; set;}
    [Export] Godot.Collections.Array<Resource> LevelUpConditions;
    

    public DistrictData(){
        Name = ""; 
        LevelUpConditions = new();
    }
}