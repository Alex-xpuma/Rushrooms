using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSwitcher : MonoBehaviour
{
    public Transform[] planets; // Массив планет
    public Transform[] points; // Массив пустых объектов, служащих точками назначения
    public Vector3[] scales; // Массив масштабов для каждой точки
    public float transitionDuration = 1f; // Длительность перемещения

    private bool isTransitioning = false;

    void Start()
    {
        if (planets.Length != points.Length || planets.Length != scales.Length)
        {
            Debug.LogError("The number of planets, points, and scales must be the same!");
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
            planets[i].position = points[i].position;
            planets[i].localScale = scales[i];
        }
    }

    private System.Collections.IEnumerator SwitchToNextPlanet()
    {
        isTransitioning = true;
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[planets.Length];
        Vector3[] targetPositions = new Vector3[planets.Length];
        Vector3[] startScales = new Vector3[planets.Length];
        Vector3[] targetScales = new Vector3[planets.Length];

        for (int i = 0; i < planets.Length; i++)
        {
            startPositions[i] = planets[i].position;
            startScales[i] = planets[i].localScale;
            int targetIndex = (i + 1) % points.Length; // Перемещаем каждую планету на одну позицию вперед
            targetPositions[i] = points[targetIndex].position;
            targetScales[i] = scales[targetIndex];
        }

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            t = Mathf.SmoothStep(0f, 1f, t); // Используем плавный шаг для плавного движения
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
                planets[i].localScale = Vector3.Lerp(startScales[i], targetScales[i], t);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < planets.Length; i++)
        {
            planets[i].position = targetPositions[i];
            planets[i].localScale = targetScales[i];
        }

        // Обновляем позиции точек и масштабов, чтобы следить за перемещением
        Transform tempPoint = points[0];
        Vector3 tempScale = scales[0];
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i] = points[i + 1];
            scales[i] = scales[i + 1];
        }
        points[points.Length - 1] = tempPoint;
        scales[scales.Length - 1] = tempScale;

        isTransitioning = false;
    }
}
