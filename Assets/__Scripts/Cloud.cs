using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject cloudSphere;
    public int numSpheresMin = 6;   //Минимальное кол-во создаваемых экземпляров CloudSphere (в действительности на 1 больше фактического)
    public int numSpheresMax = 10;  //Максимальное кол-во создаваемых экземпляров CloudSphere (в действительности на 1 больше фактического)
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);    //Максимальное расстояние (положительное или отрицательное) CloudSphere от центра Cloud вдоль каждой оси
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);   // Диапазон масштаба вдоль оси X (создаются экземпляры CloudSphere, ширина которых больше высоты)
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);   // Диапазон масштаба вдоль оси Y
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);   // Диапазон масштаба вдоль оси Z
    //В конце функции Start() масштаб по оси Y каждого экземпляра CloudSphere уменьшается в зависимости от удаленности
    //от центра вдоль оси Х. Благодаря этому толщина облаков будет уменьшаться к краям. scalYMin - это наименьший
    //возможный масштаб по оси Y (чтобы избежать появления слишком тонких облаков)
    public float scaleYMin = 2f;

    private List<GameObject> spheres;

    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();

        int num = Random.Range(numSpheresMin, numSpheresMax);
        for (int i=0; i<num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);  // Родителем компонента Transform каждого объекта CloudSphere устанавливается текущий объект Cloud (Empty object)

            //Выбрать случайное местоположение
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            //Выбрать случайный масштаб
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //Скорректировать масштаб y по расстоянию х от центра
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);

            spTrans.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Restart();
        //}
    }

    void Restart()
    {
        //Удалить старые сферы, составляющие облако
        foreach (GameObject sp in spheres)
        {
            Destroy(sp);
        }
        Start();
    }
}
