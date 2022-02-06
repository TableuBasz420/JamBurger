using UnityEngine;

public class PlayerHeart : MonoBehaviour{

    public int hearts = 3;
    public int maxHearts = 3;

    [SerializeField] HeartSystem hs;

    private void Start() {
        hs.DrawHeart(hearts, maxHearts);
    }        
    
    public void damagePlayer (int dmg){ 
        if (hearts > 0){
            hearts -= dmg;
            hs.DrawHeart(hearts, maxHearts);
        }
    }

    public void healPlayer(int dmg)
    {
        if(hearts < maxHearts){
            hearts += dmg;
            hs.DrawHeart(hearts, maxHearts);
        }   
    }
}
