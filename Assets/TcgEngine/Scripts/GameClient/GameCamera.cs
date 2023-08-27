﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TcgEngine.Client
{
    /// <summary>
    /// Game Camera has some useful features such as shake
    /// Or raycasting the mouse to a world/screen position
    /// </summary>

    public class GameCamera : MonoBehaviour
    {
        private float shake_timer = 0f;
        private float shake_intensity = 1f;

        private Camera cam;
        private Vector3 shake_vector = Vector3.zero;
        private Vector3 start_pos;

        private static GameCamera instance;

        void Awake()
        {
            instance = this;
            start_pos = transform.position;
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            //Shake FX
            if (shake_timer > 0f)
            {
                shake_timer -= Time.deltaTime;
                shake_vector = new Vector3(Mathf.Cos(shake_timer * Mathf.PI * 16f) * 0.02f, Mathf.Sin(shake_timer * Mathf.PI * 12f) * 0.01f, 0f);
                transform.position = start_pos + shake_vector * shake_intensity;
            }
            else
            {
                transform.position = start_pos;
            }
        }

        public void Shake(float intensity = 1f, float duration = 1f)
        {
            shake_intensity = intensity;
            shake_timer = duration;
        }

        public Vector2 MouseToPercent(Vector3 mouse_pos)
        {
            float x = mouse_pos.x / Screen.width;
            float y = mouse_pos.y / Screen.height;
            return new Vector2(x, y);
        }

        public Ray MouseToRay(Vector3 mouse_pos)
        {
            return cam.ScreenPointToRay(mouse_pos);
        }

        public Vector3 MouseToWorld(Vector3 mouse_pos)
        {
            Vector3 wpos = cam.ScreenToWorldPoint(mouse_pos);
            wpos.z = 0f;
            return wpos;
        }

        public static Camera GetCamera()
        {
            return instance.cam;
        }

        public static GameCamera Get()
        {
            return instance;
        }
    }
}