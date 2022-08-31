using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        //Когда в область действия триггера попадает что-то, проверить не является ли это "что-то" снарядом
        if (other.gameObject.tag == "Projectile")
        {
            Goal.goalMet = true;    //Если это снаряд, присвоить полю goalMet значение true
            //Изменить альфа-канал цвета, чтобы увеличить непрозрачность
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
