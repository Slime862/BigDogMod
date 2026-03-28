namespace BigDogMod.Scripts.ChewPrep;

public readonly record struct BigDogChewPrepEffect(BigDogChewPrepEffectType Type, int Amount, bool ApplyToAllEnemies = false);
