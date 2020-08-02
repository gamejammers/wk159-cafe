[System.Serializable]
public class CharacterIngredients 
{
    public Ingredient ingredient;
    public int amount;
    public void AddIngredients(int _amount)
    {
        amount += _amount;
    }
    public bool RemoveIngredients(int _amount)
    {
        bool success = false;
        if(amount >= _amount)
        {
            amount -= _amount;
            success = true;
        }

        return success;
    }
    public void RemoveIngredients()
    {
        amount = 0;
    }
    public CharacterIngredients(Ingredient ingredient, int amount)
    {
        this.ingredient = ingredient;
        this.amount = amount;
    }
}
