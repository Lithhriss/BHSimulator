using System;
using System.Collections.Generic;
using System.Text;

public enum SetBonus
{
    None,
    //Raids
    AresBonus,
    DivinityBonus,
    MaruBonus,
    NWBonus,
    ArsenalBonus,

    //Trials
    UnityBonus,
    TrugdorBonus,
    BushidoBonus,
    TaldBonus,
    ConducBonus,
    LuminaryBonus,
    PolarisBonus,

    //WB orlag
    Lunarbonus,
    JynxBonus,
    OblitBonus,
    AgonyBonus,
    EruptionBonus,

    //WB nether
    IllustriousBonus,
    TatersBonus,
    InfernoBonus,
    RequiemBonus

}

public enum MythicBonus
{
    None,
    Pewpew,
    Hysteria_not_Implemented,
    Bub,
    Supersition,
    NightVisage,
    Consumption,
    Decay,
    Necrosis,

    Cometfell,
    Nebuleye_Not_Implemented,
    HoodOfMenace,
    CryptTunic,
    FishNBarrel,
    EngulfintArtifact,
    Nemesis,
    Bedlam,

    MoonCollage,
    LavaDefender,
    MagMasher,
    DeweDecal,
    ShiftingBreeze,
    BrightStar,
    Veilage,
    Flickerate


}

public class Set
{
    private SetBonus setBonus;
    private int PieceCount;

    public Set(SetBonus _setBonus, int pieceCount)
    {
        setBonus = _setBonus;
        PieceCount = pieceCount;
    }

    public SetBonus GetBonus()
    {
        return setBonus;
    }
    public int GetPieceCount()
    {
        return PieceCount;
    }
}

