using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SofaUnity;

public class KeyActionsRecorder : MonoBehaviour
{
    public bool recordMode = true;
    public SofaObjectController m_controller = null;

    private List<float> m_keysTime = new List<float>();
    private List<int> m_keysAction = new List<int>();

    private int step = 0;
    private int currentAction = -1;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        recordMode = false;
#endif
        step = 0;
        if(!recordMode)
        {
            //m_keysAction = new List<int>(new int[] { 5, 5, -5, -5, 5, -5, -5, 5, -5, 4, 4, -4, -4, 6, 6, -6, 5, -5, -5, 5, 5, -5, -5, 5, -5, -5, 8, 8, -8, 8, -8, -8, 5, -5, -5, 4, -4, -4, 6, 6, -6, -6, 6, 6, -6, 4, -4, -4, 5, 5, -5, -5, 5, -5, -5, 2, -2, -2, 2, 2, -2, -2, 5, -5, -5, 10, 10, -10, -10, 10, -10, -10, 10, 10, -10, -10, 10, -10, -10, 10, -10, 10, 10, -10, -10, 8, -8, -8, 10, 10, -10, -10, 10, -10, 10, 10, 8, -10, -10, -8, -8, 10, -10, 10, 10, -10, 10, 10, -10, 8, -10, 8, -8, -8, 6, -6, 4, -4, -4, 5, 5, -5, -5, 6, -6, 4, 4, -4, 4, 4, -4, -4});
            //m_keysTime = new List<float>(new float[] { 8.82f, 8.84f, 9.099999f, 9.12f, 9.48f, 11.28f, 11.3f, 12.72f, 13.92f, 14.04f, 14.06f, 14.56f, 14.58f, 14.64f, 14.66f, 17.18f, 17.42f, 19.2f, 19.22f, 19.42f, 19.44f, 19.94f, 19.96f, 20.24f, 21.12f, 21.14f, 21.34f, 21.36f, 22.34f, 22.62f, 23.26f, 23.28f, 23.92f, 24.1f, 24.12f, 24.6f, 24.96f, 24.98f, 25.38f, 25.4f, 26.66f, 26.68f, 26.94f, 26.96f, 27.46f, 27.74f, 30.76f, 30.78f, 30.92f, 30.94f, 31.2f, 31.22f, 31.42f, 31.7f, 31.72f, 31.94f, 33.46f, 33.48f, 33.58f, 33.6f, 33.98f, 34f, 34.18f, 34.58f, 34.6f, 35.18f, 35.2f, 35.36f, 35.38f, 36.58f, 36.98f, 37f, 37.22f, 37.24f, 37.38f, 37.4f, 37.56f, 37.74f, 37.76f, 38.06f, 38.22f, 38.36f, 38.38f, 38.54f, 38.56f, 40.7f, 41.06f, 41.08f, 41.56f, 41.58f, 41.76f, 41.78f, 42.36f, 42.52f, 42.66f, 42.68f, 42.82f, 42.84f, 42.86f, 43.2f, 43.22f, 43.24f, 43.4f, 43.5f, 43.52f, 43.68f, 43.82f, 43.84f, 44f, 44f, 44.02f, 44.02f, 44.38f, 44.4f, 44.66f, 44.94f, 45.36f, 45.74f, 45.76f, 46.18f, 46.2f, 46.4f, 46.42f, 46.94f, 47.16f, 47.62f, 47.64f, 48.02f, 49.16f, 49.18f, 49.26f, 49.28f });

            m_keysAction = new List<int>(new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 6, 6, 6, 6, 6, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 4, 4, 4, 4, 4, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 4, 4, 4, 4, 4, 4, 4, 4, 4, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 5, 5, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 4, 4, 4, 4, 4, 4, 4, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 2, 2, 2, 2, 2, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 2, 2, 2, 2, 2, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 4, 4, 4, 4, 4, 4, 4, 10, 10, 10, 10, 10, 10, 10, 10, 10, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 6, 6, 6, 6, 6, 6, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 6, 6, 6, 6, 6, 6, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 6, 6, 6, 6, 6, 6, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 6, 6, 6, 6, 6, 6, 6, 6, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            m_keysTime = new List<float>(new float[] { 5.38f, 5.4f, 5.42f, 5.44f, 5.46f, 5.48f, 5.5f, 5.52f, 5.54f, 5.56f, 5.58f, 5.6f, 5.62f, 5.64f, 5.66f, 5.68f, 5.7f, 5.72f, 5.74f, 5.76f, 5.78f, 5.8f, 5.82f, 5.84f, 5.86f, 5.88f, 5.9f, 5.92f, 5.94f, 5.96f, 5.98f, 6f, 6.02f, 6.04f, 6.06f, 6.08f, 6.1f, 6.12f, 6.14f, 6.16f, 6.18f, 6.2f, 6.22f, 6.24f, 6.26f, 6.28f, 6.3f, 6.32f, 6.34f, 6.36f, 6.38f, 6.4f, 6.42f, 6.44f, 6.46f, 6.48f, 6.5f, 6.52f, 6.54f, 6.56f, 6.58f, 6.6f, 6.62f, 6.64f, 6.66f, 6.68f, 6.7f, 6.72f, 6.74f, 6.76f, 6.78f, 6.8f, 6.82f, 6.84f, 6.86f, 6.88f, 6.9f, 6.92f, 6.94f, 6.96f, 6.98f, 7f, 7.02f, 7.04f, 7.06f, 7.08f, 7.1f, 7.12f, 7.14f, 7.16f, 7.18f, 7.2f, 7.22f, 7.24f, 7.26f, 7.28f, 7.3f, 7.32f, 7.34f, 7.36f, 7.38f, 7.4f, 7.42f, 7.44f, 7.46f, 7.48f, 7.5f, 7.52f, 7.54f, 7.56f, 7.58f, 7.6f, 7.62f, 7.64f, 7.66f, 7.68f, 7.7f, 7.72f, 7.74f, 7.76f, 7.78f, 7.8f, 7.82f, 7.84f, 7.86f, 7.88f, 7.9f, 7.92f, 7.94f, 7.96f, 7.98f, 8f, 8.02f, 8.04f, 8.059999f, 8.08f, 8.099999f, 8.12f, 8.139999f, 8.16f, 8.179999f, 8.2f, 8.22f, 8.24f, 8.26f, 8.28f, 8.3f, 8.32f, 8.34f, 8.36f, 8.38f, 8.4f, 8.42f, 8.44f, 8.46f, 8.48f, 8.5f, 8.52f, 8.54f, 8.559999f, 8.58f, 8.599999f, 8.62f, 8.639999f, 8.66f, 8.679999f, 8.7f, 8.72f, 8.74f, 8.76f, 8.78f, 8.8f, 8.82f, 8.84f, 8.86f, 8.88f, 8.9f, 8.92f, 8.94f, 8.96f, 8.98f, 9f, 9.02f, 9.04f, 9.059999f, 9.08f, 9.099999f, 9.12f, 9.139999f, 9.16f, 9.179999f, 9.2f, 9.719999f, 9.74f, 9.76f, 9.78f, 9.8f, 9.82f, 9.84f, 9.86f, 9.88f, 9.9f, 9.92f, 9.94f, 9.96f, 9.98f, 10f, 10.02f, 10.04f, 10.06f, 10.08f, 10.1f, 10.12f, 10.14f, 10.16f, 10.18f, 10.2f, 10.22f, 10.24f, 10.26f, 10.28f, 10.3f, 10.32f, 10.34f, 10.36f, 10.38f, 10.4f, 10.42f, 10.44f, 10.46f, 10.48f, 10.5f, 10.52f, 10.54f, 10.56f, 10.58f, 10.6f, 10.62f, 10.64f, 10.66f, 10.68f, 10.7f, 10.72f, 10.74f, 10.76f, 10.78f, 10.8f, 10.82f, 10.84f, 10.86f, 10.88f, 10.9f, 10.92f, 10.94f, 10.96f, 10.98f, 11f, 11.02f, 11.04f, 11.06f, 11.08f, 11.3f, 11.32f, 11.34f, 11.36f, 11.38f, 11.4f, 11.42f, 11.44f, 11.46f, 11.48f, 11.5f, 11.52f, 11.54f, 11.56f, 11.58f, 11.6f, 11.62f, 12.14f, 12.16f, 12.18f, 12.2f, 12.22f, 12.24f, 12.26f, 12.28f, 12.3f, 12.32f, 12.34f, 12.36f, 12.38f, 12.4f, 12.42f, 13.1f, 13.12f, 13.14f, 13.16f, 13.18f, 13.2f, 13.22f, 13.24f, 13.26f, 13.28f, 13.3f, 13.32f, 13.34f, 13.36f, 13.38f, 13.6f, 13.62f, 13.64f, 13.66f, 13.68f, 13.7f, 13.72f, 13.74f, 13.76f, 13.78f, 13.8f, 13.82f, 13.84f, 13.86f, 13.88f, 13.9f, 13.92f, 13.94f, 13.96f, 14.18f, 14.2f, 14.22f, 14.24f, 14.26f, 14.28f, 14.3f, 14.32f, 14.34f, 14.36f, 14.38f, 14.4f, 14.42f, 14.44f, 14.46f, 14.48f, 14.5f, 14.52f, 14.54f, 14.56f, 14.58f, 14.6f, 14.62f, 14.64f, 14.66f, 14.68f, 14.7f, 14.72f, 15.72f, 15.74f, 15.76f, 15.78f, 15.8f, 15.82f, 15.84f, 15.86f, 15.88f, 15.9f, 15.92f, 15.94f, 15.96f, 16.5f, 16.52f, 16.54f, 16.56f, 16.58f, 16.6f, 16.62f, 16.64f, 16.66f, 16.68f, 16.7f, 16.72f, 16.74f, 16.76f, 16.78f, 16.8f, 16.82f, 16.84f, 16.86f, 16.88f, 16.9f, 16.92f, 16.94f, 16.96f, 16.98f, 17f, 17.02f, 17.04f, 17.06f, 17.08f, 17.1f, 17.12f, 17.14f, 17.16f, 17.18f, 17.2f, 17.22f, 17.24f, 17.26f, 17.28f, 17.3f, 17.32f, 17.34f, 17.36f, 17.38f, 17.4f, 17.42f, 17.44f, 17.46f, 17.48f, 17.5f, 17.52f, 17.54f, 17.56f, 17.58f, 17.6f, 17.62f, 17.64f, 18.06f, 18.08f, 18.1f, 18.12f, 18.14f, 18.16f, 18.18f, 18.2f, 18.22f, 18.24f, 18.26f, 18.28f, 18.3f, 18.32f, 18.34f, 18.36f, 18.38f, 18.4f, 18.42f, 18.44f, 18.46f, 18.48f, 18.5f, 18.52f, 18.54f, 18.56f, 18.58f, 18.6f, 18.62f, 18.64f, 18.66f, 18.68f, 18.7f, 18.72f, 18.74f, 18.76f, 18.78f, 18.8f, 18.82f, 18.84f, 18.86f, 18.88f, 18.9f, 18.92f, 18.94f, 18.96f, 18.98f, 19f, 19.02f, 19.04f, 19.06f, 19.08f, 19.1f, 19.12f, 19.14f, 19.16f, 19.18f, 19.2f, 19.22f, 19.24f, 19.26f, 19.28f, 19.3f, 19.32f, 19.34f, 19.36f, 19.38f, 19.4f, 19.42f, 19.44f, 19.46f, 19.48f, 19.5f, 19.52f, 19.54f, 19.56f, 19.58f, 19.6f, 19.62f, 19.64f, 19.66f, 19.68f, 19.7f, 19.72f, 19.74f, 19.76f, 19.78f, 19.8f, 19.82f, 19.84f, 19.86f, 19.88f, 19.9f, 19.92f, 19.94f, 19.96f, 19.98f, 20f, 20.02f, 20.04f, 20.06f, 20.08f, 20.1f, 20.12f, 20.14f, 20.16f, 20.18f, 20.2f, 20.74f, 20.76f, 20.78f, 20.8f, 20.82f, 20.84f, 20.86f, 20.88f, 20.9f, 20.92f, 20.94f, 20.96f, 20.98f, 21f, 21.02f, 21.04f, 21.06f, 21.08f, 21.1f, 21.12f, 21.14f, 21.16f, 21.18f, 21.2f, 21.22f, 21.24f, 21.26f, 21.28f, 21.3f, 21.32f, 21.34f, 21.36f, 21.38f, 21.4f, 21.42f, 21.44f, 21.46f, 21.48f, 21.5f, 21.52f, 21.54f, 21.56f, 21.58f, 21.6f, 21.62f, 21.64f, 21.66f, 21.68f, 21.7f, 21.72f, 21.74f, 21.76f, 21.78f, 21.8f, 21.82f, 21.84f, 21.86f, 21.88f, 21.9f, 21.92f, 21.94f, 21.96f, 21.98f, 22f, 22.02f, 22.36f, 22.38f, 22.4f, 22.42f, 22.44f, 22.46f, 22.48f, 22.5f, 22.52f, 22.54f, 22.56f, 22.58f, 22.6f, 22.62f, 26.94f, 26.96f, 26.98f, 27f, 27.02f, 27.04f, 27.34f, 27.36f, 27.38f, 27.4f, 27.42f, 27.44f, 27.46f, 27.7f, 27.72f, 27.74f, 27.76f, 28f, 28.02f, 28.04f, 28.06f, 28.08f, 28.1f, 28.12f, 28.4f, 28.42f, 28.44f, 28.46f, 28.48f, 28.52f, 28.54f, 28.56f, 28.58f, 28.6f, 28.62f, 28.64f, 28.66f, 28.78f, 28.8f, 28.82f, 28.84f, 28.86f, 28.88f, 28.9f, 29.02f, 29.04f, 29.06f, 29.08f, 29.46f, 29.48f, 29.5f, 29.52f, 29.54f, 29.56f, 29.58f, 29.6f, 30.04f, 30.06f, 30.08f, 30.1f, 30.12f, 30.38f, 30.4f, 30.42f, 30.44f, 30.46f, 30.48f, 30.5f, 30.52f, 30.54f, 30.56f, 30.72f, 30.74f, 30.76f, 30.78f, 30.8f, 30.82f, 31.18f, 31.2f, 31.22f, 31.24f, 31.26f, 31.28f, 31.3f, 31.32f, 31.52f, 31.54f, 31.56f, 31.58f, 31.6f, 31.74f, 31.76f, 31.78f, 31.8f, 31.82f, 31.84f, 31.96f, 31.98f, 32f, 32.02f, 32.18f, 32.2f, 32.22f, 32.24f, 32.34f, 32.36f, 32.38f, 32.4f, 32.42f, 32.44f, 32.46f, 32.48f, 32.5f, 32.52f, 32.54f, 32.56f, 32.58f, 32.6f, 32.62f, 32.64f, 32.66f, 32.76f, 32.78f, 32.8f, 32.82f, 33.22f, 33.24f, 33.26f, 33.28f, 33.3f, 33.32f, 33.34f, 33.44f, 33.46f, 33.48f, 33.5f, 33.52f, 33.54f, 33.56f, 33.68f, 33.7f, 33.72f, 33.74f, 33.88f, 33.9f, 33.92f, 33.94f, 33.96f, 33.98f, 34f, 34.02f, 34.16f, 34.18f, 34.2f, 34.22f, 34.24f, 34.26f, 34.28f, 34.36f, 34.38f, 34.4f, 34.42f, 34.44f, 34.46f, 34.62f, 34.64f, 34.66f, 34.68f, 34.98f, 35f, 35.02f, 35.18f, 35.2f, 35.22f, 35.24f, 35.26f, 35.28f, 35.46f, 35.48f, 35.5f, 35.52f, 35.54f, 35.56f, 35.74f, 35.76f, 35.78f, 35.8f, 35.82f, 35.84f, 35.86f, 35.88f, 36.3f, 36.32f, 36.34f, 36.36f, 36.38f, 36.4f, 36.52f, 36.54f, 36.56f, 36.58f, 36.6f, 36.62f, 36.78f, 36.8f, 36.82f, 36.84f, 36.86f, 36.88f, 36.9f, 36.92f, 37.22f, 37.24f, 37.26f, 37.28f, 37.3f, 37.32f, 37.34f, 37.36f, 37.5f, 37.52f, 37.54f, 37.56f, 37.58f, 37.6f, 37.78f, 37.8f, 37.82f, 37.84f, 37.86f, 37.98f, 38f, 38.02f, 38.04f, 38.06f, 38.08f, 38.24f, 38.26f, 38.28f, 38.3f, 38.32f, 38.34f, 38.36f, 38.38f, 38.52f, 38.54f, 38.56f, 38.58f, 38.6f, 38.62f, 38.64f, 39.06f, 39.08f, 39.1f, 39.12f, 39.14f, 39.16f, 39.18f, 39.4f, 39.42f, 39.44f, 39.56f, 39.58f, 39.6f, 39.62f, 39.64f, 39.66f, 39.9f, 39.92f, 39.94f, 39.96f, 39.98f, 40f, 40.02f, 40.2f, 40.22f, 40.24f, 40.26f, 40.28f, 40.3f, 40.38f, 40.4f, 40.42f, 40.44f, 40.46f, 40.48f, 40.5f, 40.64f, 40.66f, 40.68f, 40.7f, 40.72f, 40.94f, 40.96f, 40.98f, 41f, 41.02f, 41.04f, 41.12f, 41.14f, 41.16f, 41.32f, 41.34f, 41.36f, 41.38f, 41.4f, 41.58f, 41.6f, 41.62f, 41.64f, 41.66f, 41.78f, 41.8f, 41.82f, 41.84f, 41.86f, 41.88f, 41.9f, 42.08f, 42.1f, 42.12f, 42.14f, 42.16f, 42.18f, 42.26f, 42.28f, 42.3f, 42.32f, 42.34f, 42.36f, 42.52f, 42.54f, 42.56f, 42.58f, 42.72f, 42.74f, 42.76f, 42.78f, 42.8f, 42.82f, 42.84f, 42.98f, 43f, 43.02f, 43.04f, 43.06f, 43.08f, 43.1f, 43.12f, 43.14f, 43.3f, 43.32f, 43.34f, 43.36f, 43.38f, 43.54f, 43.56f, 43.58f, 43.6f, 43.62f, 43.64f, 43.66f, 43.86f, 43.88f, 43.9f, 43.92f, 43.94f, 43.96f, 43.98f, 44f, 44.02f, 44.04f, 44.06f, 44.08f, 44.1f, 44.12f, 44.14f, 44.16f, 44.18f, 44.2f, 44.22f, 44.24f, 44.26f, 44.74f, 44.76f, 44.78f, 44.8f, 44.82f, 44.84f, 44.96f, 44.98f, 45f, 45.02f, 45.04f, 45.06f, 45.08f, 45.1f, 45.38f, 45.4f, 45.42f, 45.44f, 45.46f, 45.48f, 45.5f, 45.52f, 45.66f, 45.68f, 45.7f, 45.72f, 45.74f, 45.76f, 45.78f, 45.8f, 45.98f, 46f, 46.02f, 46.04f, 46.06f, 46.08f, 46.26f, 46.28f, 46.3f, 46.32f, 46.34f, 46.36f, 46.52f, 46.54f, 46.56f, 46.58f, 46.6f, 46.62f, 46.64f, 46.96f, 46.98f, 47f, 47.02f, 47.04f, 47.06f, 47.08f, 47.1f, 47.64f, 47.66f, 47.68f, 47.7f, 47.72f, 47.74f, 47.92f, 47.94f, 47.96f, 47.98f, 48f, 48.02f, 48.04f, 48.06f, 48.2f, 48.22f, 48.24f, 48.26f, 48.28f, 48.3f, 48.32f, 48.34f, 48.36f, 48.38f, 48.8f, 48.82f, 48.84f, 48.86f, 48.88f, 48.9f, 48.92f, 48.94f, 48.96f, 48.98f, 49f, 49.02f, 49.04f, 49.06f, 49.08f, 49.1f, 49.12f, 49.14f, 49.16f, 49.18f, 49.2f, 49.22f, 49.24f, 49.26f, 49.28f, 49.3f, 49.32f, 49.34f, 49.36f, 49.38f, 49.4f, 49.42f, 49.44f, 49.46f, 49.48f, 49.5f, 49.52f, 49.54f, 49.56f, 49.58f, 49.6f, 49.62f, 49.64f, 49.66f, 49.68f, 49.7f, 49.72f, 49.74f, 49.76f, 49.78f, 49.8f, 49.82f, 49.84f, 49.86f, 49.88f, 49.9f, 49.92f, 49.94f, 49.96f, 49.98f, 50f, 50.02f, 50.04f, 50.06f, 50.08f, 50.1f, 50.12f, 50.14f, 50.16f, 50.18f, 50.2f, 50.22f, 50.24f, 50.26f, 50.28f, 50.3f, 50.32f, 50.34f, 50.36f, 50.38f, 50.4f, 50.42f, 50.44f, 50.46f, 50.48f, 50.5f, 50.52f, 50.54f, 50.56f, 50.58f, 50.6f, 50.62f, 50.64f, 50.66f, 50.68f, 50.7f, 50.72f, 50.74f, 50.76f });
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentTime = Time.time;

        if (!recordMode)
        {
            if (step >= m_keysTime.Count || m_controller == null)
                return;

            if (currentTime >= m_keysTime[step]) // change action
            {
                currentAction = m_keysAction[step];

                if (currentAction == 5)
                {
                    m_controller.MoveBackward();
                }
                else if (currentAction == 0)
                {
                    m_controller.MoveForward();
                }
                else if (currentAction == 6)
                {
                    m_controller.MoveRight();
                }
                else if (currentAction == 4)
                {
                    m_controller.MoveLeft();
                }
                else if (currentAction == 8)
                {
                    m_controller.MoveUp();
                }
                else if (currentAction == 2)
                {
                    m_controller.MoveDown();
                }
                else if (currentAction == 10)
                {
                    m_controller.ApplyAction();
                }

                step++;
            }            
        }
        else
        {
            if (Input.GetKey(KeyCode.Keypad5))
            {
                m_keysTime.Add(currentTime);
                m_keysAction.Add(5);
            }
            else if (Input.GetKey(KeyCode.Keypad0))
            {
                m_keysTime.Add(currentTime);
                m_keysAction.Add(0);
            }
            else if (Input.GetKey(KeyCode.Keypad6))
            {
                m_keysTime.Add(currentTime);
                m_keysAction.Add(6);
            }
            else if (Input.GetKey(KeyCode.Keypad4))
            {
                m_keysTime.Add(currentTime);
                m_keysAction.Add(4);
            }
            else if (Input.GetKey(KeyCode.Keypad8))
            {
                m_keysTime.Add(currentTime);
                m_keysAction.Add(8);
            }
            else if (Input.GetKey(KeyCode.Keypad2))
            {
                m_keysTime.Add(currentTime);
                m_keysAction.Add(2);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                m_keysTime.Add(currentTime);
                m_keysAction.Add(10);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                string result = "actions: ";
                for (int i = 0; i < m_keysAction.Count; i++)
                {
                    result += m_keysAction[i].ToString() + ", ";
                }
                Debug.Log(result);

                string result2 = "time: ";
                for (int i=0; i< m_keysTime.Count; i++)
                {
                    result2 += m_keysTime[i].ToString() + "f, ";
                }
                Debug.Log(result2);
            }
        }

    }
}
