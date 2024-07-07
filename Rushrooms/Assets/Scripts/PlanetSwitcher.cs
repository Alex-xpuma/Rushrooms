using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSwitcher : MonoBehaviour
{
    public Transform[] planets; // Массив планет
    public Transform[] points; // Массив пустых объектов, служащих точками назначения
    public Vector3[] scales; // Массив масштабов для каждой точки
    public float[] zPositions; // Массив значений Z для каждой точки
    public float transitionDuration = 1f; // Длительность перемещения


    private bool isTransitioning = false;

    void Start()
    {
        if (planets.Length != points.Length || planets.Length != scales.Length || planets.Length != zPositions.Length)
        {
            Debug.LogError("The number of planets, points, scales, and zPositions must be the same!");
            return;
        }

        ArrangePlanetsAtPoints();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTransitioning)
        {
            StartCoroutine(SwitchToNextPlanet());
        }
    }

    private void ArrangePlanetsAtPoints()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            Vector3 newPosition = points[i].position;
            newPosition.z = zPositions[i]; // Устанавливаем Z-координату
            planets[i].position = newPosition;
            planets[i].localScale = scales[i];
        }
    }

    private IEnumerator SwitchToNextPlanet()
    {
        isTransitioning = true;
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[planets.Length];
        Vector3[] targetPositions = new Vector3[planets.Length];
        Vector3[] startScales = new Vector3[planets.Length];
        Vector3[] targetScales = new Vector3[planets.Length];
        float[] startZPositions = new float[planets.Length];
        float[] targetZPositions = new float[planets.Length];

        for (int i = 0; i < planets.Length; i++)
        {
            startPositions[i] = planets[i].position;
            startScales[i] = planets[i].localScale;
            startZPositions[i] = planets[i].position.z;
            int targetIndex = (i + 1) % points.Length; // Перемещаем каждую планету на одну позицию вперед
            targetPositions[i] = points[targetIndex].position;
            targetScales[i] = scales[targetIndex];
            targetZPositions[i] = zPositions[targetIndex];
        }

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            t = Mathf.SmoothStep(0f, 1f, t); // Используем плавный шаг для плавного движения
            for (int i = 0; i < planets.Length; i++)
            {
                Vector3 newPosition = Vector3.Lerp(startPositions[i], targetPositions[i], t);
                newPosition.z = Mathf.Lerp(startZPositions[i], targetZPositions[i], t);
                planets[i].position = newPosition;
                planets[i].localScale = Vector3.Lerp(startScales[i], targetScales[i], t);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < planets.Length; i++)
        {
            Vector3 newPosition = targetPositions[i];
            newPosition.z = targetZPositions[i];
            planets[i].position = newPosition;
            planets[i].localScale = targetScales[i];
        }

        // Обновляем позиции точек и масштабов, чтобы следить за перемещением
        Transform tempPoint = points[0];
        Vector3 tempScale = scales[0];
        float tempZ = zPositions[0];
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i] = points[i + 1];
            scales[i] = scales[i + 1];
            zPositions[i] = zPositions[i + 1];
        }
        points[points.Length - 1] = tempPoint;
        scales[scales.Length - 1] = tempScale;
        zPositions[zPositions.Length - 1] = tempZ;

        isTransitioning = false;
    }


}
