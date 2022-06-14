namespace FormBuilder.Data.Seeders;

public  abstract class DataSeederBase
{
    public abstract bool NeedToSeed();
    public abstract int Seed(bool bypass = false);
}